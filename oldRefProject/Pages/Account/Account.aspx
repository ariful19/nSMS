<%@ Page Title="<%$ Resources:Application,Account %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Pages_Account_Account" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-10 col-sm-10 col-xs-10">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,SubAccountCodeId %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlSubAccountHead" runat="server" DataTextField="SubHeadCodeId" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,AccountCode %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxAccountCode" runat="server" placeholder="Enter Account Code" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Account Code" ControlToValidate="tbxAccountCode">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,AccountName %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxAccountName" runat="server" placeholder="Enter Account Name" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Account Name" ControlToValidate="tbxAccountName">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Description %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-11">
                                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                                    OnClick="btnSave_Click" OnClientClick="if ( ! SaveConfirmation()) return false;"/>
                                <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                                    OnClick="btnEdit_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                                    OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Status %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxStatus" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Balance %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <div class="input-group">
                                     <div class="input-group-addon">
                                        <i class="fa fa-dollar"></i>
                                    </div>
                                <asp:TextBox ID="tbxBalance" runat="server" placeholder="Enter Balance" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                    Enabled="True" TargetControlID="tbxBalance" FilterType="Custom" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Balance" ControlToValidate="tbxBalance">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,OpenDate %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxOpenDate" runat="server" placeholder="Enter Open Date" CssClass="form-control" MaxLength="10"></asp:TextBox>
                               <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxOpenDate"
                                    TargetControlID="tbxOpenDate">
                                </cc1:CalendarExtender>--%>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Open Date" ControlToValidate="tbxOpenDate">***</asp:RequiredFieldValidator>
                               <%--  <asp:CustomValidator runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="tbxOpenDate" ErrorMessage="Invalid Date." ValidationGroup="Group2"></asp:CustomValidator>--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptAccountHead" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th class="action">
                                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,SubAccountCodeId %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,AccountName %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,AccountCode %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Description %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,OpenDate %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Balance %>"></asp:Label></th>
                                                <th class="action">
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="action"><%# Container.ItemIndex + 1 %></td>
                                        <td><%#Eval("SubHeadId") %></td>
                                        <td><%#Eval("AccountName") %></td>
                                        <td><%#Eval("AccountCodeId") %></td>
                                        <td><%#Eval("Description") %></td>
                                        <td><%#Convert.ToDateTime(Eval("OpenDate")).ToString("dd/MM/yyyy") %></td>
                                        <td><%#Eval("Balance") %></td>
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
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
     <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
 <script type="text/javascript">
     $(document).ready(function () {
         $('#<%=tbxOpenDate.ClientID %>').datepicker({
             dateFormat: 'dd/mm/yy'

         });

         $('#<%=tbxOpenDate.ClientID %>').change(function () {
      
             var currentDate = new Date();
             var text = $('#<%=tbxOpenDate.ClientID %>').val();
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
     function SaveConfirmation() {
         return confirm("Are you sure you want to Save Exam Routine?");
     }
     function DeleteConfirmation() {
         return confirm("Are you sure you want to Delete Exam Routine?");
     }
    </script>
</asp:Content>

