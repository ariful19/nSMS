using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_MessageCreditView : System.Web.UI.Page
{
    dalMessageCredit messageCredit = new dalMessageCredit();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllMessageCreadit();
        }
    }
    private void GetAllMessageCreadit()
    {
        rptMessageCredit.DataSource = new Common().GetAll("MessageCredit");
        rptMessageCredit.DataBind();
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
                criteria = "PurchaseDate>='" + fromDate + "'";
            }
            else
            {
                criteria += " and PurchaseDate>='" + fromDate + "'";
            }

        }
        if (tbxToDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "PurchaseDate='" + toDate + "'";
            }
            else
            {
                criteria += " and PurchaseDate<='" + toDate + "'";
            }
        }
        return criteria;
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        string criteria = GetCriteria();

        DataTable dt = messageCredit.GetMessageCreditByDate(criteria);

        if (dt.Rows.Count > 0)
        {
            rptMessageCredit.DataSource = dt;
            rptMessageCredit.DataBind();
        }
        else
        {
            rptMessageCredit.DataSource = null;
            rptMessageCredit.DataBind();
            MessageController.Show("No Message Log Found!!!", MessageType.Warning, Page);
        }

    }
}