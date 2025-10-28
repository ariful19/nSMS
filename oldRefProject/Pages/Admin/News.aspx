<%@ Page Title="<%$ Resources:Application,NewsSetup %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="News.aspx.cs" Inherits="Pages_Admin_News" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    

       <div class="tab-content">
        <div id="home" class="tab-pane fade in active pt-10">
            <div class='<%=Common.SessionInfo.Panel %>'>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="col-lg-12 col-md-12">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Title %>"></asp:Label></label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Title" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label7" runat="server" Text="Title In Bangla"></asp:Label></label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="tbxNameBangla" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Title" ControlToValidate="tbxNameBangla">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Date %>"></asp:Label></label>
                                <div class="col-sm-9">
                   

                                    <asp:TextBox ID="tbxDate" runat="server" placeholder="Enter Date" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Date" ControlToValidate="tbxDate">*</asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="tbxDate" SelectedDate='<%# DateTime.Now %>'
                                        TargetControlID="tbxDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,ShortDescription %>"></asp:Label></label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="tbxShortDescription" runat="server" placeholder="Enter Short Description" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Short Description" ControlToValidate="tbxShortDescription">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label8" runat="server" Text="Short Description In Bangla"></asp:Label></label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="tbxShortDescriptionInBangla" runat="server" placeholder="Enter Short Description" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Short Description" ControlToValidate="tbxShortDescriptionInBangla">*</asp:RequiredFieldValidator>
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
                                    <asp:Image runat="server" ID="imageNews" Height="140" Width="140" />
                                    <asp:FileUpload ID="uploderPhoto" runat="server" CssClass="btn btn-default" />
                                    <p class="help-block">Note: All Images .png .jpeg .jpg are allowed.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">

                        <b>
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Details %>"></asp:Label><span>:</span>
                        </b>

                        <asp:TextBox runat="server"
                            ID="tbxDetails" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>
                        <cc1:HtmlEditorExtender ID="htmlEditorExtender1" TargetControlID="tbxDetails" runat="server">
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
                    <div class="form-group">
                        <b>
                            <asp:Label ID="Label11" runat="server" Text="Details in Bangla"></asp:Label><span>:</span>
                        </b>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="tbxDetailsBanglaEditor" Width="100%" Height="300" />
                        <cc1:HtmlEditorExtender ID="htmlEditorExtender2" TargetControlID="tbxDetailsBanglaEditor" runat="server">
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
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-11">
                            <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Edit %>" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                                OnClick="btnEdit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                                OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="box">
                                <div class="box-body">
                                    <asp:Repeater ID="rptYear" runat="server">
                                        <HeaderTemplate>
                                            <table id="example1" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Title %>"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Description %>"></asp:Label></th>
                                                          <th>
                                                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,ShortDescriptionInBangla %>"></asp:Label></th>
                                                        <th class="action">
                                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("Title") %></td>
                                                <td><%#Eval("ShortDescription") %></td>
                                                <td><%#Eval("ShortDescriptionInBangla") %></td>
                                                <td class="action">
                                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
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
        </div>
                </div>
            </div>
           </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>


