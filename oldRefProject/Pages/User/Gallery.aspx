<%@ Page Title="Gallery" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Site.master" CodeFile="Gallery.aspx.cs" Inherits="Pages_User_Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlGallery" runat="server" BorderColor="Red">
        <%-- <asp:ImageMap ID="ImageMap1" runat="server"></asp:ImageMap>--%>
        <div class="row">
            <div class="col-lg-12 col-xs-12 ">
                <div class="panel panel-default">

                    <div class="panel-body">

                        <table align="center" class="table-responsive" style="width: 95%;">
                            <tr>
                                <td colspan="2">
                                    <asp:DataList ID="dlImages" runat="server" RepeatColumns="3" RepeatLayout="Flow" HorizontalAlign="Left" RepeatDirection="Horizontal" CellPadding="5">
                                        <ItemTemplate>
                                            <a id="imageLink" href='<%# Eval("ImageName","~/Images/gallery/{0}") %>' title='<%#Eval("ImageDescription") %>'
                                                runat="server">
                                                <asp:Image ID="Image1" ImageUrl='<%# Bind("ImageName", "~/Images/gallery/{0}") %>'
                                                    runat="server" Width="150" Height="100" />
                                            </a>
                                            <div>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                            VerticalAlign="Bottom" />
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!-- /.panel -->
            </div>
            <!-- /.col-lg-12 -->
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
</asp:Content>

