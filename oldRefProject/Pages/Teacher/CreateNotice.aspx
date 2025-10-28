<%@ Page Title="<%$ Resources:Application,NoticeSetup %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CreateNotice.aspx.cs" Inherits="Pages_Teacher_CreateNotice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.11.0.css" rel="stylesheet" />
    <div class="row">
        <div class="col-sm-12">
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="<%$ Resources:Application,Student %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$ Resources:Application,Teacher %>"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="padding-bottom-15" id="divStudentCriteria">
                <asp:RadioButtonList ID="rdList1" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="For All"></asp:ListItem>
                    <asp:ListItem Value="2" Text="For Specific Class"></asp:ListItem>
                    <asp:ListItem Value="3" Text="For Specific Student"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>

    <div class="row" id="criteria">
        <div class="col-sm-12">
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label65" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
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
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                 <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label></label>
                              <div class="col-sm-6">
                            <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" CssClass="form-control" MaxLength="12"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRollNo" runat="server"
                                Enabled="True" TargetControlID="tbxReg" FilterType="Custom" ValidChars="0123456789.-"></cc1:FilteredTextBoxExtender>
                        </div>
                                  </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-flat btn-success" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-8">
            <div class="panel panel-success">
                <div class="panel-heading">Create Notice</div>
                <div class="panel-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="col-md-10 col-sm-10 col-xs-10">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Title %>"></asp:Label></label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Title" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="tbxName" ID="RegularExpressionValidator1"
                                    ValidationExpression="^[\s\S]{0,90}$" runat="server" ErrorMessage="Maximum 90 characters allowed."></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="tbxDate" runat="server" placeholder="Enter Date" CssClass="form-control" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="tbxDate" SelectedDate='<%# DateTime.Now %>'
                                    TargetControlID="tbxDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,ShortDescription %>"></asp:Label></label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="tbxShortDescription" runat="server" placeholder="Enter Short Description" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Short Description" ControlToValidate="tbxShortDescription">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="tbxShortDescription" ID="RegularExpressiontbxTopicsValidator"
                                    ValidationExpression="^[\s\S]{0,250}$" runat="server" ErrorMessage="Maximum 250 characters allowed."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Details %>"></asp:Label></label>
                            <div class="col-sm-10">
                                <asp:TextBox runat="server" ID="tbxDetails" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>
                                <cc1:HtmlEditorExtender ID="htmlEditorExtender1" TargetControlID="tbxDetails" runat="server">
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
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer text-right">
                    <asp:Button ID="btnNotice" runat="server" CssClass="btn btn-flat btn-success" Text="Send Notice" OnClick="btnNotice_Click" ValidationGroup="save" />
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="panel panel-success">
                <div class="panel-heading">Notice To...</div>
                <div class="panel-body">
                    <div id="forClass">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSpecificYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSpecificMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSpecificCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                                <div class="col-sm-8">
                                    <asp:CheckBoxList ID="chkSpecificClass" runat="server" DataTextField="ClassName" DataValueField="Id" RepeatColumns="2" CssClass="FormatRadioButtonList"></asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="updateStudent">
                        <ContentTemplate>
                            <asp:Panel ClientIDMode="Static" ID="pnlStudent" runat="server">
                                <asp:Repeater ID="rptStudent" runat="server">
                                    <HeaderTemplate>
                                        <table id="example11" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,RegistratinNo %>"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="Student Name"></asp:Label></th>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("RegNo") %></td>
                                            <td><%#Eval("NameEng") %>
                                                <asp:HiddenField ID="hdnStudentId" Value='<%#Eval("StudentToClassId") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkrow" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Panel ClientIDMode="Static" ID="pnlTeacher" runat="server">
                        <asp:Repeater ID="rptTeacher" runat="server">
                            <HeaderTemplate>
                                <table id="example11" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label10" runat="server" Text="Teacher Name"></asp:Label></th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("NameEng") %>
                                        <asp:HiddenField ID="hdnTeacherId" Value='<%#Eval("TeacherId") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkrow" runat="server" /></td>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#criteria").hide();
            //$("#forClass").hide();
            //$("#rdList1").hide();
            //$("#pnlTeacher").hide();            
            HideAllRelatedControl();
            $("#rdList").change(function () {

                var checked_radio = $("[id*=rdList] input:checked");

                if (checked_radio.val() == "1") {

                    $("#rdList1").slideDown();
                    $("#divStudentCriteria").slideDown();
                    $("#pnlTeacher").slideUp();

                    $("#rdList1").change(function () {
                        var checked_radio1 = $("[id*=rdList1] input:checked");
                        if (checked_radio1.val() == "2") {
                            $("#criteria").hide();
                            $("#forClass").slideDown();
                            $("#pnlStudent").slideUp();
                        }
                        else if (checked_radio1.val() == "3") {
                            $("#criteria").slideDown();
                            $("#forClass").slideUp();

                        }
                        else {
                            $("#criteria").slideUp();
                            $("#forClass").slideUp();
                            $("#pnlStudent").slideUp();
                            $("#pnlTeacher").slideUp();
                        }
                    });
                }
                else if (checked_radio.val() == "2") {
                    $("#pnlTeacher").slideDown();
                    $("#pnlStudent").slideUp();
                    $("#divStudentCriteria").slideUp();
                    $("#criteria").hide();
                    var radiolist = $('#rdList1').find('input:radio');
                    radiolist.removeAttr('checked');
                    $("#forClass").slideUp();

                }
                else {
                    $("#criteria").slideUp();
                    $("#forClass").slideUp();
                    $("#rdList1").slideUp();
                    $("#pnlStudent").slideUp();
                    $("#pnlTeacher").slideUp();
                }
            });
        });
        function load() {
            $("#example11 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example11 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example11 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example11 [id*=chkrow]").click(function () {
                if ($("#example11 [id*=chkrow]").length == $("#example11 [id*=chkrow]:checked").length) {
                    $("#example11 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example11 [id*=chkHeader]").prop("checked", false);
                }
            });
        }

        function HideAllRelatedControl() {
            $("#criteria").hide();
            $("#forClass").hide();
            $("#rdList1").hide();
            $("#pnlTeacher").hide();
        }


        $(document).ready(function () {
            load();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                load();
            }
        });
    </script>


</asp:Content>

