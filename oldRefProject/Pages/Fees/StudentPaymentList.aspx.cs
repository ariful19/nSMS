using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Fees_StudentPaymentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.CanAccess(this);
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
        //if (ddlClass.SelectedValue != "" && ddlClass.SelectedIndex!=0)
        //{
        //    if (criteria == "")
        //    {
        //        criteria = "er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
        //    }
        //    else
        //    {
        //        criteria += " and er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
        //    }
        //}
        if (ddlClass.SelectedIndex != 0 && ddlClass.SelectedValue != "")
        {
            if (ddlClass.SelectedValue != "")
            {
                if (ddlClass.SelectedItem.Text.ToUpper().Contains("All".ToUpper()))
                {
                    if (ddlClass.SelectedItem.Text.ToUpper().Contains("Higher".ToUpper()))
                    {
                        if (criteria == "")
                            criteria = "er_StudentToClass.ClassId in (" + HigherClassIds + ")";
                        else
                            criteria += " and er_StudentToClass.ClassId in (" + HigherClassIds + ")";
                    }
                    else
                    {
                        if (criteria == "")
                            criteria = "er_StudentToClass.ClassId in (" + SecondaryClassIds + ")";
                        else
                            criteria += " and er_StudentToClass.ClassId in (" + SecondaryClassIds + ")";
                    }

                }
                else
                {
                    if (criteria == "")
                        criteria = "er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
                    else
                        criteria += " and er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
                }
            }
        }
        

        if (ddlPaymentType.SelectedIndex != 0 && ddlPaymentType.SelectedValue !="")
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
        if (ddlReceivedBy.SelectedIndex != 0 && ddlReceivedBy.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "ss_Payment.CreatedBy='" + ddlReceivedBy.SelectedValue + "'";
            }
            else
            {
                criteria += " and ss_Payment.CreatedBy='" + ddlReceivedBy.SelectedValue + "'";
            }
        }


        return criteria;
    }

    #endregion
    public DataTable Collection()
    {
        DataTable dt = new DataTable("Collection");
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Received", typeof(decimal));
        return dt;
    }
    public DataTable TypeWiseCollection()
    {
        DataTable dt = new DataTable("Collection");
        dt.Columns.Add("MonthYear", typeof(string));
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("Received", typeof(decimal));
        return dt;
    }
    #region LoadData
    protected void LoadData()
    {
        SecondaryClassIds = "";
        HigherClassIds = "";
        DataTable dt = new dalUser().GetAllAdminUser();
        foreach (DataRow row in dt.Rows)
        {
            if (row["RoleName"].ToString().Contains("Account"))
            {

            }
            else
            {
                row.Delete();

            }
        }
        dt.AcceptChanges();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        DataTable dtClass = new Common().GetAll("bs_ClassName");
        //ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        //ddlClass.DataBind();
        //ddlClass.Items.Insert(0, "---All---");
        //ddlClass.SelectedIndex = 0;
        ddlPaymentType.DataSource = new Common().GetAll("fee_PaymentType");
        ddlPaymentType.DataBind();
        ddlPaymentType.Items.Insert(0, "---All---");
        ddlPaymentType.SelectedIndex = 0;

        ddlReceivedBy.DataSource = dt;
        ddlReceivedBy.DataBind();

        ddlReceivedBy.Items.Insert(0, "---All---");
        ddlReceivedBy.SelectedIndex = 0;

        ddlClass.DataSource = dtClass;
        ddlClass.DataBind();

        ddlClass.Items.Insert(0, "---All---");
        ddlClass.SelectedIndex = 0;

        ddlClass.Items.Insert(1, "All Secondary Classes");
        ddlClass.SelectedIndex = 1;
        ddlClass.Items.Insert(2, "All Higher Classes");
        ddlClass.SelectedIndex = 2;

        foreach (DataRow row in dtClass.Rows)
        {
            if (row["ClassName"].ToString().ToUpper().Contains("college".ToUpper()))
            {
                if (HigherClassIds != "")
                    HigherClassIds += ",";
                HigherClassIds += row["Id"].ToString();
            }
            else
            {
                if (SecondaryClassIds != "")
                    SecondaryClassIds += ",";
                SecondaryClassIds += row["Id"].ToString();
            }
        }
    }
   
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadStudentPayment();
        Clear();
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
        decimal receive = 0;
        DataTable dtCollector = Collection();
        DataTable dtTypeCollector = TypeWiseCollection();
        DataTable dt = new dalPayment().GetPaymentHistoryByCriteria(criteria);

        var q = dt.AsEnumerable()
            .GroupBy(r => r["ReceivedBy"])
            .Select(p => new {Name = p.Key, Sum = p.Sum(r => (decimal) r["TotalGiven"])})
            .ToList();
        foreach (var item in q)
        {
            receive += Convert.ToDecimal(item.Sum);
            dtCollector.Rows.Add(item.Name, item.Sum);
        }

        var s = dt.AsEnumerable()
              .GroupBy(r => r["MonthYear"])
              .Select(p => new { MonthYear = p.Key, Sum = p.Sum(r => (decimal)r["TotalGiven"])})
              .ToList();
        foreach (var item in s)
        {           
            dtTypeCollector.Rows.Add(item.MonthYear, ddlPaymentType.SelectedItem.Text, item.Sum);
        }

        if (dt.Rows.Count > 0)
        {
            if (rdbList.SelectedValue == "1")
            {
                rptTypewiseCollection.DataSource = null;
                rptTypewiseCollection.DataBind();

                rptHistory.DataSource = dt;
                rptHistory.DataBind();
                Label lblTotalAmount = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblTotalAmount");
                Label lblTotalRec = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblTotal");

                 var love = dt.AsEnumerable().GroupBy(r =>new{ Id=r["StudentToClassId"],PaymentType=r["PaymentTypeId"],MonthYear=r["MonthYear"]})
                .Select(
                    p =>
                        new
                        {
                            Id = p.Key.Id,
                            GivenAmount = p.Sum(o=> (decimal) o["TotalGiven"]),
                            Amount = p.Max(o => o["Amount"])
                        }).ToList();

            foreach (var item in love)
            {
                totalAmount += Convert.ToDecimal(item.Amount);
                totalReceive += Convert.ToDecimal(item.GivenAmount);
            }
            //foreach (DataRow row in dt.Rows)
            //{
               
            //    totalAmount += Convert.ToDecimal(row["Amount"]);
            //    totalReceive += Convert.ToDecimal(row["TotalGiven"]);
            //}
                lblTotalAmount.Text = totalAmount.ToString();
                lblTotalRec.Text = totalReceive.ToString();
            }
            else
            {
                rptHistory.DataSource = null;
                rptHistory.DataBind();

                rptTypewiseCollection.DataSource = dtTypeCollector;
                rptTypewiseCollection.DataBind();
                Label lblTotalRec = (Label)rptTypewiseCollection.Controls[rptTypewiseCollection.Controls.Count - 1].FindControl("lblTotal");
                lblTotalRec.Text = receive.ToString();
            }
            divPaymentHistory.Visible = true;

            if (dtCollector.Rows.Count > 0)
            {
                rptCollector.DataSource = dtCollector;
                rptCollector.DataBind();
                Label lblTotal = (Label)rptCollector.Controls[rptCollector.Controls.Count - 1].FindControl("lblTotal");
                lblTotal.Text = receive.ToString();
            }
        }
        else
        {
            MessageController.Show("No record found!!!", MessageType.Warning, Page);
            rptHistory.DataSource = null;
            rptHistory.DataBind();
            rptCollector.DataSource = null;
            rptCollector.DataBind();
            divPaymentHistory.Visible = false;
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=collectionPayment&SelectId=" + ddlReceivedBy.SelectedItem.Text + "&Type=" + ddlPaymentType.SelectedItem.Text + "&MonthId=" + rdbList.SelectedValue);
    }

    string HigherClassIds
    {
        set { ViewState["HigherClassIds"] = value; }
        get
        {
            try
            {
                return ViewState["HigherClassIds"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    string SecondaryClassIds
    {
        set { ViewState["SecondaryClassIds"] = value; }
        get
        {
            try
            {
                return ViewState["SecondaryClassIds"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}

