using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Result_MarksView : System.Web.UI.Page
{

    dalStudent objStudent = new dalStudent();
    dalSubject objSubject = new dalSubject();
    dalMarks objMarks = new dalMarks();
    dalReport obj= new dalReport();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (!IsPostBack)
        {
            Load();
            LoadSubject();
            CheckRollNoUse();
            pnlAssignStudent.Visible = false;
        }
    }
    #region Load Data

    protected void Load()
    {
       // ddlYear.DataSource = new dalYear().GetDefault();
      // ddlYear.DataBind();

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

        ddlMarksOutOf.DataSource = new Common().GetAll("bs_OutOfMarks");
        ddlMarksOutOf.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        ddlExamType.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamType.DataBind();
    }

    protected void LoadSubject()
    {
        DataTable dt = objSubject.GetByClassAndGroupId(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            pnlAssignStudent.Visible = true;
            ddlSubject.DataSource = dt;
            ddlSubject.DataBind();
        }
        else
        {
            pnlAssignStudent.Visible = false;
        }
    }

    #endregion

    #region Get Criteria

    protected string GetCriteria()
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
                criteria = "er_StudentToClass.CampusId" + ddlCampus.SelectedValue;
            else
                criteria += " and er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
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
        if (ddlSubject.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.SubjectId=" + ddlSubject.SelectedValue;
            else
                criteria += " and rs_ObtainMarks.SubjectId=" + ddlSubject.SelectedValue;
        }
        //if (ddlMarksOutOf.SelectedItem.Text != "")
        //{
        //    if (criteria == "")
        //        criteria = "rs_ObtainMarks.BaseMarks=" + ddlMarksOutOf.SelectedItem.Text;
        //    else
        //        criteria += " and rs_ObtainMarks.BaseMarks=" + ddlMarksOutOf.SelectedItem.Text;
        //}
        if (tbxRoll.Text != "")
        {
            if (criteria == "")
                criteria = "RollNo=" + tbxRoll.Text;
            else
                criteria += " and RollNo=" + tbxRoll.Text;
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
        }
        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
        }
        //criteria += " and IsFinishMarks='false'";
        //criteria += " and ss_Student.IsActive='True'";
        //criteria += " and ss_Student.StatusId= 1";
        return criteria;
    }

    protected string GetCriterias()
    {
        string criteria = "";

        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Year=" + ddlYear.SelectedValue;
            else
                criteria += "Year=" + ddlYear.SelectedValue;
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
                criteria = "CampusId" + ddlCampus.SelectedValue;
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
        if (ddlSubject.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "SubjectId=" + ddlSubject.SelectedValue;
            else
                criteria += " and SubjectId=" + ddlSubject.SelectedValue;
        }
        if (ddlMarksOutOf.SelectedItem.Text != "")
        {
            if (criteria == "")
                criteria = "BaseMarks=" + ddlMarksOutOf.SelectedItem.Text;
            else
                criteria += " and BaseMarks=" + ddlMarksOutOf.SelectedItem.Text;
        }
        if (tbxRoll.Text != "")
        {
            if (criteria == "")
                criteria = "RollNo=" + tbxRoll.Text;
            else
                criteria += " and RollNo=" + tbxRoll.Text;
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "RegNo=" + tbxReg.Text;
            else
                criteria += " and RegNo='" + tbxReg.Text + "'";
        }
        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and ExamTypeId=" + ddlExamType.SelectedValue;
        }

        //criteria += " and IsPublishMarks='False'";
        //criteria += " and ss_Student.IsActive='True'";
        //criteria += " and ss_Student.StatusId= 1";

        return criteria;
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
    protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            if (IsRollNoUse == "False")
            {
                Label LabelRoll = (Label)e.Item.FindControl("LabelRoll");
                LabelRoll.Visible = false;
            }
            else
            {
                Label LabelReg = (Label)e.Item.FindControl("LabelReg");
                LabelReg.Visible = false;
            }
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (IsRollNoUse == "False")
            {

                Label lblRoll = (Label)e.Item.FindControl("lblRollNo");
                lblRoll.Visible = false;
            }
            else
            {
                Label lblReg = (Label)e.Item.FindControl("lblRegNo");
                lblReg.Visible = false;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        BindData();      
    }
    protected void BindData()
    {
        string criteria = GetCriteria();
        DataTable dt = objStudent.GetStudentForMarksEdit(criteria);
     //   DataTable dt = obj.GetStudentMarksInfoByCriteria(criteria);
        if (dt.Rows.Count > 0)
        {
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
            foreach (RepeaterItem item in rptStudent.Items)
            {
                Label lblRollNo = (Label)item.FindControl("lblRollNo");
                Label lblRegNo = (Label)item.FindControl("lblRegNo");
                Label lblNameEng = (Label)item.FindControl("lblNameEng");
                Label lblAttendance = (Label)item.FindControl("lblAttendanceMarks");
                Label lblMonthly = (Label)item.FindControl("lblMonthlyMarks");
                Label lblTheory = (Label)item.FindControl("lblSubjectiveMarks");
                Label lblObjective = (Label)item.FindControl("lblObjectiveMarks");
                Label lblPractical = (Label)item.FindControl("lblPracticalMarks");
                Label lblSBS = (Label)item.FindControl("lblOtherMarks");
                HiddenField hdnStudentId = (HiddenField)item.FindControl("hdnStudentId");
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    if (hdnStudentId.Value == dt.Rows[i]["StudentToClassId"].ToString())
                    {
                        if (dt.Rows[i]["IsFinishMarks"].ToString() == "True")
                        {

                            lblNameEng.Attributes.Add("style", "background-color:#6db8ff;");
                            lblRollNo.Attributes.Add("style", "background-color:#6db8ff;");
                            lblRegNo.Attributes.Add("style", "background-color:#6db8ff;");
                            lblPractical.Attributes.Add("style", "background-color:#6db8ff;");
                            lblObjective.Attributes.Add("style", "background-color:#6db8ff;");
                            lblNoRecordFond.Attributes.Add("style", "background-color:#6db8ff;");
                            lblMonthly.Attributes.Add("style", "background-color:#6db8ff;");
                            lblAttendance.Attributes.Add("style", "background-color:#6db8ff;");
                            //lblSBS.Attributes.Add("style", "background-color:#6db8ff;");
                            lblTheory.Attributes.Add("style", "background-color:#6db8ff;");
                        }
                        if (dt.Rows[i]["IsPublishMarks"].ToString() == "True")
                        {

                            lblNameEng.Attributes.Add("style", "background-color:#4fff80;");
                            lblRollNo.Attributes.Add("style", "background-color:#4fff80;");
                            lblRegNo.Attributes.Add("style", "background-color:#6db8ff;");
                            lblPractical.Attributes.Add("style", "background-color:#4fff80;");
                            lblObjective.Attributes.Add("style", "background-color:#4fff80;");
                            lblNoRecordFond.Attributes.Add("style", "background-color:#4fff80;");
                            lblMonthly.Attributes.Add("style", "background-color:#4fff80;");
                            lblAttendance.Attributes.Add("style", "background-color:#4fff80;");
                            //lblSBS.Attributes.Add("style", "background-color:#4fff80;");
                            lblTheory.Attributes.Add("style", "background-color:#4fff80;");
                        }
                    }
                }
                   
            }
            
            pnlAssignStudent.Visible = true;
            ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
        }
        else
        {
            MessageController.Show("No student found.", MessageType.Warning, Page);
            pnlAssignStudent.Visible = false;
        }
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected DataTable GetMarks()
    {
        DataTable dt = new DataTable("dtMarks");
        dt.Columns.Add("MarksId", typeof(int));
        dt.Columns.Add("StId", typeof(int));
        dt.Columns.Add("SubId", typeof(int));
        dt.Columns.Add("Attendence", typeof(double));
        dt.Columns.Add("Monthly", typeof(double));
        dt.Columns.Add("Theory", typeof(double));
        dt.Columns.Add("Objective", typeof(double));
        dt.Columns.Add("Pracrical", typeof(double));
        //dt.Columns.Add("SBS", typeof(double));
        return dt;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        int count = 0;
        double attendance = 0;
        double monthly = 0;
        double theory = 0;
        double objective = 0;
        double practical = 0;
        double sbs = 0;
        DataTable dt = GetMarks();
        foreach (RepeaterItem item in rptStudent.Items)
        {
            CheckBox chkrow = (CheckBox)item.FindControl("chkrow");
            if (chkrow.Checked)
            {
                count++;
                HiddenField hdnStudentId = (HiddenField)item.FindControl("hdnStudentId");
                HiddenField hdnObtainMarksEntry = (HiddenField)item.FindControl("hdnObtainMarksEntry");

                Label lblAttendance = (Label)item.FindControl("lblAttendanceMarks");
                Label lblMonthly = (Label)item.FindControl("lblMonthlyMarks");
                Label lblTheory = (Label)item.FindControl("lblSubjectiveMarks");
                Label lblObjective = (Label)item.FindControl("lblObjectiveMarks");
                Label lblPractical = (Label)item.FindControl("lblPracticalMarks");
                //Label lblSBS = (Label)item.FindControl("lblOtherMarks");
                
                if (lblAttendance.Text == "") attendance = 0;
                else attendance = Convert.ToDouble(lblAttendance.Text);

                if (lblMonthly.Text == "") monthly = 0;
                else monthly = Convert.ToDouble(lblMonthly.Text);

                if (lblTheory.Text == "") theory = 0;
                else theory = Convert.ToDouble(lblTheory.Text);

                if (lblObjective.Text == "") objective = 0;
                else objective = Convert.ToDouble(lblObjective.Text);

                if (lblPractical.Text == "") practical = 0;
                else practical = Convert.ToDouble(lblPractical.Text);

                //if (lblSBS.Text == "") sbs = 0;
                //else sbs = Convert.ToDouble(lblSBS.Text);


                dt.Rows.Add(Convert.ToInt32(hdnObtainMarksEntry.Value), Convert.ToInt32(hdnStudentId.Value), Convert.ToInt32(ddlSubject.SelectedValue), attendance, monthly, theory, objective, practical);
            }
        }
        if (count > 0)
        {          
            objMarks.UpdateFinishedMarks(dt, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToDouble(ddlMarksOutOf.SelectedItem.Text), Convert.ToInt32(ddlExamType.SelectedValue), Page.User.Identity.Name, false, true);
            BindData();
            MessageController.Show("Finished Marks Successed!!!", MessageType.Information, Page);
        }
        else
        {
            MessageController.Show("Please select at least one checkbox.", MessageType.Warning, Page);
        }

    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        foreach (RepeaterItem item in rptStudent.Items)
        {
            CheckBox chkrow = (CheckBox)item.FindControl("chkrow");
            if (chkrow.Checked)
            {

                HiddenField hdnStudentId = (HiddenField)item.FindControl("hdnStudentId");
                HiddenField hdnObtainMarksEntry = (HiddenField)item.FindControl("hdnObtainMarksEntry");
                objMarks.DeleteMarks(hdnObtainMarksEntry.Value);

            }
        }
        BindData();
    }

    protected void btnPublish_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        int count = 0;
        double attendance = 0;
        double monthly = 0;
        double theory = 0;
        double objective = 0;
        double practical = 0;
        double sbs = 0;

        DataTable dt = GetMarks();
        foreach (RepeaterItem item in rptStudent.Items)
        {
            CheckBox chkrow = (CheckBox)item.FindControl("chkrow");
            if (chkrow.Checked)
            {
                count++;
                HiddenField hdnStudentId = (HiddenField)item.FindControl("hdnStudentId");
                HiddenField hdnObtainMarksEntry = (HiddenField)item.FindControl("hdnObtainMarksEntry");

                Label lblAttendance = (Label)item.FindControl("lblAttendanceMarks");
                Label lblMonthly = (Label)item.FindControl("lblMonthlyMarks");
                Label lblTheory = (Label)item.FindControl("lblSubjectiveMarks");
                Label lblObjective = (Label)item.FindControl("lblObjectiveMarks");
                Label lblPractical = (Label)item.FindControl("lblPracticalMarks");
                //Label lblSBS = (Label)item.FindControl("lblOtherMarks");

                if (lblAttendance.Text == "") attendance = 0;
                else attendance = Convert.ToDouble(lblAttendance.Text);

                if (lblMonthly.Text == "") monthly = 0;
                else monthly = Convert.ToDouble(lblMonthly.Text);

                if (lblTheory.Text == "") theory = 0;
                else theory = Convert.ToDouble(lblTheory.Text);

                if (lblObjective.Text == "") objective = 0;
                else objective = Convert.ToDouble(lblObjective.Text);

                if (lblPractical.Text == "") practical = 0;
                else practical = Convert.ToDouble(lblPractical.Text);

                //if (lblSBS.Text == "") sbs = 0;
                //else sbs = Convert.ToDouble(lblSBS.Text);


                dt.Rows.Add(Convert.ToInt32(hdnObtainMarksEntry.Value), Convert.ToInt32(hdnStudentId.Value), Convert.ToInt32(ddlSubject.SelectedValue), attendance, monthly, theory, objective, practical);
            }
        }
        if (count > 0)
        {
            objMarks.UpdatePublishedViewMarks(dt, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToDouble(ddlMarksOutOf.SelectedItem.Text), Convert.ToInt32(ddlExamType.SelectedValue), Page.User.Identity.Name, true, false);
            BindData();
            MessageController.Show("Publish Marks Successed!!!", MessageType.Information, Page);
        }
        else
        {
            MessageController.Show("Please select at least one checkbox.", MessageType.Warning, Page);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        DataTable dtSubjectWiseMarks = new dalMarks().GetStudentMarks(GetCriteria());
        if (dtSubjectWiseMarks.Rows.Count > 0)
        {
            lblNoRecordFond.Text = string.Empty;
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriterias()) + "&report=subjectWiseMarks&Type=" + ddlExamType.SelectedItem.Text + "&SelectId=" + Convert.ToInt32(ddlClass.SelectedValue));
        }
        else
        {
            lblNoRecordFond.Text = "No record found!";
        }
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected string FormatColorRow(string isFinishMarks)
    {
        bool IsFinishMarks = bool.Parse(isFinishMarks);
        bool IsPublishMarks = bool.Parse(isFinishMarks);
        if (IsFinishMarks == true)
        {
            return "style=’backGround-color:red’";
        }
        else
        {
            return "style=’backGround-color:orange’";
        }

    }
    
}





