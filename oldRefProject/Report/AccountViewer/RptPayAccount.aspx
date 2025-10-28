<%@ Page Title="<%$ Resources:Application,PayAccountReport %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master"  AutoEventWireup="true" CodeFile="RptPayAccount.aspx.cs" Inherits="Report_AccountViewer_RptPayAccount" %>

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
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-6">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,AccountId %>"></asp:Label>
                                </label>
                                <asp:TextBox runat="server" ID="tbxAccountId" placeholder="Enter Accounts Id" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Account Id." ControlToValidate="tbxAccountId">***</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-6">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,FromDate %>"></asp:Label>
                                </label>
                                <asp:TextBox runat="server" ID="tbxfromDate" placeholder="Enter From Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter From Date." ControlToValidate="tbxfromDate">***</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-6">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,ToDate %>"></asp:Label>
                                </label>
                                <asp:TextBox runat="server" ID="tbxToDate" placeholder="Enter To Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter To Date." ControlToValidate="tbxToDate">***</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <br />
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Button ID="btnReport" CssClass="btn btn-success" runat="server" Text="<%$ Resources:Application,ShowReport %>" ValidationGroup="save" OnClick="btnReport_Click" />
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

            $('#tbxToDate').change(function () {
                var currentDate = new Date();

                var text = $('#tbxToDate').val();
                var comp = text.split('/');
                var d = parseInt(comp[0], 10);
                var m = parseInt(comp[1], 10);
                var y = parseInt(comp[2], 10);
                var date = new Date(y, m - 1, d)
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