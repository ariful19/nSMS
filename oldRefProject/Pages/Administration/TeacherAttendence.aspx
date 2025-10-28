<%@ Page Title="<%$ Resources:Application,TeacherAttendence %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="TeacherAttendence.aspx.cs" Inherits="Pages_Administration_TeacherAttendence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />

    <div class="padding-bottom-15">
        <div class="form-horizontal">
        <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="2" CssClass="form-control FormatRadioButtonList">
            <asp:ListItem Value="1" Text="<%$ Resources:Application,ByDate %>"></asp:ListItem>
            <asp:ListItem Value="2" Text="ByEmployee"></asp:ListItem>
        </asp:RadioButtonList>
           <br/>
             <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                             </div>
                      </div>
             </div>
    </div>
    
    
    <div class='<%= Common.SessionInfo.Panel %>' id="1">
        <div class="panel-heading">
            <asp:Label ID="Label31" runat="server" Text="<%$ Resources:Application,TeacherList %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div>
                <div class="form-horizontal">                   
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label><span class="required">*</span></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" ClientIDMode="Static" OnTextChanged="tbxDate_TextChanged" ReadOnly="true"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbxDate" ValidationGroup="save"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                    ErrorMessage="Date format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                            <asp:Label ID="lblError" Text="Selected date must be smaller than current date." runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box">
                <div class="box-body">
                    <asp:Repeater ID="rptTeacher" runat="server">
                        <HeaderTemplate>
                            <table id="example11" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:Label ID="Label31" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Intime %>"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,OutTime %>"></asp:Label></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("NameEng") %>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("TeacherId") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxIn" runat="server" CssClass="form-control" placeholder="Ex: 10:00"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regexStartTime" ControlToValidate="tbxIn"
                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="Input valid time: ex: 10:00" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxOut" runat="server" CssClass="form-control" placeholder="Ex: 04:00"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="tbxOut"
                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="Input valid time. ex: 04:00" runat="server" />
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

    <div class='<%= Common.SessionInfo.Panel %>' id="2">
        <div class="panel-heading">
            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,TeacherList %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label4" runat="server" Text="EmployeeName"></asp:Label></label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlEmployee" runat="server" DataTextField="NameEng" DataValueField="TeacherId" CssClass="form-control dropdown" ></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6" align="right">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-6">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Archive %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxArchive" runat="server" CssClass="form-control" ClientIDMode="Static" OnTextChanged="tbxArchive_TextChanged" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatortbxArchive" runat="server" ControlToValidate="tbxArchive" ValidationGroup="save"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                    ErrorMessage="Date format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box">
                <div class="box-body">
                    <asp:Repeater ID="rptDate" runat="server">
                        <HeaderTemplate>
                            <table id="example12" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,InTime %>"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,OutTime %>"></asp:Label></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Convert.ToDateTime(Eval("Date")).ToString("dd-MM-yyyy") %>
                                    <asp:HiddenField ID="hdnDate" runat="server" Value='<%# Eval("Date")%>' />
                                </td>
                                <td class="bootstrap-timepicker">
                                    <asp:TextBox ID="intime" CssClass="form-control timepicker" runat="server" Text="" Enabled='<%#Convert.ToDateTime(Eval("Date"))>DateTime.Now?false:true %>'></asp:TextBox>

                                </td>
                                <td class="bootstrap-timepicker">
                                    <asp:TextBox ID="tbxOut" runat="server" CssClass="form-control timepicker" Text="" placeholder="Ex: 04:00" Enabled='<%#Convert.ToDateTime(Eval("Date"))>DateTime.Now?false:true %>'></asp:TextBox>
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

    <div id="3">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="<%$ Resources:Application,Submit %>" />
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="script">
    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $(".timepicker").timepicker({
            showInputs: false
        });
        $(document).ready(function () {
            $("#lblError").hide();
          
            $("#tbxDate").datepicker({
                dateFormat: "dd/mm/yy",              
                showOtherMonths: true,
                selectOtherMonths: true,                
               
            });
            $("#tbxDate").on('change', function () {
                             
                var currentDate = new Date();
                if (DateFormate(document.getElementById("tbxDate").value) > currentDate) {
                    alert("Please select smaller Date than Current Date. ");
                    $(this).val(Today());
                }

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

            });
            $("#1").hide();
            $("#2").hide();
            $("#3").hide();
            $("#rdList").change(function () {
                var checked_radio = $("[id*=rdList] input:checked");
                if (checked_radio.val() == "1") {
                    $("#1").slideDown();
                    $("#2").hide();
                    $("#3").show();
                }
                else {
                    $("#2").slideDown();
                    $("#1").hide();
                    $("#3").show();
                }
            });
        });
        $(function () {
            $("#tbxDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#tbxArchive").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
</asp:Content>



