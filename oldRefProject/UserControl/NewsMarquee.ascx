<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsMarquee.ascx.cs" Inherits="UserControl_NewsMarquee" %>


<div style="padding-left: 130px; background: url(../images/Common/update_right.jpg) no-repeat left  #E6EAEF;">

    <marquee style="cursor: pointer; padding: 10px; color: #333; font-size: 18px; font-weight: bold; margin-right: 0px;">
                       
                       <asp:Literal ID="ltrlMarque" runat="server"> </asp:Literal>
                    
                    </marquee>
</div>
