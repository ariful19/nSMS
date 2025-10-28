using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RoleManagement_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.Name == "")
            Response.Redirect("~/Login.aspx", true);
    }

   
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Admin/Dashboard.aspx", true);
    }
}