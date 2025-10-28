using Nano.DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Dashboard : BasePage
{
    dalDashbord obj = new dalDashbord();
    protected void Page_Load(object sender, EventArgs e)
    {
        using (var dm = new DatabaseManager())
        {
            var dt = dm.ExecuteQueryDt("select * from wr_Date where Id=1");

            DateTime date = dt.Rows.Count > 0 ? dt.Rows[0].Field<DateTime>("ExpireDate") : DateTime.Now.AddDays(1);
            var df = date - DateTime.Now;
            Session["expireDays"] = df.Days;
            Session["IsAccountsOrAdmin"] = Page.User.IsInRole("Accountant") || Page.User.IsInRole("Admin") || Page.User.IsInRole("Sr. Accountant");
        }

        if(!IsPostBack)
        {
            int boys = 0;
            int girls = 0;
            int others = 0;
            int totalStudent = 0;
            if (Page.User.IsInRole("Student"))
            {
                LoadClassInformation();
            }
            string criteria = "bs_Year.Year="+DateTime.Now.Year;
            DataTable dtStudent = new dalStudent().GetByCriteria(criteria);

            if (dtStudent.Rows.Count > 0)
            {
                var q = dtStudent.AsEnumerable()
          .GroupBy(r => r["ClassName"])
          .Select(p => new { ClassName = p.Key, StuCount = p.Count(r => r["RegNo"].ToString() != ""), Boys = p.Count(r => r["GenderId"].ToString() == "1"), Girls = p.Count(r => r["GenderId"].ToString() == "2"), Others = p.Count(r => r["GenderId"].ToString() == "3") })
          .ToList();
                foreach (var item in q)
                {
                     totalStudent += Convert.ToInt32(item.StuCount);
                        boys += Convert.ToInt32(item.Boys);
                        girls += Convert.ToInt32(item.Girls);
                        others += Convert.ToInt32(item.Others);                     
               }
     
            }
            lblBoys.Text = boys.ToString();
            lblGirls.Text = girls.ToString();
            lblStudent.Text = obj.GetStudent().ToString();
            lblTeacher.Text = obj.GetTeacher().ToString();
            lblUniqueVisit.Text = obj.GetUniqueVisitor().ToString();
            lblOnlineUser.Text = Membership.GetNumberOfUsersOnline().ToString();
            LoadYear();    
            
        }
    }
    protected void LoadYear()
    {
        DataTable dt = new Common().GetAll("bs_Year");
        ddlYear.DataSource = dt;
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);
    }

    [WebMethod]
    public static ArrayList ClassWiseStudent(int pData)
    {
        ArrayList list = new ArrayList();
        DataTable dt = new dalStudent().GetStudentForAllClass(pData);
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new ListItem(
                   row["ClassName"].ToString(),
                   row["Student"].ToString()
                    ));
        }

        return list;
    }
    protected void LoadClassInformation()
    {
        if (Page.User.Identity.Name != "")
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


