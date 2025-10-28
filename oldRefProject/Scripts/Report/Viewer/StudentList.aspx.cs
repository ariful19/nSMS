using System;
using System.Collections.Generic;
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=studentList");
        Response.Redirect("../../Report/Viewer/ReportView.aspx");
    }

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
            else
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
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

        return criteria;
    }

    [WebMethod]
    public static string GetSearchCriteria(string year, string classID, string groupID, string shiftID, string sectionID)
    {       

        string criteria = "";
        if (year != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + year;
            else
                criteria += " er_StudentToClass.Year=" + year;
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
}