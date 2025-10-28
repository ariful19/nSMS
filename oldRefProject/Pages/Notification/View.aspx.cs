using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_View : System.Web.UI.Page
{
    dalNotice objNotice = new dalNotice();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(Request.QueryString["ID"]!=null)
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]);
                DataTable dt = objNotice.GetById(ID);
                LoadNotice(dt);
                SeenInfo(dt);
            }
        }
    }

    
    int ID
    {
        set { ViewState["ID"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["ID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    protected void LoadNotice(DataTable dt)
    {
        
      
        if(dt.Rows.Count>0)
        {
            lblTitle.Text=dt.Rows[0]["Title"].ToString();
            lblFrom.Text = dt.Rows[0]["CreatedBy"].ToString();
            lblDate.Text = dt.Rows[0]["NoticeDate"].ToString();
            litContent.Text = dt.Rows[0]["Details"].ToString();
            lblShortDes.Text = dt.Rows[0]["ShortDescription"].ToString();
        }
    }

    private void SeenInfo(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["student_NId"].ToString() != "")
            {
                int id = objNotice.UpdateSeenInfoStu(Convert.ToInt32(dt.Rows[0]["student_NId"]), true);
            }
            if (dt.Rows[0]["teacher_NId"].ToString() != "")
            {
                int id= objNotice.UpdateSeenInfoTeach(Convert.ToInt32(dt.Rows[0]["teacher_NId"]), true);
            }
        }
    }
}