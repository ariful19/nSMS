<%@ Page Title="<%$ Resources:Header,EventCalendar%>" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeFile="EventCalendar.aspx.cs" Inherits="Pages_User_EventCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.10.3.min.css" rel="stylesheet" type="text/css" />
    <link href="../../fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../../css/jquery.qtip-2.2.0.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="calendar">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

