using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using Nano.DataAccessLayer;
using Org.BouncyCastle.Asn1.Cms;

public partial class Report_Designer_ReportView : System.Web.UI.Page
{
    dalReport obj = new dalReport();
    dalTeacher dlTeacher = new dalTeacher();
    dalPayment objPayment=new dalPayment();
    dalPayroll objPayroll = new dalPayroll();
    dalStudent objStudent= new dalStudent();
    dalSubject obSubject= new dalSubject();
    dalMarks objMarks = new dalMarks();
    ReportDocument rptDocument = new ReportDocument();
    DataTable dts= new DataTable();

    public DataSet dsGrading = new Grading();
    public static List<Tuple<string, string, string>> resultInfo;
    public static List<Tuple<string, string>> GPA;
    List<string> regNo = new List<string>();
    DatabaseManager dm = new DatabaseManager();
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
        this.Unload += Report_Designer_ReportView_Unload;
    }
    private void Report_Designer_ReportView_Unload(object sender, EventArgs e)
    {
        rptDocument.Close();
        rptDocument.Dispose();
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
        public string MediumName { get; set; }
        public string Mobile { get; set; }
        public byte[] PersonImage { get; set; }
        public string SchoolName { get; set; }
        public string CampusName { get; set; }
    }
    private class TeacherIdCard
    {
        public string NameEng { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string TeacherPin { get; set; }
        public DateTime Joindate { get; set; }
        public string BloodGroup { get; set; }
        public string Mobile { get; set; }
        public byte[] PersonImage { get; set; }
        public string SchoolName { get; set; }
        public string EmployeeId { get; set; }
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

 DataTable SalaryDT
    {
        set { ViewState["SalaryDT"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["SalaryDT"];
            }
            catch
            {
                return null;
            }
        }
    }

    string Remarks
    {
        set { ViewState["Remarks"] = value; }
        get
        {
            try
            {
                return ViewState["Remarks"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }

    string IsRegNoUse
    {
        set { ViewState["RegNo"] = value; }
        get
        {
            try
            {
                return ViewState["RegNo"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }

 public DataTable Salary()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NameEng");
        dt.Columns.Add("Designation");
        dt.Columns.Add("EmployeeId");
        dt.Columns.Add("JoiningDate");
        dt.Columns.Add("Amount");
        dt.Columns.Add("GivenAmount");
        dt.Columns.Add("DueAmount");
        dt.Columns.Add("AccountNumber");
        dt.Columns.Add("CampusName");
        dt.Columns.Add("CreatedBy");
        dt.Columns.Add("Month");
        dt.Columns.Add("Year");
        dt.Columns.Add("Basic");
        dt.Columns.Add("Increment");
        dt.Columns.Add("Allowance");
        dt.Columns.Add("Bonus");
        dt.Columns.Add("Penalty");
        return dt;
    }

 public DataTable SummaryTB()
 {
     DataTable dt = new DataTable();
     dt.Columns.Add("GradeName");
     dt.Columns.Add("LevelName");
     dt.Columns.Add("CampusName");
     dt.Columns.Add("Month");
     dt.Columns.Add("Year");
     dt.Columns.Add("GivenAmount");
     return dt;
 }

    protected void CheckRollNoUse()
    {
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            IsRollNoUse = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
        }
    }
    protected void LoadReport()
    {
        string criteria = "";
        CheckRollNoUse();
        if (Request.QueryString["query"] != null)
            criteria = EncryptionDecryption.EncryptDecryptString(Request.QueryString["query"]);
        string report = Request.QueryString["report"];
        string type = Request.QueryString["Type"];
        string monthId = Request.QueryString["MonthId"];
        string yearId = Request.QueryString["YearId"];
        string campusId = Request.QueryString["CampusId"];
        string mdmId = Request.QueryString["MediumId"];
        string selectId = Request.QueryString["SelectId"];
        string levelId = Request.QueryString["LevelId"];
        string group = Request.QueryString["GroupId"];
        string anonymusType = Request.QueryString["AnonymusType"];
        string position = Request.QueryString["position"];
        string ttlAmount = Request.QueryString["TotalAmount"];
        string ttlRcv = Request.QueryString["TotalRcv"];
        string ttlDue = Request.QueryString["TotalDue"];
        if (PreviousPage != null)
        {
            if (PreviousPage.IsCrossPagePostBack == true)
            {

            }
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
                string imagePath = Server.MapPath("~/Images/Student/" + row["PersonImage"].ToString());
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
                sIdCard.CampusName = row["CampusName"].ToString();
                sIdCard.MediumName = row["MediumName"].ToString();

                sIdCard.SchoolName = dtSchool.Rows[0]["Name"].ToString();
                lstIdCard.Add(sIdCard);
            }
            dt.TableName = "Student";

            dtSchool.TableName = "School";
            reportDataSet.Tables.Add(dt);
            reportDataSet.Tables.Add(dtSchool);
            //if (IsRollNoUse == "False")
            //    rptDocument.Load(Server.MapPath("../../Report/Designer/RegNoIdCard.rpt").ToString());
                                      
            //else
            if(type=="1")
                rptDocument.Load(Server.MapPath("../../Report/Designer/IdCard.rpt").ToString());
            else
            {

                rptDocument.Load(Server.MapPath("../../Report/Designer/rptAdmitCard.rpt").ToString());
            }


            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            rptDocument.SetDataSource(lstIdCard);
            if(type!="1")
            rptDocument.SetParameterValue("ExamType", selectId);
            
            CRV.ReportSource = rptDocument;
        }

        if (report == "AdmitCard")
        {
            List<StudentIdCard> lstAdmitcard = new List<StudentIdCard>();

            DataSet reportDataSet = new DataSet();

            DataTable dt = obj.GetStudentforAdmitCard(criteria);
            dt.Columns.Add("PImage");
            foreach (DataRow row in dt.Rows)
            {
                string imagePath = Server.MapPath("~/Images/Student/" + row["PersonImage"].ToString());
                StudentIdCard sAdmitCard = new StudentIdCard();
                if (System.IO.File.Exists(imagePath))
                {
                    byte[] imageByte = string.IsNullOrEmpty(row["PersonImage"].ToString()) ? null : System.IO.File.ReadAllBytes(imagePath);
                    sAdmitCard.PersonImage = imageByte;
                }
                sAdmitCard.NameEng = row["NameEng"].ToString();
                sAdmitCard.BloodGroup = row["BloodGroup"].ToString();
                sAdmitCard.ClassName = row["ClassName"].ToString();
                sAdmitCard.GroupName = row["GroupName"].ToString();
                sAdmitCard.Shift = row["Shift"].ToString();
                sAdmitCard.Section = row["Section"].ToString();
                sAdmitCard.RegNo = row["RegNo"].ToString();
                sAdmitCard.RollNo = row["RollNo"].ToString();
                sAdmitCard.Year = row["Year"].ToString();
                sAdmitCard.Mobile = row["Mobile"].ToString();
                sAdmitCard.CampusName = row["CampusName"].ToString();
                sAdmitCard.MediumName = row["MediumName"].ToString();

                sAdmitCard.SchoolName = dtSchool.Rows[0]["Name"].ToString();
                lstAdmitcard.Add(sAdmitCard);
            }
            dt.TableName = "Student";
            dt.TableName = "MarkSheet";
           
            reportDataSet.Tables.Add(dt);
            reportDataSet.Tables.Add(dtSchool);
            //if (IsRollNoUse == "False")
            //    rptDocument.Load(Server.MapPath("../../Report/Designer/RegNoIdCard.rpt").ToString());

            //else

            rptDocument.Load(Server.MapPath("../../Report/Designer/rptAdmitCard.rpt").ToString());


            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            rptDocument.SetDataSource(lstAdmitcard);
            CRV.ReportSource = rptDocument;
        }

        if (report == "TeacherIdCard")
        {
            List<TeacherIdCard> lstIdCard = new List<TeacherIdCard>();

            DataSet reportDataSet = new DataSet();

         // DataTable dt = obj.GetTeacherforIdCard();
            DataTable dt = new dalTeacher().GetTeacherByCriteria(criteria);
            dt.Columns.Add("TImage");
            foreach (DataRow row in dt.Rows)
            {
                string imagePath = Server.MapPath("~/Images/Person/" + row["PersonImage"].ToString());
                TeacherIdCard tIdCard = new TeacherIdCard();

                if (System.IO.File.Exists(imagePath))
                {
                    byte[] imageByte = string.IsNullOrEmpty(row["PersonImage"].ToString()) ? null : System.IO.File.ReadAllBytes(imagePath);
                    tIdCard.PersonImage = imageByte;
                }

                tIdCard.NameEng = row["NameEng"].ToString();
                tIdCard.BloodGroup = row["BloodGroup"].ToString();
                tIdCard.Email = row["Email"].ToString();
                tIdCard.Designation = row["Designation"].ToString();
                tIdCard.Joindate = Convert.ToDateTime(row["Joindate"].ToString());
                tIdCard.TeacherPin = row["TeacherPin"].ToString();
                tIdCard.Mobile = row["Mobile"].ToString();
                tIdCard.EmployeeId = row["EmployeeId"].ToString();

                tIdCard.SchoolName = dtSchool.Rows[0]["Name"].ToString();
                lstIdCard.Add(tIdCard);
            }
            dt.TableName = "Teacher";
            dtSchool.TableName = "School";
            reportDataSet.Tables.Add(dt);
            reportDataSet.Tables.Add(dtSchool);
            rptDocument.Load(Server.MapPath("../../Report/Designer/rptTeacherIdCard.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(lstIdCard);
            CRV.ReportSource = rptDocument;
        }
        if (report == "stAttendence")
        {
            DataSet reportDataSet = new DataSet();
            DataTable dt = obj.GetStudentAttendence(criteria);


            dt.Columns.Add("SchoolName");
            dt.Columns.Add("SchoolAddress");
            dt.Columns.Add("SchoolCode");

            if (dtSchool.Rows.Count > 0 && dt.Rows.Count > 0)
            {
                dt.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dt.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dt.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
            }

            dt.TableName = "StudentAttendence";
            reportDataSet.Tables.Add(dt);
            reportDataSet.Tables.Add(dtSchool);
            rptDocument.Load(Server.MapPath("../../Report/Designer/rptSAttendance.rpt").ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            rptDocument.SetDataSource(reportDataSet);
            CRV.ReportSource = rptDocument;
        }
        if (report == "AttendenceSummary")
        {
            DataTable summary = dm.NewExecuteQuery(@"Select * from View_StudentAttendenceSummary where " + criteria);

            var q = summary.AsEnumerable().GroupBy(r => r["RollNo"]);

            var schoollist = new Common().GetAll("bs_SchoolInformation");

            var s = schoollist.AsEnumerable();
            var schoolName = "";
            var code = "";
            foreach (var item in s)
            {
                schoolName = item["Name"].ToString();
                code = item["Code"].ToString();
            }

            List<rptViewAttendanceSummary> list = new List<rptViewAttendanceSummary>();

            foreach (var item in q)
            {
                var rv = new rptViewAttendanceSummary();
                rv.Roll = item.Key.ToString();
                rv.Name = item.First()["NameEng"].ToString();
                rv.CampusName = item.First()["CampusName"].ToString();
                rv.Class = item.First()["Class"].ToString();
                rv.GroupName = item.First()["GroupName"].ToString();
                rv.Shift = item.First()["Shift"].ToString();
                rv.Section = item.First()["Section"].ToString();
                rv.Attendance = item.First()["AttendenceType"].ToString();
                rv.Year = item.First()["AttendenceYear"].ToString();
                rv.MediumName = item.First()["MediumName"].ToString();

                rv.Attendance = rv.Attendance == "1" ? "Class Attendence" : "Exam Attendence";
                rv.TotalPresent = item.GroupBy(o => o["Date"])
                    .Count(o => o.Any(p => p["IsPresent"].ToString() == "True")).ToString();
                rv.TotalAbsent = item.GroupBy(o => o["Date"])
                    .Count(o => o.All(p => p["IsPresent"].ToString() == "False")).ToString();
                rv.TotalWorkingDay = item.GroupBy(o => o["Date"]).Count().ToString();
                rv.PresentPersent = Convert.ToInt32(rv.TotalPresent) * 100 / Convert.ToInt32(rv.TotalWorkingDay);


                list.Add(rv);
            }

            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            rptDocument.Load(Server.MapPath("~\\Report\\Designer\\AttendenceSummary.rpt").ToString());
            DataSet set = new DataSet();

            // rptDocument.SetDataSource(list.OrderBy(o=>o.Roll));
            rptDocument.SetDataSource(list);
            var date = DateTime.Now.ToString();
            rptDocument.SetParameterValue("printDate", date);
            rptDocument.SetParameterValue("fromDate", fromDate);
            rptDocument.SetParameterValue("toDate", toDate);
            rptDocument.SetParameterValue("SchoolName", schoolName);
            rptDocument.SetParameterValue("SchoolCode", code);
            CRV.ReportSource = rptDocument;

        }
        if (report == "teacher")
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();
            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);
            string printDate = localTime.ToString("d");
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            DataTable dtTeacher = new DataTable();
            if (type == "2")
                dtTeacher = new dalTeacher().GetAll();
            else
                dtTeacher = new dalTeacher().GetTeacherByCriteria(criteria);
            dtTeacher.TableName = "Teacher";
            reportDataSet.Tables.Add(dtTeacher);
            reportDataSet.Tables.Add(dtSchool);
            rptDocument.Load(Server.MapPath("../../Report/Designer/rptTeacherList.rpt").ToString());
            rptDocument.SetParameterValue("SchoolName", dtSchool.Rows[0]["Name"].ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("PrintDate", printDate);
            CRV.ReportSource = rptDocument;
        }

        if (report == "monthlyBill")
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();
            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);
            string printDate = localTime.ToString("d");
            // string month = localTime.ToString("MMMM");
            DateTime nextMonth = localTime.AddMonths(1);
            string month = nextMonth.ToString("MMMM");
            var mn = localTime.Month + 5;
            var reff = "0" + mn;

            rptDocument.Load(Server.MapPath("../../Report/Designer/rptMonthlyBill.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            rptDocument.SetParameterValue("PrintDate", printDate);
            rptDocument.SetParameterValue("Month", month);
            rptDocument.SetParameterValue("Reff", reff);
            CRV.ReportSource = rptDocument;
        }

        if (report == "subjectToClass")
        {

            var schoollist = new Common().GetAll("bs_SchoolInformation");

            var s = schoollist.AsEnumerable();
            var schoolName = "";
            var code = "";
            foreach (var item in s)
            {
                schoolName = item["Name"].ToString();
                code = item["Code"].ToString();
            }




            DataSet reportDataSet = new DataSet();          
            DataTable dtSubjectToClass = new DataTable();
            if (selectId == "1")
            {
                dtSubjectToClass = new dalSubject().GetSubjectToClass(criteria);
                rptDocument.Load(Server.MapPath("../../Report/Designer/rptSubjectList.rpt").ToString());
            }

            else
            {
                dtSubjectToClass = new dalSubject().GetOptionalSubject(criteria);
                rptDocument.Load(Server.MapPath("../../Report/Designer/rptCompulsoryOptionalSubjectList.rpt").ToString());
            }
               
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            dtSubjectToClass.TableName = "SubjectToClass";

            reportDataSet.Tables.Add(dtSubjectToClass);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("schoolName", schoolName);
            rptDocument.SetParameterValue("code", code);
            CRV.ReportSource = rptDocument;

            //DataTable dtSubjectToTeacher = new dalReport().SubjectToClass();
            //rptDocument.Load(Server.MapPath("../../Report/Designer/rptSubjectToClass.rpt").ToString());
            //rptDocument.SetDataSource(dtSubjectToTeacher);
            //CRV.ReportSource = rptDocument;
        }
         if (report == "subjectWiseMarks")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            DataTable dtStudent = new DataTable();
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptSubjectWiseMarks.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            if (Convert.ToInt32(selectId) > 11)
            {
                dtStudent = objMarks.GetStudentMarksForView(criteria);
            }
            else
            {
                dtStudent = objMarks.GetStudentMarksForViewForJunior(criteria);
            }
            dtStudent.TableName = "Student";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("ExamType",type);
            CRV.ReportSource = rptDocument;

        }

 if (report == "EmployeeSalary")
         {

             DateTime serverTime = DateTime.Now;
             DateTime utcTime = serverTime.ToUniversalTime();
             TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
             DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);
             string printDate = localTime.ToString("d");
             string pDate = localTime.ToString("d");
             string grade = "";
             string level = "";
             string accountNumber = "";
             DataSet reportDataset = new DataSet();
             DataTable dtEmpSalary = new DataTable();
             DataTable dtE = new DataTable();
             SalaryDT = Salary();
             if (type != "Summary")
             {
                 dtE = dlTeacher.GetTeacherByCampusIdAndGradeId(Convert.ToInt32(campusId), Convert.ToInt32(selectId), Convert.ToInt32(levelId));

                 foreach (DataRow row in dtE.Rows)
                 {
                     int empId = Convert.ToInt32(row["EmployeeId"]);
                     string name = row["NameEng"].ToString();
                     string accNum = row["AccountNumber"].ToString();
                     DateTime joinDate = (DateTime)row["JoinDate"];
                     string createdBy = row["CreatedBy"].ToString();
                     string designation = row["Designation"].ToString();
                     string campusName = row["CampusName"].ToString();
                     grade = row["Type"].ToString();
                     level = row["LevelName"].ToString();
                     dtEmpSalary = objPayroll.GetEmployeePaidSalaryMonthly(empId, Convert.ToInt32(yearId), Convert.ToInt32(monthId));
                     if (dtEmpSalary.Rows.Count > 0)
                     {
                         var basic = dtEmpSalary.AsEnumerable().Where(s => s.Field<int>("PayrollTypeId") == 1).FirstOrDefault().Field<decimal>("Amount");
                         var allowance = dtEmpSalary.AsEnumerable().Where(s => s.Field<int>("PayrollTypeId") == 2).FirstOrDefault().Field<decimal>("Amount");
                         var increment = dtEmpSalary.AsEnumerable().Where(s => s.Field<int>("PayrollTypeId") == 3).FirstOrDefault().Field<decimal>("Amount");
                         var bonus = dtEmpSalary.AsEnumerable().Where(s => s.Field<int>("PayrollTypeId") == 4).FirstOrDefault().Field<decimal>("Amount");
                         var penalty = dtEmpSalary.AsEnumerable().Where(s => s.Field<int>("PayrollTypeId") == 5).FirstOrDefault().Field<decimal>("Amount");
                         string month = dtEmpSalary.Rows[0]["Month"].ToString();
                         string year = dtEmpSalary.Rows[0]["Year"].ToString();
                         var sFirstFour = dtEmpSalary.AsEnumerable().Take(4).Sum(s => s.Field<decimal>("Amount"));
                         var sFive = dtEmpSalary.AsEnumerable().Skip(4).Sum(s => s.Field<decimal>("Amount"));
                         var sTotal = sFirstFour - sFive;

                         var sgFirstFour = dtEmpSalary.AsEnumerable().Take(4).Sum(s => s.Field<decimal>("GivenAmount"));
                         var sgFive = dtEmpSalary.AsEnumerable().Skip(4).Sum(s => s.Field<decimal>("GivenAmount"));
                         var sgTotal = sFirstFour - sFive;
                         var sdDue = sTotal - sgTotal;
                         SalaryDT.Rows.Add(name, designation, empId, joinDate.ToString("dd/MM/yy"), sTotal, sgTotal, sdDue, accNum, campusName, createdBy, month, year, basic, increment, allowance, bonus, penalty);
                     }
                 }
             }
             else
             {
                 SalaryDT = SummaryTB();
                 dtE = objPayroll.GetSalaryDetails(Convert.ToInt32(yearId), Convert.ToInt32(monthId), Convert.ToInt32(campusId));
                 var payment = dtE.AsEnumerable()
                     .GroupBy(o => o.Field<int>("GradeId"))
                     .Select(s => new
                     {
                         name = s.First()["Type"],
                         campus=s.First()["CampusName"],
                         year =s.First()["Year"],
                         month=s.First()["Month"],
                         grdId = s.Key,
                         lvls = s.GroupBy(m => m.Field<int>("LevelId"))
                         
                         .Select(lvl => new
                         {
                             name = s.First()["LevelName"],
                             lvlId = lvl.Key,
                             sum = lvl.Sum(l => l.Field<decimal>("GivenAmount"))
                         })
                     });
                 foreach (var item in payment)
                 {
                     var typeName = item.name;
                     var campusName = item.campus;
                     var yearName = item.year;
                     var monthName = item.month;
                     foreach (var grp in item.lvls)
                     {
                         var lvl = grp.name;
                         var ttl = grp.sum;
                         SalaryDT.Rows.Add(typeName,lvl,campusName,monthName,yearName,ttl);
                     }
                 }
             }
             decimal tAmount = SalaryDT.AsEnumerable().Sum(s =>decimal.Parse( s["GivenAmount"].ToString()));
             var tAmountinWords = "TK. "+ NumberToWords(Convert.ToInt32(tAmount)) + " Only";
             dtSchool.TableName = "School";
             if (type == "salaryCash" || type == "salaryBank")
             {             
                 if (type == "salaryBank")
                    rptDocument.Load(Server.MapPath("~/Report/Designer/rptSalary.rpt").ToString());
                 else
                    rptDocument.Load(Server.MapPath("~/Report/Designer/rptSalaryCash.rpt").ToString());
             }
             else if (type == "signatureBank")
             {
                 dtEmpSalary = (DataTable)Session["Salary"];
                 rptDocument.Load(Server.MapPath("~/Report/Designer/rptSignatureBank.rpt").ToString());
             }
             else if (type == "signatureCash")
             {
                 rptDocument.Load(Server.MapPath("~/Report/Designer/rptSignatureCash.rpt").ToString());
             }
             else if(type=="Summary")
             {
                 rptDocument.Load(Server.MapPath("~/Report/Designer/rptSalarySummary.rpt").ToString());
             }
             else
             {
                 rptDocument.Load(Server.MapPath("~/Report/Designer/rptSalary.rpt").ToString());
             }
             accountNumber = (campusId == "1") ? "08633000291" : "08633000338";
             rptDocument.SetDatabaseLogon(user, password, server, dbName);
             SalaryDT.TableName = "Salary";
             reportDataset.Tables.Add(dtSchool);
             reportDataset.Tables.Add(SalaryDT);
             rptDocument.SetDataSource(reportDataset);
             rptDocument.SetParameterValue("PrintDate", printDate);
             if (type == "salaryCash" || type == "salaryBank" || type == "details"|| type=="Summary")
             {
                if (type == "salaryBank")
                   {
                      rptDocument.SetParameterValue("AccountNumber", accountNumber);
                   }
                 rptDocument.SetParameterValue("TAmount", tAmount);
                 rptDocument.SetParameterValue("AmountInWords", tAmountinWords);
             }
             if (type != "Summary")
             {
                 rptDocument.SetParameterValue("Grade", grade);
                 rptDocument.SetParameterValue("Level", level);
             }              
             rptDocument.SetParameterValue("PDate", pDate);
             CRV.ReportSource = rptDocument;
            
         }


  if (report == "blankSubjectWiseMarks")
        {
            string subject = "";
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptBlankReport.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtStudent = objStudent.GetStudentForMarksEntry(criteria, Convert.ToInt32(selectId));
            DataTable dtSubject = obSubject.GetSubjectById(Convert.ToInt32(selectId));
            subject = dtSubject.Rows[0]["SubjectName"].ToString();
            dtStudent.TableName = "Student";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("ExamType", type);
            rptDocument.SetParameterValue("Subject", subject);
            CRV.ReportSource = rptDocument;

        }
     
       if (report == "markSheet")
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();
            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);

            string printDate = localTime.ToString("d");
            string workingDay = "0";
            string present = "0";
            string absent = "0";
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";

            rptDocument.Load(Server.MapPath("~/Report/Designer/rptMarksSheet.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtSubjectWiseMarks = new DataTable();
            if (type == "12")
            {
                if (group == "2")
                {
                    criteria += " and SubjectID not in (142,143,145,146)";
                }
                else
                {

                }
            }
            if (Convert.ToInt32(selectId) >= 12)
            {
                dtSubjectWiseMarks = new dalMarks().GetStudentMarksforHigherClass(criteria);
            }

            else
            dtSubjectWiseMarks = new dalMarks().GetStudentMarks(criteria);  //SubjectWishMarks(criteria);
            dtSubjectWiseMarks.TableName = "Marksheet";
            dtSubjectWiseMarks.Columns.Add("GradeLetter");
            dtSubjectWiseMarks.Columns.Add("LetterGrade");
            dtSubjectWiseMarks.Columns.Add("GradePoint");
            dtSubjectWiseMarks.Columns.Add("CGPA");
            dtSubjectWiseMarks.Columns.Add("Remarks");
            dtSubjectWiseMarks.Columns.Add("WorkingDay");
            dtSubjectWiseMarks.Columns.Add("Present");
            string criterias = "StudentToClassId=" + dtSubjectWiseMarks.Rows[0]["StudentToClassId"].ToString() + " and AttendenceYear=" + dtSubjectWiseMarks.Rows[0]["AcademicYear"].ToString();
            DataTable summary = dm.NewExecuteQuery(@"Select * from View_StudentAttendenceSummary where " + criterias);
            var q = summary.AsEnumerable().GroupBy(r => r["RegNo"]);
            //var q = summary.AsEnumerable().GroupBy(r => r["RollNo"]);
            foreach (var item in q)
            {
                present = item.GroupBy(o => o["Date"])
                    .Count(o => o.Any(p => p["IsPresent"].ToString() == "True")).ToString();
                absent = item.GroupBy(o => o["Date"])
                    .Count(o => o.All(p => p["IsPresent"].ToString() == "False")).ToString();
                workingDay = item.GroupBy(o => o["Date"]).Count().ToString();
            }
            double totalMarks = 0;
            double baseMarks = 0;
            double subjectiveMarks = 0;
            double objectiveMarks = 0;

            DataTable dtFailMarks = new DataTable();
            double totalMarksPercent = 0;
            foreach (DataRow row in dtSubjectWiseMarks.Rows)
            {
                int mediumId = Convert.ToInt32(row["MediumId"]);
                int classId = Convert.ToInt32(row["ClassId"]);
                int groupId = Convert.ToInt32(row["GroupId"]);
                int subjectId = Convert.ToInt32(row["SubjectId"]);
                string subjectName = row["SubjectName"].ToString();
                subjectiveMarks = Convert.ToDouble(row["SubjectiveMarks"]);
                objectiveMarks = Convert.ToDouble(row["ObjectiveMarks"]);

                if (classId<12)
                {
                    baseMarks = Convert.ToDouble(row["BaseMarks"]);
                    totalMarks = totalMarks + Convert.ToDouble(row["ConvertedMarks"]);
                    totalMarksPercent += Convert.ToDouble(row["ConvertedMarks"]) * 100 / Convert.ToDouble(row["BaseMarks"]);
                GradeCalculation(row);
                }
                else
                {
                    dtFailMarks = new dalFailSystem().GetByFailBySubjectAndClassId(Convert.ToInt32(yearId), Convert.ToInt32(campusId), Convert.ToInt32(type), subjectId, classId, mediumId, groupId);
                    foreach (DataRow rowfail in dtFailMarks.Rows)
                    {
                        if (subjectId == Convert.ToInt32(rowfail["SubjectToClassId"].ToString()))
                        {
                            if ((subjectiveMarks >= Convert.ToDouble(dtFailMarks.Rows[0]["SubjectiveFailMarks"])) &&
                (objectiveMarks >= Convert.ToDouble(dtFailMarks.Rows[0]["ObjectiveFailMarks"])))
                            {
                                GradeCalculation(row);                            
                            }
                            else
                            {                              
                                row["GradeLetter"] = "F";
                                row["GradePoint"] = "0.00";
                            }
                        }

                    }
                }
                }
                              
            bool flag = false;
            int count = 0;
            int failSubject = 0;
            double totalpoint = 0;
            decimal gpa = 0;
            
            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSubjectWiseMarks.Rows)
                {
                    count++;

                    if (Convert.ToInt32(dr["ClassId"]) >= 12)
                    {
                        if (dr["GradePoint"].ToString() == "0.00")
                        {                           
                            if (Convert.ToBoolean(dr["IsOptional"]) == false)
                            {
                                flag = true;
                                failSubject++;
                            }                           
                        }
                        else
                        {
                            double gPoint = 0;
                            if (Convert.ToBoolean(dr["IsOptional"]) == true)
                            {                            
                                double.TryParse(dr["GradePoint"].ToString(), out gPoint);
                                if (gPoint > 1)
                                {
                                    gPoint = gPoint - 2;
                                }
                                else
                                    gPoint = 0;

                                totalpoint += gPoint;
                            }
                            else
                            {
                                double.TryParse(dr["GradePoint"].ToString(), out gPoint);
                                totalpoint += gPoint;
                            }
                           
                        }
                    }
                    else
                    {
                        if (dr["GradePoint"].ToString() == "0.00")
                        {
                            flag = true;
                            failSubject++;
                        }
                        else
                        {
                            double gPoint = 0;
                            double.TryParse(dr["GradePoint"].ToString(), out gPoint);
                            totalpoint += gPoint;
                        }
                    }
                  
                }
                if (Convert.ToInt32(selectId) < 12)
                {
                    gpa = Math.Round(Convert.ToDecimal(totalpoint / count), 2);
                }
                else
                {
                    gpa = Math.Round(Convert.ToDecimal(totalpoint / (count - 1)), 2);
                    if (gpa > 5)
                    {
                        gpa = 5;
                    }
                }
                    
                double obtainMarks = totalMarksPercent / dtSubjectWiseMarks.Rows.Count;

                string gradePoint = string.Empty;
                string CGPA = GetCGPA(Convert.ToDecimal(obtainMarks), out gradePoint);
                int rowIndex = 0;
                rowIndex = (Int32)(dtSubjectWiseMarks.Rows.Count / 2);
                if (flag)
                    dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = "0.00";
                else
                    dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = gpa.ToString("0.00");

                string letterGrade = GetLetterGrade(gpa, out gradePoint);
                int rIndex = 0;
                rIndex = (Int32)(dtSubjectWiseMarks.Rows.Count / 2);
                dtSubjectWiseMarks.Rows[rIndex]["LetterGrade"] = letterGrade;
                dtSubjectWiseMarks.Rows[rIndex]["Remarks"] = Remarks;
                dtSubjectWiseMarks.Rows[rIndex]["WorkingDay"] = workingDay;
                dtSubjectWiseMarks.Rows[rIndex]["Present"] = present;
            }
            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                double obtainMarks = totalMarksPercent / dtSubjectWiseMarks.Rows.Count;               
                string gradePoint = "";
                string letterGrade = "";
                string CGPA = "";

                if (failSubject>0)
                {
                    letterGrade = "F";
                    CGPA = "0.00";
                    Remarks = "Fail";
                }
                else
                {
                    letterGrade = GetLetterGrade(gpa, out gradePoint);
                     CGPA = gpa.ToString("0.00");
                }

                string headerCriteria = "er_StudentToClass.Id=" + dtSubjectWiseMarks.Rows[0]["StudentToClassId"];
                DataTable dtMarksSheetHeaderInfo = new dalMarks().GetMarksSheetHeaderInfo(headerCriteria);
                dtMarksSheetHeaderInfo.TableName = "MarksSheetHeader";
                reportDataSet.Tables.Add(dtMarksSheetHeaderInfo);

                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dtSubjectWiseMarks);
                rptDocument.SetDataSource(reportDataSet);
                rptDocument.SetParameterValue("LetterGrade", letterGrade);
                rptDocument.SetParameterValue("CGPA", CGPA);
                rptDocument.SetParameterValue("Remarks", Remarks);
                rptDocument.SetParameterValue("WorkingDay", workingDay);
                rptDocument.SetParameterValue("Present", present);
                rptDocument.SetParameterValue("Position", position);
                rptDocument.SetParameterValue("PrintDate", printDate);
                CRV.ReportSource = rptDocument;
            }
        }

       //------TestExam marksheet-----

   if (report == "TestExamMarksheet")
        {

 DataSet reportDataSet = new DataSet();
            string workingDay = "0";
            string present = "0";
            string absent = "0";
            var bn1stCode = "";
            var bn2ndCode = "";
            var e1stCode = "";
            var e2ndCode = "";
            int classId = Convert.ToInt32(selectId);
            int examType = Convert.ToInt32(type);
            if (examType == 12)
            {
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptTestExamMarksheet.rpt").ToString());
            }
            else
            {
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptExamMarksheetforSecondary.rpt").ToString());
            }
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            string special = criteria;
            string ctr = criteria;

            var mdm = mdmId;
            var grp = group;

            if (mdmId=="1")
            {
                bn1stCode = grp == "2" ? "1442"
                                : grp == "3" ? "1425" : "388";
                bn2ndCode = grp == "2" ? "1443"
                    : grp == "3" ? "1426" : "389";
                e1stCode = grp == "2" ? "1445"
                    : grp == "3" ? "1428" : "390";
                e2ndCode = grp == "2" ? "1446"
                    : grp == "3" ? "1429" : "391";
            }
            else
            {
                bn1stCode = grp == "2" ? "1354"
                                : grp == "3" ? "1372" : "388";
                bn2ndCode = grp == "2" ? "1355"
                    : grp == "3" ? "1373" : "389";
                e1stCode = grp == "2" ? "1357"
                    : grp == "3" ? "1375" : "390";
                e2ndCode = grp == "2" ? "1358"
                    : grp == "3" ? "1376" : "391";
            }
                       

            var some = string.Format("{0},{1},{2},{3}", bn1stCode, bn2ndCode, e1stCode, e2ndCode);

            ctr += " and rs_ObtainMarks.SubjectID not in(" + some + ")";
            special += " and rs_ObtainMarks.SubjectID in(" + some + ")";

            DataTable dt = new dalMarks().GetStudentMarksforHigherClass(criteria);
            int mediumId = Convert.ToInt32(dt.Rows[0]["MediumId"].ToString());
            int groupId = Convert.ToInt32(dt.Rows[0]["GroupId"].ToString());

            double cgpa = 0.00;
            string letterGrade = "";
            string banglaLetterGrade = "";
            double banglaGPA = 0.00;
            string englishLetterGrade = "";
            double englishGpa = 0.00;
            double banglaAVG = 0;
            double englishAVG = 0;
            double b1baseMarks = 0;
            double b2baseMarks = 0;
            double e1baseMarks = 0;
            double e2baseMarks = 0;
            double b1subMark = 0;
            double b1Monthly = 0;
            double b1objMark = 0;
            double b1TotalMark = 0;
            double b2subMark = 0;
            double b2Monthly = 0;
            double b2objMark = 0;
            double b2TotalMark = 0;
            double e1subMark = 0;
            double e1Monthly = 0;
            double e1TotalMark = 0;
            double e2subMark = 0;
            double e2Monthly = 0;
            double e2TotalMark = 0;
            double banglaTotal = 0;
            double englishTotal = 0;
            string b1 = "";
            string b2 = "";
            string e1 = "";
            string e2 = "";
            double b1Highest = 0;
            double b2Highest = 0;
            double e1Highest = 0;
            double e2Highest = 0;
            double banglaHighest = 0;
            double englishHighest = 0;

            int b1SubId = 0;
            int b2SubId = 0;
            int e1SubId = 0;
            int e2SubId = 0;
            double b1theoryPass = 0;
            double b2theoryPass = 0;
            double b1objPass = 0;
            double b2objPass = 0;
            double e1theoryPass = 0;
            double e2theoryPass = 0;
            double e1objPass = 0;
            double e2objPass = 0;
            double banglaTheoryPass = 0;
            double banglaObjPass = 0;
            double englishTheoryPass = 0;
            double englishObjPass = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["SubjectID"].ToString() == bn1stCode)
                {
                    b1 = row["SubjectName"].ToString();
                    b1baseMarks = Convert.ToDouble(row["BaseMarks"].ToString());
                    b1subMark = Convert.ToDouble(row["SubjectiveMarks"].ToString());
                    b1Monthly = Convert.ToDouble(row["MonthlyMarks"].ToString());
                    b1objMark = Convert.ToDouble(row["ObjectiveMarks"].ToString());
                    b1TotalMark = Convert.ToDouble(row["ConvertedMarks"].ToString());
                    // b1Highest = Convert.ToDouble(row["highest"].ToString());
                }
                else if (row["SubjectID"].ToString() == bn2ndCode)
                {
                    b2 = row["SubjectName"].ToString();
                    b2baseMarks = Convert.ToDouble(row["BaseMarks"].ToString());
                    b2subMark = Convert.ToDouble(row["SubjectiveMarks"].ToString());
                    b2Monthly = Convert.ToDouble(row["MonthlyMarks"].ToString());
                    b2objMark = Convert.ToDouble(row["ObjectiveMarks"].ToString());
                    b2TotalMark = Convert.ToDouble(row["ConvertedMarks"].ToString());
                    // b2Highest = Convert.ToDouble(row["highest"].ToString()) == null ? 0 : Convert.ToDouble(row["highest"].ToString());
                }
                else if (row["SubjectID"].ToString() == e1stCode)
                {
                    e1 = row["SubjectName"].ToString();
                    e1baseMarks = Convert.ToDouble(row["BaseMarks"].ToString());
                    e1subMark = Convert.ToDouble(row["SubjectiveMarks"].ToString());
                    e1Monthly = Convert.ToDouble(row["MonthlyMarks"].ToString());
                    e1TotalMark = Convert.ToDouble(row["ConvertedMarks"].ToString());
                    // e1Highest = Convert.ToDouble(row["highest"].ToString()) == null ? 0 : Convert.ToDouble(row["highest"].ToString());
                }
                else if (row["SubjectID"].ToString() == e2ndCode)
                {
                    e2 = row["SubjectName"].ToString();
                    e2baseMarks = Convert.ToDouble(row["BaseMarks"].ToString());
                    e2subMark = Convert.ToDouble(row["SubjectiveMarks"].ToString());
                    e2Monthly = Convert.ToDouble(row["MonthlyMarks"].ToString());
                    e2TotalMark = Convert.ToDouble(row["ConvertedMarks"].ToString());
                    // e2Highest = Convert.ToDouble(row["highest"].ToString()) == null ? 0 : Convert.ToDouble(row["highest"].ToString());
                }

                else if (row["SubjectID"].ToString() == "")
                {

                }
            }

            banglaHighest = b1Highest + b2Highest;
            englishHighest = e1Highest + e2Highest;

            banglaTotal = (b1TotalMark + b2TotalMark);
            englishTotal = (e1TotalMark + e2TotalMark);

            banglaAVG = banglaTotal / (b1baseMarks + b2baseMarks) * 100;
            englishAVG = englishTotal / (e1baseMarks + e2baseMarks) * 100;

            DataTable dtSubjectWiseMarks = new dalMarks().GetStudentMarksforHigherClass(ctr);
            dtSubjectWiseMarks.TableName = "Marksheet";
            dtSubjectWiseMarks.Columns.Add("GradeLetter");
            dtSubjectWiseMarks.Columns.Add("GradePoint");
            dtSubjectWiseMarks.Columns.Add("CGPA");


            string criterias = "StudentToClassId=" + dtSubjectWiseMarks.Rows[0]["StudentToClassId"].ToString() + " and AttendenceYear=" + dtSubjectWiseMarks.Rows[0]["Year"].ToString();
            DataTable summary = dm.NewExecuteQuery(@"Select * from View_StudentAttendenceSummary where " + criterias);
            var q = summary.AsEnumerable().GroupBy(r => r["RollNo"]);
            foreach (var item in q)
            {
                present = item.GroupBy(o => o["Date"])
                    .Count(o => o.Any(p => p["IsPresent"].ToString() == "True")).ToString();
                absent = item.GroupBy(o => o["Date"])
                    .Count(o => o.All(p => p["IsPresent"].ToString() == "False")).ToString();
                workingDay = item.GroupBy(o => o["Date"]).Count().ToString();
            }

            double subjectiveMarks = 0;
            double objectiveMarks = 0;
            decimal totalMarks = 0;
            double totalNum = 0;
            DataTable dtFailMarks = new DataTable();
            foreach (DataRow row in dtSubjectWiseMarks.Rows)
            {
                totalNum += Convert.ToDouble(row["ConvertedMarks"]);
                int subjectId = Convert.ToInt32(row["SubjectID"]);
                subjectiveMarks = Convert.ToDouble(row["SubjectiveMarks"]);
                objectiveMarks = Convert.ToDouble(row["ObjectiveMarks"]);
                dtFailMarks = new dalFailSystem().GetByFailBySubjectAndClassId(Convert.ToInt32(yearId), Convert.ToInt32(campusId), Convert.ToInt32(type), subjectId, classId, mediumId, groupId);
                foreach (DataRow rowfail in dtFailMarks.Rows)
                {

                    if (subjectId == Convert.ToInt32(rowfail["SubjectToClassId"].ToString()))
                    {
                        if ((subjectiveMarks >= Convert.ToDouble(dtFailMarks.Rows[0]["SubjectiveFailMarks"])) &&
            (objectiveMarks >= Convert.ToDouble(dtFailMarks.Rows[0]["ObjectiveFailMarks"])))
                        {
                            GradeCalculation(row);
                        }
                        else
                        {
                            row["GradeLetter"] = "F";
                            row["GradePoint"] = "0.00";
                        }
                    }

                }
            }
            bool flag = false;
            int count = 0;
            int failSubject = 0;
            double totalpoint = 0;
            decimal gpa = 0;
            string LetterGrade = "";
            string crt = "";
            if (groupId == 2)
            {
                crt = "MediumId= " + mediumId + " and GroupId= " + groupId + " and ClassId= " + classId + " and ExamtypeId=" + examType + " and SubjectToClassId in (1442,1443,1445,1446)";
            }
            else if (groupId == 3)
            {
                crt = "MediumId= " + mediumId + " and GroupId= " + groupId + " and ClassId= " + classId + " and ExamtypeId=" + examType + " and SubjectToClassId in (1425,1426,1428,1429)";
            }
            else
            {
                crt = "MediumId= " + mediumId + " and GroupId= " + groupId + " and ClassId= " + classId + " and ExamtypeId=" + examType + " and SubjectToClassId in (388,389,390,391)";
            }

            DataTable dtBangEng = obSubject.GetByClassAndGroupIdforTestByCriteria(crt);

            foreach (DataRow row3 in dtBangEng.Rows)
            {
                int subId = Convert.ToInt32(row3["SubjectToClassId"]);
                DataTable dtSubj = obSubject.GetSetMarksById(subId, (Int32)examType);

                if (dtSubj.Rows[0]["Id"].ToString() != "" && subId == Convert.ToInt32(bn1stCode))
                {
                    b1theoryPass = Convert.ToDouble(dtSubj.Rows[0]["SubjectiveFailMarks"]);
                    b1objPass = Convert.ToDouble(dtSubj.Rows[0]["ObjectiveFailMarks"]);
                }
                else if (dtSubj.Rows[0]["Id"].ToString() != "" && subId == Convert.ToInt32(bn2ndCode))
                {
                    b2theoryPass = Convert.ToDouble(dtSubj.Rows[0]["SubjectiveFailMarks"]);
                    b2objPass = Convert.ToDouble(dtSubj.Rows[0]["ObjectiveFailMarks"]);
                }
                else if (dtSubj.Rows[0]["Id"].ToString() != "" && subId == Convert.ToInt32(e1stCode))
                {
                    e1theoryPass = Convert.ToDouble(dtSubj.Rows[0]["SubjectiveFailMarks"]);
                    e1objPass = Convert.ToDouble(dtSubj.Rows[0]["ObjectiveFailMarks"]);
                }
                else if (dtSubj.Rows[0]["Id"].ToString() != "" && subId == Convert.ToInt32(e2ndCode))
                {
                    e2theoryPass = Convert.ToDouble(dtSubj.Rows[0]["SubjectiveFailMarks"]);
                    e2objPass = Convert.ToDouble(dtSubj.Rows[0]["ObjectiveFailMarks"]);
                }
            }

            banglaTheoryPass = Math.Round((b1theoryPass + b2theoryPass));
            banglaObjPass = Math.Round((b1objPass + b2objPass));
            englishTheoryPass = Math.Round((e1theoryPass + e2theoryPass));
            englishObjPass = Math.Round((e1objPass + e2objPass));

            if (banglaTheoryPass <= b1subMark + b2subMark && banglaObjPass <= b1objMark + b2objMark)
            {
                banglaGPA = GetTestCGPA(banglaAVG);
                banglaLetterGrade = GetTestGrade(banglaAVG);
            }
            else
            {
                flag = true;
                banglaGPA = 0.00;
                position = "Fail";
                banglaLetterGrade = "F";
            }

            if (englishTheoryPass <= e1subMark + e2subMark && englishObjPass <= e1objPass + e2objPass)
            {
                englishGpa = GetTestCGPA(englishAVG);
                englishLetterGrade = GetTestGrade(englishAVG);
            }
            else
            {
                flag = true;
                position = "Fail";
                englishGpa = 0.00;
                englishLetterGrade = "F";

            }

            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSubjectWiseMarks.Rows)
                {
                    count++;
                    if (dr["GradePoint"].ToString() == "0.00" && (bool)dr["IsOptional"] == false)
                    {
                        flag = true;
                        failSubject++;
                    }
                    else
                    {
                        double gPoint = 0;
                        double.TryParse(dr["GradePoint"].ToString(), out gPoint);
                        totalpoint += gPoint;
                    }
                }
                gpa = Math.Round(Convert.ToDecimal(totalpoint / count), 2);
                totalpoint += banglaGPA + englishGpa;

                decimal obtainMarks = 0;
                string gradePoint = string.Empty;
                int rowIndex = 0;
                rowIndex = (Int32)(dtSubjectWiseMarks.Rows.Count / 2);
                if (flag)
                {
                    dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = "0.00";
                    cgpa = 0.00;
                    letterGrade = "F";
                }

                else
                {
                    dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = gpa.ToString("0.00");
                    cgpa = totalpoint / (dtSubjectWiseMarks.Rows.Count + 2);
                    letterGrade = GetTestCgpaGrade(cgpa);
                }

            }
            double total = 0;
            total = totalNum + banglaTotal + englishTotal;

            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                reportDataSet.Tables.Add(dtSubjectWiseMarks);
                rptDocument.SetDataSource(reportDataSet);
                rptDocument.SetParameterValue("BANGLAI", b1);
                rptDocument.SetParameterValue("BANGLAII", b2);
                rptDocument.SetParameterValue("ENGLISHI", e1);
                rptDocument.SetParameterValue("ENGLISHII", e2);
                rptDocument.SetParameterValue("ENGLISHCGPA", englishGpa);
                rptDocument.SetParameterValue("BANGLACGPA", banglaGPA);
                rptDocument.SetParameterValue("ENGLISHGRADE", englishLetterGrade);
                rptDocument.SetParameterValue("BANGLAGRADE", banglaLetterGrade);
                rptDocument.SetParameterValue("BANGLAIBASEMARK", b1baseMarks);
                rptDocument.SetParameterValue("BANGLAIIBASEMARK", b2baseMarks);
                rptDocument.SetParameterValue("ENGLISHIBASEMARK", e1baseMarks);
                rptDocument.SetParameterValue("ENGLISHIIBASEMARK", e2baseMarks);
                rptDocument.SetParameterValue("BANGLAISUBMARK", b1subMark);
                rptDocument.SetParameterValue("ENGLISHISUBMARK", e1subMark);
                rptDocument.SetParameterValue("BANGLAIMONTHLY", b1Monthly);
                rptDocument.SetParameterValue("ENGLISHIMONTHLY", e1Monthly);
                rptDocument.SetParameterValue("BANGLAIIMONTHLY", b2Monthly);
                rptDocument.SetParameterValue("ENGLISHIIMONTHLY", e2Monthly);
                rptDocument.SetParameterValue("BANGLAIOBJMARK", b1objMark);
                rptDocument.SetParameterValue("BANGLAIISUBMARK", b2subMark);
                rptDocument.SetParameterValue("ENGLISHIISUBMARK", e2subMark);
                rptDocument.SetParameterValue("BANGLAIIOBJMARK", b2objMark);
                rptDocument.SetParameterValue("BANGLATOTAL", banglaTotal);
                rptDocument.SetParameterValue("ENGLISHTOTAL", englishTotal);
                rptDocument.SetParameterValue("BANGLAPERCENT", banglaAVG);
                rptDocument.SetParameterValue("ENGLISHPERCENT", englishAVG);
                rptDocument.SetParameterValue("TOTALNUMBER", total);
                rptDocument.SetParameterValue("LETTERGRADE", letterGrade);
                rptDocument.SetParameterValue("GPA", cgpa);
                CRV.ReportSource = rptDocument;
            }

        }
     
       //------TestExam marksheet-----


