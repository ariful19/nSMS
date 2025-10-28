using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_SubjectList : BasePage
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

    #region Variable
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

    #endregion

    protected void CheckRollNoUse()
    {
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            IsRollNoUse = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
        }
        if (IsRollNoUse == "False")
        {
            //LabelRoll.Visible = false;
            //tbxRollNo.Visible = false;
        }
        else
        {
            LabelReg.Visible = false;
            tbxReg.Visible = false;
        }

    }

    //private bool CheckValidation()
    //{
    //    bool result = true;
    //    if (tbxRollNo.Visible)
    //    {
    //        if (tbxRollNo.Text == "")
    //        {
    //            tbxRollNo.BorderColor = System.Drawing.Color.Red;
    //            MessageController.Show(" Enter Roll No First....", MessageType.Warning, Page);
    //            result = false;
    //        }
    //    }
    //    else
    //    {
    //        if (tbxReg.Text == "")
    //        {
    //            tbxReg.BorderColor = System.Drawing.Color.Red;
    //            MessageController.Show(" Enter Reg. No First....", MessageType.Warning, Page);
    //            result = false;
    //        }
    //    }
    //    return result;
    //}

    //Regex regex = new Regex(@"[1-9]");
    //private bool IsInteger(string str)
    //{
    //    try
    //    {
    //        if (String.IsNullOrWhiteSpace(str))
    //        {
    //            return false;
    //        }
    //        if (!regex.IsMatch(str))
    //        {
    //            return false;
    //        }

    //        return true;

    //    }
    //    catch (Exception ex)
    //    {
    //        MessageController.Show("Roll No Must be Neumeric!!!", MessageType.Warning, Page);
    //    }
    //    return false;
    //}

    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        DataTable dtSubjectWiseMarks=new DataTable();
        if (rdList.SelectedValue == "1")
        {
            dtSubjectWiseMarks = new dalSubject().GetSubjectToClass(GetCriteria());
        }
        else
            dtSubjectWiseMarks = new dalSubject().GetOptionalSubject(GetCriteria());
      
            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                lblNoRecordFond.Text = string.Empty;
                Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=subjectToClass&SelectId=" + rdList.SelectedValue);
                
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
                criteria = "er_SubjectToClass.YearId=" + ddlYear.SelectedValue;
            else
                criteria += "er_SubjectToClass.YearId=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_SubjectToClass.MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and er_SubjectToClass.MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_SubjectToClass.CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and er_SubjectToClass.CampusId=" + ddlCampus.SelectedValue;
        }
        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_SubjectToClass.ClassId=" + ddlClass.SelectedValue;
            else
                criteria += " and er_SubjectToClass.ClassId=" + ddlClass.SelectedValue;
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_SubjectToClass.GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and er_SubjectToClass.GroupId=" + ddlGroup.SelectedValue;
        }

        if (rdList.SelectedValue == "2")
        {
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
            if (tbxReg.Text != "")
            {
                if (criteria == "")
                    criteria = "ss_Student.RegNo=" + tbxReg.Text;
                else
                    criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
            }
            criteria += " and ss_Student.StatusId= 1";
        }

    
        return criteria;
    }


    #endregion
}