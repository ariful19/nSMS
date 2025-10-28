<%@ Page Title="<%$ Resources:Application,StudentAttendance %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Attendence.aspx.cs" Inherits="Pages_Student_Attendence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="panel panel-success">
        <div class="panel-body">
            
            <div class="row">
                <div class="col-sm-6">
                    <div class="padding-bottom-15">
                        <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="2" CssClass="FormatRadioButtonList">
                              <asp:ListItem Value="1" Text="<%$ Resources:Application,DailyAttendence %>" Selected="True"></asp:ListItem>
                              <asp:ListItem Value="2" Text="<%$ Resources:Application,ExamAttendence %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="padding-bottom-15">
                        <asp:RadioButtonList ID="rdAttendence" runat="server" ClientIDMode="Static" RepeatColumns="2" CssClass="FormatRadioButtonList">
                            <asp:ListItem Value="1" Text="Monthly"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Date"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="row" id="date">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label4" runat="server" Text="From Date"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxFromDate" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10"></asp:TextBox>                                          
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxToDate" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                                                             
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="month">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label2" runat="server" Text="Month"></asp:Label></label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control dropdown">
                                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>  
                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" CssClass="btn btn-flat btn-success" Text="Show Attendence"/>  
                          
                </div>
                <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
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
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll%>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter Roll No." MaxLength="10" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                            Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
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

           
        </div>
    </div>
    <asp:UpdatePanel ID="updateAttendence" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <asp:Repeater ID="rptStudent" runat="server">
                                <HeaderTemplate>
                                    <table id="example11" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                                <th>Status
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Convert.ToDateTime(Eval("Date")).ToString("dd-MMM-yyyy") %></td>
                                        <td><%#Eval("NameEng") %>
                                            <asp:HiddenField ID="hdnStudentId" Value='<%#Eval("StudentToClassId") %>' runat="server" />
                                        </td>
                                        <td><%#Eval("IsPresent").ToString()=="True"?"Present":"Absent" %>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
     <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#tbxFromDate").datepicker({
                dateFormat: 'dd/mm/yy'

            });

            $("#tbxToDate").datepicker({
                dateFormat: 'dd/mm/yy'

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
                

            $("#month").hide();
            $("#date").hide();
            $("#btnShow").hide();
            $("#rdAttendence").change(function () {
                var checked_radio = $("[id*=rdAttendence] input:checked");
                if (checked_radio.val() == "1") {
                    $("#month").slideDown();
                    $("#btnShow").slideDown();
                    $("#date").hide();
                }
                else {
                    $("#date").slideDown();
                    $("#btnShow").slideDown();
                    $("#month").hide();
                }
            });
        });
       
      
     </script>
</asp:Content>

