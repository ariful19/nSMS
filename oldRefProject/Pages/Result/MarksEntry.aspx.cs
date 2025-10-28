using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Result_MarksEntry : BasePage
{
    dalStudent objStudent = new dalStudent();
    dalSubject objSubject = new dalSubject();
    dalMarks objMarks = new dalMarks();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (!IsPostBack)
        {
            LoadAllDropdownList();
            LoadSubject();
            CheckRollNoUse();
            pnlAssignStudent.Visible = false;
        }
    }
    #region Load Data
    protected void LoadAllDropdownList()
    {
        //ddlYear.DataSource = new dalYear().GetDefault();
        //ddlYear.DataBind();

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
        DataTable dt = objSubject.GetByClassAndGroupId(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue),Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        ddlSubject.DataSource = dt;
        ddlSubject.DataBind();
    }

    protected void LoadStudentSubjectWise()
    {
        DataTable dt = objStudent.GetStudentSubjectWise(Convert.ToInt32(ddlClass.SelectedValue),
            Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue));
        rptStudent.DataSource = dt;
        rptStudent.DataBind();
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
                criteria += "er_StudentToClass.Year=" + ddlYear.SelectedValue;
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
        criteria += " and ss_Student.IsActive='True'";
        criteria += " and ss_Student.StatusId= 1";
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

                Label lblRoll = (Label)e.Item.FindControl("lblRoll");
                lblRoll.Visible = false;
            }
            else
            {
                Label lblReg = (Label)e.Item.FindControl("lblReg");
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
        if (ddlSubject.SelectedItem ==null)
        {
            MessageController.Show("Select Subject.", MessageType.Warning, Page);
            return;
        }
        DataTable dt = objStudent.GetStudentForMarksEntry(criteria, Convert.ToInt32(ddlSubject.SelectedValue));

        int roll = 0;
        List<int> lstRoll = new List<int>();

        foreach (DataRow row in dt.Rows)
        {
            int.TryParse(row["StudentToClassID"].ToString(), out roll);
            lstRoll.Add(roll);
        }

        string rolls = String.Join(",", lstRoll.ToArray());
        if (rolls.Length > 0)
        {
            DataTable dtExisting = objStudent.GetExistingStudentInMarksEntry(ddlYear.SelectedValue, int.Parse(ddlSubject.SelectedValue), rolls, int.Parse(ddlExamType.SelectedValue));
            int count = dt.Rows.Count;
            int marksID = 0;
            foreach (DataRow row in dtExisting.Rows)
            {
                int.TryParse(row["StudentToClassId"].ToString(), out marksID);

                for (int i = 0; i < count; i++)
                {
                    if (marksID == Convert.ToInt32(dt.Rows[i]["StudentToClassId"]))
                    {
                        dt.Rows[i].Delete();
                        dt.AcceptChanges();
                        break;
                    }
                }
                count = dt.Rows.Count;
            }
        }
        if (dt.Rows.Count > 0)
        {
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
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
    protected DataTable GetMarks()
    {
        DataTable dt = new DataTable("dtMarks");
        dt.Columns.Add("StId", typeof(int));
        dt.Columns.Add("SubId", typeof(int));
        dt.Columns.Add("Attendance", typeof(double));
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
        double monthlty = 0;
        double theory = 0;
        double objective = 0;
        double practical = 0;
        double sbs = 0;
        double outOfMarks = Convert.ToDouble(ddlMarksOutOf.SelectedItem.Text);
        Session["outOfMarks"] = outOfMarks;
        DataTable dt = GetMarks();
        foreach (RepeaterItem item in rptStudent.Items)
        {
            CheckBox chkrow = (CheckBox)item.FindControl("chkrow");
            if (chkrow.Checked)
            {
                double sumOfMarks = 0;
                count++;
                HiddenField hdnStudentId = (HiddenField)item.FindControl("hdnStudentId");
                TextBox tbxAttendance = (TextBox)item.FindControl("attendanceTextBox");
                TextBox tbxMonthly = (TextBox)item.FindControl("mothlyTextBox");
                TextBox tbxTheory = (TextBox)item.FindControl("tbxTheory");
                TextBox tbxObjective = (TextBox)item.FindControl("tbxObjective");
                TextBox tbxPractical = (TextBox)item.FindControl("tbxPractical");
                TextBox tbxSBS = (TextBox)item.FindControl("tbxSBS");

                DataTable dtExisting = objStudent.GetExistingStudentInMarksEntry(ddlYear.SelectedValue, int.Parse(ddlSubject.SelectedValue), hdnStudentId.Value, int.Parse(ddlExamType.SelectedValue));
                if (dtExisting.Rows.Count == 0)
                {

                    if (tbxAttendance.Text == "") attendance = 0;
                    else attendance = Convert.ToDouble(tbxAttendance.Text);

                    if (tbxMonthly.Text == "") monthlty = 0;
                    else monthlty = Convert.ToDouble(tbxMonthly.Text);

                    if (tbxTheory.Text == "") theory = 0;
                    else theory = Convert.ToDouble(tbxTheory.Text);

                    if (tbxObjective.Text == "") objective = 0;
                    else objective = Convert.ToDouble(tbxObjective.Text);

                    if (tbxPractical.Text == "") practical = 0;
                    else practical = Convert.ToDouble(tbxPractical.Text);

                    //if (tbxSBS.Text == "") sbs = 0;
                    //else sbs = Convert.ToDouble(tbxSBS.Text);

                    sumOfMarks = attendance + monthlty + theory + objective + practical + sbs;

                    if (sumOfMarks <= outOfMarks && sumOfMarks >= 0)
                    {
                        dt.Rows.Add(Convert.ToInt32(hdnStudentId.Value), Convert.ToInt32(ddlSubject.SelectedValue), attendance, monthlty, theory, objective, practical);
                    }
                    else
                    {
                        MessageController.Show("Marks must be less then" + ' ' + +outOfMarks + ".", MessageType.Warning, Page);
                        break;
                    }
                }
                //else
                //{
                //    MessageController.Show("Already entry this craiteria..", MessageType.Warning, Page);
                //    return;
                //}             
            }
        }
        if (dt.Rows.Count > 0)
        {
            if (count > 0)
            {
                objMarks.InsertMarks(dt, Convert.ToInt32(ddlYear.SelectedValue), outOfMarks, Convert.ToInt32(ddlExamType.SelectedValue), Page.User.Identity.Name, false, false);
                BindData();
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            }
            else
            {
                MessageController.Show("Please select at least one checkbox.", MessageType.Warning, Page);
            }
        }
        else
        {
            MessageController.Show("Marks must be less then" +' '+ + outOfMarks +".", MessageType.Warning, Page);
        }
        //BindData();
    }

    protected void btnBlankReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        DataTable dt = objStudent.GetStudentForMarksEntry(GetCriteria(), Convert.ToInt32(ddlSubject.SelectedValue));
        if (dt.Rows.Count > 0)
        {
          
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=blankSubjectWiseMarks&Type=" + ddlExamType.SelectedItem.Text+ "&SelectId="+ ddlSubject.SelectedValue.ToString());
        }
        else
        {
            MessageController.Show("No student found.", MessageType.Warning, Page);
            pnlAssignStudent.Visible = false;
        }
      
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
}

