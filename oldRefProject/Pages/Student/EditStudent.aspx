<%@ Page Title="<%$ Resources:Application,EditStudent %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EditStudent.aspx.cs" Inherits="Pages_Student_EditStudent" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/MultipartFormControl.css" rel="stylesheet" />
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
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="bs_Medium" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="label77" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" AutoPostBack="true" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" AutoPostBack="true" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                  
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" AutoPostBack="true" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RollNo %>"></asp:Label>
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlRollNo" runat="server" DataTextField="RollNo" DataValueField="PersonId" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlRollNo_SelectedIndexChanged"></asp:DropDownList>
                            <asp:DropDownList ID="ddlRegNo" runat="server" DataTextField="RegNo" DataValueField="PersonId" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlRegNo_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="studentInfoDiv" runat="server" visible="false">
        <div class="row">
                <div class="col-sm-12">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            <h3 class="panel-title">Academic Information</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Academic Year</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblYear" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnStudentId" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Medium</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblMedium" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Campus</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblCampus" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Class</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblClass" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Studentship</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Group</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblGroup" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Shift</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblShift" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Section</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblSection" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">Student ID</label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblRoll" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-footer text-right">
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#AcademicInfo"><i class="fa fa-edit"></i>Edit</button>
                        </div>
                        <div class="modal fade" tabindex="-1" id="AcademicInfo" role="dialog">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header bg-danger">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Academic Information Edit</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-horizontal">
                                                    <%--<div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPromotionYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                        </div>
                                                    </%--div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPromotionMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPromotionCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPromotionClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>--%>
                                                     <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label7" runat="server" Text="Studentship"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Status" DataValueField="Id" CssClass="form-control dropdown">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6">
                                                <div class="form-horizontal">
                                                    <%--<div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPromotionGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPromotionShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPromotionSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,NewRollNo%>"></asp:Label></label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter New Stu. ID" MaxLength="9" CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                                                Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnAcademicUpdate" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnAcademicUpdate_Click" />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
        

        <div class="row">
            <div class="col-sm-12">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Personal Information</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">
                                        <asp:Label ID="lblReg" runat="server" Text="Student ID"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Name(English)</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblNameEng" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnPersonId" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Name(Bangla)</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblNameBan" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Gender</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Religion</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblReligion" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Blood Group</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblBlood" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Birth Certificate No</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblBirthCertificate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Date of Birth</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblDoB" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Phone(Home)</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPhnHome" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Mobile(Home)</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMobHome" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Phone</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPhn" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Mobile</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-footer text-right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#PersonalInfo"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" tabindex="-1" id="PersonalInfo" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Personal Information Edit</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-horizontal">

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Student ID<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxRegNo" runat="server" ReadOnly="true" placeholder="Enter Registration No." CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Name(English)<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxNameEng" runat="server" placeholder="Enter Name(English)" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Name(Bangla)</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxNameBan" runat="server" placeholder="Enter Name(Bangla)" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Gender<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlGender" runat="server" DataTextField="Gender" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Religion<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlReligion" runat="server" DataTextField="Religion" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Blood Group<span class="required">*</span></label>
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
                                                    <label for="inputEmail3" class="col-sm-5">Birth Certificate No.</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate No." CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Date of Birth<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxDateOfBirth"
                                                            TargetControlID="tbxDateOfBirth"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Phone(Home)</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPhnHome" runat="server" placeholder="Enter Phone(Home)" CssClass="form-control"></asp:TextBox>
                                                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbxPhnHome" ErrorMessage="Value must be a whole number" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Mobile(Home)</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMobHome" runat="server" placeholder="Enter Mobile(Home)" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="rexNumber" ControlToValidate="tbxMobHome" ValidationExpression="[0-9]{11}" ErrorMessage="Please enter 11 digit number!" />

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Phone</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone No." CssClass="form-control"></asp:TextBox>
                                                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbxPhone" ErrorMessage="Value must be a whole number" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Mobile<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile No." CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ControlToValidate="tbxMobile" ValidationExpression="[0-9]{11}" ErrorMessage="Please enter 11 digit number!" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnPersonal" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnPersonal_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Father Information</h3>
                    </div>
                    <div class="panel-body">

                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Father Name</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Qualification</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblFatherEdu" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Profession</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblFatherPro" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">National Id</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblFatherNId" runat="server"></asp:Label>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Phone No.</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblFatherPhn" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Yearly Income</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblFatherIncome" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="panel panel-footer" align="right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#FatherInfo"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" id="FatherInfo" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Father Information Edit</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father Name<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxFathername" runat="server" placeholder="Enter Father Name" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                                            ErrorMessage="Enter Father Name" ControlToValidate="tbxFathername">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father Edu. Qualification<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlFatherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father Profession<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlFatherPro" runat="server" DataTextField="Profession" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father Phone No.</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxFatherPhn" runat="server" placeholder="Enter Father Phone No." CssClass="form-control"></asp:TextBox>
                                                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbxFatherPhn" ErrorMessage="Value must be a whole number" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father National ID</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxFatherNID" runat="server" placeholder="Enter Father National ID" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator13" runat="server" ValidationGroup="save"
                                                            ErrorMessage="Enter Father National ID" ControlToValidate="tbxFatherNID">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father Yearly Income</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxFatherIncome" runat="server" placeholder="Enter Father Yearly Income" CssClass="form-control"></asp:TextBox>
                                                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbxFatherIncome" ErrorMessage="Value must be a whole number" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnFatherInfo" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnFatherInfo_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Mother Information</h3>
                    </div>
                    <div class="panel-body">

                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Mother Name</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMotherName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Qualification</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMotherEdu" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Profession</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMotherPro" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">National Id</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMotherNId" runat="server"></asp:Label>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Phone No.</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMotherPhn" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Yearly Income</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMotherIncome" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-footer" align="right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#MotherInfo"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" id="MotherInfo" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Mother Information Edit</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother Name<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMotherName" runat="server" placeholder="Enter Mother Name" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                                                            ErrorMessage="Enter Mother Name" ControlToValidate="tbxMotherName">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother Edu. Qualification<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlMotherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother Profession<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlMotherPro" runat="server" DataTextField="Profession" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother Phone No.</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMotherPhn" runat="server" placeholder="Enter Mother Phone No." CssClass="form-control"></asp:TextBox>
                                                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbxMotherPhn" ErrorMessage="Value must be a whole number" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother National ID</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMotherNID" runat="server" placeholder="Enter Mother National ID" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                                            ErrorMessage="Enter Mother National ID" ControlToValidate="tbxMotherNID">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother Yearly Income</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMotherIncome" runat="server" placeholder="Enter Mother Yearly Income" CssClass="form-control"></asp:TextBox>
                                                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbxMotherIncome" ErrorMessage="Value must be a whole number" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnMotherInfo" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnMotherInfo_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Present Address</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Division</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPresentDiv" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">District</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPresentDis" ClientIDMode="Static" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Thana</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPresentThana" ClientIDMode="Static" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Post Office</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPresentPO" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Postal Code</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPresentPC" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Address</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPresentAddress" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-footer text-right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#PresentAdd"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" id="PresentAdd" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Present Address</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Division</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPresentDiv" ClientIDMode="Static" runat="server" DataTextField="Division" DataValueField="Id"
                                                            CssClass="form-control dropdown">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">District</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPresentDis" ClientIDMode="Static" runat="server" DataTextField="District" DataValueField="Id"
                                                            CssClass="form-control dropdown">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Thana</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPresentThana" ClientIDMode="Static" runat="server" DataTextField="Thana" DataValueField="Id"
                                                            CssClass="form-control dropdown">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Post Office</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPostOffice" runat="server" ClientIDMode="Static" placeholder="Enter Post Office" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                                            ErrorMessage="Enter Post Office" ControlToValidate="tbxPostOffice">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Postal Code</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPostalCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator22" runat="server" ValidationGroup="save"
                                                            ErrorMessage="Enter Postal Code" ControlToValidate="tbxPostalCode">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Address</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPresentAddress" runat="server" ClientIDMode="Static" placeholder="Enter Address" CssClass="form-control"
                                                            TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnPresentAddress" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnPresentAddress_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Permanent Address</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Division</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPermanentDiv" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">District</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPermanentDis" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Thana</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPermanentThana" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Post Office</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPermanentPO" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Postal Code</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPermanentPC" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Address</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPermanentAddress" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-footer" align="right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#PermanentAdd"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" id="PermanentAdd" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Permanent Address</h4>
                                </div>
                                <div class="modal-body" align="center">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Division</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPermanentDiv" ClientIDMode="Static" runat="server" DataTextField="Division" DataValueField="Id"
                                                            CssClass="form-control dropdown">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">District</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPermanentDis" ClientIDMode="Static" runat="server" DataTextField="District"
                                                            DataValueField="Id" CssClass="form-control dropdown">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Thana</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPermanentThana" ClientIDMode="Static" runat="server" DataTextField="Thana" DataValueField="Id"
                                                            CssClass="form-control dropdown">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Post Office</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPermanentPost" runat="server" ClientIDMode="Static" placeholder="Enter Post Office" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Postal Code</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPermanentPostCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator28" runat="server" ValidationGroup="save"
                                                            ErrorMessage="Enter Postal Code" ControlToValidate="tbxPermanentPostCode">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Address</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPermanentAddress" runat="server" ClientIDMode="Static" placeholder="Enter Address" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnPermanentAddress" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnPermanentAddress_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Local Guardian Information:</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Local Guardian -1 Name</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblLocalGuardian1" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Local Guardian -1 Mobile No.</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblLocalG1MobileNo" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Local Guardian -2 Name</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblLocalGuardian2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Local Guardian -2 Mobile No.</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblLocalG2MobileNo" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-footer" align="right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#LocalGuardianInfo"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" id="LocalGuardianInfo" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Local Guardian Information Edit:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Local Guardian -1 Name</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxLocalGuardian1" runat="server" placeholder="Enter L. Guardian-1 Name" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ControlToValidate="tbxLocalGuardian1" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Local Guardian -1 Mobile No.</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxLocG1MobileNo" runat="server" placeholder="Enter L. Guardian-1 Mobile" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="tbxLocG1MobileNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Local Guardian -2 Name</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxLocalGuardian2" runat="server" placeholder="Enter L. Guardian-2 Name" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ControlToValidate="tbxLocalGuardian2" ID="RegularExpressionValidator6" ValidationExpression="^[\s\S]{5,50}$" runat="server" ErrorMessage="Min 5 Max 50 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Local Guardian -2 Mobile No.</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxLocG2MobileNo" runat="server" placeholder="Enter L. Guardian-2 Mobile" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender555" runat="server"
                                                        Enabled="True" TargetControlID="tbxLocG2MobileNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnLocalGuardianInfo" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnLocalGuardianInfo_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Other Information :</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Son/Daughter/Grandchild of Freedom Fighter</label>
                                    <div class="col-sm-6">
                                        <asp:CheckBox ID="chkFreedom" runat="server" Enabled="false" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Tribal</label>
                                    <div class="col-sm-6">
                                        <asp:CheckBox ID="chkTribal" runat="server" Enabled="false" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Physically Defect</label>
                                    <div class="col-sm-6">
                                        <asp:CheckBox ID="chkPhyDef" runat="server" Enabled="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-footer" align="right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#OtherInfo"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" id="OtherInfo" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Other Information Edit :</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-6">Son/Daughter/Grandchild of Freedom Fighter</label>
                                                    <div class="col-sm-6">
                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-6">Tribal</label>
                                                    <div class="col-sm-6">
                                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-6">Physically Defect</label>
                                                    <div class="col-sm-6">
                                                        <asp:CheckBox ID="CheckBox3" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnOtherInfo" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnOtherInfo_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <h3 class="panel-title">Photo</h3>
                    </div>
                    <div class="col-sm-4">

                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label63" runat="server" Text="<%$ Resources:Application,StudentPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body text-center">
                            <asp:Image runat="server" ID="imgPerson" alt="Photo" Height="140" Width="140" />
                        </div>
                        <div class="panel panel-footer text-center">
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#Photo"><i class="fa fa-camera"></i>Change</button>
                        </div>
                        <div class="modal fade" id="Photo" role="dialog">
                            <div class="modal-dialog modal-sm">
                                <div class="modal-content">
                                    <div class="modal-header bg-danger">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Student Photo Upload</h4>
                                    </div>
                                    <div class="modal-body" align="center">
                                        <asp:FileUpload ID="uploderStudent" runat="server" CssClass="btn btn-default" />
                                        <asp:HiddenField ID="hdnPersonImage" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnPersoImage" runat="server" CssClass="btn btn-default" Text="Upload" OnClick="btnPersoImage_Click" />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label64" runat="server" Text="<%$ Resources:Application,FatherPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body text-center">
                            <asp:Image runat="server" ID="imgFather" alt="Photo" Height="140" Width="140" />
                        </div>
                        <div class="panel panel-footer text-center">
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#FatherPhoto"><i class="fa fa-camera"></i>Change</button>
                        </div>
                        <div class="modal fade" id="FatherPhoto" role="dialog">
                            <div class="modal-dialog modal-md">
                                <div class="modal-content">
                                    <div class="modal-header bg-danger">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Father Photo Upload</h4>
                                    </div>
                                    <div class="modal-body text-center">
                                        <asp:FileUpload ID="upFather" runat="server" CssClass="btn btn-default" />
                                        <asp:HiddenField ID="hdnFatherImage" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnFatherImage" runat="server" CssClass="btn btn-default" Text="Upload" OnClick="btnFatherImage_Click" />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,MotherPhoto %>"></asp:Label></h3>
                        </div>
                        <div class="panel-body text-center">
                            <asp:Image runat="server" ID="imgMother" alt="Photo" Height="140" Width="140" />
                        </div>
                        <div class="panel panel-footer text-center">
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#MotherPhoto"><i class="fa fa-camera"></i>Change</button>
                        </div>
                        <div class="modal fade" id="MotherPhoto" role="dialog">
                            <div class="modal-dialog modal-sm">
                                <div class="modal-content">
                                    <div class="modal-header bg-danger">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Mother Photo Upload</h4>
                                    </div>
                                    <div class="modal-body" align="center">
                                        <asp:FileUpload ID="upMother" runat="server" CssClass="btn btn-warning" />
                                        <asp:HiddenField ID="hdnMotherImage" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnMother" runat="server" CssClass="btn btn-default" Text="Upload" OnClick="btnMother_Click" />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../../jquery/jquery-2.1.1.min.js"></script>
    <script src="../../JS/LoadAddressStudent.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