if (report == "CollegeTestExam")
        {

           List<TestExamCollege> list = new List<TestExamCollege>();

            TestExamCollege test = new TestExamCollege();
            DataSet reportDataSet = new DataSet();
            int classId = Convert.ToInt32(type);
            int examType = Convert.ToInt32(type);
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptCollegeTestExamMarksSheet.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            DataTable dt = new dalMarks().GetStudentMarksforHigherClass(criteria);
            dt.TableName = "TestExamCollege";
            test.name = dt.Rows[0]["NameEng"].ToString();
            test.father = dt.Rows[0]["FatherNameEng"].ToString();
            test.mother = dt.Rows[0]["MotherNameEng"].ToString();
            test.classname = dt.Rows[0]["ClassName"].ToString();
            test.shift = dt.Rows[0]["ShiftName"].ToString();
            test.section = dt.Rows[0]["SectionName"].ToString();
            test.group = dt.Rows[0]["GroupName"].ToString();
            test.year = dt.Rows[0]["Year"].ToString();
            test.regNo = dt.Rows[0]["RegNo"].ToString();
            test.campus = dt.Rows[0]["CampusName"].ToString();
            test.medium = dt.Rows[0]["MediumName"].ToString();
            test.examtype = dt.Rows[0]["ExamType"].ToString();

            bool isEven = false;

            var subject1 = GetCollegeTestResult(Convert.ToInt32(dt.Rows[0]["SubjectID"].ToString()), Convert.ToInt32(dt.Rows[1]["SubjectID"].ToString()), dt.Rows[0]["SubjectName"].ToString(), dt.Rows[1]["SubjectName"].ToString(), Convert.ToDouble(dt.Rows[0]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[1]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[0]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[1]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[0]["PracticalMarks"]), Convert.ToDouble(dt.Rows[1]["PracticalMarks"]), Convert.ToDouble(dt.Rows[0]["SubjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[1]["SubjectiveFailMarks"]), Convert.ToDouble(dt.Rows[0]["ObjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[1]["ObjectiveFailMarks"]), Convert.ToDouble(dt.Rows[0]["BaseMarks"]), Convert.ToDouble(dt.Rows[1]["BaseMarks"]), Convert.ToBoolean(dt.Rows[0]["IsOptional"]), Convert.ToBoolean(dt.Rows[1]["IsOptional"]));
            subject1.name = test.name;
            subject1.father = test.father;
            subject1.mother = test.mother;
            subject1.classname = test.classname;
            subject1.shift = test.shift;
            subject1.section = test.section;
            subject1.group = test.group;
            subject1.year = test.year;
            subject1.regNo = test.regNo;
            subject1.campus = test.campus;
            subject1.medium = test.medium;
            subject1.examtype = test.examtype;

            var subject2 = GetCollegeTestResult(Convert.ToInt32(dt.Rows[2]["SubjectID"].ToString()), Convert.ToInt32(dt.Rows[3]["SubjectID"].ToString()), dt.Rows[2]["SubjectName"].ToString(), dt.Rows[3]["SubjectName"].ToString(), Convert.ToDouble(dt.Rows[2]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[3]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[2]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[3]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[2]["PracticalMarks"]), Convert.ToDouble(dt.Rows[3]["PracticalMarks"]), Convert.ToDouble(dt.Rows[2]["SubjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[3]["SubjectiveFailMarks"]), Convert.ToDouble(dt.Rows[2]["ObjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[3]["ObjectiveFailMarks"]), Convert.ToDouble(dt.Rows[2]["BaseMarks"]), Convert.ToDouble(dt.Rows[3]["BaseMarks"]), Convert.ToBoolean(dt.Rows[2]["IsOptional"]), Convert.ToBoolean(dt.Rows[3]["IsOptional"]));


            var subject3 = new TestExamCollege { firstSubject = dt.Rows[4]["SubjectName"].ToString(), firstTheory = Convert.ToDouble(dt.Rows[4]["SubjectiveMarks"]), firstObjective = Convert.ToDouble(dt.Rows[4]["ObjectiveMarks"]), firstPractical = Convert.ToDouble(dt.Rows[4]["PracticalMarks"]), firstSubjectId = Convert.ToInt32(dt.Rows[4]["SubjectId"].ToString()), subjectTotal = Convert.ToDouble(dt.Rows[4]["ConvertedMarks"]), gpa = GetTestCGPA((Convert.ToDouble(dt.Rows[4]["ConvertedMarks"]) / Convert.ToDouble(dt.Rows[4]["BaseMarks"]) * 100)), grade = GetTestGrade((Convert.ToDouble(dt.Rows[4]["ConvertedMarks"]) / Convert.ToDouble(dt.Rows[4]["BaseMarks"]) * 100)), subjectAVG = (Convert.ToDouble(dt.Rows[4]["ConvertedMarks"]) / Convert.ToDouble(dt.Rows[4]["BaseMarks"]) * 100), baseMarks = Convert.ToDouble(dt.Rows[4]["BaseMarks"]) };



            var subject4 = GetCollegeTestResult(Convert.ToInt32(dt.Rows[5]["SubjectID"].ToString()), Convert.ToInt32(dt.Rows[6]["SubjectID"].ToString()), dt.Rows[5]["SubjectName"].ToString(), dt.Rows[6]["SubjectName"].ToString(), Convert.ToDouble(dt.Rows[5]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[6]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[5]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[6]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[5]["PracticalMarks"]), Convert.ToDouble(dt.Rows[6]["PracticalMarks"]), Convert.ToDouble(dt.Rows[5]["SubjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[6]["SubjectiveFailMarks"]), Convert.ToDouble(dt.Rows[5]["ObjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[6]["ObjectiveFailMarks"]), Convert.ToDouble(dt.Rows[5]["BaseMarks"]), Convert.ToDouble(dt.Rows[6]["BaseMarks"]), Convert.ToBoolean(dt.Rows[5]["IsOptional"]), Convert.ToBoolean(dt.Rows[6]["IsOptional"]));

            var subject5 = GetCollegeTestResult(Convert.ToInt32(dt.Rows[7]["SubjectID"].ToString()), Convert.ToInt32(dt.Rows[8]["SubjectID"].ToString()), dt.Rows[7]["SubjectName"].ToString(), dt.Rows[8]["SubjectName"].ToString(), Convert.ToDouble(dt.Rows[7]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[8]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[7]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[8]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[7]["PracticalMarks"]), Convert.ToDouble(dt.Rows[8]["PracticalMarks"]), Convert.ToDouble(dt.Rows[7]["SubjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[8]["SubjectiveFailMarks"]), Convert.ToDouble(dt.Rows[7]["ObjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[8]["ObjectiveFailMarks"]), Convert.ToDouble(dt.Rows[7]["BaseMarks"]), Convert.ToDouble(dt.Rows[8]["BaseMarks"]), Convert.ToBoolean(dt.Rows[7]["IsOptional"]), Convert.ToBoolean(dt.Rows[8]["IsOptional"]));
            var subject6 = GetCollegeTestResult(Convert.ToInt32(dt.Rows[9]["SubjectID"].ToString()), Convert.ToInt32(dt.Rows[10]["SubjectID"].ToString()), dt.Rows[9]["SubjectName"].ToString(), dt.Rows[10]["SubjectName"].ToString(), Convert.ToDouble(dt.Rows[9]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[10]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[9]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[10]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[9]["PracticalMarks"]), Convert.ToDouble(dt.Rows[10]["PracticalMarks"]), Convert.ToDouble(dt.Rows[9]["SubjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[10]["SubjectiveFailMarks"]), Convert.ToDouble(dt.Rows[9]["ObjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[10]["ObjectiveFailMarks"]), Convert.ToDouble(dt.Rows[9]["BaseMarks"]), Convert.ToDouble(dt.Rows[10]["BaseMarks"]), Convert.ToBoolean(dt.Rows[9]["IsOptional"]), Convert.ToBoolean(dt.Rows[10]["IsOptional"]));
            var subject7 = GetCollegeTestResult(Convert.ToInt32(dt.Rows[11]["SubjectID"].ToString()), Convert.ToInt32(dt.Rows[12]["SubjectID"].ToString()), dt.Rows[11]["SubjectName"].ToString(), dt.Rows[12]["SubjectName"].ToString(), Convert.ToDouble(dt.Rows[11]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[12]["SubjectiveMarks"]), Convert.ToDouble(dt.Rows[11]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[12]["ObjectiveMarks"]), Convert.ToDouble(dt.Rows[11]["PracticalMarks"]), Convert.ToDouble(dt.Rows[12]["PracticalMarks"]), Convert.ToDouble(dt.Rows[11]["SubjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[12]["SubjectiveFailMarks"]), Convert.ToDouble(dt.Rows[11]["ObjectiveFailMarks"]) + Convert.ToDouble(dt.Rows[12]["ObjectiveFailMarks"]), Convert.ToDouble(dt.Rows[11]["BaseMarks"]), Convert.ToDouble(dt.Rows[12]["BaseMarks"]), Convert.ToBoolean(dt.Rows[11]["IsOptional"]), Convert.ToBoolean(dt.Rows[12]["IsOptional"]));

            list.Add(subject1);
            list.Add(subject2);
            list.Add(subject3);
            list.Add(subject4);
            list.Add(subject5);
            list.Add(subject6);
            list.Add(subject7);

            var totalGrade = list.Sum(o => o.gpa);
            var optionalMarks = list.Where(os => os.isOptional == true).Sum(os => os.gpa);
            totalGrade = (optionalMarks >= 2) ? (totalGrade - 2) : totalGrade;

            var mCGPA = (list.Where(o=>o.isOptional==false).Any(o=>o.gpa==0.00))? 0.00 : totalGrade/6;
            var mGrade = (mCGPA >= 0.00 && mCGPA < 1) ? "F" : (mCGPA >= 1 && mCGPA < 2) ? "D" : (mCGPA >= 2 && mCGPA < 3) ? "C" : (mCGPA >= 3 && mCGPA < 3.5) ? "B" : (mCGPA >= 3.5 && mCGPA < 4) ? "A-" : (mCGPA >= 4 && mCGPA < 5) ? "A" : "A+";

            dt.TableName = "TestExamCollege";
            reportDataSet.Tables.Add(dt);
            rptDocument.SetDataSource(list);
            rptDocument.SetParameterValue("Position", position);
            rptDocument.SetParameterValue("MCGPA", mCGPA);
            rptDocument.SetParameterValue("MGRADE", mGrade);

            CRV.ReportSource = rptDocument;


        }



        if (report == "mark")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";

            rptDocument.Load(Server.MapPath("~/Report/Designer/rptStudentMarks.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            DataTable dtSubjectWiseMarks = new dalMarks().GetHigherStudentMarks(criteria);  //SubjectWishMarks(criteria);
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
                bool flag = false;
                int count = 0;
                int failSubject = 0;
                double totalpoint = 0;
                decimal gpa = 0;
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
                        double gPoint = 0;
                        double.TryParse(dr["GradePoint"].ToString(), out gPoint);
                        totalpoint += gPoint;
                    }
                }
                gpa = Math.Round(Convert.ToDecimal(totalpoint / count), 2);
                decimal obtainMarks = totalMarks / dtSubjectWiseMarks.Rows.Count;
                string gradePoint = string.Empty;
                string CGPA = GetCGPA(obtainMarks, out gradePoint);
                int rowIndex = 0;
                rowIndex = (Int32)(dtSubjectWiseMarks.Rows.Count / 2);
                if (flag)
                    dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = "0.00";
                else
                    dtSubjectWiseMarks.Rows[rowIndex]["CGPA"] = gpa.ToString("0.00");
            }
            if (dtSubjectWiseMarks.Rows.Count > 0)
            {
                string headerCriteria = "er_StudentToClass.Id=" + dtSubjectWiseMarks.Rows[0]["StudentToClassId"];
                //+ " and ExamType='" + dtSubjectWiseMarks.Rows[0]["ExamType"] + "'";
                DataTable dtMarksSheetHeaderInfo = new dalMarks().GetMarksSheetHeaderInfo(headerCriteria);
                dtMarksSheetHeaderInfo.TableName = "MarksSheetHeader";
                reportDataSet.Tables.Add(dtMarksSheetHeaderInfo);

                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dtSubjectWiseMarks);
                rptDocument.SetDataSource(reportDataSet);
                CRV.ReportSource = rptDocument;


          //      rptDocument.ExportToHttpResponse
          //(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "MarksSheet");
            }
        }
        if (report == "DueStudentPayment")
        {
            var data = obj.GetStudentDuepayment(criteria).AsEnumerable();
            var schoollist = new Common().GetAll("bs_SchoolInformation");

            var s = schoollist.AsEnumerable();
            var schoolName = "";
            var code = "";
            foreach (var item in s)
            {
                schoolName = item["Name"].ToString();
                code = item["Code"].ToString();
            }
            List<rptDueStudentPayment> list = new List<rptDueStudentPayment>();
            foreach (var item in data)
            {
                var dstu = new rptDueStudentPayment();
                dstu.RegNo = item["StudentId"].ToString();
                dstu.Name = item["NameEng"].ToString();
                dstu.DueAmount = item["DueAmount"].ToString();
                dstu.Amount = item["Amount"].ToString();
                dstu.TotalGiven = item["TotalGiven"].ToString();
                dstu.Year = item["Year"].ToString();
                list.Add(dstu);
            }
            rptDocument.Load(Server.MapPath("~\\Report\\Designer\\DueStudentPayment.rpt").ToString());
            rptDocument.SetDataSource(list);
            rptDocument.SetParameterValue("schoolName", schoolName);
            rptDocument.SetParameterValue("code", code);
            CRV.ReportSource = rptDocument;
        }
        if (report == "studentList")
        {
             DateTime serverTime = DateTime.Now;

            DateTime utcTime = serverTime.ToUniversalTime(); 


            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);

            string printDate = Convert.ToString(localTime.ToShortDateString());

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            if (IsRollNoUse == "False")
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptRegNoStudentList.rpt").ToString());
            else
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptStudentList.rpt").ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtStudent = obj.GetStudentInfoByCriteria(criteria);
            dtStudent.TableName = "Student";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("PrintDate", printDate);
            CRV.ReportSource = rptDocument;
        }
        if (report == "AllAssignstudentList")
        {

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            if (IsRollNoUse == "False")
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptRegNoAssignStudentList.rpt").ToString());
            else
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptAssignStudentList.rpt").ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            //DataTable dtStudent = obj.GetAllAssignStudentInfoByCriteria();
            DataTable dtStudent = obj.GetStudentInfoByCriteria(criteria);
            dtStudent.TableName = "Student";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            CRV.ReportSource = rptDocument;
        }

   if (report == "NotPromotedStudenList")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptNotPromotedStudentList.rpt").ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtStudent = objStudent.GetStudentForPromotion(criteria);
            dtStudent.TableName = "Student";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("Year", type);
            CRV.ReportSource = rptDocument;
        }


        if (report == "UnassignstudentList")
        {
            int year = Convert.ToInt32(Session["Year"].ToString());
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptUnassignstudentList.rpt").ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtUStudent = obj.GetUnassignStudent(year);
            dtUStudent.TableName = "UnassignStudent";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtUStudent);
            rptDocument.SetDataSource(reportDataSet);
            CRV.ReportSource = rptDocument;
        }
 if (report == "AllInActiveStudentList")
        {
            DateTime serverTime = DateTime.Now;

            DateTime utcTime = serverTime.ToUniversalTime();

            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);

            string printDate = Convert.ToString(localTime.ToShortDateString());

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptAllInActivestudentList.rpt").ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtStudent = obj.GetStudentInfoByCriteria(criteria);
            dtStudent.TableName = "Student";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("PrintDate", printDate);
            CRV.ReportSource = rptDocument;
        }

        if (report == "StudentDetails")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptStudentDetails.rpt"));

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtStudentDetails = obj.GetStudentDetails(criteria);
            dtStudentDetails.TableName = "StudentDetails";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudentDetails);
            rptDocument.SetDataSource(reportDataSet);
            CRV.ReportSource = rptDocument;
        }
        if (report == "TeacherAttendence")
        {
            var schoollist = new Common().GetAll("bs_SchoolInformation");

            var s = schoollist.AsEnumerable();
            var schoolName = "";
            var code = "";
            foreach (var item in s)
            {
                schoolName = item["Name"].ToString();
                code = item["Code"].ToString();
            }
            int teacher = Convert.ToInt32(Session["Teacher"]);
            string year = Session["Year"].ToString();
            string month = Session["Month"].ToString();
            var data = dlTeacher.GetTeacherAttendenceByTeacherID(teacher, year, month);
            var q = data.AsEnumerable();
            List<rptTeacherAttendance> list = new List<rptTeacherAttendance>();
            foreach (var item in q)
            {
                var rt = new rptTeacherAttendance();
                rt.Name = item["NameEng"].ToString();
                rt.Month = item["Month"].ToString();
                if (rt.Month == "1")
                {
                    rt.Month = "January";
                }
                if (rt.Month == "2")
                {
                    rt.Month = "February";
                }
                if (rt.Month == "3")
                {
                    rt.Month = "March";
                }
                if (rt.Month == "4")
                {
                    rt.Month = "April";
                }
                if (rt.Month == "5")
                {
                    rt.Month = "May";
                }
                if (rt.Month == "6")
                {
                    rt.Month = "June";
                }
                if (rt.Month == "7")
                {
                    rt.Month = "July";
                }
                if (rt.Month == "8")
                {
                    rt.Month = "August";
                }
                if (rt.Month == "9")
                {
                    rt.Month = "September";
                }
                if (rt.Month == "10")
                {
                    rt.Month = "October";
                }
                if (rt.Month == "11")
                {
                    rt.Month = "November";
                }
                if (rt.Month == "12")
                {
                    rt.Month = "December";
                }
                rt.Intime = item["InTime"].ToString();
                rt.Outtime = item["OutTime"].ToString();
                rt.Date = Convert.ToDateTime(item["Date"].ToString()).ToString("dd/MM/yyyy");

                list.Add(rt);
            }
            rptDocument.Load(Server.MapPath("~\\Report\\Designer\\TeacherAttendence.rpt").ToString());
            rptDocument.SetDataSource(list);
            rptDocument.SetParameterValue("SchoolName", schoolName);
            rptDocument.SetParameterValue("SchoolCode", code);
            CRV.ReportSource = rptDocument;
        }
        if (report == "collectionPayment")
        {
            DataSet reportDataSet = new DataSet();
            decimal amount = 0;
            decimal totalAmount = 0;
            decimal totalReceive = 0;
            decimal totalDue = 0;
            decimal schoolReceived = 0;
            decimal collegeReceived = 0;
            string amountWord = "";

            DateTime serverTime = DateTime.Now;

            DateTime utcTime = serverTime.ToUniversalTime(); 


            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);

            string printDate = Convert.ToString(localTime.ToShortDateString());

           

            dtSchool.TableName = "School";
            DataTable dtTypeCollector = TypeWiseCollection();
            if (monthId == "1")
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptCollectionPayment.rpt").ToString());
            else
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptSummaryCollection.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dt = new dalPayment().GetPaymentHistoryByCriteria(criteria);
            if (monthId == "1")
            {
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["PaymentType"].ToString() == "Monthly Fee")
                        {
                            row["PaymentType"] = row["MonthYear"];
                        }
                        amount += Convert.ToDecimal(row["TotalGiven"]);
                        if (row["ClassName"].ToString().ToUpper().Contains("COLLEGE"))
                        {
                            collegeReceived += Convert.ToDecimal(row["TotalGiven"]);
                        }
                        else
                        {
                            schoolReceived += Convert.ToDecimal(row["TotalGiven"]);
                        }
                    }

                    var love = dt.AsEnumerable().GroupBy(r => new { Id = r["StudentToClassId"], PaymentType = r["PaymentTypeId"], MonthYear = r["MonthYear"] })
               .Select(
                   p =>
                       new
                       {
                           Id = p.Key.Id,
                           GivenAmount = p.Sum(o => (decimal)o["TotalGiven"]),
                           Amount = p.Max(o => o["Amount"])
                       }).ToList();

                    foreach (var item in love)
                    {
                        totalAmount += Convert.ToDecimal(item.Amount);
                        totalReceive += Convert.ToDecimal(item.GivenAmount);
                    }
                }
            }
            else
            {
                var s = dt.AsEnumerable()
             .GroupBy(r => r["MonthYear"])
             .Select(p => new { MonthYear = p.Key, Sum = p.Sum(r => (decimal)r["TotalGiven"]) })
             .ToList();
                foreach (var item in s)
                {
                    dtTypeCollector.Rows.Add(item.MonthYear, dt.Rows[0]["PaymentType"], dt.Rows[0]["CampusName"], dt.Rows[0]["Year"], item.Sum);
                }
            }

            totalDue = totalAmount - totalReceive;

            amountWord = "Total Collection : " + NumberToWords(Convert.ToInt32(totalAmount)) + " Taka.";

            reportDataSet.Tables.Add(dtSchool);


            if (monthId == "1")
            {
                dt.TableName = "StudentPayment";
                reportDataSet.Tables.Add(dt);
                rptDocument.SetDataSource(reportDataSet);
                rptDocument.SetParameterValue("AmountWord", amountWord);
                rptDocument.SetParameterValue("ReceivedBy", selectId);
                rptDocument.SetParameterValue("TotalAmount", totalAmount);
                rptDocument.SetParameterValue("TotalDue", totalDue);
                rptDocument.SetParameterValue("Type", type);
                rptDocument.SetParameterValue("SchoolAmount", schoolReceived);
                rptDocument.SetParameterValue("CollegeAmount", collegeReceived);
                rptDocument.SetParameterValue("PrintDate", printDate);
            }
            else
            {
               dtTypeCollector.TableName = "StudentPayment";
                reportDataSet.Tables.Add(dtTypeCollector);
                rptDocument.SetDataSource(reportDataSet);
                rptDocument.SetParameterValue("ReceivedBy", selectId);
                rptDocument.SetParameterValue("PrintDate", printDate);
                rptDocument.SetParameterValue("Type", type);
            }

            CRV.ReportSource = rptDocument;
        }
        if (report == "duePayment")
        {
            int totalDue = 0;
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptDuePayment.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            //DataTable dts = new dalStudent().GetStudentByCriteria(criteria);
            DataTable dt = new dalPayment().GetStudentDueByCriteria(criteria);
            dt.Columns.Add("AmountWord");
            dt.TableName = "StudentPayment";
            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dt);
            rptDocument.SetDataSource(reportDataSet);
            foreach (DataRow row in dt.Rows)
            {
                totalDue += Convert.ToInt32(row["DueAmount"]);
            }
            dt.Rows[0]["AmountWord"] = "Total Due : " + NumberToWords(totalDue) + "Taka Only.";
            rptDocument.SetParameterValue("AmountWord", AmountWord);
            CRV.ReportSource = rptDocument;
        }
        if (report == "income")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptIncome.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dt = new dalAccount().GetIncome();
            dt.TableName = "Income";
            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dt);
            rptDocument.SetDataSource(reportDataSet);
            //rptDocument.SetParameterValue("AmountWord", AmountWord);
            CRV.ReportSource = rptDocument;
        }
        if (report == "expense")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptExpense.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dt = new dalAccount().GetExpense();
            dt.TableName = "Expense";
            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dt);
            rptDocument.SetDataSource(reportDataSet);
            //rptDocument.SetParameterValue("AmountWord", AmountWord);
            CRV.ReportSource = rptDocument;
        }
        if (report == "StudentPayment")
        {
            int totalPaid = 0;
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptStudentPayment.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            string billNo = type;
            DataTable dt = new dalPayment().GetPaymentHistoryByBillNo(billNo);
            dt.Columns.Add("AmountWord");
            dt.TableName = "StudentPayment";
            foreach (DataRow row in dt.Rows)
            {
                totalPaid += Convert.ToInt32(row["TotalGiven"]);
            }
            dt.Rows[0]["AmountWord"] = "Total Paid : " + NumberToWords(totalPaid) + "Taka Only.";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dt);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("AmountWord", dt.Rows[0]["AmountWord"]);
            CRV.ReportSource = rptDocument;
        }
        if (report == "testimonial")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptTestimonial.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            DataTable dt = new dalStudent().GetStudentByCriteria(criteria);
           


            if (dt.Rows.Count > 0)
            {
                dt.TableName = "Marksheet";
                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dt);
                rptDocument.SetDataSource(reportDataSet);
                CRV.ReportSource = rptDocument;
            }
        }
        if (report == "classWiseStudentDueList")
        {
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptClassWiseStudentDue.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            
            DataTable dtStudent = new dalStudent().GetByCriteria(criteria);

            DataTable dtDue= LoadStudentAllPayment(dtStudent);


           if (dtDue.Rows.Count > 0)
            {
                dtDue.TableName = "StudentPayment";
                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dtDue);
                rptDocument.SetDataSource(reportDataSet);
                CRV.ReportSource = rptDocument;
            }
        }

        if (report == "StudentReportHistory")
        {
            DateTime serverTime = DateTime.Now;

            DateTime utcTime = serverTime.ToUniversalTime();


            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);

            string printDate = localTime.ToString("d");

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptDueStudentPaymentHistory.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            string RegNo = "";

            DataTable dtStudent = new dalStudent().GetByCriteria(criteria);
            if (dtStudent.Rows.Count>0)
            {
                
                
                DataTable dt = objPayment.GetPaymentHistoryByStudentSubjectId(Convert.ToInt32(type), Convert.ToInt32(selectId));
                dt.Columns.Add("RegNo");
                dt.Columns.Add("CampusName");

                foreach (DataRow row in dt.Rows)
                {
                    row["RegNo"] = dtStudent.Rows[0]["RegNo"].ToString();
                    row["CampusName"] = dtStudent.Rows[0]["CampusName"].ToString();
                }
               

            if (dt.Rows.Count > 0)
            {
                dt.TableName = "StudentPayment";

                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dt);
                rptDocument.SetDataSource(reportDataSet);
                rptDocument.SetParameterValue("Medium", anonymusType);
                rptDocument.SetParameterValue("PrintDate", printDate);
                rptDocument.SetParameterValue("TotalAmount", ttlAmount);
                rptDocument.SetParameterValue("TotalRcv", ttlRcv);
                rptDocument.SetParameterValue("TotalDue", ttlDue);
                CRV.ReportSource = rptDocument;
            }

            }
          
            
            
        
        }


        if (report == "DueStudentPaymentList")
        {
            DateTime serverTime = DateTime.Now;

            DateTime utcTime = serverTime.ToUniversalTime();


            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);

            string printDate = localTime.ToString("d");


            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptDuePayment.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            DataTable dt = new DataTable();

            if (selectId=="Monthly")
            {
                dt = LoadStudentPaymentDue(criteria, type, monthId);
            }
            else
            {
                dt = LoadStudentPayment(criteria,type,selectId);
            }


            if (dt.Rows.Count > 0)
            {

                dt.TableName = "StudentPayment";
                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dt);
                rptDocument.SetDataSource(reportDataSet);
                rptDocument.SetParameterValue("PrintDate", printDate);
                CRV.ReportSource = rptDocument;
            }
        }

        if (report == "DueAllStudentPaymentMonthly")
        {
        
            DateTime serverTime =  DateTime.Now;      

            DateTime utcTime = serverTime.ToUniversalTime();
      

            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);

            string printDate = localTime.ToString("d");

           

           
          
            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            rptDocument.Load(Server.MapPath("~/Report/Designer/rptDueAllStudentPayment.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            DataTable dt = LoadStudentPaymentType(criteria, type, selectId);

            if (dt.Rows.Count > 0)
            {

                dt.TableName = "StudentPayment";
                reportDataSet.Tables.Add(dtSchool);
                reportDataSet.Tables.Add(dt);                
                rptDocument.SetDataSource(reportDataSet);
                rptDocument.SetParameterValue("PrintDate", printDate);
                CRV.ReportSource = rptDocument;
            }
        }
       
   if (report == "notPromotedlist")
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();
            TimeZoneInfo bd = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bd);
            string printDate = Convert.ToString(localTime.ToShortDateString());

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
                rptDocument.Load(Server.MapPath("~/Report/Designer/rptNotPromoted.rpt").ToString());

            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            DataTable dtStudent = objStudent.GetStudentForPromotion(criteria);
            dtStudent.TableName = "Student";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStudent);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("PrintDate", printDate);
            CRV.ReportSource = rptDocument;

        }


 if (report == "AdmissionInformation")
        {
            DataSet reportDataSet = new DataSet();
            int studentCount = 0;
            int prvStudentCount = 0;
            int boys = 0;
            int newAdmission = 0;
            int girls = 0;
            int others = 0;
            int science = 0;
            int commerce = 0;
            int none = 0;
            int banglaVersion = 0;
            int englishVersion = 0;
            int englishMedium = 0;
            int totalStudent = 0;
            string year = "";
            string campusName = type;
            string medium = selectId;
            DataTable dt = AdmissionDt();
            DataTable dts = PreviousYearAdmission();
            dtSchool.TableName = "School";
            dt.TableName = "AdmissionReport";
            dts.TableName = "PreviousYearAdmission";
            DataTable dtStudent = new dalStudent().GetAllAdmittedStudentInfo(criteria);
            DataTable dtPrevious = new dalStudent().GetAllAdmittedStudentInfo(anonymusType);

            if (dtStudent.Rows.Count > 0)
            {
                var q = dtStudent.AsEnumerable()
          .GroupBy(r => r["ClassName"])
          .Select(p => new
          {
              ClassName = p.Key,
              StuCount = p.Count(r => r["RegNo"].ToString() != ""),
              NewAdmission = p.Count(r => r["AdmissionYear"].ToString() == DateTime.Now.Year.ToString()),
              Boys = p.Count(r => r["Gender"].ToString() == "Male"),
              Girls = p.Count(r => r["Gender"].ToString() == "Female"),
              Others = p.Count(r => r["Gender"].ToString() == "Other"),
              Science = p.Count(r => r["GroupId"].ToString() == "2"),
              Commerce = p.Count(r => r["GroupId"].ToString() == "3"),
              None = p.Count(r => r["GroupId"].ToString() == "4"),
              BanglaVersion = p.Count(r => r["MediumId"].ToString() == "1"),
              EnglishVersion = p.Count(r => r["MediumId"].ToString() == "2"),
              EnglishMedium = p.Count(r => r["MediumId"].ToString() == "4")
          })
          .ToList();
                foreach (var item in q)
                {
                    dt.Rows.Add(item.ClassName, item.StuCount, item.NewAdmission, item.Science, item.Commerce, item.None, item.Boys, item.Girls, item.Others, item.BanglaVersion, item.EnglishVersion, item.EnglishMedium);
                    studentCount += Convert.ToInt32(item.StuCount);
                    newAdmission += Convert.ToInt32(item.NewAdmission);
                    boys += Convert.ToInt32(item.Boys);
                    girls += Convert.ToInt32(item.Girls);
                    others += Convert.ToInt32(item.Others);
                    science += Convert.ToInt32(item.Science);
                    commerce += Convert.ToInt32(item.Commerce);
                    none += Convert.ToInt32(item.None);
                    banglaVersion += Convert.ToInt32(item.BanglaVersion);
                    englishVersion += Convert.ToInt32(item.EnglishVersion);
                    englishMedium += Convert.ToInt32(item.EnglishMedium);

                    //totalStudent +=Convert.ToInt32(item)
                }
                year = dtStudent.Rows[0]["Year"].ToString();

            }

            if (dtPrevious.Rows.Count > 0)
            {
                var q = dtPrevious.AsEnumerable()
                      .GroupBy(r => r["ClassName"])
                      .Select(p => new
                      {
                          PreviousClassName = p.Key,
                          PreviousStudent = p.Count(r => r["RegNo"].ToString() != "")
                      })
                      .ToList();
                foreach (var item in q)
                {
                    dts.Rows.Add(item.PreviousClassName, item.PreviousStudent);
                    prvStudentCount += Convert.ToInt32(item.PreviousStudent);
                }

            }

            reportDataSet.Tables.Add(dtStudent);
            reportDataSet.Tables.Add(dtPrevious);
            reportDataSet.Tables.Add(dt);
            reportDataSet.Tables.Add(dts);
            reportDataSet.Tables.Add(dtSchool);
            rptDocument.Load(Server.MapPath("../../Report/Designer/rptAdmissionReport.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);
            rptDocument.SetDataSource(reportDataSet);
            rptDocument.SetParameterValue("Student", studentCount.ToString());
            rptDocument.SetParameterValue("PrvStudent", prvStudentCount.ToString());
            rptDocument.SetParameterValue("NewAdmission", newAdmission.ToString());
            rptDocument.SetParameterValue("Boys", boys.ToString());
            rptDocument.SetParameterValue("Girls", girls.ToString());
            rptDocument.SetParameterValue("Others", others.ToString());
            rptDocument.SetParameterValue("Science", science.ToString());
            rptDocument.SetParameterValue("Commerce", commerce.ToString());
            rptDocument.SetParameterValue("CampusName", campusName);
            rptDocument.SetParameterValue("Year", year);
            rptDocument.SetParameterValue("Medium", medium);
            rptDocument.SetParameterValue("BanglaVersion", banglaVersion);
            rptDocument.SetParameterValue("EnglishVersion", englishVersion);
            rptDocument.SetParameterValue("EnglishMedium", englishMedium);
            CRV.ReportSource = rptDocument;
        }

        
    }

    private DataTable LoadStudentAdmissionInformation(string criteria)
    {
        throw new NotImplementedException();
    }


    decimal MonthlyReceived
    {
        set { ViewState["MonthlyReceived"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["MonthlyReceived"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    string AmountWord
    {
        set { ViewState["AmountWord"] = value; }
        get
        {
            try
            {
                return ViewState["AmountWord"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    string MonthYear
    {
        set { ViewState["MonthYear"] = value; }
        get
        {
            try
            {
                return ViewState["MonthYear"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    string PaymentType
    {
        set { ViewState["PaymentType"] = value; }
        get
        {
            try
            {
                return ViewState["PaymentType"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    public DataTable TotalDue()
    {
        DataTable dt = new DataTable("TotalDue");
        dt.Columns.Add("StudentId", typeof (int));
        dt.Columns.Add("NameEng", typeof(string));
        dt.Columns.Add("RegNo", typeof(string));
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("MediumName", typeof(string));
        dt.Columns.Add("CampusName", typeof(string));
        dt.Columns.Add("Amount", typeof(double));
        dt.Columns.Add("TotalGiven", typeof(double));
        dt.Columns.Add("DueAmount", typeof(double));
        dt.Columns.Add("RollNo", typeof(string));
        dt.Columns.Add("Mobile", typeof(string));
        dt.Columns.Add("Year", typeof (string));
        return dt;
    }

   public DataTable AdmissionDt()
    {
        DataTable dt = new DataTable("AdmissionDt");
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("Student", typeof(int));
        dt.Columns.Add("NewAdmission", typeof(int));
        dt.Columns.Add("Science", typeof(int));
        dt.Columns.Add("Commerce", typeof(int));
        dt.Columns.Add("None", typeof(int));
        dt.Columns.Add("Boys", typeof(int));
        dt.Columns.Add("Girls", typeof(int));
        dt.Columns.Add("Others", typeof(int));
        dt.Columns.Add("BanglaVersion", typeof(int));
        dt.Columns.Add("EnglishVersion", typeof(int));
        dt.Columns.Add("EnglishMedium", typeof(int));

        return dt;
    }

     public DataTable PreviousYearAdmission()
    {
        DataTable dt = new DataTable("PreviousYearAdmission");
        dt.Columns.Add("PreviousClassName", typeof(string));
        dt.Columns.Add("PreviousStudent", typeof(int));
        return dt;
    }

    public DataTable StudentDue()
    {
        DataTable dt = new DataTable("TotalDue");
        dt.Columns.Add("StudentId", typeof(int));
        dt.Columns.Add("RegNo", typeof(string));
        dt.Columns.Add("NameEng", typeof(string));
        dt.Columns.Add("CampusName", typeof(string));
        dt.Columns.Add("MediumName", typeof(string));
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("Year", typeof(string));
        dt.Columns.Add("Month", typeof(string));
        dt.Columns.Add("Mobile", typeof(string));
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("Amount", typeof(double));
        dt.Columns.Add("TotalGiven", typeof(double));
        dt.Columns.Add("DueAmount", typeof(double));
        
        return dt;
    }



    public DataTable DtPayment()
    {
        DataTable dtpayment = new DataTable("TotalPayment");
        dtpayment.Columns.Add("MonthId", typeof(string));
        dtpayment.Columns.Add("Month", typeof(string));
        dtpayment.Columns.Add("CampusName", typeof(string));
        dtpayment.Columns.Add("MediumName", typeof(string));
        dtpayment.Columns.Add("ClassName", typeof(string));
        dtpayment.Columns.Add("Year", typeof(string));
        //dt.Columns.Add("Month", typeof(string));
        dtpayment.Columns.Add("PaymentType", typeof(string));
        dtpayment.Columns.Add("Amount", typeof(double));
        dtpayment.Columns.Add("TotalGiven", typeof(double));
        dtpayment.Columns.Add("DueAmount", typeof(double));
        return dtpayment;
    }
    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";
        if ((number / 10000000) > 0)
        {
            words += NumberToWords(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        if ((number / 100000) > 0)
        {
            words += NumberToWords(number / 100000) + " Lakh ";
            number %= 100000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    private DataTable LoadStudentAllPayment(DataTable dts)
    {

        decimal amount = 0;
        decimal totalAmount = 0;
        decimal totalDue = 0;
        int paymentTypeId = 0;

        DataTable dtTotalDue = TotalDue();
        DataTable dt = new DataTable();
        

        dts.Columns.Add("MonthYear", typeof(string));
        //DataTable dts = new dalStudent().GetByCriteria(criteria);
        for (int i = 0; dts.Rows.Count > i; i++)
        {
            decimal total = 0;
            decimal received = 0;
            decimal dueAmount = 0;
            decimal dueMonth = 0;
            MonthYear = "";
            int StudentToClassId = Convert.ToInt32(dts.Rows[i]["StudentToClassId"]);
            int mediumId = Convert.ToInt32(dts.Rows[i]["MediumId"]);
            int campusId = Convert.ToInt32(dts.Rows[i]["CampusId"]);
            int classId = Convert.ToInt32(dts.Rows[i]["ClassId"]);
            int groupId = Convert.ToInt32(dts.Rows[i]["GroupId"]);
            int shiftId = Convert.ToInt32(dts.Rows[i]["ShiftId"]);
            int sectionId = Convert.ToInt32(dts.Rows[i]["SectionId"]);
            int yearId = Convert.ToInt32(dts.Rows[i]["Year"]);
            string name = dts.Rows[i]["NameEng"].ToString();
            string regNo = dts.Rows[i]["RegNo"].ToString();
            string rollNo = dts.Rows[i]["RollNo"].ToString();
            string mobile = dts.Rows[i]["Mobile"].ToString();
            string className = dts.Rows[i]["ClassName"].ToString();
            string groupName = dts.Rows[i]["GroupName"].ToString();
            string shiftName = dts.Rows[i]["Shift"].ToString();
            string sectionName = dts.Rows[i]["Section"].ToString();
            string mediumName = dts.Rows[i]["MediumName"].ToString();
            string campusName = dts.Rows[i]["CampusName"].ToString();
            string yearName = dts.Rows[i]["YearName"].ToString();

            dt = new dalPaymentType().GetPaymentWithoutDefault(StudentToClassId, yearId, mediumId, campusId, classId, groupId, shiftId, sectionId, false,false);

            if (dt.Rows.Count > 0)
            {
                int monthId = 0;
                MonthlyReceived = 0;
                int year = DateTime.Now.Year;
                int StartMonth = Convert.ToInt32(dt.Rows[0]["StartMonth"]);
                int EndMonth = Convert.ToInt32(dt.Rows[0]["EndMonth"]);
                if (StartMonth < EndMonth)
                {
                    for (int j = StartMonth; EndMonth + 1 > j; j++)
                    {
                        monthId = j;
                        dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId,yearId);
                    }
                }
                else
                {
                    for (int j = StartMonth; 13 > j; j++)
                    {
                        monthId = j;
                        dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId,yearId);
                    }
                    for (int j = 1; EndMonth + 1 > j; j++)
                    {
                        monthId = j;
                        year = DateTime.Now.Year + 1;
                        dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId,yearId);
                    }
                }
                total += dueMonth;
                received += MonthlyReceived;
                foreach (DataRow row in dt.Rows)
                {

                    amount = Convert.ToDecimal(row["Amount"]);
                    total += (amount);
                    paymentTypeId = Convert.ToInt32(row["PaymentTypeId"]);
                    DataTable dtPaid = new dalPayment().GetOthersPayment(StudentToClassId, yearId, classId, paymentTypeId);

                    if (dtPaid.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtPaid.Rows)
                        {
                            dueAmount = Convert.ToDecimal(rows["DueAmount"]);
                            received += Convert.ToDecimal(rows["TotalGiven"]);
                        }
                    }
                }

                totalAmount += total;
                
                    dtTotalDue.Rows.Add(StudentToClassId, name, regNo, className, mediumName, campusName, total, received, (total - received), rollNo, mobile,yearName);
                    totalDue += (total - received);
                

            }

            if (MonthYear != "")
                dts.Rows[i]["MonthYear"] = MonthYear;
            else
                dts.Rows[i]["MonthYear"] = "No Monthly Payment";
        }
        if (dtTotalDue.Rows.Count > 0)
        {
            dtTotalDue.Columns.Add("TotalAmount", typeof(double));
            dtTotalDue.Rows[0]["TotalAmount"] = totalAmount;
        }
        AmountWord = "Total Dues : " + NumberToWords(Convert.ToInt32(totalDue)) + " Taka.";
        return dtTotalDue;

    }
    private decimal LoadMonthRpt(int StudentToClassId, int year, int mediumId, int campusId, int classId, int groupId, int shiftId, int sectionId, int monthId,int yearId)
    {
        string month = "";
        int paymentTypeId = 0;
        decimal amount = 0;
        decimal dueMonth = 0;
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        DataTable dtMonthly = new dalPaymentType().GetPaymentTypeByIsMonthly(true);

         foreach (DataRow row in dtMonthly.Rows)
         {
             paymentTypeId = Convert.ToInt32(row["Id"]);
             DataTable dtMonth = new dalPayment().GetMonthById(monthId);
             DataTable dtMonthlyFee = new dalPayment().GetMonthlyFees(StudentToClassId, yearId, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);

             if (dtMonthlyFee.Rows.Count > 0)
             {
                 month = dtMonth.Rows[0]["Month"].ToString();
                 amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
                 paymentTypeId = Convert.ToInt32(dtMonthlyFee.Rows[0]["PaymentTypeId"]);
                 if (amount != 0)
                 {
                     decimal totalReceive = 0;
                     DataTable dtPaid = new dalPayment().GetMonthlyPayment(StudentToClassId, yearId, classId, paymentTypeId, monthId);
                     if (dtPaid.Rows.Count > 0)
                     {
                         decimal dueAmount = 0;
                         foreach (DataRow rows in dtPaid.Rows)
                         {
                             if (currentMonth >= Convert.ToInt32(monthId))
                                 dueAmount = Convert.ToDecimal(rows["Amount"]);
                             totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                         }
                     }
                     if (currentMonth >= monthId && year == currentYear)
                     {
                        
                     }
                     dueMonth += amount;
                         MonthlyReceived += totalReceive;

                 }

             }
         }
        return dueMonth;
    }
    private string GetLetterGrade(decimal totalPoint, out string gradePoint)
    {
        string gradeName = string.Empty;
        gradePoint = string.Empty;
        DataTable dtGrade = new Common().GetAll("rs_Grade");
        foreach (DataRow drg in dtGrade.Rows)
        {

            if ((totalPoint >= Convert.ToDecimal(drg["StartPoint"])) && (totalPoint <= Convert.ToDecimal(drg["EndPoint"])))
            {
                gradeName = drg["GradeName"].ToString();
                gradePoint = drg["GradePoint"].ToString();
                Remarks = drg["Remarks"].ToString();
            }
        }

        return gradeName;
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

        return gradePoint;
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
            double baseMarks = Convert.ToDouble(dr["BaseMarks"]);
           // double totalMarks = Convert.ToDouble(dr["ConvertedMarks"]);
            double totalMarks =(Convert.ToDouble(dr["ConvertedMarks"]) / baseMarks) * 100;
            if ((totalMarks >= Convert.ToDouble(drg["StartMarks"])) && (totalMarks < Convert.ToDouble(drg["EndMarks"]) + 1))
            {
                dr["GradeLetter"] = drg["GradeName"].ToString();
                dr["GradePoint"] = drg["GradePoint"].ToString();
            }
        }
    }
    private DataTable LoadStudentPaymentDue(string criteria, string paymentTypeId, string monthId)
    {
        
        decimal total = 0;
        decimal totalGiven = 0;
        decimal totalDue = 0;
        
        DataTable dtDue = StudentDue();
       

        DataTable dtStudent = new dalStudent().GetByCriteria(criteria);
        DataTable dtPaymentType = new dalPaymentType().GetById(Convert.ToInt32(paymentTypeId));
        bool isMonthly = Convert.ToBoolean(dtPaymentType.Rows[0]["IsMonthly"]);
        criteria +=" and ss_Payment.PaymentTypeId=" + paymentTypeId;
        if (isMonthly)
        {
            criteria += " and ss_Payment.Month=" + monthId;
        }

        DataTable dtPayment = new dalPayment().GetStudentDueByCriteria(criteria);  
        DataTable dtMonth = new dalYear().GetMonthById(Convert.ToInt32(monthId));
        string month = dtMonth.Rows[0]["Month"].ToString();

        if (dtPaymentType.Rows.Count > 0)
        {
            if (dtPaymentType.Rows[0]["IsMonthly"].ToString() == "False")
                monthId = "0";
        }

        if (dtStudent.Rows.Count > 0)
        {
            foreach (DataRow item in dtStudent.Rows)
            {
                

                string studentToClassId = item["StudentToClassId"].ToString();
                string regNo = item["RegNo"].ToString();
                string nameEng = item["NameEng"].ToString();
                string campusName = item["CampusName"].ToString();
                string mediumName = item["MediumName"].ToString();
                string className = item["ClassName"].ToString();

                string yearId = item["Year"].ToString();
                string mediumId = item["MediumId"].ToString();
                string campusId = item["CampusId"].ToString();
                string classId = item["ClassId"].ToString();
                string yearName = item["YearName"].ToString();
                string mobile = item["Mobile"].ToString();
                decimal monthlyAmount = 0;
                decimal monthlyGiven = 0;


                DataTable dtMonthlyDue = new dalPayment().GetMonthlyDues(Convert.ToInt32(item["StudentToClassId"]),
                          Convert.ToInt32(yearId), Convert.ToInt32(mediumId),
                          Convert.ToInt32(campusId), Convert.ToInt32(classId),
                          Convert.ToInt32(monthId), Convert.ToInt32(paymentTypeId));

                if (dtMonthlyDue.Rows.Count > 0)
                {
                    monthlyAmount = Convert.ToDecimal(dtMonthlyDue.Rows[0]["Amount"]);

                    foreach (DataRow dataRow in dtPayment.Rows)
                    {
                        if (studentToClassId == dataRow["StudentToClassId"].ToString())
                        {
                            monthlyGiven += Convert.ToDecimal(dataRow["TotalGiven"]);
                        }
                    }

                }

                dtDue.Rows.Add(studentToClassId, regNo, nameEng, campusName, mediumName, className, yearName, month, mobile, dtMonthlyDue.Rows[0]["PaymentType"], monthlyAmount, monthlyGiven, monthlyAmount - monthlyGiven);
            }
            
        }

        return dtDue;

    }
    private DataTable LoadStudentPayment(string criteria, string paymentTypeIds, string selectedId)
    {

        decimal amount = 0;
        decimal totalAmount = 0;
        decimal totalDue = 0;
        decimal totalReceived = 0;

        int paymentTypeId = Convert.ToInt32(paymentTypeIds);
        
        DataTable dtTotalDue = StudentDue();

        DataTable dtAcademicPayment = new DataTable();

        DataTable dtStudent = new dalStudent().GetByCriteria(criteria);

        for (int i = 0; dtStudent.Rows.Count > i; i++)
        {

            decimal total = 0;
            decimal received = 0;
            decimal dueAmount = 0;
            decimal dueMonth = 0;
            int studentToClassId = Convert.ToInt32(dtStudent.Rows[i]["StudentToClassId"]);
            int classId = Convert.ToInt32(dtStudent.Rows[i]["ClassId"]);
            int mediumId = Convert.ToInt32(dtStudent.Rows[i]["MediumId"]);
            int campusId = Convert.ToInt32(dtStudent.Rows[i]["CampusId"]);
            int groupId = Convert.ToInt32(dtStudent.Rows[i]["GroupId"]);
            int shiftId = Convert.ToInt32(dtStudent.Rows[i]["ShiftId"]);
            int sectionId = Convert.ToInt32(dtStudent.Rows[i]["SectionId"]);
            int yearId = Convert.ToInt32(dtStudent.Rows[i]["Year"]);
            string name = dtStudent.Rows[i]["NameEng"].ToString();
            string regNo = dtStudent.Rows[i]["RegNo"].ToString();
            string className = dtStudent.Rows[i]["ClassName"].ToString();
            string campusName = dtStudent.Rows[i]["CampusName"].ToString();
            string mediumName = dtStudent.Rows[i]["MediumName"].ToString();
            string yearName = dtStudent.Rows[i]["YearName"].ToString();
            string mobile = dtStudent.Rows[i]["Mobile"].ToString();

            dtAcademicPayment = new dalPaymentType().GetPaymentWithoutDefault(studentToClassId, Convert.ToInt32(yearId), mediumId, campusId, classId, groupId, shiftId, sectionId, false, false);


            if (dtAcademicPayment.Rows.Count > 0)
            {
                int monthId = 0;
                MonthlyReceived = 0;
                int year = DateTime.Now.Year;
                int StartMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["StartMonth"]);
                int EndMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["EndMonth"]);
                if (StartMonth < EndMonth)
                {
                    for (int j = StartMonth; EndMonth + 1 > j; j++)
                    {
                        monthId = j;
                        dueMonth += LoadMonth(studentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId,selectedId,yearId);
                    }
                }
                else
                {
                    for (int j = StartMonth; 13 > j; j++)
                    {
                        monthId = j;
                        dueMonth += LoadMonth(studentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId, selectedId, yearId);
                    }
                    for (int j = 1; EndMonth + 1 > j; j++)
                    {
                        monthId = j;
                        year = DateTime.Now.Year + 1;
                        dueMonth += LoadMonth(studentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId, selectedId, yearId);
                    }
                }
                total += dueMonth;
                received += MonthlyReceived;

                totalAmount += total;

                dtTotalDue.Rows.Add(studentToClassId, regNo, name, campusName, mediumName, className, yearName,selectedId, mobile , PaymentType, total, received, total - received);
                totalDue += (total - received);
                totalReceived += received;

            }
        }

        return dtTotalDue;

    }

    private decimal LoadMonth(int StudentToClassId, int year, int mediumId, int campusId, int classId, int groupId, int shiftId, int sectionId, int monthId, int paymentTypeId,string selectedId, int yearId)
    {
        string month = "";

        decimal amount = 0;
        decimal dueMonth = 0;
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        DataTable dtMonth = new dalPayment().GetMonthById(monthId);
        DataTable dtMonthlyFee = new dalPayment().GetMonthlyFees(StudentToClassId, Convert.ToInt32(yearId), mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);

        if (dtMonthlyFee.Rows.Count > 0)
        {
            month = dtMonth.Rows[0]["Month"].ToString();
            amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
            paymentTypeId = Convert.ToInt32(dtMonthlyFee.Rows[0]["PaymentTypeId"]);
            PaymentType = dtMonthlyFee.Rows[0]["PaymentType"].ToString();

            if (amount != 0)
            {
                decimal totalReceive = 0;
                DataTable dtPaid = new dalPayment().GetMonthlyPayment(StudentToClassId, Convert.ToInt32(yearId), classId, paymentTypeId, monthId);
                if (dtPaid.Rows.Count > 0)
                {
                    decimal dueAmount = 0;
                    foreach (DataRow rows in dtPaid.Rows)
                    {
                        dueAmount = Convert.ToDecimal(rows["Amount"]);
                        totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                    }
                }
                if (selectedId == "Up To Current Month")
                {
                    if (currentMonth >= monthId && year == currentYear)
                    {
                        dueMonth += amount;
                        MonthlyReceived += totalReceive;
                    }
                }
                else
                {
                    dueMonth += amount;
                    MonthlyReceived += totalReceive;
                }
            }
        }

        return dueMonth;
    }

    private decimal LoadAllStudentMonthlyPayment(DataTable dtStudent, int monthId, int paymentTypeId, int year, DataTable dt,string className)
    {
        string month = "";
        
        string campusName = "";
        string mediumName = "";
        string paymentType = "";

        decimal dueMonth = 0;
        decimal monthlyReceived = 0;
        foreach (DataRow item in dtStudent.Rows)
        {
            int studentToClassId = Convert.ToInt32(item["StudentToClassId"]);
            int classId = Convert.ToInt32(item["ClassId"]);
            int mediumId = Convert.ToInt32(item["MediumId"]);
            int campusId = Convert.ToInt32(item["CampusId"]);
            int groupId = Convert.ToInt32(item["GroupId"]);
            int shiftId = Convert.ToInt32(item["ShiftId"]);
            int sectionId = Convert.ToInt32(item["SectionId"]);
            //className = item["ClassName"].ToString();
            campusName = item["CampusName"].ToString();
            mediumName = item["MediumName"].ToString();
            int yearId = Convert.ToInt32(dtStudent.Rows[0]["Year"]);

            decimal amount = 0;

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;           
            DataTable dtMonthlyFee = new dalPayment().GetMonthlyFees(studentToClassId,yearId, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);

            if (monthId != 0)
            {
                DataTable dtMonth = new dalPayment().GetMonthById(monthId);
                month = dtMonth.Rows[0]["Month"].ToString();
            }
            else
            {
                month = "Academic";
            }

            //if (dtMonthlyFee.Rows.Count > 0)
            //{
            //    month = dtMonth.Rows[0]["Month"].ToString();
            //    amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
            //    paymentType = dtMonthlyFee.Rows[0]["PaymentType"].ToString();
            //    if (amount != 0)
            //    {
            //        decimal totalReceive = 0;
            //        DataTable dtPaid = new dalPayment().GetMonthlyPayment(studentToClassId,yearId, classId, paymentTypeId, monthId);
            //        if (dtPaid.Rows.Count > 0)
            //        {
            //            decimal dueAmount = 0;
            //            foreach (DataRow rows in dtPaid.Rows)
            //            {
            //                dueAmount = Convert.ToDecimal(rows["Amount"]);
            //                totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
            //            }
            //        }

            //        dueMonth += amount;
            //        monthlyReceived += totalReceive;
            //        //if (ddlSelect.SelectedValue == "2")
            //        //{
            //        //    if (currentMonth >= monthId && year == currentYear)
            //        //    {

            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    dueMonth += amount;
            //        //    MonthlyReceived += totalReceive;
            //        //}
            //    }
            //}

            if (dtMonthlyFee.Rows.Count > 0)
            {

                amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
                paymentType = dtMonthlyFee.Rows[0]["PaymentType"].ToString();
                if (amount != 0)
                {
                    decimal totalReceive = 0;
                    DataTable dtPaid = new dalPayment().GetMonthlyPayment(studentToClassId, yearId, classId, paymentTypeId, monthId);
                    if (monthId != 0)
                    {
                        // Monthly Paid Payment

                        dtPaid = new dalPayment().GetMonthlyPayment(studentToClassId, yearId, classId, paymentTypeId, monthId);
                    }
                    else
                    {
                        // Academic Paid Payment 
                        dtPaid = new dalPayment().GetOthersPayment(studentToClassId, yearId, classId, paymentTypeId);
                    }

                    if (dtPaid.Rows.Count > 0)
                    {
                        decimal dueAmount = 0;
                        foreach (DataRow rows in dtPaid.Rows)
                        {
                            dueAmount = Convert.ToDecimal(rows["Amount"]);
                            totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                        }
                    }

                    dueMonth += amount;
                    monthlyReceived += totalReceive;
                    //if (ddlSelect.SelectedValue == "2")
                    //{
                    //    if (currentMonth >= monthId && year == currentYear)
                    //    {

                    //    }
                    //}
                    //else
                    //{
                    //    dueMonth += amount;
                    //    MonthlyReceived += totalReceive;
                    //}
                }
            }


        }

        dt.Rows.Add(monthId, month + "-" + year, campusName, mediumName, className, year, paymentType, dueMonth, monthlyReceived, dueMonth - monthlyReceived);
        MonthlyReceived += monthlyReceived;
        return dueMonth;

    }
    private DataTable LoadStudentPaymentType(string criteria, string paymentTypeIds,string className)
    {

        decimal amount = 0;
        decimal totalAmount = 0;
        decimal totalDue = 0;
        decimal totalReceived = 0;

        int paymentTypeId = Convert.ToInt32(paymentTypeIds);

        DataTable dtPaymentType = new dalPaymentType().GetById(paymentTypeId);
        bool isMonthly = Convert.ToBoolean(dtPaymentType.Rows[0]["IsMonthly"]);

        DataTable dtTotalDue = DtPayment();


        DataTable dtStudent = new dalStudent().GetByCriteria(criteria);

        if (dtStudent.Rows.Count > 0)
        {
            decimal total = 0;
            decimal received = 0;
            decimal dueAmount = 0;
            decimal dueMonth = 0;
            int studentToClassId = Convert.ToInt32(dtStudent.Rows[0]["StudentToClassId"]);
            int classId = Convert.ToInt32(dtStudent.Rows[0]["ClassId"]);
            int mediumId = Convert.ToInt32(dtStudent.Rows[0]["MediumId"]);
            int campusId = Convert.ToInt32(dtStudent.Rows[0]["CampusId"]);
            int groupId = Convert.ToInt32(dtStudent.Rows[0]["GroupId"]);
            int shiftId = Convert.ToInt32(dtStudent.Rows[0]["ShiftId"]);
            int sectionId = Convert.ToInt32(dtStudent.Rows[0]["SectionId"]);
            int yearId = Convert.ToInt32(dtStudent.Rows[0]["Year"]);
            int year = Convert.ToInt32(dtStudent.Rows[0]["YearName"]);


            DataTable dtAcademicPayment = new dalPaymentType().GetPaymentWithoutDefault(studentToClassId,yearId, mediumId, campusId, classId, groupId, shiftId, sectionId, false, false);
            if (dtAcademicPayment.Rows.Count > 0)
            {

                int monthId = 0;
                MonthlyReceived = 0;
               
                int StartMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["StartMonth"]);
                int EndMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["EndMonth"]);

                if (isMonthly)
                {
                    if (StartMonth < EndMonth)
                    {
                        for (int j = StartMonth; EndMonth + 1 > j; j++)
                        {
                            monthId = j;
                            dueMonth += LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue, className);
                        }
                    }
                    else
                    {
                        for (int j = StartMonth; 13 > j; j++)
                        {
                            monthId = j;
                            dueMonth += LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue, className);
                        }
                        for (int j = 1; EndMonth + 1 > j; j++)
                        {
                            monthId = j;
                            year = Convert.ToInt32(dtStudent.Rows[0]["YearName"]) + 1;
                            dueMonth += LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue, className);
                        }
                    }
                }
                else
                {
                    dueMonth = LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue, className);
                }
               
            }
        }

        return dtTotalDue;

    }

    public DataTable TypeWiseCollection()
    {
        DataTable dt = new DataTable("Collection");
        dt.Columns.Add("MonthYear", typeof(string));
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("CampusName", typeof(string));
        dt.Columns.Add("Year", typeof(string));
        dt.Columns.Add("TotalGiven", typeof(decimal));
        return dt;
    }

    private double GetTestCGPA(double num)
    {
        double cgpa = 0.00;

        DataTable dtGrade = new Common().GetAll("rs_Grade");

        foreach (DataRow row in dtGrade.Rows)
        {
            if ((num >= Convert.ToDouble(row["StartMarks"])) && (num < Convert.ToDouble(row["EndMarks"]) + 1))
            {
                cgpa = Convert.ToDouble(row["GradePoint"].ToString());
            }
        }
        return cgpa;
    }

    private string GetTestGrade(double num)
    {
        string grade = "";

        DataTable dtGrade = new Common().GetAll("rs_Grade");

        foreach (DataRow row in dtGrade.Rows)
        {
            if ((num >= Convert.ToDouble(row["StartMarks"])) && (num < Convert.ToDouble(row["EndMarks"]) + 1))
            {
                grade = row["GradeName"].ToString();
            }
        }
        return grade;
    }

    private string GetTestCgpaGrade(double num)
    {
        string grade = "";

        DataTable dtGrade = new Common().GetAll("rs_Grade");

        foreach (DataRow row in dtGrade.Rows)
        {
            if ((num >= Convert.ToDouble(row["StartPoint"])) && (num < Convert.ToDouble(row["EndPoint"])))
            {
                grade = row["GradeName"].ToString();
            }
        }
        return grade;
    }    
    
   private TestExamCollege GetCollegeTestResult(int sub1, int sub2, string firstSubject, string secondSubject, double firstTheory, double secondTheory, double firstObjective, double secondObjective, double firstPractical, double secondPractical, double subjectiveFailMarks, double objectiveFailMarks , double firstBaseMarks, double secondBasemarks , bool firstOptional, bool scondOptional)
    {
        TestExamCollege test = new TestExamCollege();
        double countCGPA = 0;
        double theoryMarks = 0;
        double objectiveMarks = 0;
        double passMarks = 0;
        
        string subId = "(" + sub1 + "," + sub2 + ")";
        //  string subjectId = sub1 + "," + sub2;
        test.firstSubject = firstSubject;
        test.secondSubject = secondSubject;
        test.firstBaseMarks = firstBaseMarks;
        test.secondBaseMarks = secondBasemarks;
        test.isOptional = ((firstOptional ==true) && (scondOptional ==true))? true:false;
        test.firstTheory = firstTheory;
        test.secondTheory = secondTheory;
        test.firstObjective = firstObjective;
        test.secondObjective = secondObjective;
        test.firstPractical = firstPractical;
        test.secondPractical = secondPractical;
        test.subjectiveFailMarks = subjectiveFailMarks;
        test.objectiveFailMarks = objectiveFailMarks;

        test.Highest = test.firstHighest + test.secondHighest;
        test.baseMarks = firstBaseMarks + secondBasemarks;
        test.theoryTotal = 0;
        test.objectiveTotal = 0;
        test.practicalTotal = 0;
        test.subjectTotal = 0;
        test.subjectAVG = 0;
        test.gpa = 0.00;
        test.grade = "F";
        int flag = 0;


        test.theoryTotal = firstTheory + secondTheory;
        test.objectiveTotal = firstObjective + secondObjective;
        test.practicalTotal = firstPractical + secondPractical;
        test.subjectTotal = test.theoryTotal + test.objectiveTotal + test.practicalTotal;
        test.subjectAVG = (test.subjectTotal / test.baseMarks) * 100;

        if ((test.theoryTotal < subjectiveFailMarks || test.objectiveTotal < objectiveFailMarks) && (!test.isOptional))
        {
            flag++;
        }
        else
        {
            test.grade = GetTestGrade(test.subjectAVG);
            test.gpa = GetTestCGPA(test.subjectAVG);
        }

        return test;
    }


    private DataTable GetClgTestTbl()
    {
        DataTable dt = new DataTable();
        dt.Rows.Add("StudentId");
        dt.Rows.Add("SubjectId");
        dt.Rows.Add("ExamTypeId");
        dt.Rows.Add("SubjectName");
        dt.Rows.Add("SubjectiveMarks");
        dt.Rows.Add("ObjectiveMarks");
        dt.Rows.Add("PracticalMarks");
        dt.Rows.Add("ConvertedMarks");
        dt.Rows.Add("BaseMarks");
        return dt;
    }



}





































































