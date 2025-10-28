<%@ Page Title="<%$ Resources:Application,DailyMarksEntry %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="DailyMarksEntry.aspx.cs" Inherits="Pages_Result_DailyMarksEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        Sys.Application.add_load(Load);
    </script>
    <div class='<%= Common.SessionInfo.Panel %>'>
        <div class="panel-heading">
            Criteria
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"
                                OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"
                                OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"
                                OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,ExamType %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlExamType" runat="server" DataTextField="ExamType" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,MarksOutof %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMarksOutOf" runat="server" DataTextField="DailyOrWeeklyMarks" DataValueField="Id" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="SubjectName" DataValueField="SubjectId" CssClass="form-control dropdown"
                                OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label15" runat="server" Text="Exam Date"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxDate" ValidationGroup="save"
                                ErrorMessage="Please Select Date"></asp:RequiredFieldValidator>
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
        <div class="panel panel-footer">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-success" Text="<%$ Resources:Application,Search %>" />
        </div>
    </div>
    <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                Student List
            </div>
            <div class="panel-body">
                <div class="box">
                    <div class="box-body">
                        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                            <HeaderTemplate>
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                            <th>
                                                <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label>
                                                <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                            </th>
                                            <th>Name</th>
                                            <th>Get Marks</th>
                                            <th>Remarks</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkrow" runat="server" /></td>
                                    <td>
                                        <asp:Label ID="lblRoll" Text='<%#Eval("RollNo") %>' runat="server" />
                                        <asp:Label ID="lblReg" Text='<%#Eval("RegNo") %>' runat="server" />
                                    </td>
                                    <td>
                                        <%#Eval("NameEng") %>
                                        <asp:HiddenField ID="hdnStudentId" Value='<%#Eval("StudentToClassId") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="getMarksTextBox" runat="server" Width="100%" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ControlToValidate="getMarksTextBox" ValidationExpression="^([0-9]{1,2}){1}(\.[0-9]{1,2})?$|^100$"
                                            ErrorMessage="Please enter valid number!" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredAttendanceTextBox" runat="server"
                                            Enabled="True" TargetControlID="getMarksTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="remarksTextBox" runat="server" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
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
            <div class="panel panel-footer">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </asp:Panel>
    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            //document.getElementById('tbxDate').value = Today();

            $("#tbxDate").datepicker({
                dateFormat: "dd/mm/yy",
                showOtherMonths: true,
                selectOtherMonths: true,
            });
            $("#tbxDate").on('change', function () {

                var currentDate = new Date();
                var text = $('#tbxDate').val();
                var comp = text.split('/');
                var d = parseInt(comp[0], 10);
                var m = parseInt(comp[1], 10);
                var y = parseInt(comp[2], 10);
                var date = new Date(y, m - 1, d)

                if (date.getFullYear() == y && date.getMonth() + 1 == m && date.getDate() == d) {

                } else {
                    alert('Invalid date');
                    $(this).val(Today());
                }

                if (date > currentDate) {
                    alert("Please select smaller Date than Current Date. ");
                    $(this).val(Today());
                }


            });

            function Today() {
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!

                var yyyy = today.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                var today = dd + '/' + mm + '/' + yyyy;
                return today;
            }
        });
        function Load() {
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
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

