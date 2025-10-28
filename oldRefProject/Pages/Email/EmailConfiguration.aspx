<%@ Page Title="<%$ Resources:Application,EmailConfiguration %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmailConfiguration.aspx.cs" Inherits="Pages_Email_EmailConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>

            <div class="form-horizontal">
                <div class="form-group">
                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Submit" runat="server" />
                </div>
                
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,DisPlayName %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="tbxName" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="tbxName"
                            ErrorMessage="Please enter 'tbxUserID'." ToolTip="UserID is required." ValidationGroup="Submit">*</asp:RequiredFieldValidator>

                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,DisplayEmail %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="tbxDisplayEmail" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxDisplayEmail"
                            ErrorMessage="Please enter 'Password'." ToolTip="Password is required." ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,ReplyEmail %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="tbxReply" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxReply"
                            ErrorMessage="Please enter 'Password'." ToolTip="Password is required." ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </div>
                </div>




                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="tbxUserName" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbxUserName"
                            ErrorMessage="Please enter 'Password'." ToolTip="Password is required." ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regVEmail" runat="server" ValidationGroup="Submit"
                            Display="None" ErrorMessage="Invalid UserID format.<br />&nbsp;" ControlToValidate="tbxUserName"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Password %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbxPassword"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxPassword"
                            ErrorMessage="Please enter 'Password'." ToolTip="Password is required." ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Port %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="tbxPort" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxPort"
                            ErrorMessage="Please enter 'Port'." ToolTip="Port is required." ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Submit" runat="server"
                            ControlToValidate="tbxPort" ErrorMessage="Please Enter Valid Port" Operator="DataTypeCheck"
                            Type="Integer"></asp:CompareValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Server %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbxServer"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxServer"
                            ErrorMessage="Please enter 'Server'." ToolTip="Server is required." ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,SSLEnable %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:CheckBox ID="chkSSL" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Authentication %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:CheckBox ID="chkAuthentication" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-sm-10">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="Submit"
                            Text="<%$ Resources:Application,Save %>" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

