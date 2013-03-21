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
    public partial class Countries : System.Web.UI.Page
    {
        #region Private Variables

        Country _currentCountry = new Country();
        State _currentState = new State();
        City _currentCity = new City();
        District _currentDistrict = new District();

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!IsPostBack)
                {
                    // Assign Common GridView Properties to all GridViews used in the page
                    GridViewProperties.AssignGridViewProperties(gvCountry);
                    GridViewProperties.AssignGridViewProperties(gvStates);
                    GridViewProperties.AssignGridViewProperties(gvCity);
                    GridViewProperties.AssignGridViewProperties(gvDistrict);

                    // Get Country Details
                    GetCountryDetails();

                    // For setting the focus to the first control in the modal popups
                    lnkAddEditCountry.Attributes.Add("onclick", "fnSetFocus('" + txtCountry.ClientID + "');");
                    lnkAddUpdateState.Attributes.Add("onclick", "fnSetFocus('" + ddlCountry.ClientID + "');");
                    lnkAddEditCity.Attributes.Add("onclick", "fnSetFocus('" + ddlCountryCity.ClientID + "');");
                    lnkAddEditDistrict.Attributes.Add("onclick", "fnSetFocus('" + ddlDistrictCountry.ClientID + "');");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "Page_Load", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region Events

        protected void btnCountryAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Create a new Country Object
                _currentCountry = new Country();

                // Set whether Add / Edit
                if (txtCountryID.Text.ToString() != "0")
                    _currentCountry.AddEditOption = 1;
                else
                    _currentCountry.AddEditOption = 0;

                // Assign values to the Country Object
                _currentCountry.CountryID = Convert.ToInt32(txtCountryID.Text.ToString());
                _currentCountry.CountryDescription = txtCountry.Text.ToString();

                // Add / Edit the Country
                TransactionResult result;
                _currentCountry.ScreenMode = ScreenMode.Add;
                result = _currentCountry.Commit();

                // Display the Status - Whether successfully saved or not
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successful get the country details
                if (result.Status == TransactionStatus.Success)
                {
                    GetCountryDetails();
                    txtCountryID.Text = "0";
                    txtCountry.Text = "";
                    tcntAllCSCTabs.ActiveTab = tpnlCountry;
                }
                else
                {
                    txtCountryID.Text = "0";
                    txtCountry.Text = "";
                    tcntAllCSCTabs.ActiveTab = tpnlCountry;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnCountryAdd_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnCountryCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Controls cleared on Cancel
                mpeEdit.Hide();
                GetCountryDetails();
                txtCountryID.Text = "0";
                txtCountry.Text = "";
                tcntAllCSCTabs.ActiveTab = tpnlCountry;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnCountryCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // When cancelled, transfer the control to the default (home) page
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnStateSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Create a new State Object
                _currentState = new State();

                // Set whether Add / Edit
                if (txtStateID.Text.ToString() != "0")
                    _currentState.AddEditOption = 1;
                else
                    _currentState.AddEditOption = 0;

                // Assign values to the State Object
                _currentState.CountryID = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
                _currentState.StateID = Convert.ToInt32(txtStateID.Text.ToString());
                _currentState.StateDescription = txtState.Text.ToString();

                // Add / Edit the State
                TransactionResult result;
                _currentState.ScreenMode = ScreenMode.Add;
                result = _currentState.Commit();

                // Display the Status - Whether successfully saved or not
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successful
                if (result.Status == TransactionStatus.Success)
                {
                    
                    txtStateID.Text = "0";
                    txtState.Text = "";
                    tcntAllCSCTabs.ActiveTab = tpnlStates;
                }
                else
                {
                    
                    txtStateID.Text = "0";
                    txtState.Text = "";
                    tcntAllCSCTabs.ActiveTab = tpnlStates;
                }
                ddlStateCountry_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnStateSave_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnStateCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Controls cleared on Cancel
                txtStateID.Text = "";
                txtState.Text = "";
                mpePopupState.Hide();
                tcntAllCSCTabs.ActiveTab = tpnlStates;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnStateCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void ddlStateCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlStateCountry.SelectedValue != "" && ddlStateCountry.SelectedValue != null)
                {
                    gvStates.DataSource = _currentState.GetStateListByCountryID(Convert.ToInt32(ddlStateCountry.SelectedValue));
                    gvStates.DataBind();
                    ddlCountry.SelectedValue = ddlStateCountry.SelectedValue;
                }
                else
                {
                    gvStates.DataSource = null;
                    gvStates.DataBind();
                    ddlCountry.SelectedIndex = 0;
                }
                tcntAllCSCTabs.ActiveTab = tpnlStates;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "ddlStateCountry_SelectedIndexChanged", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void ddlCityCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCityCountry.SelectedValue != "" && ddlCityCountry.SelectedValue != null)
                {
                    ddlCityState.Items.Clear();
                    ddlCityState.DataSource = _currentState.GetStateListByCountryID(Convert.ToInt32(ddlCityCountry.SelectedValue));
                    ddlCityState.DataTextField = "State";
                    ddlCityState.DataValueField = "StateID";
                    ddlCityState.DataBind();
                    ddlCityState.Items.Insert(0, "-- Select One --");
                    ddlCityState.Items[0].Value = "";
                    ddlCountryCity.SelectedValue = ddlCityCountry.SelectedValue;
                    ddlCountryCity_SelectedIndexChanged(sender, e);
                    mpbPopupCity.Hide();
                    gvCity.DataSource = null;
                    gvCity.DataBind();
                }
                else
                {
                    ddlCityState.Items.Clear();
                    ddlCityState.DataSource = null;
                    ddlCityState.DataBind();
                    ddlCityState.Items.Insert(0, "-- Select One --");
                    ddlCityState.Items[0].Value = "";
                    gvCity.DataSource = null;
                    gvCity.DataBind();
                    ddlCountryCity.SelectedIndex = 0;
                    ddlCountryCity_SelectedIndexChanged(sender, e);
                    mpbPopupCity.Hide();
                }
                tcntAllCSCTabs.ActiveTab = tpnlCity;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "ddlCityCountry_SelectedIndexChanged", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void ddlCityState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCityState.SelectedValue != "" && ddlCityState.SelectedValue != null)
                {
                    gvCity.DataSource = _currentCity.GetCityListByStateID(Convert.ToInt32(ddlCityState.SelectedValue));
                    gvCity.DataBind();
                    ddlCountryState.SelectedValue = ddlCityState.SelectedValue;
                    
                }
                else
                {
                    gvCity.DataSource = null;
                    gvCity.DataBind();
                    ddlCountryState.SelectedIndex = 0;
                    
                }
                tcntAllCSCTabs.ActiveTab = tpnlCity;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "ddlCityState_SelectedIndexChanged", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnCitySave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Create a new City Object
                _currentCity = new City();

                // Set whether Add / Edit
                if (txtCityID.Text.ToString() != "0")
                    _currentCity.AddEditOption = 1;
                else
                    _currentCity.AddEditOption = 0;

                // Assign values to the City Object
                _currentCity.StateID = Convert.ToInt32(ddlCountryState.SelectedValue.ToString());
                _currentCity.CityID = Convert.ToInt32(txtCityID.Text.ToString());
                _currentCity.CityDescription = txtCity.Text.ToString();

                // Add / Edit the City
                TransactionResult result;
                _currentCity.ScreenMode = ScreenMode.Add;
                result = _currentCity.Commit();

                // Display the Status - Whether successfully saved or not
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successful
                if (result.Status == TransactionStatus.Success)
                {
                    ddlCityState_SelectedIndexChanged(sender, e);
                    txtCityID.Text = "";
                    txtCity.Text = "";
                    tcntAllCSCTabs.ActiveTab = tpnlCity;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnCitySave_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnCityCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Controls cleared on Cancel
                txtCityID.Text = "";
                txtCity.Text = "";
                mpbPopupCity.Hide();
                tcntAllCSCTabs.ActiveTab = tpnlCity;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnCityCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void ddlCountryCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCountryCity.SelectedValue != "" && ddlCountryCity.SelectedValue != null)
                {
                    ddlCountryState.Items.Clear();
                    ddlCountryState.DataSource = _currentState.GetStateListByCountryID(Convert.ToInt32(ddlCountryCity.SelectedValue));
                    ddlCountryState.DataTextField = "State";
                    ddlCountryState.DataValueField = "StateID";
                    ddlCountryState.DataBind();
                    ddlCountryState.Items.Insert(0, "-- Select One --");
                    ddlCountryState.Items[0].Value = "";
                }
                else
                {
                    ddlCountryState.Items.Clear();
                    ddlCountryState.DataSource = null;
                    ddlCountryState.DataBind();
                    ddlCountryState.Items.Insert(0, "-- Select One --");
                    ddlCountryState.Items[0].Value = "";
                }
                tcntAllCSCTabs.ActiveTab = tpnlCity;
                mpbPopupCity.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "ddlCountryCity_SelectedIndexChanged", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        //District

        protected void ddlDistrictCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistrictCountries.SelectedValue != "" && ddlDistrictCountries.SelectedValue != null)
                {
                    ddlDistrictStates.Items.Clear();
                    ddlDistrictStates.DataSource = _currentState.GetStateListByCountryID(Convert.ToInt32(ddlDistrictCountries.SelectedValue));
                    ddlDistrictStates.DataTextField = "State";
                    ddlDistrictStates.DataValueField = "StateID";
                    ddlDistrictStates.DataBind();
                    ddlDistrictStates.Items.Insert(0, "-- Select One --");
                    ddlDistrictStates.Items[0].Value = "";
                    ddlDistrictCountry.SelectedValue = ddlDistrictCountries.SelectedValue;
                    ddlDistrictCountry_SelectedIndexChanged(sender, e);
                    mpePopUpDistrict.Hide();
                    gvDistrict.DataSource = null;
                    gvDistrict.DataBind();
                }
                else
                {
                    ddlDistrictStates.Items.Clear();
                    ddlDistrictStates.DataSource = null;
                    ddlDistrictStates.DataBind();
                    ddlDistrictStates.Items.Insert(0, "-- Select One --");
                    ddlDistrictStates.Items[0].Value = "";
                    gvDistrict.DataSource = null;
                    gvDistrict.DataBind();
                    ddlDistrictCountry.SelectedIndex = 0;
                }
                tcntAllCSCTabs.ActiveTab = tpnlDistrict;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "ddlDistrictCountries_SelectedIndexChanged", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void ddlDistrictStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistrictStates.SelectedValue != "" && ddlDistrictStates.SelectedValue != null)
                {
                    gvDistrict.DataSource = _currentDistrict.GetDistrictListByStateID(Convert.ToInt32(ddlDistrictStates.SelectedValue));
                    gvDistrict.DataBind();
                    ddlDistrictState.SelectedValue = ddlDistrictStates.SelectedValue;
                }
                else
                {
                    gvDistrict.DataSource = null;
                    gvDistrict.DataBind();
                    ddlDistrictState.SelectedIndex = 0;
                }
                tcntAllCSCTabs.ActiveTab = tpnlDistrict;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "ddlDistrictStates_SelectedIndexChanged", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnDistrictSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Create a new District Object
                _currentDistrict = new District();

                // Set whether Add / Edit
                if (txtDistrictID.Text.ToString() != "0")
                    _currentDistrict.AddEditOption = 1;
                else
                    _currentDistrict.AddEditOption = 0;

                // Assign values to the District Object
                _currentDistrict.StateID = Convert.ToInt32(ddlDistrictState.SelectedValue.ToString());
                _currentDistrict.DistrictID = Convert.ToInt32(txtDistrictID.Text.ToString());
                _currentDistrict.DistrictDescription = txtDistrict.Text.ToString();

                // Add / Edit the District
                TransactionResult result;
                _currentDistrict.ScreenMode = ScreenMode.Add;
                result = _currentDistrict.Commit();

                // Display the Status - Whether successfully saved or not
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successful
                if (result.Status == TransactionStatus.Success)
                {
                    ddlDistrictStates_SelectedIndexChanged(sender, e);
                    txtDistrictID.Text = "";
                    txtDistrict.Text = "";
                    tcntAllCSCTabs.ActiveTab = tpnlDistrict;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnDistrictSave_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void btnDistrictCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Controls cleared on Cancel
                txtDistrictID.Text = "";
                txtDistrict.Text = "";
                mpePopUpDistrict.Hide();
                tcntAllCSCTabs.ActiveTab = tpnlDistrict;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "btnDistrictCancel_Click", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void ddlDistrictCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistrictCountry.SelectedValue != "" && ddlDistrictCountry.SelectedValue != null)
                {
                    ddlDistrictState.Items.Clear();
                    ddlDistrictState.DataSource = _currentState.GetStateListByCountryID(Convert.ToInt32(ddlDistrictCountry.SelectedValue));
                    ddlDistrictState.DataTextField = "State";
                    ddlDistrictState.DataValueField = "StateID";
                    ddlDistrictState.DataBind();
                    ddlDistrictState.Items.Insert(0, "-- Select One --");
                    ddlDistrictState.Items[0].Value = "";
                }
                else
                {
                    ddlDistrictState.Items.Clear();
                    ddlDistrictState.DataSource = null;
                    ddlDistrictState.DataBind();
                    ddlDistrictState.Items.Insert(0, "-- Select One --");
                    ddlDistrictState.Items[0].Value = "";
                }
                tcntAllCSCTabs.ActiveTab = tpnlDistrict;
                mpePopUpDistrict.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "ddlDistrictCountry_SelectedIndexChanged", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region Grid Events

        #region Country

        protected void gvCountry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Set the Serial Number
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text = ((gvCountry.PageIndex * gvCountry.PageSize) + e.Row.RowIndex + 1).ToString();
                    string strSingleQuotes = e.Row.Cells[2].Text;

                    strSingleQuotes = strSingleQuotes.Replace("'", "\\'");
                    // Attach Confirmation to the Delete Image Button
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteCountry");
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
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvCountry_RowDataBound", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvCountry_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                // Set the Control Values in the Popup from the Selected Grid Row
                txtCountryID.Text = Convert.ToString(gvCountry.DataKeys[e.NewEditIndex].Value);
                txtCountry.Text = gvCountry.Rows[e.NewEditIndex].Cells[2].Text.ToString();

                // Show the Popup
                mpeEdit.Show();

                // Set the Active Tab
                tcntAllCSCTabs.ActiveTab = tpnlCountry;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvCountry_RowEditing", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvCountry_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TransactionResult result;
                // Get the selected row's Country id
                int countryIDToDelete = Convert.ToInt32(gvCountry.DataKeys[e.RowIndex].Value);

                // Delete the selected Country
                _currentCountry = new Country();
                _currentCountry.CountryID = countryIDToDelete;
                _currentCountry.ScreenMode = ScreenMode.Delete;
                result = _currentCountry.Commit();

                // Display the status of the delete
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successfully deleted, get the country details
                if (result.Status == TransactionStatus.Success)
                {
                    GetCountryDetails();
                    tcntAllCSCTabs.ActiveTab = tpnlCountry;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvCountry_RowDeleting", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region State

        protected void gvStates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Set the Serial Number
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text = ((gvStates.PageIndex * gvStates.PageSize) + e.Row.RowIndex + 1).ToString();
                    string strSingleQuotes = e.Row.Cells[3].Text;

                    strSingleQuotes = strSingleQuotes.Replace("'", "\\'");
                    // Attach Confirmation to the Delete Image Button
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteState");
                    lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete \\'" + strSingleQuotes + "\\'?');");

                    // Hide Columns
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    // Hide Column Headers
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvStates_RowDataBound", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvStates_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                // Set the Control Values in the Popup from the Selected Grid Row
                ddlCountry.SelectedValue = Convert.ToString(gvStates.Rows[e.NewEditIndex].Cells[2].Text.ToString());
                txtStateID.Text = Convert.ToString(gvStates.DataKeys[e.NewEditIndex].Value);
                txtState.Text = gvStates.Rows[e.NewEditIndex].Cells[3].Text.ToString();

                // Show the Popup
                mpePopupState.Show();

                // Set the Active Tab
                tcntAllCSCTabs.ActiveTab = tpnlStates;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvStates_RowEditing", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvStates_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TransactionResult result;
                // Get the selected row's state id
                int stateIDToDelete = Convert.ToInt32(gvStates.DataKeys[e.RowIndex].Value);

                // Delete the selected state
                _currentState = new State();
                _currentState.StateID = stateIDToDelete;
                _currentState.ScreenMode = ScreenMode.Delete;
                result = _currentState.Commit();

                // Display the status of the delete
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successfully deleted
                if (result.Status == TransactionStatus.Success)
                {
                    ddlStateCountry_SelectedIndexChanged(sender, e);
                    tcntAllCSCTabs.ActiveTab = tpnlStates;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvStates_RowDeleting", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region City

        protected void gvCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Set the Serial Number
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text = ((gvCity.PageIndex * gvCity.PageSize) + e.Row.RowIndex + 1).ToString();
                    string strSingleQuotes = e.Row.Cells[3].Text;

                    strSingleQuotes = strSingleQuotes.Replace("'", "\\'");
                    // Attach Confirmation to the Delete Image Button
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteCity");
                    lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete \\'" + strSingleQuotes + "\\'?');");

                    // Hide Columns
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    // Hide Column Headers
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvCity_RowDataBound", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvCity_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TransactionResult result;
                // Get the selected row's City id
                int cityIDToDelete = Convert.ToInt32(gvCity.DataKeys[e.RowIndex].Value);

                // Delete the selected City
                _currentCity = new City();
                _currentCity.CityID = cityIDToDelete;
                _currentCity.ScreenMode = ScreenMode.Delete;
                result = _currentCity.Commit();

                // Display the status of the delete
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successfully deleted
                if (result.Status == TransactionStatus.Success)
                {
                    ddlCityState_SelectedIndexChanged(sender, e);
                    tcntAllCSCTabs.ActiveTab = tpnlCity;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvCity_RowDeleting", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvCity_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                // Set the Control Values in the Popup from the Selected Grid Row
                int CountryID = 0;
                CountryID = Convert.ToInt32(gvCity.Rows[e.NewEditIndex].Cells[2].Text.ToString());
                _currentState = new State(CountryID, true);
                ddlCountryCity.SelectedValue = Convert.ToString(_currentState.CountryID);

                ddlCountryCity_SelectedIndexChanged(sender, e);
                ddlCountryState.SelectedValue = Convert.ToString(gvCity.Rows[e.NewEditIndex].Cells[2].Text.ToString());

                txtCityID.Text = Convert.ToString(gvCity.DataKeys[e.NewEditIndex].Value);
                txtCity.Text = gvCity.Rows[e.NewEditIndex].Cells[3].Text.ToString();

                // Show the Popup
                mpbPopupCity.Show();

                // Set the Active Tab
                tcntAllCSCTabs.ActiveTab = tpnlCity;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvCity_RowEditing", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #region District

        protected void gvDistrict_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TransactionResult result;
                // Get the selected row's District id
                int districtIDToDelete = Convert.ToInt32(gvDistrict.DataKeys[e.RowIndex].Value);

                // Delete the selected District
                _currentDistrict = new District();
                _currentDistrict.DistrictID = districtIDToDelete;
                _currentDistrict.ScreenMode = ScreenMode.Delete;
                result = _currentDistrict.Commit();

                // Display the status of the delete
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                // If successfully deleted
                if (result.Status == TransactionStatus.Success)
                {
                    ddlDistrictStates_SelectedIndexChanged(sender, e);
                    tcntAllCSCTabs.ActiveTab = tpnlDistrict;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvDistrict_RowDeleting", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvDistrict_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                // Set the Control Values in the Popup from the Selected Grid Row
                int CountryID = 0;
                CountryID = Convert.ToInt32(gvDistrict.Rows[e.NewEditIndex].Cells[2].Text.ToString());
                _currentState = new State(CountryID, true);
                ddlDistrictCountry.SelectedValue = Convert.ToString(_currentState.CountryID);

                ddlDistrictCountry_SelectedIndexChanged(sender, e);
                ddlDistrictState.SelectedValue = Convert.ToString(gvDistrict.Rows[e.NewEditIndex].Cells[2].Text.ToString());

                txtDistrictID.Text = Convert.ToString(gvDistrict.DataKeys[e.NewEditIndex].Value);
                txtDistrict.Text = gvDistrict.Rows[e.NewEditIndex].Cells[3].Text.ToString();

                // Show the Popup
                mpePopUpDistrict.Show();

                // Set the Active Tab
                tcntAllCSCTabs.ActiveTab = tpnlDistrict;
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvDistrict_RowEditing", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        protected void gvDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Set the Serial Number
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text = ((gvDistrict.PageIndex * gvDistrict.PageSize) + e.Row.RowIndex + 1).ToString();
                    string strSingleQuotes = e.Row.Cells[3].Text;

                    strSingleQuotes = strSingleQuotes.Replace("'", "\\'");
                    // Attach Confirmation to the Delete Image Button
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteDistrict");
                    lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete \\'" + strSingleQuotes + "\\'?');");

                    // Hide Columns
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    // Hide Column Headers
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "gvDistrict_RowDataBound", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Get Country Details
        /// </summary>
        private void GetCountryDetails()
        {
            try
            {
                gvCountry.DataSource = _currentCountry.GetCountryList().Tables[0];
                gvCountry.DataBind();

                ddlCountry.DataSource = _currentCountry.GetCountryList().Tables[0];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();

                ddlStateCountry.DataSource = _currentCountry.GetCountryList().Tables[0];
                ddlStateCountry.DataTextField = "Country";
                ddlStateCountry.DataValueField = "CountryID";
                ddlStateCountry.DataBind();

                ddlCityCountry.DataSource = _currentCountry.GetCountryList().Tables[0];
                ddlCityCountry.DataTextField = "Country";
                ddlCityCountry.DataValueField = "CountryID";
                ddlCityCountry.DataBind();

                ddlCountryCity.DataSource = _currentCountry.GetCountryList().Tables[0];
                ddlCountryCity.DataTextField = "Country";
                ddlCountryCity.DataValueField = "CountryID";
                ddlCountryCity.DataBind();

                ddlDistrictCountries.DataSource = _currentCountry.GetCountryList().Tables[0];
                ddlDistrictCountries.DataTextField = "Country";
                ddlDistrictCountries.DataValueField = "CountryID";
                ddlDistrictCountries.DataBind();

                ddlDistrictCountry.DataSource = _currentCountry.GetCountryList().Tables[0];
                ddlDistrictCountry.DataTextField = "Country";
                ddlDistrictCountry.DataValueField = "CountryID";
                ddlDistrictCountry.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("Countries.aspx", "", "GetCountryDetails", ex.Message.ToString(), new ECGroupConnection());
                throw;
            }
        }

        #endregion
    }
}
