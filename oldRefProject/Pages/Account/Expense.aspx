<%@ Page Title="<%$ Resources:Application,Expense %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Expense.aspx.cs" Inherits="Pages_Account_Expense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
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
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbxDate" ValidationGroup="save"
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                        ErrorMessage="Date format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,ExpenseCategory %>"></asp:Label></label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlExpense" runat="server" DataValueField="Id" DataTextField="ExpenseCategory" CssClass="form-control"></asp:DropDownList>
                                    <div class="input-group-addon">
                                        <a title="Add new" data-toggle="modal" href="#" data-target="#Income"><i class="fa fa-plus"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Amount %>"></asp:Label></label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-dollar"></i>
                                    </div>
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
                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Attachment %>"></asp:Label></label>
                            <div class="col-sm-9">
                                <asp:FileUpload ID="attachmentUpload" runat="server" CssClass="btn btn-default" />

                                <asp:Image ID="imgAttachment" runat="server" CssClass="img img-thumble image-responsive pull-right" Width="120" Height="120" Visible="false" />


                                <div id="pdfHolder" runat="server" visible="false" style="width: 300px; height: 250px; overflow-y: scroll">
                                    <asp:Literal ID="pdfContent" runat="server" />
                                </div>


                                <p class="help-block">Note: only .pdf,.jpg ,.png are allowed.</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="<%$ Resources:Application,Save %>" ValidationGroup="save" />
                                 <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-success"  ValidationGroup="save" Visible="false"
                            OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-1">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Note %>"></asp:Label></label>
                            <div class="col-sm-11">
                                <asp:TextBox ID="tbxNote" TextMode="MultiLine" Rows="8" runat="server" placeholder="Enter Note" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row pt-10">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rpt" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th class="action">
                                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,ExpenseCategory %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Amount %>"></asp:Label></th>

                                                <th class="action">
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="action"><%# Container.ItemIndex + 1 %></td>
                                        <td><%#Convert.ToDateTime(Eval("ExpenseDate")).ToString("dd/MM/yyyy") %></td>
                                        <td><%#Eval("ExpenseCategory") %></td>
                                        <td><%#Eval("Amount") %></td>
                                        <td class="action">
                                            <asp:LinkButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ToolTip="Edit" CssClass="fa fa-2x fa-edit" />
                                           <%-- <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ToolTip="Edit" CssClass="fa fa-2x fa-file-archive-o" />--%>
                                            <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" OnClientClick="return confirm('Are you sure?')" /></td>
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

    <div class="row">
        <div id="Income" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-info">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Add new Expense Category</h4>
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
                                <label for="inputEmail3" class="col-sm-3">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,ExpenseCategory %>"></asp:Label></label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Expense Category" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="add"
                                        ErrorMessage="Enter Expense Category" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnIncomeCategory" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="add"
                            OnClick="btnIncomeCategory_Click" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">

    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">


        $(".timepicker").timepicker({
            showInputs: false
        });
        $(document).ready(function () {
            //    $("#lbelError").hide();
            //    $("#tbxDate").change(function () {
            //        var dt = $("#tbxDate").val();
            //        var cd = new Date();

            //        if (Date.parse(dt) > Date.parse(cd)) {
            //            $("#lbelError").slideDown();
            //            $("#3").hide();
            //        }
            //        else {
            //            $("#lbelError").hide();
            //            $("#3").show();
            //        }

            //    });
            $("#1").hide();
            $("#2").hide();
            $("#3").hide();
            $("#rdList").change(function () {
                var checked_radio = $("[id*=rdList] input:checked");
                if (checked_radio.val() == "1") {
                    $("#1").slideDown();
                    $("#2").hide();
                    $("#3").show();
                }
                else {
                    $("#2").slideDown();
                    $("#1").hide();
                    $("#3").show();

                }
            });
        });
        $(function () {
            $("#tbxDate").datepicker({ dateFormat: 'dd/mm/yy' });
            // $("#tbxArchive").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
</asp:Content>

