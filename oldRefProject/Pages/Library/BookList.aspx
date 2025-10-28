<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="BookList.aspx.cs" Inherits="Pages_Library_BookList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-success">
                <div class="panel-body">
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:Label ID="Label1" runat="server" Text="Title"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label2" runat="server" Text="Author"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label5" runat="server" Text="Category"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label7" runat="server" Text="Sub-Category"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label4" runat="server" Text="Language"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label6" runat="server" Text="Edition"></asp:Label></th>
                                        <th>
                                            <asp:Label ID="Label8" runat="server" Text="Publisher"></asp:Label></th>
                                        <th class="action">
                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="action"><%#Eval("Title") %></td>
                                <td><%#Eval("Author") %></td>
                                <td><%#Eval("Category") %></td>
                                <td><%#Eval("SubCategory") %></td>
                                <td><%#Eval("Language") %></td>
                                <td><%#Eval("Edition") %></td>
                                <td><%#Eval("Publisher") %></td>
                                <td class="action">
                                    <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("Id")%>' CssClass="fa fa-2x fa-eye" ToolTip="View"/>
                                    <asp:LinkButton ID="btnEdit" runat="server" PostBackUrl='<%#String.Concat("~/Pages/Library/AddBook.aspx?Id=",Eval("Id")) %>' CssClass="fa fa-2x fa-edit" ToolTip="Edit" />
                                    <asp:LinkButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' CssClass="fa fa-2x fa-trash-o" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
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

    <div class="row">
        <div class="modal fade" tabindex="-1" id="bookDetails" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header bg-danger">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Book Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-9">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label5" runat="server" Text="Category"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label2" runat="server" Text="Sub-Category"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblSubCategory" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label3" runat="server" Text="Country"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label4" runat="server" Text="Language"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblLanguage" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label10" runat="server" Text="Publisher"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblPublisher" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-5">
                                                    <asp:Label ID="Label11" runat="server" Text="Edition"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblEdtion" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label8" runat="server" Text="ISBN"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblISBN" runat="server" placeholder="Enter ISBN"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label9" runat="server" Text="Volume"></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblVolume" runat="server" placeholder="Enter Volume No."></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label14" runat="server" Text="Self No."></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblSelfNo" runat="server" placeholder="Enter self no."></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label15" runat="server" Text="Cell No."></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblCellNo" runat="server" placeholder="Enter cell no"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label16" runat="server" Text="Is avilable"></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblAvailable" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group" id="stock">
                                                <label for="inputEmail3" class="col-sm-4">
                                                    <asp:Label ID="Label17" runat="server" Text="Stock"></asp:Label></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblStock" runat="server" placeholder="Enter no of book available"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2">
                                                    <asp:Label ID="Label1" runat="server" Text="Title"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblTitle" runat="server" placeholder="Enter Title"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2">
                                                    <asp:Label ID="Label6" runat="server" Text="Author"></asp:Label><span class="red">*</span></label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblAuthor" runat="server" placeholder="Enter Author"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2">
                                                    <asp:Label ID="Label7" runat="server" Text="Subtitle"></asp:Label></label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblSubTitle" runat="server" placeholder="Enter sub title"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2">
                                                    <asp:Label ID="Label12" runat="server" Text="Key words"></asp:Label></label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblKeyWord" runat="server" placeholder="Enter Key Word"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2">
                                                    <asp:Label ID="Label13" runat="server" Text="Description"></asp:Label></label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblDescription" TextMode="MultiLine" Rows="7" runat="server" placeholder="Enter Short Description"></asp:Label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="col-sm-12 text-center">
                                    <div class="panel panel-success">
                                        <div class="panel-body">
                                            <asp:Image ID="imgCover" runat="server" CssClass="img img-responsive"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        function openModal() {
            $('#bookDetails').modal('show');
        }
    </script>
</asp:Content>

