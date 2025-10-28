using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_Attendence : System.Web.UI.Page
{
    dalStudent obj = new dalStudent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckRollNoUse();
            Load();
        }

    }
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
    public bool ValidationCheck()
    {       
        if (rdAttendence.SelectedValue == "2")
        {
            if (tbxFromDate.Text == "" || tbxToDate.Text == "")
            {
                string myStringVariable = "Date Must be needed!!!";
                MessageController.Show("Date Must be needed!!!", MessageType.Warning, Page);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                return true;
            }

            if (DateTime.ParseExact(tbxToDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(tbxFromDate.Text, "dd/MM/yyyy", null))
            {
                MessageController.Show("From date must be smaller then To date.", MessageType.Error, Page);
                string myStringVariables = "From date must be smaller then To date.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariables + "');", true);
                return true;
            }
        }
        else if (rdAttendence.SelectedValue == "1")
        {
            return false;
        }
        else
        {
            string myStringVariable = "Must be Select any Type from Monthly or Date!!!";
            MessageController.Show("Must be Select any Type from Monthly or Date!!!", MessageType.Warning, Page);
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            return true;
        }
        return false;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (ValidationCheck())
        {
            rptStudent.DataSource = null;
            rptStudent.DataBind();
            return;
        }
        string criteria = GetCriteria();
        int roll = (!string.IsNullOrEmpty(tbxRoll.Text)) ? Convert.ToInt32(tbxRoll.Text) : 0;
        DataTable dt = obj.GetAttendenceByCriteria(criteria, roll);
        if(dt.Rows.Count>0)
        {
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
        }
        else
        {
            MessageController.Show("No record found to Show.", MessageType.Warning, Page);
            rptStudent.DataSource = null;
            rptStudent.DataBind();
        }
    }

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";        

        if (rdList.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "AttendenceType=" + rdList.SelectedValue;
            else
                criteria += " and AttendenceType=" + rdList.SelectedValue;
        }
        //-----Newly Implemented-----\\
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
        if (tbxRoll.Text != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.RollNo=" + tbxRoll.Text;
            else
                criteria += " and er_StudentToClass.RollNo=" + tbxRoll.Text;
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
        }

        if (rdAttendence.SelectedValue == "1")
        {
            if (ddlYear.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "ss_Attendence.Year=" + ddlYear.SelectedValue;
                else
                    criteria += " and ss_Attendence.Year=" + ddlYear.SelectedValue;
            }
            if (ddlMonth.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "ss_Attendence.Month=" + ddlMonth.SelectedValue;
                else
                    criteria += " and ss_Attendence.Month=" + ddlMonth.SelectedValue;
            }            
           
        }
        else
        {
            DateTime srtDate = DateTime.ParseExact(tbxFromDate.Text, "dd/MM/yyyy", null);
            string fromDate = srtDate.ToString("MM/dd/yyyy");
            DateTime endDate = DateTime.ParseExact(tbxToDate.Text, "dd/MM/yyyy", null);
            string toDate = endDate.ToString("MM/dd/yyyy");

            if (tbxFromDate.Text != "")
            {
                if (criteria == "")
                    criteria = "ss_Attendence.Date>='" + fromDate + "'";
                else
                    criteria += " and ss_Attendence.Date>='" + fromDate + "'";
            }
            if (tbxToDate.Text != "")
            {
                if (criteria == "")
                    criteria = "ss_Attendence.Date<='" + toDate + "'";
                else
                    criteria += " and ss_Attendence.Date<='" + toDate + "'";
            }
          
        }
        criteria += " and ss_Student.IsActive='True'";
        return criteria;
    }

    #endregion

}