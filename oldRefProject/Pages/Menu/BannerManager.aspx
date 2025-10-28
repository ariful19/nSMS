<%@ Page Title="<%$ Resources:Application,BannerManager %>" Language="C#" AutoEventWireup="true" CodeFile="BannerManager.aspx.cs" MasterPageFile="~/MasterPage/AdminMaster.master" Inherits="Pages_Menu_BannerManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:UpdatePanel ID="InvoiceUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-xs-12 ">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>Banner Manager </strong>
                        </div>
                        <div class="panel-body">

                            <table align="center" class="table-responsive" style="width: 95%;">
                                <tr>
                                    <td colspan="2">
                                        <asp:DataList ID="dlImages" runat="server" RepeatColumns="3" RepeatLayout="Flow" HorizontalAlign="Left" RepeatDirection="Horizontal" CellPadding="5">
                                            <ItemTemplate>
                                                <a id="imageLink" href='<%# Eval("ImageName","~/Images/Banner/{0}") %>' title='<%#Eval("ImageDescription") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image1" ImageUrl='<%# Bind("ImageName", "~/Images/Banner/{0}") %>'
                                                        runat="server" Width="150" Height="100" />
                                                </a>
                                                <div>

                                                    <asp:CheckBox ID="chkActive" Text="Is Active" Checked='<%# bool.Parse(Eval("ImageIsActive").ToString()) %>'
                                                        runat="server" />
                                                    &nbsp; 
                                <asp:ImageButton ID="ibDelete" Height="20px" Width="20px" ToolTip="Delete" ImageUrl="~/Images/Common/dltgrd.png"
                                    runat="server" CommandArgument='<%# Bind("GalleryImageID") %>' OnClick="ibDelete_Click" />
                                                    &nbsp; 
                                <asp:ImageButton ID="ibSave" Height="20px" Width="20px" ToolTip="Save" ImageUrl="~/Images/Common/oksmal.jpg"
                                    runat="server" CommandArgument='<%# Bind("GalleryImageID") %>' OnClick="ibSave_Click" />
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle BorderColor="Brown" BorderStyle="dotted" BorderWidth="3px" HorizontalAlign="Center"
                                                VerticalAlign="Bottom" />
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Is Active ?
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Upload Banner Image :
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fileuploadimages" CssClass="browse btn btn-default input-lg" ClientIDMode="Static" runat="server" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>Image Title :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Image Description :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-default" type="button" Text="Upload Image" OnClick="btnSubmit_Click" />

                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="panel-footer">
                        </div>
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
