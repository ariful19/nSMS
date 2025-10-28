<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestBook.ascx.cs" Inherits="UserControl_LatestBook" %>



<div class='<%= Common.SessionInfo.Panel %>'>
  <div class="panel-heading latestbook-heading">Latest Book</div>
  <div class="panel-body">
    <asp:DataList ID="rptLatest" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
    <ItemTemplate>
        <asp:HyperLink ID="lnkProductImage" class="linkImage" Target="_Blank" NavigateUrl='<%# String.Concat("~/ReadBook.aspx?Name=",Container.DataItem.ToString()) %>' 
            runat="server">
            <asp:Image ID="Image1" Height="180" runat="server" AlternateText='<%#Container.DataItem.ToString()%>' ImageUrl='<%# String.Concat("~/Ebooks/",Container.DataItem.ToString(),"/files/thumb/1.jpg" )%>' />
        </asp:HyperLink>
        <div class="Dt_RowSeparator"></div>
    </ItemTemplate>
</asp:DataList>
  </div>
</div>
