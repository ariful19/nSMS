<%@ Page Title="<%$ Resources:Application,UnassignedStudentSMSNotification %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="StudentSMS.aspx.cs" Inherits="Pages_Notification_StudentSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div class="panel-body">
        <div class="col-lg-6 col-md-6">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default" OnClick="btnSearch_Click" Text="<%$ Resources:Application,Search %>" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
            <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptUnassign" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblName" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
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
                                                <asp:CheckBox ID="chkrow" data-student='<%#Eval("StudentId") %>' runat="server"></asp:CheckBox></td>
                                            <td>
                                                <%#Eval("RegistratinNo") %>
                                            </td>
                                            <td>
                                                <%#Eval("NameEng") %>

                                                 <asp:HiddenField ID="hdnPersonId" Value='<%#Eval("PersonID") %>' runat="server" />
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
                    <div class="panel-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Send SMS" OnClick="btnSave_Click" ValidationGroup="save" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="col-sm-6">
            <div class='<%= Common.SessionInfo.Panel %>' id="templete">
                <div class="panel-heading">
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,SMSTemplete %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,SMSTemplete %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlEmailTemplte" runat="server" DataTextField="Name" DataValueField="Id"
                                    OnSelectedIndexChanged="ddlEmailTemplte_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control dropdown">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="updateEmail" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Body %>"></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblBody" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlEmailTemplte" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

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

            var $allCheckbox = $("#example1 [id*=chkHeader]");
            var $checkboxes = $("#example1 [id*=chkrow]");
      
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
            $("#example1").DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": false,
                "autoWidth": true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

