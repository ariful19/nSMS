<%@ Page Title="<%$ Resources:Application,IssueAndReturnBooks %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="IssueAndReturnBooks.aspx.cs" Inherits="Pages_Library_IssueAndReturnBooks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div class="padding-bottom-15">
        <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
            <asp:ListItem Value="1" Text="<%$ Resources:Application,Student %>" Selected="True"></asp:ListItem>
            <asp:ListItem Value="2" Text="<%$ Resources:Application,Teacher %>"></asp:ListItem>
            <asp:ListItem Value="3" Text="<%$ Resources:Application,Other %>"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
        <div class="form-horizontal">
            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Add" />

            <div class="<%= Common.SessionInfo.Panel %>">
                <div class="panel-heading">
                    <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
                </div>
                <div class="panel-body">

                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
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
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label31" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label32" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RollNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxRollNo" runat="server" CssClass="form-control dropdown" placeholder="Enter Roll No."></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortbxRollNo" runat="server" ValidationGroup="save"
                                        ControlToValidate="tbxRollNo" ErrorMessage="Please Enter Roll No ">Enter Roll No</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredtbxDurationExtendertbxRollNo" runat="server"
                                        Enabled="True" TargetControlID="tbxRollNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="tbxReg" FilterType="Custom" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ClientIDMode="Static" ID="Panel1" runat="server">
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="example5" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblSelect" runat="server" Text="Select" />
                                                </th>
                                                <th>
                                                    <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblName" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                                </th>

                                                <th id="thRoll" runat="server">
                                                    <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RollNo %>"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblMobile" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkrow" runat="server" /></td>
                                        <td>
                                            <asp:Label ID="lblRegNo" Text='<%#Eval("RegNo") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNameEng" Text='<%#Eval("NameEng") %>' runat="server" />
                                            <asp:HiddenField ID="hdnPersonId" Value='<%#Eval("PersonID") %>' runat="server" />
                                        </td>

                                        <td id="tdRoll" runat="server">
                                            <asp:Label ID="lblRollNo" Text='<%#Eval("RollNo") %>' runat="server" />


                                        </td>
                                        <td>
                                            <asp:Label ID="lblMobile" Text='<%#Eval("Mobile") %>' runat="server" />

                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                    <asp:Button ID="btnStudent" CssClass="btn btn-default" runat="server" Text="<%$ Resources:Application,AddStudent %>" OnClick="btnStudent_Click" />

                </div>

            </div>
        </asp:Panel>
        <%--<div class="tab-pane fade" id="messages">--%>
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label57" runat="server" Text="<%$ Resources:Application,Student %>"></asp:Label>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12">
                    <div class="form-horizontal">
                        <%--This is test----------------------------------------------------------%>
                        <asp:GridView ID="gvStudent" CssClass="table" runat="server" AutoGenerateColumns="false" Caption="Student Information" CaptionAlign="Top" OnRowDataBound="GridView_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,RegNo %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("RegNo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Name %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("NameEng") %>
                                        <asp:HiddenField ID="hdnStuPersonId" Value='<%#Eval("PersonId") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Roll %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("RollNo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="<%$ Resources:Application,Mobile %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Mobile") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--This is test----------------------------------------------------------%>
                    </div>
                </div>
            </div>
        </div>
        <%--  </div>--%>
    </asp:Panel>


    <asp:Panel ClientIDMode="Static" ID="pnlTeacher" runat="server">
        <%--<div class="tab-pane fade" id="messages">--%>

        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Teacher %>"></asp:Label>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,TeacherPinNo %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxTecherPin" runat="server" CssClass="form-control dropdown" placeholder="Enter Teacher Pin No."></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                    ControlToValidate="tbxRollNo" ErrorMessage="Please Enter Teacher Pin No">Enter Teacher Pin No</asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    Enabled="True" TargetControlID="tbxTecherPin" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ClientIDMode="Static" ID="Panel2" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,TeacherList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptTeacher" runat="server">
                                    <HeaderTemplate>
                                        <table id="example3" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label18" runat="server" Text="Select"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,TeacherPinNo %>"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label></th>


                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkrow" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTeacherPin" Text='<%#Eval("TeacherPin") %>' runat="server" />

                                            </td>
                                            <td>
                                                <%#Eval("NameEng") %>
                                                <asp:HiddenField ID="hdnPersonID" Value='<%#Eval("PersonID") %>' runat="server" />
                                            </td>
                                            <td>
                                                <%#Eval("Mobile") %>
                                            </td>

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
                    <div class="panel-footer">
                        <asp:Button ID="btnTeacher" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:Application,AddTeacher %>" OnClick="btnTeacher_Click" />

                    </div>
                </div>
            </asp:Panel>
            <div class="col-lg-12 col-md-12">
                <div class="form-horizontal">
                    <%--This is test----------------------------------------------------------%>
                    <asp:GridView ID="gvTeacher" CssClass="table" runat="server" AutoGenerateColumns="false" Caption="Teacher Information" CaptionAlign="Top">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,Name %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <%# Eval("NameEng") %>
                                    <asp:HiddenField ID="hdnTechPersonId" Value='<%#Eval("PersonId") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,TeacherPinNo %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <%# Eval("TeacherPin") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,Address %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <%# Eval("Address") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,Mobile %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <%# Eval("Mobile") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>

        <%--  </div>--%>
    </asp:Panel>


    <asp:Panel ClientIDMode="Static" ID="pnlOther" runat="server">
        <%--<div class="tab-pane fade" id="messages">--%>
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Other %>"></asp:Label>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12">
                    <div class="form-horizontal">
                        <%--This is test----------------------------------------------------------%>
                        <asp:GridView ID="gvOther" CssClass="table" runat="server" AutoGenerateColumns="false" ClientIDMode="Static" Caption="Other Information" CaptionAlign="Top">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Name %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("NameEng") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Roll %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Designation") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Address %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Address") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Mobile %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Mobile") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Action %>" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete11" runat="server" ClientIDMode="Static" OnClick="btndelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--This is test----------------------------------------------------------%>
                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#Other">
                            <i class="fa fa-plus"></i>
                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,Addnew%>"></asp:Label>

                        </button>
                        <!-- Modal -->
                        <div class="modal fade" id="Other" role="dialog">
                            <div class="modal-dialog">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header bg-danger">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">
                                            <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application, Other %>"></asp:Label></h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-horizontal">
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Add" />

                                            <div class="<%= Common.SessionInfo.Panel %>">
                                                <div class="panel-heading">
                                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
                                                </div>
                                                <div class="panel-body">

                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="tbxOtherName" runat="server" DataTextField="Name" DataValueField="Id" CssClass="form-control dropdown"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="tbxOtherDsg" runat="server" DataTextField="Designation" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="false">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="tbxOtherAddress" runat="server" DataTextField="Address" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="false" TextMode="MultiLine">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="tbxOtherMobile" runat="server" CssClass="form-control dropdown" placeholder="Enter Mobile No."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                                                    ControlToValidate="tbxRollNo">Enter Mobile No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button class="btn btn-default" ID="btnOther" runat="server" Text="<%$ Resources:Application,Add %>"></asp:Button>
                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                            <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,Close %>"></asp:Label></button>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </asp:Panel>

    <asp:Panel ClientIDMode="Static" ID="pnlReturnBook" runat="server">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="lblReturn" runat="server" Text="<%$ Resources:Application,ReturnBook %>"></asp:Label>
            </div>
            <div class="panel-body">

                <div class="col-lg-12 col-md-12">
                    <div class="form-horizontal">

                        <asp:GridView ID="gvBookReturn" CssClass="table" runat="server" AutoGenerateColumns="false" ClientIDMode="Static" Caption="Issed Book List" CaptionAlign="Top">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Title %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuedBookId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        <asp:Label ID="lblBookId" runat="server" Text='<%# Eval("BookId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Title %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Author %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Author") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,Publisher %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Publisher") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,IssueDate %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("IssueDate", "{0:dd/MM/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,DueDate %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("ReturnDate", "{0:dd/MM/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Application,SelectBook %>" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkReturnBook" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-6 text-right">
                        <asp:Button ID="btnReturn" runat="server" class="btn btn-success btn-flat" Text="Return Book" OnClick="btnReturn_Click" OnClientClick="if ( ! ReturnConfirmation()) return false;"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ClientIDMode="Static" ID="pnlAssignedBook" runat="server">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label45" runat="server" Text="<%$ Resources:Application,IssueBook %>"></asp:Label>
            </div>
            <div class="panel-body">

                <div class="col-lg-12 col-md-12">
                    <div class="form-horizontal">
                        <div class="row" id="date">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label46" runat="server" Text="From Date"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="tbxDate" SelectedDate='<%# DateTime.Now %>'
                                                TargetControlID="tbxDate" Enabled="false"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label47" runat="server" Text="Due Date"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="tbxDueDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="tbxDueDate" SelectedDate='<%# DateTime.Now %>'
                                                TargetControlID="tbxDueDate" Enabled="false"></cc1:CalendarExtender>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <%--  <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label12" runat="server" Text="Due Date"></asp:Label></label>--%>

                                        <div class="col-sm-8">
                                            <asp:Label ID="Label13" runat="server" CssClass="form-control" BorderStyle="Groove" BorderColor="PeachPuff">No. of Days : <b>07</b></asp:Label>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-12 col-md-12">
                            <div class="form-horizontal">
                                <asp:GridView ID="gvBookIssue" CssClass="table" runat="server" AutoGenerateColumns="false" ClientIDMode="Static" Caption="Selected Book List" CaptionAlign="Top">
                                    <Columns>
                                        <asp:TemplateField HeaderText="<%$ Resources:Application,Title %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBookId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Application,Title %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                            <ItemTemplate>
                                                <%# Eval("Title") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Application,Author %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                            <ItemTemplate>
                                                <%# Eval("Author") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Application,Publisher %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                            <ItemTemplate>
                                                <%# Eval("Publisher") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Application,DueDate %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                            <ItemTemplate>
                                                <%# Eval("DueDate") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:Application,SelectBook %>" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkBook" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <div class="col-sm-6 text-right">
                                    <asp:Button ID="btnIssue" runat="server" class="btn btn-success btn-flat" Text="Issue Book" OnClick="btnIssue_Click" OnClientClick="if ( ! IssueConfirmation()) return false;"></asp:Button>
                                    <%--  <asp:Button ID="btnReturn" runat="server" class="btn btn-success btn-flat" Text="Return Book" OnClick="btnReturn_Click"> </asp:Button>--%>
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-success btn-flat" Text="Reset" OnClick="btnReset_Click"
                                        OnClientClick="if ( ! UserResetConfirmation()) return false;"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel ClientIDMode="Static" ID="pnlAllBookList" runat="server">
        <%--<ContentTemplate>--%>
        <div class="col-sm-9">
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="lblCriteria" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12">
                        <div class="form-horizontal">
                            <asp:Repeater ID="rptBook" runat="server" OnItemCommand="RepeaterGetSelectedID">
                                <HeaderTemplate>
                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label1" runat="server" Text="Image"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="Details"></asp:Label></th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Image ID="imgBook" runat="server" Width="100" ImageUrl='<%# Eval("CoverPhoto").ToString()!=""?string.Concat("~/Images/Book/",Eval("CoverPhoto")):"../../Images/Common/no-photo.jpg" %>' class="img-responsive img-thumbnail" />
                                        </td>
                                        <td>
                                            <h4>
                                                <asp:Label ID="lblTitle" runat="server" Text=' <%#Eval("Title") %> '></asp:Label></h4>
                                            <h6>ISBN : <b><%#Eval("ISBN") %> </b></h6>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label5" runat="server" Text="Author"></asp:Label></label>
                                                            <div class="col-sm-8">
                                                                <asp:HiddenField ID="HiddenBookID" runat="server" Value='<%# Eval("Id") %>' />
                                                                :
                                                                <asp:Label ID="lblAuthor" runat="server" Text=' <%#Eval("Author") %> '></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label1" runat="server" Text="Publisher"></asp:Label></label>
                                                            <div class="col-sm-8">
                                                                :
                                                                <asp:Label ID="lblPublisher" runat="server" Text=' <%#Eval("Publisher") %> '></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label4" runat="server" Text="Edition"></asp:Label></label>
                                                            <div class="col-sm-8">
                                                                :
                                                                <asp:Label ID="lblEdition" runat="server" Text=' <%#Eval("Edition") %> '></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-4">
                                                                <asp:Label ID="Label44" runat="server" Text="Stock "></asp:Label></label>
                                                            <div class="col-sm-8">
                                                                :  
                                                                    <asp:Label ID="lblStock" runat="server" Text=' <%#Eval("Stock") %> '></asp:Label>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <cc1:Rating ID="Rating1" AutoPostBack="true" OnChanged="OnRatingChanged" runat="server"
                                                        StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                        FilledStarCssClass="FilledStar">
                                                    </cc1:Rating>
                                                    <br />
                                                    <asp:Label ID="lblRatingStatus" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="col-sm-6 text-right">
                                                    <asp:Button ID="btnView" runat="server" class="btn btn-success btn-flat" Text="View" OnCommand="btnView_Command" CommandArgument='<%# Eval("Id")%>'></asp:Button>
                                                    <asp:Button ID="btnRequest" runat="server" class="btn btn-success btn-flat" Text="Request" CommandName="Select"></asp:Button>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
               </table>
                                </FooterTemplate>

                            </asp:Repeater>
                            <div class="row">
                                <div class="modal fade" tabindex="-1" id="bookDetails" role="dialog">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header bg-danger">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Book Details</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-9">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-horizontal">
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-5">
                                                                            <asp:Label ID="Label3" runat="server" Text="Category"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-5">
                                                                            <asp:Label ID="Label2" runat="server" Text="Sub-Category"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblSubCategory" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-5">
                                                                            <asp:Label ID="Label7" runat="server" Text="Country"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-5">
                                                                            <asp:Label ID="Label8" runat="server" Text="Language"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblLanguage" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-5">
                                                                            <asp:Label ID="Label15" runat="server" Text="Publisher"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblPublisher" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-5">
                                                                            <asp:Label ID="Label34" runat="server" Text="Edition"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblEdtion" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-horizontal">
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-4">
                                                                            <asp:Label ID="Label35" runat="server" Text="ISBN"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="lblISBN" runat="server" placeholder="Enter ISBN"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-4">
                                                                            <asp:Label ID="Label36" runat="server" Text="Volume"></asp:Label></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="lblVolume" runat="server" placeholder="Enter Volume No."></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-4">
                                                                            <asp:Label ID="Label37" runat="server" Text="Self No."></asp:Label></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="lblSelfNo" runat="server" placeholder="Enter self no."></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-4">
                                                                            <asp:Label ID="Label38" runat="server" Text="Cell No."></asp:Label></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="lblCellNo" runat="server" placeholder="Enter cell no"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-4">
                                                                            <asp:Label ID="Label39" runat="server" Text="Is avilable"></asp:Label></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="lblAvailable" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" id="stock">
                                                                        <label for="inputEmail3" class="col-sm-4">
                                                                            <asp:Label ID="Label40" runat="server" Text="Stock"></asp:Label></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="lblStock" runat="server" placeholder="Enter no of book available"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="form-horizontal">
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-2">
                                                                            <asp:Label ID="Label1" runat="server" Text="Title"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-10">
                                                                            <asp:Label ID="lblTitle" runat="server" placeholder="Enter Title"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-2">
                                                                            <asp:Label ID="Label6" runat="server" Text="Author"></asp:Label><span class="red">*</span></label>
                                                                        <div class="col-sm-10">
                                                                            <asp:Label ID="lblAuthor" runat="server" placeholder="Enter Author"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-2">
                                                                            <asp:Label ID="Label41" runat="server" Text="Subtitle"></asp:Label></label>
                                                                        <div class="col-sm-10">
                                                                            <asp:Label ID="lblSubTitle" runat="server" placeholder="Enter sub title"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-2">
                                                                            <asp:Label ID="Label42" runat="server" Text="Key words"></asp:Label></label>
                                                                        <div class="col-sm-10">
                                                                            <asp:Label ID="lblKeyWord" runat="server" placeholder="Enter Key Word"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-2">
                                                                            <asp:Label ID="Label43" runat="server" Text="Description"></asp:Label></label>
                                                                        <div class="col-sm-10">
                                                                            <asp:Label ID="lblDescription" TextMode="MultiLine" Rows="7" runat="server" placeholder="Enter Short Description"></asp:Label>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="col-sm-12 text-center">
                                                            <div class="panel panel-success">
                                                                <div class="panel-body">
                                                                    <asp:Image ID="imgCover" runat="server" CssClass="img img-responsive" />
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
            </div>
        </div>
        <%--    </ContentTemplate>--%>
    </asp:Panel>

    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>

    <script src="../../JS/Teacher.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%=btnStudent.ClientID %>').hide();
            $('#<%=btnTeacher.ClientID %>').hide();

            $("#example5 [id*=chkrow]").click(function () {

                var theCheckboxes = $("#example5 [id*=chkrow]");

                if (theCheckboxes.filter(":checked")) {
                    $('#<%=btnStudent.ClientID %>').slideDown();
                    if (theCheckboxes.filter(":checked").length > 1) {
                        $(this).removeAttr("checked");
                        alert("Please selected One student at a time for Issue Book.");

                        return false;
                    }
                }
                if (theCheckboxes.filter(":checked").length == 0) {
                    $('#<%=btnStudent.ClientID %>').slideUp();
                }


            });

            $("#example3 [id*=chkrow]").click(function () {

                var theCheckboxes = $("#example3 [id*=chkrow]");

                if (theCheckboxes.filter(":checked")) {
                    $('#<%=btnTeacher.ClientID %>').slideDown();
                    if (theCheckboxes.filter(":checked").length > 1) {
                        $(this).removeAttr("checked");
                        alert("Please selected One Teacher at a time for Issue Book.");

                        return false;
                    }
                }
                if (theCheckboxes.filter(":checked").length == 0) {
                    $('#<%=btnTeacher.ClientID %>').slideUp();
                }


            });

            $("#example5").DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": false,
                "autoWidth": true,

            });

            $("#example2").DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": false,
                "autoWidth": true,
            });

            $("#example3").DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": false,
                "autoWidth": true,

            });


            var checked_radio = $("[id*=rdList] input:checked");
            if (checked_radio.val() == "1") {
                $("#criteria").slideDown();
                $("#pnlAssignStudent").slideDown();
                $("#templete").slideDown();
                $("#pnlTeacher").hide();
                $("#pnlOther").hide();

            }
            else {

                $("#criteria").hide();
                $("#pnlAssignStudent").hide();
                $("#pnlTeacher").slideDown();
                $("#pnlOther").slideDown();
                $("#templete").slideDown();
                $("#pnlOther").hide();

            }

            // $("#pnlTeacher").hide();


            $("#rdList").change(function () {
                var checked_radio = $("[id*=rdList] input:checked");
                if (checked_radio.val() == "1") {
                    $("#criteria").slideDown();
                    $("#pnlAssignStudent").slideDown();
                    $("#templete").slideDown();
                    $("#pnlTeacher").hide();
                    $("#pnlOther").hide();
                    $("#pnlAllBookList").hide();
                    $("#pnlAssignedBook").hide();
                    $("#pnlReturnBook").hide();
                    $("table[id$='gvStudent']").html("");
                    $("table[id$='gvBookReturn']").html("");
                    $("table[id$='rptStudent']").html("");


                }
                if (checked_radio.val() == "2") {
                    $("#criteria").slideDown();
                    $("#pnlAssignStudent").hide();
                    $("#pnlTeacher").slideDown();
                    $("#templete").slideDown();
                    $("#pnlOther").hide();
                    $("#pnlAllBookList").hide();
                    $("#pnlAssignedBook").hide();
                    $("#pnlReturnBook").hide();
                    $("table[id$='gvTeacher']").html("");
                    $("table[id$='gvBookReturn']").html("");


                }
                if (checked_radio.val() == "3") {
                    $("#criteria").hide();
                    $("#pnlAssignStudent").hide();
                    $("#pnlTeacher").hide();
                    $("#templete").slideDown();
                    $("#pnlOther").slideDown();
                    $("#pnlAllBookList").hide();
                    $("#pnlAssignedBook").hide();
                    $("#pnlReturnBook").hide();
                    $("table[id$='gvBookReturn']").html("");

                }
            });

            $(".aa").first().closest(".treeview").addClass("active");
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $("#example1").DataTable({
                    "paging": false,
                    "lengthChange": false,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": true
                });
                //$(".treeview").removeClass("active");
                $(".aa").first().closest(".treeview").addClass("active");
            }
        });

        $(function () {
            $("[id*=gvOther] [id*=lnkDelete]").hide();
            $("[id*=btnOther]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=gvOther]");
                //Reference the first row.
                var row = gridView.find("tr").eq(1);
                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();

                }

                //Clone the reference first row.
                row = row.clone(true);
                var txtBoard = $("[id*=tbxOtherName]");
                SetValue(row, 0, "nameEng", txtBoard);

                var txtSubject = $("[id*=tbxOtherDsg]");
                SetValue(row, 1, "designation", txtSubject);

                var txtYear = $("[id*=tbxOtherAddress]");
                SetValue(row, 2, "address", txtYear);

                var txtGPA = $("[id*=tbxOtherMobile]");
                SetValue(row, 3, "mobile", txtGPA);



                //Add the row to the GridView.
                gridView.append(row);
                $("[id*=gvOther] [id*=lnkDelete]").show();
                $("#pnlAllBookList").slideDown();
                $('#Other').modal('hide');
                return false;
            });

            function SetValue(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());
                //Create and add a Hidden Field to send value to server. 
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);
                //Clear the TextBox.
                textbox.val("");
            }
        });

        $(function () {
            $("[id*=gvOther]").on('click', '[id*=lnkDelete11]', function (e) {
                e.preventDefault();
                var totalRows = $("[id*=gvOther] tr").length;
                if (totalRows > 2) {
                    $("[id*=gvOther] [id*=lnkDelete11]").show();
                    var row = $(this).closest("tr");
                    row.remove();

                }
                else {
                    $("[id*=gvOther] [id*=lnkDelete11]").hide();

                }
                return false;
            });



        });

        function UserResetConfirmation() {
            return confirm("Are you sure you want to Reset?");
        }

        function IssueConfirmation() {
            return confirm("Are you sure you want to Issue Slected Book?");
        }

        function ReturnConfirmation() {
            return confirm("Are you sure you want to Return Slected Book?");
        }



    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        function openModal() {
            $('#bookDetails').modal('show');
        }


    </script>
</asp:Content>

