<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Titles.aspx.cs" Inherits="ECGroup.ForeignMissionFoundation.Titles" %>

<%@ Register Src="../UserControls/Footer.ascx" TagName="FMFFooter" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/Header.ascx" TagName="FMFHeader" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/FMFMenu.ascx" TagName="FMFMenu" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Title</title>
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
    <form id="frmTitleMasterPage" runat="server">
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
                                Title Master Page
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" style="text-align: left; width: 97%">
                                    <tr>
                                        <td align="left">
                                            <asp:ImageButton ID="lnkAddTitles" runat="server" ImageUrl="~/Images/btnAdd.gif"
                                                OnClick="lnkAddTitles_Click" />
                                            <asp:Panel ID="pnlStatusEdit" runat="server" CssClass="ModalPopup" Style="display: none"
                                                Width="300px">
                                                <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="3">
                                                    <tr>
                                                        <td colspan="2" style="width: 100%; text-align: left">
                                                            <asp:RequiredFieldValidator ID="rfvTitles" runat="server" ValidationGroup="ValidateTitles"
                                                                ControlToValidate="txtTitles" ErrorMessage="Please Enter the Title" SetFocusOnError="True">
                                                                <li style="list-style-type: circle">
                                                                    <asp:Label ID="lblTitlesValidation" runat="server" ForeColor="red" Text="Please Enter the Title"></asp:Label></li>
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr2" visible="false" runat="server">
                                                        <td style="width: 40%" align="right">
                                                            <asp:Label ID="lblTitlesID" CssClass="Label" runat="server" Text="TitlesID" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%" align="left">
                                                            <asp:TextBox ID="txtTitlesID" Width="120px" CssClass="textarea" Text="0" Visible="false"
                                                                runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 40%" align="right">
                                                            <asp:Label ID="lblTitles" CssClass="Label" runat="server" Text="Title"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%" align="left">
                                                            <asp:TextBox ID="txtTitles" Width="250px" MaxLength="200" CssClass="textarea" runat="server">
                                                            </asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbeTitles" runat="server" TargetControlID="txtTitles"
                                                                FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <center>
                                                                <br />
                                                                <asp:ImageButton ID="btnTitleAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                                    CausesValidation="true" ValidationGroup="ValidateTitles" TabIndex="102" OnClick="btnTitleAdd_Click" />&nbsp;
                                                                <asp:ImageButton ID="btnTitleCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                                                    CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnTitleCancel_Click" />&nbsp;
                                                            </center>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="mpeEdit" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="false" PopupControlID="pnlStatusEdit" PopupDragHandleControlID="pnlStatusEdit"
                                                TargetControlID="lnkAddTitles">
                                            </cc1:ModalPopupExtender>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" align="left">
                                            <asp:Panel ID="pnlGridView" runat="Server" Width="100%" Height="400px" ScrollBars="vertical">
                                                <asp:GridView ID="gvTitle" runat="server" DataKeyNames="TitleID" AllowSorting="true"
                                                    OnRowDataBound="gvTitle_RowDataBound" OnRowDeleting="gvTitle_RowDeleting" OnRowEditing="gvTitle_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TitleID" HeaderText="TitleID" />
                                                        <asp:BoundField DataField="Title" ItemStyle-Width="94%" HeaderText="Title">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%" align="left">
                                                                            <asp:ImageButton ID="ibtnEditTitle" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td style="width: 50%" align="right">
                                                                            <asp:ImageButton ID="ibtnDeleteTitle" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
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
