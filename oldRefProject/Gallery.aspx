<%@ Page Title="<%$ Resources:Header,Gallery %>" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Gallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
        <asp:Panel ID="pnlGallery" runat="server" BorderColor="Red">
        <asp:ImageMap ID="ImageMap1" runat="server"></asp:ImageMap>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

