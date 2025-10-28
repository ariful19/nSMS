<%@ Page Title="<%$ Resources:Application,UserManagement %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Pages_RoleManagement_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">

        <ContentTemplate>
            <div class="col-md-6">

                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CreateUserWizard1" />
                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-md-7">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label><span class="red">*</span></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control" Style="margin-left: 0px"
                            Width="240px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            ErrorMessage="Please enter 'User Name'." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-md-7">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Password %>"></asp:Label><span class="red">*</span></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"
                            Style="margin-left: 0px" Width="240px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                            ErrorMessage="Please enter 'Password'." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Password"
                            ErrorMessage="Password length must be 5 or more" ValidationExpression="\w{5,255}"
                            ValidationGroup="CreateUserWizard1">*</asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-md-7">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,ConfirmPassword %>"></asp:Label><span class="red">*</span></label>
                    <div class="col-md-5">
                        <asp:TextBox CssClass="form-control" ID="ConfirmPassword" runat="server" TextMode="Password"
                            Style="margin-left: 0px" Width="240px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                            ErrorMessage="Please enter 'Confirm Password'." ToolTip="Confirm Password is required."
                            ValidationGroup="CreateUserWizard1"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                            ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="'Password' and 'Confirmation Password' must match."
                            ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-md-7">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label><span class="red">*</span></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="Email" CssClass="form-control" runat="server" Style="margin-left: 0px"
                            Width="240px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                            ErrorMessage="Please enter 'E-mail'." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="CreateUserWizard1" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ControlToValidate="Email" ErrorMessage="Invalid email address."></asp:RegularExpressionValidator>


                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-md-7">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Role %>"></asp:Label><span class="red">*</span></label>
                    <div class="col-md-5">
                        <asp:DropDownList ID="lbxAssignRole" runat="server" DataTextField="RoleName" DataValueField="RoleName" CssClass="dropdown form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="dropdown form-control"
                            runat="server" ControlToValidate="lbxAssignRole" ErrorMessage="Please select 'Role'."
                            ToolTip="Role is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-7 col-sm-10">
                        <asp:HiddenField runat="server" ID="updatedUser"></asp:HiddenField>

                        <asp:Button ID="btnAddEditUser" CssClass=" btn btn-primary" runat="server" OnClick="btnAddEditUser_Click" Text="<%$ Resources:Application,CreateUser %>"
                            EnableViewState="False" ValidationGroup="CreateUserWizard1" />
                    </div>
                </div>

            </div>

            <%-- <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
                <WizardSteps>
                    <asp:CreateUserWizardStep ID="CreateUserWizardStep_1" runat="server">
                        <ContentTemplate>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-10 col-sm-10 col-xs-10">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CreateUserWizard1" />
                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-md-7">
                                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label><span class="red">*</span></label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control" Style="margin-left: 0px"
                                            Width="240px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="Please enter 'User Name'." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-md-7">
                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Password %>"></asp:Label><span class="red">*</span></label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"
                                            Style="margin-left: 0px" Width="240px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Please enter 'Password'." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Password length must be 5 or more" ValidationExpression="\w{5,255}"
                                            ValidationGroup="CreateUserWizard1">*</asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-md-7">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,ConfirmPassword %>"></asp:Label><span class="red">*</span></label>
                                    <div class="col-md-5">
                                        <asp:TextBox CssClass="form-control" ID="ConfirmPassword" runat="server" TextMode="Password"
                                            Style="margin-left: 0px" Width="240px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                            ErrorMessage="Please enter 'Confirm Password'." ToolTip="Confirm Password is required."
                                            ValidationGroup="CreateUserWizard1"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                            ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="'Password' and 'Confirmation Password' must match."
                                            ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-md-7">
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label><span class="red">*</span></label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="Email" CssClass="form-control" runat="server" Style="margin-left: 0px"
                                            Width="240px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                            ErrorMessage="Please enter 'E-mail'." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="CreateUserWizard1" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ControlToValidate="Email"  ErrorMessage="Invalid email address."></asp:RegularExpressionValidator>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-md-7">
                                       <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Role %>"></asp:Label><span class="red">*</span></label>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="lbxAssignRole" runat="server" DataTextField="RoleName" DataValueField="RoleName" CssClass="dropdown form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="dropdown form-control"
                                            runat="server" ControlToValidate="lbxAssignRole" ErrorMessage="Please select 'Role'."
                                            ToolTip="Role is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-offset-7 col-sm-10">
                                        <asp:Button ID="btnAddEditUser" CssClass=" btn btn-primary" runat="server" OnClick="btnAddEditUser_Click" Text="<%$ Resources:Application,CreateUser %>"
                                            EnableViewState="False" ValidationGroup="CreateUserWizard1" />
                                    </div>
                                </div>
                        </ContentTemplate>
                        <CustomNavigationTemplate>
                        </CustomNavigationTemplate>
                    </asp:CreateUserWizardStep>
                    <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                    </asp:CompleteWizardStep>
                </WizardSteps>
            </asp:CreateUserWizard>--%>


            <div class="form-group">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
   <div class="row">
                                <label>
                                    <span>Role</span>
                                    <asp:DropDownList ID="ddlRoles" runat="server" DataTextField="RoleName"></asp:DropDownList>
                                </label>
                                <asp:Button runat="server" ID="btnLoad" OnClick="btnLoad_Click" Text="Load"/>
                            </div>
                            <asp:Repeater ID="rptUser" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblUserName" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblRole" runat="server" Text="<%$ Resources:Application,Role %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblEmail" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblAction" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("UserName") %></td>
                                        <td><%#Eval("RoleName") %></td>
                                        <td><%#Eval("Email") %></td>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" CausesValidation="false" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("UserName")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                            <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("UserName")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddEditUser" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#example1").DataTable();
            $('#example2').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false,
                "ordering": true,
                "info": true,
                "autoWidth": false
            });
            //$(".aa").closest(".treeview").addClass("active");
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $("#example1").DataTable();
                $('#example2').DataTable({
                    "paging": true,
                    "lengthChange": false,
                    "searching": false,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
                //$(".aa").closest(".treeview").addClass("active");
            }
        });
    </script>

</asp:Content>

