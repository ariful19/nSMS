<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChairmanSpeach.ascx.cs" Inherits="UserControl_ChairmanSpeach" %>
        <style>
		.static
		{
			color:White;
			background-color:#E0E0E0;
			 cursor:pointer;
	    }
        h1.capitalize {
    text-transform: capitalize;
}
		</style>
 <div class="widget-main">
        <div class="widget-main-title">
        <h1 class="widget-title capitalize" style="text-align:center; cursor:not-allowed">
           <img src="http://dhakapbs3.org.bd/content/com_mess.png" width="26" height="20"> <span style="font-size: 11pt;cursor:not-allowed"><strong><asp:label ID="lblTitle" runat="server"></asp:label></strong></span>
           
        </h1></div>  
            <div class="widget-inner"style="text-align:center; cursor:not-allowed">
                                   
                <div class="our-campus clearfix">
            <asp:Image ID="img" BorderStyle="Double" BackColor="Silver" runat="server" CssClass="img-thumbnail" Height="160" Width="150" /><br />
           <b> <asp:label ID="lblName" Font-Size="11pt" runat="server"></asp:label></b> <br />
           <b> <asp:label ID="lblDesignation" Font-Size="11pt" runat="server"></asp:label></b> <br />
          <b>  <asp:label ID="lblAddress" Font-Size="11pt" runat="server"></asp:label></b> <br />
            
            <div class="static"> <asp:HyperLink ID="HyperLink2" ForeColor="Red" runat="server" NavigateUrl='~/Pages/User/ChairmanDetails.aspx'></asp:HyperLink></div>
          
      
    
</div></div>
</div>
