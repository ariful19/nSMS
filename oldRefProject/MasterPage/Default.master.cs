using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Default : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Common.SessionInfo == null)
        {
            Common.SessionInfo = new bdoSessionInfo();
            LoadSession();
        }
    }
    protected void LoadSession()
    {
        DataTable dt = new Common().GetAll("bs_GeneralSetting");
        if (dt.Rows.Count > 0)
        {
            Common.SessionInfo.ColorTheme = dt.Rows[0]["Theme"].ToString();
            Common.SessionInfo.DateFormat = dt.Rows[0]["DateFormat"].ToString();
            Common.SessionInfo.TimeZone = dt.Rows[0]["TimeZone"].ToString();
            Common.SessionInfo.Button = dt.Rows[0]["Button"].ToString();
            Common.SessionInfo.Panel = dt.Rows[0]["Panel"].ToString();
        }


    }
    //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    HttpCookie cookie = new HttpCookie("Language");
    //    cookie.Value = ddlLanguage.SelectedValue.ToString();
    //    cookie.Expires = DateTime.Now.AddMonths(6);
    //    Response.SetCookie(cookie);
    //    Response.Redirect(Request.RawUrl);
    //}
}
