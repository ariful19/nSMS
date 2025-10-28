<%@ Page Title="<%$ Resources:Application,NoticeDetails %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Pages_Notification_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Notification Details</h3>
                       
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                        <div class="mailbox-read-info">
                            <h2><asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                            <h4><asp:Label ID="lblShortDes" runat="server"></asp:Label></h4>
                            <h5>Notice By: <asp:Label ID="lblFrom" runat="server"></asp:Label>
                 
                                <span class="mailbox-read-time pull-right">Notice Date: <asp:Label ID="lblDate" runat="server"></asp:Label></span></h5>
                        </div>
                        <!-- /.mailbox-controls -->
                        <div class="mailbox-read-message">
                            <asp:Literal ID="litContent" runat="server"></asp:Literal>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

