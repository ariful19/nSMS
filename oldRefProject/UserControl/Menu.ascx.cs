using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Menu : System.Web.UI.UserControl
{
    dalHomeMenu objMenu = new dalHomeMenu();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParent();
            LoadSchoolInfo();
            if (Request.Cookies["CurrentLanguage"].Value == "bn-BD")
            {
                lnkBangla.Visible = false;
            }
            else
            {
                lnkEnglish.Visible = false;
            }

        }

    }

    protected void GetParent()
    {
        DataTable dt = objMenu.GetAllParanet();

        // DataTable dt = objMenu.GetParanet();

        if (dt.Rows.Count > 0)
        {
            rptParent.DataSource = dt;
            rptParent.DataBind();  //rptMenu

            rptMenu.DataSource = dt;
            rptMenu.DataBind();
        }

    }
    protected void rptCategory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rptChild = (Repeater)e.Item.FindControl("rptChild");
        HiddenField hdnValue = (HiddenField)e.Item.FindControl("hdnValue");
        //DataTable dt = objMenu.GetAllChildByParentID(Convert.ToInt32(hdnValue.Value));

        DataTable dt = objMenu.GetChildByParentID(Convert.ToInt32(hdnValue.Value));

        if (dt.Rows.Count > 0)
        {

            rptChild.DataSource = dt;
            rptChild.DataBind();

        }

    }

    protected void LoadSchoolInfo()
    {
        //DataTable dt = new Common().GetAll("bs_SchoolInformation");
        //if (dt.Rows.Count > 0)
        //{
        //    lblSchool.Text = dt.Rows[0]["Name"].ToString();
        //}
    }
   
    protected void lnkBangla_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("CurrentLanguage");
        cookie.Value = "bn-BD";
        cookie.Expires = DateTime.Now.AddMonths(6);
        Response.SetCookie(cookie);
        Response.Cookies.Add(cookie);
        Response.Redirect(Request.RawUrl);
        lnkBangla.Visible = false;
    }
    protected void lnkEnglish_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("CurrentLanguage");
        cookie.Value = "en-US";
        cookie.Expires = DateTime.Now.AddMonths(6);
        Response.SetCookie(cookie);
        Response.Cookies.Add(cookie);
        Response.Redirect(Request.RawUrl);
        lnkEnglish.Visible = false;
    }
}