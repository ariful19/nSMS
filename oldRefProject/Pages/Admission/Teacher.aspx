<%@ Page Title="<%$ Resources:Application,TeacherPersonalInformation %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="~/Pages/Admission/Teacher.aspx.cs" CodeFile="~/Pages/Admission/Teacher.aspx.cs" Inherits="Pages_Admission_Teacher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Multipart.css" rel="stylesheet" />
    <link href="../../Styles/Table.css" rel="stylesheet" />

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
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,TeacherPersonalInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,TeacherPINNo %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxRegNo" runat="server" placeholder="Enter PIN No." ClientIDMode="Static" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter PIN No." ControlToValidate="tbxRegNo">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label86" runat="server" Text="<%$ Resources:Application,EmployeeId %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxEmpId" runat="server" placeholder="Enter Employee ID" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Employee ID" ControlToValidate="tbxEmpId">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxNameEng" runat="server" placeholder="Enter Name(English)" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Name(English)" ControlToValidate="tbxNameEng">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxNameEng" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxNameBan" runat="server" placeholder="Enter Name(Bangla)" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label88" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Designation" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group" id="divCatagory" runat="server" visible="false">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="lbl20" runat="server" Text="Level"></asp:Label><span class="required">*</span>
                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCatagory" runat="server" CssClass="form-control-dropdown">
                                       <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Pre-Primary Section"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Primary Section"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="High School Section"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="College Section"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Gender %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlGender" runat="server" DataTextField="Gender" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Religion %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlReligion" runat="server" DataTextField="Religion" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,BloodGroup %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBlood" runat="server" CssClass="form-control dropdown" DataTextField="BloodGroup" DataValueField="Id">
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label87" runat="server" Text="<%$ Resources:Application,Nationality %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxNationality" runat="server" placeholder="Enter Nationality" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,NationalIdNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxNID" runat="server" placeholder="Enter National Id No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxNIDExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                             <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label89" runat="server" Text="Bank Account"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxBankAccount" runat="server" placeholder="Enter Account Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,BirthCertificateNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxBirthCertificateExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxBirthCertificate" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,DateofBirth %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxDateOfBirth" ValidationGroup="save" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" ErrorMessage="Invalid Date of Birth format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Date of Birth" ControlToValidate="tbxDateOfBirth">*</asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxDateOfBirth"
                                        TargetControlID="tbxDateOfBirth"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,PhoneHome %>"></asp:Label><span style="color: green"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPhnHome" runat="server" placeholder="Enter Phone(Home)" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxPhnHomeExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxPhnHome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,MobileHome %>"></asp:Label><span style="color: green"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMobHome" runat="server" placeholder="Enter Mobile(Home)" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxMobHomeExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxMobHome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="tbxMobHome" ErrorMessage="Must start with 01 and Enter Valid Mobile Number!" ValidationExpression="^[0][1][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$" ValidationGroup="Save" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,Phone %>"></asp:Label><span style="color: green"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxPhoneExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxPhone" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,SMSReceiverNumber%>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile No." CssClass="form-control" Text="01" MaxLength="11"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxMobileExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxMobile" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="tbxMobile" ErrorMessage="Must start with 01 and Enter Valid Mobile Number!" ValidationExpression="^[0][1][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$" ValidationGroup="Save" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Email Address" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator
                                        ID="regEmail"
                                        ControlToValidate="tbxEmail"
                                        Text="Invalid email"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,Fax %>"></asp:Label><span style="color: green"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFax" runat="server" placeholder="Enter Fax No." CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,JoinDate %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxJoinDate" runat="server" placeholder="Enter Joining Date" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbxJoinDate" ValidationGroup="save" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" ErrorMessage="Invalid Joining Date format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Join Date" ControlToValidate="tbxJoinDate">*</asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxJoinDate"
                                        TargetControlID="tbxJoinDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,TeacherFatherInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,FatherName %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFathername" runat="server" placeholder="Enter Father Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Father Name" ControlToValidate="tbxFathername">*</asp:RequiredFieldValidator>



                                    <asp:RegularExpressionValidator ControlToValidate="tbxFathername" ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,FatherEduQualification %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlFatherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Application,FatherProfession %>"></asp:Label><span class="required"></span></label>
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
                                    <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,FatherPhoneNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherPhn" runat="server" placeholder="Enter Father Phone No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxFatherPhnExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Application,FatherNationalID %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherNID" runat="server" placeholder="Enter Father National ID" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxFatherNIDExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Application,FatherYearlyIncome %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherIncome" runat="server" placeholder="Enter Father Yearly Income" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxFatherIncomeExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherIncome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Application,TeacherMotherInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,MotherName %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherName" runat="server" placeholder="Enter Mother Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Mother Name" ControlToValidate="tbxMotherName">*</asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ControlToValidate="tbxMotherName" ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label31" runat="server" Text="<%$ Resources:Application,MotherEduQualification %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlMotherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label32" runat="server" Text="<%$ Resources:Application,MotherProfession %>"></asp:Label><span class="required"></span></label>
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
                                    <asp:Label ID="Label33" runat="server" Text="<%$ Resources:Application,MotherPhoneNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherPhn" runat="server" placeholder="Enter Mother Phone No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxMotherPhnExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label34" runat="server" Text="<%$ Resources:Application,MotherNationalID %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherNID" runat="server" placeholder="Enter Mother National ID" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxMotherNIDExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label35" runat="server" Text="<%$ Resources:Application,MotherYearlyIncome %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherIncome" runat="server" placeholder="Enter Mother Yearly Income" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxMotherIncomeExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherIncome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label82" runat="server" Text="Pay scale"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label83" runat="server" Text="Grade"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPayScaleGrade" runat="server" DataTextField="Type" DataValueField="Id" CssClass="form-control" OnSelectedIndexChanged="ddlPayScaleGrade_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label90" runat="server" Text="Level"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlLevel" runat="server" DataTextField="LevelName" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdateSalary" runat="server">
                                <ContentTemplate>


                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label84" runat="server" Text="Pay Scale"></asp:Label><span class="required">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblPayScale" runat="server"></asp:Label>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label85" runat="server" Text="Basic Salary"></asp:Label><span class="required">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblBasicSalary" runat="server"></asp:Label>

                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlPayScaleGrade" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label36" runat="server" Text="<%$ Resources:Application,TeacherAccountInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label37" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxUserName" runat="server" ClientIDMode="Static" placeholder="Enter User Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter User Name" ControlToValidate="tbxUserName">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label38" runat="server" Text="<%$ Resources:Application,Role %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlRole" runat="server" DataTextField="RoleName" DataValueField="RoleName" CssClass="dropdown form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label39" runat="server" Text="<%$ Resources:Application,Password %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPassword" runat="server" ClientIDMode="Static" TextMode="Password" placeholder="Enter Password" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Password" ControlToValidate="tbxPassword">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label40" runat="server" Text="<%$ Resources:Application,ConfirmPassword %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxConfirmPassword" runat="server" ClientIDMode="Static" TextMode="Password" placeholder="Enter Confirm Password" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Confirm Password" ControlToValidate="tbxConfirmPassword">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbxPassword"
                                        ValidationGroup="save" ControlToValidate="tbxConfirmPassword" Display="Dynamic"
                                        ErrorMessage="Password and Confirm Password are not same." Operator="Equal">
                                        <asp:Label ID="Label41" runat="server" Text="<%$ Resources:Application,PasswordandConfirmPasswordarenotsame %>"></asp:Label>
                                    </asp:CompareValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-1">
                                    <asp:CheckBox ID="chkDummy" runat="server" ClientIDMode="Static" />
                                </div>
                                <label for="inputEmail3" class="col-sm-10">
                                    <asp:Label ID="Label42" runat="server" Text="<%$ Resources:Application,Doyouwanttocreateadummyusernameandpassword %>"></asp:Label></label>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <input type="button" name="next" class="next action-button" runat="server" value="<%$ Resources:Application,Next %>" />
        </fieldset>
        <fieldset>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label43" runat="server" Text="<%$ Resources:Application,PresentAddress %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label44" runat="server" Text="<%$ Resources:Application,Division %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPresentDiv" ClientIDMode="Static" runat="server" DataTextField="Division" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label45" runat="server" Text="<%$ Resources:Application,District %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPresentDis" ClientIDMode="Static" runat="server" DataTextField="District" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label46" runat="server" Text="<%$ Resources:Application,Thana %>"></asp:Label></label>
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
                                    <asp:Label ID="Label47" runat="server" Text="<%$ Resources:Application,PostOffice %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPostOffice" runat="server" ClientIDMode="Static" placeholder="Enter Post Office" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label48" runat="server" Text="<%$ Resources:Application,PostalCode %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPostalCode" runat="server" ClientIDMode="Static" placeholder="Enter Present Postal Code" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxPostalCodeExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxPostalCode" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label49" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
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
                    <asp:Label ID="Label50" runat="server" Text="<%$ Resources:Application,PermanentAddressSame %>"></asp:Label>


                </div>
                <div class="panel-body">
                    <%--<div class="text-left">
                                <asp:CheckBox ID="CheckBox1" runat="server" />Same as Present
                            </div>--%>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label51" runat="server" Text="<%$ Resources:Application,Division %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPermanentDiv" ClientIDMode="Static" runat="server" DataTextField="Division" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label52" runat="server" Text="<%$ Resources:Application,District %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPermanentDis" ClientIDMode="Static" runat="server" DataTextField="District" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label53" runat="server" Text="<%$ Resources:Application,Thana %>"></asp:Label></label>
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
                                    <asp:Label ID="Label54" runat="server" Text="<%$ Resources:Application,PostOffice %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPermanentPost" runat="server" ClientIDMode="Static" placeholder="Enter Post Office" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label55" runat="server" Text="<%$ Resources:Application,PostalCode %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPermanentPostCode" runat="server" ClientIDMode="Static" placeholder="Enter permanent Postal Code" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxPermanentPostCodeExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxPermanentPostCode" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label56" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
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
                    <asp:Label ID="Label57" runat="server" Text="<%$ Resources:Application,TeacherEducation %>"></asp:Label>

                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12">
                        <div class="form-horizontal">


                            <%--This is test----------------------------------------------------------%>
                            <asp:GridView ID="gvEducation" CssClass="table" runat="server" AutoGenerateColumns="false" DataKeyNames="TeacherId">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Degree %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("DegreeName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Board %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("Board") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Subject %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("Subject") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Year %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("PassingYear") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Grade %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("Grade") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,GPAScale %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("GPAScale")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Division %>" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                        <ItemTemplate>
                                            <%# Eval("ResultDivision")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Action %>" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" ClientIDMode="Static"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <%--This is test----------------------------------------------------------%>
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#myModal">
                                <i class="fa fa-plus"></i>
                                <asp:Label ID="Label64" runat="server" Text="<%$ Resources:Application,Addnew %>"></asp:Label></button>
                            <!-- Modal -->
                            <div class="modal fade" id="myModal" role="dialog">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header bg-danger">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">
                                                <asp:Label ID="Label58" runat="server" Text="<%$ Resources:Application,TeacherEducationInformation %>"></asp:Label></h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-horizontal">
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Add" />
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label59" runat="server" Text="<%$ Resources:Application,DegreeName %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlDegree" ClientIDMode="Static" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown" >
                                                           <%-- <asp:ListItem Value="1" Text="SSC"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="HSC"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="A Level"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="O Level"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="Alim"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Dakhil"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="Honours"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="Master"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="Diploma"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="dvSubject">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label60" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="tbxSubject" CssClass="form-control" placeholder="Enter Subject Name"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label61" runat="server" Text="<%$ Resources:Application,BordUniversity %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="tbxBoard" CssClass="form-control" placeholder="Board or University Name"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label62" runat="server" Text="<%$ Resources:Application,PassingYear %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="tbxYear" CssClass="form-control" placeholder="Enter Passing Year"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredtbxYearExtender" runat="server"
                                                            Enabled="True" TargetControlID="tbxYear" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label63" runat="server" Text="<%$ Resources:Application,Result %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlResult" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
                                                            <asp:ListItem Value="1" Text="First"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Second"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Third"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Grade"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="dvGrade">
                                                    <label for="inputEmail3" class="col-sm-4"></label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox runat="server" ID="tbxGrade" CssClass="form-control" placeholder="Grade Point"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox runat="server" ID="tbxScale" CssClass="form-control" placeholder="Out of"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button class="btn btn-default" ID="btnAdd" runat="server" Text="<%$ Resources:Application,Add %>" OnClick="btnedu_Click"></asp:Button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,Close %>"></asp:Label></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label66" runat="server" Text="<%$ Resources:Application,TeacherTraining %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12">
                        <div class="form-horizontal">
                            <asp:GridView ID="gvTraining" ClientIDMode="Static" CssClass="table" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,TrainingName %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("TrainingName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,InstituteName %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("InstituteName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,StartDate %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("StartDate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="<%$ Resources:Application,EndDate %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("EndDate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Topics %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("Topics") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Duration %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                        <ItemTemplate>
                                            <%# Eval("Duration")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Application,Action %>" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete11" runat="server" ClientIDMode="Static"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#Training">
                                <i class="fa fa-plus"></i>
                                <asp:Label ID="Label67" runat="server" Text="<%$ Resources:Application,AddNew %>"></asp:Label></button>

                            <!-- Modal -->
                            <div class="modal fade" id="Training" role="dialog">
                                <div class="modal-dialog">

                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header bg-danger">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">
                                                <asp:Label ID="Label68" runat="server" Text="<%$ Resources:Application,TeacherTrainingInformation %>"></asp:Label></h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label69" runat="server" Text="<%$ Resources:Application,TrainingName %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxTrainingName" runat="server" CssClass="form-control" placeholder="Enter Training Name"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label70" runat="server" Text="<%$ Resources:Application,InstituteName %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxInstitute" runat="server" CssClass="form-control" placeholder="Enter Institute Name"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label71" runat="server" Text="<%$ Resources:Application,StartDate %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxStartDate" runat="server" CssClass="form-control" placeholder="Enter Start Date" ClientIDMode="Static"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="tbxStartDate" Format="dd/MM/yyyy"
                                                            TargetControlID="tbxStartDate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label72" runat="server" Text="<%$ Resources:Application,EndDate %>"></asp:Label></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxEndDate" runat="server" CssClass="form-control" placeholder="Enter End Date" ClientIDMode="Static"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="tbxEndDate" Format="dd/MM/yyyy"
                                                            TargetControlID="tbxEndDate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="Label73" runat="server" Text="<%$ Resources:Application,Topics %>"></asp:Label>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxTopics" runat="server" CssClass="form-control" placeholder="Enter Topics" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="btnTraining" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Application,Add %>" />
                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                <asp:Label ID="Label74" runat="server" Text="<%$ Resources:Application,Close %>"></asp:Label></button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label75" runat="server" Text="<%$ Resources:Application,OtherInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-5">
                                    <asp:Label ID="Label76" runat="server" Text="<%$ Resources:Application,SonDaughterGrandchildofFreedomFighter %>"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:CheckBox ID="chkFreedom" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-5">
                                    <asp:Label ID="Label77" runat="server" Text="<%$ Resources:Application,Tribal %>"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:CheckBox ID="chkTribal" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-5">
                                    <asp:Label ID="Label78" runat="server" Text="<%$ Resources:Application,PhysicallyDefect %>"></asp:Label></label>
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
                <div class="col-lg-4 col-md-4" align="center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label79" runat="server" Text="<%$ Resources:Application,TeacherPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:Image ID="imgTeacher" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                            <asp:FileUpload ID="uploderTeacher" runat="server" CssClass="btn btn-default" /><br />

                            <p class="help-block">Note: All Images .png .jpeg .jpg are allowed.</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4" align="center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label80" runat="server" Text="<%$ Resources:Application,FatherPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:Image ID="imgFather" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                            <asp:FileUpload ID="uploadFather" runat="server" CssClass="btn btn-default" /><br />


                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4" align="center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label81" runat="server" Text="<%$ Resources:Application,MotherPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:Image ID="imgMother" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                            <asp:FileUpload ID="uploadMother" runat="server" CssClass="btn btn-default" /><br />
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Only .jpg/.png/jpeg image allowed" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpeg|.jpg|.png)$" ControlToValidate="uploadMother"></asp:RegularExpressionValidator>--%>
                        </div>
                    </div>
                </div>
            </div>
            <input type="button" name="previous" class="previous action-button" runat="server" value="<%$ Resources:Application,Previous %>" />
            <asp:Button ID="btnSave" runat="server" class="btn action-button" ValidationGroup="save" OnClick="btnSave_Click" Text="<%$ Resources:Application,Save %>" />
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="../../Scripts/jquery.easing.min.js"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/languages/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/jquery.validationEngine.js"
        charset="utf-8"></script>
    <script src="../../JS/Teacher.js"></script>
</asp:Content>
