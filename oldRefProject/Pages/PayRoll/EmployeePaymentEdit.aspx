<%@ Page Title="EmployeePaymentEdit" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeePaymentEdit.aspx.cs" Inherits="Pages_PayRoll_EmployeePaymentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="criteria">
        <div class="panel-heading">
            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="Grade"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Type" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="Level"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlLevel" runat="server" DataTextField="LevelName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblEmployee" runat="server" Text="EmployeeID"></asp:Label>
                        </label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlEmployee" runat="server" DataTextField="EmployeeId" DataValueField="EmployeeId" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:Panel ClientIDMode="Static" ID="pnlAssignEmployee" runat="server">
                <div class="panel-heading">
                    <asp:Label ID="Label7" runat="server" Text="Employee List"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptEmployee" runat="server">
                                <HeaderTemplate>
                                    <table id="example5" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <%-- <asp:CheckBox ID="chkHeaderEmp" runat="server" />--%>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblName" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                                </th>

                                                <th>
                                                    <asp:Label ID="lblEmployee" runat="server" Text="EmployeeId"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblMobile" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkrowEmp" runat="server" /></td>
                                        <td><%#Eval("NameEng") %>
                                            <asp:HiddenField ID="hdnPersonId" Value='<%#Eval("PersonID") %>' runat="server" />
                                            <asp:HiddenField ID="hdnEmpId" Value='<%#Eval("EmployeeId") %>' runat="server" />
                                        </td>
                                        <td>
                                            <%#Eval("EmployeeId")%>
                                        </td>
                                        <td>
                                            <%#Eval("Mobile") %>                                              
                                        </td>
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

            </asp:Panel>
        </div>

    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:Panel ClientIDMode="Static" ID="pnlEmpPayment" runat="server">
                <div class="panel-heading">
                    <asp:Label runat="server" ID="lblPayment" Text="Employee Payment"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="box">
                        <div class="box-body">
                            <style>
                                #example8 input[type='text'] {
                                    max-width: 9em;
                                    text-align: center;
                                }
                            </style>
                            <asp:Repeater ID="rptEmployeePayment" runat="server" OnItemDataBound="rptMonthlyPayment_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="example8" class="table table-bordered table-hover">
                                        <thead>
                                            <tr style="color: darkmagenta; font: bold 16px arial,verdana; text-align: center">
                                                <th>
                                                    <asp:CheckBox ID="chkHeader" runat="server" />
                                                </th>
                                                <th class="text-center" id="thHeader" runat="server">
                                                    <asp:Label ID="lblHeader" runat="server" Text="Month"></asp:Label>
                                                </th>
                                                <th class="text-center" id="thHeader1" runat="server">
                                                    <asp:Label ID="lblHeader1" runat="server"></asp:Label>
                                                </th>

                                                <th class="text-center" id="thHeader2" runat="server">
                                                    <asp:Label ID="lblHeader2" runat="server"></asp:Label>
                                                </th>
                                                <th class="text-center" id="thHeader3" runat="server">
                                                    <asp:Label ID="lblHeader3" runat="server"></asp:Label>
                                                </th>
                                                <th class="text-center" id="thHeader4" runat="server">
                                                    <asp:Label ID="lblHeader4" runat="server"></asp:Label>
                                                </th>
                                                <th class="text-center" id="thHeader5" runat="server">
                                                    <asp:Label ID="lblHeader5" runat="server"></asp:Label>
                                                </th>
                                               <%-- <th class="text-center" id="tdHeader6" runat="server">
                                                    <asp:Label ID="lblHeader6" runat="server"></asp:Label>
                                                </th>--%>
                                          
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="sRow" style="color: black; font: bold 13px arial,verdana;">
                                        <td>
                                            <asp:CheckBox ID="chkrowP" CssClass=".chekR" runat="server" data-monthyear='<%#Eval("MonthYear")%>' />                             
                                        </td>
                                        <td id="tdItem" runat="server">
                                            <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnGradeId" runat="server" Value='<%#Eval("GradeId") %>'/>
                                            <asp:HiddenField ID="hdnLevelId" runat="server" Value='<%#Eval("LevelId") %>'/>
                                            <asp:HiddenField ID="hdnMonthId" runat="server" Value='<%#Eval("MonthId") %>' />
                                        </td>
                                        <td id="tdItem1" runat="server">
                                            <asp:TextBox ID="tbxItem1" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBox1" runat="server"
                                                Enabled="True" TargetControlID="tbxItem1" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblItem1" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblUpdateId1" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblPayrollToDesignationId1" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td id="tdItem2" runat="server">
                                            <asp:TextBox ID="tbxItem2" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                Enabled="True" TargetControlID="tbxItem2" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblItem2" runat="server" Visible="false"></asp:Label>
                                              <asp:Label ID="lblUpdateId2" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblPayrollToDesignationId2" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td id="tdItem3" runat="server">
                                            <asp:TextBox ID="tbxItem3" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                Enabled="True" TargetControlID="tbxItem3" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblItem3" runat="server" Visible="false"></asp:Label>
                                              <asp:Label ID="lblUpdateId3" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblPayrollToDesignationId3" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td id="tdItem4" runat="server">
                                            <asp:TextBox ID="tbxItem4" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                Enabled="True" TargetControlID="tbxItem4" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblItem4" runat="server" Visible="false"></asp:Label>
                                               <asp:Label ID="lblUpdateId4" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblPayrollToDesignationId4" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td id="tdItem5" runat="server">
                                            <asp:TextBox ID="tbxItem5" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                Enabled="True" TargetControlID="tbxItem5" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblItem5" runat="server" Visible="false"></asp:Label>
                                             <asp:Label ID="lblUpdateId5" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblPayrollToDesignationId5" runat="server" Visible="false"></asp:Label>
                                        </td>
                                     <%--   <td id="tdItem6" runat="server">
                                            <asp:TextBox ID="tbxItem6" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                Enabled="True" TargetControlID="tbxItem6" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            <asp:Label ID="lblItem6" runat="server" Visible="false"></asp:Label>

                                        </td>--%>
                                        <td style="display: none">
                                        <asp:TextBox ID="txtPayrollToDesignationId" Text='<%#Eval("PayrollToDesignationId") %>' runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            Enabled="True" TargetControlID="txtPayrollToDesignationId" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblPayrollTypeId" runat="server" Text='<%#Eval("PayrollTypeId") %>'></asp:Label>
                                        <asp:Label ID="lblUpdateId" runat="server" Text='<%#Eval("UpdateId") %>'></asp:Label>
                                    </td>
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
                <div class="panel-footer">
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="Update" OnClientClick="return (Validate());" ValidationGroup="save" />
                </div>
            </asp:Panel>
        </div>
    </div>

    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {

            $("#example8 [id*=chkHeader]").click(function () {
                if ($(this).is(':checked')) {
                    $("#example8 [id*=chkrowP]").prop("checked", true);
                }
                else {
                    $("#example8 [id*=chkrowP]").prop("checked", false);
                }
            });

            $("#example8 [id*=chkrowP]").click(function () {
                if ($("#example8 [id*=chkrowP]").length == $("#example8 [id*=chkrowP]:checked").length) {
                    $("#example8 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example8 [id*=chkHeader]").prop("checked", false);
                }
            });


            $("#example8 input[type='text']").on('input', function () {
                let tb1 = this.parentElement.parentElement.querySelector("input[id*='tbxItem1']");
                let tb2 = this.parentElement.parentElement.querySelector("input[id*='tbxItem2']");
                let tb3 = this.parentElement.parentElement.querySelector("input[id*='tbxItem3']");
                let tb4 = this.parentElement.parentElement.querySelector("input[id*='tbxItem4']");
                let tb5 = this.parentElement.parentElement.querySelector("input[id*='tbxItem5']");
                let tb6 = this.parentElement.parentElement.querySelector("input[id*='tbxItem6']");
                tb6.value = parseFloat(tb1.value) + parseFloat(tb2.value) + parseFloat(tb3.value)
                    + parseFloat(tb4.value) - parseFloat(tb5.value)
            })
        })
    </script>
</asp:Content>


