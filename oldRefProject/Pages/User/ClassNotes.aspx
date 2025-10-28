<%@ Page Title="<%$ Resources:Header,ClassNote %>" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeFile="ClassNotes.aspx.cs" Inherits="Pages_User_ClassNotes" %>

<%@ Register Src="~/UserControl/SchoolHeader.ascx" TagPrefix="uc" TagName="SchoolHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="panel panel-success">
        <div class="panel-body" id="criteria">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="SubjectName" DataValueField="SubjectToClassId" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="showResult">
                        <div class="col-sm-offset-4 col-sm-6">
                            <asp:Button ID="btnSearch" CssClass="btn btn-flat btn-success" OnClick="btnSearch_Click" runat="server" Text="<%$ Resources:Application,Search %>"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-success   pt-10">
        <div class="panel-body">
            <asp:Repeater ID="rptNotes" runat="server">
                <HeaderTemplate>
                    <table id="example1" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Title</th>
                                <th>Date</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HyperLink ID="HyperLink2" runat="server" Text='<%#Eval("Title") %>' NavigateUrl='<%# String.Concat("~/Pages/user/ClassNoteDetails.aspx?ID=", Eval("Id")) %>'></asp:HyperLink></td>
                        <td><%#Convert.ToDateTime(Eval("Date")).ToString("dd-MMM-yyyy") %></td>
                        <td><%#Eval("ShortDescription") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
           </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
