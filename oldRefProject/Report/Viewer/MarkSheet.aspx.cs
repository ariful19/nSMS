using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_MarkSheet : BasePage
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

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        ddlExamType.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamType.DataBind();


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

    string Position
    {
        set { ViewState["Position"] = value; }
        get
        {
            try
            {
                return ViewState["Position"].ToString();
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
  
    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (CheckValidation())
       {
           DataTable dtSubjectWiseMarks = new DataTable();

           if (Convert.ToInt32(ddlClass.SelectedValue) < 12)
           {
               dtSubjectWiseMarks = new dalMarks().GetStudentMarks(GetCriteria());
           }
           else
               dtSubjectWiseMarks = new dalMarks().GetStudentMarksforHigherClass(GetCriteria());               
           
            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                lblNoRecordFond.Text = string.Empty;
                GetMeritPosition(dtSubjectWiseMarks);
                if (Convert.ToInt32(ddlClass.SelectedValue) >= 12)
                {
                    if (ddlClass.SelectedValue == "19" && ddlExamType.SelectedValue=="12")
                    {
                        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=CollegeTestExam&position=" + Position + "&SelectId=" + ddlClass.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&Type=" + ddlExamType.SelectedValue + "&GroupId=" + ddlGroup.SelectedValue);
                    }
                    
  else if (Convert.ToInt32(ddlExamType.SelectedValue) >= 13 && Convert.ToInt32(ddlExamType.SelectedValue)<=16)
                    {
                        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=markSheet&position=" + Position + "&SelectId=" + ddlClass.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&Type=" + ddlExamType.SelectedValue + "&GroupId=" + ddlGroup.SelectedValue + "&MediumId=" + ddlMedium.SelectedValue);
                    }
                    else 
                    {
                        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=TestExamMarksheet&position=" + Position + "&SelectId=" + ddlClass.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&Type=" + ddlExamType.SelectedValue + "&GroupId=" + ddlGroup.SelectedValue + "&MediumId=" + ddlMedium.SelectedValue);
                    }

                }
                else
                {
                    Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=markSheet&position=" + Position + "&SelectId=" + ddlClass.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&Type=" + ddlExamType.SelectedValue + "&GroupId=" + ddlGroup.SelectedValue);
                }
                //Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=mark");
            }
            else
            {
                lblNoRecordFond.Text = "No record found!";
            }
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

    #region Get Criteria
    protected string GetCriteria()
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
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
        }
        criteria += " and ss_Student.StatusId= 1";
        return criteria;
    }

    #endregion
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


    private void GetMeritPosition(DataTable dtSubjectWiseMarks)
    {
       
        int countFail = 0;

        dtSubjectWiseMarks.Columns.Add("Remarks");
        int classId = Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["ClassId"].ToString());
        if (classId >= 12)
        {
            foreach (DataRow row in dtSubjectWiseMarks.Rows)
            {
                string remarks = "";
                remarks = row["Remarks"].ToString();
                int mediumId = Convert.ToInt32(row["MediumId"]);
                classId = Convert.ToInt32(row["ClassId"]);
                int groupId = Convert.ToInt32(row["GroupId"]);
                string subjectName = row["SubjectName"].ToString();
                int subjectToClassId = Convert.ToInt32(row["SubjectId"].ToString());
                double subjectiveMarks = Convert.ToDouble(row["SubjectiveMarks"]);
                double objectiveMarks = Convert.ToDouble(row["ObjectiveMarks"]);

                DataTable dtFailMarks = new dalFailSystem().GetByFailBySubjectAndClassId(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlCampus.SelectedValue),Convert.ToInt32(ddlExamType.SelectedValue),subjectToClassId, classId, mediumId, groupId);
                        
                    if ((subjectiveMarks >= Convert.ToDouble(dtFailMarks.Rows[0]["SubjectiveFailMarks"])) &&
                    (objectiveMarks >= Convert.ToDouble(dtFailMarks.Rows[0]["ObjectiveFailMarks"])))
                       {
                          remarks = "Pass";
                       }
                   else
                       {
                           remarks = "Fail";
                           
                           if (Convert.ToInt32(row["ClassId"]) >=12)
                           {
                               if (Convert.ToBoolean(row["IsOptional"]) == false)
                               {
                                   countFail++;
                               }                               
                           }
                           else
                           {
                               countFail++;
                           }
                           
                        }

            }
          
        }
      
        if (countFail > 0)
        {
            Position = "Fail";
        }
        else
        {
            DataTable dt = new DataTable();
            if (classId >= 12)
            {
                 dt = new dalMarks().GetMeritPositionByCriteriaForHigher(GetCriteriaForMeritPosision());
            }
            else
                 dt = new dalMarks().GetMeritPositionByCriteria(GetCriteriaForMeritPosision());
            
       
           double totalmarks = 0;
           int studentToClassId = 0;
           foreach (DataRow row in dtSubjectWiseMarks.Rows)
           {
               totalmarks += (Convert.ToDouble(row["SubjectiveMarks"]) + Convert.ToDouble(row["ObjectiveMarks"]) +
                              Convert.ToDouble(row["PracticalMarks"]) + Convert.ToDouble(row["AttendanceMarks"]) +
                              Convert.ToDouble(row["MonthlyMarks"]));
               //totalmarks += (Convert.ToDouble(row["SubjectiveMarks"]) + Convert.ToDouble(row["ObjectiveMarks"]) + Convert.ToDouble(row["PracticalMarks"]) + Convert.ToDouble(row["AttendanceMarks"]) + Convert.ToDouble(row["MonthlyMarks"])) * 100 / Convert.ToDouble(row["BaseMarks"]);
               studentToClassId = Convert.ToInt32(row["StudentToClassId"]);
           }
           foreach (DataRow dRow in dt.Rows)
           {

               if (studentToClassId == Convert.ToInt32(dRow["StudentToClassId"]))
               {
                   Position = dRow["Position"].ToString();
                   if (Position == "1")
                       Position += "st";
                   else if (Position == "2")
                       Position += "nd";
                   else if (Position == "3")
                       Position += "rd";
                   else
                       Position += "th";
               }
           }
        }       
    }


    //private void GetMeritPostionForTest(DataTable dtSubjectWiseMarks)
    //{
    //    TestExamCollege test = new TestExamCollege();

    //    var deptfirst4 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1693 : 0;
    //    var deptsecond4 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1694 : 0;

    //    var deptfirst5 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1696 : 0;
    //    var deptsecond5 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1697 : 0;

    //    var deptfirst6 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1699 : 0;
    //    var deptsecond6 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1700 : 0;

    //    var deptfirst7 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1708 : 0;
    //    var deptsecond7 = (Convert.ToInt32(dtSubjectWiseMarks.Rows[0]["GroupId"]) == 2) ? 1709 : 0;

    //    var sub1 = (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1686 && o.Field<int>("SubjectID") <= 1687).Sum(o => o.Field<double>("SubjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1686 && o.Field<int>("SubjectID") <= 1687).Sum(o => o.Field<double>("SubjectiveFailMarks"))) || 
    //     (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1686 && o.Field<int>("SubjectID") <= 1687).Sum(o => o.Field<double>("ObjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1686 && o.Field<int>("SubjectID") <= 1687).Sum(o => o.Field<double>("ObjectiveFailMarks")));

    //    var sub2 = (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1689 && o.Field<int>("SubjectID") <= 1690).Sum(o => o.Field<double>("SubjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1689 && o.Field<int>("SubjectID") <= 1690).Sum(o => o.Field<double>("SubjectiveFailMarks"))) ||
    //     (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1689 && o.Field<int>("SubjectID") <= 1690).Sum(o => o.Field<double>("ObjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= 1689 && o.Field<int>("SubjectID") <= 1690).Sum(o => o.Field<double>("ObjectiveFailMarks")));

    //    var sub3 = (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") == 1692).Sum(o => o.Field<double>("SubjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") == 1692).Sum(o => o.Field<double>("SubjectiveFailMarks"))) ||
    //    (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") == 1692).Sum(o => o.Field<double>("ObjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") == 1692).Sum(o => o.Field<double>("ObjectiveFailMarks")));

    //    var sub4 = (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst4 && o.Field<int>("SubjectID") <= deptsecond4).Sum(o => o.Field<double>("SubjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst4 && o.Field<int>("SubjectID") <= deptsecond4).Sum(o => o.Field<double>("SubjectiveFailMarks"))) ||
    //     (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst4 && o.Field<int>("SubjectID") <= deptsecond4).Sum(o => o.Field<double>("ObjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst4 && o.Field<int>("SubjectID") <= deptsecond4).Sum(o => o.Field<double>("ObjectiveFailMarks")));

    //    var sub5 = (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst5 && o.Field<int>("SubjectID") <= deptsecond5).Sum(o => o.Field<double>("SubjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst5 && o.Field<int>("SubjectID") <= deptsecond5).Sum(o => o.Field<double>("SubjectiveFailMarks"))) ||
    //     (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst5 && o.Field<int>("SubjectID") <= deptsecond5).Sum(o => o.Field<double>("ObjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst5 && o.Field<int>("SubjectID") <= deptsecond5).Sum(o => o.Field<double>("ObjectiveFailMarks")));

    //    var sub6 = (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst6 && o.Field<int>("SubjectID") <= deptsecond6).Sum(o => o.Field<double>("SubjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst6 && o.Field<int>("SubjectID") <= deptsecond6).Sum(o => o.Field<double>("SubjectiveFailMarks"))) ||
    //     (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst6 && o.Field<int>("SubjectID") <= deptsecond5).Sum(o => o.Field<double>("ObjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst6 && o.Field<int>("SubjectID") <= deptsecond6).Sum(o => o.Field<double>("ObjectiveFailMarks")));

    //    var sub7 = (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst7 && o.Field<int>("SubjectID") <= deptsecond7).Sum(o => o.Field<double>("SubjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst7 && o.Field<int>("SubjectID") <= deptsecond7).Sum(o => o.Field<double>("SubjectiveFailMarks"))) ||
    //     (dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst7 && o.Field<int>("SubjectID") <= deptsecond7).Sum(o => o.Field<double>("ObjectiveMarks")) < dtSubjectWiseMarks.AsEnumerable().Where(o => o.Field<int>("SubjectID") >= deptfirst7 && o.Field<int>("SubjectID") <= deptsecond7).Sum(o => o.Field<double>("ObjectiveFailMarks")));
    //}

    protected string GetCriteriaForMeritPosision()
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
        if(ddlGroup.SelectedValue !="")
        {
            if(criteria=="")
                criteria = "er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
        }


        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and rs_ObtainMarks.ExamTypeId=" + ddlExamType.SelectedValue;
        }
        //if (int.Parse(ddlClass.SelectedValue) < 12)
        //{
        //    var gender = Common.GetGenderByRegNo(tbxReg.Text);

        //    if (criteria == "")
        //        criteria = "st_Person.GenderId=" + gender;
        //    else
        //        criteria += " and st_Person.GenderId=" + gender;
        //}
        return criteria;
    }

   
}











