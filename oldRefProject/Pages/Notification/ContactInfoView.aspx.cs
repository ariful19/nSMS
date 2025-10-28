using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_ContactInfoView : System.Web.UI.Page
{
    dalContactInfo obj = new dalContactInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllContactInfoByDate();
        }
    }
    private void GetAllContactInfoByDate()
    {
        rptMessageLog.DataSource = new Common().GetAll("bs_ContactInfo");
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
                criteria = "Date>='" + fromDate + "'";
            }
            else
            {
                criteria += " and Date>='" + fromDate + "'";
            }

        }
        if (tbxToDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "Date='" + toDate + "'";
            }
            else
            {
                criteria += " and Date<='" + toDate + "'";
            }
        }
        return criteria;
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        string criteria = GetCriteria();

        DataTable dt = obj.GetContactInfoByDate(criteria);

        if (dt.Rows.Count > 0)
        {
            rptMessageLog.DataSource = dt;
            rptMessageLog.DataBind();
        }
        else
        {
            rptMessageLog.DataSource = null;
            rptMessageLog.DataBind();
            MessageController.Show("No Contact Info Found!!!", MessageType.Warning, Page);
        }

    }
}