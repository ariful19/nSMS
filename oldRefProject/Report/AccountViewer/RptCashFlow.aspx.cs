using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_AccountViewer_RptCashFlow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Get Criteria

    protected string GetCriteria()
    {
        string criteria = "";


        DateTime srtDate = DateTime.ParseExact(tbxfromDate.Text, "dd/MM/yyyy", null);
        string fromDate = srtDate.ToString("MM/dd/yyyy");
        DateTime endDate = DateTime.ParseExact(tbxToDate.Text, "dd/MM/yyyy", null);
        string toDate = endDate.ToString("MM/dd/yyyy");

        if (tbxfromDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "TDate>='" + fromDate + "'";
                Session["rptFromDate"] = fromDate;
                string datu = Session["rptFromDate"].ToString();
            }
            else
            {
                Session["rptFromDate"] = fromDate;
                criteria += " and TDate between '" + fromDate + "'";
            }

        }
        if (tbxToDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = " and" + toDate;
                Session["rptToDate"] = toDate;
            }
            else
            {

                criteria += " and TDate<='" + toDate + "'";
                Session["rptToDate"] = toDate;
            }
        }
        return criteria;
    }
    #endregion
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string criteria = GetCriteria();

        Response.Redirect("../../Report/AccountViewer/AccountReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=cashFlow");
    }
}