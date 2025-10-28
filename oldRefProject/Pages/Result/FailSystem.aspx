<%@ Page Title="<%$ Resources:Application,FailSystem %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="FailSystem.aspx.cs" Inherits="Pages_Result_FailSystem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables_themeroller.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-4">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12">
                                <div class="form-horizontal">
                                     <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,Examtype %>"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlExamtype" runat="server" DataTextField="ExamType" DataValueField="Id" CssClass="form-control dropdown">
                                            </asp:DropDownList>
                                        </div>
                                    </div>  
                                     <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"
                                                OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>  
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"
                                                OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"
                                                OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="updSubject" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="SubjectName" DataValueField="Id" CssClass="form-control dropdown"
                                                        OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-8">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,FailSystem %>"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                            </div>
                            <div class="col-lg-12 col-md-12">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,SubjectiveMarks %>"></asp:Label></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="tbxSubjective" runat="server" Placeholder="Enter Subjective Fail Marks" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                                ErrorMessage="Enter Subjective Fail Marks" ControlToValidate="tbxSubjective">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="validateMarks" ControlToValidate="tbxSubjective" ValidationExpression="^([0-9]{1,2}){1}(\.[0-9]{1,2})?$|^100$"
                                                ErrorMessage="Please enter valid number!" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,ObjectiveMarks %>"></asp:Label></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="tbxObjective" runat="server" Placeholder="Enter Objective Fail Marks" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                                ErrorMessage="Enter Objective Fail Marks" ControlToValidate="tbxObjective">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="tbxObjective" ValidationExpression="^([0-9]{1,2}){1}(\.[0-9]{1,2})?$|^100$"
                                                ErrorMessage="Please enter valid number!" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="<%$ Resources:Application,Save %>" ValidationGroup="save" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptFailSystem" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                 <th>
                                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,SubjectName %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,SubjectivePassMarks %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,ObjectivePassMarks %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("MediumName") %></td>
                                        <td><%#Eval("ClassName") %></td>
                                        <td><%#Eval("GroupName") %></td>
                                        <td><%#Eval("SubjectName") %></td>
                                        <td><%#Eval("SubjectiveFailMarks") %></td>
                                        <td><%#Eval("ObjectiveFailMarks") %></td>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                            <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
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
            //$(".treeview").removeClass("active");
            $(".aa").first().closest(".treeview").addClass("active");
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
                //$(".treeview").removeClass("active");
                $(".aa").first().closest(".treeview").addClass("active");
            }
        });
    </script>
</asp:Content>

