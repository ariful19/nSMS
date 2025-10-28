<%@ Page Title="<%$ Resources:Application,SubVoucher %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SubVoucher.aspx.cs" Inherits="Pages_Account_SubVoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    
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
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,VoucherCodeId %>"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlMainVoucherCode" runat="server" DataTextField="VoucherCodeId" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,SubVoucherCodeId %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxSubVoucherCode" runat="server" placeholder="Enter Sub Voucher Code" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Sub Voucher Code" ControlToValidate="tbxSubVoucherCode">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,SubVoucherName %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxSubVoucherName" runat="server" placeholder="Enter Sub Voucher Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Sub Voucher Name" ControlToValidate="tbxSubVoucherName">*</asp:RequiredFieldValidator>
                    </div>
                </div> 
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptSubVoucher" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th class="action">
                                                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label>
                                                    </th>
                                                      <th>
                                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,SubVoucherCodeId %>"></asp:Label>
                                                    </th>
                                                      <th>
                                                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,SubVoucherName %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,VoucherCodeId %>"> 
                                                        </asp:Label>
                                                    </th>                                              
                                                   <%-- <th class="action">
                                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label>
                                                    </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="action"><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("SubVoucherId") %></td>
                                            <td><%#Eval("SubVoucherName") %></td>
                                            <td><%#Eval("VoucherCodeId") %></td>
                                           <%-- <td class="action">
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                                <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />

                                            </td>--%>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

