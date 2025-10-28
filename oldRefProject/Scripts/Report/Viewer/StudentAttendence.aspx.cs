using System;
using System.Collections.Generic;
using System.Linq;
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
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query="+EncryptionDecryption.EncryptDecryptString(GetCriteria())+"&report=stAttendence");
    }

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Year=" + ddlYear.SelectedItem.Text;
            else
                criteria += " ss_Attendence.Year=" + ddlYear.SelectedItem.Text;
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
        //if (tbxDate.Text != "")
        //{
        //    if (criteria == "")
        //        criteria = "Date='" + Convert.ToDateTime(tbxDate.Text)+"'";
        //    else
        //        criteria += " and Date='" + Convert.ToDateTime(tbxDate.Text)+"'";
        //}

        if (ddlAttendenceType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.AttendenceType=" + ddlAttendenceType.SelectedValue;
            else
                criteria += " and ss_Attendence.AttendenceType=" + ddlAttendenceType.SelectedValue;
        }

        return criteria;
    }
    #endregion
}