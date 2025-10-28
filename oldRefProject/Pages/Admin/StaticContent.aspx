<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="StaticContent.aspx.cs" Inherits="Pages_Admin_StaticContent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-md-10 col-sm-10 col-xs-10">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                <asp:HiddenField ID="hdnID" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">Page Name</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlPage" runat="server" DataTextField="PageName" DataValueField="Id" CssClass="form-control dropdown"
                    OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>

            </div>
        </div>
        <div class="form-group">
            <asp:UpdatePanel>
                <ContentTemplate>
                    <label for="inputEmail3" class="col-sm-2">Content</label>
                    <div class="col-sm-7">
                         <asp:TextBox runat="server" ID="tbxContent" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>
                        <cc1:HtmlEditorExtender ID="htmlEditorExtender1" TargetControlID="tbxContent" runat="server" >
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
               
        </Toolbar>   
                            </cc1:HtmlEditorExtender>
                        
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlPage" />
                </Triggers>
            </asp:UpdatePanel>
        </div>


        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-11">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>

