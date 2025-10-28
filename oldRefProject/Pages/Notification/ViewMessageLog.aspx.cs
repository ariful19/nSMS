using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_ViewMessageLog : System.Web.UI.Page
{
    dalMessageLog messageLog = new dalMessageLog();
    dalSubject obj = new dalSubject();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllMessageLogByDate();
        }
    }
    private void GetAllMessageLogByDate()
    {
        rptMessageLog.DataSource = new Common().GetAll("MessageLog");
        rptMessageLog.DataBind();
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
                criteria = "SendDate>='" + fromDate + "'";
            }
            else
            {
                criteria += " and SendDate>='" + fromDate + "'";
            }

        }
        if (tbxToDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "SendDate='" + toDate + "'";
            }
            else
            {
                criteria += " and SendDate<='" + toDate + "'";
            }
        }
        return criteria;
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        string criteria = GetCriteria();

        DataTable dt = messageLog.GetMessageLogByDate(criteria);

        if (dt.Rows.Count > 0)
        {
            rptMessageLog.DataSource = dt;
            rptMessageLog.DataBind();
        }
        else
        {
            rptMessageLog.DataSource = null;
            rptMessageLog.DataBind();
            MessageController.Show("No Message Log Found!!!", MessageType.Warning, Page);
        }

    }
}