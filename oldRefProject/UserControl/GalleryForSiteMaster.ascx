<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GalleryForSiteMaster.ascx.cs" Inherits="UserControl_GalleryForSiteMaster" %>


<asp:Panel ID="pnlGallery" runat="server" BorderColor="Red">
       <%-- <asp:ImageMap ID="ImageMap1" runat="server"></asp:ImageMap>--%>

          <div class="widget-main">
                <div class="widget-main-title">
                    
    <h4 class="widget-title"><asp:Label ID="Label19" runat="server" Text="<%$ Resources:Header,Gallery %>"></asp:Label></h4>
                    </div>
   <div class="widget-inner">
                        <div class="gallery-small-thumbs clearfix">
                           <div class="">
                                 <table align="center" class="table-responsive" style="width: 95%;">
                    <tr>
                        <td colspan="2">
                            <asp:DataList ID="dlImages" runat="server" RepeatColumns="3" RepeatLayout="Flow" HorizontalAlign="Left" RepeatDirection="Horizontal" CellPadding="5">
                        <ItemTemplate>
                            <a id="imageLink" href='<%# Eval("ImageName","~/Images/gallery/{0}") %>' title='<%#Eval("ImageDescription") %>'
                                runat="server">
                                <asp:Image ID="Image1" ImageUrl='<%# Bind("ImageName", "~/Images/gallery/{0}") %>'
                                    runat="server" Width="100" Height="80" />
                            </a>
                            <div>
                                
                            </div>
                        </ItemTemplate>
                        <ItemStyle  BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                            VerticalAlign="Bottom" />
                    </asp:DataList>
                        </td>
                    </tr>
                   
                </table>
                               </div>
                               
                        </div>
                       
                        <br />
                        <div>
                            <a href="../../Gallery.aspx"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Header,SeeMore %>"></asp:Label></a>
                        </div>
                    
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
                </div>

    </asp:Panel>
