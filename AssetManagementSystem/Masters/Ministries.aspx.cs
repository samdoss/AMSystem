using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ECGroup.ForeignMissionFoundation.BusinessLayer;

namespace ECGroup.ForeignMissionFoundation
{
    public partial class Ministries : System.Web.UI.Page
    {
        #region Private Variables

        Ministry _currentMinistry = new Ministry();

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!IsPostBack)
                {
                    // Set ViewState Variables
                    ViewState["SortDirection"] = "ASC";
                    ViewState["SortExpression"] = "Ministry";

                    // Assign Common GridView Properties to all GridViews used in the page
                    GridViewProperties.AssignGridViewProperties(gvMinistries);
                    gvMinistries.Width = Unit.Percentage(97);
                    GetMinistryDetails();

                    // For setting the focus to the first control in the modal popup
                    lnkAddMinistry.Attributes.Add("onclick", "fnSetFocus('" + txtMinistry.ClientID + "');");                    
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "Page_Load", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region Events

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // When cancelled, transfer the control to the default (home) page
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "btnCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnMinistryAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Create a new Ministry Object
                _currentMinistry = new Ministry();

                // Set whether Add / Edit
                if (txtMinistryID.Text.ToString() != "0")
                    _currentMinistry.AddEditOption = 1;
                else
                    _currentMinistry.AddEditOption = 0;

                // Assign values to the Ministry Object
                _currentMinistry.MinistryID = Convert.ToInt32(txtMinistryID.Text.ToString());
                _currentMinistry.MinistryDescription = txtMinistry.Text.ToString();

                // Add / Edit the Ministry
                TransactionResult result;
                _currentMinistry.ScreenMode = ScreenMode.Add;
                result = _currentMinistry.Commit();

                // Display the Status - Whether successfully saved or not
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successful, get the Ministry details
                if (result.Status == TransactionStatus.Success)
                {
                    GetMinistryDetails();
                    txtMinistry.Text = "";
                    txtMinistryID.Text = "";
                }
                else
                {
                    txtMinistry.Text = "";
                    txtMinistryID.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "btnMinistryAdd_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnMinistryCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeEdit.Hide();
                GetMinistryDetails();
                txtMinistry.Text = "";
                txtMinistryID.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "btnMinistryCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void lnkAddMinistry_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Clear text fields
                txtMinistryID.Text = "0";
                txtMinistry.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "lnkAddMinistry_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region Grid Events

        protected void gvMinistries_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Set the Serial Number
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text = ((gvMinistries.PageIndex * gvMinistries.PageSize) + e.Row.RowIndex + 1).ToString();

                    string strSingleQuotes = e.Row.Cells[2].Text;

                    strSingleQuotes = strSingleQuotes.Replace("'", "\\'");
                    // Attach Confirmation to the Delete Image Button
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteMinistry");
                    lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete \\'" + strSingleQuotes + "\\'?');");

                    // Hide Columns
                    e.Row.Cells[1].Visible = false;
                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    // Hide Column Headers
                    e.Row.Cells[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "gvMinistries_RowDataBound", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvMinistries_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                // Set the Control Values in the Popup from the Selected Grid Row
                txtMinistryID.Text = Convert.ToString(gvMinistries.DataKeys[e.NewEditIndex].Value);
                txtMinistry.Text = gvMinistries.Rows[e.NewEditIndex].Cells[2].Text.ToString();

                // Show the Popup
                mpeEdit.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "gvMinistries_RowEditing", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvMinistries_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TransactionResult result;
                // Get the selected row's Ministry id
                int ministryIDToDelete = Convert.ToInt32(gvMinistries.DataKeys[e.RowIndex].Value);

                // Delete the selected Ministry
                _currentMinistry = new Ministry();
                _currentMinistry.MinistryID = ministryIDToDelete;
                _currentMinistry.ScreenMode = ScreenMode.Delete;
                result = _currentMinistry.Commit();

                // Display the status of the delete
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successfully deleted, get the Ministry Details
                if (result.Status == TransactionStatus.Success)
                {
                    GetMinistryDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "gvMinistries_RowDeleting", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get Ministry Details
        /// </summary>
        private void GetMinistryDetails()
        {
            try
            {
                gvMinistries.DataSource = _currentMinistry.GetMinistryList().Tables[0];
                gvMinistries.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Ministries.aspx", "", "GetMinistryDetails", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion
    }
}
