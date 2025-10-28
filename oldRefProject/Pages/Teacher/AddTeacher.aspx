<%@ Page Title="<%$ Resources:Application,AddTeacher %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AddTeacher.aspx.cs" Inherits="Pages_Teacher_AddTeacher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/MultipartFormControl.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="board">
                <!-- <h2>Welcome to IGHALO!<sup>™</sup></h2>-->
                <div class="board-inner">
                    <ul class="nav nav-tabs" id="myTab">
                        <li class="active">
                            <a href="#home" data-toggle="tab" title="welcome">
                                <span class="round-tabs one">
                                    <i class="fa fa-user"></i>Personal
                                </span>
                            </a></li>

                        <li class="disabled"><a href="#profile" data-toggle="tab" title="profile">
                            <span class="round-tabs two">
                                <i class="fa fa-hospital-o"></i>contact
                            </span>
                        </a>
                        </li>
                        <li class="disabled"><a href="#messages" data-toggle="tab" title="bootsnipp goodies">
                            <span class="round-tabs three">
                                <i class="fa fa-graduation-cap"></i>Education/Training
                            </span></a>
                        </li>

                        <li class="disabled"><a href="#settings" data-toggle="tab" title="blah blah">
                            <span class="round-tabs four">
                                <i class="fa fa-dollar"></i>Payment
                            </span>
                        </a></li>

                        <li class="disabled"><a href="#doner" data-toggle="tab" title="completed">
                            <span class="round-tabs five">
                                <i class="fa fa-camera"></i>Photo
                            </span></a>
                        </li>

                    </ul>
                </div>

                <div class="tab-content">
                    <div class="tab-pane fade in active" id="home">
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
                                                <asp:TextBox ID="tbxRegNo" runat="server" placeholder="Enter PIN No." ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Registration No." ControlToValidate="tbxRegNo">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxNameEng" runat="server" placeholder="Enter Name(English)" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Name(English)" ControlToValidate="tbxNameEng">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label><span class="required"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxNameBan" runat="server" placeholder="Enter Name(Bangla)" CssClass="form-control"></asp:TextBox>
                                              <%--  <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Name(Bangla)" ControlToValidate="tbxNameBan">*</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Designation" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
                                                <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,NationalIdNo %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxNID" runat="server" placeholder="Enter National Id No." CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,BirthCertificateNo %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate No." CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-lg-6 col-md-6">
                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,DateofBirth %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth"></asp:TextBox>
                                                                                                   
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxDateOfBirth" ValidationGroup="save" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" ErrorMessage="Invalid Date of Birth format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>

                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Date of Birth" ControlToValidate="tbxDateOfBirth">*</asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxDateOfBirth"
                                                    TargetControlID="tbxDateOfBirth">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,PhoneHome %>"></asp:Label><span style="color: green"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxPhnHome" runat="server" placeholder="Enter Phone(Home)" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,MobileHome %>"></asp:Label><span style="color: green"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMobHome" runat="server" placeholder="Enter Mobile(Home)" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,Phone %>"></asp:Label><span style="color: green"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone No." CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile No." CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label><span class="required"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Email Address" CssClass="form-control"></asp:TextBox>
                                             
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
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Join Date" ControlToValidate="tbxJoinDate">*</asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxJoinDate"
                                                    TargetControlID="tbxJoinDate">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>
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
                        <button type="submit" href="#profile" name="home_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="fa fa-arrow-circle-o-right"></span></button>
                    </div>
                    <div class="tab-pane fade" id="profile">
                        <div class='<%= Common.SessionInfo.Panel %>'>
                            <div class="panel-heading">
                                <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,TeacherFatherInformation %>"></asp:Label>
                            </div>
                            <div class="panel-body">
                                <div class="col-lg-6 col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,FatherName %>"></asp:Label><span class="required"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxFathername" runat="server" placeholder="Enter Father Name" CssClass="form-control"></asp:TextBox>
                                              <%--  <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Father Name" ControlToValidate="tbxFathername">*</asp:RequiredFieldValidator>--%>
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
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Application,FatherNationalID %>"></asp:Label><span class="required"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxFatherNID" runat="server" placeholder="Enter Father National ID" CssClass="form-control"></asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator13" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Father National ID" ControlToValidate="tbxFatherNID">*</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Application,FatherYearlyIncome %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxFatherIncome" runat="server" placeholder="Enter Father Yearly Income" CssClass="form-control"></asp:TextBox>
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
                                                <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,MotherName %>"></asp:Label><span class="required"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMotherName" runat="server" placeholder="Enter Mother Name" CssClass="form-control"></asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Mother Name" ControlToValidate="tbxMotherName">*</asp:RequiredFieldValidator>--%>
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
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label34" runat="server" Text="<%$ Resources:Application,MotherNationalID %>"></asp:Label><span class="required"></span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMotherNID" runat="server" placeholder="Enter Mother National ID" CssClass="form-control"></asp:TextBox>
                                              <%--  <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Mother National ID" ControlToValidate="tbxMotherNID">*</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label35" runat="server" Text="<%$ Resources:Application,MotherYearlyIncome %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMotherIncome" runat="server" placeholder="Enter Mother Yearly Income" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                                              <%--  <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Post Office" ControlToValidate="tbxPostOffice">*</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label48" runat="server" Text="<%$ Resources:Application,PostalCode %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxPostalCode" runat="server" ClientIDMode="Static" placeholder="Enter Present Postal Code" CssClass="form-control"></asp:TextBox>
                                            <%--    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator22" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Postal Code" ControlToValidate="tbxPostalCode">*</asp:RequiredFieldValidator>--%>
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
                <%--                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator28" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Postal Code" ControlToValidate="tbxPermanentPostCode">*</asp:RequiredFieldValidator>--%>
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
                        <button type="submit" href="#messages" name="profile_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="fa fa-arrow-circle-o-right"></span></button>
                    </div>
                    <div class="tab-pane fade" id="messages">
                        <div class='<%= Common.SessionInfo.Panel %>'>
                            <div class="panel-heading">
                                <asp:Label ID="Label57" runat="server" Text="<%$ Resources:Application,TeacherEducation %>"></asp:Label>

                            </div>
                            <div class="panel-body">
                                <div class="col-lg-12 col-md-12">
                                    <div class="form-horizontal">
                                        <%--This is test----------------------------------------------------------%>
                                        <asp:GridView ID="gvEducation" CssClass="table" runat="server" AutoGenerateColumns="false">
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
                                                                    <asp:DropDownList ID="ddlDegree" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
                                                                        <asp:ListItem Value="1" Text="SSC"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="HSC"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="A Level"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="O Level"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="Alim"></asp:ListItem>
                                                                        <asp:ListItem Value="6" Text="Dakhil"></asp:ListItem>
                                                                        <asp:ListItem Value="7" Text="Honours"></asp:ListItem>
                                                                        <asp:ListItem Value="8" Text="Master"></asp:ListItem>
                                                                        <asp:ListItem Value="9" Text="Diploma"></asp:ListItem>
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
                                                        <asp:Button class="btn btn-default" ID="btnAdd" runat="server" Text="<%$ Resources:Application,Add %>"></asp:Button>
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
                                                                    <asp:TextBox ID="tbxStartDate" runat="server" CssClass="form-control" placeholder="Enter Start Date"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="tbxStartDate" Format="dd/MM/yyyy"
                                                                        TargetControlID="tbxStartDate">
                                                                    </cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-4">
                                                                    <asp:Label ID="Label72" runat="server" Text="<%$ Resources:Application,EndDate %>"></asp:Label></label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="tbxEndDate" runat="server" CssClass="form-control" placeholder="Enter End Date"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="tbxEndDate" Format="dd/MM/yyyy"
                                                                        TargetControlID="tbxEndDate">
                                                                    </cc1:CalendarExtender>
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
                        <button type="submit" href="#settings" name="messages_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>

                    </div>
                    <div class="tab-pane fade" id="settings">

                        <button type="submit" href="#doner" name="settings_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>
                    </div>
                    <div class="tab-pane fade" id="doner">
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('a[title]').tooltip();

            $('.btn-submit').on('click', function (e) {
                var formname = $(this).attr('name');
                var tabname = $(this).attr('href');
                e.preventDefault();
                $('ul.nav li a[href="' + tabname + '"]').parent().removeClass('disabled');
                $('ul.nav li a[href="' + tabname + '"]').trigger('click');
            });
            $('ul.nav li').on('click', function (e) {
                if ($(this).hasClass('disabled')) {
                    e.preventDefault();
                    return false;
                }
            });
        });
    </script>
</asp:Content>

