<%@ Page Title="<%$ Resources:Application,SiteConfiguration %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SiteConfiguration.aspx.cs" Inherits="Pages_Setting_SiteConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <ul class="nav nav-tabs">
        <li id="1"><a data-toggle="tab" href="#home"><strong>
            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,SchoolInformation %>"></asp:Label></strong></a></li>
        <li id="2"><a data-toggle="tab" href="#menu1"><strong>
            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,GeneralSetting %>"></asp:Label></strong></a></li>
        <li id="3"><a data-toggle="tab" href="#menu2"><strong>
            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,SMSGetwaySetting %>"></asp:Label></strong></a></li>
        <li id="4"><a data-toggle="tab" href="#menu3"><strong>
            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,NotificationSetting %>"></asp:Label></strong></a></li>
    </ul>
    <div class="tab-content">
        <div id="home" class="tab-pane fade in active pt-10">
            <div class='<%=Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,SchoolInformation %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SchoolInfo" />
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,SchooName %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxSchoolInfo" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="SchoolInfo"
                                    ErrorMessage="Enter Schoo Name." ControlToValidate="tbxSchoolInfo">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,SchoolCode %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxSchoolCode" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="SchoolInfo"
                                    ErrorMessage="Enter School Code." ControlToValidate="tbxSchoolCode">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,SchoolLogo %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:Image ID="imgSchool" runat="server" CssClass="img-thumbnail" Height="33.9" Width="42.2" /><br />
                                <asp:FileUpload ID="uploderStudent" runat="server" onChange="ShowThumbnail()" CssClass="btn btn-default" /><br />
                                <p class="help-block">Note: All Images .png .jpeg .jpg are allowed.</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,EstablishedYear %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxEstd" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="SchoolInfo"
                                    ErrorMessage="Enter Established Year." ControlToValidate="tbxEstd">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,EstablishedBy %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxEstBy" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxAddress" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Details %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxDetails" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control textarea"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnSchoolInfo" ClientIDMode="Static" runat="server" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="SchoolInfo"
                        OnClick="btnSchoolInfo_Click" />
                </div>
            </div>
        </div>
        <div id="menu1" class="tab-pane fade in pt-10">
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,GeneralSetting %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="General" />
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,Theme %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlTheme" runat="server" CssClass="form-control">
                                    <asp:ListItem>Default</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,DateFormat %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddldateformat" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="dd/MM/yyyy" Value="dd/MM/yyyy" Selected="True" />
                                    <asp:ListItem Text="dd/MMM/yyyy" Value="dd/MMM/yyyy" />
                                    <asp:ListItem Text="yy/MM/dd" Value="yy/MM/dd" />
                                    <asp:ListItem Text="yyyy-MM-dd" Value="yyyy-MM-dd" />
                                    <asp:ListItem Text="dd-MMM-yyyy" Value="dd-MMM-yyyy" />
                                    <asp:ListItem Text="dd-MM-yyyy" Value="dd-MM-yyyy" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,TimeZone %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlTimeZone" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-12">(GMT-12:00) International dateline, west</asp:ListItem>
                                    <asp:ListItem Value="-11">(GMT-11:00) Midway Islands, Samoan Islands</asp:ListItem>
                                    <asp:ListItem Value="-10">(GMT-10:00) Hawaii</asp:ListItem>
                                    <asp:ListItem Value="-9">(GMT-09:00) Alaska</asp:ListItem>
                                    <asp:ListItem Value="-8">(GMT-08:00) Pacific Time (USA og Canada); Tijuana</asp:ListItem>
                                    <asp:ListItem Value="-7">(GMT-07:00) Mountain Time (USA og Canada)</asp:ListItem>
                                    <asp:ListItem Value="-6">(GMT-06:00) Central time (USA og Canada)</asp:ListItem>
                                    <asp:ListItem Value="-5">(GMT-05:00) Eastern time (USA og Canada)</asp:ListItem>
                                    <asp:ListItem Value="-4">(GMT-04:00) Atlantic Time (Canada)</asp:ListItem>
                                    <asp:ListItem Value="-3.5">(GMT-03:30) Newfoundland</asp:ListItem>
                                    <asp:ListItem Value="-3">(GMT-03:00) Brasilia</asp:ListItem>
                                    <asp:ListItem Value="-2">(GMT-02:00) Mid-Atlantic</asp:ListItem>
                                    <asp:ListItem Value="-1">(GMT-01:00) Azorerne</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="true">(GMT) Greenwich Mean Time: Dublin, Edinburgh, Lissabon, London</asp:ListItem>
                                    <asp:ListItem Value="1">(GMT+01:00) Amsterdam, Berlin, Bern, Rom, Stockholm, Wien</asp:ListItem>
                                    <asp:ListItem Value="2">(GMT+02:00) Athen, Istanbul, Minsk</asp:ListItem>
                                    <asp:ListItem Value="3">(GMT+03:00) Moscow, St. Petersburg, Volgograd</asp:ListItem>
                                    <asp:ListItem Value="3.5">(GMT+03:30) Teheran</asp:ListItem>
                                    <asp:ListItem Value="4">(GMT+04:00) Abu Dhabi, Muscat</asp:ListItem>
                                    <asp:ListItem Value="4.5">(GMT+04:30) Kabul</asp:ListItem>
                                    <asp:ListItem Value="5">(GMT+05:00) Islamabad, Karachi, Tasjkent</asp:ListItem>
                                    <asp:ListItem Value="5.5">(GMT+05:30) Kolkata, Chennai, Mumbai, New Delhi</asp:ListItem>
                                    <asp:ListItem Value="5.75">(GMT+05:45) Katmandu</asp:ListItem>
                                    <asp:ListItem Value="6">(GMT+06:00) Astana, Dhaka</asp:ListItem>
                                    <asp:ListItem Value="6.5">(GMT+06:30) Rangoon</asp:ListItem>
                                    <asp:ListItem Value="7">(GMT+07:00) Bangkok, Hanoi, Djakarta</asp:ListItem>
                                    <asp:ListItem Value="8">(GMT+08:00) Beijing, Chongjin, SAR Hongkong, Ürümqi</asp:ListItem>
                                    <asp:ListItem Value="9">(GMT+09:00) Osaka, Sapporo, Tokyo</asp:ListItem>
                                    <asp:ListItem Value="9.5">(GMT+09:30) Adelaide</asp:ListItem>
                                    <asp:ListItem Value="10">(GMT+10:00) Canberra, Melbourne, Sydney</asp:ListItem>
                                    <asp:ListItem Value="11">(GMT+11:00) Magadan, Solomon Islands, New Caledonien</asp:ListItem>
                                    <asp:ListItem Value="12">(GMT+12:00) Fiji, Kamtjatka, Marshall Islands</asp:ListItem>
                                    <asp:ListItem Value="13">(GMT+13:00) Nuku'alofa</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,Button %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlButton" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="btn btn-default" Text="Default"></asp:ListItem>
                                    <asp:ListItem Value="btn btn-primany" Text="Primary"></asp:ListItem>
                                    <asp:ListItem Value="btn btn-danger" Text="Danger"></asp:ListItem>
                                    <asp:ListItem Value="btn btn-info" Text="Info"></asp:ListItem>
                                    <asp:ListItem Value="btn btn-success" Text="Success"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,Panel %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlPanel" runat="server" CssClass="form-control">
                                    <asp:ListItem Value='<%= Common.SessionInfo.Panel %>' Text="Default"></asp:ListItem>
                                    <asp:ListItem Value="panel panel-primany" Text="Primary"></asp:ListItem>
                                    <asp:ListItem Value="panel panel-danger" Text="Danger"></asp:ListItem>
                                    <asp:ListItem Value='<%= Common.SessionInfo.Panel %>' Text="Info"></asp:ListItem>
                                    <asp:ListItem Value="panel panel-success" Text="Success"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnGeneral" runat="server" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="General"
                        OnClick="btnGeneral_Click" />
                </div>
            </div>
        </div>
        <div id="menu2" class="tab-pane fade in pt-10">
            <div class='<%= Common.SessionInfo.Panel %>'>
                <div class="panel-heading">
                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,SMSGetwaySetting %>"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Getway" />
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,URL %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxUrl" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="Getway"
                                    ErrorMessage="Enter Getway URL." ControlToValidate="tbxSchoolInfo">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Application,UserName %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="Getway"
                                    ErrorMessage="Enter User Name." ControlToValidate="tbxSchoolCode">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,Password %>"></asp:Label><span class="required">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="Getway"
                                    ErrorMessage="Enter Password." ControlToValidate="tbxPassword">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnGetway" runat="server" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="Getway"
                        OnClick="btnGetway_Click" />
                </div>
            </div>
        </div>
        <div id="menu3" class="tab-pane fade in pt-10">
            <div class="row">
                <asp:UpdatePanel ID="updatedd" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-5">
                            <div class='<%= Common.SessionInfo.Panel %>'>
                                <div class="panel-heading">
                                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,Notification %>"></asp:Label>
                                </div>
                                <div class="panel-body">
                                    <asp:Repeater ID="rptNotification" runat="server">
                                        <HeaderTemplate>
                                            <table id="notification" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>
                                                            <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,NotificationTitle %>"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Application,SendEmail %>"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,SendSMS %>"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("Id") %></td>
                                                <td><%#Eval("Title") %></td>
                                                <td><%#Eval("SendEmail").ToString()=="True"?"Yes":"No" %></td>
                                                <td><%#Eval("SendSMS").ToString()=="True"?"Yes":"No" %></td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
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
                        <div class="col-sm-7">
                            <div class='<%= Common.SessionInfo.Panel %>'>
                                <div class="panel-heading">
                                    <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,AddNewNotification %>"></asp:Label>
                                </div>
                                <div class="panel-body">
                                    <div class="form-horizontal">

                                        <div class="col-sm-offset-3 col-sm-10">
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Notification" />
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Application,NotificationTitle %>"></asp:Label><span class="required">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="tbxTitle" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:HiddenField ID="Id" ClientIDMode="Static" runat="server" />
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="Notification"
                                                    ErrorMessage="Enter Notification Title." ControlToValidate="tbxTitle">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Application,SendEmail %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:CheckBox ClientIDMode="Static" ID="chkEmail" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-4">
                                                <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Application,SendSMS %>"></asp:Label></label>
                                            <div class="col-sm-6">
                                                <asp:CheckBox ClientIDMode="Static" ID="chkSMS" runat="server" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <asp:Button ID="btnNotification" runat="server" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="Notification"
                                        OnClick="btnNotification_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>

