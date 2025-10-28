<%@ Page Title="<%$ Resources:Application,StudentPayment %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PaymentReceived.aspx.cs" Inherits="Pages_Fees_PaymentReceived" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SchoolHeader.ascx" TagPrefix="uc" TagName="SchoolHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <div runat="server" id="criteria">
        <div class="panel-heading">
            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label37" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
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
                    <%--   <div class="form-group">
                        <div class="col-sm-6">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" OnClick="btnSearch_Click"/>
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label31" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll%>"></asp:Label>
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo%>"></asp:Label>
                        </label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlRollNo" runat="server" DataTextField="RollNo" DataValueField="PersonId" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlRollNo_SelectedIndexChanged"></asp:DropDownList>
                            <asp:DropDownList ID="ddlRegNo" runat="server" DataTextField="RegNo" DataValueField="PersonId" CssClass="form-control dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlRegNo_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="divStudentPayment" runat="server">
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-12">
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
                                                <label for="inputEmail3" class="col-sm-2">
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label></label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblNameEng" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2">
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
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label25" runat="server" Text="Studentship"></asp:Label>:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
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
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Remarks %>"></asp:Label>:</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="col-sm-12" id="divMonthlyPayment" runat="server">
            <div>
                <div class="panel-heading">
                    <%--<asp:Label ID="Label25" runat="server" Text="Monthly Payment"></asp:Label>--%>
                </div>
                <div class="panel-body">
                    <div class="col-sm-6">
                        <asp:Repeater ID="rptPaymentMonth" runat="server">
                            <HeaderTemplate>
                                <asp:Label ID="lblHdr" runat="server" Text="Monthly Fees" BackColor="#ffffcc" ForeColor="#006666" Font-Bold="true" Font-Size="Large" BorderColor="#6600ff" BorderWidth="2px"></asp:Label>
                                <table id="example8" class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="color: darkmagenta; font: bold 16px arial,verdana;">
                                            <th class="text-left">
                                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Month %>"></asp:Label></th>
                                            <th class="text-center">Amount</th>
                                            <th class="text-center">Due</th>
                                            <th class="text-center">Paid</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="color: black; font: bold 13px arial,verdana;">
                                    <td class="text-left">
                                        <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear") %>'></asp:Label>
                                        <asp:Label ID="lblPaymentType" runat="server" Text='<%#Eval("PaymentType") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>' Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hdnMonthId" runat="server" Value='<%#Eval("MonthId") %>' />

                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>' CssClass="form-control"></asp:Label>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="hdnPaymentTypeId" runat="server" Text='<%#Eval("PaymentTypeId") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDueAmount" runat="server" CssClass="form-control"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:CheckBox ID="chkMonthlyPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chkOtherPayment_OnCheckedChanged" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td class="text-left">
                                        <asp:Label ID="Label4" runat="server" ClientIDMode="Static" Text="Monthly Due:" BackColor="#ccffcc" ForeColor="#000099" Font-Bold="true" Font-Size="Larger" Visible="false"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="lblMonthlyDue" runat="server" ClientIDMode="Static" Text="0.00" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true" Font-Size="Larger" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                </tbody>
                                                        </table>                                                        
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="col-sm-6">
                        <asp:Repeater ID="rptPaymentMonthlyOthers" runat="server" OnItemDataBound="rptPaymentMonthly_ItemDataBound">
                            <HeaderTemplate>
                                <asp:Label ID="lblHdr" runat="server" Text="Monthly Others Payment" BackColor="#ffffcc" ForeColor="#006666" Font-Bold="true" Font-Size="Large" BorderColor="#6600ff" BorderWidth="2px"></asp:Label>
                                <table id="example8" class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="color: darkmagenta; font: bold 16px arial,verdana;">
                                            <th class="text-center" id="tdHeader1" runat="server">
                                                <asp:Label ID="lblHeadr1" runat="server"></asp:Label>
                                            </th>
                                            <th class="text-center" id="tdHeader2" runat="server">
                                                <asp:Label ID="lblHeadr2" runat="server"></asp:Label>
                                            </th>
                                            <th class="text-center" id="tdHeader3" runat="server">
                                                <asp:Label ID="lblHeadr3" runat="server"></asp:Label>
                                            </th>
                                            <th class="text-center" id="tdHeader4" runat="server">
                                                <asp:Label ID="lblHeadr4" runat="server"></asp:Label>
                                            </th>
                                            <th class="text-center" id="tdHeader5" runat="server">
                                                <asp:Label ID="lblHeadr5" runat="server"></asp:Label>
                                            </th>
                                            <th class="text-center">Due</th>
                                            <th class="text-center">Paid</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="color: black; font: bold 13px arial,verdana;">
                                    <td class="text-center" id="tdItem1" runat="server">
                                        <asp:TextBox ID="tbxItem1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:Label ID="lblItem1" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>' Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hdnMonthId" runat="server" Value='<%#Eval("MonthId") %>' />
                                    </td>
                                    <td id="tdItem2" runat="server">
                                        <asp:TextBox ID="tbxItem2" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:Label ID="lblItem2" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td id="tdItem3" runat="server">
                                        <asp:TextBox ID="tbxItem3" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:Label ID="lblItem3" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td id="tdItem4" runat="server">
                                        <asp:TextBox ID="tbxItem4" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:Label ID="lblItem4" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td id="tdItem5" runat="server">
                                        <asp:TextBox ID="tbxItem5" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:Label ID="lblItem5" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDueAmount" runat="server" CssClass="form-control"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:CheckBox ID="chkMonthlyPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chkOtherPayment_OnCheckedChanged" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%-- <tr>
                            <td class="text-left">
                                <asp:Label ID="Label4" runat="server" ClientIDMode="Static" Text="Monthly Due:" BackColor="#ccffcc" ForeColor="#000099" Font-Bold="true" Font-Size="Larger" Visible="false"></asp:Label>
                            </td>
                            <td class="text-center">
                                <asp:Label ID="lblMonthlyDue" runat="server" ClientIDMode="Static" Text="0.00" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true" Font-Size="Larger" Visible="false"></asp:Label>
                            </td>
                        </tr>--%>
                        </tbody>
                                                        </table>                                                        
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-sm-6 form" id="paymentDiv" runat="server">
            <div>
                <div class="panel-heading" id="hideheading">
                    <%--   <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,StudentPayment %>"></asp:Label>--%>
                </div>
                <div class="panel-body">
                    <div id="dvContents" class="mainbody">
                        <asp:Panel ID="pnlRpt" runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Repeater ID="rptPaymentOthers" runat="server" OnItemDataBound="rptPaymentOthers_ItemDataBound">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHdr" runat="server" Text="Academic Payment" BorderColor="#6600ff" BorderWidth="2px" BackColor="#ffffcc" ForeColor="#006666" Font-Bold="true" Font-Size="Large"></asp:Label>
                                            <table id="example11" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="color: darkmagenta; font: bold 16px arial,verdana;">
                                                        <th class="text-left">Payment Type</th>
                                                        <th class="text-center">Amount</th>
                                                        <%--<th class="text-center">Status</th>--%>
                                                        <th class="text-center">
                                                            <asp:CheckBox ID="chkOtherAll" runat="server" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="color: black; font: bold 13px arial,verdana;">
                                                <td class="text-left">
                                                    <asp:Label ID="lblPaymentType" runat="server" ClientIDMode="Static" Text='<%#Eval("PaymentType") %>'></asp:Label>

                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblAmount" runat="server" ClientIDMode="Static" Text='<%#Eval("Amount") %>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:CheckBox ID="chkOtherPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chkOtherPayment_OnCheckedChanged" />
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblDues" runat="server" Text='<%#Eval("Dues") %>' Visible="false"></asp:Label>

                                                    <asp:Label ID="lblPaymentId" runat="server" Text='<%#Eval("PaymentTypeId") %>' Visible="false"></asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:Label ID="Label4" runat="server" ClientIDMode="Static" Text="Academic Due : " BackColor="#ccffcc" ForeColor="#000099" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                                    <asp:Label ID="lblPreviousDue" runat="server" ClientIDMode="Static" Text="0.00" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true" Font-Size="Larger"></asp:Label></td>
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

                            <div class="row pt-10">
                                <div class="col-sm-4" style="display: none">
                                    <asp:Label ID="lblPreMonth" runat="server"></asp:Label>
                                    <asp:Label ID="lblPreDue" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row" style="display: none">
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

            </div>
        </div>
        <div class="col-sm-6 form" id="Div1" runat="server">
            <div class="col-sm-12">
                <asp:Repeater ID="rptReceivedPayment" runat="server">
                    <HeaderTemplate>
                        <asp:Label ID="lblHdr" runat="server" Text="Select Payment" BorderColor="#6600ff" BorderWidth="2px" BackColor="#ffffcc" ForeColor="#006666" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <table id="example11" class="table table-bordered table-hover">
                            <thead>
                                <tr style="color: darkmagenta; font: bold 16px arial,verdana; border: 2px;">
                                    <th class="text-left">Month-Year</th>
                                    <th class="text-center">Payment Type</th>
                                    <th class="text-center">Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="color: black; font: bold 13px arial,verdana;">
                            <td class="text-left">
                                <%#Eval("MonthYear") %>

                            </td>
                            <td class="text-center">
                                <%#Eval("PaymentType") %>
                            </td>
                            <td class="text-center">
                                <%#Eval("TotalDue") %>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td class="text-left"></td>
                            <td class="text-right">
                                <asp:Label ID="Label3" runat="server" Text="Grand Total : " BackColor="#ccffcc" ForeColor="#000099" Font-Bold="true" Font-Size="Larger"></asp:Label>

                            </td>
                            <td>
                                <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static" Text="0.00" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true" Font-Size="Larger"></asp:Label>

                            </td>
                        </tr>
                        </tbody>
                                                        </table>                                                        
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div>
                <hr />
            </div>
            <div id="btnhide">
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
    <div class="row">
        <div class="col-sm-12" id="divPaymentHistory" runat="server">
            <div>
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
                 <div class="panel-footer">
                    <asp:Button ID="btnReport" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:Application,GenerateReport %>" OnClick="btnReport_Click" />
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
</asp:Content>
