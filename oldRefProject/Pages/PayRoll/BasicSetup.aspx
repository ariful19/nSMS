<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="BasicSetup.aspx.cs" Inherits="Pages_PayRoll_BasicSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label8" runat="server" Text="Office Start Time"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxStartTime" runat="server" placeholder="Enter Casual Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Casual Leave" ControlToValidate="tbxStartTime">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label9" runat="server" Text="Office End Time"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxEndTime" runat="server" placeholder="Enter Casual Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Casual Leave" ControlToValidate="tbxEndTime">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label5" runat="server" Text="Casual Leave (Days)"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxCasualLeave" runat="server" placeholder="Enter Casual Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Casual Leave" ControlToValidate="tbxCasualLeave">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label1" runat="server" Text="Sick Leave(Days)"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxSickLeave" runat="server" placeholder="Enter Sick Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Sick Leave" ControlToValidate="tbxSickLeave">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label2" runat="server" Text="Maximum Overtime(Hours)"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxMaxOvertime" runat="server" placeholder="Enter Sick Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Sick Leave" ControlToValidate="tbxMaxOvertime">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label3" runat="server" Text="Minimum Overtime(Hours)"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxMinOvertime" runat="server" placeholder="Enter Sick Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Sick Leave" ControlToValidate="tbxMinOvertime">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label4" runat="server" Text="Overtime Payment(per Hour)"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxOvertimePayment" runat="server" placeholder="Enter Sick Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Sick Leave" ControlToValidate="tbxOvertimePayment">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label6" runat="server" Text="Late Present allows(Days)"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxLatePresentAllow" runat="server" placeholder="Enter Sick Leave" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Sick Leave" ControlToValidate="tbxLatePresentAllow">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label7" runat="server" Text="Leave Forfit"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:RadioButtonList ID="rdLeaveForfit" runat="server" RepeatColumns="2" CssClass="FormatRadioButtonList">
                            <asp:ListItem Value="1" Text="Paid"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Add to next"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

