<%@ Page Title="<%$ Resources:Application,SubjecttoClass %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SubjectToClass.aspx.cs" Inherits="Pages_Enrollment_SubjectToClass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables_themeroller.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div>
        <div class="col-sm-6">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,ClassName %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control"
                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,SubjectName %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="SubjectName" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,SubjectCategory %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="SubjectCategory" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,OrderBy %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="tbxOrderBy" runat="server" placeholder="Enter Order By" CssClass="form-control" MaxLength="3"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            Enabled="True" TargetControlID="tbxOrderBy" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Order By" ControlToValidate="tbxOrderBy">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,IsOptional %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:CheckBox ID="chkOPtional" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-4 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                         <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="box">
                <div class="box-body">
                    <asp:Repeater ID="rptCategory" runat="server">
                        <HeaderTemplate>
                            <table id="example3" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Id %>"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Description %>"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Example %>"></asp:Label></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Id") %></td>
                                <td><%#Eval("SubjectCategory") %></td>
                                <td><%#Eval("Example") %></td>
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
    <%--<asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>--%>
            <div class="col-sm-12">
                <div class="box">
                    <div class="box-body">
                        <asp:Repeater ID="rptSubject" runat="server">
                            <HeaderTemplate>
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr class="table table-bordered table-hover">
                                            <th>
                                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,SubjectName %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,OrderBy %>"></asp:Label></th>
                                            <th class="action">
                                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("SubjectName") %></td>
                                    <td><%#Eval("MediumName") %></td>
                                    <td><%#Eval("ClassName") %></td>
                                    <td><%#Eval("GroupName") %></td>
                                    <td><%#Eval("OrderBy") %></td>
                                    <td class="action">
                                          <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
      <%-- <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%>
                                    </td>
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
       <%-- </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlClass" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function() {
            $('#example2').DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false
            });
            //Sys.WebForms.PageRequestManager.getInstance().add_endRequest();
            //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            //function EndRequestHandler(sender, args) {
            //    $('#example2').DataTable({
            //        "paging": false,
            //        "lengthChange": false,
            //        "searching": true,
            //        "ordering": true,
            //        "info": true,
            //        "autoWidth": false
            //    });
        });
    </script>

</asp:Content>




