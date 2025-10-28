<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChairmanContent.aspx.cs" MasterPageFile="~/MasterPage/AdminMaster.master" Inherits="Pages_Menu_ChairmanContent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function onContentsChange() {
            alert('contents changed');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="col-lg-12 col-md-12">
        <div class="col-lg-6 col-md-6">
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label91" runat="server" Text="<%$ Resources:Application,Title %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtTitle" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                        <asp:HiddenField ID="hdnContentId" runat="server" />

                    </div>
                </div>
            </div>
            
          
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,TitleBangla %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtTitleBangla" runat="server" placeholder="Enter Title banlga" CssClass="form-control"></asp:TextBox>


                    </div>
                </div>
            </div>

            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,NameEng %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtName" runat="server" placeholder="Enter Name" CssClass="form-control"></asp:TextBox>


                    </div>
                </div>
            </div>
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,NameBan %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtNameBangla" runat="server" placeholder="Enter Name Bangla" CssClass="form-control"></asp:TextBox>


                    </div>
                </div>
            </div>
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>


                    </div>
                </div>
            </div>
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,DesignationBan %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDesignationBangla" runat="server" placeholder="Enter Designation Bangla" CssClass="form-control"></asp:TextBox>


                    </div>
                </div>
            </div>
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Address %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,AddressBan %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtAddressBanlga" runat="server" placeholder="Enter Address bangla" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6 col-md-6">
            <div class="form-vertical">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,Photo %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-8">
                        <asp:Image runat="server" ID="image" alt="Photo" Height="140" Width="140" />
                        <asp:FileUpload ID="uploderPhoto" runat="server" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="form-group">
            <b>
                <asp:Label ID="Label2" runat="server" Text="Description In English"></asp:Label><span>:</span>
            </b>
            <asp:Label ID="lblcharecter" runat="server" ClientIDMode="Static"></asp:Label>
            <asp:TextBox runat="server"
                ID="tbxDetails" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>
            <cc1:HtmlEditorExtender ID="htmlEditorExtender1" OnImageUploadComplete="ajaxFileUpload_OnUploadComplete" TargetControlID="tbxDetails" runat="server">
                <Toolbar>
                    <cc1:Undo />
                    <cc1:Redo />
                    <cc1:Bold />
                    <cc1:Italic />
                    <cc1:Underline />
                    <cc1:StrikeThrough />
                    <cc1:Subscript />
                    <cc1:Superscript />
                    <cc1:JustifyLeft />
                    <cc1:JustifyCenter />
                    <cc1:JustifyRight />
                    <cc1:JustifyFull />
                    <cc1:InsertOrderedList />
                    <cc1:InsertUnorderedList />
                    <cc1:CreateLink />
                    <cc1:UnLink />
                    <cc1:RemoveFormat />
                    <cc1:SelectAll />
                    <cc1:UnSelect />
                    <cc1:Delete />
                    <cc1:Cut />
                    <cc1:Copy />
                    <cc1:Paste />
                    <cc1:BackgroundColorSelector />
                    <cc1:ForeColorSelector />
                    <cc1:FontNameSelector />
                    <cc1:FontSizeSelector />
                    <cc1:Indent />
                    <cc1:Outdent />
                    <cc1:InsertHorizontalRule />
                    <cc1:HorizontalSeparator />
                    <cc1:InsertImage />
                </Toolbar>
            </cc1:HtmlEditorExtender>
        </div>
        <div class="form-group">
            <b>
                <asp:Label ID="Label1" runat="server" Text="Description In Bangla"></asp:Label><span>:</span>
            </b>
            <asp:Label ID="Label3" runat="server" ClientIDMode="Static"></asp:Label>
            <%--           <cc1:Editor ID="tbxDetailsEditorInBangla" runat="server" />--%>

            <asp:TextBox runat="server" TextMode="MultiLine" ID="tbxDetailsEditorInBangla" Width="100%" Height="300" />
            <cc1:HtmlEditorExtender ID="htmlEditorExtender2" OnImageUploadComplete="AjaxFileUpload1_OnUploadComplete" runat="server" TargetControlID="tbxDetailsEditorInBangla">
                <Toolbar>
                    <cc1:Undo />
                    <cc1:Redo />
                    <cc1:Bold />
                    <cc1:Italic />
                    <cc1:Underline />
                    <cc1:StrikeThrough />
                    <cc1:Subscript />
                    <cc1:Superscript />
                    <cc1:JustifyLeft />
                    <cc1:JustifyCenter />
                    <cc1:JustifyRight />
                    <cc1:JustifyFull />
                    <cc1:InsertOrderedList />
                    <cc1:InsertUnorderedList />
                    <cc1:CreateLink />
                    <cc1:UnLink />
                    <cc1:RemoveFormat />
                    <cc1:SelectAll />
                    <cc1:UnSelect />
                    <cc1:Delete />
                    <cc1:Cut />
                    <cc1:Copy />
                    <cc1:Paste />
                    <cc1:BackgroundColorSelector />
                    <cc1:ForeColorSelector />
                    <cc1:FontNameSelector />
                    <cc1:FontSizeSelector />
                    <cc1:Indent />
                    <cc1:Outdent />
                    <cc1:InsertHorizontalRule />
                    <cc1:HorizontalSeparator />
                    <cc1:InsertImage />
                </Toolbar>
            </cc1:HtmlEditorExtender>
        </div>
    </div>
    <br />
    <div class="col-md-12">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click" />
    </div>

</asp:Content>
