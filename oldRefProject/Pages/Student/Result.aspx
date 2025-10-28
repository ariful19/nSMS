<%@ Page Title="<%$ Resources:Application,ResultView %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Pages_Student_Result" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                    <div id="yearDiv" runat="server">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                          <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
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
                    </div>
                   <%-- <div id="currentYearDiv" runat="server">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="yearTextBox" runat="server" CssClass="form-control dropdown" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
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
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,ExamType %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlExamType" runat="server" DataTextField="ExamType" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label>
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                        </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxRollNo" runat="server" placeholder="Enter Roll No." CssClass="form-control dropdown" MaxLength="9"></asp:TextBox>
                            <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" CssClass="form-control" MaxLength="12"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ControlToValidate="tbxRollNo">Enter Roll No</asp:RequiredFieldValidator>--%>
                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRollNo" runat="server"
                                Enabled="True" TargetControlID="tbxRollNo" FilterType="Custom" ValidChars="0123456789.-"></cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnReport" CssClass="btn btn-flat btn-success" OnClick="btnReport_Click" runat="server" Text="Show Result" ValidationGroup="save" />
        </div>
    </div>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">

        <ContentTemplate>
            <div style="text-align: center">
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <asp:Panel ID="pnlResult" runat="server">

                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label9" runat="server" Text="Name"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label10" runat="server" Text="Father's Name"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblFather" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label11" runat="server" Text="Mother's Name"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblMother" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label12" runat="server" Text="Exam Type"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblExamType" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label14" runat="server" Text="Reg. No"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label23" runat="server" Text="GPA"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblGPA" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label13" runat="server" Text="Class"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblClass" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label15" runat="server" Text="Group"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblGroup" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label19" runat="server" Text="Shift"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblShift" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label21" runat="server" Text="Section"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblSection" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="LabelRollNo" runat="server" Text="Roll No"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblRoll" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="lblFailSubject" runat="server" Text="Fail Subject" Visible="false"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblFail" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Repeater ID="rptResult" runat="server">
                                    <HeaderTemplate>
                                        <table id="example11" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label5" runat="server" Text="Subject Name"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label1" runat="server" Text="Grade Letter"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label3" runat="server" Text="Grade Point"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("SubjectName") %></td>
                                            <td><%#Eval("GradeLetter") %></td>
                                            <td><%#Eval("GradePoint") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

