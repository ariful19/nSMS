using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_StudentIdCard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
    
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
            else
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += "and er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
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
        if (tbxRegNo.Text != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.tbxRegNo.Text=" + tbxRegNo.Text;
            else
                criteria += " and RegNo='" + tbxRegNo.Text +"'";
        }
       criteria += " and ss_Student.StatusId= 1";
      if (criteria != "")
        {
            DataTable dt = new dalReport().GetStudentforIdCard(criteria);

            if (dt.Rows.Count>0)
            {
                lblNoRecordFond.Text = "";
                Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(criteria) + "&report=IdCard&Type=" + rdList.SelectedValue + "&SelectId=" + ddlExamType.SelectedItem.Text);  
            }
            else
            {
                lblNoRecordFond.Text = "No record found!";

            }
        }
    }
    #region Load Data
    protected void Load()
    {
        ddlExamType.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamType.DataBind();

        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

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

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

    }
    #endregion

}






