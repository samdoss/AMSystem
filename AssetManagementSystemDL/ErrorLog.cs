using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace AssetManagementSystemDL
{
    public class ErrorLog
    {
        public static void LogErrorMessageToDb(string pageName, string className, string eventName, string errorMessage, CloudWebAppLibraryDL.CloudConnection _ArthurConnection)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase(_ArthurConnection.DatabaseName);
                DbCommand dbCommand = database.GetStoredProcCommand("spAddLogErrorMessageToDB");
                database.AddInParameter(dbCommand, "PageName", DbType.String, pageName);
                database.AddInParameter(dbCommand, "ClassName", DbType.String, className);
                database.AddInParameter(dbCommand, "EventName", DbType.String, eventName);
                database.AddInParameter(dbCommand, "ErrorMessage", DbType.String, errorMessage);
                database.ExecuteNonQuery(dbCommand);
            }
            catch
            {
            }
        }
    }
}
