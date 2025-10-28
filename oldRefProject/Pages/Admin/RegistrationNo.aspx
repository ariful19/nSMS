<%@ Page Title="<%$ Resources:Application,DynamicRegNo %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="RegistrationNo.aspx.cs" Inherits="Pages_Admin_RegistrationNo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div class="row">
        <div class="col-sm-4">
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdlReg" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="<%$ Resources:Application,AutometicRegNo %>" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$ Resources:Application,MenualRegNo %>"></asp:ListItem>
                </asp:RadioButtonList>
            </div>

        </div>
        <div class="col-sm-8">
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,UseRoll %>"></asp:Label></label>
                <div class="col-sm-4">
                    <asp:CheckBox ID="chkRoll" runat="server" ClientIDMode="Static" Checked="true" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="padding-bottom-15" id="divMedium">
                <asp:RadioButtonList ID="rdlMedium" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="<%$ Resources:Application,BanglaMedium %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$ Resources:Application,EnglishMedium %>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="<%$ Resources:Application,EnglishVersion %>"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="col-sm-12">
            <div id="divYearCriteria">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="lblYear" runat="server" Text="<%$ Resources:Application,UseYear %>"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:CheckBox ID="chkYear" runat="server" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
            </div>
            <div class="col-sm-12">
            <asp:Panel ID="pnlYear" runat="server">

                <div class="padding-bottom-15" id="divYear">
                    <asp:RadioButtonList ID="rdlSelectYear" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                        <asp:ListItem Value="1" Text="Short Form"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Long Form"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </asp:Panel>

        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <div class="form-horizontal">
                <div class="form-group">

                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,RegNoFormat %>"></asp:Label></label>
                </div>
                <asp:Panel ID="styelMedium" runat="server">
                    <div class="form-group">
                        <div class="padding-bottom-15" id="divStyleMedium">
                            <asp:RadioButtonList ID="rdlStyleMedium" runat="server" ClientIDMode="Static" RepeatColumns="2" CssClass="form-control FormatRadioButtonList" BorderColor="White">
                                <asp:ListItem Value="1" Text="BN-XXXX"></asp:ListItem>
                                <asp:ListItem Value="2" Text="BNXXXX"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="styelYearS" runat="server">
                    <div class="padding-bottom-15" id="divStyleYear">
                        <asp:RadioButtonList ID="rdlStyleYearShort" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" BorderColor="White">
                            <asp:ListItem Value="3" Text="BN-17-XXXX"></asp:ListItem>
                            <asp:ListItem Value="4" Text="BN17-XXXX"></asp:ListItem>
                            <asp:ListItem Value="5" Text="BN17XXXX"></asp:ListItem>
                            <asp:ListItem Value="6" Text="BN-XXXX-17"></asp:ListItem>
                            <asp:ListItem Value="7" Text="BN-XXXX17"></asp:ListItem>
                            <asp:ListItem Value="8" Text="BNXXXX17"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
                <asp:Panel ID="styelYearL" runat="server">
                    <div class="padding-bottom-15" id="divStyleLYear">
                        <asp:RadioButtonList ID="rdlStyelLong" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList" BorderColor="White">
                            <asp:ListItem Value="9" Text="BN-2017-XXXX"></asp:ListItem>
                            <asp:ListItem Value="10" Text="BN2017-XXXX"></asp:ListItem>
                            <asp:ListItem Value="11" Text="BN2017XXXX"></asp:ListItem>
                            <asp:ListItem Value="12" Text="BN-XXXX-2017"></asp:ListItem>
                            <asp:ListItem Value="13" Text="BN-XXXX2017"></asp:ListItem>
                            <asp:ListItem Value="14" Text="BNXXXX2017"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </div>

    <div class="form-horizontal">
        <div class="form-group">

            <div class="col-md-10 col-sm-10 col-xs-10">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>

            </div>
        </div>

        <div class="form-group">
            <label for="inputEmail3" class="col-sm-1">
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Prefix %>"></asp:Label></label>
            <div class="col-sm-3">
                <asp:TextBox ID="tbxPrefix" runat="server" placeholder="Enter Prefix" CssClass="form-control"></asp:TextBox>
                <%--<asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Class" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>--%>
            </div>
        </div>

        <div class="form-group">
            <div class="col-sm-offset-1 col-sm-11">
                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="Update" OnClick="btnSave_Click" Visible="false" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnReset_Click" Visible="false" />
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-12">
                <div class="box">
                    <div class="box-body">
                        <asp:Repeater ID="rptYear" runat="server">
                            <HeaderTemplate>
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
                                        <asp:ImageButton ID="ImageButton1" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        $(document).ready(function () {



            $("#divStyleYear").hide();
            $("#divStyleLYear").hide();

            $("#rdlReg").change(function () {
                var radiolist1 = $('#rdlSelectYear').find('input:radio');
                radiolist1.removeAttr('checked');
                var radiolist = $('#rdlStyleYearShort').find('input:radio');
                radiolist.removeAttr('checked');
                var radiolists = $('#rdlStyelLong').find('input:radio');
                radiolists.removeAttr('checked');
                var radiolistss = $('#rdlMedium').find('input:radio');
                radiolistss.removeAttr('checked');

                var checked_radio = $("[id*=rdlReg] input:checked");

                if (checked_radio.val() == "1") {
                    $("#divYearCriteria").slideDown();
                    $("#divMedium").slideDown();
                } else {
                    $("#divYearCriteria").hide();
                    $("#divMedium").hide();
                }
            });

            $("#rdlMedium").change(function () {

                var radiolist = $('#rdlStyleYearShort').find('input:radio');
                radiolist.removeAttr('checked');
                var radiolists = $('#rdlStyelLong').find('input:radio');
                radiolists.removeAttr('checked');

                var checked_radio = $("[id*=rdlMedium] input:checked");

                if (checked_radio.val() == "1") {
                    $("#divStyleMedium").slideDown();
                }
                else if (checked_radio.val() == "2") {

                    $("#divStyleMedium").slideDown();
                }
                else {
                    $("#divStyleMedium").slideDown();
                }
            });


            $("#<%= chkYear.ClientID %>").change(function () {
                if ($(this).is(':checked')) {
                    //Here try to call the handler using jquery ajax.                    

                    $("#divYear").slideDown()
                    $("#rdlSelectYear").change(function () {

                        var checked_radio = $("[id*=rdlSelectYear] input:checked");

                        if (checked_radio.val() == "1") {
                            $("#divStyleYear").slideDown();
                            $("#divStyleLYear").slideUp();
                            $("#divStyleMedium").slideUp();

                            var radiolist = $('#rdlStyelLong').find('input:radio');
                            radiolist.removeAttr('checked');
                            var radiolists = $('#rdlStyleMedium').find('input:radio');
                            radiolists.removeAttr('checked');
                        }
                        else if (checked_radio.val() == "2") {
                            $("#divStyleLYear").slideDown();
                            $("#divStyleMedium").slideUp();
                            $("#divStyleYear").slideUp();

                            var radiolist = $('#rdlStyleYearShort').find('input:radio');
                            radiolist.removeAttr('checked');
                            var radiolists = $('#rdlStyleMedium').find('input:radio');
                            radiolists.removeAttr('checked');
                        }
                    });


                } else {

                    var radiolist = $('#rdlSelectYear').find('input:radio');
                    radiolist.removeAttr('checked');
                    $("#divStyleMedium").slideDown()
                    $("#divStyleLYear").slideUp();;
                    $("#divStyleYear").slideUp();
                    $("#divYear").slideUp();
                    // Do something
                }
            });
        });
    </script>
</asp:Content>
