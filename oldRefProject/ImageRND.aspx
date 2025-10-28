<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageRND.aspx.cs" Inherits="ImageRND" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="attachmentUpload" runat="server" CssClass="btn btn-default" />

         <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="<%$ Resources:Application,Save %>" ValidationGroup="save" />
    </div>
    </form>
</body>
</html>
