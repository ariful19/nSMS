<%@ Page Title="<%$ Resources:Header,ContactUs %>" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Pages_User_ContactUs" %>

<%@ Register TagName="Contact" TagPrefix="UC" Src="~/UserControl/ContactUs.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="col-md-6">
                <p>
                    <i class=""></i>College Campus: 1132/C, Baitul Aman Housing Society, Adabor,Dhaka.<br />

                                            School Campus(Adabor): 18 ,Adorsho Chaya Nir Housing, Ring Road, Adabor, Dhaka-1207.<br />

                                            Zigatola Campus: 41/7/1, Natun Rasta, Kacha Bazar, Zigatola,Dhaka-1209.
                </p>
                <p><i class="fa fa-phone"></i>+8801912486153, +8801912486170</p>
                <p><i class="fa fa-envelope"></i><a href="#">E-mail: Info@queenscollege.edu.bd</a></p>
            </div>
            <div style="width: 100%; max-width: 100%; overflow: hidden; height: 450px; color: red;">
                <div id="embedded-map-canvas" style="height: 100%; width: 100%; max-width: 100%;">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d29214.013064165996!2d90.34804003866579!3d23.756234649399616!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3755bf4a11892839%3A0x1a06b12214c33d85!2sQueen&#39;s+School+%26+College!5e0!3m2!1sen!2s!4v1505126379658" width="100%" height="100%" frameborder="0" style="border:0" allowfullscreen></iframe>
                </div>
                <a class="google-map-enabler" rel="nofollow" href="http://www.interserver-coupons.com" id="grab-map-data">interserver coupons</a><style>                                                                                                                                                                                                                                                                                  </style>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
    <UC:Contact ID="contact" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

<%--    <iframe src="https://www.google.com.bd/maps/embed/place/Nanosoft/@23.7875222,90.4256182,19z/data=!4m5!3m4!1s0x0:0xa5cab660f3654a94!8m2!3d23.7876292!4d90.4255801" width="100%" height="450" frameborder="0" style="border: 0" allowfullscreen></iframe> 
    https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3650.8653765017307!2d90.4256182!3d23.7875222!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3755c7be0ac36397%3A0x84eef6c29457ce13!2sThana+Rd%2C+Dhaka!5e0!3m2!1sen!2sbd!4v1455772958191 --%>




