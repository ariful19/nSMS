using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_AdminMenu : System.Web.UI.UserControl
{
    dalTaskManager objTask = new dalTaskManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.Name != "")
        {
            lblUser.Text = Page.User.Identity.Name;
            LoadParent();
            LoadUserProfileImage();
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }

    }
    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        Session["RedirectFrom"] = null;
        Response.Redirect("~/Default.aspx", true);
    }
    protected void LoadParent()
    {
        DataTable dt = objTask.GetParanet(Controller.RoleIdByUserName(Page.User.Identity.Name));
        if (dt.Rows.Count > 0)
        {
            rptParent.DataSource = dt;
            rptParent.DataBind();
        }
    }


    private void LoadUserProfileImage()
    {
        DataTable dt = new dalPerson().GetByUserName(Page.User.Identity.Name);
        if (dt.Rows.Count > 0)
        {
            string imageName = Server.MapPath("/Images/Person/" + dt.Rows[0]["PersonImage"].ToString());

            if (!System.IO.File.Exists(imageName))
            {
                imageName = "../Images/Student/" + dt.Rows[0]["PersonImage"].ToString();
            }
            else
            {
                imageName = "../Images/Person/" + dt.Rows[0]["PersonImage"].ToString();
            }


            imgProfile.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["PersonImage"].ToString()) ? "../Images/Common/avatar5.png" : imageName;
        }
    }
    protected void rptCategory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rptChild = (Repeater)e.Item.FindControl("rptChild");
        HiddenField hdnValue = (HiddenField)e.Item.FindControl("hdnValue");
        DataTable dt = objTask.GetChild(Convert.ToInt32(hdnValue.Value), Controller.RoleIdByUserName(Page.User.Identity.Name));
        if (dt.Rows.Count > 0)
        {
            //string url = dt.Rows[0]["URL"].ToString();

            //if (url.Contains("Report"))
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        row["URL"] = "window.open('" + row["URL"] + "', 'name')";
            //    }
            //}

            rptChild.DataSource = dt;
            rptChild.DataBind();

        }



    }

}