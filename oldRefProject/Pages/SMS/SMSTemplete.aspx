<%@ Page Title="<%$ Resources:Application,SMSTemplete %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SMSTemplete.aspx.cs" Inherits="Pages_SMS_SMSTemplete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../Styles/jquery.dataTables_themeroller.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
     <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,TemplateName %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="txtTemplateName" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTemplateName" ValidationGroup="save"
                            ErrorMessage="Input Templte Name"></asp:RequiredFieldValidator>
                    </div>
                </div>
                 <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,IsDefault %>"></asp:Label></label>
                    <div class="col-sm-10">
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Variables %>"></asp:Label></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="tbxVariables" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxVariables" ValidationGroup="save"
                            ErrorMessage="Input Variables"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Description %>"></asp:Label></label>
                    <div class="col-sm-7">
                        <asp:Label ID="lblcharecter" runat="server" ClientIDMode="Static"></asp:Label> 
                          <asp:TextBox runat="server" ID="tbxDetails" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>


                        <cc1:HtmlEditorExtender ID="htmlEditorExtender1" TargetControlID="tbxDetails" runat="server" >
                             <Toolbar>
            <cc1:Undo />
                <cc1:Redo />
                <cc1:Bold />
                <cc1:Italic />
                <cc1:Underline />
                <cc1:StrikeThrough />
                <cc1:Subscript />
                <cc1:Superscript />
                <cc1:JustifyLeft />
                <cc1:JustifyCenter />
                <cc1:JustifyRight />
                <cc1:JustifyFull />
                <cc1:InsertOrderedList />
                <cc1:InsertUnorderedList />
                <cc1:CreateLink />
                <cc1:UnLink />
                <cc1:RemoveFormat />
                <cc1:SelectAll />
                <cc1:UnSelect />
                <cc1:Delete />
                <cc1:Cut />
                <cc1:Copy />
                <cc1:Paste />
                <cc1:BackgroundColorSelector />
                <cc1:ForeColorSelector />
                <cc1:FontNameSelector />
                <cc1:FontSizeSelector />
                <cc1:Indent />
                <cc1:Outdent />
                <cc1:InsertHorizontalRule />
                <cc1:HorizontalSeparator />
               
        </Toolbar>   
                            </cc1:HtmlEditorExtender>

                        <asp:TextBox ID="tbxchar" ClientIDMode="Static" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-sm-10">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:Application,Save %>" OnClick="btnSave_Click" ValidationGroup="save"/>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptSMS" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr><th><asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,TemplateName %>"></asp:Label></th>
                                                    <th><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Variables %>"></asp:Label></th>
                                                    <th><asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,IsDefault %>"></asp:Label></th>
                                                    <th><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Name") %></td>
                                            <td><%#Eval("Variable") %></td>
                                            <td><%#Eval("IsDefault") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                                <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            var text_max = 10;
            $('#lblcharecter').text(text_max + " characters remaining");
            $('#tbxchar').keyup(function () {
                var text_length = $('#tbxchar').val().length;
                var text_remaining = text_max - text_length;
                $('#lblcharecter').text(text_remaining + " characters remaining");
            });

            $("#example1").DataTable();
            $('#example2').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false,
                "ordering": true,
                "info": true,
                "autoWidth": false
            });
            //$(".treeview").removeClass("active");
            $(".aa").first().closest(".treeview").addClass("active");
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $("#example1").DataTable();
                $('#example2').DataTable({
                    "paging": true,
                    "lengthChange": false,
                    "searching": false,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
                //$(".treeview").removeClass("active");
                $(".aa").first().closest(".treeview").addClass("active");
            }
        });
    </script>

</asp:Content>

