<%@ Page Title="<%$ Resources:Application,MessageCreditView %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="MessageCreditView.aspx.cs" Inherits="Pages_Notification_MessageCreditView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-6">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,FromDate %>"></asp:Label>
                        </label>
                        <asp:TextBox runat="server" ID="tbxfromDate" placeholder="Enter From Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter From Date." ControlToValidate="tbxfromDate">Enter From Date.</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-6">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,ToDate %>"></asp:Label>
                        </label>
                        <asp:TextBox runat="server" ID="tbxToDate" placeholder="Enter To Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter To Date." ControlToValidate="tbxToDate">Enter To Date.</asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="col-sm-2">
                    <div class="form-group">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="<%$ Resources:Application,Search %>" ValidationGroup="save" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:Panel ClientIDMode="Static" ID="pnlMessageLog" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,MessageCreditList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
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
            </asp:Panel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
   <script src="../../Scripts/bootstrap-timepicker.min.js"></script>

    <script type="text/javascript">
        $(function () {

            $("#tbxfromDate").datepicker({
                dateFormat: 'dd/mm/yy'

            });
            $("#tbxToDate").datepicker({ dateFormat: 'dd/mm/yy' });

            $('#tbxfromDate').change(function () {
                var currentDate = new Date();
                var text = $('#tbxfromDate').val();
                var comp = text.split('/');
                var d = parseInt(comp[0], 10);
                var m = parseInt(comp[1], 10);
                var y = parseInt(comp[2], 10);
                var date = new Date(y, m - 1, d);

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

            $('#tbxToDate').change(function () {
                var currentDate = new Date();

                var text = $('#tbxToDate').val();
                var comp = text.split('/');
                var d = parseInt(comp[0], 10);
                var m = parseInt(comp[1], 10);
                var y = parseInt(comp[2], 10);
                var date = new Date(y, m - 1, d);
                var date = DateFormate(text);

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
        function DateFormate(x) {
            var todate = x.substring(0, 2);
            var toMonth = x.substring(3, 5);
            var toYear = x.substring(6, 10);
            var toDate = new Date(toYear, toMonth - 1, todate);
            return toDate;
        }

    </script>
</asp:Content>

