<%@ Page Title="<%$ Resources:Application,ClassAttendanceSetup %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="StudentAttendence.aspx.cs" Inherits="Pages_Administration_StudentAttendence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <div class="padding-bottom-15" runat="server" Visible="False">
        <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="2" CssClass="form-control FormatRadioButtonList">
            <asp:ListItem Value="1" Text="<%$ Resources:Application,DailyAttendence %>" Selected="True"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class='<%= Common.SessionInfo.Panel %>'>
        <div class="panel-heading">
            <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
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
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
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
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" />
                </div>
            </div>

            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll%>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter Roll No." MaxLength="9" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo%>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" MaxLength="10" CssClass="form-control"></asp:TextBox>
                            <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="tbxReg" FilterType="Custom" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-1">
                            <asp:Label ID="lblYear" runat="server" Text="<%$ Resources:Application,SendSMS %>"></asp:Label></label>
                        <div class="col-sm-1">
                            <asp:CheckBox ID="chkSMS" runat="server" OnCheckedChange="chkSMS_CheckedChanged" ClientMode="Static"/>
                        </div>
                        <div class="col-sm-10">
                            <div class="padding-bottom-15" id="divStudentCriteria">
                            <asp:RadioButtonList ID="rdlSMS" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" BorderColor="White">
                            <asp:ListItem Value="1" Text="For All"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Absent Only"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Present Only"></asp:ListItem>
                           </asp:RadioButtonList>
                           </div>
                        </div>
                    </div>
                   
                </div>

            </div>
        </div>
    </div>
    <div class="col-lg-6 col-md-6">
        <div class="form-horizontal">
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-1">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></label>
                <div class="col-sm-3">
                    <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxDate" ValidationGroup="save"
                        ErrorMessage="Please Select Date"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlSMS" runat="server" ClientIDMode="Static">
    
   
        
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal" id="divPresentSMS">                    
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:Label ID="lblSubject" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Body %>"></asp:Label></label>
                        <div class="col-sm-10">
                            <asp:Label ID="lblBody" runat="server"></asp:Label>
                        </div>
                    </div>
                  
                </div>
                <div class="form-horizontal" id="divAbsentSMS">
                   <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:Label ID="lblSub" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Body %>"></asp:Label></label>
                        <div class="col-sm-10">
                            <asp:Label ID="lblbdy" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>               
       
    </asp:Panel>
    <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
        
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
               
            </div>
            <div class="panel-body">               
                   
                        <asp:Repeater ID="rptStudentRoll" runat="server" OnItemDataBound="rptStudentRoll_ItemDataBound">
                            <HeaderTemplate>
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll %>">
                                                </asp:Label>
                                                <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>">
                                                </asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRoll" Text='<%#Eval("RollNo") %>' runat="server"/>
                                        <asp:Label ID="lblReg" Text='<%#Eval("RegNo") %>' runat="server"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" Text='<%#Eval("NameEng") %>' runat="server" Visible="True" />
                                        <asp:Label ID="hdnStudentId" Text='<%#Eval("StudentToClassId") %>' runat="server" Visible="false" />
                                        <asp:Label ID="hdnMobile" Text='<%#Eval("Mobile") %>' runat="server" Visible="false" />
                                        <asp:Label ID="hdnEmail" Text='<%#Eval("Email") %>' runat="server" Visible="false" />
                                        <asp:Label ID="hdnUser" Text='<%#Eval("UserName") %>' runat="server" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkrow" runat="server" /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>                  
                                
            </div>
            <div class="panel-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Submit" ValidationGroup="save" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        $(document).ready(function () {
            $("#divStudentCriteria").hide();
            $("#pnlSMS").hide();
            $("#<%= chkSMS.ClientID %>").change(function () {
                if ($(this).is(':checked')) {
                    //Here try to call the handler using jquery ajax.                    
                    $("#divStudentCriteria").toggle();
                    $("#pnlSMS").slideDown();
                    $("#divPresentSMS").hide();
                    $("#divAbsentSMS").hide();
                    $("#rdlSMS").change(function () {

                        var checked_radio = $("[id*=rdlSMS] input:checked");

                        if (checked_radio.val() == "1") {                          
                            $("#divPresentSMS").slideDown();
                            $("#divAbsentSMS").slideDown();
                        }
                        else if (checked_radio.val() == "2") {
                           
                            $("#divAbsentSMS").slideDown();
                            $("#divPresentSMS").slideUp();   
                        }
                        else {
                            $("#divPresentSMS").slideDown();
                            $("#divAbsentSMS").slideUp();
                        }
                    });
                    
                
            } else {
                    
                    var radiolist = $('#rdlSMS').find('input:radio');
                    radiolist.removeAttr('checked');
                    $("#divStudentCriteria").slideUp();
                    $("#pnlSMS").slideUp();
                    // Do something
                }
            });
            document.getElementById('tbxDate').value = Today();

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
        });
    </script>
</asp:Content>

