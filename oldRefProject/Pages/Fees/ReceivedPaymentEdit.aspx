<%@ Page Title="<%$ Resources:Application,ReceivedPaymentEdit %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ReceivedPaymentEdit.aspx.cs" Inherits="Pages_Fees_ReceivedPaymentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <div class="row">
        <div class="col-sm-12">
            <div class=''>
                <div class="panel-heading">
                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,StudentPaymentHistory %>" ForeColor="#6600ff" Font-Size="14" Width="100%" Font-Underline="true"></asp:Label>
                </div>
                <div id="divSearch" runat="server" class="form-group" style="margin-top: 10px; padding-bottom: 10px">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,BillNo %>"></asp:Label>
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxBillNo" runat="server" placeholder="Enter Bill No" CssClass="form-control" MaxLength="12" ForeColor="#990099" Font-Size="13" Width="100%"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                            Enabled="True" TargetControlID="tbxBillNo" FilterType="Custom" ValidChars="0123456789.-"></cc1:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save1"
                            ControlToValidate="tbxBillNo">Enter Reg No</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-5">
                        <asp:Button ID="btnHistory" CssClass="btn btn-flat btn-primary" runat="server" Text="View History" OnClick="btnHistory_Click" ValidationGroup="save1" />
                        <asp:Button ID="btnPrint" CssClass="btn btn-flat btn-default" runat="server" Text="Print" OnClick="btnPrint_Click" ValidationGroup="save1" />
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row" id="divPaymentUpdate" runat="server">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,BillNo%>" ForeColor="#cc3300" Font-Size="13" Width="100%"></asp:Label>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxBillEdit" runat="server" placeholder="Enter Bill No" MaxLength="12" CssClass="form-control" ForeColor="#990099" Font-Size="13" Width="100%"></asp:TextBox>
                                        <asp:Label ID="lblPaymentId" runat="server" Visible="false"></asp:Label>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" TargetControlID="tbxBillEdit" FilterType="Custom" ValidChars="0123456789.-"></cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                            ControlToValidate="tbxBillEdit">Enter Bill No</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Amount%>"></asp:Label>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxAmount" runat="server" placeholder="Enter Amount" MaxLength="12" CssClass="form-control" Enabled="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,PaidAmount%>"></asp:Label>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxGivenAmount" runat="server" placeholder="Enter Amount" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            Enabled="True" TargetControlID="tbxAmount" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                            ControlToValidate="tbxAmount">Enter Amount</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Remarks %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxRemarks" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                            ControlToValidate="tbxRemarks">Enter Remarks</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-sm-6">
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-default" Text="Update" ValidationGroup="save" OnClick="btnUpdate_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlSession" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Month %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control dropdown" DataTextField="Month" DataValueField="Id"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label34" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,PaymentType %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" DataTextField="PaymentType" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:Repeater ID="rptHistory" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th class="text-center">Bill No</th>
                                        <th class="text-center">Session</th>
                                        <th class="text-center">Month</th>
                                        <th class="text-center">Date</th>
                                        <th class="text-center">Name</th>
                                        <th class="text-center">Total Amount</th>
                                        <th class="text-center">Total Given</th>
                                        <th class="text-center">Total Due</th>
                                        <th class="text-center">Receive By</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("BillNo") %></td>
                                <td><%#Eval("Year") %></td>
                                <td><%#Eval("MonthYear") %></td>
                                <td><%#Eval("Date") %></td>
                                <td><%#Eval("PaymentType") %></td>
                                <td><%#Eval("Amount") %></td>
                                <td><%#Eval("TotalGiven") %></td>
                                <td><%#Eval("DueAmount") %></td>
                                <td><%#Eval("CreatedBy") %></td>
                                <td>
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
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="script">
    <script src="../../JS/print.js"></script>
    <script type="text/javascript">
        $("#tbxDate").datepicker({
            dateFormat: "dd/mm/yy",
            showOtherMonths: true,
            selectOtherMonths: true,


        });

        $("#tbxDate").on('change', function () {
            var currentDate = new Date();
            var text = $('#tbxDate').val();
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

    </script>
</asp:Content>


