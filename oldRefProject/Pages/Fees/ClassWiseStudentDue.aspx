<%@ Page Title="Class Wise Student Due List" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ClassWiseStudentDue.aspx.cs" Inherits="Pages_Fees_ClassWiseStudentDue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" ForeColor="#0000ff" Font-Size="14" Width="100%">
                    <asp:ListItem Value="1" Text="Month Wise" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Student Wise"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    <div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label37" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divPaymentType" runat="server">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,PaymentType %>"></asp:Label>
                        </label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlPaymentType" runat="server" DataTextField="PaymentType" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6" id="divCriteria1" runat="server">
                <div class="form-horizontal">

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label31" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
         <div class="panel panel-footer">
            <div class="form-group">
                <div class="col-sm-8">
                    <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" ValidationGroup="save" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnAllReport" CssClass="btn btn-success" runat="server" Text="Report" ValidationGroup="save" OnClick="btnAllReport_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="divDueList" runat="server">
        <div class="col-sm-12">
            <div>
                <div class="panel-heading">
                    <asp:Label ID="lblStuDueList" runat="server" Text="Student Dues List"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rptHistory" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th class="text-center">Stu.ID</th>
                                        <th class="text-center">Name</th>
                                        <th class="text-center">Class</th>
                                        <th class="text-center">Total Amount</th>
                                        <th class="text-center">Total Given</th>
                                        <th class="text-center">Total Due</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%#Eval("RegNo") %></td>
                                <td class="text-center"><%#Eval("NameEng") %></td>
                                <td class="text-center"><%#Eval("ClassName") %></td>
                                <td class="text-center"><%#Eval("Amount") %></td>
                                <td class="text-center"><%#Eval("TotalGiven") %></td>
                                <td class="text-center"><%#Eval("DueAmount") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            <tbody>
                                <td></td>
                                <td></td>
                                <%-- <td></td>
                                <td></td>
                                <td></td>--%>
                                <%--  <td></td>
                                <td></td>--%>
                                <td></td>
                                <td class="text-left">
                                    <asp:Label ID="Label3" runat="server" Text="Total: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="text-left">
                                    <asp:Label ID="Label2" runat="server" Text="Total Recevied: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblReceived" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="text-left">
                                    <asp:Label ID="Label5" runat="server" Text="Total Due: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblDue" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                </td>
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
    <div class="row" id="divPaymentHistory" runat="server">
        <div class="col-sm-12">
            <div>
                <div class="panel-heading">
                    <asp:Label ID="Label6" runat="server" Text="Month wise Dues List"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="Repeaterall" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th class="text-center">Month</th>
                                        <th class="text-center">Campus</th>
                                        <th class="text-center">Medium</th>
                                        <th class="text-center">Class</th>
                                        <th class="text-center">Payment</th>
                                        <th class="text-center">Total Amount</th>
                                        <th class="text-center">Total Given</th>
                                        <th class="text-center">Total Due</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="text-center" style="display: none"><%#Eval("Year") %></td>
                                <td class="text-center" style="display: none"><%#Eval("MonthId") %></td>
                                <td class="text-center"><%#Eval("Month") %></td>
                                <td class="text-center"><%#Eval("CampusName") %></td>
                                <td class="text-center"><%#Eval("MediumName") %></td>
                                <td class="text-center"><%#Eval("ClassName") %></td>
                                <td class="text-center"><%#Eval("PaymentType") %></td>
                                <td class="text-center"><%#Eval("Amount") %></td>
                                <td class="text-center"><%#Eval("TotalGiven") %></td>
                                <td class="text-center"><%#Eval("DueAmonunt") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            <tbody>
                                <td></td>
                                <td></td>

                                <%--<td></td>
                                <td></td>--%>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td class="text-left">
                                    <asp:Label ID="Label3" runat="server" Text="Total: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="text-left">
                                    <asp:Label ID="Label2" runat="server" Text="Total Recevied: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblReceived" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="text-left">
                                    <asp:Label ID="Label5" runat="server" Text="Total Due: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblDue" runat="server" ClientIDMode="Static" Text="0.00" Font-Bold="true"></asp:Label>
                                </td>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnMonthReport" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:Application,GenerateReport %>" OnClick="btnMonthReport_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
       
       function pageLoad() {
            var checked_radio = $("[id*=rdList] input:checked");
            var value = checked_radio.val();

            if (value == 1) {
                $("#<%= divCriteria1.ClientID %>").hide();
                $("#<%= divPaymentType.ClientID %>").slideDown();
            }
            else {
                $("#<%= divCriteria1.ClientID %>").slideDown();
                $("#<%= divPaymentType.ClientID %>").hide();

            }


            $("[id*=rdList]").on('change', function () {
                var checked_radio = $("[id*=rdList] input:checked");
                var value = checked_radio.val();

                if (value == 1) {
                    $("#<%= divCriteria1.ClientID %>").hide();
                    $("#<%= divPaymentType.ClientID %>").slideDown();
                }
                else {
                    $("#<%= divCriteria1.ClientID %>").slideDown();
                    $("#<%= divPaymentType.ClientID %>").hide();
                }

            });
        };
    
    </script>
</asp:Content>

