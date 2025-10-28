using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PayRoll_EmployeePayment : System.Web.UI.Page
{
    dalTeacher objTeacher = new dalTeacher();
    dalPayroll objPayroll = new dalPayroll();
    dalYear objYear = new dalYear();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    #region Load
    protected void LoadData()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlGrade.DataSource = new Common().GetAll("tbl_Grade");
        ddlGrade.DataBind();

        ddlLevel.DataSource = new Common().GetAll("tbl_Level");
        ddlLevel.DataBind();

        LoadEmployee();
    }

    private void LoadEmployee()
    {

        if (ddlGrade.SelectedItem.Text == "---Select---" || ddlCampus.SelectedItem.Text == "---Select---")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please Select data from list');", true);
        }
        else
        {
            Common.DropdownByDiCondition(ddlEmployee, "view_EmployeeInfo", "Campus", "GradeId", "LevelId", ddlCampus.SelectedItem.Value, ddlGrade.SelectedItem.Value, ddlLevel.SelectedItem.Value, 1, 0);
        }
    }

    #endregion

    #region variable
    decimal DueAmount
    {
        set { ViewState["DueAmount"] = value; }
        get
        {
            try
            {
                return Convert.ToDecimal(ViewState["DueAmount"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    decimal TotalDue
    {
        set { ViewState["TotalDue"] = value; }
        get
        {
            try
            {
                return Convert.ToDecimal(ViewState["TotalDue"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    decimal TotalGiven
    {
        set { ViewState["TotalGiven"] = value; }
        get
        {
            try
            {
               return Convert.ToDecimal(ViewState["TotalGiven"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int EmployeeId
    {
        set { ViewState["EmployeeId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["EmployeeId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    string AccountNumber
    {
        set { ViewState["AccountNumber"] = value; }
        get
        {
            try
            {
                return ViewState["AccountNumber"].ToString();
            }
            catch
            {
                return null;
            }
        }
    }
    int UpdateId
    {
        set { ViewState["UpdateId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["UpdateId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int StartMonth
    {
        set { ViewState["StartMonth"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["StartMonth"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int EndMonth
    {
        set { ViewState["EndMonth"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["EndMonth"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    DataTable dtMonthlyPayment
    {
        set { ViewState["dtMonthlyPayment"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtMonthlyPayment"];
            }
            catch
            {
                return null;
            }
        }
    }
    int PersonId
    {
        set { ViewState["PersonId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["PersonId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int PayrollToDesignationId
    {
        set { ViewState["PayrollToDesignationId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["PayrollToDesignationId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    DataTable dtPaid
    {
        set { ViewState["dtPaid"] = value; }
        get
        {
            try
            {
                return (DataTable)(ViewState["dtPaid"]);
            }
            catch
            {
                return null;
            }
        }
    }

    #endregion
  
    #region index
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        EmployeeDetails();

    }

    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadEmployee();

    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadEmployee();

    }

    #endregion

    public DataTable MonthlyPayment()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("MonthYear", typeof(string));
        dt.Columns.Add("Year", typeof(int));
        dt.Columns.Add("MonthId", typeof(int));
        dt.Columns.Add("PayrollTypeId", typeof(int));
        dt.Columns.Add("Total", typeof(decimal));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("PayrollType", typeof(string));
        return dt;
    }

    public DataTable Payment()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("EmployeeId", typeof(int));
        dt.Columns.Add("GradeId", typeof(int));
        dt.Columns.Add("LevelId", typeof(int));
        dt.Columns.Add("CampusId", typeof(int));
        dt.Columns.Add("MonthId", typeof(int));
        dt.Columns.Add("YearId", typeof(int));
        dt.Columns.Add("PayrollTypeId", typeof(int));
        dt.Columns.Add("AccountNumber", typeof(string));
        dt.Columns.Add("TotalAmount", typeof(decimal));
        return dt;
    }
    protected void EmployeeDetails()
    {
        DataTable dt = objTeacher.GetTeacherByCriteria(GetCriteria());
        EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]);
        int gradeId = Convert.ToInt32(ddlGrade.SelectedValue);
        int levelId = Convert.ToInt32(ddlLevel.SelectedValue);
        PersonId = Convert.ToInt32(dt.Rows[0]["PersonId"]);
        AccountNumber = string.IsNullOrEmpty(dt.Rows[0]["AccountNumber"].ToString()) ? "NONE" : dt.Rows[0]["AccountNumber"].ToString();
        DataTable dts = objPayroll.GetPaymentByEmployee(EmployeeId, Convert.ToInt32(ddlYear.SelectedValue));
        StartMonth = Convert.ToInt32(dts.Rows[0]["StartMonth"]);
        EndMonth = Convert.ToInt32(dts.Rows[0]["EndMonth"]);
        if (dt.Rows.Count > 0 && StartMonth >0)
        {
            rptEmployee.DataSource = null;
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind();
            LoadMonthYear();
        }
        else
        {
            rptEmployee.DataSource = null;
            rptEmployee.DataBind();

        }
       
    }

  
    protected string GetCriteria()
    {
        string criteria = "";

        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "Campus=" + ddlCampus.SelectedValue;
            }
            else
                criteria += " and Campus=" + ddlCampus.SelectedValue;
        }
        if (ddlGrade.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "GradeId=" + ddlGrade.SelectedValue;
            }
            else
                criteria += " and GradeId=" + ddlGrade.SelectedValue;
        }
        if (ddlLevel.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "LevelId=" + ddlLevel.SelectedValue;
            }
            else
                criteria += " and LevelId=" + ddlLevel.SelectedValue;
        }
        if (ddlEmployee.SelectedIndex != 0)
        {
            if (criteria == "")
            {
                criteria = "EmployeeId=" + ddlEmployee.SelectedValue;
            }
            else
                criteria += " and EmployeeId=" + ddlEmployee.SelectedValue;
        }
        return criteria;
    }

    protected void LoadMonthYear()
    {

        int roll = 0;
        DueAmount = 0;
        int year = new dalYear().GetNameId(Convert.ToInt32(ddlYear.SelectedValue));
        List<int> lstmonthId = new List<int>();
        dtMonthlyPayment = MonthlyPayment();

        if (StartMonth < EndMonth)
        {
            for (int i = StartMonth; EndMonth + 1 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthlyPayment);
            }
        }
        else
        {
            for (int i = StartMonth; 13 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthlyPayment);
            }
            year = year + 1;
            for (int i = 1; EndMonth + 1 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthlyPayment);
            }
        }

        if (dtMonthlyPayment.Rows.Count > 0)
        {
            rptEmployeePayment.DataSource = dtMonthlyPayment;
            rptEmployeePayment.DataBind();
        }
        else
        {
            rptEmployeePayment.DataSource = null;
            rptEmployeePayment.DataBind();
        }
    }

    private void LoadMonthRpt(int monthId, int year, DataTable dt)
    {
        string month = "";
        int updateId = 0;
        int payrollToDesignationId = 0;
        decimal amount = 0;

        DataTable dtMonth = objYear.GetMonthById(monthId);
        DataTable dtT = new Common().GetAll("tbl_PayrollType");
        string payrollType = dtT.Rows[0]["PayrollType"].ToString();
        int payrollTypeId = Convert.ToInt32(dtT.Rows[0]["Id"]);
        DataTable dtMonthlyFee = objPayroll.GetPaymentByIndividual(EmployeeId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), monthId, payrollTypeId);
        month = dtMonth.Rows[0]["Month"].ToString();
        payrollTypeId = Convert.ToInt32(dtMonthlyFee.Rows[0]["PayrollTypeId"]);
        amount = Convert.ToInt32(dtMonthlyFee.Rows[0]["Amount"]);
        payrollToDesignationId = Convert.ToInt32(dtMonthlyFee.Rows[0]["Id"]);
        updateId = Convert.ToInt32(dtMonthlyFee.Rows[0]["EmployeeId"]);

        dt.Rows.Add(month + "-" + year, Convert.ToInt32(ddlYear.SelectedValue), monthId, payrollTypeId, 0, amount, payrollType);
    }

    protected void rptMonthlyPayment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dt = new Common().GetAll("tbl_PayrollType");

        if (e.Item.ItemType == ListItemType.Header)
        {
            Label lblHeader1 = (Label)e.Item.FindControl("lblHeader1");
            Label lblHeader2 = (Label)e.Item.FindControl("lblHeader2");
            Label lblHeader3 = (Label)e.Item.FindControl("lblHeader3");
            Label lblHeader4 = (Label)e.Item.FindControl("lblHeader4");
            Label lblHeader5 = (Label)e.Item.FindControl("lblHeader5");
            Label lblHeader6 = (Label)e.Item.FindControl("lblHeader6");

            if (dt.Rows.Count > 0)
            {
                lblHeader1.Visible = true;
                lblHeader1.Text = dt.Rows[0]["PayrollType"].ToString();

                if (dt.Rows.Count > 1)
                {
                    lblHeader2.Visible = true;
                    lblHeader2.Text = dt.Rows[1]["PayrollType"].ToString();
                    if (dt.Rows.Count > 2)
                    {
                        lblHeader3.Visible = true;
                        lblHeader3.Text = dt.Rows[2]["PayrollType"].ToString();
                        if (dt.Rows.Count > 3)
                        {
                            lblHeader4.Visible = true;
                            lblHeader4.Text = dt.Rows[3]["PayrollType"].ToString();
                            if (dt.Rows.Count > 4)
                            {
                                lblHeader5.Visible = true;
                                lblHeader5.Text = dt.Rows[4]["PayrollType"].ToString();
                            }
                            else
                            {
                                lblHeader5.Visible = false;
                                lblHeader6.Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader6")).Visible = false;
                            }
                        }
                        else
                        {
                            lblHeader4.Visible = false;
                            lblHeader5.Visible = false;
                            lblHeader6.Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader6")).Visible = false;
                        }
                    }
                    else
                    {
                        lblHeader3.Visible = false;
                        lblHeader4.Visible = false;
                        lblHeader5.Visible = false;
                        lblHeader6.Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader3")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader6")).Visible = false;
                    }
                }
                else
                {
                    lblHeader2.Visible = false;
                    lblHeader3.Visible = false;
                    lblHeader4.Visible = false;
                    lblHeader5.Visible = false;
                    lblHeader6.Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader2")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader3")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader6")).Visible = false;
                }

            }

            else
            {
                lblHeader1.Visible = false;
                lblHeader2.Visible = false;
                lblHeader3.Visible = false;
                lblHeader4.Visible = false;
                lblHeader5.Visible = false;
                lblHeader6.Visible = false;

                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader1")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader2")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader3")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader6")).Visible = false;
            }
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnMonthId = (HiddenField)e.Item.FindControl("hdnMonthId");
            HiddenField hdnDesignationId = (HiddenField)e.Item.FindControl("hdnDesignationId");

            TextBox tbxItem1 = (TextBox)e.Item.FindControl("tbxItem1");
            TextBox tbxItem2 = (TextBox)e.Item.FindControl("tbxItem2");
            TextBox tbxItem3 = (TextBox)e.Item.FindControl("tbxItem3");
            TextBox tbxItem4 = (TextBox)e.Item.FindControl("tbxItem4");
            TextBox tbxItem5 = (TextBox)e.Item.FindControl("tbxItem5");
            Label lblItem1 = (Label)e.Item.FindControl("lblItem1");
            Label lblItem2 = (Label)e.Item.FindControl("lblItem2");
            Label lblItem3 = (Label)e.Item.FindControl("lblItem3");
            Label lblItem4 = (Label)e.Item.FindControl("lblItem4");
            Label lblItem5 = (Label)e.Item.FindControl("lblItem5");
            Label lblUpdateId1 = (Label)e.Item.FindControl("lblUpdateId1");
            Label lblUpdateId2 = (Label)e.Item.FindControl("lblUpdateId2");
            Label lblUpdateId3 = (Label)e.Item.FindControl("lblUpdateId3");
            Label lblUpdateId4 = (Label)e.Item.FindControl("lblUpdateId4");
            Label lblUpdateId5 = (Label)e.Item.FindControl("lblUpdateId5");
            Label lblPayrollToDesignationId1 = (Label)e.Item.FindControl("lblPayrollToDesignationId1");
            Label lblPayrollToDesignationId2 = (Label)e.Item.FindControl("lblPayrollToDesignationId2");
            Label lblPayrollToDesignationId3 = (Label)e.Item.FindControl("lblPayrollToDesignationId3");
            Label lblPayrollToDesignationId4 = (Label)e.Item.FindControl("lblPayrollToDesignationId4");
            Label lblPayrollToDesignationId5 = (Label)e.Item.FindControl("lblPayrollToDesignationId5");
            if (dt.Rows.Count > 0)
            {
                tbxItem1.Visible = true;
                lblItem1.Text = dt.Rows[0]["Id"].ToString();
                tbxItem1.Text = MonthlyPayment(Convert.ToInt32(lblItem1.Text), Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));
                lblUpdateId1.Text = UpdateId.ToString();
                lblPayrollToDesignationId1.Text = PayrollToDesignationId.ToString();
                if (dt.Rows.Count > 1)
                {
                    tbxItem2.Visible = true;
                    lblItem2.Text = dt.Rows[1]["Id"].ToString();
                    tbxItem2.Text = MonthlyPayment(Convert.ToInt32(lblItem2.Text), Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));
                    lblUpdateId2.Text = UpdateId.ToString();
                    lblPayrollToDesignationId2.Text = PayrollToDesignationId.ToString();
                    if (dt.Rows.Count > 2)
                    {
                        tbxItem3.Visible = true;
                        lblItem3.Text = dt.Rows[2]["Id"].ToString();
                        tbxItem3.Text = MonthlyPayment(Convert.ToInt32(lblItem3.Text), Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));
                        lblUpdateId3.Text = UpdateId.ToString();
                        lblPayrollToDesignationId3.Text = PayrollToDesignationId.ToString();
                        if (dt.Rows.Count > 3)
                        {
                            tbxItem4.Visible = true;
                            lblItem4.Text = dt.Rows[3]["Id"].ToString();
                            tbxItem4.Text = MonthlyPayment(Convert.ToInt32(lblItem4.Text), Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));
                            lblUpdateId4.Text = UpdateId.ToString();
                            lblPayrollToDesignationId4.Text = PayrollToDesignationId.ToString();
                            if (dt.Rows.Count > 4)
                            {
                                tbxItem5.Visible = true;
                                lblItem5.Text = dt.Rows[4]["Id"].ToString();
                                tbxItem5.Text = MonthlyPayment(Convert.ToInt32(lblItem5.Text), Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));
                                lblUpdateId5.Text = UpdateId.ToString();
                                lblPayrollToDesignationId5.Text = PayrollToDesignationId.ToString();
                            }
                            else
                            {
                                tbxItem5.Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                            }
                        }
                        else
                        {
                            tbxItem4.Visible = false;
                            tbxItem5.Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                        }
                    }
                    else
                    {
                        tbxItem3.Visible = false;
                        tbxItem4.Visible = false;
                        tbxItem5.Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem3")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                    }
                }

                else
                {
                    tbxItem2.Visible = false;
                    tbxItem3.Visible = false;
                    tbxItem4.Visible = false;
                    tbxItem5.Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem2")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem3")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                }
            }
            else
            {
                tbxItem1.Visible = false;
                tbxItem2.Visible = false;
                tbxItem3.Visible = false;
                tbxItem4.Visible = false;
                tbxItem5.Visible = false;

                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem1")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem2")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem3")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
            }

        }
    }

    private string MonthlyPayment(int payrollTypeId, int monthId, int yearId, int gradeId, int levelId)
    {

        string amount = "";
        DataTable dts = objPayroll.GetPaymentByPayroll(payrollTypeId, monthId, yearId, gradeId,levelId, Convert.ToInt32(ddlCampus.SelectedValue), EmployeeId);

        if (dts.Rows.Count > 0)
        {
            amount = dts.Rows[0]["Amount"].ToString();
            UpdateId = Convert.ToInt32(dts.Rows[0]["EmployeeId"].ToString());
            PayrollToDesignationId = Convert.ToInt32(dts.Rows[0]["Id"]);
        }

        return amount;
    }

    #region Button
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void btnPaid_Click(object sender, EventArgs e)
    {
        int result = 0;
        TotalGiven = Convert.ToDecimal(tbxAmount.Text);
        foreach (DataRow row in dtPaid.Rows)
        {
            decimal dueAmount = 0;
            decimal givenAmount = 0;
            int empId = Convert.ToInt32(row["EmployeeId"]);
            int gradeId = Convert.ToInt32(row["GradeId"]);
            int levelId = Convert.ToInt32(row["LevelId"]);
            string billNo = tbxBillNo.Text.ToString();
            int campusId = Convert.ToInt32(row["CampusId"]);
            int monthId = Convert.ToInt32(row["MonthId"]);
            int yearId = Convert.ToInt32(row["YearId"]);
            int payrollTypeId = Convert.ToInt32(row["PayrollTypeId"]);
            decimal amount = Convert.ToDecimal(row["TotalAmount"]);
            if (TotalDue == TotalGiven)
            {
                dueAmount = 0;
                givenAmount = amount;
            }
            else
            {
                TotalGiven -= amount;

                if (amount == 0)
                {
                    dueAmount = 0;
                }
                else
                {
                    givenAmount = (TotalGiven >= 0) ? amount : amount - TotalGiven;
                    dueAmount = (TotalGiven >= 0) ? 0 : amount;
                }
            }
            
            result = objPayroll.InsertEmployeeSalaryPaid(empId, gradeId,levelId ,campusId, monthId, yearId, payrollTypeId, AccountNumber, amount, givenAmount, dueAmount, billNo, Page.User.Identity.Name, DateTime.Now);
        }

        if (result != -1)
        {
            MessageController.Show("Salary paid Successfully!", MessageType.Confirmation, Page);
        }
        else
        {
            MessageController.Show("Salary Allready been paid!", MessageType.Warning, Page);
        }

    }
    #endregion

    protected void checkPayment_OnCheckChanged(object sender, EventArgs e)
    {
        ClearPopup();
        DataTable dtP = new DataTable();
        dtPaid = Payment();
        decimal amount=0;
        decimal totalAmount = 0;
        decimal totalGiven = 0;
        decimal totalDue = 0;
        decimal penalty=0;
        decimal basic = 0;
        decimal allowance = 0;
        decimal bonus = 0;
        int payrollTypeId = 0;
        decimal increment = 0;
        foreach (RepeaterItem items in rptEmployee.Items)
        {

            CheckBox chkEmp = (CheckBox)items.FindControl("chkrowEmp");
            if (chkEmp.Checked)
            {
                foreach (RepeaterItem item in rptEmployeePayment.Items)
                {
                    CheckBox chkMonth = (CheckBox)item.FindControl("chkrowP");
                    if (chkMonth.Checked)
                    {
                        HiddenField hdnMonthId = (HiddenField)item.FindControl("hdnMonthId");
                        HiddenField hdnYearId = (HiddenField)item.FindControl("hdnYearId");
                        Label lblMonthYear = (Label)item.FindControl("lblMonthYear");

                        dtP = objPayroll.GetEmployeeSalaryMonthly(EmployeeId, Convert.ToInt32(hdnYearId.Value), Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));

                        basic = dtP.AsEnumerable().Where(o => o.Field<int>("PayrollTypeId") == 1).Sum(s => s.Field<decimal>("Amount"));
                        allowance = dtP.AsEnumerable().Where(o => o.Field<int>("PayrollTypeId") == 2).Sum(s => s.Field<decimal>("Amount"));
                        increment = dtP.AsEnumerable().Where(o => o.Field<int>("PayrollTypeId") == 3).Sum(s => s.Field<decimal>("Amount"));
                        bonus = dtP.AsEnumerable().Where(o => o.Field<int>("PayrollTypeId") == 4).Sum(s => s.Field<decimal>("Amount"));
                        penalty = dtP.AsEnumerable().Where(o => o.Field<int>("PayrollTypeId") == 5).Sum(s => s.Field<decimal>("Amount"));

                        totalAmount = basic + allowance + increment + bonus - penalty;
                        // totalDue= totalAmount - Convert.ToDecimal(tbxAmount.Text);
                        popGrandTotal.Text = totalAmount.ToString();
                        popBasic.Text = basic.ToString();
                        popIncrement.Text = increment.ToString();
                        popAllowance.Text = allowance.ToString();
                        popPenalty.Text = penalty.ToString();
                        popBonus.Text = bonus.ToString();

                        foreach (DataRow row in dtP.Rows)
                        {
                            payrollTypeId = Convert.ToInt32(row["PayrollTypeId"]);
                            amount = Convert.ToDecimal(row["Amount"]);

                            dtPaid.Rows.Add(EmployeeId, Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(hdnYearId.Value), payrollTypeId, AccountNumber, amount);
                        }

                        TotalDue = dtPaid.AsEnumerable().Sum(o => o.Field<decimal>("TotalAmount")); 
                        //totalDue = totalAmount - Convert.ToDecimal(popPaying.Text);
                       // fullyPaid = ((double)totalDue == 0.00) ? true : false;

                      //  dtPaid.Rows.Add(EmployeeId, Convert.ToInt32(ddlDesignation.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue),Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(hdnYearId.Value), accountNumber, totalAmount);
                      //  totalAmount += dtP.AsEnumerable().Sum(o => o.Field<decimal>("Amount"));
                    }
                }

            }

            else
            {
                MessageController.Show("Please Select Employee First", MessageType.Warning, Page);
            }

        }
    }

    protected void ClearPopup()
    {
        tbxAmount.Text = "";
        tbxBillNo.Text = "";
    }


}
