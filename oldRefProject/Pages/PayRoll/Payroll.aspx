<%@ Page Title="Payroll" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Payroll.aspx.cs" Inherits="Pages_PayRoll_Payroll" %>

<%@ Register Src="~/UserControl/SchoolHeader.ascx" TagName="SchoolHeader" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-4">
            <div class="panel panel-success">
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Year" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label3" runat="server" Text="Month"></asp:Label></label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control dropdown">
                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label4" runat="server" Text="Pin Code"></asp:Label></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxRegNo" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Registration No." ControlToValidate="tbxRegNo">Enter Teacher Pin No.</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-flat btn-success" OnClick="btnGenerate_Click" Text="Generate Bill" ValidationGroup="save" />
                </div>
            </div>
        </div>
        <div class="col-sm-8">
            <asp:UpdatePanel ID="Update" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlPayroll" runat="server">
                        <div class="panel panel-success">
                            <div class="panel-body mainbody">
                                <uc:SchoolHeader ID="school" runat="server" />
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table class="table table-responsive mytable">
                                            <tr>
                                                <td>Name</td>
                                                <td>
                                                    <asp:Label ID="lblTeacherName" runat="server" Text="Name"></asp:Label></td>
                                                <td>Grade</td>
                                                <td>
                                                    <asp:Label ID="lblGrade" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Designation</td>
                                                <td>
                                                    <asp:Label ID="lblDesignation" runat="server" Text="Designation"></asp:Label></td>
                                                <td>PayScale</td>
                                                <td>
                                                    <asp:Label ID="lblScale" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Teacher Pin</td>
                                                <td>
                                                    <asp:Label ID="lblPinCode" runat="server" Text="Pin Code"></asp:Label></td>
                                                <td>Basic Salary</td>
                                                <td>
                                                    <asp:Label ID="lblBasic" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div>
                                    <hr />
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <h4>***Allowance***</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Repeater ID="rptAllowance" runat="server">
                                            <HeaderTemplate>
                                                <table id="example12" class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:Label ID="Label3" runat="server" Text="Allowance Type"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="Label1" runat="server" Text="Percent(%)"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="Label2" runat="server" Text="Amount"></asp:Label></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("Allowance") %>
                                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                                    </td>
                                                    <td><%#Eval("AllowancePercent") %></td>
                                                    <td class="text-right"><%#Eval("AllowanceAmount") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <h4>***Deduction***</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Repeater ID="rptDeduction" runat="server">
                                            <HeaderTemplate>
                                                <table id="example12" class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:Label ID="Label3" runat="server" Text="Deduction Type"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="Label1" runat="server" Text="Percent(%)"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="Label2" runat="server" Text="Amount"></asp:Label></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("Deduction") %>
                                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                                    </td>
                                                    <td><%#Eval("DeductionPercent") %></td>
                                                    <td class="text-right"><%#Eval("DeductionAmount") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-6 col-sm-offset-6 text-right">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-6">
                                                    <asp:Label ID="Label6" runat="server" Text="Basic Salary"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblBasicSalary" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-6">
                                                    <asp:Label ID="Label7" runat="server" Text="Total Allowance (+)"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblTotalAllowance" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-6">
                                                    <asp:Label ID="Label8" runat="server" Text="Total Deduction (-)"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblTotalDeduction" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <hr />
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-sm-offset-6 text-right">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-6">
                                                    <asp:Label ID="Label12" runat="server" Text="Net Salary"></asp:Label></label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblNetSalary" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnPrintBill" ClientIDMode="Static" runat="server" Text="Print Bill" class="btn btn-block btn-flat btn-warning" />
                                    </div>
                                    <div class="col-sm-6">
                                        <button type="button" class="btn btn-block btn-flat btn-success" data-toggle="modal" data-target="#payment">Payment</button>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGenerate" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script src="../../JS/print.js"></script>
</asp:Content>

