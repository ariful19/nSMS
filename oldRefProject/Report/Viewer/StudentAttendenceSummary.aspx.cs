using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_StudentAttendenceSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckRollNoUse();
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

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();
    }
    #endregion

    string IsRollNoUse
    {
        set { ViewState["StudentId"] = value; }
        get
        {
            try
            {
                return ViewState["StudentId"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    protected void CheckRollNoUse()
    {
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            IsRollNoUse = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
        }
        if (IsRollNoUse == "False")
        {
            LabelRoll.Visible = false;
            tbxRoll.Visible = false;
        }
        else
        {
            LabelReg.Visible = false;
            tbxReg.Visible = false;
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=AttendenceSummary");
      
    }

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";

        DateTime srtDate = DateTime.ParseExact(tbxFromDate.Text, "dd/MM/yyyy", null);
        string fromDate = srtDate.ToString("MM/dd/yyyy");
        DateTime endDate = DateTime.ParseExact(tbxToDate.Text, "dd/MM/yyyy", null);
        string toDate = endDate.ToString("MM/dd/yyyy");

        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "AttendenceYear=" + ddlYear.SelectedItem.Text;
            else
                criteria += "AttendenceYear=" + ddlYear.SelectedItem.Text;
        }
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "MediumName='" + ddlMedium.SelectedItem.Text + "'";
            else
                criteria += " and MediumName='" + ddlMedium.SelectedItem.Text + "'";
        }
        if (ddlAttendenceType.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = "AttendenceType" + ddlAttendenceType.SelectedValue;
            }
            else
            {
                criteria += " and AttendenceType= " + ddlAttendenceType.SelectedValue;
            }
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "CampusName='" + ddlCampus.SelectedItem.Text + "'";
            else
                criteria += "and CampusName='" + ddlCampus.SelectedItem.Text + "'";
        }
        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Class='" + ddlClass.SelectedItem.Text + "'";
            else
                criteria += " and Class='" + ddlClass.SelectedItem.Text+"'";
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "GroupName='" + ddlGroup.SelectedItem.Text + "'";
            else
                criteria += " and GroupName='" + ddlGroup.SelectedItem.Text+"'";
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Shift='" + ddlShift.SelectedItem.Text + "'";
            else
                criteria += " and Shift='" + ddlShift.SelectedItem.Text+"'";
        }
        if (ddlSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria += "Section='" + ddlSection.SelectedItem.Text + "'";
            else
                criteria += " and Section='" + ddlSection.SelectedItem.Text+"'";
        }
        if (tbxFromDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "Date=" + fromDate;
                Session["rptFromDate"] = fromDate;
            }
            else
            {
                Session["rptFromDate"] = fromDate;
                criteria += " and Date between '" + fromDate + "'";
            }           
        }
        if (tbxToDate.Text != "")
        {
            if (criteria == "")
            {
                criteria = "and" + toDate;
                Session["rptToDate"] = toDate;
            }
            else
            {
                Session["rptToDate"] = toDate;
                criteria += "and '" + toDate + "'";
            }        
        }
        if (tbxRoll.Visible)
        {
            if (tbxRoll.Text != "")
            {
                if (criteria == "")
                    criteria = "and" + tbxRoll.Text;
                else
                    criteria += " and RollNo= " + tbxRoll.Text;
            }
            else
            {
                criteria += "order by RollNo";
            }
        }
        else
        {
            if (tbxReg.Text != "")
            {
                if (criteria == "")
                    criteria = "RegNo=" + tbxReg.Text;
                else
                    criteria += " and RegNo='" + tbxReg.Text + "'";
            }
            else
            {
                criteria += "order by RegNo";
            }
        }
        return criteria;
    }
    #endregion

}