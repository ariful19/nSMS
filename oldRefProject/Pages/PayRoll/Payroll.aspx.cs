using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PayRoll_Payroll : System.Web.UI.Page
{
    dalPayroll obj = new dalPayroll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlPayroll.Visible = false;
            Load();
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DataTable dt = obj.GetPayrollForTeacher(tbxRegNo.Text);
        {
            if (dt.Rows.Count > 0)
            {
                lblTeacherName.Text = dt.Rows[0]["NameEng"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPinCode.Text = dt.Rows[0]["TeacherPin"].ToString();
                lblGrade.Text = dt.Rows[0]["Type"].ToString();
                lblScale.Text = dt.Rows[0]["Scale"].ToString();
                lblBasic.Text = dt.Rows[0]["Basic"].ToString();
                LoadAllowance(Convert.ToInt32(dt.Rows[0]["Id"].ToString()));
                LoadDeduction(Convert.ToInt32(dt.Rows[0]["Id"].ToString()));
                lblBasicSalary.Text = dt.Rows[0]["Basic"].ToString();
                lblNetSalary.Text = ((Convert.ToDecimal(lblBasicSalary.Text) + Convert.ToDecimal(lblTotalAllowance.Text)) - (Convert.ToDecimal(lblTotalDeduction.Text))).ToString();
                pnlPayroll.Visible = true;
                ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
            }
            else
            {
                MessageController.Show("No data found",MessageType.Warning,Page);
            }
        }

    }
    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
    }
    protected void LoadAllowance(int id)
    {
        DataTable dt = obj.GetAllowanceBytypeId(id);
        if (dt.Rows.Count > 0)
        {
            object sumObject;
            sumObject = dt.Compute("Sum(AllowanceAmount)", "");
            rptAllowance.DataSource = dt;
            rptAllowance.DataBind();
            lblTotalAllowance.Text = sumObject.ToString();
        }
    }
    protected void LoadDeduction(int id)
    {
        DataTable dt = obj.GetDeductionBytypeId(id);
        if (dt.Rows.Count > 0)
        {
            object sumObject;
            sumObject = dt.Compute("Sum(DeductionAmount)", "");
            rptDeduction.DataSource = dt;
            rptDeduction.DataBind();
            lblTotalDeduction.Text = sumObject.ToString();
        }

    }
}