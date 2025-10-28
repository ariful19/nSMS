<%@ Page Title="<%$ Resources:Application,StudentPaymentHistory %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PaidHistory.aspx.cs" Inherits="Pages_Student_PaidHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <div class="row">
        <div class="col-sm-12">
            <div class='<%= Common.SessionInfo.Panel %>'>

                <div class="panel-heading">
                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,StudentPaymentHistory %>" ForeColor="#6600ff" Font-Size="14" Width="100%" Font-Underline="true"></asp:Label>
                </div>
                <div id="divSearch" runat="server" class="form-group" style="margin-top: 10px; padding-bottom: 10px">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Reg. No" CssClass="form-control" MaxLength="12" ForeColor="#990099" Font-Size="13" Width="100%"></asp:TextBox>
                       <%-- <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                            Enabled="True" TargetControlID="tbxReg" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save1"
                            ControlToValidate="tbxReg">Enter Reg No</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-5">
                        <asp:Button ID="btnHistory" CssClass="btn btn-flat btn-primary" runat="server" Text="View History" OnClick="btnHistory_Click" ValidationGroup="save1" />
                    </div>
                </div>
                <div class="panel-body">
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
                                <td><%#Eval("NameEng") %></td>
                                <td><%#Eval("Amount") %></td>
                                <td><%#Eval("TotalGiven") %></td>
                                <td><%#Eval("DueAmount") %></td>
                                <td><%#Eval("CreatedBy") %></td>
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


