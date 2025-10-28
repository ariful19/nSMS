<%@ Page Title="<%$ Resources:Application,AddStudent %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Student.aspx.cs" Inherits="Pages_Admission_Student" %>

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
        <ul id="progressbar">
            <li class="active">
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,PersonalDetails %>"></asp:Label></li>
            <li>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,ContactEducation %>"></asp:Label></li>
            <li>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Photograph %>"></asp:Label></li>
        </ul>
        <div class="col-md-12 col-sm-12 col-xs-12">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
            <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
            <asp:HiddenField ID="hdnID" runat="server" />
        </div>
        <fieldset>
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdlMedium" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" OnSelectedIndexChanged="rdlMedium_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="1" Text="Bangla Version" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$ Resources:Application,EnglishVersion %>"></asp:ListItem>
                    <asp:ListItem Value="4" Text="<%$ Resources:Application,EnglishMedium %>"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="col-lg-12 col-md-12">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label><span class="required">*</span></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div id="chkboxDiv" runat="server">
                <asp:CheckBox ID="chkAssign" ClientIDMode="Static" runat="server" />
                <b>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,AssignStudenttoClass %>"></asp:Label></b>
            </div>
                </div>
            </div>
            
            <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,AssignStudenttoClass %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" OnSelectedIndexChanged="ddlMedium_OnSelectedIndexChanged" AutoPostBack="True" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlGroup_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
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
                                <div class="form-group" id="stuRoll" runat="server" visible="False">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="lblRoll" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxRoll" runat="server" CssClass="form-control" placeholder="Enter Roll No" MaxLength="10"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                            Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <asp:Panel ClientIDMode="Static" ID="pnlStuPersonalInfo" runat="server">
                    <div class="panel-heading">
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,StudentPersonalInformation %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxRegNo" runat="server" placeholder="Enter Student ID" BorderStyle="Double" ReadOnly="True" BorderColor="RosyBrown" CssClass="form-control" MaxLength="40" OnTextChanged="tbxRegNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Student ID" ControlToValidate="tbxRegNo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxNameEng" runat="server" placeholder="Enter Name(English)" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Name(English)" ControlToValidate="tbxNameEng">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ControlToValidate="tbxNameEng" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxNameBan" runat="server" placeholder="Enter Name(Bangla)" CssClass="form-control"></asp:TextBox>
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
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,BirthCertificateNo %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate No." CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,DateofBirth %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lbldateofbirth" style="color: red"></label>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxDateOfBirth"
                                            TargetControlID="tbxDateOfBirth"></cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxDateOfBirth" ValidationGroup="save"
                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                            ErrorMessage="Invalid Date of Birth format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Date of Birth" ControlToValidate="tbxDateOfBirth">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,PhoneHome %>"></asp:Label><span style="color: green"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxPhnHome" runat="server" placeholder="Enter Phone(Home)" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            Enabled="True" TargetControlID="tbxPhnHome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,MobileHome %>"></asp:Label><span style="color: green"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxMobHome" runat="server" placeholder="Enter Mobile(Home)" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" TargetControlID="tbxMobHome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,Phone %>"></asp:Label><span style="color: green"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone No." CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            Enabled="True" TargetControlID="tbxPhone" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label24" runat="server" Text="<%$  Resources:Application,SMSReceiverNumber  %>"></asp:Label><span style="color: red">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile No." CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            Enabled="True" TargetControlID="tbxMobile" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator7" ControlToValidate="tbxMobile" ValidationExpression="[0-9]{11}" ErrorMessage="Please enter 11 digit number!" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>' id="Div">
                <asp:Panel ClientIDMode="Static" ID="pnlStuAccountInfo" runat="server">
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
                                        <asp:TextBox ID="tbxUserName" runat="server" ReadOnly="True" placeholder="Enter User Name" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
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

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label71" runat="server" Text="Studentship"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Status" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
                </asp:Panel>
            </div>
            <input type="button" name="next" class="next action-button" value="<%$ Resources:Application,Next %>" runat="server" id="btnNext" />
        </fieldset>
        <fieldset>
            <div class="'<%= Common.SessionInfo.Panel %>'">
                <div class="panel-heading">
                    <asp:Label ID="Label31" runat="server" Text="<%$ Resources:Application,StudentFatherInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label32" runat="server" Text="<%$ Resources:Application,FatherName %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFathername" runat="server" placeholder="Enter Father Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Father Name" ControlToValidate="tbxFathername">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxFathername" ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label33" runat="server" Text="<%$ Resources:Application,FatherEduQualification %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlFatherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label34" runat="server" Text="<%$ Resources:Application,FatherProfession %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlFatherPro" runat="server" DataTextField="Profession" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label35" runat="server" Text="<%$ Resources:Application,FatherPhoneNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherPhn" runat="server" placeholder="Enter Father Phone No." CssClass="form-control" MaxLength="12"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label36" runat="server" Text="<%$ Resources:Application,FatherNationalID %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherNID" runat="server" placeholder="Enter Father National ID" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label37" runat="server" Text="<%$ Resources:Application,FatherYearlyIncome %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherIncome" runat="server" placeholder="Enter Father Yearly Income" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherIncome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label38" runat="server" Text="<%$ Resources:Application,StudentMotherInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label39" runat="server" Text="<%$ Resources:Application,MotherName %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherName" runat="server" placeholder="Enter Mother Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Mother Name" ControlToValidate="tbxMotherName">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxMotherName" ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label40" runat="server" Text="<%$ Resources:Application,MotherEduQualification %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlMotherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label41" runat="server" Text="<%$ Resources:Application,MotherProfession %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlMotherPro" runat="server" DataTextField="Profession" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label42" runat="server" Text="<%$ Resources:Application,MotherPhoneNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherPhn" runat="server" placeholder="Enter Mother Phone No." CssClass="form-control" MaxLength="12"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label43" runat="server" Text="<%$ Resources:Application,MotherNationalID %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherNID" runat="server" placeholder="Enter Mother National ID" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label44" runat="server" Text="<%$ Resources:Application,MotherYearlyIncome %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherIncome" runat="server" placeholder="Enter Mother Yearly Income" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherIncome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label66" runat="server" Text="<%$ Resources:Application,StudentLocalInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label67" runat="server" Text="<%$ Resources:Application,LocalGuardian1 %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocalGuardian1" runat="server" placeholder="Enter Local Guardian - 1 Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxLocalGuardian1" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label68" runat="server" Text="<%$ Resources:Application,LocalGuardian2 %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocalGuardian2" runat="server" placeholder="Enter Local Guardian - 2 Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxLocalGuardian2" ID="RegularExpressionValidator6" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label69" runat="server" Text="<%$ Resources:Application,LocalGuardian1MobileNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocG1MobileNo" runat="server" placeholder="Enter L. Guardian-1 Mobile No." CssClass="form-control" MaxLength="13"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                        Enabled="True" TargetControlID="tbxLocG1MobileNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label70" runat="server" Text="<%$ Resources:Application,LocalGuardian2MobileNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocG2MobileNo" runat="server" placeholder="Enter L. Guardian-2 Mobile No." CssClass="form-control" MaxLength="13"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                        Enabled="True" TargetControlID="tbxLocG2MobileNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label45" runat="server" Text="<%$ Resources:Application,PresentAddress %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label46" runat="server" Text="<%$ Resources:Application,Division %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPresentDiv" ClientIDMode="Static" runat="server" DataTextField="Division" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label47" runat="server" Text="<%$ Resources:Application,District %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPresentDis" ClientIDMode="Static" runat="server" DataTextField="District" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label48" runat="server" Text="<%$ Resources:Application,Thana %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPresentThana" ClientIDMode="Static" runat="server" DataTextField="Thana" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label49" runat="server" Text="<%$ Resources:Application,PostOffice %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPostOffice" runat="server" ClientIDMode="Static" placeholder="Enter Post Office" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label50" runat="server" Text="<%$ Resources:Application,PostalCode %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPostalCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                        Enabled="True" TargetControlID="tbxPostalCode" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label51" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPresentAddress" runat="server" ClientIDMode="Static" placeholder="Enter Address" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:CheckBox ID="chkPresent" ClientIDMode="Static" runat="server" />
                    <asp:Label ID="Label58" runat="server" Text="<%$ Resources:Application,PermanentAddressSame %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label52" runat="server" Text="<%$ Resources:Application,Division %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPermanentDiv" ClientIDMode="Static" runat="server" DataTextField="Division" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label53" runat="server" Text="<%$ Resources:Application,District %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPermanentDis" ClientIDMode="Static" runat="server" DataTextField="District" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label54" runat="server" Text="<%$ Resources:Application,Thana %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPermanentThana" ClientIDMode="Static" runat="server" DataTextField="Thana" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label55" runat="server" Text="<%$ Resources:Application,PostOffice %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPermanentPost" runat="server" ClientIDMode="Static" placeholder="Enter Post Office" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label56" runat="server" Text="<%$ Resources:Application,PostalCode %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPermanentPostCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label57" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPermanentAddress" runat="server" ClientIDMode="Static" placeholder="Enter Address" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label59" runat="server" Text="<%$ Resources:Application,OtherInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-5">
                                    <asp:Label ID="Label60" runat="server" Text="<%$ Resources:Application,SonDaughterGrandchildofFreedomFighter %>"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:CheckBox ID="chkFreedom" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-5">
                                    <asp:Label ID="Label61" runat="server" Text="<%$ Resources:Application,Tribal %>"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:CheckBox ID="chkTribal" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-5">
                                    <asp:Label ID="Label62" runat="server" Text="<%$ Resources:Application,PhysicallyDefect %>"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:CheckBox ID="chkPhyDef" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <input type="button" name="previous" class="previous action-button" runat="server" value="<%$ Resources:Application,Previous %>" />
            <input type="button" name="next" class="next action-button" runat="server" value="<%$ Resources:Application,Next %>" />
        </fieldset>
        <fieldset>
            <div class="row">
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label63" runat="server" Text="<%$ Resources:Application,StudentPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:Image ID="imgStudent" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                            <asp:FileUpload ID="uploderStudent" runat="server" onChange="ShowThumbnail()" CssClass="btn btn-default" /><br />
                            <p class="help-block">Note: All Images .png .jpeg .jpg are allowed.</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label64" runat="server" Text="<%$ Resources:Application,FatherPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:Image ID="imgFather" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                            <asp:FileUpload ID="uploadFather" runat="server" CssClass="btn btn-default" /><br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,MotherPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:Image ID="imgMother" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                            <asp:FileUpload ID="uploadMother" runat="server" CssClass="btn btn-default" /><br />
                        </div>
                    </div>
                </div>
            </div>
            <input type="button" name="previous" class="previous action-button" runat="server" value="<%$ Resources:Application,Previous %>" />
            <asp:Button ID="btnSave" runat="server" CssClass="action-button" ValidationGroup="save" OnClick="btnSave_Click" Text="<%$ Resources:Application,Save %>" Width="100px" />
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script src="../../JS/Student.js"></script>
    <script>
        $(document).ready(function () {
            $('#tbxDateOfBirth').change(function () {
                var start = $('#tbxDateOfBirth').val().split('/');
                var dob = new Date(start[2], start[1] - 1, start[0]);
                var end = new Date();
                var days = "";
                if (dob < end) {
                    days = (end - dob) / 1000 / 60 / 60 / 24;
                }
                if (days < 365 * 2) {
                    // As requsted by Sagor Addmission date of birth change 3 to 2 years.
                    //alert("Your bellow three yers");
                    $('#tbxDateOfBirth').val('');
                    $('#lbldateofbirth').text('You are bellow Two years');
                }
                else {
                    $('#lbldateofbirth').text('');
                }

            });

            <%--  $("#<%= rdlMedium.ClientID %>").change(function () {
                $("#<%= chkboxDiv.ClientID %>").slideDown();
                $("#<%= pnlStuPersonalInfo.ClientID %>").slideDown();
                $("#<%= pnlStuAccountInfo.ClientID %>").slideDown();
                $("#<%= btnNext.ClientID %>").slideDown();
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
                                                     
                        }
                    },
                    error: function (req, status, error) {
                        alert("Error try again");
                    }
                });
                return false;
            });--%>
        });
    </script>
</asp:Content>

