<%@ Control Language="C#" AutoEventWireup="true" CodeFile="News.ascx.cs" Inherits="UserControl_News" %>
<div class="widget-main">
    <div class="widget-main-title">
        <h4 class="widget-title">
            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Header,LatestNews %>"></asp:Label></h4>
    </div>
   <div class="widget-inner">
        <asp:Repeater runat="server" ID="rptNews" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <div class="blog-list-post clearfix">
                    <div class="blog-list-details">
                        <div class="blog-list-thumb">
                            <a href='<%# String.Concat("../Pages/user/NewsDetails.aspx?ID=", Eval("Id")) %>' title="What you have to know News Details">
                                <asp:HiddenField Value='<%# Eval("Photo") %>' ID="HiddenField1" runat="server" />
                                <asp:Image ID="Image1" runat="server" Height="65" Width="65" />
                            </a>
                        </div>
                        <h5 class="blog-list-title">
                            <asp:HyperLink ID="HyperLink2" runat="server" Text='<%#Eval("Title") %>' NavigateUrl='<%# String.Concat("~/Pages/user/NewsDetails.aspx?ID=", Eval("Id")) %>'></asp:HyperLink>
                        </h5>
                        <p class="blog-list-meta small-text"><span><%# Convert.ToDateTime(Eval("Date").ToString()).ToString("dd-MMM-yyyy")%></span></p>
                        <p class="small-text"><%# Eval("ShortDescription") %>... ...</p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
