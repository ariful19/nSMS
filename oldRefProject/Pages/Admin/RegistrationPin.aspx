<%@ Page Title="<%$ Resources:Application,RegistrationPin %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="RegistrationPin.aspx.cs" Inherits="Pages_Admin_RegistrationPin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="col-lg-6 col-md-6">
        <div class="form-horizontal">
            <asp:Label ID="Label" runat="server" Font-Size="16" ForeColor="#0000ff" Font-Bold="true" Width="100%">Select any one for cutomize</asp:Label>
            <br />
            <hr />
            <div class="form-group">
                <div class="col-sm-12">

                    <asp:RadioButtonList ID="rdlSelect" runat="server" ClientIDMode="Static" RepeatColumns="2" CssClass="form-control FormatRadioButtonList" ForeColor="#990099" Font-Size="14" Width="100%">
                        <asp:ListItem Value="1" Text="<%$ Resources:Application,StudentRegistration %>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$ Resources:Application,TeacherPin %>"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="padding-bottom-15">
                        <asp:RadioButtonList ID="rdlReg" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" ForeColor="#990099" Font-Size="13" Width="100%">
                            <asp:ListItem Value="1" Text="<%$ Resources:Application,AutomaticRegNo %>"></asp:ListItem>
                            <asp:ListItem Value="2" Text="<%$ Resources:Application,ManualRegNo %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <div id="divRegistration">
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="padding-bottom-15">
                            <asp:RadioButtonList ID="rdlMedium" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" ForeColor="#990099" Font-Size="13" Width="100%">
                                <asp:ListItem Value="1" Text="<%$ Resources:Application,BanglaMedium %>"></asp:ListItem>
                                <asp:ListItem Value="2" Text="<%$ Resources:Application,EnglishMedium %>"></asp:ListItem>
                                <asp:ListItem Value="3" Text="<%$ Resources:Application,EnglishVersion %>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-12">
                        <asp:Label ID="Label3" runat="server" Font-Size="13" ForeColor="#0000ff" Font-Bold="true" Width="100%">Using Year as Reg. Prefix</asp:Label>
                        <br />
                        <hr />
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Yes %>" ForeColor="#333300" Font-Size="12" Width="100%"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:CheckBox ID="chkYearYes" runat="server" ClientIDMode="Static" Width="100%" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,No %>" ForeColor="#333300" Font-Size="12" Width="100%"></asp:Label></label>
                                <div class="col-sm-4">
                                    <asp:CheckBox ID="chkYearNo" runat="server" ClientIDMode="Static" Width="100%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divYearSelect">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="padding-bottom-15" id="divYear" runat="server">
                                <asp:RadioButtonList ID="rdlSelectYear" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" ForeColor="#990099" Font-Size="13" Width="100%">
                                    <asp:ListItem Value="1" Text="Short Form"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Long Form"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,RegNoFormat %>" Font-Size="13" ForeColor="#0000ff" Font-Bold="true" Width="100%"></asp:Label>

                            <hr />
                            <div class="form-group" id="divYearFormet">
                                <div class="padding-bottom-15" id="divStyleMedium">
                                    <asp:RadioButtonList ID="rdlStyleMedium" runat="server" ClientIDMode="Static" RepeatColumns="2" CssClass="form-control FormatRadioButtonList" BorderColor="White" ForeColor="#333300" Width="100%">
                                        <asp:ListItem Value="1" Text="BN-XXXX"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="BNXXXX"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="padding-bottom-15" id="divStyleYear">
                                    <asp:RadioButtonList ID="rdlStyleYearShort" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" BorderColor="White" ForeColor="#333300" Width="100%">
                                        <asp:ListItem Value="3" Text="BN-17-XXXX"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="BN17-XXXX"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="BN17XXXX"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="BN-XXXX-17"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="BN-XXXX17"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="BNXXXX17"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="padding-bottom-15" id="divStyleLYear">
                                    <asp:RadioButtonList ID="rdlStyelLong" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" BorderColor="White" ForeColor="#333300" Width="100%">
                                        <asp:ListItem Value="9" Text="BN-2017-XXXX"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="BN2017-XXXX"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="BN2017XXXX"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="BN-XXXX-2017"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="BN-XXXX2017"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="BNXXXX2017"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">

                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message" Width="100%"></asp:Label>

                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Prefix %>" ForeColor="#990099" Font-Size="13" Width="100%"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxPrefix" runat="server" placeholder="Enter Prefix" CssClass="form-control" Width="100%"></asp:TextBox>
                    </div>
                </div>

            </div>


        </div>
    </div>
    <div class="col-lg-6 col-md-6">
        <div class="form-horizontal" id="rollDiv">
            <asp:Label ID="Label1" runat="server" Font-Size="16" ForeColor="#0000ff" Font-Bold="true" Width="100%">Using Roll No</asp:Label>
            <br />
            <hr />
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">
                    <asp:Label ID="lblRoll" runat="server" Text="<%$ Resources:Application,Yes %>" ForeColor="#333300" Font-Size="12" Width="100%"></asp:Label></label>
                <div class="col-sm-4">
                    <asp:CheckBox ID="chkRollYes" runat="server" ClientIDMode="Static" Checked="true" AutoPostBack="true" OnCheckedChanged="chkRollYes_CheckedChanged" Width="100%" />
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,No %>" ForeColor="#333300" Font-Size="12" Width="100%"></asp:Label></label>
                <div class="col-sm-4">
                    <asp:CheckBox ID="chkRollNo" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkRollYes_CheckedChanged" Width="100%" />
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-12">
                    <asp:Label ID="lblMessege" runat="server" ForeColor="Red" Font-Size="12" Width="100%"></asp:Label></label>
            </div>

        </div>
        <div class="form-horizontal" id="rptYearDiv">
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptYear" runat="server">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHdr" runat="server" Text="Student Registration No. Format" BorderStyle="Groove" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true"></asp:Label>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Prefix %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,YearStyle %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,RegNoFormat %>"></asp:Label></th>
                                                <th class="action">
                                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="action"><%#Eval("Id") %></td>
                                        <td><%#Eval("MediumName") %></td>
                                        <td><%#Eval("Prefix") %></td>
                                        <td><%#Eval("Year") %></td>
                                        <td><%#Eval("Format") %></td>
                                        <td class="action">
                                            <asp:ImageButton ID="imgBtn" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
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
        <div class="form-horizontal" id="rptTeacherDiv">
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptTeacher" runat="server">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHdr" runat="server" Text="Teacher Pin Format" BorderStyle="Groove" BackColor="#ccffcc" ForeColor="#ff0000" Font-Bold="true"></asp:Label>
                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,PinFormet %>"></asp:Label></th>
                                                <%-- <th>
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Prefix %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,YearStyle %>"></asp:Label></th>
                                                   <th>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,RegNoFormat %>"></asp:Label></th>
                                                <th class="action">
                                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="action"><%#Eval("Id") %></td>
                                        <td><%#Eval("PinFormat") %></td>
                                        <%-- <td><%#Eval("Prefix") %></td>--%>
                                        <%--<td><%#Eval("Year") %></td>
                                        <%--<td><%#Eval("Format") %></td>
                                        <td class="action">
                                            <asp:ImageButton ID="imgBtn" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />--%>
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
    </div>
    <div class="col-lg-12 col-md-12">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-offset-1 col-sm-11">
                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="Update" OnClick="btnSave_Click"/>
                    <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnReset_Click"/>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12 col-md-12">
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        $(document).ready(function () {

            $("#btnSave").hide();
            $("#btnReset").hide();
            $("#rptYearDiv").hide();
            $("#rptTeacherDiv").hide();
            $('#rollDiv').slideDown();

            var checked_radio = $("[id*=rdlSelect] input:checked");
            if (checked_radio.val() == "1") {
                $("#btnSave").slideDown();
                $("#btnReset").slideDown();
                $("#rptYearDiv").slideDown();
                $("#rptTeacherDiv").slideUp();
                $('#rollDiv').hide();
            }
            if (checked_radio.val() == "2") {
                $("#rptYearDiv").slideUp();
                $("#rptTeacherDiv").slideDown();
                $('#rollDiv').hide();
            }

            $("#rdlSelect").change(function () {
                var checked_radio = $("[id*=rdlSelect] input:checked");

                if (checked_radio.val() == "1") {
                    $("#rdlReg").slideDown();
                    $("#rdlMedium").slideDown();
                    $("#rptYearDiv").slideDown();
                    $("#rptTeacherDiv").hide();
                    $("#divYearSelect").slideUp();
                    $("#btnSave").slideUp();
                    $("#btnReset").slideUP();
                    $('#rollDiv').slideDown();

                } else {
                    $('#rdlMedium').find('input:checked').attr('checked', false);
                    $("#rdlReg").slideDown();
                    $("#rdlMedium").hide();
                    $("#divYearSelect").slideDown();
                    $("#btnSave").slideDown();
                    $("#btnReset").slideDown();
                    $("#rptYearDiv").hide();
                    $("#rptTeacherDiv").slideDown();
                    $('#rollDiv').hide();
                }
            });

            $("#rdlReg").change(function () {
                var checked_radio = $("[id*=rdlReg] input:checked");
                var checked_radio1 = $("[id*=rdlSelect] input:checked");
                if (checked_radio.val() == "1") {
                    $("#divRegistration").slideDown();
                    if (checked_radio1.val() == "2") {
                        $("#divYearSelect").slideDown();
                        $("#divStyleMedium").slideDown();
                    }

                } else {

                    //$("#divRegistration").slideUp();
                    $('#rdlSelectYear').find('input:checked').attr('checked', false);
                    $('#rdlStyleMedium').find('input:checked').attr('checked', false);
                    $('#rdlStyleYearShort').find('input:checked').attr('checked', false);
                    $('#rdlStyelLong').find('input:checked').attr('checked', false);
                    $('#chkYearYes').attr("checked", false).checkboxradio("refresh");


                }
                $('#chkYearNo').attr("checked", false).checkboxradio("refresh");
            });
            $("#rdlMedium").change(function () {
                var checked_radio = $("[id*=rdlMedium] input:checked");
                if (checked_radio.val() == "1") {
                    $("#divYearSelect").slideDown();
                    $("#divStyleMedium").slideDown();

                } else {
                    $("#divYearSelect").slideDown();
                    $("#divStyleMedium").slideDown();

                }
            });

            $("#<%= chkYearYes.ClientID %>").change(function () {
                if ($(this).is(':checked')) {
                    $('#rdlStyleMedium').find('input:checked').attr('checked', false);
                    $("#divYearSelect").slideDown();
                    $("#rdlSelectYear").slideDown();

                    $('#chkYearNo').attr("checked", false).checkboxradio("refresh");
                } else {
                    $("#divStyleMedium").slideDown();
                    $("#rdlSelectYear").slideUp();
                    $("#divStyleYear").slideUp();
                    $("#divStyleLYear").slideUp();
                    $('#chkYearNo').attr("checked", true).checkboxradio("refresh");
                }
            });
            $("#<%= chkYearNo.ClientID %>").change(function () {
                if ($(this).is(':checked')) {
                    $('#rdlSelectYear').find('input:checked').attr('checked', false);
                    $('#rdlStyleYearShort').find('input:checked').attr('checked', false);
                    $('#rdlStyelLong').find('input:checked').attr('checked', false);
                    $("#rdlSelectYear").slideUp();
                    $("#divStyleYear").slideUp();
                    $("#divStyleLYear").slideUp();
                    $("#divStyleMedium").slideDown();
                    $('#chkYearYes').attr("checked", false).checkboxradio("refresh");

                }
            });
            $("#rdlSelectYear").change(function () {
                var checked_radio = $("[id*=rdlSelectYear] input:checked");
                if (checked_radio.val() == "1") {
                    $('#rdlStyelLong').find('input:checked').attr('checked', false);
                    $('#rdlStyleMedium').find('input:checked').attr('checked', false);
                    $("#divStyleMedium").slideUp();
                    $("#divStyleLYear").slideUp();
                    $("#divStyleYear").slideDown();

                } else {
                    $('#rdlStyleMedium').find('input:checked').attr('checked', false);
                    $('#rdlStyleYearShort').find('input:checked').attr('checked', false);
                    $("#divStyleMedium").slideUp();
                    $("#divStyleYear").slideUp();
                    $("#divStyleLYear").slideDown();

                }
            });

            $("#<%= chkRollYes.ClientID %>").change(function () {
                if ($(this).is(':checked')) {

                    $('#chkRollNo').attr("checked", false).checkboxradio("refresh");
                }
            });
            $("#<%= chkRollNo.ClientID %>").change(function () {
                if ($(this).is(':checked')) {

                    $('#chkRollYes').attr("checked", false).checkboxradio("refresh");
                }
            });

        });
    </script>
</asp:Content>
