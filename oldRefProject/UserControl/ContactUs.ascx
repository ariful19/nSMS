<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactUs.ascx.cs" Inherits="UserControl_ContactUs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="widget-main">
    <div class="widget-main-title">
        <h4 class="widget-title"><asp:Label ID="Label19" runat="server" Text="<%$ Resources:Header,ContactUs %>"></asp:Label></h4>
    </div>
    <div class="widget-inner">
        <div class="form-group">
            <asp:TextBox CssClass="form-control" ID="tbxName" runat="server" placeholder="Name">
                                            </asp:TextBox>
        </div>
        <div class="form-group">
           <asp:TextBox CssClass="form-control" ID="tbxEmail" runat="server" placeholder="Email">
                                            </asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="tbxEmail" ErrorMessage="Please Enter Correct Email" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="form-control" ID="tbxMobile" runat="server" placeholder="Mobile">
                                            </asp:TextBox>
              <cc1:FilteredTextBoxExtender ID="Filtered1" runat="server"
                                            Enabled="True" TargetControlID="tbxMobile" FilterType="Custom" ValidChars="0123456789.">
                                        </cc1:FilteredTextBoxExtender>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="form-control" ID="tbxSubject" runat="server" placeholder="Subject">
                                            </asp:TextBox>
        </div>
        <div class="form-group">
           <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="3" ID="tbxMessage" runat="server" placeholder="Message">
                                            </asp:TextBox>
        </div>
        <asp:Button ID="btnSubmit" CssClass="btn btn-success" Text="<%$ Resources:Application,Submit %>" runat="server" OnClick="btn_OnClick" />
    </div>
</div>
<script type="text/javascript">
    function isEmail(tbxEmail) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(tbxEmail);
    }
</script>


