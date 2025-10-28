<%@ Page Title="<%$ Resources:Application,StudentPayment %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PaymentReceivedBafa.aspx.cs" Inherits="Pages_Fees_PaymentReceivedBafa" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SchoolHeader.ascx" TagPrefix="uc" TagName="SchoolHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <div id="criteria">
        <div class="panel-heading">
            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="col-lg-5 col-md-5">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Month %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control dropdown">
                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label34" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo%>"></asp:Label>
                        </label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" MaxLength="10" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                        </label>
                        <div class="col-sm-8">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" ValidationGroup="Save" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-7 col-md-7">
                <div class="form-horizontal">

                    <asp:GridView ID="gvSubject" runat="server" CssClass="table" ClientIDMode="Static" Font-Names="Arial" OnRowDataBound="RowDataBound" Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B" AutoGenerateColumns="false" OnRowCommand="gvSubject_RowCommand" Caption="Class List" CaptionAlign="Top" HeaderStyle-ForeColor="PaleVioletRed" HeaderStyle-BackColor="#ffffcc">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,SLNo %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,Class %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <%# Eval("ClassName") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,TotalDue %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <%# Eval("DueAmount") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,Action %>" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/Images/Common/paynow.jpg" OnClientClick="javascript:return confirm('Are you sure you want to Pay Now?');" Text="Find" runat="server" CommandName="Find" CommandArgument='<%# Eval("ClassId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>
    <div class="row" id="divStudentPayment" runat="server">
        <div class="col-sm-8">
            <div class="row">
                <div class="col-sm-8">
                    <div>
                        <div class="panel-heading">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,StudentInformation %>"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Panel ID="pnlStudentInfo" runat="server">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <asp:Image ID="imgPerson" runat="server" Height="80" Width="80" />
                                    </div>
                                    <div class="col-sm-9">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblNameEng" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblNameBan" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top: 10px;">
                                    <div class="col-sm-5">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblYear" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblCampusName" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblClass" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblGroup" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label33" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblShift" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label36" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblSection" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <%-- <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="LabelRollNo" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblRoll" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,FatherName %>"></asp:Label></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblFName" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,MotherName %>"></asp:Label></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblMName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label>:</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label>:</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label41" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>:</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div>
                        <div class="panel-heading">
                            <asp:Label ID="Label25" runat="server" Text="Monthly Payment"></asp:Label>
                        </div>
                        <asp:Repeater ID="rptPaymentMonth" runat="server" OnItemDataBound="rptPaymentMonthly_ItemDataBound">
                            <HeaderTemplate>
                                <asp:Label ID="lblHdr" runat="server" Text="Monthly Payment" BackColor="#ffffcc" ForeColor="#006666" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <table id="example8" class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="color: darkmagenta; font: bold 16px arial,verdana;">
                                            <th class="text-left">
                                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Month %>"></asp:Label></th>
                                            <th class="text-center">Amount</th>
                                            <th class="text-center">Paid</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="color: black; font: bold 13px arial,verdana;">
                                    <td class="text-left">
                                        <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear") %>'></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>' Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hdnMonthId" runat="server" Value='<%#Eval("MonthId") %>' />

                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="hdnPaymentTypeId" runat="server" Text='<%#Eval("PaymentTypeId") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:CheckBox ID="chkMonthlyPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chkMonthlyPayment_OnCheckedChanged" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td class="text-left">
                                        <asp:Label ID="Label4" runat="server" ClientIDMode="Static" Text="Monthly Due :" BackColor="#ccffcc" ForeColor="#000099" Font-Bold="true" Font-Size="12"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblMonthlyDue" runat="server" ClientIDMode="Static" Text="0.00" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                    </td>
                                </tr>
                                </tbody>
                                                        </table>                                                        
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            </div>
        </div>
        <div class="col-sm-4 form">
            <div class=''>
                <div class="panel-heading" id="hideheading">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,StudentPayment %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div id="dvContents" class="mainbody">
                        <asp:Panel ID="pnlPdf" runat="server">
                            <div id="info">
                                <uc:SchoolHeader runat="server" ID="SchoolHeader" />
                                <table width="100%" cellspacing="10" cellpadding="10">
                                    <tr height="10px">
                                        <td>
                                            <asp:Label ID="Label35" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="printName" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="printReg" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="pt-10">
                                        <td>
                                            <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="PrintClass" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="printGroup" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="pt-10">
                                        <td>
                                            <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="printShift" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="printSection" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlRpt" runat="server">
                            <div class="row">

                                <div class="col-sm-12" id="rptDiv">
                                    <asp:Repeater ID="rptPreviousDue" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHdr" runat="server" Text="Previous Month's Due List" BorderStyle="Groove" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true"></asp:Label>
                                            <table id="example2" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center">Month</th>
                                                        <%-- <th class="text-center">Amount</th>--%>
                                                        <th class="text-center">Due Amount</th>
                                                        <th class="text-center">Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("Month") %></td>
                                                <%-- <td class="text-right"><%#Eval("Total") %> </td>--%>
                                                <td><%#Eval("Due") %></td>
                                                <td class="text-right"><%#Eval("Status") %> </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td></td>
                                                <td class="text-left">
                                                    <asp:Label ID="Label3" runat="server" Text="Total Due : " ForeColor="#ff0000" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblTotal" runat="server" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true">0</asp:Label></td>
                                            </tr>
                                            </tbody>
                                                        </table>                                                        
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <div class="col-sm-4" style="display: none">
                                        <asp:Label ID="lblPreMonth" runat="server"></asp:Label>
                                        <asp:Label ID="lblPreDue" runat="server"></asp:Label>
                                    </div>

                                </div>
                                <div class="col-sm-12">
                                    <asp:Repeater ID="rptPaymentOthers" runat="server" OnItemDataBound="rptPaymentOthers_ItemDataBound">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHdr" runat="server" Text="Academic Payment" BorderColor="#6600ff" BorderWidth="2px" BackColor="#ffffcc" ForeColor="#006666" Font-Bold="true" Font-Size="Large"></asp:Label>
                                            <table id="example11" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="color: darkmagenta; font: bold 16px arial,verdana;">
                                                        <th class="text-left">Payment Type</th>
                                                        <th class="text-center">Amount</th>
                                                        <th class="text-center">Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="color: black; font: bold 13px arial,verdana;">
                                                <td class="text-left"><%#Eval("PaymentType") %></td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblAmount" runat="server" ClientIDMode="Static" Text='<%#Eval("Amount") %>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                    <asp:Label ID="lblDues" runat="server" Text='<%#Eval("Dues") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblPaymentId" runat="server" Text='<%#Eval("PaymentTypeId") %>' Visible="false"></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:CheckBox ID="chkOtherPayment" runat="server" OnCheckedChanged="chkOtherPayment_OnCheckedChanged" AutoPostBack="true"/>
                                                    <asp:Label ID="hdnPaymentTypeId" runat="server" ClientIDMode="Static" Text='<%#Eval("PaymentTypeId") %>' Visible="false"></asp:Label>
                                                    
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:Label ID="Label4" runat="server" ClientIDMode="Static" Text="Academic Due : " BackColor="#ccffcc" ForeColor="#000099" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    <asp:Label ID="lblPreviousDue" runat="server" ClientIDMode="Static" Text="0.00" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true" Font-Size="Medium"></asp:Label></td>
                                                <td class="text-right">
                                                    <asp:Label ID="Label3" runat="server" Text="Total : " BackColor="#ccffcc" ForeColor="#000099" Font-Bold="true" Font-Size="Larger"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static" Text="0.00" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true" Font-Size="Larger"></asp:Label></td>
                                            </tr>
                                            </tbody>
                                                        </table>                                                        
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="display: none">
                                    Due
                                </div>
                                <div class="col-sm-2 text-right" style="display: none">
                                    <asp:Label ID="lblPreviousDue" runat="server" ClientIDMode="Static" Text="0.00"></asp:Label>
                                </div>
                                <div class="col-sm-2" style="display: none">
                                    Total
                                </div>
                                <div class="col-sm-4 text-right" style="display: none">
                                    <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static" Text="0.00"></asp:Label>
                                </div>
                                <div class="col-sm-4" style="display: none">
                                    <a data-toggle="modal" href="#" data-target="#latefee">Late Fee</a>
                                </div>
                                <div class="col-sm-2 text-right" style="display: none">
                                    <asp:Label ID="lblFine" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                </div>
                                <div class="col-sm-3" style="display: none">
                                    <a title="Add new" data-toggle="modal" href="#" data-target="#scholar">Scholar</a>
                                </div>
                                <div class="col-sm-3 text-right" style="display: none">
                                    <asp:Label ID="lblScholar" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                </div>
                            </div>
                            <div>
                                <hr />
                            </div>
                            <div class="row pt-10">
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    Grand Total
                                </div>
                                <div class="col-sm-6 text-right">
                                    <asp:Label ID="lblGrandTotal" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div id="editor"></div>
                <div class="panel panel-footer" id="btnhide">
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:Button ID="btnPdf" OnClick="btnPdf_Click" ClientIDMode="Static" runat="server" Text="Generate PDF" class="btn btn-block btn-flat btn-info" />
                            <%-- <div class="btn btn-block btn-flat btn-info" id="btnGeneratePDF">Generate PDF</div>--%>
                        </div>
                        <div class="col-sm-6">
                            <button type="button" class="btn btn-block btn-flat btn-success" data-toggle="modal" onclick="SaveConfirmation();">Payment</button>
                            <%--  data-toggle="modal" data-target="#payment"--%>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-6">
                            <asp:Button ID="btnPrintBill" runat="server" Text="Print Bill" class="btn btn-block btn-flat btn-warning" OnClick="btnPdf_Click" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="Button4" runat="server" Text="Cancle" class="btn btn-block btn-flat btn-danger" OnClick="btnCancle_Click" />
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <div class="row">
        <div id="payment" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-info">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Payment</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-9">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>Previous Due</label>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:Label ID="popPreviousDue" runat="server" ClientIDMode="Static" Text="0.00"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <label>Total</label>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <asp:Label ID="popTotal" runat="server" ClientIDMode="Static" Text="0.00"></asp:Label>
                                    </div>
                                </div>
                                <div class="row pt-10">
                                    <div class="col-sm-4">
                                        <label>Late Fee</label>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:Label ID="popLateFee" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label>Scholar</label>
                                    </div>
                                    <div class="col-sm-3 text-right">
                                        <asp:Label ID="popScholar" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div>
                                    <hr />
                                </div>
                                <div class="row height">
                                    <div class="col-sm-6">
                                        <label>Bill No</label>
                                    </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:TextBox ID="tbxBillNo" runat="server" ClientIDMode="Static" placeholder="Bill No"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row height">
                                    <div class="col-sm-6">
                                        <label>Grand Total</label>
                                    </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="popGrandTotal" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div class="row height">
                                    <div class="col-sm-6">
                                        <label>Total Paying</label>
                                    </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="popPaying" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div class="divider">
                                    <hr />
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="inputEmail3">
                                                <asp:Label ID="Label20" runat="server" Text="Amount"></asp:Label></label>
                                            <asp:TextBox ID="tbxAmount" runat="server" placeholder="Amount" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldtbxAmountValidator" runat="server" ValidationGroup="save"
                                                ErrorMessage="Enter Amount First..." ControlToValidate="tbxAmount">Enter Amount</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="inputEmail3">
                                                <asp:Label ID="Label26" runat="server" Text="Mode of Payment"></asp:Label></label>
                                            <asp:DropDownList ID="ddlPaymentMode" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="1" Text="Cash"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Credit Card"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Cheque"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-10 col-sm-10 col-xs-10">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payment" />
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </div>
                                    </div>


                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label>BDT Notes</label>
                                <div class="btn-group btn-group-vertical" style="width: 100%;">
                                    <button type="button" class="btn btn-info btn-block quick-cash" id="quick-payable">
                                        0.00
                                    </button>
                                    <button type="button" class="btn btn-block btn-flat btn-warning quick-cash">5</button>
                                    <button type="button" class="btn btn-block btn-flat btn-warning quick-cash">10</button>
                                    <button type="button" class="btn btn-block btn-flat btn-warning quick-cash">20</button>
                                    <button type="button" class="btn btn-block btn-flat btn-warning quick-cash">50</button>
                                    <button type="button" class="btn btn-block btn-flat btn-warning quick-cash">100</button>
                                    <button type="button" class="btn btn-block btn-flat btn-warning quick-cash">500</button>
                                    <button type="button" class="btn btn-block btn-flat btn-warning quick-cash">1000</button>
                                    <button type="button" class="btn btn-block btn-danger"
                                        id="clear-cash-notes">
                                        Clear</button>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-flat btn-info" OnClick="btnSubmit_Click" ValidationGroup="payment" />
                        <button type="button" class="btn btn-flat btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
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
                                        <th class="text-center">BillNo</th>
                                        <th class="text-center">Year</th>
                                        <th class="text-center">Month</th>
                                        <th class="text-center">Date</th>
                                        <th class="text-center">Payment</th>
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
                                <td><%#Eval("PaymentType") %></td>
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
        var amount = 0;
        $(document).on('click', '.quick-cash', function () {
            var $quick_cash = $(this);
            var amt = $quick_cash.contents().filter(function () {
                return this.nodeType == 3;
            }).text();
            var $pi = $("#tbxAmount");
            var gr = Number($("#popGrandTotal").html());
            amount = Number(amount) + Number(amt);
            if (gr >= amount) {
                $pi.val(amount);
                $("#popPaying").text(amount);


                var note_count = $quick_cash.find('span');
                if (note_count.length == 0) {
                    $quick_cash.append('<span class="badge">1</span>');
                } else {
                    note_count.text(parseInt(note_count.text()) + 1);
                }
            }
            else
                alert('Please select equal or less than from grand total.');
        });
        $(document).on('click', '#clear-cash-notes', function () {
            $('.quick-cash').find('.badge').remove();
            $("#tbxAmount").val('').change().focus();
            $("#popPaying").text('');
            amount = 0;
        });


        function ShowPaymentModal() {
            if ($('#cphMain_tbxRegNo').val() == "") {
                alert("You need to enter registration no.");
            }
            else {

                $('#payment').show();
            }
        }

        function SaveConfirmation() {
            var due = document.getElementById('<%=lblPreDue.ClientID%>').innerText;
            var month = document.getElementById('<%=lblPreMonth.ClientID%>').innerText;
            var currentDue = document.getElementById('<%=lblPreviousDue.ClientID%>').innerText;
            var selectedDue = Number($("#popGrandTotal").html());
            if (selectedDue == 0) {
                alert("No Due selected.");
                return false;
            }
            if (due.length > 0) {
                alert("Please Pay " + month + "'s Due first!!!");
                return false;
            }
                //else if (currentDue == "0.00") {
                //    alert("No Due for this Month!!!");
                //    return false;
                //}
            else {
                $('#payment').modal();
            }

        }
        document.getElementById('tbxDate').value = Today();

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

        $("#example1 [id*=chkOtherPayment]").click(function () {
            if ($("#example1 [id*=chkOtherPayment]").length == $("#example1 [id*=chkrow]:checked").length) {
                $("#example1 [id*=chkHeader]").prop("checked", true);
            } else {
                $("#example1 [id*=chkHeader]").prop("checked", false);
            }
        });
    </script>
    <script type="text/javascript">

        function MouseEvents(objRef, evt) {

            var checkbox = objRef.getElementsByTagName("input")[0];

            if (evt.type == "mouseover") {

                objRef.style.backgroundColor = "orange";

            }

            else {

                if (checkbox.checked) {

                    objRef.style.backgroundColor = "aqua";

                }

                else if (evt.type == "mouseout") {

                    if (objRef.rowIndex % 2 == 0) {

                        //Alternating Row Color

                        objRef.style.backgroundColor = "#C2D69B";

                    }

                    else {

                        objRef.style.backgroundColor = "white";

                    }

                }

            }

        }

    </script>
</asp:Content>
