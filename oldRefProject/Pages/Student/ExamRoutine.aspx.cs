using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_ExamRoutine : System.Web.UI.Page
{
    dalRoutine obj = new dalRoutine();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Load();
            if (Common.SessionInfo.StudentId > 0)
            {
                ddlClass.SelectedValue = Common.SessionInfo.ClassId.ToString();
                ddlGroup.SelectedValue = Common.SessionInfo.GroupId.ToString();
                ddlShift.SelectedValue = Common.SessionInfo.ShiftId.ToString();
                ddlYear.Enabled = false;
                ddlClass.Enabled = false;
                ddlGroup.Enabled = false;
                ddlShift.Enabled = false;
            }
            pnlExamRoutine.Visible = false;
        }
    }

    protected void btnShowRoutine_Click(object sender, EventArgs e)
    {
        string criteria = GetCriteria();
        DataTable dt = obj.GetExamRoutineByCriteria(criteria);
        if(dt.Rows.Count>0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
            pnlExamRoutine.Visible = true;
            ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
        }
        else
        {
            pnlExamRoutine.Visible = false;
            MessageController.Show("No Exam Routine Found.",MessageType.Warning,Page);
        }

    }
    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
        ddlYear.DataBind();

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlExamType.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamType.DataBind();
    }
    #endregion
    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_ExamRoutine.Year=" + ddlYear.SelectedValue;
            else
                criteria += " bs_ExamRoutine.Year=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_ExamRoutine.MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and bs_ExamRoutine.MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_ExamRoutine.CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and bs_ExamRoutine.CampusId=" + ddlCampus.SelectedValue;
        }
        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_ExamRoutine.ClassId=" + ddlClass.SelectedValue;
            else
                criteria += " and bs_ExamRoutine.ClassId=" + ddlClass.SelectedValue;
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_ExamRoutine.GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and bs_ExamRoutine.GroupId=" + ddlGroup.SelectedValue;
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_ExamRoutine.ShiftId=" + ddlShift.SelectedValue;
            else
                criteria += " and bs_ExamRoutine.ShiftId=" + ddlShift.SelectedValue;
        }
        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_ExamRoutine.ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and bs_ExamRoutine.ExamTypeId=" + ddlExamType.SelectedValue;
        }
        return criteria;
    }
    #endregion
}