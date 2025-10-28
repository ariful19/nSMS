<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Pages_Notification_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <link href="../../Styles/bootstrap.min.css" rel="stylesheet" />
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Inbox</h3>
                        <div class="box-tools pull-right">
                            <div class="has-feedback">
                                <input type="text" class="form-control input-sm" placeholder="Search Mail">
                                <span class="fa fa-search-plus form-control-feedback"></span>
                            </div>
                        </div>
                        <!-- /.box-tools -->
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                        <div class="table-responsive mailbox-messages">
                            <asp:Repeater ID="rptNotice" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-hover table-striped">
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr>
                                        <td class="mailbox-star"><a href="#"><i class="fa fa-star text-yellow"></i></a></td>
                                        <td class="mailbox-name">
                                            <a href="<%#String.Concat("../../Pages/Notification/View.aspx?Id=",Eval("ID")) %>">
                                                <%#Eval("Title") %>
                                            </a>
                                        </td>
                                        <td class="mailbox-subject">
                                            <%#Eval("ShortDescription") %>
                                        </td>
                                        <td class="mailbox-attachment">
                                            <%#Eval("CreatedBy") %>
                                        </td>
                                        <td class="mailbox-date">
                                            <%#Eval("Date") %>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

