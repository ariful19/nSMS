<%@ Page Title="<%$ Resources:Application,JournalEntry %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JournalEntry.aspx.cs" Inherits="Pages_JournalEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <script>
        function btnPost_Click() {
            var strconfirm = confirm("Are you sure you want to Data Post To Ledger ?");
            if (strconfirm == true) {
                return true;
            }
        }

        function showOverlay() {
            var popup = $find('modalPopup');
            popup.show();
        }

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "#C2D69B";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }
        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {

                //If checked change color to Aqua

                row.style.backgroundColor = "aqua";

            }

            else {

                //If not checked change back to original color

                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    row.style.backgroundColor = "#C2D69B";

                }

                else {

                    row.style.backgroundColor = "white";

                }

            }



            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;
        }

    </script>
    



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
                   <%-- <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-1">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label>
                                </label>
                                <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-1">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Month %>"></asp:Label>
                                </label>
                                <asp:DropDownList ID="ddlMonth" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown">
                                    <asp:ListItem Value="0">--Month--</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-1">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label>
                                </label>
                                <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Designation" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-11">
                            <asp:Button ID="btnInsert" runat="server" Text="<%$ Resources:Application,NewInsert %>" CssClass="btn btn-primary" OnClick="btnInsert_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnReset_Click" />
                            <asp:Button ID="btnposttoledger" runat="server" Text="<%$ Resources:Application,PostToLedger %>" CssClass="btn btn-primary" OnClientClick="btnPost_Click();" OnClick="btnposttoledger_Click" />
                            <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" OnClick="btnEdit_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-success" id="journalDiv" runat="server" visible="False">
                <div class="panel-body" style="width: 100%; height: 200px; overflow: scroll">
                    <asp:GridView ID="GvJournal" runat="server"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Acc ID">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hfjrnlid" Value='<%# Eval("JrTranId") %>' />
                                    <asp:TextBox ID="txtaccid" runat="server" Text='<%#Eval("AccountCodeId") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acc Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtaccname" runat="server" Text='<%#Eval("AccountName") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Head Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsubcodeid" runat="server" Text='<%#Eval("HeadCodeId") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purpose">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtpurpose" runat="server" Text='<%#Eval("Purpose") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtdramount" runat="server" Text='<%#Eval("DrAmount") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cr Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcramount" runat="server" Text='<%#Eval("CrAmount") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRNO">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtmrno" runat="server" Text='<%#Eval("MrNo") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcheckno" runat="server" Text='<%#Eval("ChequeNo") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtvoucherno" runat="server" Text='<%#Eval("VNo") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher Type">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtvouchertype" runat="server" Text='<%#Eval("VType") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TR Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txttrdate" runat="server" Text='<%#Eval("TDate","{0:dd/MM/yyyy}") %>' Width="100%"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxOpenDate"
                                        TargetControlID="txttrdate">
                                    </cc1:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtremarks" runat="server" Text='<%#Eval("Remarks") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#1E5A96" ForeColor="White" />
                    </asp:GridView>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-flat btn-success" Text="Add New Row" OnClick="Btnaddnewrow_Click" />
                </div>
            </div>

            <div class="panel panel-success" id="divLgledger" runat="server" visible="True">
                <div class="panel-body">
                    <asp:GridView ID="GvShowJournal" runat="server"
                        AutoGenerateColumns="false" Width="100%">
                       
                        <Columns>
                            
                            <asp:TemplateField HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchk" runat="server" Text="Check All"></asp:Label>
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="  " TextAlign="Left" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="server" onclick="Check_Click(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acc ID">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hfjrnlid" Value='<%# Eval("JrTranId") %>' />
                                    <asp:Label ID="lblAccid" runat="server" Text='<%#Eval("AccountCodeId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Account Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblAId" runat="server" Text='<%#Eval("AccountName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Sub Code ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblJrnlid" runat="server" Text='<%#Eval("HeadCodeId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purpose">
                                <ItemTemplate>
                                    <asp:Label ID="lblPurpose" runat="server" Text='<%#Eval("Purpose") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblDramount" runat="server" Text='<%#Eval("DrAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cr Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblCramount" runat="server" Text='<%#Eval("CrAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TR Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrdate" runat="server" Text='<%#Eval("CrAmount") %>'></asp:Label>                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Application,Action %>">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#1E5A96" ForeColor="White" HorizontalAlign="Center"/>
                    </asp:GridView>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

