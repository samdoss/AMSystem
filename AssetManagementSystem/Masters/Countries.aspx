<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Countries.aspx.cs" Inherits="ECGroup.ForeignMissionFoundation.Countries" %>

<%@ Register Src="../UserControls/Footer.ascx" TagName="FMFFooter" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/Header.ascx" TagName="FMFHeader" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/FMFMenu.ascx" TagName="FMFMenu" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Country/State/City/District</title>

    <script type="text/javascript">
        var clientid;
        function fnSetFocus(txtClientId)
        {
        	clientid=txtClientId;
        	setTimeout("fnFocus()",1000);            
        }
  
        function fnFocus()
        {
            eval("document.getElementById('"+clientid+"').focus()");
        }

    </script>

    <link href="../Style/FMFStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../Style/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmCountryMasterPage" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table cellpadding="0" cellspacing="0" border="0" width="975" align="center">
            <tr>
                <td colspan="2">
                    <uc1:FMFHeader ID="ECHeader1" runat="server"></uc1:FMFHeader>
                </td>
            </tr>
            <tr>
                <td valign="top" width="975">
                    <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                        <tr>
                            <td valign="top" width="200" height="100%" background="../images/td-middle.jpg">
                                <uc2:FMFMenu ID="ECMenu1" runat="server"></uc2:FMFMenu>
                            </td>
                            <td align="center" width="775" height="100%" valign="top" background="../images/td-content-middle.jpg">
                                <table cellpadding="0" cellspacing="0" border="0" width="775" height="100%">
                                    <tr>
                                        <td background="../images/td-content-top.jpg" width="775" height="25" class="Menu">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="../images/td-content-middle.jpg" align="center" valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0" width="750" height="100%">
                                                <tr>
                                                    <td valign="top" height="100%">
                                                        <div style="word-spacing: 2px; line-height: 1.5" align="justify">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr id="Tr1" runat="server" align="left" style="height: 20px">
                                                                    <td colspan="2" class="LabelHeader">
                                                                        Country Master Page
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="left">
                                                                        <cc1:TabContainer ID="tcntAllCSCTabs" runat="server" ActiveTabIndex="0" ScrollBars="Vertical"
                                                                            Height="380px" Width="100%">
                                                                            <cc1:TabPanel ID="tpnlCountry" runat="server" HeaderText="Country" Width="100%">
                                                                                <ContentTemplate>
                                                                                    <table border="0" width="97%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="lnkAddEditCountry" runat="server" ImageUrl="~/Images/btnAdd.gif" />
                                                                                                <cc1:ModalPopupExtender ID="mpeEdit" runat="server" BackgroundCssClass="modalBackground"
                                                                                                    DropShadow="false" PopupControlID="pnlStatusCountryEdit" PopupDragHandleControlID="pnlStatusCountryEdit"
                                                                                                    TargetControlID="lnkAddEditCountry">
                                                                                                </cc1:ModalPopupExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td valign="top">
                                                                                                <asp:GridView ID="gvCountry" runat="server" DataKeyNames="CountryID" AllowSorting="true"
                                                                                                    OnRowDataBound="gvCountry_RowDataBound" OnRowDeleting="gvCountry_RowDeleting"
                                                                                                    OnRowEditing="gvCountry_RowEditing">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="CountryID" HeaderText="CountryID" />
                                                                                                        <asp:BoundField DataField="Country" ItemStyle-Width="94%" HeaderText="Country">
                                                                                                            <ItemStyle />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                                                                            <ItemTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 50%" align="left">
                                                                                                                            <asp:ImageButton ID="ibtnEditCountry" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                        <td style="width: 50%" align="right">
                                                                                                                            <asp:ImageButton ID="ibtnDeleteCountry" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    Country
                                                                                </HeaderTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="tpnlStates" runat="server" HeaderText="State" Width="100%">
                                                                                <ContentTemplate>
                                                                                    <table border="0" width="97%">
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:ImageButton ID="lnkAddUpdateState" runat="server" ImageUrl="~/Images/btnAdd.gif" />
                                                                                                <cc1:ModalPopupExtender ID="mpePopupState" runat="server" BackgroundCssClass="modalBackground"
                                                                                                    PopupControlID="pnlAddEditState" PopupDragHandleControlID="pnlAddEditState" TargetControlID="lnkAddUpdateState"
                                                                                                    DynamicServicePath="" Enabled="True">
                                                                                                </cc1:ModalPopupExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td style="width: 7%;" align="left" valign="middle">
                                                                                                <asp:Label ID="lblStateCountry" CssClass="Label" Text="Country" runat="server">
                                                                                                </asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 93%" align="left" valign="middle">
                                                                                                <asp:DropDownList ID="ddlStateCountry" AutoPostBack="true" AppendDataBoundItems="True"
                                                                                                    Width="200px" CssClass="Dropdownlist" runat="server" OnSelectedIndexChanged="ddlStateCountry_SelectedIndexChanged">
                                                                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td colspan="2" align="left">
                                                                                                <asp:GridView ID="gvStates" runat="server" AllowSorting="True" DataKeyNames="StateID"
                                                                                                    OnRowDataBound="gvStates_RowDataBound" OnRowDeleting="gvStates_RowDeleting" OnRowEditing="gvStates_RowEditing">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle VerticalAlign="Top" Width="10px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="StateID" HeaderText="StateID" />
                                                                                                        <asp:BoundField DataField="CountryID" HeaderText="CountryID" />
                                                                                                        <asp:BoundField DataField="State" ItemStyle-Width="94%" HeaderText="State"></asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                                                                            <ItemTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 50%" align="left">
                                                                                                                            <asp:ImageButton ID="ibtnEditState" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                        <td style="width: 50%" align="right">
                                                                                                                            <asp:ImageButton ID="ibtnDeleteState" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle Width="50px" />
                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    State
                                                                                </HeaderTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="tpnlCity" runat="server" HeaderText="City" Width="100%">
                                                                                <ContentTemplate>
                                                                                    <table border="0" width="97%">
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:ImageButton ID="lnkAddEditCity" runat="server" ImageUrl="~/Images/btnAdd.gif" />
                                                                                                <cc1:ModalPopupExtender ID="mpbPopupCity" runat="server" BackgroundCssClass="modalBackground"
                                                                                                    PopupControlID="pnlAddEditCity" PopupDragHandleControlID="pnlAddEditCity" TargetControlID="lnkAddEditCity"
                                                                                                    DynamicServicePath="" Enabled="True">
                                                                                                </cc1:ModalPopupExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td style="width: 7%;" align="left" valign="middle">
                                                                                                <asp:Label ID="lblCityCountry" CssClass="Label" Text="Country" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 93%" align="left" valign="middle">
                                                                                                <asp:DropDownList ID="ddlCityCountry" AutoPostBack="True" AppendDataBoundItems="True"
                                                                                                    Width="200px" CssClass="Dropdownlist" runat="server" OnSelectedIndexChanged="ddlCityCountry_SelectedIndexChanged">
                                                                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td style="width: 7%;" align="left" valign="middle">
                                                                                                <asp:Label ID="lblCityState" CssClass="Label" Text="State" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 93%" align="left" valign="middle">
                                                                                                <asp:DropDownList ID="ddlCityState" AutoPostBack="True" AppendDataBoundItems="True"
                                                                                                    Width="200px" CssClass="Dropdownlist" runat="server" OnSelectedIndexChanged="ddlCityState_SelectedIndexChanged">
                                                                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td colspan="2" align="left">
                                                                                                <asp:GridView ID="gvCity" runat="server" AllowSorting="True" DataKeyNames="CityID"
                                                                                                    OnRowDataBound="gvCity_RowDataBound" OnRowDeleting="gvCity_RowDeleting" OnRowEditing="gvCity_RowEditing">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="CityID" HeaderText="CityID" />
                                                                                                        <asp:BoundField DataField="StateID" HeaderText="StateID" />
                                                                                                        <asp:BoundField DataField="City" ItemStyle-Width="94%" HeaderText="City"></asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                                                                            <ItemTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 50%" align="left">
                                                                                                                            <asp:ImageButton ID="ibtnEditCity" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                        <td style="width: 50%" align="right">
                                                                                                                            <asp:ImageButton ID="ibtnDeleteCity" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle Width="50px" />
                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    City
                                                                                </HeaderTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="tpnlDistrict" runat="server" HeaderText="District" Width="100%">
                                                                                <ContentTemplate>
                                                                                    <table border="0" width="97%">
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:ImageButton ID="lnkAddEditDistrict" runat="server" ImageUrl="~/Images/btnAdd.gif" />
                                                                                                <cc1:ModalPopupExtender ID="mpePopUpDistrict" runat="server" BackgroundCssClass="modalBackground"
                                                                                                    PopupControlID="pnlAddEditDistrict" PopupDragHandleControlID="pnlAddEditDistrict"
                                                                                                    TargetControlID="lnkAddEditDistrict" DynamicServicePath="" Enabled="True">
                                                                                                </cc1:ModalPopupExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td style="width: 7%;" align="left" valign="middle">
                                                                                                <asp:Label ID="lblDistrictCountries" CssClass="Label" Text="Country" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 93%" align="left" valign="middle">
                                                                                                <asp:DropDownList ID="ddlDistrictCountries" AutoPostBack="True" AppendDataBoundItems="True"
                                                                                                    Width="200px" CssClass="Dropdownlist" runat="server" OnSelectedIndexChanged="ddlDistrictCountries_SelectedIndexChanged">
                                                                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td style="width: 7%;" align="left" valign="middle">
                                                                                                <asp:Label ID="lblDistrictStates" CssClass="Label" Text="State" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 93%" align="left" valign="middle">
                                                                                                <asp:DropDownList ID="ddlDistrictStates" AutoPostBack="True" AppendDataBoundItems="True"
                                                                                                    Width="200px" CssClass="Dropdownlist" runat="server" OnSelectedIndexChanged="ddlDistrictStates_SelectedIndexChanged">
                                                                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr valign="top">
                                                                                            <td colspan="2" align="left">
                                                                                                <asp:GridView ID="gvDistrict" runat="server" AllowSorting="True" DataKeyNames="DistrictID"
                                                                                                    OnRowDataBound="gvDistrict_RowDataBound" OnRowDeleting="gvDistrict_RowDeleting"
                                                                                                    OnRowEditing="gvDistrict_RowEditing">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="DistrictID" HeaderText="DistrictID" />
                                                                                                        <asp:BoundField DataField="StateID" HeaderText="StateID" />
                                                                                                        <asp:BoundField DataField="District" ItemStyle-Width="94%" HeaderText="District"></asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                                                                            <ItemTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 50%" align="left">
                                                                                                                            <asp:ImageButton ID="ibtnEditDistrict" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                        <td style="width: 50%" align="right">
                                                                                                                            <asp:ImageButton ID="ibtnDeleteDistrict" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle Width="50px" />
                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    District
                                                                                </HeaderTemplate>
                                                                            </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trUpdateCancelButtonRow" runat="server">
                                                                    <td align="center" colspan="2" valign="top" style="height: 15px;">
                                                                        <br />
                                                                        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                                                            ImageAlign="Middle" TabIndex="103" OnClick="btnCancel_Click" />&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="pnlStatusCountryEdit" runat="server" CssClass="ModalPopup" Style="display: none"
                                                            Width="280px">
                                                            <table style="width: 100%" border="0" cellpadding="1" cellspacing="3">
                                                                <tr>
                                                                    <td colspan="2" style="width: 100%; text-align: left">
                                                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ValidationGroup="ValidateCountry"
                                                                            ControlToValidate="txtCountry" ErrorMessage="Please Enter the Country" Text="*"
                                                                            SetFocusOnError="True">
                                                                            <li style="list-style-type: circle">
                                                                                <asp:Label ID="lblCountryValidation" runat="server" ForeColor="red" Text="Please Enter the Country"></asp:Label></li>
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr2" visible="false" runat="server">
                                                                    <td style="width: 40%" align="right">
                                                                        <asp:Label ID="lblCountryID" CssClass="Label" runat="server" Text="CountryID" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 60%" align="left">
                                                                        <asp:TextBox ID="txtCountryID" Width="120px" CssClass="textarea" Text="0" Visible="false"
                                                                            runat="server">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%" align="right">
                                                                        <asp:Label ID="lblCountry" CssClass="Label" runat="server" Text="Country"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 60%" align="left">
                                                                        <asp:TextBox ID="txtCountry" Width="220px" CssClass="textarea" MaxLength="50" runat="server"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="ftbeCountry" runat="server" TargetControlID="txtCountry"
                                                                            FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                            Enabled="True" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <center>
                                                                            <br />
                                                                            <asp:ImageButton ID="btnCountryAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                                                CausesValidation="true" ValidationGroup="ValidateCountry" TabIndex="402" OnClick="btnCountryAdd_Click" />&nbsp;
                                                                            <asp:ImageButton ID="btnCountryCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                                                                CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnCountryCancel_Click" />&nbsp;
                                                                        </center>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlAddEditState" runat="server" CssClass="ModalPopup" Style="display: none"
                                                            Width="280px">
                                                            <center>
                                                                <table style="width: 100%" border="0" cellpadding="1" cellspacing="3">
                                                                    <tr>
                                                                        <td colspan="2" style="width: 100%; text-align: left">
                                                                            <asp:RequiredFieldValidator ID="rfvStateCountryID" runat="server" ValidationGroup="ValidateState"
                                                                                ControlToValidate="ddlCountry" ErrorMessage="Please Select Country" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblCountryError" runat="server" ForeColor="Red" Text="Please Select Country"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RequiredFieldValidator ID="rfvStateDescription" runat="server" ValidationGroup="ValidateState"
                                                                                ControlToValidate="txtState" ErrorMessage="Please Enter State" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblStateDescriptionError" runat="server" ForeColor="Red" Text="Please Enter State"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr3" visible="false" runat="server">
                                                                        <td id="Td1" style="width: 40%" align="right" runat="server">
                                                                            <asp:Label ID="lblStateID" CssClass="Label" runat="server" Text="StateID" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td id="Td2" style="width: 60%" align="left" runat="server">
                                                                            <asp:TextBox ID="txtStateID" Width="120px" CssClass="textarea" Text="0" Visible="False"
                                                                                runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 28%" align="right">
                                                                            <asp:Label ID="lblCountryState" CssClass="Label" runat="server" Text="Country"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 72%" align="left">
                                                                            <asp:DropDownList ID="ddlCountry" AppendDataBoundItems="True" Width="225px" CssClass="Dropdownlist"
                                                                                runat="server">
                                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="middle">
                                                                            <asp:Label ID="lblState" CssClass="Label" runat="server" Text="State"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtState" Width="220px" CssClass="textarea" MaxLength="50" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="ftbeState" runat="server" TargetControlID="txtState"
                                                                                FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                                Enabled="True" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <center>
                                                                                <br />
                                                                                <asp:ImageButton ID="btnStateSave" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                                                    ValidationGroup="ValidateState" TabIndex="402" OnClick="btnStateSave_Click" />&nbsp;
                                                                                <asp:ImageButton ID="btnStateCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                                                                    CausesValidation="False" ImageAlign="Middle" TabIndex="103" OnClick="btnStateCancel_Click" />&nbsp;
                                                                            </center>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </center>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlAddEditCity" runat="server" CssClass="ModalPopup" Style="display: none"
                                                            Width="280px">
                                                            <center>
                                                                <table style="width: 100%" border="0" cellpadding="1" cellspacing="3">
                                                                    <tr>
                                                                        <td colspan="2" style="width: 100%; text-align: left">
                                                                            <asp:RequiredFieldValidator ID="rfvCityCountryValidator" runat="server" ValidationGroup="ValidateCity"
                                                                                ControlToValidate="ddlCountryCity" ErrorMessage="Please Select Country" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblCountryCityError" runat="server" ForeColor="Red" Text="Please Select Country"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RequiredFieldValidator ID="rfvCityStateValidator" runat="server" ValidationGroup="ValidateCity"
                                                                                ControlToValidate="ddlCountryState" ErrorMessage="Please Select State" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblStateCityError" runat="server" ForeColor="Red" Text="Please Select State"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RequiredFieldValidator ID="rfvCityValidator" runat="server" ValidationGroup="ValidateCity"
                                                                                ControlToValidate="txtCity" ErrorMessage="Please Enter City" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblCityError" runat="server" ForeColor="Red" Text="Please Enter City"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr4" visible="False" runat="server">
                                                                        <td id="Td3" style="width: 40%" align="right" runat="server">
                                                                            <asp:Label ID="lblCityID" CssClass="Label" runat="server" Text="CityID" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td id="Td4" style="width: 60%" align="left" runat="server">
                                                                            <asp:TextBox ID="txtCityID" Width="120px" CssClass="textarea" Text="0" Visible="False"
                                                                                runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 28%" align="right">
                                                                            <asp:Label ID="lblCountryCity" CssClass="Label" runat="server" Text="Country"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 72%" align="left">
                                                                            <asp:DropDownList ID="ddlCountryCity" AppendDataBoundItems="True" Width="225px" CssClass="Dropdownlist"
                                                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountryCity_SelectedIndexChanged">
                                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblStateCity" CssClass="Label" runat="server" Text="State"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlCountryState" AppendDataBoundItems="True" Width="225px"
                                                                                CssClass="Dropdownlist" runat="server">
                                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="middle">
                                                                            <asp:Label ID="lblCity" CssClass="Label" runat="server" Text="City"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtCity" Width="220px" CssClass="textarea" MaxLength="50" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="ftbeCity" runat="server" TargetControlID="txtCity"
                                                                                FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                                Enabled="True" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <center>
                                                                                <br />
                                                                                <asp:ImageButton ID="btnCitySave" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                                                    ValidationGroup="ValidateCity" TabIndex="402" OnClick="btnCitySave_Click" />&nbsp;
                                                                                <asp:ImageButton ID="btnCityCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                                                                    CausesValidation="False" ImageAlign="Middle" TabIndex="103" OnClick="btnCityCancel_Click" />&nbsp;
                                                                            </center>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </center>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlAddEditDistrict" runat="server" CssClass="ModalPopup" Style="display: none"
                                                            Width="280px">
                                                            <center>
                                                                <table style="width: 100%" border="0" cellpadding="1" cellspacing="3">
                                                                    <tr>
                                                                        <td colspan="2" style="width: 100%; text-align: left">
                                                                            <asp:RequiredFieldValidator ID="rfvDistrictCountry" runat="server" ValidationGroup="ValidateDistrict"
                                                                                ControlToValidate="ddlDistrictCountry" ErrorMessage="Please Select Country" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblDistrictCountryError" runat="server" ForeColor="Red" Text="Please Select Country"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RequiredFieldValidator ID="rfvDistrictState" runat="server" ValidationGroup="ValidateDistrict"
                                                                                ControlToValidate="ddlDistrictState" ErrorMessage="Please Select State" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblDistrictStateError" runat="server" ForeColor="Red" Text="Please Select State"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RequiredFieldValidator ID="rfvDistrictError" runat="server" ValidationGroup="ValidateDistrict"
                                                                                ControlToValidate="txtDistrict" ErrorMessage="Please Enter District" SetFocusOnError="True">
                                                                                <li style="list-style-type: circle">
                                                                                    <asp:Label ID="lblDistrictError" runat="server" ForeColor="Red" Text="Please Enter District"></asp:Label></li>
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr5" visible="False" runat="server">
                                                                        <td id="Td5" style="width: 40%" align="right" runat="server">
                                                                            <asp:Label ID="lblDistrictID" CssClass="Label" runat="server" Text="DistrictID" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td id="Td6" style="width: 60%" align="left" runat="server">
                                                                            <asp:TextBox ID="txtDistrictID" Width="120px" CssClass="textarea" Text="0" Visible="False"
                                                                                runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 28%" align="right">
                                                                            <asp:Label ID="lblDistrictCountry" CssClass="Label" runat="server" Text="Country"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 72%" align="left">
                                                                            <asp:DropDownList ID="ddlDistrictCountry" AppendDataBoundItems="True" Width="225px"
                                                                                CssClass="Dropdownlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictCountry_SelectedIndexChanged">
                                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblDistrictState" CssClass="Label" runat="server" Text="State"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlDistrictState" AppendDataBoundItems="True" Width="225px"
                                                                                CssClass="Dropdownlist" runat="server">
                                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="middle">
                                                                            <asp:Label ID="lblDistrict" CssClass="Label" runat="server" Text="District"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtDistrict" Width="220px" CssClass="textarea" MaxLength="50" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="ftbeDistrict" runat="server" TargetControlID="txtDistrict"
                                                                                FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                                Enabled="True" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <center>
                                                                                <br />
                                                                                <asp:ImageButton ID="btnDistrictSave" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                                                    ValidationGroup="ValidateDistrict" TabIndex="402" OnClick="btnDistrictSave_Click" />&nbsp;
                                                                                <asp:ImageButton ID="btnDistrictCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                                                                    CausesValidation="False" ImageAlign="Middle" TabIndex="103" OnClick="btnDistrictCancel_Click" />&nbsp;
                                                                            </center>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </center>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" width="200">
                                <table cellpadding="0" cellspacing="0" border="0" width="200">
                                    <tr>
                                        <td background="../images/td-left-bottom.jpg" width="200" height="25">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" width="775">
                                <table cellpadding="0" cellspacing="0" border="0" width="775">
                                    <tr>
                                        <td background="../images/td-content-bottom.jpg" width="775" height="25">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <uc3:FMFFooter ID="ECFooter1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>