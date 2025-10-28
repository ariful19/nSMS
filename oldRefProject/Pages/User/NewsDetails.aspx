<%@ Page Title="<%$ Resources:Header,NewsDetails %>" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeFile="NewsDetails.aspx.cs" Inherits="Pages_User_NewsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:Label ID="lblTitle" runat="server"></asp:Label>

    <asp:Literal ID="litDetails" runat="server"></asp:Literal>
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
</asp:Content>

