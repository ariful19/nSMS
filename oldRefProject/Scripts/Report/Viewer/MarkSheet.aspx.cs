using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_MarkSheet : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
        }
    }
    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        ddlExamType.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamType.DataBind();

    }
    #endregion
    protected void btnReport_Click(object sender, EventArgs e)
    {
        DataTable dtSubjectWiseMarks = new dalMarks().GetStudentMarks(GetCriteria());
        if (dtSubjectWiseMarks.Rows.Count > 0)
        {
            lblNoRecordFond.Text = string.Empty;
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=markSheet");            
        }
        else
        {
            lblNoRecordFond.Text = "No record found!";
        }
       
    }

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.YearId=" + ddlYear.SelectedItem.Text;
            else
                criteria += "rs_ObtainMarks.YearId=" + ddlYear.SelectedItem.Text;
        }

        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
            else
                criteria += " and er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
            else
                criteria += " and er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
        }
        if (ddlSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
            else
                criteria += " and er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
        }
        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
        }
        if (tbxRollNo.Text != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.RollNo=" + tbxRollNo.Text;
            else
                criteria += " and er_StudentToClass.RollNo=" + tbxRollNo.Text;
        }

        return criteria;
    }

    #endregion
}