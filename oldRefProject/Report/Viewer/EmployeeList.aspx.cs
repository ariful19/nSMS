using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_EmployeeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
        }
    }

    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlGrade.DataSource = new Common().GetAll("tbl_Grade");
        ddlGrade.DataBind();

        ddlLevel.DataSource = new Common().GetAll("tbl_Level");
        ddlLevel.DataBind();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rdList.SelectedValue != "3")
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=teacher&Type=" + rdList.SelectedValue);
        }
        else
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=TeacherIdCard&Type=" + rdList.SelectedValue);
        }
       
    }

    public string GetCriteria()
    {
        string criteria = "";
        if (rdList.SelectedValue != "2")
        {
            if (ddlYear.SelectedValue != "")
            {
                //if (criteria == "")
                //    criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
                //else
                //    criteria += " and er_StudentToClass.Year=" + ddlYear.SelectedValue;
            }
            if (ddlCampus.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "tr_Teacher.Campus=" + ddlCampus.SelectedValue;
                else
                    criteria += " and tr_Teacher.Campus=" + ddlCampus.SelectedValue;
            }
            if (ddlGrade.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "tr_Teacher.GradeId=" + ddlGrade.SelectedValue;
                else
                    criteria += " and tr_Teacher.GradeId=" + ddlGrade.SelectedValue;
            }
            if (ddlLevel.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "tr_Teacher.LevelId=" + ddlLevel.SelectedValue;
                else
                    criteria += " and tr_Teacher.LevelId=" + ddlLevel.SelectedValue;
            }
            if (tbxEmployeeId.Text != "")
            {
                criteria += " and tr_Teacher.EmployeeId=" + tbxEmployeeId.Text;
            }
           criteria += " and tr_Teacher.IsActive= 1";
        }
       
        
        return criteria;
    }
}


