<%@ Page Title="<%$ Resources:Application,FeesforClass %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PaymentFeeSetup.aspx.cs" Inherits="Pages_Fees_PaymentFeeSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="criteria">
        <div class="panel-heading">
            <asp:Label ID="lblCriteria" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,StartMonth %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlStartMonth" runat="server" DataTextField="Month" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,EndMonth %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlEndMonth" runat="server" DataTextField="Month" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" />
        </div>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-sm-9">
                    <div>
                        <div class="panel-heading">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,FeesforClass %>"></asp:Label>
                            <a href="#" title="Add new Type" id="newItem" data-toggle="modal" data-target="#Item" class="pull-right"><i class="fa fa-plus"></i></a>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="pnlNew" runat="server">
                                <asp:Repeater ID="rptPaymentType" runat="server">
                                    <HeaderTemplate>
                                        <table id="example12" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,PaymentType %>"></asp:Label></th>
                                                    <th>Amount</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("PaymentType") %>
                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPaymentType" Text='<%#Eval("Amount") %>' runat="server" paleholder="Enter Payment Amount" CssClass="form-control"></asp:TextBox>
                                                <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Currency"
                                                    ControlToValidate="tbxPaymentType" ErrorMessage="Value must be a whole number" />
                                            </td>

                                            <td style="display: none">
                                                <asp:TextBox ID="txtPaymentToClassID" Text='<%#Eval("PaymentToClassID") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:TextBox ID="tbxIsDefault" Text='<%#Eval("IsDefault") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                        <div class="panel-footer" align="right">
                            <asp:Button ID="btnPayment" runat="server" OnClick="btnPayment_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Submit %>" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-9" style="display: none">
                    <div>
                        <div class="panel-heading">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Fees %>"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <asp:Repeater ID="rptPaymentByClass" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,PaymentType %>"></asp:Label></th>
                                                <th>Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("PaymentType") %></td>
                                        <td><%#Eval("Amount") %></td>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="row">
        <div class="modal fade" tabindex="-1" id="Item" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header bg-danger">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Add New Payment Type</h4>
                    </div>
                    <div class="modal-body text-center">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-10 col-sm-10 col-xs-10">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                    <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                    <asp:HiddenField ID="hdnID" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,PaymentType %>"></asp:Label></label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Payment Type" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Payment Type" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,IsMonthly %>"></asp:Label></label>
                                <div class="col-sm-10">
                                    <asp:CheckBox ID="chkMonthly" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,IsDefault %>"></asp:Label></label>
                                <div class="col-sm-10">
                                    <asp:CheckBox ID="chkDefault" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSave" ClientIDMode="Static" runat="server" Text="Save" CssClass="btn btn-success" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <button type="button" class="btn btn-info" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

