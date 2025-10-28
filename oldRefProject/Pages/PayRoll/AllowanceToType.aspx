<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="AllowanceToType.aspx.cs" Inherits="Pages_PayRoll_AllowanceToType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">

                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label5" runat="server" Text="Payroll Type"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlType" runat="server" DataTextField="Type" DataValueField="Id" CssClass="form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-8">
                        <div class='<%= Common.SessionInfo.Panel %>'>
                            <div class="panel-heading">
                                <asp:Label ID="Label3" runat="server" Text="Allowance Type"></asp:Label>
                                <a href="#" title="Add new Type" id="newItem" data-toggle="modal" data-target="#addnew" class="pull-right"><i class="fa fa-plus"></i>Add new</a>
                            </div>
                            <div class="panel-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <table id="example12" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label3" runat="server" Text="Allowance Type"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label1" runat="server" Text="Amount(%)"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Allowance") %>
                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxAmount" runat="server" placeholder="Enter Amount" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter Amount" ControlToValidate="tbxAmount">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="panel-footer">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Save %>" />
                                <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Edit %>" Visible="false"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="addnew" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header bg-info">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Add new Income Category</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-10 col-sm-10 col-xs-10">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="add" />
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label2" runat="server" Text="Allowance"></asp:Label></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Allowance Type" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="add"
                                                ErrorMessage="Enter Allowance Type" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnAddNew" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="add"
                                    OnClick="btnAddNew_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlType" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

