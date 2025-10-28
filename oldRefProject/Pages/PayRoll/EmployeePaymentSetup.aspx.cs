using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PayRoll_EmployeePaymentSetup : System.Web.UI.Page
{
    dalPayroll obj = new dalPayroll();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    #region LoadData
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

        ddlStartMonth.DataSource = new Common().GetAll("bs_Month");
        ddlStartMonth.DataBind();
        ddlEndMonth.DataSource = new Common().GetAll("bs_Month");
        ddlEndMonth.DataBind();

        ddlStartMonth.Items.Insert(0, "---Select---");
        ddlEndMonth.Items.Insert(0, "---Select---");

    }
   
    #endregion
    #region Variable
    protected DataTable Payment()
    {
        DataTable dt = new DataTable("dtPayment");
        dt.Columns.Add("UpdatedID", typeof(int));
        dt.Columns.Add("PayID", typeof(int));
        dt.Columns.Add("YearID", typeof(int));
        dt.Columns.Add("CampusID", typeof(int));
        dt.Columns.Add("GradeId", typeof(int));
        dt.Columns.Add("LevelId", typeof(int));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("StrMonth", typeof(int));
        dt.Columns.Add("EndMonth", typeof(int));
        return dt;
    }
    #endregion

    #region Function
    protected void LoadPaymentByDesignation()
    {

        DataTable dt = obj.GetPaymentforPayroll(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));

       
        if (dt.Rows.Count > 0)
        {
            rptPaymentType.DataSource = null;
            rptPaymentType.DataSource = dt;
            rptPaymentType.DataBind();
            //ddlStartMonth.SelectedValue = string.IsNullOrEmpty(dt.Rows[0]["Startmonth"].ToString()) ? "---Select---" : dt.Rows[0]["Startmonth"].ToString();
            //ddlEndMonth.SelectedValue = string.IsNullOrEmpty(dt.Rows[0]["Endmonth"].ToString()) ? "---Select---" : dt.Rows[0]["Endmonth"].ToString();
        }
        else
        {
            rptPaymentType.DataSource = null;
            rptPaymentType.DataBind();
        }
    }
  
    #endregion

    #region Button

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadPaymentByDesignation();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        ID = obj.InsertPayrolType(tbxName.Text, Page.User.Identity.Name);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This payment type name already exists. Please try anothor.", MessageType.Error, Page);
        tbxName.Text = "";
        LoadPaymentByDesignation();
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        DataTable dt = Payment();
        if (ddlStartMonth.SelectedIndex == 0 && ddlEndMonth.SelectedIndex == 0)
        {
            MessageController.Show("Please select Month correctly.", MessageType.Error, Page);
            return;
        }

        foreach (RepeaterItem item in rptPaymentType.Items)
        {
            string amount = "0";
            TextBox tbxAmmount = (TextBox)item.FindControl("tbxPayrollType");
            TextBox txtPayrollToDesignationID = (TextBox)item.FindControl("txtPayrollToDesignationID");
            HiddenField hdnId = (HiddenField)item.FindControl("hdnId");


            if (tbxAmmount.Text != "")
            {
                amount = tbxAmmount.Text;
            }
            dt.Rows.Add(int.Parse(txtPayrollToDesignationID.Text), Convert.ToInt32(hdnId.Value), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlGrade.SelectedValue),Convert.ToInt32(ddlLevel.SelectedValue), amount, Convert.ToInt32(ddlStartMonth.SelectedValue), Convert.ToInt32(ddlEndMonth.SelectedValue));
        }


        if (dt.Rows.Count > 0)
        {
           int result= obj.PayrollAmountInsert(dt, Page.User.Identity.Name, DateTime.Now);
           if (result != -1)
           {
               MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
           }
           else
               MessageController.Show("Payment Setup Allready Exist. !", MessageType.Error, Page);
           LoadPaymentByDesignation();
        }
        else
        {
            MessageController.Show("No data available for entry", MessageType.Information, Page);
        }
    }
    #endregion

}
