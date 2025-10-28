<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Banner.ascx.cs" Inherits="UserControl_Banner" %>


<div class="flexslider">
    <ul class="slides">
        <asp:Repeater ID="rptImage" runat="server">
            <%--<ItemTemplate>
                <li>
                    <asp:Image ID="img1" runat="server" ImageUrl='<%#("~/Images/Banner/" + Container.DataItem.ToString())%>'
                               AlternateText="" title="" />
                </li>
            </ItemTemplate>--%>

            <ItemTemplate>
                <li>
                    <asp:Image ID="img1" runat="server" ImageUrl='<%# Bind("ImageName", "~/Images/Banner/{0}") %>'
                               AlternateText="" title="" />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<script type="text/javascript">
    $(window).load(function () {
        $('.flexslider').flexslider({
            animation: "slide",
            start: function (slider) {
                $('body').removeClass('loading');
            }
        });
    });
</script>
