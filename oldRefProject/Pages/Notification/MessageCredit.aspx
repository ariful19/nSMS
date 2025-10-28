<%@ Page Title="<%$ Resources:Application,MessageCreditSetup %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="MessageCredit.aspx.cs" Inherits="Pages_Notification_MessageCredit" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />


    <asp:UpdatePanel ID="Update" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-9">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,PurchaseDate %>"></asp:Label></label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Quantity %>"></asp:Label></label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <asp:TextBox ID="tbxQuantity" runat="server" CssClass="form-control" placeholder="Enter Quantity" ClientIDMode="Static"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Balance %>"></asp:Label></label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <asp:TextBox ID="tbxAmount" runat="server" placeholder="Enter Amount" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Amount" ControlToValidate="tbxAmount">*</asp:RequiredFieldValidator>
                                </div>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxAmount" ValidationGroup="save" ErrorMessage="Amount maximum length eight digits and can use two digits after dot" ValidationExpression="^(?!\.?$)\d{0,8}(\.\d{0,2})?$"> </asp:RegularExpressionValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                    Enabled="True" TargetControlID="tbxAmount" FilterType="Custom" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>

                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Transaction %>"></asp:Label></label>

                            <div class="col-sm-9">
                                <div class="input-group">
                                    <asp:TextBox ID="tbxTranscation" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,IsDefault %>"></asp:Label>
                    </label>
                    <div class="col-sm-9">
                         <div class="input-group">
                        <asp:CheckBox ID="chkDefault" runat="server" />
                             </div>
                    </div>
                </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="<%$ Resources:Application,Purchase %>" ValidationGroup="save" />
                                <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                                    OnClick="btnEdit_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                                    OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <div class="row pt-10">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptMessageCredit" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th class="action">
                                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Quantity %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,PurchaseDate %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,SendQuantity %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Balance %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,AvailableBalance %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Transaction %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,IsDefault %>"></asp:Label>
                                                </th>

                                                <th class="action">
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="action"><%# Container.ItemIndex + 1 %></td>
                                        <td><%#Eval("PurchaseQuantity")%></td>
                                        <td><%#Convert.ToDateTime(Eval("PurchaseDate")).ToString("dd/MM/yyyy") %></td>
                                        <td><%#Eval("SendQuantity") %></td>
                                        <td><%#Eval("Balance") %></td>
                                        <td><%#Eval("AvailableBalance") %></td>
                                        <td><%#Eval("TransactionNumber") %></td>
                                        <td><%#Eval("IsDefault") %></td>
                                        <td class="action">
                                            <asp:LinkButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ToolTip="Edit" CssClass="fa fa-2x fa-edit" />
                                            <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" OnClientClick="return confirm('Are you sure?')" />
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
   <%-- <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $(".timepicker").timepicker({
            showInputs: false
        });
        $(function () {
            $("#tbxDate").datepicker({ dateFormat: 'dd/mm/yy' });
            //$("#tbxArchive").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>--%>
</asp:Content>

