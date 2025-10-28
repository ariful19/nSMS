<%@ Page Title="<%$ Resources:Application,GradeSetup %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Grade.aspx.cs" Inherits="Pages_Result_Grade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../../Styles/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables_themeroller.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
             
            <div class="form-horizontal">
                <div class="form-group">

                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="col-lg-6 col-md-6">
                    <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3"><asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,GradeName %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="tbxGradeName" runat="server" placeholder="Enter Grade Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Grade Name" ControlToValidate="tbxGradeName">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,GradePoint %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="tbxGradePoint" runat="server" placeholder="Enter Grade Point" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Grade Point" ControlToValidate="tbxGradePoint">*</asp:RequiredFieldValidator>
                    </div>
                </div>               
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3"><asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Remarks %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="tbxRemarks" runat="server" placeholder="Enter Remarks Here" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Remarks Here" ControlToValidate="tbxRemarks">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
                        </div>
           </div>
                <div class="col-lg-6 col-md-6">
                    <div class="form-horizontal">
                         <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,StartMarks %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="tbxStartMarks" runat="server" placeholder="Enter Start Marks" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Start Marks" ControlToValidate="tbxStartMarks">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,EndMarks %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="tbxEndMarks" runat="server" placeholder="Enter End Marks" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter End Marks" ControlToValidate="tbxEndMarks">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                        <%-- <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3"><asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,StartPoint %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="tbxStartPoint" runat="server" placeholder="Enter Start Point" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Start Point" ControlToValidate="tbxStartPoint">*</asp:RequiredFieldValidator>
                    </div>
                </div>--%>
               <%-- <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3"><asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,EndPoint %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="tbxEndPoint" runat="server" placeholder="Enter End Point" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter End Point" ControlToValidate="tbxEndPoint">*</asp:RequiredFieldValidator>
                    </div>
                </div>--%>
                        </div>
                    </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptYear" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th class="action"><asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                                    <th><asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,GradeName %>"></asp:Label></th>
                                                    <th><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,GradePoint %>"></asp:Label></th>
                                                    <th><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,StartMarks %>"></asp:Label></th>
                                                    <th><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,EndMarks %>"></asp:Label></th>
                                                     <%--<th><asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,PointRange %>"></asp:Label></th>--%>
                                                     
                                                     <th><asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Remarks %>"></asp:Label></th>
                                                    <th class="action"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="action"><%#Eval("Id") %></td>
                                            <td><%#Eval("GradeName") %></td>
                                            <td><%#Eval("GradePoint") %></td>
                                            <td><%#Eval("StartMarks") %></td>
                                            <td><%#Eval("EndMarks") %></td>
                                            <%--<td><%#Eval("StartPoint") %>-<%#Eval("EndPoint") %></td>--%>                            
                                            <td><%#Eval("Remarks") %></td>
                                            <td class="action">
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>--%>
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
