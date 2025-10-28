using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Fees_ClassCollectionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
            divPaymentHistory.Visible = false;

        }
    }
    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";

        DateTime srtDate = DateTime.ParseExact(tbxFromDate.Text, "dd/MM/yyyy", null);
        string fromDate = srtDate.ToString("MM/dd/yyyy");
        DateTime endDate = DateTime.ParseExact(tbxToDate.Text, "dd/MM/yyyy", null);
        string toDate = endDate.ToString("MM/dd/yyyy");

        if (tbxFromDate.Text != "")
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
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
            }
            else
            {
                criteria += " and er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
            }
        }
        if (ddlPaymentType.SelectedIndex !=0 && ddlPaymentType.SelectedValue !="")
        {
            if (criteria == "")
            {
                criteria = "PaymentTypeId=" + ddlPaymentType.SelectedValue ;
            }
            else
            {
                criteria += " and PaymentTypeId=" + ddlPaymentType.SelectedValue ;
            }
        }
        if (ddlClass.SelectedIndex != 0 && ddlClass.SelectedValue != "")
        {

            if (criteria == "")
            {
                criteria = "er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
            }
            else
            {
                criteria += " and er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
            }
        }
        return criteria;
    }

    #endregion

    #region LoadData
    protected void LoadData()
    {
        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();
        ddlCampus.Items.Insert(0, "---Select---");
        ddlCampus.SelectedIndex = 0;

        ddlPaymentType.DataSource = new Common().GetAll("fee_PaymentType");
        ddlPaymentType.DataBind();
        ddlPaymentType.Items.Insert(0, "---All---");
        ddlPaymentType.SelectedIndex = 0;

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();
        ddlClass.Items.Insert(0, "---Select---");
        ddlClass.SelectedIndex = 0;
    }
   
    #endregion

    private bool CheckValidation()
    {
        bool result = true;
        if (ddlCampus.SelectedIndex == 0 || ddlClass.SelectedIndex == 0)
        {
            MessageController.Show(" Select Campus,Payment Types and Class....", MessageType.Warning, Page);
            result = false;
        }
        return result;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (CheckValidation())
        {
            LoadStudentPayment();
        }
        
    }
    private void Clear()
    {
        //tbxRegNo.Text = "";
        //tbxRoll.Text = "";
        //ddlCourse.SelectedIndex = 0;
        //ddlTerm.SelectedIndex = 0;
        //ddlSubject.SelectedIndex = 0;
    }
    private void LoadStudentPayment()
    {
        string criteria = GetCriteria();
        decimal totalAmount = 0;
        decimal totalReceive = 0;
        DataTable dt = new dalPayment().GetPaymentHistoryByCriteria(criteria);

        if (dt.Rows.Count > 0)
        {
            rptHistory.DataSource = dt;
            rptHistory.DataBind();
            Label lblTotalAmount = (Label)rptHistory.Controls[rptHistory.Controls.Count -1].FindControl("lblTotalAmount");
            Label lblTotalRec = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblTotal");
            foreach (DataRow row in dt.Rows)
            {
                totalAmount += Convert.ToDecimal(row["Amount"]);
                totalReceive += Convert.ToDecimal(row["TotalGiven"]);
            }
            lblTotalAmount.Text = totalAmount.ToString();
            lblTotalRec.Text = totalReceive.ToString();
            divPaymentHistory.Visible = true;
        }
        else
        {
            MessageController.Show("No record found!!!", MessageType.Warning, Page);
            rptHistory.DataSource = null;
            rptHistory.DataBind();
            divPaymentHistory.Visible = false;
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=collectionPayment");
    }
}