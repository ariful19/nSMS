using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_AccountViewer_RptCostofGoods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReport();
        }
    }

    private void LoadReport()
    {


        Response.Redirect("../../Report/AccountViewer/AccountReportView.aspx?query=" + "&report=costOfGoods");
    }
}