<%@ Page Title="<%$ Resources:Application,ClassNoteDetails %>" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeFile="ClassNoteDetails.aspx.cs" Inherits="Pages_User_ClassNoteDetails" EnableEventValidation="false"%>

<%@ Register Src="~/UserControl/SchoolHeader.ascx" TagPrefix="uc" TagName="SchoolHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="pdf" runat="server">
        <div class="panel panel-success   pt-10">
            <div class="panel-body">
                <div style="text-align: center">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <asp:Panel ID="pnlResult" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-success blog-list-post">
                                <div class="panel-body" style="padding: 16px;">
                                    <div id="logoDiv" runat="server">
                                         <uc:SchoolHeader runat="server" ID="SchoolHeader"/>
                                    </div>                                   
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <asp:Label ID="lblTitle" runat="server" class="col-sm-12 text-center" Font-Bold="true" Font-Size="30px" ForeColor="#cc0099"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Literal ID="litDetails" runat="server"></asp:Literal>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer" style="padding: 16px;">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-flat btn-success" OnClick="btnGenerateReport_Click" runat="server" Text="<%$ Resources:Application,Print %>" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
</asp:Content>
