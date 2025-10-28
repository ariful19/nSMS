<%@ Page Title="<%$ Resources:Application,LeaveApplication %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="LeaveApplication.aspx.cs" Inherits="Pages_Administration_LeaveApplication" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="tbxName" runat="server" TextMode="SingleLine" CssClass="form-control" Placeholder="Enter your name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="tbxName" ValidationGroup="save"
                            ErrorMessage="Enter your name.">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,TeacherPINNo %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="tbxPinCode" runat="server" TextMode="SingleLine" CssClass="form-control" Placeholder="Enter your pin code."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None" ControlToValidate="tbxPinCode" ValidationGroup="save"
                            ErrorMessage="Enter your name.">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Designation %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-md-5">
                        <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Designation" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-md-5">
                        <asp:TextBox ID="tbxSubject" runat="server" TextMode="SingleLine" CssClass="form-control" Placeholder="Enter type of absense"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None" ControlToValidate="tbxSubject" ValidationGroup="save"
                            ErrorMessage="Enter type of absense.">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,FromDate %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-md-3">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <asp:TextBox ID="tbxFromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxFromDate" SelectedDate='<%# DateTime.Now %>' CssClass=""
                                TargetControlID="tbxFromDate">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter From Date." ControlToValidate="tbxFromDate">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,ToDate %>"></asp:Label><span class="required">*</span></label>
                    <div class="col-md-3">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <asp:TextBox ID="tbxToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxToDate" SelectedDate='<%# DateTime.Now %>' CssClass=""
                                TargetControlID="tbxToDate">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter To Date." ControlToValidate="tbxToDate">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-md-2">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Description %>"></asp:Label></label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server" ID="tbxDetails" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>
                        <cc1:HtmlEditorExtender ID="htmlEditorExtender1" TargetControlID="tbxDetails" runat="server" >
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
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-sm-10">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:Application,Submit %>" OnClick="btnSave_Click" ValidationGroup="save" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

