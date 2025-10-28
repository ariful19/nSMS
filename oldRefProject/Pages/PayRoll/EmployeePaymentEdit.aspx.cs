using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PayRoll_EmployeePaymentEdit : System.Web.UI.Page
{

    dalTeacher objTeacher = new dalTeacher();
    dalPayroll objPayroll = new dalPayroll();
    dalYear objYear = new dalYear();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
        }
    }

    #region Variable
    DataTable dtMonthYear
    {
        set { ViewState["dtMonthYear"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtMonthYear"];
            }
            catch
            {
                return null;
            }
        }
    }

    DataTable dtPayment
    {
        set { ViewState["dtPayment"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtPayment"];
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

    int DesignationId
    {
        set { ViewState["DesignationId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["DesignationId"]);
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

    #endregion
    protected DataTable MonthYear()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MonthYear");
        dt.Columns.Add("MonthId");
        dt.Columns.Add("GradeId");
        dt.Columns.Add("LevelId");
        dt.Columns.Add("PayrollToDesignationId");
        dt.Columns.Add("PayrollTypeId");
        dt.Columns.Add("UpdateId", typeof(int));
        dt.Columns.Add("Amount");
        return dt;
    }

    protected DataTable Payment()
    {
        DataTable dt = new DataTable("dtPayment");
        dt.Columns.Add("PersonId", typeof(int));
        dt.Columns.Add("EmployeeId", typeof(int));
        dt.Columns.Add("UpdatedID", typeof(int));
        dt.Columns.Add("MID", typeof(int));
        dt.Columns.Add("PayrollID", typeof(int));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("InsertUpdateID", typeof(int));
        dt.Columns.Add("StrMonth", typeof(string));
        dt.Columns.Add("EndMonth", typeof(string));
        dt.Columns.Add("GradeId", typeof(int));
        dt.Columns.Add("LevelId", typeof(int));
        return dt;
    }

    protected void Load()
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

    protected string GetCriterias()
    {
        string criteria = "";

        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "YearId=" + ddlYear.SelectedValue;
            }
            else
                criteria += " and YearId=" + ddlYear.SelectedValue;
        }

        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "CampusId=" + ddlCampus.SelectedValue;
            }
            else
                criteria += " and CampusId=" + ddlCampus.SelectedValue;
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

        return criteria;
    }
    private void LoadEmployee()
    {

        if (ddlGrade.SelectedItem.Text == "---Select---" || ddlLevel.SelectedItem.Text=="---Select---" || ddlCampus.SelectedItem.Text == "---Select---")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please Select data from list');", true);
        }
        else
        {
            Common.DropdownByDiCondition(ddlEmployee, "view_EmployeeInfo", "Campus", "GradeId","LevelId", ddlCampus.SelectedItem.Value, ddlGrade.SelectedItem.Value,ddlLevel.SelectedItem.Value, 1, 0);
        }
    }

    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadEmployee();

    }

    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadEmployee();

    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadEmployee();
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        EmployeeDetails();

    }

    protected void EmployeeDetails()
    {
        DataTable dt = objTeacher.GetTeacherByCriteria(GetCriteria());

        EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]);
        DesignationId = Convert.ToInt32(dt.Rows[0]["DesignationId"]);
        PersonId = Convert.ToInt32(dt.Rows[0]["PersonId"]);
        if (dt.Rows.Count > 0)
        {
            rptEmployee.DataSource = null;
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind();
        }
        LoadMonthYear();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        int empId = 0;
        DataTable dts = new Common().GetAll("bs_Month");
        int monthId = 0;
        dtPayment = Payment();
        foreach (RepeaterItem item in rptEmployee.Items)
        {
            CheckBox chkrowEmp = (CheckBox)item.FindControl("chkrowEmp");
            HiddenField hdnEmpId = (HiddenField)item.FindControl("hdnEmpId");
            if (chkrowEmp.Checked)
            {
                empId = Convert.ToInt32(hdnEmpId.Value);

                foreach (RepeaterItem items in rptEmployeePayment.Items)
                {
                    
                    int pId = Convert.ToInt32(ddlEmployee.SelectedValue);
                    int yearId = Convert.ToInt32(ddlYear.SelectedValue);
                    int gradeId = Convert.ToInt32(ddlGrade.SelectedValue);
                    int levelId = Convert.ToInt32(ddlLevel.SelectedValue);
                    TextBox txtPayrollToDesignationId = (TextBox)items.FindControl("txtPayrollToDesignationId");
                    Label lblPayrollTypeId = (Label)items.FindControl("lblPayrollTypeId");
                    Label lblUpdateId = (Label)items.FindControl("lblUpdateId");
                    HiddenField hdnMonthId = (HiddenField)items.FindControl("hdnMonthId");
                    CheckBox chkMonthly = (CheckBox)items.FindControl("chkrowP");
                    Label lblMonthYear = (Label)items.FindControl("lblMonthYear");
                    string month = lblMonthYear.Text.Split('-')[0];
                    monthId = dts.AsEnumerable()
                        .First(dr => dr["Month"].ToString() == month)
                        .Field<int>("Id");
                    Label lblUpdateId1 = (Label)items.FindControl("lblUpdateId1");
                    Label lblUpdateId2 = (Label)items.FindControl("lblUpdateId2");
                    Label lblUpdateId3 = (Label)items.FindControl("lblUpdateId3");
                    Label lblUpdateId4 = (Label)items.FindControl("lblUpdateId4");
                    Label lblUpdateId5 = (Label)items.FindControl("lblUpdateId5");
                    Label lblUpdateId6 = (Label)items.FindControl("lblUpdateId6");

                    Label lblPayrollToDesignationId1 = (Label)items.FindControl("lblPayrollToDesignationId1");
                    Label lblPayrollToDesignationId2 = (Label)items.FindControl("lblPayrollToDesignationId2");
                    Label lblPayrollToDesignationId3 = (Label)items.FindControl("lblPayrollToDesignationId3");
                    Label lblPayrollToDesignationId4 = (Label)items.FindControl("lblPayrollToDesignationId4");
                    Label lblPayrollToDesignationId5 = (Label)items.FindControl("lblPayrollToDesignationId5");
                    Label lblPayrollToDesignationId6 = (Label)items.FindControl("lblPayrollToDesignationId6");

                    Label lblItem1 = (Label)items.FindControl("lblItem1");
                    Label lblItem2 = (Label)items.FindControl("lblItem2");
                    Label lblItem3 = (Label)items.FindControl("lblItem3");
                    Label lblItem4 = (Label)items.FindControl("lblItem4");
                    Label lblItem5 = (Label)items.FindControl("lblItem5");
                    Label lblItem6 = (Label)items.FindControl("lblItem6");

                    TextBox tbxItem1 = (TextBox)items.FindControl("tbxItem1");
                    TextBox tbxItem2 = (TextBox)items.FindControl("tbxItem2");
                    TextBox tbxItem3 = (TextBox)items.FindControl("tbxItem3");
                    TextBox tbxItem4 = (TextBox)items.FindControl("tbxItem4");
                    TextBox tbxItem5 = (TextBox)items.FindControl("tbxItem5");
                    TextBox tbxItem6 = (TextBox)items.FindControl("tbxItem6");
                    if (chkMonthly.Checked)
                    {
                        if (tbxItem1.Visible)
                            LoadEmployeePayment(dtPayment, EmployeeId, monthId, Convert.ToInt32(lblItem1.Text), tbxItem1.Text, Convert.ToInt32(lblUpdateId1.Text), Convert.ToInt32(lblPayrollToDesignationId1.Text));
                        if (tbxItem2.Visible)
                            LoadEmployeePayment(dtPayment, EmployeeId, monthId, Convert.ToInt32(lblItem2.Text), tbxItem2.Text, Convert.ToInt32(lblUpdateId2.Text), Convert.ToInt32(lblPayrollToDesignationId2.Text));
                        if (tbxItem3.Visible)
                            LoadEmployeePayment(dtPayment, EmployeeId, monthId, Convert.ToInt32(lblItem3.Text), tbxItem3.Text, Convert.ToInt32(lblUpdateId3.Text), Convert.ToInt32(lblPayrollToDesignationId3.Text));
                        if (tbxItem4.Visible)
                            LoadEmployeePayment(dtPayment, EmployeeId, monthId, Convert.ToInt32(lblItem4.Text), tbxItem4.Text, Convert.ToInt32(lblUpdateId4.Text), Convert.ToInt32(lblPayrollToDesignationId4.Text));
                        if (tbxItem5.Visible)
                            LoadEmployeePayment(dtPayment, EmployeeId, monthId, Convert.ToInt32(lblItem5.Text), tbxItem5.Text, Convert.ToInt32(lblUpdateId5.Text), Convert.ToInt32(lblPayrollToDesignationId5.Text));
                        //if (tbxItem6.Visible)
                        //    LoadEmployeePayment(dtPayment, EmployeeId, monthId, Convert.ToInt32(lblItem6.Text), tbxItem6.Text, Convert.ToInt32(lblUpdateId6.Text), Convert.ToInt32(lblPayrollToDesignationId6.Text));
                    }

                }

                if (dtPayment.Rows.Count > 0)
                {
                    int result = objPayroll.EmployeePaymentIndividualInsert(dtPayment, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), DesignationId, Page.User.Identity.Name, DateTime.Now);
                    if (result != -1)
                    {
                        MessageController.Show("Payment Setup Updated Successfully!", MessageType.Information, Page);
                    }
                    else
                        MessageController.Show("Payment Setup allready exist!!", MessageType.Warning, Page);
                }
                else
                {
                    MessageController.Show("Please Select Month-Year!", MessageType.Warning, Page);
                }

            }
            else
            {
                MessageController.Show("Please Select Employee", MessageType.Warning, Page);
            }

        }

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
            HiddenField hdnGradeId = (HiddenField)e.Item.FindControl("hdnGradeId");

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


    protected void LoadMonthYear()
    {

        int roll = 0;
        DataTable dtYear = new dalYear().GetById(Convert.ToInt32(ddlYear.SelectedValue));
        DataTable dtPayroll = new Common().GetAll("tbl_PayrollType");
        int payrollTypeId = Convert.ToInt32(dtPayroll.Rows[0]["Id"]);
        int year = Convert.ToInt32(dtYear.Rows[0]["Year"]);
        DataTable dtMonth = objPayroll.GetPaymentSetupByCriteria(GetCriterias());
        StartMonth = Convert.ToInt32(dtMonth.Rows[0]["StartMonth"].ToString());
        EndMonth = Convert.ToInt32(dtMonth.Rows[0]["EndMonth"].ToString());
        List<int> lstmonthId = new List<int>();
        dtMonthYear = MonthYear();

        if (StartMonth < EndMonth)
        {
            for (int i = StartMonth; EndMonth + 1 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthYear, payrollTypeId);
            }
        }
        else
        {
            for (int i = StartMonth; 13 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthYear, payrollTypeId);
            }
            for (int i = 1; EndMonth + 1 > i; i++)
            {
                roll = i;
                int nextYear = year + 1;
                LoadMonthRpt(roll, nextYear, dtMonthYear, payrollTypeId);
            }
        }

        if (dtMonthYear.Rows.Count > 0)
        {
            pnlEmpPayment.Visible = true;
            btnUpdate.Visible = true;
            rptEmployeePayment.DataSource = dtMonthYear;
            rptEmployeePayment.DataBind();
        }
        else
        {
            rptEmployeePayment.DataSource = null;
            rptEmployeePayment.DataBind();
            btnUpdate.Visible = false;
            pnlEmpPayment.Visible = true;
        }

    }
    private void LoadMonthRpt(int monthId, int year, DataTable dt, int payrollTypeId)
    {
        string month = "";
        int updateId = 0;
        int payrollToDesignationId = 0;
        decimal amount = 0;

        DataTable dtMonth = objYear.GetMonthById(monthId);
        DataTable dtMonthlyFee = objPayroll.GetPaymentByIndividual(EmployeeId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), monthId, payrollTypeId);
        month = dtMonth.Rows[0]["Month"].ToString();
        payrollTypeId = Convert.ToInt32(dtMonthlyFee.Rows[0]["PayrollTypeId"]);
        amount = Convert.ToInt32(dtMonthlyFee.Rows[0]["Amount"]);
        payrollToDesignationId = Convert.ToInt32(dtMonthlyFee.Rows[0]["Id"]);
        updateId = Convert.ToInt32(dtMonthlyFee.Rows[0]["EmployeeId"]);

        dt.Rows.Add(month + "-" + year, monthId, Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue), payrollToDesignationId, payrollTypeId, updateId, amount);
    }

    private void LoadEmployeePayment(DataTable dt, int employeeId, int monthId, int payrollTypeId, string tbxAmount, int updateId, int PayrollToDesignationId)
    {
        DataTable dts = new DataTable();

        dts = objPayroll.GetPaymentByIndividual(employeeId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), monthId, payrollTypeId);
        if (dts.Rows.Count > 0)
        {
            dt.Rows.Add(PersonId, employeeId, PayrollToDesignationId, monthId, payrollTypeId, tbxAmount, updateId, StartMonth, EndMonth, Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));
        }
    }

}


