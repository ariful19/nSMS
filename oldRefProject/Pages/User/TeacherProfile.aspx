<%@ Page Title="Employee Profile" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="TeacherProfile.aspx.cs" Inherits="Pages_User_TeacherProfile" %>

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
                <asp:Label ID="lblinfo" runat="server" Font-Size="20px" Font-Bold="true" Text=""></asp:Label>
            </div>
            <div class="col-sm-3">
                <div>
                    <div class="panel-heading">
                        <h3 class="panel-title">Photo</h3>
                    </div>
                    <div class="panel-body text-center">
                        <asp:Image runat="server" ID="imgPerson" alt="Photo" Height="140" Width="140" />
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-9">
                <div>
                    <div class="panel-heading">
                        <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Personal Information:</asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-6">
                                        <asp:Label ID="lblReg" runat="server" Text="Registration No"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnPersonId" runat="server" />
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
                                    <label class="col-sm-6">Join Date</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblJoinDate"></asp:Label>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-sm-6">Designation</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblDesignation"></asp:Label>
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label class="col-sm-6">Nationality</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblNationality"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                 <div class="form-group">
                                    <label class="col-sm-6">National ID</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblNationalId"></asp:Label>
                                    </div>
                                </div>
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
                                <div class="form-group">
                                    <label class="col-sm-6">E-mail</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblEmail"></asp:Label>
                                    </div>
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
                        <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Father Information:</asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-3">
                            <asp:Image runat="server" ID="imgFather" alt="Photo" CssClass="img-responsive img-thumbnail" />                         
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
                </div>
            </div>
            <div class="col-sm-6">
                <div>
                    <div class="panel-heading">
                        <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Mother Information:</asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-3">
                            <asp:Image runat="server" ID="imgMother" alt="Photo" CssClass="img-responsive img-thumbnail" /> 
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
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div>
                    <div class="panel-heading">
                        <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Present Address:</asp:Label>
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
                </div>
            </div>
            <div class="col-sm-6">
                <div>
                    <div class="panel-heading">
                        <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Permanent Address:</asp:Label>
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
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlForTeacher" runat="server">
            <div class="row">
                <div class="col-sm-12">
                    <div>
                        <div class="panel-heading">
                            <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Education:</asp:Label>
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
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Application,BordUniversity %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
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
                    </div>
                </div>
            </div>
            <%-- Edit Training--%>
            <div class="row">
                <div class="col-sm-12">
                    <div>
                        <div class="panel-heading">
                            <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Training:</asp:Label>
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
                                        </Columns>
                                    </asp:GridView>
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
                        <asp:Label runat="server" Font-Size="18px" Font-Bold="true">Other Information:</asp:Label>
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

