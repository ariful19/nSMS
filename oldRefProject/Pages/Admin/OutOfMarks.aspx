<%@ Page Title="<%$ Resources:Application,OutOfMarksSetup %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="OutOfMarks.aspx.cs" Inherits="Pages_Admin_OutOfMarks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="save"/>
                        <asp:Label runat="server" ID="lblMessage" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField runat="server" ID="hnId"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2">
                        <asp:Label runat="server" ID="lblOutOfMarks" Text="<%$ Resources:Application,OutOfMarks %>">"></asp:Label>
                    </label>
                      <div class="col-sm-3">
                        <asp:TextBox ID="tbxOutOfMarks" runat="server" placeholder="Enter Out Of Marks" CssClass="form-control" MaxLength="3"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Out Of Marks" ControlToValidate="tbxOutOfMarks">*</asp:RequiredFieldValidator>
                          <cc1:FilteredTextBoxExtender runat="server" ID="FilteredtbxOutOfMarks" 
                              Enabled="True" TargetControlID="tbxOutOfMarks" FilterType="Custom" ValidChars="0123456789">
                           </cc1:FilteredTextBoxExtender>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" ValidationGroup="save" Visible="false" OnClick="btnEdit_Click"/>
                        <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnReset_Click"/>
                    </div>
                </div>
                  <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptOutOfMarks" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th class="action">
                                                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,OutOfMarks %>"></asp:Label></th>
                                                    <th class="action">
                                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                             <td class="action"><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("OutOfMarks") %></td>
                                            <td class="action">
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                                <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
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

