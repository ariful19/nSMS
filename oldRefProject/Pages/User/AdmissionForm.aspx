<%@ Page Title="Admission Form" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AdmissionForm.aspx.cs" Inherits="Pages_User_AdmissionForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
    <link href="../../Styles/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables_themeroller.css" rel="stylesheet" />
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Multipart.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="msform">

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-4">
                <asp:RadioButton ID="rdoEnglishVersion" GroupName="g1" Checked="true" Text="English Version" runat="server" />
            </div>
            <div class="col-md-4">
                <asp:RadioButton ID="rdoEnglishMedium" GroupName="g1" Text="English Medium" runat="server" />
            </div>
            <div class="col-md-4">
                <asp:RadioButton ID="rdoBanglaMedium" GroupName="g1" Text="Bangla Medium" runat="server" />
            </div>
        </div>


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
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,StudentPersonalInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label91" runat="server" Text="<%$ Resources:Application,ClassApplying %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlClassApplying" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>


                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Surname %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxSurName" runat="server" placeholder="Enter Surname" CssClass="form-control"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Surname" ControlToValidate="tbxNameEng">*</asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxNameEng" runat="server" placeholder="Enter Name(English)" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Name(English)" ControlToValidate="tbxNameEng">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label111" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtNameBan" runat="server" placeholder="Enter Name(Bangla)" CssClass="form-control"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Name(Bangla)" ControlToValidate="txtNameBan">*</asp:RequiredFieldValidator>--%>

                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,DateofBirth %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Enter Date of Birth" ClientIDMode="Static"></asp:TextBox>
                                     <label id="lbldateofbirth" style="color: red"></label>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxDateOfBirth"
                                        TargetControlID="tbxDateOfBirth"></cc1:CalendarExtender>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Nationality %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtNationality" runat="server" placeholder="Enter Nationality" CssClass="form-control"></asp:TextBox>

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
                                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,Gender %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlGender" runat="server" DataTextField="Gender" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

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
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Nationality %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Nationality" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label36" runat="server" Text="<%$ Resources:Application,FatherNationalID %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherNID" runat="server" placeholder="Enter Father National ID" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Father National ID" ControlToValidate="tbxFatherNID">*</asp:RequiredFieldValidator>
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
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label94" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtFatherEmail" runat="server" placeholder="Ex. Jhon@gmail.com" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator
                                        ID="regEmail"
                                        ControlToValidate="txtFatherEmail"
                                        Text="Invalid email"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        runat="server" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Organization %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtOrganization" runat="server" placeholder="Enter Organization" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label35" runat="server" Text="<%$ Resources:Application,FatherPhoneNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherPhn" runat="server" placeholder="Enter Father Phone No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                        Enabled="True" TargetControlID="tbxFatherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Mobile %>">></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtFatherMobileNo" runat="server" placeholder="Enter Father Mobile No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        Enabled="True" TargetControlID="txtFatherMobileNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label37" runat="server" Text="<%$ Resources:Application,FatherYearlyIncome %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxFatherIncome" runat="server" placeholder="Enter Father Yearly Income" CssClass="form-control"></asp:TextBox>
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
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label85" runat="server" Text="<%$ Resources:Application,Nationality %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMothereNationality" runat="server" placeholder="Enter Nationality" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label43" runat="server" Text="<%$ Resources:Application,MotherNationalID %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherNID" runat="server" placeholder="Enter Mother National ID" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
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
                                    <asp:TextBox ID="tbxMotherPhn" runat="server" placeholder="Enter Mother Phone No." CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                        Enabled="True" TargetControlID="tbxMotherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,Organization %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMothergOrganization" runat="server" placeholder="Enter Sibling Organization" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label92" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMotherDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label93" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMotherEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label44" runat="server" Text="<%$ Resources:Application,MotherYearlyIncome %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxMotherIncome" runat="server" placeholder="Enter Mother Yearly Income" CssClass="form-control"></asp:TextBox>
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
                    <asp:Label ID="Label112" runat="server" Text="<%$ Resources:Application,StudentLocalInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label113" runat="server" Text="Guardian-1 Name"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocalGuardian1" runat="server" placeholder="Enter Guardian-1 Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxLocalGuardian1" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label114" runat="server" Text="Guardian-2 Name "></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocalGuardian2" runat="server" placeholder="Enter Guardian-2 Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxLocalGuardian2" ID="RegularExpressionValidator6" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>   
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label115" runat="server" Text="Guardian-1 Mobile"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocG1MobileNo" runat="server" placeholder="Enter Guardian-1 Mobile" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                        Enabled="True" TargetControlID="tbxLocG1MobileNo" FilterType="Custom" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label116" runat="server" Text="Guardian-2 Mobile"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxLocG2MobileNo" runat="server" placeholder="Enter Guardian-2 Mobile" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                        Enabled="True" TargetControlID="tbxLocG2MobileNo" FilterType="Custom" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,StudentSiblingsInfo %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtSiblingName" runat="server" placeholder="Enter Sibling Name" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Gender %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSiblingGender" runat="server" DataTextField="Gender" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label107" runat="server" Text="<%$ Resources:Application,Age %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtSiblingAge" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>


                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Profession %>"> </asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSiblingOccupation" runat="server" DataTextField="Profession" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label102" runat="server" Text="<%$ Resources:Application,Organization %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtSibInstituteName" runat="server" placeholder="Enter Institute Name" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label95" runat="server" Text="<%$ Resources:Application,PreviousStudyInfo %>"></asp:Label>
                    <asp:CheckBox ID="chkPreviousStudy" ClientIDMode="Static" runat="server" />
                </div>
                <asp:Panel ID="pnlPreviousStudy" ClientIDMode="Static" runat="server">
                    <div class="panel-body">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label96" runat="server" Text="<%$ Resources:Application,SchoolName %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPreSchoolName" runat="server" placeholder="Enter School Name" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label97" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPreAddress" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label100" runat="server" Text="<%$ Resources:Application,LastGrade %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPreLastGrade" runat="server" placeholder="Enter Last grade/class" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label101" runat="server" Text="<%$ Resources:Application,AnnualResult %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPreResult" runat="server" placeholder="Enter Annual assessment/Result" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label98" runat="server" Text="<%$ Resources:Application,Telephone %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtpreTelephon" runat="server" placeholder="Enter Telephone" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            Enabled="True" TargetControlID="txtpreTelephon" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label103" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPreEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label99" runat="server" Text="<%$ Resources:Application,Fax %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPreFax" runat="server" placeholder="Enter Fax" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="'<%= Common.SessionInfo.Panel %>'">
                <div class="panel-heading">
                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,ReferencesInfo %>"></asp:Label>
                    <asp:CheckBox ID="chkReference" runat="server" ClientIDMode="Static" />
                </div>
                <asp:Panel ID="pnlReference" ClientIDMode="Static" runat="server">
                    <div class="panel-body">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label><span class="required">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtReferenceName" runat="server" placeholder="Enter Reference Name" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,Nationality %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtReferenceNationality" runat="server" placeholder="Enter Nationality" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,NationalIdNo %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRefNationalId" runat="server" placeholder="Enter Father National ID" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            Enabled="True" TargetControlID="txtRefNationalId" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label106" runat="server" Text="<%$ Resources:Application,RelationCandidate %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRefRelation" runat="server" placeholder="Enter Relation with the candidate" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,EduQualification %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlRefEduQuali" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Application,Profession %>"></asp:Label><span class="required"></span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlReferenceProfession" runat="server" DataTextField="Profession" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>



                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,Organization %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="ddlReferenceOrganization" runat="server" placeholder="Enter Organization" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,YearlyIncome %> "></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRefTearlyIncome" runat="server" placeholder="Enter Father Yearly Income" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                            Enabled="True" TargetControlID="txtRefTearlyIncome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label66" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtReferenceAddress" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRefDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtReferenceMobile" runat="server" placeholder="Enter Mobile No" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            Enabled="True" TargetControlID="txtReferenceMobile" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRefeEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <input type="button" name="next" class="next action-button" value="<%$ Resources:Application,Next %>" runat="server" />
        </fieldset>
        <fieldset>
            <div class="'<%= Common.SessionInfo.Panel %>'">
                <div class="panel-heading">
                    <asp:Label ID="Label67" runat="server" Text="<%$ Resources:Application,HealthInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label68" runat="server" Text="<%$ Resources:Application,Height %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtHeight" runat="server" placeholder="Enter Height" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label69" runat="server" Text="<%$ Resources:Application,Weight %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtWeight" runat="server" placeholder="Enter Weight" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label70" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label><span class="required"></span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtHealthDate" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtHealthDate"
                                        TargetControlID="txtHealthDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label72" runat="server" Text="<%$ Resources:Application,PhysicalDrawBack %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPhysicalDrawback" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
                                        <asp:ListItem Value="0">--- select --</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label73" runat="server" Text="<%$ Resources:Application,DrawBackDetails %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtDrawbackDetails" disabled="True" ClientIDMode="Static" runat="server" placeholder="Enter Draw-back details" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label71" runat="server" Text="<%$ Resources:Application,BloodGroup %>"></asp:Label><span class="required">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBlood" runat="server" CssClass="form-control dropdown" DataTextField="BloodGroup" DataValueField="Id">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label75" runat="server" Text="<%$ Resources:Application,FoodMedicines %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMedicinesGroup" runat="server" placeholder="Enter Medicines(Group Name)" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>


                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label79" runat="server" Text="<%$ Resources:Application,AllergyProblem %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlAlergyProb" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
                                        <asp:ListItem Value="0">--- select --</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label74" runat="server" Text="<%$ Resources:Application,AvoidForAllergy %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtAvoidable" runat="server" disabled="True" ClientIDMode="Static" placeholder="Enter Avoidable" CssClass="form-control"></asp:TextBox>
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
                                    <%--<asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Post Office" ControlToValidate="tbxPostOffice">*</asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label50" runat="server" Text="<%$ Resources:Application,PostalCode %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxPostalCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control"></asp:TextBox>
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
                                    <asp:TextBox ID="tbxPermanentPostCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control"></asp:TextBox>
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
                                    <asp:Label ID="Label76" runat="server" Text="<%$ Resources:Application,IsGameNeedAvoide %>"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlGameAvoid" Width="350px" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
                                        <asp:ListItem Value="0">--- select --</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-5">
                                    <asp:Label ID="Label77" runat="server" Text="<%$ Resources:Application,Details %>"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtGameAvoidDetail" disabled="True" Width="350px" runat="server" ClientIDMode="Static" placeholder="Enter Details" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
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

            <div class="'<%= Common.SessionInfo.Panel %>'">
                <div class="panel-heading">
                    <asp:Label ID="Label78" runat="server" Text="<%$ Resources:Application,SchoolFeedbackInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label84" runat="server" Text="<%$ Resources:Application,SchoolFeedBack %> "></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSchoolFeedback" runat="server" CssClass="form-control dropdown">
                                        <asp:ListItem Value="0">--- select --</asp:ListItem>
                                        <asp:ListItem Value="1">News Papers</asp:ListItem>
                                        <asp:ListItem Value="2">Advirtisement</asp:ListItem>
                                        <asp:ListItem Value="2">Friends/Relatives</asp:ListItem>
                                        <asp:ListItem Value="2">Internet</asp:ListItem>
                                        <asp:ListItem Value="2">Others</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label80" runat="server" Text="<%$ Resources:Application,ChosenCauseOfSc %> "></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtChooseReason" runat="server" placeholder="Why have you chosen SISB?" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label81" runat="server" Text="<%$ Resources:Application,ExpectationFromSc %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtExpectation" runat="server" placeholder="What your expectations from SISB?" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>



                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label86" runat="server" Text="<%$ Resources:Application,IsKnownAnyone %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlInstiteKnown" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
                                        <asp:ListItem Value="0">--- select --</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label87" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtInsKnownName" runat="server" disabled="True" ClientIDMode="Static" placeholder="Enter Name" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label88" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtInsKnownDesignation" disabled="True" ClientIDMode="Static" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>

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
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label ID="Label63" runat="server" Text="<%$ Resources:Application,StudentPhoto %>"></asp:Label></h3>
                            </div>
                            <div class="panel-body center-block">
                                <asp:Image ID="imgStudent" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                                <asp:FileUpload ID="uploderStudent" runat="server" Width="200" CssClass="btn btn-default" /><br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label ID="Label64" runat="server" Text="<%$ Resources:Application,FatherPhoto %>"></asp:Label></h3>
                            </div>
                            <div class="panel-body center-block">
                                <asp:Image ID="imgFather" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                                <asp:FileUpload ID="uploadFather" runat="server" Width="200" CssClass="btn btn-default" /><br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,MotherPhoto %>"></asp:Label></h3>
                            </div>
                        </div>
                        <div class="panel-body center-block">
                            <asp:Image ID="imgMother" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                            <asp:FileUpload ID="uploadMother" Width="200" runat="server" CssClass="btn btn-default" /><br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label ID="Label82" runat="server" Text="<%$ Resources:Application,Declatration %>"></asp:Label></h3>
                            </div>
                            <div class="panel-body center-block">
                                <asp:Label ID="Label110" runat="server" Text="<%$ Resources:Application,DeclarationAplication %>"></asp:Label><br />
                                <asp:Label ID="Label109" runat="server" Text="<%$ Resources:Application,DeclarationAplication1 %>"></asp:Label><br />
                                <asp:Label ID="Label108" runat="server" Text="<%$ Resources:Application,DeclarationAplication2 %>"></asp:Label><br />
                                <asp:Label ID="Label105" runat="server" Text=" <%$ Resources:Application,DeclarationAplication3 %>"></asp:Label><br />
                                <br />
                                <asp:Label ID="Label104" runat="server" Text="<%$ Resources:Application,StudentSignature %>"></asp:Label>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label83" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:TextBox ID="txtStuDeclarationName" runat="server" placeholder="Enter Name" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">

                                <asp:Label ID="Label89" runat="server" Text="<%$ Resources:Application,Signature %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" placeholder="Signature" CssClass="form-control"></asp:TextBox>
                            <asp:FileUpload ID="uploadSignature" Width="200" runat="server" CssClass="btn btn-default" /><br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 text-center">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label90" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body center-block">
                            <asp:TextBox ID="txtDateofSignature" runat="server" ReadOnly="true" placeholder="DD/MM/YYYY" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDateofSignature"></cc1:CalendarExtender>

                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-success">
                <input type="button" name="previous" class="previous action-button" runat="server" value="<%$ Resources:Application,Previous %>" />
                <asp:Button ID="btnSave" runat="server" CssClass="action-button" ValidationGroup="save" OnClick="btnSave_Click" Text="<%$ Resources:Application,Save %>" Width="100px" />
            </div>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
     <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script src="../../JS/StudentAdmission.js"></script>
    <script>
        $(document).ready(function() {
            $('#tbxDateOfBirth').change(function() {
                var start = $('#tbxDateOfBirth').val().split('/');
                var dob = new Date(start[2], start[1] - 1, start[0]);
                var end = new Date();
                var days = "";
                if (dob < end) {
                    days = (end - dob) / 1000 / 60 / 60 / 24;
                }
                if (days < 365 * 3) {
                    $('#tbxDateOfBirth').val();
                    $('#lbldateofbirth').text('You are bellow Three years!!!');
                }
                else
                {
                    $('#lbldateofbirth').text('');
                }
            });
        });
    </script>
</asp:Content>

