using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Data;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    protected override void OnPreInit(EventArgs e)
    {
        //if (Common.SessionInfo == null)
        //{
        Common.SessionInfo = new bdoSessionInfo();
        LoadSession();
        LoadClassInformation();
        // }

    }
    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override void InitializeCulture()
    {
        string lang = String.Empty;
        HttpCookie cookie = Request.Cookies["CurrentLanguage"];
        if (cookie != null && cookie.Value != null)
        {
            lang = cookie.Value;
            CultureInfo Cul = CultureInfo.CreateSpecificCulture(lang);
            System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
            System.Threading.Thread.CurrentThread.CurrentCulture = Cul;
        }
        else
        {
            lang = "en-US";
            CultureInfo Cul = CultureInfo.CreateSpecificCulture(lang);
            System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
            System.Threading.Thread.CurrentThread.CurrentCulture = Cul;

            HttpCookie cookie_new = new HttpCookie("CurrentLanguage");
            cookie_new.Value = lang;
            cookie_new.Expires = DateTime.Now.AddMonths(6);
            Response.SetCookie(cookie_new);
        }
        base.InitializeCulture();
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

            Common.SessionInfo.StudentId = 0;
            Common.SessionInfo.StudentToClassId = 0;
            Common.SessionInfo.ClassId = 0;
        }
    }
    protected void LoadClassInformation()
    {
        if (Page.User.IsInRole("Student"))
        {
            DataTable dt = new dalStudent().ClassInfoByUserName(Page.User.Identity.Name);
            {
                if (dt.Rows.Count > 0)
                {
                    Common.SessionInfo.StudentId = Convert.ToInt32(dt.Rows[0]["StudentId"].ToString());
                    Common.SessionInfo.StudentToClassId = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                    Common.SessionInfo.ClassId = Convert.ToInt32(dt.Rows[0]["ClassId"].ToString());
                    Common.SessionInfo.GroupId = Convert.ToInt32(dt.Rows[0]["GroupId"].ToString());
                    Common.SessionInfo.ShiftId = Convert.ToInt32(dt.Rows[0]["ShiftId"].ToString());
                    Common.SessionInfo.SectionId = Convert.ToInt32(dt.Rows[0]["SectionId"].ToString());
                }
            }
        }
    }
}