<%@ Page Title="Profile" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Pages_User_Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Multipart.css" rel="stylesheet" />
    <link href="../../Styles/Table.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="pdf" runat="server">
        <div class="row">
            <asp:Panel ID="logoPanal" runat="server" Visible="false">
                <div class="row">
                    <div class="col-sm-4">
                        <img src="../../Images/Common/School_logo.png" style="height: 92px; float: right" />
                    </div>
                    <div style="padding-top: 20px" class="col-sm-8">

                        <asp:Label ID="lblSchool" Font-Size="25px" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                        <asp:Label ID="lblcode" Font-Size="18px" runat="server" Font-Bold="True" Style="padding-left: 100px"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <div style="width: 100%; text-align: center">
                <asp:Label ID="lblinfo" runat="server" Font-Size="Medium" Font-Bold="true" Text=""></asp:Label>
            </div>
            <div class="col-sm-3">
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Photo</h3>
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
                                    <h4 class="modal-title">Photo Upload</h4>
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
            </div>
            <div class="col-sm-9">
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Personal Information:</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">
                                        <asp:Label ID="lblReg" runat="server" Text="Enter Student ID"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnPersonId" runat="server" />
                                    </div>
                                </div>
                                <div id="empId" runat="server">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-6">
                                            <asp:Label ID="lblEmployee" runat="server"></asp:Label>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblEmpId" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Name(English)</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblNameEng" runat="server"></asp:Label>
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
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">Campus Name</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblCampus" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div id="divJoinDate" runat="server" visible="False">
                                    <div class="form-group">
                                        <label class="col-sm-6">Join Date</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblJoinDate"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-6">Grade Level</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblGrade"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-6">Designation </label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblDesignation"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-6">Level</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblCategory"></asp:Label>
                                        </div>
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
                                    <label for="inputEmail3" class="col-sm-6">SMS Receiver Number</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div id="divEmail" runat="server" visible="False">
                                    <div class="form-group">
                                        <label class="col-sm-6">E-mail</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblEmail"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-6">Nationality</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblNationality"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-6">National ID</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblNationalId"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-6">Bank Account</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblBankAccount"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-6">Status</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                        </div>
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
                                    <h4 class="modal-title">Personal Information Edit:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">
                                                        <asp:Label ID="lblPIN" runat="server" Text="Enter Student ID"></asp:Label><span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxRegNo" runat="server" placeholder="Enter pin" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div id="divEmpId" runat="server" visible="False">
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="lblEmp" runat="server"></asp:Label><span class="required">*</span></label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxEmplId" runat="server" placeholder="Enter Employee ID" CssClass="form-control"></asp:TextBox>
                                                        </div>
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
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Campus Name<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-control dropdown" DataTextField="CampusName" DataValueField="Id">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div id="divJoin" runat="server" visible="False">
                                                    <div class="form-group">
                                                        <label class="col-sm-4">Join Date<span class="required">*</span></label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="tbxJoinDate" placeholder="Enter Join Date" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">Grade Level<span class="required">*</span></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control dropdown" DataTextField="Type" DataValueField="Id">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">
                                                            <asp:Label ID="lbl20" runat="server" Text="Level"></asp:Label><span class="required">*</span>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control-dropdown" DataTextField="LevelName" DataValueField="Id">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4">Designation<span class="required">*</span></label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Designation" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                        </div>
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
                                                        <asp:TextBox ID="tbxDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Phone(Home)</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPhnHome" runat="server" placeholder="Enter Phone(Home)" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Mobile(Home)</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMobHome" runat="server" placeholder="Enter Mobile(Home)" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">Phone</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone No." CssClass="form-control" MaxLength="13"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-5">SMS Receiver Number<span class="required">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile No." CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div id="divEmailAddress" runat="server" visible="False">
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-5">Email</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Your Email" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-5">Nationality</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxNationality" runat="server" placeholder="Enter Nationality" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-5">National ID</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxNationalId" runat="server" placeholder="Enter National ID" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-5">Bank Account</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxBankAccount" runat="server" placeholder="Enter Bank Account" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-5">Status</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control dropdown" DataTextField="Status" DataValueField="Id">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnPersonal" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnPersonal_Click" Enabled="false" />
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
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Father Information:</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-3">
                            <asp:Image runat="server" ID="imgFather" alt="Photo" CssClass="img-responsive img-thumbnail" />
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#FatherPhoto"><i class="fa fa-camera"></i>Change</button>
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
                        <div class="col-sm-9">
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
                                    <h4 class="modal-title">Father Information Edit:</h4>
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
                                                    <label for="inputEmail3" class="col-sm-4">Father Edu. Qualification</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlFatherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father Profession</label>
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
                                                        <%--   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                            Enabled="True" TargetControlID="tbxFatherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father National ID</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxFatherNID" runat="server" placeholder="Enter Father National ID" CssClass="form-control"></asp:TextBox>
                                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                            Enabled="True" TargetControlID="tbxFatherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Father Yearly Income</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxFatherIncome" runat="server" placeholder="Enter Father Yearly Income" CssClass="form-control"></asp:TextBox>
                                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            Enabled="True" TargetControlID="tbxFatherIncome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnFatherInfo" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnFatherInfo_Click" Enabled="false" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Mother Information:</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-3">
                            <asp:Image runat="server" ID="imgMother" alt="Photo" CssClass="img-responsive img-thumbnail" />
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#MotherPhoto"><i class="fa fa-camera"></i>Change</button>
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
                        <div class="col-sm-9">
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
                                                    <label for="inputEmail3" class="col-sm-4">Mother Edu. Qualification</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlMotherEdu" runat="server" DataTextField="Qualification" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother Profession</label>
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
                                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                            Enabled="True" TargetControlID="tbxMotherPhn" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother National ID</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMotherNID" runat="server" placeholder="Enter Mother National ID" CssClass="form-control"></asp:TextBox>
                                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                            Enabled="True" TargetControlID="tbxMotherNID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Mother Yearly Income</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxMotherIncome" runat="server" placeholder="Enter Mother Yearly Income" CssClass="form-control"></asp:TextBox>
                                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                            Enabled="True" TargetControlID="tbxMotherIncome" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnMotherInfo" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnMotherInfo_Click" Enabled="false" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="localDiv" class="col-sm-12" runat="server">
            <div>
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
                                                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="tbxLocG1MobileNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
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
                                                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender555" runat="server"
                                                        Enabled="True" TargetControlID="tbxLocG2MobileNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
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
        <div class="row">
            <div class="col-sm-6">
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Present Address:</h3>
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
                                    <h4 class="modal-title">Present Address Edit:</h4>
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
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4">Postal Code</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="tbxPostalCode" runat="server" ClientIDMode="Static" placeholder="Enter Postal Code" CssClass="form-control"></asp:TextBox>
                                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                            Enabled="True" TargetControlID="tbxPostalCode" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
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
                                    <asp:Button ID="btnPresentAddress" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnPresentAddress_Click" Enabled="false" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Permanent Address:</h3>
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
                                    <h4 class="modal-title">Permanent Address Edit:</h4>
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
                                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                            Enabled="True" TargetControlID="tbxPermanentPostCode" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
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
                                    <asp:Button ID="btnPermanentAddress" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btnPermanentAddress_Click" Enabled="false" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlForTeacher" runat="server">
            <div class="row">
                <div class="col-sm-12">
                    <div>
                        <div class="panel-heading">
                            <h3 class="panel-title">Education:</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12">
                                <div class="form-horizontal">
                                    <asp:GridView ID="gvEducation" CssClass="table" runat="server" AutoGenerateColumns="false" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="OnPagingEducation" OnRowEditing="EditEducation" OnRowUpdating="UpdateEducation" OnRowCancelingEdit="CancelEditEducation">
                                        <Columns>

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <%# Eval("Id") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblIdEdu" runat="server" Text='<%# Eval("Id")%>'> </asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Application,Degree %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("DegreeName") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxDegreeName" runat="server" Text='<%# Eval("DegreeName")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,BordUniversity %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("Board") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxBoard" runat="server" Text='<%# Eval("Board")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,Subject %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("Subject") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxSubject" runat="server" Text='<%# Eval("Subject")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,Year %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("PassingYear") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxPassingYear" runat="server" Text='<%# Eval("PassingYear")%>' Width="100%"></asp:TextBox>
                                                    <%--  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server"
                                                        Enabled="True" TargetControlID="tbxPassingYear" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,Grade %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("Grade") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxGrade" runat="server" Text='<%# Eval("Grade")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,GPAScale %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("GPAScale")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxGPAScale" runat="server" Text='<%# Eval("GPAScale")%>' Width="100%"></asp:TextBox>
                                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server"
                                                        Enabled="True" TargetControlID="tbxGPAScale" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,Division %>" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                                <ItemTemplate>
                                                    <%# Eval("ResultDivision")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxResultDivision" runat="server" Text='<%# Eval("ResultDivision")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Action" HeaderText="Action" />
                                        </Columns>
                                    </asp:GridView>

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
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Add" />
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label59" runat="server" Text="<%$ Resources:Application,DegreeName %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:HiddenField ID="hdnEducation" runat="server" />
                                                                <asp:DropDownList ID="ddlExm" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
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
                                                                <asp:TextBox runat="server" ID="tbxSub" CssClass="form-control" placeholder="Enter Subject Name"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label61" runat="server" Text="<%$ Resources:Application,BordUniversity %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox runat="server" ID="tbxBrd" CssClass="form-control" placeholder="Board or University Name"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label62" runat="server" Text="<%$ Resources:Application,PassingYear %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox runat="server" ID="tbxYr" CssClass="form-control" placeholder="Enter Passing Year"></asp:TextBox>
                                                                <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                                    Enabled="True" TargetControlID="tbxYr" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label63" runat="server" Text="<%$ Resources:Application,Result %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlRslt" ClientIDMode="Static" runat="server" CssClass="form-control dropdown">
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
                                                                <asp:TextBox runat="server" ID="tbxGP" CssClass="form-control" placeholder="Grade Point"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox runat="server" ID="tbxScl" CssClass="form-control" placeholder="Out of"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-footer" align="right">
                            <%-- <button type="button" class="btn btn-default" data-toggle="modal" data-target="#Education"><i class="fa fa-edit"></i>Edit</button>--%>
                        </div>
                        <div class="modal fade" id="Education" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header bg-danger">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Teacher Education Information</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-horizontal">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Add" />
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">Degree Name</label>
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
                                                <label for="inputEmail3" class="col-sm-4">Subject/Group</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="tbxSubject" CssClass="form-control" placeholder="Enter Subject Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">Bord/University</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="tbxBoard" CssClass="form-control"
                                                        placeholder="Board or University Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">Passing Year</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="tbxYear" CssClass="form-control" placeholder="Enter Passing Year"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">Result</label>
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
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- Edit Training--%>
            <div class="row">
                <div class="col-sm-12">
                    <div>
                        <div class="panel-heading">
                            <h3 class="panel-title">Training</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12">
                                <div class="form-horizontal">
                                    <asp:GridView ID="gvTraining" ClientIDMode="Static" CssClass="table" runat="server" AutoGenerateColumns="false" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="OnPagingTraining" OnRowEditing="EditTraining" OnRowUpdating="UpdateTraining" OnRowCancelingEdit="CancelEditTraining">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <%# Eval("Id") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id")%>'> </asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Application,TrainingName %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("TrainingName") %>
                                                    <input type="hidden" runat="server" id="hdnTrainingTd" value='<%#Eval("Id") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxTrainingName" runat="server" Text='<%# Eval("TrainingName")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,InstituteName %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("InstituteName") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxInstituteName" runat="server" Text='<%# Eval("InstituteName")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,StartDate %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("StartDate")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxStartDate" runat="server" Text='<%# Eval("StartDate")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Application,EndDate %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("EndDate")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxEndDate" runat="server" Text='<%# Eval("EndDate")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,Topics %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("Topics") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxTopics" runat="server" Text='<%# Eval("Topics")%>' Width="100%"></asp:TextBox>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="tbxTopics" ID="RegularExpressiontbxTopicsValidator"
                                                        ValidationExpression="^[\s\S]{0,160}$" runat="server" ErrorMessage="Maximum 160 characters allowed."></asp:RegularExpressionValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,Duration %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                                <ItemTemplate>
                                                    <%# Eval("Duration")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbxDuration" runat="server" Text='<%# Eval("Duration")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" HeaderText="Action" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="panel panel-footer" align="right">
                                    <%--<button type="button" id="btnEditEdu" class="btn btn-default" data-toggle="modal" data-target="#Training" ><i class="fa fa-edit"></i>Edit</button>--%>
                                </div>

                                <div class="modal fade" id="Training" role="dialog">
                                    <div class="modal-dialog">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header bg-danger">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Teacher Training Information</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">Training Name</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxTrainingName" runat="server" CssClass="form-control" placeholder="Enter Training Name"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnTrainnig" runat="server" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">Institute</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxInstitute" runat="server" CssClass="form-control" placeholder="Enter Institute Name"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">Start Date</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxStartDate" runat="server" CssClass="form-control" placeholder="Enter Start Date" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">End Date</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxEndDate" runat="server" CssClass="form-control" placeholder="Enter End Date" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">Topics Cover</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxTopics" runat="server" CssClass="form-control" placeholder="Enter Topics" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-4">Duration</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="tbxDuration" runat="server" CssClass="form-control" placeholder="Enter Topics"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button runat="server" CssClass="btn btn-default" Text="Update" ID="btnSaveTraining"></asp:Button>
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
        </asp:Panel>
        <div class="row">
            <div class="col-sm-12">
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Other Information:</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5">
                                        <asp:Label ID="lblFreedom" runat="server" Text="<%$ Resources:Application,SonDaughterGrandchildofFreedomFighter %>"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkFreedom" runat="server" Enabled="false" Checked="false" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5">
                                        <asp:Label ID="lblTribal" runat="server" Text="<%$ Resources:Application,Tribal %>"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkTribal" runat="server" Enabled="false" Checked="false" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-5">
                                        <asp:Label ID="lblPhyDef" runat="server" Text="<%$ Resources:Application,PhysicallyDefect %>"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkPhyDef" runat="server" Enabled="false" Checked="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-footer" align="right">
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#OtherInfo"><i class="fa fa-edit"></i>Edit</button>
                    </div>
                    <div class="modal fade" tabindex="-1" id="OtherInfo" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Other Information Edit:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-8">
                                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,SonDaughterGrandchildofFreedomFighter %>" Width="100%"></asp:Label></label>
                                                    <div class="col-sm-4">
                                                        <asp:CheckBox ID="chkmFreedomFighter" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-8">
                                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Tribal %>"></asp:Label></label>
                                                    <div class="col-sm-4">
                                                        <asp:CheckBox ID="chkmTribal" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-8">
                                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,PhysicallyDefect %>"></asp:Label></label>
                                                    <div class="col-sm-4">
                                                        <asp:CheckBox ID="chkmPhyDef" runat="server" />
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
                <asp:Button ID="btnGenerateReport" runat="server" CssClass="btn btn-success" Text="Generate PDF" OnClick="btnGenerateReport_Click" />
            </div>
        </div>
    </div>
    <script src="../../jquery/jquery-2.1.1.min.js"></script>
    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script src="../../JS/LoadAddress.js"></script>
    <script src="../../Scripts/jquery.easing.min.js"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/languages/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/jquery.validationEngine.js"
        charset="utf-8"></script>
    <script src="../../JS/Teacher.js"></script>
    <script type="text/javascript">
        $(".timepicker").timepicker({
            showInputs: false
        });
        $(document).ready(function () {
            $("#tbxDateOfBirth").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#tbxStartDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#tbxEndDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#tbxJoinDate").datepicker({ dateFormat: 'dd/mm/yy' });

            $("#tbxDateOfBirth").on('change', function () {

                var currentDate = new Date();
                var text = $('#tbxDateOfBirth').val();
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

            $("#tbxStartDate").on('change', function () {

                var currentDate = new Date();
                var text = $('#tbxStartDate').val();
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

            $("#tbxEndDate").on('change', function () {

                var currentDate = new Date();
                var text = $('#tbxEndDate').val();
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

        });
    </script>
</asp:Content>


