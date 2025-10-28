using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Designer_StudentAttendence : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Load();
        }

    }
    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlAttendenceType.DataSource = new Common().GetAll("bs_AttendenceType");
        ddlAttendenceType.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

    }
    #endregion

    protected void btnReport_Click(object sender, EventArgs e)
    {

            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=stAttendence");
    }

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        DateTime srtDate = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        string sDate = srtDate.ToString("MM/dd/yyyy");

        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Year=" + ddlYear.SelectedValue;
            else
                criteria += " ss_Attendence.Year=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and CampusId=" + ddlCampus.SelectedValue;
        }
        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ClassId=" + ddlClass.SelectedValue;
            else
                criteria += " and ClassId=" + ddlClass.SelectedValue;
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and GroupId=" + ddlGroup.SelectedValue;
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ShiftId=" + ddlShift.SelectedValue;
            else
                criteria += " and ShiftId=" + ddlShift.SelectedValue;
        }
        if (ddlSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "SectionId=" + ddlSection.SelectedValue;
            else
                criteria += " and SectionId=" + ddlSection.SelectedValue;
        }    
        if (ddlAttendenceType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.AttendenceType=" + ddlAttendenceType.SelectedValue;
            else
                criteria += " and ss_Attendence.AttendenceType=" + ddlAttendenceType.SelectedValue;
        }
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Date>='" + sDate + "'";
            else
                criteria += " and ss_Attendence.Date>='" + sDate + "'";
        }
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Date<='" + sDate + "'";
            else
                criteria += " and ss_Attendence.Date<='" + sDate + "'";
        }
        return criteria;
    }
    #endregion
    Regex regex = new Regex(@"[1-9]");

    private bool IsInteger(string str)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (!regex.IsMatch(str))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            MessageController.Show("Value Must be Neumeric!!!", MessageType.Warning,Page);
        }
        return false;
    }
}