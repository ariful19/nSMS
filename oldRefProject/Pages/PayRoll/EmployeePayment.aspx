<%@ Page Title="EmployeePayment" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/AdminMaster.master" CodeFile="EmployeePayment.aspx.cs" Inherits="Pages_PayRoll_EmployeePayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="criteria">
        <div class="panel-heading">
            <asp:Label ID="lblCriteria" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>                
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                       <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="Grade"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Type" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">                  
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="Level"></asp:Label></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlLevel" runat="server" DataTextField="LevelName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
        <div class="panel-footer">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:Panel ClientIDMode="Static" ID="pnlAssignEmployee" runat="server">
              
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
                                            <asp:CheckBox ID="chkrowP" CssClass=".chekR" OnCheckedChanged="checkPayment_OnCheckChanged" AutoPostBack="true" runat="server" data-monthyear='<%#Eval("MonthYear")%>' />                             
                                        </td>
                                        <td id="tdItem" runat="server">
                                            <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear")%>'></asp:Label>
                <%--                            <asp:HiddenField ID="hdnDesignationId" runat="server" Value='<%#Eval("DesignationId") %>' --%>
                                            <asp:HiddenField ID="hdnMonthId" runat="server" Value='<%#Eval("MonthId") %>' />
                                            <asp:HiddenField ID="hdnYearId" runat="server" Value='<%#Eval("Year") %>' />

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
<%--                                        <asp:TextBox ID="txtPayrollToDesignationId" Text='<%#Eval("PayrollToDesignationId") %>' runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            Enabled="True" TargetControlID="txtPayrollToDesignationId" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>--%>
                                        <asp:Label ID="lblPayrollTypeId" runat="server" Text='<%#Eval("PayrollTypeId") %>'></asp:Label>
                                       <%-- <asp:Label ID="lblUpdateId" runat="server" Text='<%#Eval("UpdateId") %>'></asp:Label>--%>
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
                   <button type="button" name="Payment" class="btn btn-info" runat="server" data-toggle="modal" data-target="#myModal" >Pay</button>

                 <%--  <asp:Button Text="Payment" BackColor="Green" runat="server" OnClick="btnPaid_Click" />--%>
                </div>
                  <div class="row">
        <div id="myModal" class="modal fade" role="dialog">
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
                                    <div class="col-sm-2">
                                        <label>Basic</label>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <asp:Label ID="popBasic" runat="server" ClientIDMode="Static" Text="0.00"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <label>Allowance</label>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <asp:Label ID="popAllowance" runat="server" ClientIDMode="Static" Text="0.00"></asp:Label>
                                    </div>
                                </div>
                                <div class="row pt-10">
                                    <div class="col-sm-4">
                                        <label>Bonus</label>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:Label ID="popBonus" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>Increment</label>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:Label ID="popIncrement" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>Penalty</label>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:Label ID="popPenalty" runat="server" Text="0.00" ClientIDMode="Static"></asp:Label>
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
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
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
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-flat btn-info" OnClick="btnPaid_Click" ValidationGroup="payment" />
                        <button type="button" class="btn btn-flat btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
            </asp:Panel>
        </div>
    </div>
    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
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

  </script>
</asp:Content>

