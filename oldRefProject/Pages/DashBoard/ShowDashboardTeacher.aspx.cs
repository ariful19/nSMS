using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DashBoard_ShowDashboardTeacher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadAllTeacher();
        lblError.Visible = false;
    }
    private void LoadAllTeacher()
    {
        DataTable dt = new dalDashbord().GetAllTeacher();
        if (dt.Rows.Count > 0)
        {
            rptTeacher.DataSource = dt;
            rptTeacher.DataBind();
        }
        else
        {
            rptTeacher.DataSource = null;
            rptTeacher.DataBind();
            lblError.Visible = true;
        }
    }

}