<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutUsContent.aspx.cs" MasterPageFile="~/MasterPage/AdminMaster.master" Inherits="Pages_Menu_AboutUsContent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script>
        function onContentsChange() {
            alert('contents changed');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    
    <div class="col-md-12">
        <div class="form-group">
            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                <asp:ListItem Value="0">---Select---</asp:ListItem>
                <asp:ListItem Value="1">About Us</asp:ListItem>
                <asp:ListItem Value="2">Special Facilities</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            
            <b>
                <asp:Label ID="Label2" runat="server" Text="English Text"></asp:Label><span>:</span>
            </b>

            <asp:Label ID="lblcharecter" runat="server" ClientIDMode="Static"></asp:Label>

           <asp:HiddenField ID="hdnContentId" runat="server"/>
            
        <asp:TextBox runat="server"
        ID="tbxDetails" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>
    <cc1:HtmlEditorExtender  ID="htmlEditorExtender1" OnImageUploadComplete="ajaxFileUpload_OnUploadComplete" TargetControlID="tbxDetails"  runat="server">
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
                <asp:Label ID="Label1" runat="server" Text="Bangla Text"></asp:Label><span>:</span>
            </b>

            <asp:Label ID="Label3" runat="server" ClientIDMode="Static"></asp:Label>

            
      <asp:TextBox runat="server"  TextMode="MultiLine" ID="tbxDetailsEditorInBangla" Width="100%" Height="300"></asp:TextBox>
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
