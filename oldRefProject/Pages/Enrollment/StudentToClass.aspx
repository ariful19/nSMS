<%@ Page Title="<%$ Resources:Application,AssignStudentClass %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="StudentToClass.aspx.cs" Inherits="Pages_Enrollment_StudentToClass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div class="col-sm-4">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RollNo %>"></asp:Label></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter Roll No." CssClass="form-control" MaxLength="9"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                    Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Stu. ID" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="tbxReg" FilterType="Custom" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-4 col-sm-6">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-4">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,AssignStudent %>"></asp:Label>
            </div>
            <div class="panel-body">
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="red"></asp:Label>
                <asp:Repeater ID="rptCurrent" runat="server" OnItemDataBound="rptCurrent_ItemDataBound">
                    <HeaderTemplate>
                        <table id="example1" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                     <th>
                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                    <th>
                                        <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                    <th>
                                        <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label>
                                        <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="action"><%# Container.ItemIndex + 1 %></td>
                            <td><%#Eval("NameEng") %></td>
                            <td>
                                <asp:Label ID="lblRoll" Text='<%#Eval("RollNo") %>' runat="server" />
                                <asp:Label ID="lblReg" Text='<%#Eval("RegNo") %>' runat="server" />
                            </td>
                            <td align="right" width="15px">
                                <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/minus.png" ToolTip="Remove" OnClientClick="return confirm('Are you sure?')" /></td>
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
    <div class="col-sm-4">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,UnassignStudent %>"></asp:Label>
            </div>
            <div class="panel-body">
                <asp:Repeater ID="rptUnassign" runat="server" OnItemDataBound="rptUnassign_ItemDataBound">
                    <HeaderTemplate>
                        <table id="example1" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                    <th>
                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                    </th>
                                    <th id="thRoll" runat="server">
                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,AssignRoll %>"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkrow" data-student='<%#Eval("StudentId") %>' runat="server"></asp:CheckBox></td>
                            <td>
                                <%#Eval("RegistratinNo") %>
                            </td>
                            <td>
                                <%#Eval("NameEng") %>

                                <asp:HiddenField ID="hdnStudentId" runat="server" Value='<%#Eval("StudentId") %>' />
                            </td>
                            <td id="tdRoll" runat="server">
                                <asp:TextBox ID="txtStudentID" Width="80px" runat="server" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                                </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="panel-footer">
                <asp:Button ID="btnAssign" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Application,Assign %>" OnClick="btnAssign_Click" />
            </div>
        </div>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%#Eval("NameEng") %>
                        <asp:HiddenField ID="hdnValue" runat="server" Value='<%#Eval("StudentId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script src="../../Scripts/prettify.min.js"></script>
    <script src="../../Scripts/multiselect.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#example1 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example1 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkrow]").prop("checked", false);
                }
            });
            $("#example1 [id*=chkrow]").click(function () {
                if ($("#example1 [id*=chkrow]").length == $("#example1 [id*=chkrow]:checked").length) {
                    $("#example1 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkHeader]").prop("checked", false);
                }
            });
            //$("#example1").DataTable({
            //    "paging": false,
            //    "lengthChange": false,
            //    "searching": true,
            //    "ordering": true,
            //    "info": false,
            //    "autoWidth": true
            //});
            //$(".aa").first().closest(".treeview").addClass("active");
            //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            //function EndRequestHandler(sender, args) {
            //    $("#example1").DataTable({
            //        "paging": false,
            //        "lengthChange": false,
            //        "searching": true,
            //        "ordering": true,
            //        "info": false,
            //        "autoWidth": true
            //    });
            //    //$(".treeview").removeClass("active");
            //    $(".aa").first().closest(".treeview").addClass("active");
            //}
        });
    </script>
</asp:Content>

