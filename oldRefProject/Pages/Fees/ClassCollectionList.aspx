<%@ Page Title="<%$ Resources:Application,ClassCoectionList %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ClassCollectionList.aspx.cs" Inherits="Pages_Fees_ClassCollectionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class=''>
        <div class="panel-body">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-6">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,FromDate %>"></asp:Label>
                            </label>
                            <asp:TextBox runat="server" ID="tbxFromDate" placeholder="Enter From Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
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
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,PaymentType %>"></asp:Label>
                        </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPaymentType" DataTextField="PaymentType" DataValueField="Id" runat="server" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="col-sm-2">
            <div class="form-group">
                <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="<%$ Resources:Application,Search %>" ValidationGroup="save" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>
    <div class="row" id="divPaymentHistory" runat="server">
        <div class="col-sm-12">
            <div class=''>
                <div class="panel-heading">
                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,StudentPaymentHistory %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rptHistory" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th class="text-center">Bill No</th>
                                        <th class="text-center">Campus</th>
                                        <th class="text-center">Medium</th>
                                        <th class="text-center">Stu.Name</th>
                                        <th class="text-center">Stu. ID</th>
                                        <th class="text-center">Month-Year</th>
                                        <th class="text-center">Payment</th>
                                        <th class="text-center">Total Amount</th>
                                        <th class="text-center">Received Amount</th>
                                        <th class="text-center">Date</th>
                                        <th class="text-center">Received By</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%#Eval("BillNo") %></td>
                                <td class="text-center"><%#Eval("CampusName") %></td>
                                <td class="text-center"><%#Eval("MediumName") %></td>
                                <td class="text-center"><%#Eval("NameEng") %></td>
                                <td class="text-center"><%#Eval("RegNo") %></td>
                                <td class="text-center"><%#Eval("MonthYear") %></td>
                                <td class="text-center"><%#Eval("PaymentType") %></td>
                                <td class="text-center"><%#Eval("Amount") %></td>
                                <td class="text-center"><%#Eval("TotalGiven") %></td>
                                <td class="text-center"><%#Eval("Date") %></td>
                                <td class="text-center"><%#Eval("CreatedBy") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            <tbody>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td class="text-center">
                                        <asp:Label ID="Label5" runat="server" Text="Total Amount: " Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblTotalAmount" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="Label3" runat="server" Text="Total Received: " Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnReport" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:Application,GenerateReport %>" OnClick="btnReport_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tbxFromDate").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#tbxToDate").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#tbxFromDate").on('change', function () {

                var text = $('#<%=tbxFromDate.ClientID %>').val();
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
                var currentDate = new Date();

                if (DateFormate(document.getElementById("tbxFromDate").value) > currentDate) {
                    alert("Please select smaller Date than Current Date. ");
                    $(this).val(Today());
                }
                var toDate = DateFormate(document.getElementById("tbxToDate").value);

                if (document.getElementById("tbxToDate").value != '') {
                    if (DateFormate(document.getElementById("tbxFromDate").value) > toDate) {
                        alert("To Date must be smaller than To Date. ");
                        $(this).val(toDate.format('dd/MM/yyyy'));
                    }
                }
            });
            $("#tbxToDate").on('change', function () {

                var text = $('#<%=tbxToDate.ClientID %>').val();
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
                var currentDate = new Date();
                if (DateFormate(document.getElementById("tbxToDate").value) > currentDate) {
                    alert("Please select smaller Date than Current Date. ");
                    $(this).val(Today());
                }
                var fromDate = DateFormate(document.getElementById("tbxFromDate").value);

                if (document.getElementById("tbxFromDate").value != '') {
                    if (DateFormate(document.getElementById("tbxToDate").value) < fromDate) {
                        alert("To Date must be bigger than From Date. ");
                        $(this).val(Today());
                    }
                }
            });
            function DateFormate(x) {
                var todate = x.substring(0, 2);
                var toMonth = x.substring(3, 5);
                var toYear = x.substring(6, 10);
                var toDate = new Date(toYear, toMonth - 1, todate);
                return toDate;
            }
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
        });
    </script>
</asp:Content>
