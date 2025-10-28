using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DashBoard_ShowDashboardStudent : System.Web.UI.Page
{
    dalPaymentType objpaymentType = new dalPaymentType();
    dalStudent objStudent = new dalStudent();
    dalPayment objPayment = new dalPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAllStudent();
            lblError.Visible = false;
        }
    }   
    private void LoadAllStudent()
    {
        DataTable dt = new dalDashbord().GetAllStudent();
        if (dt.Rows.Count > 0)
        {
            rptClass.DataSource = dt;
            rptClass.DataBind();
        }
        else
        {
            rptClass.DataSource = null;
            rptClass.DataBind();
            lblError.Visible = true;
        }
    }
}