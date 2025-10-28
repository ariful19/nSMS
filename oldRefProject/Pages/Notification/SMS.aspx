<%@ Page Title="<%$ Resources:Application,SMSNotification %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SMS.aspx.cs" Inherits="Pages_Notification_SMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="All Active Student" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$ Resources:Application,Student %>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="All Active Employee"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    <div class='<%= Common.SessionInfo.Panel %>' id="criteria">
        <div class="panel-heading">
            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6" id="crt1">
                <div class="form-horizontal">
                    <div class="form-group" id="yearId">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChange"></asp:DropDownList>
                        </div>
                    </div>
                     <div class="form-group" id="mediumId">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                     <div class="form-group" id="campusId">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlCampus_SelectedIndexChange"></asp:DropDownList>
                            </div>
                        </div>
                    <div class="form-group" id="classId">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>     
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="<%$ Resources:Application,Search %>" />
                </div>
            </div>
            <div class="col-lg-6 col-md-6" id="crt2">
                <div class="form-horizontal">
                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
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
                                <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                    </th>
                                                     <th>
                                                        <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label>
                                                        <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblName" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                                    </th>

                                                    <th>
                                                        <asp:Label ID="lblMobile" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkrow" runat="server" /></td>
                                             <td>
                                                <asp:Label ID="lblRoll" Text='<%#Eval("RollNo") %>' runat="server" />
                                                <asp:Label ID="lblReg" Text='<%#Eval("RegNo") %>' runat="server" />
                                            </td>
                                            <td><%#Eval("NameEng") %>
                                                <asp:HiddenField ID="hdnStudentId" Value='<%#Eval("PersonID") %>' runat="server" />
                                            </td>

                                            <td>
                                                <%#Eval("Mobile") %>
                                            </td>
                                            <td>
                                                <%#Eval("Email") %>
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
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Submit" ValidationGroup="save" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ClientIDMode="Static" ID="pnlAllAssignStudent" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptAllStudent" runat="server">
                                    <HeaderTemplate>
                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblName" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                                    </th>

                                                    <th>
                                                        <asp:Label ID="lblMobile" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Email %>"></asp:Label>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkrow" runat="server" /></td>
                                            <td>
                                                <%#Eval("RegNo") %>
                                            </td>
                                            <td><%#Eval("NameEng") %>
                                                <asp:HiddenField ID="hdnStudentId" Value='<%#Eval("PersonID") %>' runat="server" />
                                            </td>

                                            <td>
                                                <%#Eval("Mobile") %>
                                            </td>
                                            <td>
                                                <%#Eval("Email") %>
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
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Submit" ValidationGroup="save" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ClientIDMode="Static" ID="pnlTeacher" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,TeacherList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptTeacher" runat="server">
                                    <HeaderTemplate>
                                        <table id="example3" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label></th>


                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkrow" runat="server" />
                                            </td>
                                            <td>
                                                <%#Eval("NameEng") %>
                                                <asp:HiddenField ID="hdnPersonID" Value='<%#Eval("PersonID") %>' runat="server" />
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
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="<%$ Resources:Application,Submit %>" ValidationGroup="save" />
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
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="lblErrorH" runat="server" Text="" ForeColor="Violet" Font-Underline="true" Font-Bold="true"></asp:Label>
                                </label>
                              
                                <div class="col-sm-6">
                                  <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            
                        </div>

                       
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

            //$("#example1").DataTable({
            //    "paging": true,
            //    "lengthChange": false,
            //    "searching": true,
            //    "ordering": true,
            //    "info": false,
            //    "autoWidth": true
            //});

            //$("#example2").DataTable({
            //    "paging": true,
            //    "lengthChange": false,
            //    "searching": true,
            //    "ordering": true,
            //    "info": false,
            //    "autoWidth": true
            //});
            //$("#example3").DataTable({
            //    "paging": true,
            //    "lengthChange": false,
            //    "searching": true,
            //    "ordering": true,
            //    "info": false,
            //    "autoWidth": true
            //});

            //var oTable1 = $('#example1').dataTable({
            //    stateSave: true
            //});
            //var allPages1 = oTable1.fnGetNodes();

            var oTable2 = $('#example2').dataTable({
                stateSave: true
            });
            var allPages2 = oTable2.fnGetNodes();

            var $allCheckbox = $("#example1 [id*=chkHeader]");
            var $checkboxes = $("#example1 [id*=chkrow]");
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
           

            var checked_radio = $("[id*=rdList] input:checked");
            if (checked_radio.val() == "1") {
                $("#crt2").slideUp();
                $("#mediumId").hide();
                $("#classId").hide();
                $("#pnlAllAssignStudent").slideDown();
                $("#templete").slideDown();
                $("#pnlTeacher").hide();
                $("#pnlAssignStudent").hide();
            }
            else if (checked_radio.val() == "2") {
                $("#crt1").slideDown();
                $("#crt2").slideDown();
                $("#mediumId").show();
                $("#classId").show();
                $("#pnlAssignStudent").slideDown();
                $("#pnlTeacher").slideUp();
                $("#templete").slideDown();
                $("#pnlAllAssignStudent").slideUp();
            } else {
                $("#crt2").slideUp();
                $("#mediumId").hide();
                $("#classId").hide();
                $("#pnlAssignStudent").slideUp();
                $("#pnlTeacher").slideDown();
                $("#templete").slideDown();
                $("#pnlAllAssignStudent").slideUp();
            }

            // $("#pnlTeacher").hide();
            $("#rdList").change(function () {

                var checked_radio = $("[id*=rdList] input:checked");
                if (checked_radio.val() == "1") {
                    $("#crt2").slideUp();
                    $("#mediumId").hide();
                    $("#classId").hide();
                    $("#pnlAllAssignStudent").slideDown();
                    $("#templete").slideDown();
                    $("#pnlTeacher").hide();
                    $("#pnlAssignStudent").hide();
                }
                else if (checked_radio.val() == "2") {
                    $("#crt1").slideDown();
                    $("#crt2").slideDown();
                    $("#mediumId").show();
                    $("#classId").show();
                    $("#pnlAssignStudent").slideDown();
                    $("#pnlTeacher").slideUp();
                    $("#templete").slideDown();
                    $("#pnlAllAssignStudent").slideUp();
                    $("#")
                    
                } else {
                    $("#crt2").slideUp();
                    $("#mediumId").hide();
                    $("#classId").hide();
                    $("#pnlAssignStudent").slideUp();
                    $("#pnlTeacher").slideDown();
                    $("#templete").slideDown();
                    $("#pnlAllAssignStudent").slideUp();
                }
            });
        });
    </script>

</asp:Content>

