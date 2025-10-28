<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPageContent.aspx.cs" MasterPageFile="~/MasterPage/AdminMaster.master" Inherits="Pages_Menu_EditPageContent" %>

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
        <b>Page Name :</b>&nbsp;&nbsp;
        <asp:TextBox ID="txtPageName" runat="server"></asp:TextBox>    
        <asp:HiddenField ID="hdfPageContentId" runat="server" />   
    </div>
     <div class="col-md-12">
        <b>Page Title :</b>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtPageTitle" runat="server"></asp:TextBox>       
    </div>
    <br />
    <br />
    <div class="col-md-12">
        <div class="form-group">
            <b>
                <asp:Label ID="Label2" runat="server" Text="Description In English"></asp:Label><span>:</span>
            </b>

            <asp:Label ID="lblcharecter" runat="server" ClientIDMode="Static"></asp:Label>
<%--            <cc1:Editor ID="tbxDetails" runat="server" />--%>
           
            
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
                <asp:Label ID="Label1" runat="server" Text="Description In Bangla"></asp:Label><span>:</span>
            </b>

            <asp:Label ID="Label3" runat="server" ClientIDMode="Static"></asp:Label>
<%--           <cc1:Editor ID="tbxDetailsEditorInBangla" runat="server" />--%>
            
      <asp:TextBox runat="server"  TextMode="MultiLine" ID="tbxDetailsEditorInBangla" Width="100%" Height="300"/>
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
    
    <div class="form-group">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptPageContent" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,contentname %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,contenttitle %>"></asp:Label></th>
                                              <th>
                                              <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("PageName") %></td>
                                        <td><%#Eval("PageTitel") %></td>
                                        
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("PageContentID")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                          <%--  <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("PageContentID")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('If you want to delete a parent ensure that all child will be deleted\n\n\r\t\t\t\t\t\tAre you sure? ')" />--%>

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
</asp:Content>
