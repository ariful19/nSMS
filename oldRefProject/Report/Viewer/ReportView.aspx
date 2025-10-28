<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportView.aspx.cs" Inherits="Report_Designer_ReportView" %>

<!DOCTYPE html>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/jscript" src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script>

        $(document).ready(function () {            
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <CR:CrystalReportViewer ID="CRV" runat="server" Width="100%" HasCrystalLogo="False"
                CssClass="LeftAlign" EnableParameterPrompt="False" HasGotoPageButton="False"
                BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" HasSearchButton="False"
                HasDrillUpButton="False" />
        </div>
    </form>
</body>
</html>
