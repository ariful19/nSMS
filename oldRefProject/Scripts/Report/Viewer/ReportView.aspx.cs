using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Designer_ReportView : System.Web.UI.Page
{
    dalReport obj = new dalReport();
    ReportDocument rptDocument = new ReportDocument();
    public DataSet dsGrading = new Grading();
    public static List<Tuple<string, string, string>> resultInfo;
    public static List<Tuple<string, string>> GPA;
    List<string> regNo = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReport();
        }
        else
        {

            LoadReport();
        }
        CRV.DisplayGroupTree = false;
        CRV.HasToggleGroupTreeButton = false;
    }



    private class StudentIdCard
    {
        public string NameEng { get; set; }
        public string BloodGroup { get; set; }
        public string ClassName { get; set; }
        public string GroupName { get; set; }
        public string Shift { get; set; }
        public string Section { get; set; }

        public string RegNo { get; set; }
        public string RollNo { get; set; }
        public string Year { get; set; }
        public string Mobile { get; set; }
        public byte[] PersonImage { get; set; }
        public string SchoolName { get; set; }
    }



    protected void LoadReport()
    {
        string criteria = "";
        if (Request.QueryString["query"] != null)
            criteria = EncryptionDecryption.EncryptDecryptString(Request.QueryString["query"]);
        string report = Request.QueryString["report"];

        if (PreviousPage != null)
        {
            if (PreviousPage.IsCrossPagePostBack == true)
            {

            }
        }
        else
        {

        }

        string user = System.Configuration.ConfigurationManager.AppSettings["User"];
        string password = System.Configuration.ConfigurationManager.AppSettings["password"];
        string server = System.Configuration.ConfigurationManager.AppSettings["Server"];
        string dbName = System.Configuration.ConfigurationManager.AppSettings["DBName"];

        DataTable dtSchool = new Common().GetAll("bs_SchoolInformation");

        if (report == "IdCard")
        {
            List<StudentIdCard> lstIdCard = new List<StudentIdCard>();

            DataSet reportDataSet = new DataSet();

            DataTable dt = obj.GetStudentforIdCard(criteria);
            dt.Columns.Add("PImage");
            foreach (DataRow row in dt.Rows)
            {
                string imagePath = Server.MapPath("~/Images/Person/" + row["PersonImage"].ToString());
                StudentIdCard sIdCard = new StudentIdCard();
                if (System.IO.File.Exists(imagePath))
                {
                    byte[] imageByte = string.IsNullOrEmpty(row["PersonImage"].ToString()) ? null : System.IO.File.ReadAllBytes(imagePath);
                    sIdCard.PersonImage = imageByte;
                }


                sIdCard.NameEng = row["NameEng"].ToString();
                sIdCard.BloodGroup = row["BloodGroup"].ToString();
                sIdCard.ClassName = row["ClassName"].ToString();
                sIdCard.GroupName = row["GroupName"].ToString();
                sIdCard.Shift = row["Shift"].ToString();
                sIdCard.Section = row["Section"].ToString();
                sIdCard.RegNo = row["RegNo"].ToString();
                sIdCard.RollNo = row["RollNo"].ToString();
                sIdCard.Year = row["Year"].ToString();
                sIdCard.Mobile = row["Mobile"].ToString();
                sIdCard.Section = row["NameEng"].ToString();

                sIdCard.SchoolName = dtSchool.Rows[0]["Name"].ToString();
                lstIdCard.Add(sIdCard);
                //dt.Rows[0]["PImage"] = imageByte;
                //dt.Rows[0]["PersonImage"] = imageByte; //Server.MapPath("Images/Person/" + row["PersonImage"].ToString());
            }


            dt.TableName = "Student";

            dtSchool.TableName = "School";
            reportDataSet.Tables.Add(dt);
            reportDataSet.Tables.Add(dtSchool);

            rptDocument.Load(Server.MapPath("../../Report/Designer/IdCard.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(lstIdCard);
            CRV.ReportSource = rptDocument;
        }

        if (report == "stAttendence")
        {
            DataTable dt = obj.GetStudentAttendence(criteria);
            rptDocument.Load(Server.MapPath("../../Report/Designer/rptStudentAttendence.rpt").ToString());
            rptDocument.SetDataSource(dt);
            CRV.ReportSource = rptDocument;
        }
        if (report == "teacher")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            DataTable dtTeacher = new dalTeacher().GetAll();
            dtTeacher.TableName = "Teacher";
            reportDataSet.Tables.Add(dtTeacher);
            reportDataSet.Tables.Add(dtSchool);
            rptDocument.Load(Server.MapPath("../../Report/Designer/rptTeacherList.rpt").ToString());
            rptDocument.SetParameterValue("SchoolName", dtSchool.Rows[0]["Name"].ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            rptDocument.SetDataSource(reportDataSet);
            CRV.ReportSource = rptDocument;
        }
        if (report == "subjectToClass")
        {
            DataTable dtSubjectToTeacher = new dalReport().SubjectToClass();
            rptDocument.Load(Server.MapPath("../../Report/Designer/rptSubjectToClass.rpt").ToString());
            rptDocument.SetDataSource(dtSubjectToTeacher);
            CRV.ReportSource = rptDocument;
        }
        if (report == "markSheet")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";

            rptDocument.Load(Server.MapPath("~/Report/Designer/rptMarkSheet.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            DataTable dtSubjectWiseMarks = new dalMarks().GetStudentMarks(criteria);  //SubjectWishMarks(criteria);
            dtSubjectWiseMarks.TableName = "Marksheet";
            dtSubjectWiseMarks.Columns.Add("GradeLetter");
            dtSubjectWiseMarks.Columns.Add("GradePoint");
            dtSubjectWiseMarks.Columns.Add("CGPA");
            decimal totalMarks = 0;
            foreach (DataRow row in dtSubjectWiseMarks.Rows)
            {
                totalMarks = totalMarks + Convert.ToDecimal(row["ConvertedMarks"]);
                GradeCalculation(row);
            }

            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                decimal obtainMarks = totalMarks / dtSubjectWiseMarks.Rows.Count;
                string gradePoint = string.Empty;
                string CGPA = GetCGPA(obtainMarks, out gradePoint);
                int rowIndex = 0;
                rowIndex = (Int32)(dtSubjectWiseMarks.Rows.Count / 2);

                dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = CGPA;
            }
            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                string headerCriteria = "er_StudentToClass.Id=" + dtSubjectWiseMarks.Rows[0]["StudentToClassId"];
                DataTable dtMarksSheetHeaderInfo = new dalMarks().GetMarksSheetHeaderInfo(headerCriteria);
                dtMarksSheetHeaderInfo.TableName = "MarksSheetHeader";
                reportDataSet.Tables.Add(dtMarksSheetHeaderInfo);

                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dtSubjectWiseMarks);
                rptDocument.SetDataSource(reportDataSet);
                CRV.ReportSource = rptDocument;
            }
        }

        if (report == "studentList")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptStudentList.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtStudent = obj.GetStudentInfoByCriteria(criteria);
            dtStudent.TableName = "Student";


            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            CRV.ReportSource = rptDocument;
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

    public DataTable SubjectWishMarks(string criteria)
    {
        resultInfo = new List<Tuple<string, string, string>>();
        DataTable dtMarks = null;
        if (!string.IsNullOrEmpty(criteria))
        {
            dtMarks = obj.GetSubjectMarks(criteria);
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
                        }
                    }
                }
            }
            else
            {
                if (!resultInfo.Any(t => t.Item1 == drr["RegNo"].ToString() && t.Item2 == drr["SubId"].ToString()))
                {
                    resultInfo.Add(new Tuple<string, string, string>(drr["RegNo"].ToString(), drr["SubId"].ToString(), drr["PaperNo"].ToString()));
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
                            //foreach (DataRow drprevious in drPreviousMarks)
                            //{
                            //    if (drprevious["Id"].ToString() != dr["Id"].ToString())
                            //    {
                            //        dtTempMarks.Rows.Remove(drprevious);
                            //    }
                            //}
                        }
                    }
                }
            }
            dr.EndEdit();
        }
        return dtTempMarks;
    }
    public DataTable CalculateResult(string criteria)
    {
        resultInfo = new List<Tuple<string, string, string>>();
        DataTable dtMarks = null;
        if (!string.IsNullOrEmpty(criteria))
        {
            dtMarks = obj.GetMarks(criteria);
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

    public DataTable CalculateGPA(DataTable dt)
    {
        int count = 1;
        foreach (DataRow dr in dt.Rows)
        {
            if (!regNo.Contains(dr["RegNo"].ToString()))
            {
                count = 1;
                regNo.Add(dr["RegNo"].ToString());
                if (dr["GradePoint"].ToString() != "-1111")
                    dsGrading.Tables["dtGrading"].Rows.Add(dr["RegNo"].ToString(), Convert.ToDecimal(dr["GradePoint"].ToString()), Convert.ToDecimal(dr["GradePoint"].ToString()), count);
                else
                    dsGrading.Tables["dtGrading"].Rows.Add(dr["RegNo"].ToString(), 0, 0, 1);
            }
            else
            {
                DataRow drGrading = dsGrading.Tables["dtGrading"].Select("RegNo='" + dr["RegNo"].ToString() + "'").First();
                drGrading.BeginEdit();
                if (dr["GradePoint"].ToString() != "-1111")
                {
                    count++;
                    drGrading["TotalGPA"] = Convert.ToDecimal(dr["GradePoint"].ToString()) + Convert.ToDecimal(drGrading["TotalGPA"].ToString());
                    drGrading["GPA"] = Convert.ToDecimal(drGrading["TotalGPA"].ToString()) / Convert.ToDecimal(count);
                    drGrading["SubjectNo"] = count;
                }
                else
                {
                    drGrading["TotalGPA"] = Convert.ToDecimal(0) + Convert.ToDecimal(drGrading["TotalGPA"].ToString());
                }
                drGrading.EndEdit();
            }
        }
        return dsGrading.Tables["dtGrading"];
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
        if ((subjective >= Convert.ToDouble(dtFailMarks.Rows[0]["SubjectiveFailMarks"])) && (objective >= Convert.ToDouble(dtFailMarks.Rows[0]["ObjectiveFailMarks"])))
            return true;
        return false;
    }
    public void GradeCalculation(DataRow dr)
    {
        DataTable dtGrade = new Common().GetAll("rs_Grade");
        foreach (DataRow drg in dtGrade.Rows)
        {
            double totalMarks = Convert.ToDouble(dr["ConvertedMarks"]);
            if ((totalMarks >= Convert.ToDouble(drg["StartMarks"])) && (totalMarks < Convert.ToDouble(drg["EndMarks"]) + 1))
            {
                dr["GradeLetter"] = drg["GradeName"].ToString();
                dr["GradePoint"] = drg["GradePoint"].ToString();
            }
        }
    }


}