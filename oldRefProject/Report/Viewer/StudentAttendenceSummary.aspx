<%@ Page Title="<%$ Resources:Application,StudentAttendanceSummary %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="StudentAttendenceSummary.aspx.cs" Inherits="Report_Viewer_StudentAttendenceSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Pages/../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <script src="../../Pages/../Scripts/bootstrap-timepicker.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class='<%= Common.SessionInfo.Panel %>'>
        <div class="panel-heading">
            <h4>
                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label></h4>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,AttendenceType %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlAttendenceType" runat="server" CssClass="form-control dropdown">
                                <asp:ListItem Value="1" Text="Class Attendence"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Exam Attendence"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,FromDate %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxFromDate" runat="server" placeholder="Enter From Date" CssClass="form-control" ClientIDMode="Static"
                                MaxLength="10" required="required"></asp:TextBox>
                            <%-- <label id="lblformdate"></label>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,ToDate %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxToDate" runat="server" placeholder="Enter To Date" CssClass="form-control" ClientIDMode="Static" MaxLength="10" required="required"></asp:TextBox>
                            <%-- <label id="lbltodate" ></label>      --%>
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
            <asp:Button ID="btnReport" CssClass="btn btn-success" runat="server" Text="<%$ Resources:Application,GenerateReport %>" ValidationGroup="save" OnClick="btnReport_Click" />
        </div>
    </div>
    <%--OnClientClick="return validation()"--%>

    <script type="text/javascript">
        $(function () {
            $("#tbxFromDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#tbxToDate").datepicker({ dateFormat: 'dd/mm/yy' });
        });
        $("#tbxFromDate").on('change', function () {

            var text = $('#<%=tbxFromDate.ClientID %>').val();
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

            var currentDate = new Date();

            if (DateFormate(document.getElementById("tbxFromDate").value) > currentDate) {
                alert("Please select smaller Date than Current Date. ");
                $(this).val(Today());
            }

            var toDate = DateFormate(document.getElementById("tbxToDate").value);

            if (document.getElementById("tbxToDate").value != '') {
                if (DateFormate(document.getElementById("tbxFromDate").value) > toDate) {
                    alert("To Date must be smaller than To Date. ");
                    $(this).val(toDate.format('dd/MM/yyyy'));
                }
            }

        });

            $("#tbxToDate").on('change', function () {

                var text = $('#<%=tbxToDate.ClientID %>').val();
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
                var currentDate = new Date();
                if (DateFormate(document.getElementById("tbxToDate").value) > currentDate) {
                    alert("Please select smaller Date than Current Date. ");
                    $(this).val(Today());
                }
                var fromDate = DateFormate(document.getElementById("tbxFromDate").value);
                if (document.getElementById("tbxFromDate").value != '') {
                    if (DateFormate(document.getElementById("tbxToDate").value) < fromDate) {
                        alert("To Date must be bigger than From Date. ");
                        $(this).val(Today());
                    }
                }
            });
            function DateFormate(x) {
                var todate = x.substring(0, 2);
                var toMonth = x.substring(3, 5);
                var toYear = x.substring(6, 10);
                var toDate = new Date(toYear, toMonth - 1, todate);
                return toDate;
            }
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
    </script>
</asp:Content>



