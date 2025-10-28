<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="UserControl_Menu" %>
<div class="responsive-navigation visible-sm visible-xs">
    <a href="#" class="menu-toggle-btn">
        <i class="fa fa-bars"></i>
    </a>
    <div class="responsive_menu">
        <ul class="main_menu">
            <li><a href="../../Default.aspx">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Header,Home %>"></asp:Label></a></li>

            <asp:Repeater runat="server" ID="rptMenu" OnItemDataBound="rptCategory_OnItemDataBound">
                <ItemTemplate>
                    <li class="active">

                        <a href='<%#Eval("URL") %>'>
                            <span><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString() %></span>

                        </a>
                        <asp:HiddenField runat="server" ID="hdnValue" Value='<%#Eval("MenuID")%>' />
                        <asp:Repeater runat="server" ID="rptChild">
                            <HeaderTemplate>

                                <ul class="sub-menu">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <a href='<%#Eval("URL") %>?id=<%#Eval("PageContentID") %>'><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString()  %></a>

                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>

                            </FooterTemplate>
                        </asp:Repeater>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

            <li><a href="../../Pages/User/Result.aspx">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Header,Result %>"></asp:Label></a>
            </li>

            <li><a href="../../Gallery.aspx">
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Header,Gallery %>"></asp:Label></a>
            </li>
            <li><a href="../../Pages/User/EventCalendar.aspx">
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Header,Event %>"></asp:Label></a>
            </li>
            <li><a href="../../Pages/User/ClassNotes.aspx">
                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Header,ClassNote %>"></asp:Label></a>
            </li>
            <li><a href="../../Pages/User/ContactUs.aspx">
                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Header,ContactUs %>"></asp:Label></a></li>
        </ul>
        <!-- /.main_menu -->
        <ul class="social_icons">
            <li><a href="#"><i class="fa fa-facebook"></i></a></li>
            <li><a href="#"><i class="fa fa-twitter"></i></a></li>
            <li><a href="#"><i class="fa fa-pinterest"></i></a></li>
            <li><a href="#"><i class="fa fa-google-plus"></i></a></li>
            <li><a href="#"><i class="fa fa-rss"></i></a></li>
        </ul>
        <!-- /.social_icons -->
    </div>
    <!-- /.responsive_menu -->
</div>
<!-- /responsive_navigation -->
<header class="site-header">
    <section class="container">
        <div class="row">
            <div class="col-md-3 header-left">
                <p style="font-size: 11px; opacity: 0.9;">
                    <i class="fa fa-phone"></i>
                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Header,address1 %>"></asp:Label>
                </p>
                <p style="font-size: 11px; opacity: 0.9;">
                    <i class="fa fa-envelope"></i>
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Header,address2 %>"></asp:Label>
                </p>
                <p style="font-size: 11px; opacity: 0.9;">
                    <i class="fa fa-home"></i>
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Header,address3 %>"></asp:Label>
                </p>
            </div>
            <!-- /.header-left -->
            <div class="col-md-5 text-center">
                <div class="logo">
                    <a href="../../Default.aspx" title="School" rel="home">
                        <img src="../../Images/Common/School_logo.png" height="70" alt="Your School Name" /><h3 style="font-size: 26px">
                            <%-- <asp:Label ID="lblSchool" runat="server"></asp:Label>--%>
                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Header,DPI %>"></asp:Label></h3>
                    </a>
                </div>
            </div>
            <!-- /.col-md-4 -->
            <div class="col-sm-1"></div>
            <div class="col-sm-3 header-right">
                <ul class="small-links">
                    <li>
                        <asp:LinkButton ID="lnkBangla" runat="server" Style="color: #fff" Text="বাংলা" OnClick="lnkBangla_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lnkEnglish" runat="server" Style="color: #fff" Text="English" OnClick="lnkEnglish_Click"></asp:LinkButton>
                    </li>
                    <li><a href="../../Login.aspx">
                        <asp:Label ID="Label17" runat="server" Style="color: #fff" Text="<%$ Resources:Header,Login %>"></asp:Label></a></li>
                </ul>
                <div class="search-form">
                    <div id="imaginary_container">
                        <div class="input-group stylish-input-group">
                            <asp:TextBox ID="tbxHeaderSearch" runat="server" placeholder="Search the site..." CssClass="form-control" />
                            <span class="input-group-addon">
                                <button type="submit">
                                    <span class="fa fa-search-plus"></span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.header-right -->
        </div>
    </section>
    <!-- /.container -->
    <div class="nav-bar-main" role="navigation">
        <section class="container">
            <nav class="main-navigation clearfix visible-md visible-lg" role="navigation">


                <ul class="main-menu sf-menu">

                    <li class="active"><a href="../../Default.aspx">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Header,Home %>"></asp:Label></a>

                    </li>

                    <asp:Repeater runat="server" ID="rptParent" OnItemDataBound="rptCategory_OnItemDataBound">
                        <ItemTemplate>
                            <li class="active">

                                <a href='<%#Eval("URL") %>'>
                                    <span><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString() %></span>

                                </a>
                                <asp:HiddenField runat="server" ID="hdnValue" Value='<%#Eval("MenuID")%>' />
                                <asp:Repeater runat="server" ID="rptChild">
                                    <HeaderTemplate>
                                        <ul class="sub-menu">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href='<%#Eval("URL") %>?id=<%#Eval("PageContentID") %>'><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString()  %></a>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li><a href="../../Pages/User/Result.aspx">
                        <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Header,Result %>"></asp:Label></a>
                    </li>

                    <li><a href="../../Gallery.aspx">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Header,Gallery %>"></asp:Label></a>
                    </li>
                    <li><a href="../../Pages/User/EventCalendar.aspx">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Header,Event %>"></asp:Label></a>
                    </li>
                    <li><a href="../../Pages/User/ClassNotes.aspx">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Header,ClassNote %>"></asp:Label></a>
                    </li>
                    <li><a href="../../Pages/User/ContactUs.aspx">
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Header,ContactUs %>"></asp:Label></a></li>
                </ul>
            </nav>
            <!-- /.main-navigation -->
        </section>
        <!-- /.container -->
    </div>
    <!-- /.nav-bar-main -->

</header>
