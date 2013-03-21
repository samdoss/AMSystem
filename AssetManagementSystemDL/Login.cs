using System;
using System.Collections.Generic;
using System.Text;

namespace Arthur.PurchaseEntryDL
{
    public class Login
    {
        private int _addEditOption;
        private int _companyId;
        private bool _loginAccess;
        private int _loginId;
        private Arthur.PurchaseEntryDL.ArthurConnection _myConnection;
        private string _oldPassword;
        private string _password;
        private string _role;
        private int _roleId;
        private Arthur.PurchaseEntryDL.ScreenMode _screenMode;
        private string _userName;

        public Login()
            : base()
        {
            this._myConnection = new Arthur.PurchaseEntryDL.ArthurConnection();
            this.AddEditOption = 0;
        }

        public Login(string LoginName, string LoginPassword, int LoginRoleID)
            : base()
        {
            this._myConnection = new Arthur.PurchaseEntryDL.ArthurConnection();
            this.AddEditOption = 0;
        }

        public int AddEditOption
        {
            get { return _addEditOption; }
            set { _addEditOption = value; }
        }

        public int CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        public bool LoginAccess
        {
            get { return _loginAccess; }
            set { _loginAccess = value; }
        }

        public int LoginID
        {
            get { return _loginId; }
            set { _loginId = value; }
        }

        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public int RoleID
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        public Arthur.PurchaseEntryDL.ScreenMode ScreenMode
        {
            get { return _screenMode; }
            set { _screenMode = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private Arthur.PurchaseEntryDL.TransactionResult AddEditLoginUser(Microsoft.Practices.EnterpriseLibrary.Data.Database db, System.Data.Common.DbTransaction transaction)
        {
            int i;
            Arthur.PurchaseEntryDL.TransactionResult transactionResult;
            bool bl;
            i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spAddEditLoginUser");
            db.AddInParameter(dbCommand, "LoginID", System.Data.DbType.Int32, this._loginId);
            db.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, this._companyId);
            db.AddInParameter(dbCommand, "UserName", System.Data.DbType.String, this._userName);
            db.AddInParameter(dbCommand, "Password", System.Data.DbType.String, this._password);
            db.AddInParameter(dbCommand, "RoleID", System.Data.DbType.String, this._roleId);
            db.AddInParameter(dbCommand, "LoginAccess", System.Data.DbType.Boolean, this._loginAccess);
            db.AddInParameter(dbCommand, "AddEditOption", System.Data.DbType.Int16, this._addEditOption);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            this._loginId = (int)db.GetParameterValue(dbCommand, "Return Value");
            bl = i != 0;
            if (!bl)
            {
                transactionResult = new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Failure, "User Name Already Exists. Please Change Some Other Name");
            }
            else
            {
                bl = i != -1;
                if (!bl)
                {
                    bl = this._addEditOption != 1;
                    if (!bl)
                    {
                        return new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Failure, "Failure Updated");
                    }
                    transactionResult = new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Failure, "Failure Adding");
                }
                else
                {
                    bl = this._addEditOption != 1;
                    transactionResult = !bl ? new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Success, "Successfully Updated") : new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Success, "Successfully Added");
                }
            }
            return transactionResult;
        }

        public bool ChangePassword()
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
            System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spUpdateUserPassword");
            database.AddInParameter(dbCommand, "UserName", System.Data.DbType.String, this._userName);
            database.AddInParameter(dbCommand, "OldPassword", System.Data.DbType.String, this._oldPassword);
            database.AddInParameter(dbCommand, "NewPassword", System.Data.DbType.String, this._password);
            return System.Convert.ToBoolean(database.ExecuteScalar(dbCommand));
        }

        public Arthur.PurchaseEntryDL.TransactionResult Commit()
        {
            Arthur.PurchaseEntryDL.TransactionResult transactionResult;
            Microsoft.Practices.EnterpriseLibrary.Data.Database database;
            System.Data.Common.DbConnection dbConnection;
            System.Data.Common.DbTransaction dbTransaction;
            System.Exception exception;
            bool bl;
            transactionResult = null;
            database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
            dbConnection = database.CreateConnection();
            try
            {
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    switch (this._screenMode)
                    {
                        case Arthur.PurchaseEntryDL.ScreenMode.Add:
                            transactionResult = AddEditLoginUser(database, dbTransaction);
                            bl = transactionResult.Status != Arthur.PurchaseEntryDL.TransactionStatus.Failure;
                            if (!bl)
                            {
                                dbTransaction.Rollback();
                                return transactionResult;
                            }
                            break;

                        case Arthur.PurchaseEntryDL.ScreenMode.Edit:
                            dbTransaction.Commit();
                            return transactionResult;

                        case Arthur.PurchaseEntryDL.ScreenMode.Delete:
                            transactionResult = DeleteLoginUser(database, dbTransaction);
                            bl = transactionResult.Status != Arthur.PurchaseEntryDL.TransactionStatus.Failure;
                            if (!bl)
                            {
                                dbTransaction.Rollback();
                                return transactionResult;
                            }
                            break;

                    }
                }
                catch (System.Exception exception1)
                {
                    exception = exception1;
                    dbTransaction.Rollback();
                    bl = this._screenMode != Arthur.PurchaseEntryDL.ScreenMode.Add;
                    if (!bl)
                    {
                        Arthur.PurchaseEntryDL.ErrorLog.LogErrorMessageToDb("", "Login.cs", "Commit For Add", exception.Message.ToString(), this._myConnection);
                        return new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Failure, "Failure Adding");
                    }
                    bl = this._screenMode != Arthur.PurchaseEntryDL.ScreenMode.Delete;
                    if (!bl)
                    {
                        Arthur.PurchaseEntryDL.ErrorLog.LogErrorMessageToDb("", "Login.cs", "Commit For Delete", exception.Message.ToString(), this._myConnection);
                        return new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Failure, "Failure Deleting");
                    }
                }
            }
            finally
            {
                bl = dbConnection == null;
                if (!bl)
                {
                    dbConnection.Dispose();
                }
            }
            return null;
        }

        private Arthur.PurchaseEntryDL.TransactionResult DeleteLoginUser(Microsoft.Practices.EnterpriseLibrary.Data.Database db, System.Data.Common.DbTransaction transaction)
        {
            Arthur.PurchaseEntryDL.TransactionResult transactionResult;
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spDeleteLoginUser");
            db.AddInParameter(dbCommand, "LoginID", System.Data.DbType.Int32, this._loginId);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            i = (int)db.GetParameterValue(dbCommand, "Return Value");
            transactionResult = i == -1 ? new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Failure, "Failure Deleted") : new Arthur.PurchaseEntryDL.TransactionResult(Arthur.PurchaseEntryDL.TransactionStatus.Success, "Successfully Deleted");
            return transactionResult;
        }

        public System.Data.DataSet GetLoginDetailList()
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Data.Database database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                dataSet = database.ExecuteDataSet(database.GetStoredProcCommand("spGetLoginDetailList"));
            }
            catch (System.Exception exception1)
            {
                Arthur.PurchaseEntryDL.ErrorLog.LogErrorMessageToDb("", "Login.cs", "GetLoginDetailList", exception1.Message.ToString(), this._myConnection);
            }
            return dataSet;
        }

        public System.Data.DataSet GetLoginRoleDetailList()
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Data.Database database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                dataSet = database.ExecuteDataSet(database.GetStoredProcCommand("spGetRoleList"));
            }
            catch (System.Exception exception1)
            {
                Arthur.PurchaseEntryDL.ErrorLog.LogErrorMessageToDb("", "Login.cs", "GetLoginRoleDetailList", exception1.Message.ToString(), this._myConnection);
            }
            return dataSet;
        }

        public bool GetLoginUserDetail()
        {
            bool bl;
            System.Data.SqlClient.SqlDataReader sqlDataReader;
            bool bl1;
            try
            {
                bl = false;
                Microsoft.Practices.EnterpriseLibrary.Data.Database database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetLoginUserDetail");
                database.AddInParameter(dbCommand, "UserName", System.Data.DbType.String, this._userName);
                database.AddInParameter(dbCommand, "Password", System.Data.DbType.String, this._password);
                sqlDataReader = database.ExecuteReader(dbCommand) as System.Data.SqlClient.SqlDataReader;
                try
                {
                    while (sqlDataReader.Read())
                    {
                        bl = true;
                        this._loginId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("LoginID"));
                        this._companyId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CompanyID"));
                        this._userName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("UserName"));
                        this._roleId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("RoleID"));
                        this._role = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Role"));
                        this._loginAccess = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("LoginAccess"));
                    }
                }
                finally
                {
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Dispose();
                    }
                }
                bl1 = bl;
            }
            catch (System.Exception exception1)
            {
                Arthur.PurchaseEntryDL.ErrorLog.LogErrorMessageToDb("", "Login.cs", "GetLoginUserDetail", exception1.Message.ToString(), this._myConnection);
                throw;
            }
            return bl1;
        }
    }
}
