<%@ Page Title="About Us" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="Pages_User_AboutUs" %>

<%@ Register TagName="News" TagPrefix="UC" Src="~/UserControl/News.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:Panel ID="pnlAboutUs" runat="server">

        <asp:Label ID="lblAboutus" runat="server" Text="">

        </asp:Label>

    </asp:Panel>
       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
</asp:Content>

