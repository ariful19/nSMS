<%@ Page Title="<%$ Resources:Application,SubjecttoStudent %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SubjectToStudent.aspx.cs" Inherits="Pages_Enrollment_SubjectToStudent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/prettify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script>
        Sys.Application.add_load(Load);
    </script>

    <div class="row">
        <div class="col-sm-8">
            <div class="row">
                <div class="col-sm-12">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6 col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label><span class="required">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                      <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label><span class="required">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"
                                                OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                   
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6">
                                <div class="form-horizontal">
                                     <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label><span class="required">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlGrouo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label><span class="required">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RollNo %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter Roll No." CssClass="form-control" MaxLength="9"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" CssClass="form-control" MaxLength="12"></asp:TextBox>
                            <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="tbxReg" FilterType="Custom" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>--%>
                        </div>
                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">

                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-5">
                                    <asp:ListBox ID="list1" ClientIDMode="Static" runat="server" class="form-control" Height="280px" DataTextField="SubjectName" 
                                       SelectionMode="Multiple" DataValueField="Id"></asp:ListBox>
                                </div>
                                <div class="col-sm-7">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                                <button class="btn btn-info btn-xs" id="btnAdd" style="width: 50px;"><i class="fa fa-angle-right"></i></button>
                                                <br />
                                                <button class="btn btn-info btn-xs" id="btnAddAll" style="width: 50px;"><i class="fa fa-angle-double-right"></i></button>
                                                <br />
                                                <button type="button" class="btn btn-info btn-xs" id="btnRemove" style="width: 50px;"><i class="fa fa-angle-left"></i></button>
                                                <br />
                                                <button type="button" class="btn btn-info btn-xs" id="btnRemoveAll" style="width: 50px;"><i class="fa fa-angle-double-left"></i></button>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="list2" runat="server" ClientIDMode="Static" CssClass="form-control" Height="150px" SelectionMode="Multiple"></asp:ListBox>
                                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Compulsory %>"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                                <button class="btn btn-info btn-xs" id="btnAddOp" style="width: 50px;"><i class="fa fa-angle-right"></i></button>
                                                <br />
                                                <button class="btn btn-info btn-xs" id="btnRemoveOp" style="width: 50px;"><i class="fa fa-angle-left"></i></button>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="list3" runat="server" ClientIDMode="Static" CssClass="form-control" Height="50px" SelectionMode="Multiple"></asp:ListBox>
                                                <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Optional %>"></asp:Label>
                                            </div>
                                        </div>
                                        <asp:Button ID="btnAssign" runat="server" CssClass="btn btn-primary btn-lg pull-right" Text="Assign" OnClick="btnAssign_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <asp:UpdatePanel ID="update" runat="server">
                <ContentTemplate>
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkstudentHeader" runat="server" /></th>
                                               <th>
                                                        <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label>
                                                        <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                                    </th>
                                                <th>
                                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkStudentRow"  data-student='<%#Eval("StudentToClassId") %>' runat="server" /></td>
                                          <td>
                                                <asp:Label ID="lblRoll" Text='<%#Eval("RollNo") %>' runat="server" />
                                                <asp:Label ID="lblReg" Text='<%#Eval("RegNo") %>' runat="server" />
                                            </td>
                                        <td>
                                            <%#Eval("NameEng") %>
                                            <asp:HiddenField ID="hdnStudentToClassId" Value='<%#Eval("StudentToClassId") %>' runat="server" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

     <div class="col-sm-4">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading" style="background-color:lightgray">
               Assign Subject List 
            </div>
            <div class="panel-body">
                <asp:Repeater ID="rptSubject" runat="server">
                    <HeaderTemplate>
                        <table id="example1" class="table table-bordered table-hover table-responsive">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:CheckBox ID="chkSubjectHeader" runat="server" /></th>
                                    <th>Subject</th>
                                    <th>Is Optional</th>
                               
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkSubjectRow" runat="server" /></td>
                            <td>
                                <%#Eval("SubjectName") %>
                                <asp:HiddenField ID="hdnSubjectToClassId" runat="server" Value='<%#Eval("Id") %>' />
                            </td>
                            <td>
                                <%#Eval("IsOptional") %>
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

    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script src="../../Scripts/prettify.min.js"></script>
    <script src="../../Scripts/multiselect.min.js"></script>

    <script type="text/javascript">
        $(document).ready(
               function () {
                   $('#btnAdd').click(
                       function (e) {
                           $('#list1 > option:selected').appendTo('#list2');
                           e.preventDefault();
                       });

                   $('#btnAddAll').click(
                   function (e) {
                       $('#list1 > option').appendTo('#list2');
                       $('#list2 > option').prop("selected", true);
                       e.preventDefault();
                   });

                   $('#btnRemove').click(
                   function (e) {
                       $('#list2 > option:selected').appendTo('#list1');
                       $('#list2 > option').prop("selected", true);
                       e.preventDefault();
                   });

                   $('#btnRemoveAll').click(
                   function (e) {
                       $('#list2 > option').appendTo('#list1');
                       e.preventDefault();
                   });
                   $('#btnAddOp').click(function (e) {
                       e.preventDefault();
                       var length = $('#list3 option').size();
                       if (length > 0) {
                           alert('Only one subject can be optional.');
                       }
                       else {
                           $('#list1 > option:selected').appendTo('#list3');
                           $('#list3 > option').prop("selected", true);
                       }
                   });
                   $('#btnRemoveOp').click(function (e) {
                       $('#list3 > option').appendTo('#list1');
                       e.preventDefault();
                   });
               });
        /*******************************************************************************************/
        function Load() {
            $("#example1 [id*=chkstudentHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example1 [id*=chkStudentRow]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkStudentRow]").prop("checked", false);
                }
            });

            $("#example1 [id*=chkStudentRow]").click(function () {
                if ($("#example1 [id*=chkStudentRow]").length == $("#example1 [id*=chkStudentRow]:checked").length) {
                    $("#example1 [id*=chkstudentHeader]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkstudentHeader]").prop("checked", false);
                }
            });
            /*******************************************************************************/

            $("#example1 [id*=chkSubjectHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example1 [id*=chkSubjectRow]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkSubjectRow]").prop("checked", false);
                }
            });

            $("#example1 [id*=chkSubjectRow]").click(function () {
                if ($("#example1 [id*=chkSubjectRow]").length == $("#example1 [id*=chkSubjectRow]:checked").length) {
                    $("#example1 [id*=chkSubjectHeader]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkSubjectHeader]").prop("checked", false);
                }
            });
        }
        $(function () {
            Load();
        });
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                Load();
            });
        };
    </script>
</asp:Content>


