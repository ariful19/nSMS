using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Common.SessionInfo == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];
            if (cookie != null && cookie.Value != null)
            {
                if (cookie.Value.IndexOf("en-") >= 0)
                {
                    ddlLanguage.SelectedValue = "en-US";
                }
                else
                {
                    ddlLanguage.SelectedValue = "bn-BD";
                }
            }
            lblYear.Text = DateTime.Now.Year.ToString();
            if (Page.Title != "")
            {
                litPage.Text = Page.Title;
            }
            Page.Header.DataBind();
        }
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("CurrentLanguage");
        cookie.Value = ddlLanguage.SelectedValue;
        cookie.Expires = DateTime.Now.AddMonths(6);
        Response.SetCookie(cookie);
        Response.Cookies.Add(cookie);
        Response.Redirect(Request.RawUrl);
    }
}
