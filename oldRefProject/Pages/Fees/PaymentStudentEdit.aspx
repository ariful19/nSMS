<%@ Page Title="<%$ Resources:Application,FeesforStudent %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PaymentStudentEdit.aspx.cs" Inherits="Pages_Fees_PaymentStudentEdit" %>

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
                            <asp:Label ID="Label37" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-6">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll%>"></asp:Label>
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo%>"></asp:Label>
                        </label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter Roll No." MaxLength="9" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                            <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" MaxLength="12" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblRemarks" runat="server" Text="<%$ Resources:Application,Remarks %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="tbxRemarks" runat="server" placeholder="Enter Remarks" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                ControlToValidate="tbxRemarks">Enter Remarks</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-5">
            <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                <div>
                    <div class="panel-heading">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="example5" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                    </th>
                                                    <th id="thRoll" runat="server">
                                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,RollNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblName" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                                    </th>

                                                    <th>
                                                        <asp:Label ID="lblMobile" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkrow" runat="server" /></td>
                                            <td id="tdRoll" runat="server">
                                                <%#Eval("RollNo") %>
                                            </td>
                                            <td><%#Eval("NameEng") %>
                                                <asp:HiddenField ID="hdnPersonId" Value='<%#Eval("PersonID") %>' runat="server" />
                                            </td>
                                            <td>
                                                <%#Eval("RegNo") %>
                                                <asp:HiddenField ID="hdnStudentSId" Value='<%#Eval("StudentToClassId") %>' runat="server" />
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
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNew" runat="server">
                <asp:Repeater ID="rptPaymentType" runat="server">
                    <HeaderTemplate>
                        <asp:Label ID="lblHdr" runat="server" Text="Academic Payment" BorderStyle="Groove" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true"></asp:Label>
                        <table id="example12" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,PaymentType %>"></asp:Label></th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("PaymentType") %>
                                <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="tbxPaymentAmount" Text='<%#Eval("Amount") %>' runat="server" paleholder="Enter Payment Amount" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBox1" runat="server"
                                    Enabled="True" TargetControlID="tbxPaymentAmount" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                            </td>
                            <td style="display: none">
                                <asp:TextBox ID="txtPaymentToClassID" Text='<%#Eval("PaymentToClassID") %>' runat="server" paleholder="Enter Payment Amount" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdnInsertUpdate" runat="server" Value='<%#Eval("StudentToClassId") %>' />
                                <asp:Label ID="lblStartMonth" runat="server" Text='<%#Eval("StartMonth") %>'></asp:Label>
                                <asp:Label ID="lblEndMonth" runat="server" Text='<%#Eval("EndMonth") %>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                                </table>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
            <div class="panel-footer" align="left">
                <%--       <asp:Button ID="btnPayment" runat="server" OnClick="btnPayment_Click" CssClass="btn btn-primary" Text="<%$ Resources:Application,Submit %>" OnClientClick="return (Validate());" />--%>
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnPayment_Click" CssClass="btn btn-primary" Text="Update Academic Fee" OnClientClick="return (Validate());" ValidationGroup="save" />
            </div>
        </div>
        <div class="col-sm-7">
            <div>
                <div class="panel-heading">
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,FeesforStudent %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlEdit" runat="server">
                        <asp:Repeater ID="rptPaymentMonth" runat="server" OnItemDataBound="rptPaymentMonthly_ItemDataBound">
                            <HeaderTemplate>
                                <asp:Label ID="lblHdr" runat="server" Text="Monthly Payment" BorderStyle="Groove" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true"></asp:Label>
                                <table id="example8" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Month %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label6" runat="server" Text="Monthle Fee"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="lblHeader1" runat="server"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="lblHeader2" runat="server"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="lblHeader3" runat="server"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="lblHeader4" runat="server"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="lblHeader5" runat="server"></asp:Label></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnMonthId" runat="server" Value='<%#Eval("MonthId") %>' />
                                    </td>
                                    <%--<td>
                                        <%#Eval("PaymentType") %>
                                    </td>--%>
                                    <td>
                                        <asp:TextBox ID="tbxPaymentAmount" runat="server" Text='<%#Eval("Amount") %>' paleholder="Enter Payment Amount" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBox1" runat="server"
                                            Enabled="True" TargetControlID="tbxPaymentAmount" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td class="text-center">
                                        <asp:TextBox ID="tbxItem1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" TargetControlID="tbxItem1" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblItem1" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUpdateId1" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPaymentToClassId1" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:TextBox ID="tbxItem2" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            Enabled="True" TargetControlID="tbxItem2" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblItem2" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUpdateId2" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPaymentToClassId2" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:TextBox ID="tbxItem3" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            Enabled="True" TargetControlID="tbxItem3" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblItem3" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUpdateId3" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPaymentToClassId3" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:TextBox ID="tbxItem4" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            Enabled="True" TargetControlID="tbxItem4" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblItem4" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUpdateId4" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPaymentToClassId4" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:TextBox ID="tbxItem5" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                            Enabled="True" TargetControlID="tbxItem5" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblItem5" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUpdateId5" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPaymentToClassId5" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkMonthlyFee" runat="server" />
                                    </td>
                                    <td style="display: none">
                                        <asp:TextBox ID="txtPaymentToClassID" Text='<%#Eval("PaymentToClassID") %>' runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            Enabled="True" TargetControlID="txtPaymentToClassID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        <asp:Label ID="lblPaymentTypeId" runat="server" Text='<%#Eval("PaymentTypeId") %>'></asp:Label>
                                        <asp:Label ID="lblUpdateId" runat="server" Text='<%#Eval("UpdateId") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </div>
            </div>
            <div class="panel-footer" align="right">
                <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" CssClass="btn btn-primary" Text="Update Monthly Fee" OnClientClick="return (ValidateMonthlyFee());" />
            </div>
            <%--            <div>

                <div class="panel-body">
                </div>
            </div>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
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
                                        <th class="text-center">Session</th>
                                        <th class="text-center">Month</th>
                                        <th class="text-center">Date</th>
                                        <%--  <th class="text-center">Name</th>--%>
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
                                <%--        <td><%#Eval("NameEng") %></td>--%>
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
    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script>
        $(document).ready(function () {

            //$("#example1 [id*=chkHeader]").click(function () {
            //    if ($(this).is(":checked")) {
            //        $("#example1 [id*=chkrow]").prop("checked", true);
            //    } else {
            //        $("#example1 [id*=chkrow]").prop("checked", false);
            //    }
            //});

            //$("#example1 [id*=chkrow]").click(function () {
            //    if ($("#example1 [id*=chkrow]").length == $("#example1 [id*=chkrow]:checked").length) {
            //        $("#example1 [id*=chkHeader]").prop("checked", true);
            //    } else {
            //        $("#example1 [id*=chkHeader]").prop("checked", false);
            //    }
            //});


            var $allCheckbox = $("#example5 [id*=chkHeader]");
            var $checkboxes = $("#example5 [id*=chkrow]");
            var $allCheckbox1 = $("#example2 [id*=chkHeader]");
            var $checkboxes1 = $("#example2 [id*=chkrow]");
            var $allCheckbox2 = $("#example3 [id*=chkHeader]");
            var $checkboxes2 = $("#example3 [id*=chkrow]");
            $allCheckbox.change(function () {
                if ($allCheckbox.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox.removeAttr('checked');
                }
                else {
                    $allCheckbox.attr('checked', 'checked');
                }
            });
            $allCheckbox1.change(function () {
                if ($allCheckbox1.is(':checked')) {
                    $checkboxes1.attr('checked', 'checked');
                }
                else {
                    $checkboxes1.removeAttr('checked');
                }
            });
            $checkboxes1.change(function () {
                if ($checkboxes1.not(':checked').length) {
                    $allCheckbox1.removeAttr('checked');
                }
                else {
                    $allCheckbox1.attr('checked', 'checked');
                }
            });
            $allCheckbox2.change(function () {
                if ($allCheckbox2.is(':checked')) {
                    $checkboxes2.attr('checked', 'checked');
                }
                else {
                    $checkboxes2.removeAttr('checked');
                }
            });
            $checkboxes2.change(function () {
                if ($checkboxes2.not(':checked').length) {
                    $allCheckbox2.removeAttr('checked');
                }
                else {
                    $allCheckbox2.attr('checked', 'checked');
                }
            });
            $("#example5").DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": true
            });
        });
    </script>
    <script type="text/javascript">
        $("#example8").DataTable({
            "paging": true,
            "lengthChange": false,
            "searching": false,
            "ordering": false,
            "info": false,
            "autoWidth": true
        });
        function Validate() {
            //Using Jquery
            var chkboxrowcount = $("#example5 input[id*='chkrow']:checkbox:checked").size();
            if (chkboxrowcount == 0) {
                alert("Please select Student First.");
                return false;
            }
            else if (chkboxrowcount == 1) {

                return confirm("Are you sure you want to Change Payment Fee for Slected Student?");
            }
            else {
                return confirm("Are you sure you want to Transaction for Slected Students?");
            }
        }
        function ValidateMonthlyFee() {
            //Using Jquery            
            var chkboxrowcount = $("#example5 input[id*='chkrow']:checkbox:checked").size();
            var chkboxrowcount1 = $("#example8 input[id*='chkMonthlyFee']:checkbox:checked").size();
            if (chkboxrowcount == 0) {
                alert("Please select Student First.");
                return false;
            }
            if (chkboxrowcount1 == 0) {
                alert("Please select Month First.");
                return false;
            }
            else if (chkboxrowcount1 == 1) {

                return confirm("Are you sure you want to Change Payment Fee for Slected Month?");
            }
            else {
                return confirm("Are you sure you want to Transaction for Slected Months?");
            }
        }

        function SaveConfirmation() {
            return confirm("Are you sure you want to Save Class Routine?");
        }

        function ShowDialogBox(title, content, btn1text, btn2text, functionText, parameterList) {
            var btn1css;
            var btn2css;

            if (btn1text == '') {
                btn1css = "hidecss";
            } else {
                btn1css = "showcss";
            }

            if (btn2text == '') {
                btn2css = "hidecss";
            } else {
                btn2css = "showcss";
            }
            $("#lblMessage").html(content);

            $("#dialog").dialog({
                resizable: false,
                title: title,
                modal: true,
                width: '400px',
                height: 'auto',
                bgiframe: false,
                hide: { effect: 'scale', duration: 400 },

                buttons: [
                                {
                                    text: btn1text,
                                    "class": btn1css,
                                    click: function () {

                                        $("#dialog").dialog('close');

                                    }
                                },
                                {
                                    text: btn2text,
                                    "class": btn2css,
                                    click: function () {
                                        $("#dialog").dialog('close');
                                    }
                                }
                ]
            });
        }
    </script>
</asp:Content>

