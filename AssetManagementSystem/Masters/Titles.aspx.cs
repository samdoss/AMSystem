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
    public partial class Titles : System.Web.UI.Page
    {
        #region Private Variables

        Title _currentTitle = new Title();

        #endregion

        #region Page Events

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
                    GridViewProperties.AssignGridViewProperties(gvTitle);
                    gvTitle.Width = Unit.Percentage(97);
                    GetTitleDetails();

                    // For setting the focus to the first control in the modal popup
                    lnkAddTitles.Attributes.Add("onclick", "fnSetFocus('" + txtTitles.ClientID + "');");      
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "Page_Load", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "btnCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnTitleAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                _currentTitle = new Title();

                if (txtTitlesID.Text.ToString() != "0")
                    _currentTitle.AddEditOption = 1;
                else
                    _currentTitle.AddEditOption = 0;

                _currentTitle.TitleID = Convert.ToInt32(txtTitlesID.Text.ToString());
                _currentTitle.TitleDescription = txtTitles.Text.ToString();

                TransactionResult result;
                _currentTitle.ScreenMode = ScreenMode.Add;
                result = _currentTitle.Commit();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                if (result.Status == TransactionStatus.Success)
                {
                    txtTitles.Text = "";
                    txtTitlesID.Text = "0";
                    GetTitleDetails();
                }
                else
                {
                    txtTitlesID.Text = "0";
                    txtTitles.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "btnTitleAdd_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnTitleCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeEdit.Hide();
                GetTitleDetails();
                txtTitlesID.Text = "0";
                txtTitles.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "btnTitleCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvTitle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                txtTitlesID.Text = Convert.ToString(gvTitle.DataKeys[e.NewEditIndex].Value);
                txtTitles.Text = gvTitle.Rows[e.NewEditIndex].Cells[2].Text.ToString();
                mpeEdit.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "gvTitle_RowEditing", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvTitle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TransactionResult result;
                // Get the selected row's title id
                int titleIDToDelete = Convert.ToInt32(gvTitle.DataKeys[e.RowIndex].Value);

                // Delete the selected title
                _currentTitle = new Title();
                _currentTitle.TitleID = titleIDToDelete;
                _currentTitle.ScreenMode = ScreenMode.Delete;
                result = _currentTitle.Commit();

                // Display the status of the delete
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successfully deleted
                if (result.Status == TransactionStatus.Success)
                {
                    GetTitleDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "gvTitle_RowDeleting", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvTitle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text = ((gvTitle.PageIndex * gvTitle.PageSize) + e.Row.RowIndex + 1).ToString();
                    string strSingleQuotes = e.Row.Cells[2].Text;

                    strSingleQuotes = strSingleQuotes.Replace("'", "\\'");
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteTitle");
                    lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete \\'" + strSingleQuotes + "\\'?');");

                    e.Row.Cells[1].Visible = false;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "gvTitle_RowDataBound", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void lnkAddTitles_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtTitlesID.Text = "0";
                txtTitles.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "lnkAddTitles_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region Private Methods

        private void GetTitleDetails()
        {
            try
            {
                gvTitle.DataSource = _currentTitle.GetTitleList().Tables[0];
                gvTitle.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Titles.aspx", "", "GetTitleDetails", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion
    }
}
