using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_List : System.Web.UI.Page
{
    dalNotice objNotice = new dalNotice();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadNotice();
        }
    }
    protected void LoadNotice()
    {
        if (Page.User.IsInRole("SystemAdmin") || Page.User.IsInRole("Admin"))
        {

        }
        else if (Page.User.IsInRole("Student"))
        {
            DataTable dt = objNotice.GetNoticeForStudent(Common.SessionInfo.StudentToClassId);
            if (dt.Rows.Count > 0)
            {
                rptNotice.DataSource = dt;
                rptNotice.DataBind();               
            }
            else
            {
            }
        }
        else
        {
            DataTable dt = objNotice.UnseenNoticeForTeacher(16);
            if (dt.Rows.Count > 0)
            {

                rptNotice.DataSource = dt;
                rptNotice.DataBind();
            }
            else
            {

            }

        }
    }
}