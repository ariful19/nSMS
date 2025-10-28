<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControl_Header" %>

<div class="col-lg-4 col-md-4 logo">
    Your School Name
</div>
<div class="col-lg-8 col-md-8" align="right">
    <nav class="navbar navbar-default" id="vin-navbar">
        <div class="">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

            </div>


            <div class="collapse navbar-collapse " id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav pull-right" id="vin-nav">
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink></li>
                    <li class="dropdown" runat="server" id="liMenuSetup">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Book <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
                            <li>
                                <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Pages/User/BookList.aspx">Book List</asp:HyperLink></li>
                            <li class="dropdown-submenu"><a href="#">Products 2</a>
                                <ul class="dropdown-menu">
                                    <li>
                                        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Pages/Admin/productadministration.aspx">PRODUCT LIST</asp:HyperLink></li>

                                    <li>
                                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Pages/Admin/ProductCategory.aspx">CATEGORY</asp:HyperLink></li>
                                    <li>
                                        <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/Pages/Admin/ProductSubCategory.aspx">SUB CATEGORY</asp:HyperLink></li>

                                </ul>
                            </li>

                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Pages/User/AboutUs.aspx">About</asp:HyperLink></li>

                    <li>
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Pages/User/ContactUs.aspx">Contact</asp:HyperLink></li>

                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container-fluid -->
    </nav>
</div>
