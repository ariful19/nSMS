using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_StudentList : BasePage
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
        var item=ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);
        //ddlYear.DataSource = new dalYear().GetDefault();
        //ddlYear.DataBind();

        Common.LoadDropdown(ddlMedium, "bs_Medium", 1, 0);

        Common.LoadDropdown(ddlClass, "bs_ClassName", 1, 0);

        Common.LoadDropdown(ddlCampus, "bs_CampusName", 1, 0);

        Common.LoadDropdown(ddlGroup, "bs_Group", 1, 0);

        Common.LoadDropdown(ddlShift, "bs_Shift", 1, 0);

        Common.LoadDropdown(ddlSection, "bs_Section", 1, 0);
    }
    #endregion
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rdList.SelectedValue == "1")
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=studentList");
            Response.Redirect("../../Report/Viewer/ReportView.aspx");

        }
        else if (rdList.SelectedValue == "2")
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=AllAssignstudentList");
            Response.Redirect("../../Report/Viewer/ReportView.aspx");
        }
        else if (rdList.SelectedValue == "3")
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=UnassignstudentList");
            Response.Redirect("../../Report/Viewer/ReportView.aspx");
        }
    else
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=AllInActiveStudentList");
            Response.Redirect("../../Report/Viewer/ReportView.aspx");
        }
    }
    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";

        if(rdList.SelectedValue=="3")
        {
            criteria = "ss_Student.StatusId= 2";
        }
        else
        {
            if (ddlYear.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
                else
                    criteria += " and er_StudentToClass.Year=" + ddlYear.SelectedValue;
            }
            if (ddlCampus.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "bs_CampusName.Id=" + ddlCampus.SelectedValue;
                else
                    criteria += " and bs_CampusName.Id=" + ddlCampus.SelectedValue;
            }

            if(rdList.SelectedValue=="1")
            {
                if (ddlMedium.SelectedValue != "")
                {
                    if (criteria == "")
                        criteria = "bs_Medium.Id=" + ddlMedium.SelectedValue;
                    else
                        criteria += " and bs_Medium.Id=" + ddlMedium.SelectedValue;
                }
              
                if (ddlClass.SelectedValue != "")
                {
                    if (criteria == "")
                        criteria = "bs_ClassName.Id=" + ddlClass.SelectedValue;
                    else
                        criteria += " and bs_ClassName.Id=" + ddlClass.SelectedValue;
                }
                if (ddlGroup.SelectedValue != "")
                {
                    if (criteria == "")
                        criteria = "bs_Group.Id=" + ddlGroup.SelectedValue;
                    else
                        criteria += " and bs_Group.Id=" + ddlGroup.SelectedValue;
                }
                if (ddlShift.SelectedValue != "")
                {
                    if (criteria == "")
                        criteria = "bs_Shift.Id=" + ddlShift.SelectedValue;
                    else
                        criteria += " and bs_Shift.Id=" + ddlShift.SelectedValue;
                }
                if (ddlSection.SelectedValue != "")
                {
                    if (criteria == "")
                        criteria = "bs_Section.Id=" + ddlSection.SelectedValue;
                    else
                        criteria += " and bs_Section.Id=" + ddlSection.SelectedValue;
                }
                criteria += " and ss_Student.StatusId= 1";
            }
            else
            {
                if(rdList.SelectedValue=="2")
                {
                    criteria += " and ss_Student.StatusId= 1";
                }
                else
                {
                    criteria += " and ss_Student.StatusId= 2";
                }
            }
        }
        
        return criteria;
    }
    [WebMethod]
    public static string GetSearchCriteria(string year, string mediumID, string campusID, string classID, string groupID, string shiftID, string sectionID)
    {
        string criteria = "";
        if (year != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + year;
            else
                criteria += " er_StudentToClass.Year=" + year;
        }
        if (mediumID != "")
        {
            if (criteria == "")
                criteria = "bs_Medium.Id=" + mediumID;
            else
                criteria += " and bs_Medium.Id=" + mediumID;
        }
        if (campusID != "")
        {
            if (criteria == "")
                criteria = "bs_CampusName.Id=" + campusID;
            else
                criteria += " and bs_CampusName.Id=" + campusID;
        }
        if (classID != "")
        {
            if (criteria == "")
                criteria = "bs_ClassName.Id=" + classID;
            else
                criteria += " and bs_ClassName.Id=" + classID;
        }
        if (groupID != "")
        {
            if (criteria == "")
                criteria = "bs_Group.Id=" + groupID;
            else
                criteria += " and bs_Group.Id=" + groupID;
        }
        if (shiftID != "")
        {
            if (criteria == "")
                criteria = "bs_Shift.Id=" + shiftID;
            else
                criteria += " and bs_Shift.Id=" + shiftID;
        }
        if (sectionID != "")
        {
            if (criteria == "")
                criteria = "bs_Section.Id=" + sectionID;
            else
                criteria += " and bs_Section.Id=" + sectionID;
        }

        criteria = EncryptionDecryption.EncryptDecryptString(criteria);

        return criteria;
    }
    #endregion
    protected void LoadAllUnassignStudent()
    {
        //DataTable dt = obj.GetUnassignStudent(ddlYear.SelectedItem.Text);
    }
}