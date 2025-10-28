<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="Pages_Student_AddStudent" %>

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
                                    <i class="fa fa-lock"></i>Account
                                </span>
                            </a></li>
                     
                        <li class="disabled"><a href="#profile" data-toggle="tab" title="profile">
                            <span class="round-tabs two">
                                <i class="fa fa-user"></i>Personal
                            </span>
                        </a>
                        </li>
                        <li class="disabled"><a href="#messages" data-toggle="tab" title="bootsnipp goodies">
                            <span class="round-tabs three">
                                <i class="fa fa-group"></i>Parent
                            </span></a>
                        </li>

                        <li class="disabled"><a href="#settings" data-toggle="tab" title="blah blah">
                            <span class="round-tabs four">
                                <i class="fa fa-home"></i>Address
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
                        <div class="col-sm-12">
                            <div class='<%= Common.SessionInfo.Panel %>'>
                                <div class="panel-heading">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,StudentAccountInformation %>"></asp:Label>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6 col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label><span class="required">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="tbxUserName" runat="server" placeholder="Enter User Name" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                                        ErrorMessage="Enter User Name" ControlToValidate="tbxUserName">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Password %>"></asp:Label><span class="required">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="tbxPassword" runat="server" TextMode="Password" placeholder="Enter Password" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                                                        ErrorMessage="Enter Password" ControlToValidate="tbxPassword">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,ConfirmPassword %>"></asp:Label><span class="required">*</span></label>
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
                                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Doyouwanttocreateadummyusernameandpassword %>"></asp:Label></label>
                                            </div>

                                            <div class="form-group">
                                                <label for="chkShowPassword">
                                                    <input type="checkbox" id="chkShowPassword" />
                                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Showpassword %>"></asp:Label></label>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <button type="submit" href="#profile" name="home_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="fa fa-arrow-circle-o-right"></span></button>
                    </div>
                    <div class="tab-pane fade" id="profile">
                        <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                            <div class='<%= Common.SessionInfo.Panel %>'>
                                <div class="panel-heading">
                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,AssignStudenttoClass %>"></asp:Label>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6 col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label><span class="required">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label><span class="required">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label><span class="required">*</span></label>
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
                                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label><span class="required">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label><span class="required">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label><span class="required">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter Roll No." CssClass="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class='<%= Common.SessionInfo.Panel %>'>
                            <div class="panel-heading">
                                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,StudentPersonalInformation %>"></asp:Label>
                            </div>
                            <div class="panel-body">
                                <div class="col-lg-6 col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxRegNo" runat="server" placeholder="Enter Registration No." CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Registration No." ControlToValidate="tbxRegNo">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxNameEng" runat="server" placeholder="Enter Name(English)" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Name(English)" ControlToValidate="tbxNameEng">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxNameBan" runat="server" placeholder="Enter Name(Bangla)" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Name(Bangla)" ControlToValidate="tbxNameBan">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,Gender %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlGender" runat="server" DataTextField="Gender" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,Religion %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlReligion" runat="server" DataTextField="Religion" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,BloodGroup %>"></asp:Label><span class="required">*</span></label>
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
                                                <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,BirthCertificateNo %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate No." CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,DateofBirth %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Date of Birth" ControlToValidate="tbxDateOfBirth">*</asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxDateOfBirth"
                                                    TargetControlID="tbxDateOfBirth">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,PhoneHome %>"></asp:Label><span style="color: green">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxPhnHome" runat="server" placeholder="Enter Phone(Home)" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,MobileHome %>"></asp:Label><span style="color: green">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMobHome" runat="server" placeholder="Enter Mobile(Home)" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Application,Phone %>"></asp:Label><span style="color: green">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone No." CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label><span style="color: red">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile No." CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Mobile No." ControlToValidate="tbxMobile">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <button type="submit" href="#messages" name="profile_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="fa fa-arrow-circle-o-right"></span></button>
                    </div>
                    <div class="tab-pane fade" id="messages">
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
                                                <asp:Label ID="Label33" runat="server" Text="<%$ Resources:Application,FatherEduQualification %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlFatherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label34" runat="server" Text="<%$ Resources:Application,FatherProfession %>"></asp:Label><span class="required">*</span></label>
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
                                                <asp:TextBox ID="tbxFatherPhn" runat="server" placeholder="Enter Father Phone No." CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label36" runat="server" Text="<%$ Resources:Application,FatherNationalID %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxFatherNID" runat="server" placeholder="Enter Father National ID" CssClass="form-control"></asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator13" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Father National ID" ControlToValidate="tbxFatherNID">*</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label37" runat="server" Text="<%$ Resources:Application,FatherYearlyIncome %>"></asp:Label></label>
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
                                                <asp:Label ID="Label40" runat="server" Text="<%$ Resources:Application,MotherEduQualification %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlMotherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label41" runat="server" Text="<%$ Resources:Application,MotherProfession %>"></asp:Label><span class="required">*</span></label>
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
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label43" runat="server" Text="<%$ Resources:Application,MotherNationalID %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMotherNID" runat="server" placeholder="Enter Mother National ID" CssClass="form-control"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Mother National ID" ControlToValidate="tbxMotherNID">*</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label44" runat="server" Text="<%$ Resources:Application,MotherYearlyIncome %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxMotherIncome" runat="server" placeholder="Enter Mother Yearly Income" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <button type="submit" href="#settings" name="messages_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>

                    </div>
                    <div class="tab-pane fade" id="settings">
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
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Post Office" ControlToValidate="tbxPostOffice">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label50" runat="server" Text="<%$ Resources:Application,PostalCode %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxPostalCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator22" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Postal Code" ControlToValidate="tbxPostalCode">*</asp:RequiredFieldValidator>
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
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator28" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Postal Code" ControlToValidate="tbxPermanentPostCode">*</asp:RequiredFieldValidator>
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

                        <button type="submit" href="#doner" name="settings_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>
                    </div>
                    <div class="tab-pane fade" id="doner">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 text-center">
                                <div class='<%= Common.SessionInfo.Panel %>'>
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            <asp:Label ID="Label63" runat="server" Text="<%$ Resources:Application,StudentPhoto %>"></asp:Label></h3>
                                    </div>
                                    <div class="panel-body center-block">
                                        <asp:Image ID="imgStudent" runat="server" CssClass="img-thumbnail" Height="140" Width="140" /><br />
                                        <asp:FileUpload ID="uploderStudent" runat="server" CssClass="btn btn-default" /><br />
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
