using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nano.DataAccessLayer;

public partial class Pages_Student_Result : System.Web.UI.Page
{
    dalReport obj = new dalReport();
    public DataSet dsGrading = new Grading();
    public static List<Tuple<string, string, string>> resultInfo;
    public static List<Tuple<string, string>> GPA;
    List<string> regNo = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlResult.Visible = false;            
            Load();
            if (Common.SessionInfo.StudentId > 0)
            {
                yearDiv.Visible = true;
                //currentYearDiv.Visible = true;
                tbxRollNo.Visible = false;
                LabelRoll.Visible = false;
                //LabelReg.Visible = false;
                //tbxReg.Visible = false;
                LabelReg.Visible = true;
                tbxReg.Visible = true;
                ddlClass.SelectedValue = Common.SessionInfo.ClassId.ToString();
                //yearTextBox.Text = DateTime.Now.Year.ToString();
                //ddlYear.Enabled = false;
                //ddlClass.Enabled = false;
                //ddlGroup.Enabled = false;
                //ddlShift.Enabled = false;
                //ddlSection.Enabled = false;
                ddlYear.Enabled = true;
                ddlClass.Enabled = false;
                ddlGroup.Enabled = true;
                ddlShift.Enabled = true;
                ddlSection.Enabled = true;
            }
            else
            {
                yearDiv.Visible = true;
                //currentYearDiv.Visible = false;
                ddlYear.Enabled = true;
                ddlClass.Enabled = true;
                ddlGroup.Enabled = true;
                ddlShift.Enabled = true;
                ddlSection.Enabled = true;
                tbxRollNo.Visible = true;
                LabelRoll.Visible = true;
                LabelReg.Visible = true;
                tbxReg.Visible = true;
            }
            CheckRollNoUse();
        }
    }
    Regex regex = new Regex(@"[1-9]");
    private bool IsInteger(string str)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(str))
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
            MessageController.Show("Roll No Must be Neumeric!!!", MessageType.Warning, Page);
        }

        return false;
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

        ddlExamType.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamType.DataBind();

    }
    #endregion
    protected string GetFilteredCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.YearId=" + ddlYear.SelectedValue;
            else
                criteria += "rs_ObtainMarks.YearId=" + ddlYear.SelectedValue;
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
                criteria = "er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
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
        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
        }
        if (tbxRollNo.Text != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.RollNo=" + tbxRollNo.Text;
            else
                criteria += " and er_StudentToClass.RollNo=" + tbxRollNo.Text;
        }
        else
        {
            DataTable dt = new dalStudent().GetStudentToClassByStudentID(Common.SessionInfo.StudentId, Convert.ToInt32(ddlClass.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.RollNo=" + dt.Rows[0]["RollNo"].ToString();
                else
                    criteria += " and er_StudentToClass.RollNo=" + dt.Rows[0]["RollNo"].ToString();
            }
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
        }
        criteria += " and (IsPublishMarks='True' or IsFinishMarks='True')";
        return criteria;
    }
    protected void Clear()
    {
        lblErrorMessage.Text = "";
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
            tbxRollNo.Visible = false;
        }
        else
        {
            LabelReg.Visible = false;
            tbxReg.Visible = false;
        }

    }

    private bool CheckValidation()
    {
        bool result = true;
        if (tbxRollNo.Visible)
        {
            if (tbxRollNo.Text == "")
            {
                tbxRollNo.BorderColor = System.Drawing.Color.Red;
                MessageController.Show(" Enter Roll No First....", MessageType.Warning, Page);
                result = false;
            }
        }
        else
        {
            if (tbxReg.Text == "")
            {
                tbxReg.BorderColor = System.Drawing.Color.Red;
                MessageController.Show(" Enter Reg. No First....", MessageType.Warning, Page);
                result = false;
            }
        }
        return result;
    }     
    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        StudentResult();
       
    }

    protected void StudentResult()
    {
        if (CheckValidation())
        {
            DataTable dtSubjectWiseMarks = new dalMarks().GetStudentMarks(GetFilteredCriteria());

            dtSubjectWiseMarks.Columns.Add("GradeLetter");
            dtSubjectWiseMarks.Columns.Add("GradePoint");
            dtSubjectWiseMarks.Columns.Add("CGPA");
            decimal totalMarks = 0;
            decimal baseMarks = 0;
            decimal totalMarksPercent = 0;
            foreach (DataRow row in dtSubjectWiseMarks.Rows)
            {
                //totalMarks = totalMarks + Convert.ToDecimal(row["ConvertedMarks"]);
                baseMarks = Convert.ToDecimal(row["BaseMarks"]);
                totalMarks = totalMarks + Convert.ToDecimal(row["ConvertedMarks"]);
                totalMarksPercent += Convert.ToDecimal(row["ConvertedMarks"]) * 100 / Convert.ToDecimal(row["BaseMarks"]);
                GradeCalculation(row);
            }
            double totalpoint = 0;
            decimal gpa = 0;
            bool flag = false;
            int count = 0;
            int failSubject = 0;
            string criteria = GetCriteria();
            //DataTable dt = CalculateResult(criteria);
            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
               // decimal obtainMarks = totalMarks / dtSubjectWiseMarks.Rows.Count;
                decimal obtainMarks = totalMarksPercent / dtSubjectWiseMarks.Rows.Count;
                string gradePoint = string.Empty;
                string CGPA = GetCGPA(obtainMarks, out gradePoint);
                int rowIndex = 0;
                rowIndex = (Int32)(dtSubjectWiseMarks.Rows.Count / 2);

                dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = CGPA;
            }
            else
            {
                lblErrorMessage.Text = "No result found.";
                pnlResult.Visible = false;
                return;

            }

            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                pnlResult.Visible = true;
                foreach (DataRow dr in dtSubjectWiseMarks.Rows)
                {
                    count++;
                    if (dr["GradePoint"].ToString() == "0.00")
                    {
                        flag = true;
                        failSubject++;
                    }
                    else
                    {
                        double gradePoint = 0;
                        double.TryParse(dr["GradePoint"].ToString(), out gradePoint);

                        totalpoint += gradePoint;
                    }
                }
                gpa = Math.Round(Convert.ToDecimal(totalpoint / count), 2);
                lblName.Text = dtSubjectWiseMarks.Rows[0]["NameEng"].ToString();
                lblFather.Text = dtSubjectWiseMarks.Rows[0]["FatherNameEng"].ToString();
                lblMother.Text = dtSubjectWiseMarks.Rows[0]["MotherNameEng"].ToString();
                lblClass.Text = ddlClass.SelectedItem.Text;
                lblGroup.Text = ddlGroup.SelectedItem.Text;
                lblShift.Text = ddlShift.SelectedItem.Text;
                lblSection.Text = ddlSection.SelectedItem.Text;
                lblRegNo.Text = dtSubjectWiseMarks.Rows[0]["RegNo"].ToString();
                if (tbxRollNo.Visible)
                    lblRoll.Text = tbxRollNo.Text;
                else
                    LabelRollNo.Visible = false;
                if (flag)
                {
                    lblGPA.Text = "0.00";
                    lblFail.Text = "Fail in " + failSubject + "  subject(s)"; 
                    lblGPA.ForeColor = System.Drawing.Color.Red;
                    lblFail.ForeColor = System.Drawing.Color.Red;
                    lblFailSubject.Visible = true;
                    lblFail.Visible = true;
                }
                else
                {
                    lblGPA.Text = gpa.ToString("0.00");
                    lblGPA.ForeColor = System.Drawing.Color.Green;
                    lblFailSubject.Visible = false;
                    lblFail.Visible = false;
                }

                lblExamType.Text = ddlExamType.SelectedItem.Text;
                rptResult.DataSource = dtSubjectWiseMarks;
                rptResult.DataBind();
                Clear();
            }
            else
            {
                lblErrorMessage.Text = "No result Published Yet...!!!!";
                pnlResult.Visible = false;

            }
        }
    }
    private string GetCGPA(decimal totalMarks, out string gradePoint)
    {
        string gradeName = string.Empty;
        gradePoint = string.Empty;
        DataTable dtGrade = new Common().GetAll("rs_Grade");
        foreach (DataRow drg in dtGrade.Rows)
        {

            if ((totalMarks >= Convert.ToDecimal(drg["StartMarks"])) && (totalMarks < Convert.ToDecimal(drg["EndMarks"]) + 1))
            {
                gradeName = drg["GradeName"].ToString();
                gradePoint = drg["GradePoint"].ToString();
            }
        }

        return gradeName;
    }
    public DataTable CalculateResult(string criteria)
    {
        resultInfo = new List<Tuple<string, string, string>>();
        DataTable dtMarks = null;
        if (!string.IsNullOrEmpty(criteria))
        {
            dtMarks = new dalMarks().GetStudentMarks(criteria);
        }
        DataTable dtTempMarks = dtMarks.Copy();
        foreach (DataColumn col in dtTempMarks.Columns)
        {
            col.ReadOnly = false;
        }
        foreach (DataRow drr in dtMarks.Rows)
        {
            decimal previousGrade = -1;
            Boolean isFail = true;
            DataRow[] drExists = dtTempMarks.Select("RegNo='" + drr["RegNo"].ToString() + "' and SubjectId=" + drr["SubjectId"]);
            if (drExists.Count() <= 0)
            {
                continue;
            }
            DataRow dr = drExists[0];
            dr.BeginEdit();
            if (IsPass(dr))
            {
                if (!resultInfo.Any(t => t.Item1 == drr["RegNo"].ToString() && t.Item2 == drr["SubId"].ToString()))
                {
                    resultInfo.Add(new Tuple<string, string, string>(drr["RegNo"].ToString(), drr["SubId"].ToString(), drr["PaperNo"].ToString()));
                    if (drr["CategoryId"].ToString() == "1" || drr["CategoryId"].ToString() == "4")
                    {
                        dr["ConvertedMarks"] = Convert.ToDecimal(dr["ConvertedMarks"].ToString());
                        dr["SubjectiveMarks"] = Convert.ToDecimal(dr["SubjectiveMarks"].ToString());
                        dr["ObjectiveMarks"] = Convert.ToDecimal(dr["ObjectiveMarks"].ToString());
                        dr["PracticalMarks"] = Convert.ToDecimal(dr["PracticalMarks"].ToString());
                        dr["OtherMarks"] = Convert.ToDecimal(dr["OtherMarks"].ToString());
                        GradeCalculation(dr);
                    }
                }
                else
                {
                    if (drr["CategoryId"].ToString() == "1" || drr["CategoryId"].ToString() == "4")
                    {
                        continue;
                    }
                    var previousResult = SelectionPrevious(dr["RegNo"].ToString(), dr["SubId"].ToString());
                    foreach (var r in previousResult)
                    {
                        DataRow[] drPreviousMarks = dtTempMarks.Select("RegNo='" + r.Item1 + "' and SubId='" + r.Item2 + "' and PaperNo='" + r.Item3 + "' ");
                        if (drPreviousMarks.Count() > 0)
                        {
                            previousGrade = Convert.ToDecimal(drPreviousMarks[0]["GradePoint"].ToString());
                            dr["ConvertedMarks"] = ((Convert.ToDecimal(dr["ConvertedMarks"].ToString()) + Convert.ToDecimal(drPreviousMarks[0]["ConvertedMarks"].ToString())) / 2).ToString();
                            dr["SubjectiveMarks"] = ((Convert.ToDecimal(dr["SubjectiveMarks"].ToString()) + Convert.ToDecimal(drPreviousMarks[0]["SubjectiveMarks"].ToString())) / 2).ToString();
                            dr["ObjectiveMarks"] = ((Convert.ToDecimal(dr["ObjectiveMarks"].ToString()) + Convert.ToDecimal(drPreviousMarks[0]["ObjectiveMarks"].ToString())) / 2).ToString();
                            dr["PracticalMarks"] = (Convert.ToDecimal(dr["PracticalMarks"].ToString()) + Convert.ToDecimal(drPreviousMarks[0]["PracticalMarks"].ToString())).ToString();
                            dr["OtherMarks"] = (Convert.ToDecimal(dr["OtherMarks"].ToString()) + Convert.ToDecimal(drPreviousMarks[0]["OtherMarks"].ToString())).ToString();
                            GradeCalculation(dr);
                            foreach (DataRow drprevious in drPreviousMarks)
                            {
                                if (drprevious["Id"].ToString() != dr["Id"].ToString())
                                {
                                    dtTempMarks.Rows.Remove(drprevious);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (!resultInfo.Any(t => t.Item1 == drr["RegNo"].ToString() && t.Item2 == drr["SubId"].ToString()))
                {
                    resultInfo.Add(new Tuple<string, string, string>(drr["RegNo"].ToString(), drr["SubId"].ToString(), drr["PaperNo"].ToString()));
                    dr["GradeLetter"] = "F";
                    dr["GradePoint"] = "0";
                }
                else
                {
                    if (drr["CategoryId"].ToString() == "1" || drr["CategoryId"].ToString() == "4")
                    {
                        continue;
                    }
                    var previousResult = SelectionPrevious(dr["RegNo"].ToString(), dr["SubId"].ToString());
                    foreach (var r in previousResult)
                    {
                        DataRow[] drPreviousMarks = dtTempMarks.Select("RegNo='" + r.Item1 + "' and SubId='" + r.Item2 + "' and PaperNo='" + r.Item3 + "' ");
                        if (drPreviousMarks.Count() > 0)
                        {
                            dr["GradeLetter"] = "F";
                            dr["GradePoint"] = "0";
                            foreach (DataRow drprevious in drPreviousMarks)
                            {
                                if (drprevious["Id"].ToString() != dr["Id"].ToString())
                                {
                                    dtTempMarks.Rows.Remove(drprevious);
                                }
                            }
                        }
                    }
                }
            }
            dr.EndEdit();
        }
        return dtTempMarks;
    }

    private static IEnumerable<Tuple<string, string, string>> SelectionPrevious(string s1, string s2)
    {
        var ret = from t in resultInfo
                  where t.Item1 == s1 && t.Item2 == s2
                  select new Tuple<string, string, string>(t.Item1, t.Item2, t.Item3);
        return ret;
    }
    public Boolean IsPass(DataRow dr)
    {
        double subjective = Convert.ToDouble(dr["SubjectiveMarks"].ToString());
        double objective = Convert.ToDouble(dr["ObjectiveMarks"].ToString());

        DataTable dtFailMarks = new dalFailSystem().GetBySubjectId(Convert.ToInt32(dr["SubjectId"].ToString()));
        if (dtFailMarks.Rows.Count > 0)
        {
            if ((subjective >= Convert.ToDouble(dtFailMarks.Rows[0]["SubjectiveFailMarks"])) && (objective >= Convert.ToDouble(dtFailMarks.Rows[0]["ObjectiveFailMarks"])))
                return true;

        }
        return false;
    }
    public void GradeCalculation(DataRow dr)
    {
        DataTable dtGrade = new Common().GetAll("rs_Grade");
        foreach (DataRow drg in dtGrade.Rows)
        {
            double baseMarks = Convert.ToDouble(dr["BaseMarks"]);            
            double totalMarks = (Convert.ToDouble(dr["ConvertedMarks"]) / baseMarks) * 100;
            if ((totalMarks >= Convert.ToDouble(drg["StartMarks"])) && (totalMarks < Convert.ToDouble(drg["EndMarks"]) + 1))
            {
                dr["GradeLetter"] = drg["GradeName"].ToString();
                dr["GradePoint"] = drg["GradePoint"].ToString();
            }
        }
    }

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.YearId=" + ddlYear.SelectedItem.Text;
            else
                criteria += "rs_ObtainMarks.YearId=" + ddlYear.SelectedItem.Text;
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
        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
        }
        if (tbxRollNo.Text != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.RollNo=" + tbxRollNo.Text;
            else
                criteria += " and er_StudentToClass.RollNo=" + tbxRollNo.Text;
        }
        else
        {
            DataTable dt = new dalStudent().GetStudentToClassByStudentID(Common.SessionInfo.StudentId, Convert.ToInt32(ddlClass.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.RollNo=" + dt.Rows[0]["RollNo"].ToString();
                else
                    criteria += " and er_StudentToClass.RollNo=" + dt.Rows[0]["RollNo"].ToString();
            }
        }

        return criteria;
    }
    #endregion
}