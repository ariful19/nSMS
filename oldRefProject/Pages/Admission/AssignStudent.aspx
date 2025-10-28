<%@ Page Title="Assign Student" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AssignStudent.aspx.cs" Inherits="Pages_Admission_AssignStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables_themeroller.css" rel="stylesheet" />
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Multipart.css" rel="stylesheet" />
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />

    <style>
        .error {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div id="msform">
      
        <div class="col-md-12 col-sm-12 col-xs-12">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
            <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
            <asp:HiddenField ID="hdnID" runat="server" />
        </div>
        <fieldset>
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdlMedium" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="<%$ Resources:Application,BanglaMedium %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$ Resources:Application,EnglishMedium %>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="<%$ Resources:Application,EnglishVersion %>"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                      
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxRoll" runat="server" CssClass="form-control" placeholder="Enter Roll No"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                            Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789.">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1_roll" Display="None" runat="server" ErrorMessage="Please Enter Roll Number" ControlToValidate="tbxRoll">*</asp:RequiredFieldValidator>
                                        

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                   
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxRegNo" runat="server" placeholder="Enter Registration No." CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Registration No." ControlToValidate="tbxRegNo">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label><span class="required">*</span>
                                    <asp:HiddenField ID="hdfStudentAdmissionID" runat="server" />
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxNameEng" runat="server" placeholder="Enter Name(English)" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Name(English)" ControlToValidate="tbxNameEng">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxNameBan" runat="server" placeholder="Enter Name(Bangla)" CssClass="form-control"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Name(Bangla)" ControlToValidate="tbxNameBan">*</asp:RequiredFieldValidator>--%>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,Gender %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlGender" runat="server" DataTextField="Gender" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,Religion %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlReligion" runat="server" DataTextField="Religion" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,BloodGroup %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBlood" runat="server" CssClass="form-control dropdown" DataTextField="BloodGroup" DataValueField="Id">
                                    </asp:DropDownList>
                                </div>
                            </div>
                              <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,DateofBirth %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lbldateofbirth" style="color: red"></label>
                                        <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxDateOfBirth"
                                        TargetControlID="tbxDateOfBirth"></cc1:CalendarExtender>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxDateOfBirth" ValidationGroup="save"
                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                            ErrorMessage="Invalid Date of Birth format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Date of Birth" ControlToValidate="tbxDateOfBirth">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">

                                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,serialno %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlserialno" runat="server" DataTextField="SerialNo" AutoPostBack="true" DataValueField="StudentAdmissionID" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlserialno_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,ClassApplying %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtClassAppling" ReadOnly="true" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,BirthCertificateNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate No." CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                      
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,PhoneHome %>"></asp:Label><span style="color: green"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPhnHome" runat="server" placeholder="Enter Phone(Home)" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                        Enabled="True" TargetControlID="tbxPhnHome" FilterType="Custom" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,MobileHome %>"></asp:Label><span style="color: green"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMobHome" runat="server" placeholder="Enter Mobile(Home)" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        Enabled="True" TargetControlID="tbxMobHome" FilterType="Custom" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,Phone %>"></asp:Label><span style="color: green"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        Enabled="True" TargetControlID="tbxPhone" FilterType="Custom" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label24" runat="server" Text="<%$  Resources:Application,SMSReceiverNumber  %>"></asp:Label><span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                        Enabled="True" TargetControlID="tbxMobile" FilterType="Custom" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Application,StudentAccountInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxUserName" runat="server" Enabled="false" placeholder="Enter User Name" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter User Name" ControlToValidate="tbxUserName">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Application,Password %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPassword" runat="server" TextMode="Password" placeholder="Enter Password" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Password" ControlToValidate="tbxPassword">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Application,ConfirmPassword %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxConfirmPassword" runat="server" TextMode="Password" placeholder="Enter Confirm Password" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbxPassword"
                                        ControlToValidate="tbxConfirmPassword" Display="Dynamic"
                                        ErrorMessage="Password and Confirm Password are not same." Operator="Equal">Password and Confirm Password are not same.</asp:CompareValidator>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Confirm Password" ControlToValidate="tbxConfirmPassword">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="chkShowPassword">
                                    <asp:CheckBox ID="chkDummy" runat="server" ClientIDMode="Static" />
                                    <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Application,Doyouwanttocreateadummyusernameandpassword %>"></asp:Label></label>
                            </div>

                            <div class="form-group">
                                <label for="chkShowPassword">
                                    <input type="checkbox" id="chkShowPassword" />
                                    <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,Showpassword %>"></asp:Label></label>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <asp:Button ID="btnSave" runat="server" CssClass="action-button" ValidationGroup="save" OnClick="btnSave_Click" Text="<%$ Resources:Application,Save %>" Width="100px" />
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script src="../../JS/Student.js"></script>
    <script>
        $(document).ready(function () {
           
            var radiolist = $('#rdlMedium').find('input:radio');
            radiolist.removeAttr('checked');

            $("#tbxDateOfBirth").datepicker({
                dateFormat: 'dd/mm/yy',
            });

            $('#tbxDateOfBirth').change(function () {
                var start = $('#tbxDateOfBirth').val().split('/');
                var dob = new Date(start[2], start[1] - 1, start[0]);
                var end = new Date();
                var days = "";
                if (dob < end) {
                    days = (end - dob) / 1000 / 60 / 60 / 24;
                }
                if (days < 365 * 3) {
                    //alert("Your bellow three yers");
                    $('#tbxDateOfBirth').val('');
                    $('#lbldateofbirth').text('You are bellow three years');
                }
                else {
                    $('#lbldateofbirth').text('');
                }

            });

            $("#<%= rdlMedium.ClientID %>").change(function () {
              
                var checked_radio = $("[id*=rdlMedium] input:checked");
                var value = checked_radio.val();

                $.ajax({
                    type: "POST",
                    url: "Student.aspx/LoadRegistrationNo",
                    contentType: "application/json; charset=utf-8",
                    data: '{"roll":"' + value + '"}',
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d) {
                            $('#' + '<%=tbxRegNo.ClientID %>').val(msg.d);
                            $('#' + '<%=tbxUserName.ClientID %>').val(msg.d);
                            //$('#tbxRegNo').val(msg.d.roll);                           
                        }
                    },
                    error: function (req, status, error) {
                        alert("Error try again");
                    }
                });
                return false;
            });
        });
    </script>
</asp:Content>

