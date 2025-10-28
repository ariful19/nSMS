using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_ClassRoutine : System.Web.UI.Page
{
    dalRoutine objRoutine = new dalRoutine();
    List<string> day = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            pnlClassRoutine.Visible = false;
            if (Common.SessionInfo.StudentId > 0)
            {
                ddlClass.SelectedValue = Common.SessionInfo.ClassId.ToString();
                ddlGroup.SelectedValue = Common.SessionInfo.GroupId.ToString();
                ddlShift.SelectedValue = Common.SessionInfo.ShiftId.ToString();
                ddlSection.SelectedValue = Common.SessionInfo.SectionId.ToString();
                ddlYear.Enabled = false;
                ddlClass.Enabled = false;
                ddlGroup.Enabled = false;
                ddlShift.Enabled = false;
                ddlSection.Enabled = false;
            }
           
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

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();
    }
    #endregion
    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Year=" + ddlYear.SelectedValue;
            else
                criteria += " Year=" + ddlYear.SelectedValue;
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
        return criteria;
    }
    #endregion
    protected void btnShowRoutine_Click(object sender, EventArgs e)
    {
        string criteria = GetCriteria();
        //DataTable dt = objRoutine.GetByCriteria(criteria);
        DataTable dt = objRoutine.GetClassRoutineByCriteria(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
        
        if (dt.Rows.Count > 0)
        {
            gv.DataSource = dt;
            gv.DataBind();
            pnlClassRoutine.Visible = true;
            ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
        }
        else
        {
            pnlClassRoutine.Visible = false;
            MessageController.Show("No Class Routine Found.", MessageType.Warning, Page);
        }

    }
    private List<DataRow> GetAllDatarow(DataTable dt, string dayName)
    {
        List<DataRow> lstDatarow = new List<DataRow>();

        foreach (DataRow row in dt.Rows)
        {
            if (row["Day"].ToString() == dayName)
            {
                lstDatarow.Add(row);
            }
        }

        return lstDatarow;
    }

}