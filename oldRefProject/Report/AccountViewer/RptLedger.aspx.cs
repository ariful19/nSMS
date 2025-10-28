using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_AccountViewer_RptLedger : System.Web.UI.Page
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

        if (tbxAccountId.Text != "")
        {
            if (criteria == "")
            {
                criteria = "tbl_Lgledger.AccountCodeId='" + tbxAccountId.Text + "'";
                Session["rptAccountCodeId"] = tbxAccountId.Text;                
            }
            else
            {
                Session["rptAccountCodeId"] = tbxAccountId.Text;
                criteria += " and tbl_Lgledger.AccountCodeId='" + tbxAccountId.Text + "'";
            }

        }
        if (tbxfromDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "TDate='" + fromDate + "'";
                Session["rptFromDate"] = tbxfromDate.Text;                
            }
            else
            {
                criteria += " and TDate>='" + fromDate + "'";
                Session["rptFromDate"] = tbxfromDate.Text;    
            }

        }
        if (tbxToDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "TDate='" + toDate+"'";
                Session["rptToDate"] = tbxToDate.Text;
            }
            else
            {

                criteria += " and TDate<='" + toDate + "'";
                Session["rptToDate"] = tbxToDate.Text;
            }
        }
        return criteria;
    }
    #endregion
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string criteria = GetCriteria();

        Response.Redirect("../../Report/AccountViewer/AccountReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=ledger");
    }
}