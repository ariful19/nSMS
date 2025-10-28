using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_ExpenseReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    #region  Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";

        DateTime strDate = DateTime.ParseExact(tbxfromDate.Text, "dd/MM/yyyy", null);
        string fromDate = strDate.ToString("MM/dd/yyyy");
        DateTime endDate = DateTime.ParseExact(tbxToDate.Text, "dd/MM/yyyy", null);
        string toDate = endDate.ToString("MM/dd/yyyy");

        if (tbxfromDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "TDate>='" + fromDate + "'";
                Session["rptFromDate"] = tbxfromDate.Text;
            }
            else
            {
                Session["rptFromDate"] = tbxfromDate.Text;
                criteria += " and TDate between '" + fromDate + "'";
            }

        }
        if (tbxToDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = " and" + toDate;
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
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=expense");
    }
}