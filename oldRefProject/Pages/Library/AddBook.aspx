<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="AddBook.aspx.cs" Inherits="Pages_Library_AddBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="panel panel-success">
        <div class="panel-body">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                <asp:HiddenField ID="hdnID" runat="server" />
            </div>
            <div class="col-sm-9">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label5" runat="server" Text="Category"></asp:Label><span class="red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCategory" runat="server" DataValueField="Id" DataTextField="Category" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="Update" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4">
                                            <asp:Label ID="Label2" runat="server" Text="Sub-Category"></asp:Label><span class="red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" DataValueField="Id" DataTextField="SubCategory" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlCategory" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label3" runat="server" Text="Country"></asp:Label><span class="red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCountry" runat="server" DataValueField="Id" DataTextField="Country" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label4" runat="server" Text="Language"></asp:Label><span class="red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" DataValueField="Id" DataTextField="Language" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label10" runat="server" Text="Publisher"></asp:Label><span class="red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlPublisher" runat="server" DataValueField="Id" DataTextField="Publisher" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label11" runat="server" Text="Edition"></asp:Label><span class="red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlEdtion" runat="server" DataValueField="Id" DataTextField="Edition" CssClass="form-control"></asp:DropDownList>
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
                                    <asp:TextBox ID="tbxISBN" runat="server" placeholder="Enter ISBN" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter ISBN" ControlToValidate="tbxISBN">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label9" runat="server" Text="Volume"></asp:Label></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbxVolume" runat="server" placeholder="Enter Volume No." CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label14" runat="server" Text="Self No."></asp:Label></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbxSelfNo" runat="server" placeholder="Enter self no." CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label15" runat="server" Text="Cell No."></asp:Label></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbxCellNo" runat="server" placeholder="Enter cell no" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label16" runat="server" Text="Is avilable"></asp:Label></label>
                                <div class="col-sm-8">
                                    <asp:CheckBox ID="chkAvailable" runat="server" ClientIDMode="Static"></asp:CheckBox>
                                </div>
                            </div>
                            <div class="form-group" id="stock">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label17" runat="server" Text="Stock"></asp:Label></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbxStock" runat="server" placeholder="Enter no of book available" CssClass="form-control"></asp:TextBox>
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
                                    <asp:TextBox ID="tbxTitle" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Title" ControlToValidate="tbxTitle">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label6" runat="server" Text="Author"></asp:Label><span class="red">*</span></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tbxAuthor" runat="server" placeholder="Enter Author" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Author" ControlToValidate="tbxAuthor">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label7" runat="server" Text="Subtitle"></asp:Label></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tbxSubTitle" runat="server" placeholder="Enter sub title" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label12" runat="server" Text="Key words"></asp:Label></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tbxKeyWord" runat="server" placeholder="Enter Key Word" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label13" runat="server" Text="Description"></asp:Label></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tbxDescription" TextMode="MultiLine" Rows="7" runat="server" placeholder="Enter Short Description" CssClass="form-control"></asp:TextBox>

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
                            <asp:Image ID="imgCover" runat="server" ClientIDMode="Static" class="img-thumbnail" ImageUrl="~/Images/Common/no-photo.jpg"/>
                            <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="panel-footer">
                            <span class="btn btn-info btn-file"><asp:Label ID="lblBrowse" runat="server" Text="Browse"></asp:Label>
                              <asp:FileUpload ID="fuCoverPhoto" runat="server" onchange="readURL(this)" />
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                OnClick="btnSave_Click" />
            <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                OnClick="btnEdit_Click" />
            <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                OnClick="btnReset_Click" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#FileUpload1").change(function () {
                fuCoverPhoto(this);
            });
            if ($("#chkAvailable").checked) {
                $("#stock").show();
                alert("s dfs");
            }
            else {
                $("#stock").show();
            }
            $("#chkAvailable").click(function () {
                if (this.checked) {
                    $("#stock").slideDown();
                }
                else {
                    $("#stock").slideUp();
                }
            });
        });

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgCover').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>
</asp:Content>

