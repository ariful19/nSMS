using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for dalTeacher
/// </summary>
public class dalTeacher
{
    DatabaseManager dm = new DatabaseManager();
    public dalTeacher()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Insert(int personId, int designationId, int gradeId ,int levelId, int campusId ,string TeacherPin,string empId, string NId, DateTime joinDate, string createdBy, DateTime createdDate,bool isActive, DataTable dtEducation, DataTable dtTraining)
    {

        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@DesignationId", designationId);
        dm.AddParameteres("@GradeId", gradeId);
        dm.AddParameteres("@LevelId", levelId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@TeacherPin", TeacherPin);
        dm.AddParameteres("@EmployeeId", empId);
        dm.AddParameteres("@NId", NId);
        dm.AddParameteres("@JoinDate", joinDate);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        dm.AddParameteres("@IsActive", isActive);
        dm.AddParameteres("@dtEducation", dtEducation);
        dm.AddParameteres("@dtTraining", dtTraining);

        DataTable dt = dm.ExecuteQuery("USP_Teacher_Insert2");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int Id, int personId, int designationId, string TeacherPin, string NId, DateTime joinDate, string createdBy, DateTime createdDate)
    {
        dm.AddParameteres("@Id", Id);
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@DesignationId", designationId);
        dm.AddParameteres("@TeacherPin", TeacherPin);
        dm.AddParameteres("@NId", NId);
        dm.AddParameteres("@JoinDate", joinDate);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        DataTable dt = dm.ExecuteQuery("USP_Teacher_Update");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int EducationInsert(DataTable dt)
    {
        DataSet ds = new DataSet("dsEducation");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        return dm.ExecuteNonQuery("USP_Teacher_EducationInsert");
    }

    public int EducationUpdate(DataTable dt)
    {
        DataSet ds = new DataSet("dsEducation");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        return dm.ExecuteNonQuery("USP_Teacher_EducationUpdate");
    }
    public int TrainingInsert(DataTable dt)
    {
        DataSet ds = new DataSet("dsTraining");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        return dm.ExecuteNonQuery("USP_Teacher_TrainingInsert");
    }

    public int TrainingInsert(TeacherTraining TeacherTraining)
    {
        dm.AddParameteres("@TrainingName", TeacherTraining.TrainingName);
        dm.AddParameteres("@InstituteName", TeacherTraining.InstituteName);
        dm.AddParameteres("@StartDate", TeacherTraining.StartDate);
        dm.AddParameteres("@EndDate", TeacherTraining.EndDate);
        dm.AddParameteres("@TeacherID", TeacherTraining.TeacherId);
        dm.AddParameteres("@Topics", TeacherTraining.Topics);
        dm.AddParameteres("@Duration", TeacherTraining.Duration);
        return dm.ExecuteNonQuery("USP_InsertTrainingInfo");
    }
    public int TrainingUpdate(DataTable dt)
    {
        DataSet ds = new DataSet("dsTraining");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        return dm.ExecuteNonQuery("USP_Teacher_TrainingUpdate");
    }

    public int BankAccountInsert(int personId, int designationId, int campusId,bool isActive, string accountNumber, string createdBy, DateTime createdDate)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@DesignationId", designationId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@IsActive", isActive);
        dm.AddParameteres("@AccountNumber", accountNumber);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        return dm.ExecuteNonQuery("USP_Insert_EmployeeAccountNumber");
    }

    public DataTable GetAll()
    {
        return dm.ExecuteQuery("USP_Teacher_GetAll");
    }

    public int AttendenceInsert(DataTable dt, string year, string month, string createdBy, DateTime createdDate, DateTime date)
    {
        DataSet ds = new DataSet("dsAttendence");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@Month", month);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        dm.AddParameteres("@Date", date);
        return dm.ExecuteNonQuery("USP_Teacher_AttendenceInserted");
    }
    public DataTable GetByDate(DateTime date)
    {
        dm.AddParameteres("@Date", date);
        return dm.ExecuteQuery("USP_Teacher_AttendenceGetByDate");
    }
    public DataTable GetByPersonId(int personId)
    {
        dm.AddParameteres("@PersonId", personId);
        return dm.ExecuteQuery("USP_Teacher_GetByPersonId");
    }
    public DataTable EducationByTeacherId(int TeacherId)
    {
        dm.AddParameteres("@TeacherId", TeacherId);
        return dm.ExecuteQuery("USP_TeacherEducation_GetByPersonId");
    }
    public DataTable TrainingByTeacherId(int TeacherId)
    {
        dm.AddParameteres("@TeacherId", TeacherId);
        return dm.ExecuteQuery("USP_TeacherTraining_GetByPersonId");
    }
    public int LeaveApplication(string name, string pinCode, int designation, string subject, DateTime fromDate, DateTime toDate, string description)
    {
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@Designation", designation);
        dm.AddParameteres("@PinCode", pinCode);
        dm.AddParameteres("@Subject", subject);
        dm.AddParameteres("@FromDate", fromDate);
        dm.AddParameteres("@ToDate", toDate);
        dm.AddParameteres("@Desctiption", description);
        return dm.ExecuteNonQuery("USP_Teacher_LeaveInsert");
    }
    public int ApproveLeaveApplication(int id, DateTime approveFromDate, DateTime approveToDate, string approvedBy, DateTime approvedDate, bool status, string comment)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@ApproveFromDate", approveFromDate);
        dm.AddParameteres("@ApproveToDate", approveToDate);
        dm.AddParameteres("@ApprovedBy", approvedBy);
        dm.AddParameteres("@ApprovedDate", approvedDate);
        dm.AddParameteres("@Status", status);
        dm.AddParameteres("@Comment", comment);
        return dm.ExecuteNonQuery("USP_Teacher_LeaveApprove");
    }

    public int InsertPayScale(int typeId, string pincode)
    {
        dm.AddParameteres("@TypeId", typeId);
        dm.AddParameteres("@PinCode", pincode);
        return dm.ExecuteNonQuery("USP_Teacher_InsertPayScale");
    }


    public DataTable GetAllTeacher()
    {
        string sql = @"Select * from dbo.st_Person join dbo.tr_Teacher on tr_Teacher.PersonID=st_Person.Id";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetAllTeacherByYearCampus(int campusId)
    {
        dm.AddParameteres("@CampusId", campusId);
        return dm.ExecuteQuery("USP_GetAllTeacherByYearCampus");
    }

    public int EducationUpdate(TeacherEducation techerEducation)
    {
        dm.AddParameteres("@Id", techerEducation.Id);
        dm.AddParameteres("@DegreeName", techerEducation.DegreeName);
        dm.AddParameteres("@Board", techerEducation.Board);
        dm.AddParameteres("@Grade", techerEducation.Grade);
        dm.AddParameteres("@PassingYear", techerEducation.PassingYear);
        dm.AddParameteres("@Subject", techerEducation.Subject);
        dm.AddParameteres("@GPAScale", techerEducation.GPAScale);
        dm.AddParameteres("@ResultDivision", techerEducation.ResultDivision);
        return dm.ExecuteNonQuery("USP_Teacher_EducationUpdate");
    }

    public int TrainingUpdate(TeacherTraining TeacherTraining)
    {
        dm.AddParameteres("@Id", TeacherTraining.TrainingID);
        dm.AddParameteres("@TrainingName", TeacherTraining.TrainingName);
        dm.AddParameteres("@InstituteName", TeacherTraining.InstituteName);
        dm.AddParameteres("@StartDate", TeacherTraining.StartDate);
        dm.AddParameteres("@EndDate", TeacherTraining.EndDate);
        //dm.AddParameteres("@TeacherID", TeacherTraining.TeacherId);
        dm.AddParameteres("@Topics", TeacherTraining.Topics);
        dm.AddParameteres("@Duration", TeacherTraining.Duration);
        return dm.ExecuteNonQuery("USP_TecherTraining_Update");
    }
    public DataTable GetTeacherAttendenceByTeacherID(int TeacherId, string year, string month)
    {
        dm.AddParameteres("@TeacherId", TeacherId);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@Month", month);
        return dm.ExecuteQuery("USP_Teacher_GetTeacherAttendence");
    }
    public DataTable GetTeacherByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Teacher_GetByCriteria");
    }

 public DataTable GetTeacherByCampusIdAndGradeId(int campusId, int gradeId, int levelId)
    {
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@GradeId", gradeId);
        dm.AddParameteres("@LevelId", levelId);
        return dm.ExecuteQuery("USP_GetTeacherByCampusIdAndGradeId");
    }

    public DataTable GetTeacherPayment(int empId, int yearId, int monthId)
    {
        dm.AddParameteres("@PersonId", empId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@MonthId", monthId);
        return dm.ExecuteQuery("USP_GetTeacherPayment");
    }
    public int AttendenceInsertByDt(DataTable dt, string year, string month, string createdBy, DateTime createdDate)
    {
        int id = 0;
        foreach (DataRow dtRow in dt.Rows)
        {
            dm.AddParameteres("@Date", Convert.ToDateTime(dtRow["Date"].ToString()));
            dm.AddParameteres("@TId", Convert.ToInt32(dtRow["TId"].ToString()));
            dm.AddParameteres("@TimeIn", dtRow["timeIn"].ToString());
            dm.AddParameteres("@OutTime", dtRow["OutTime"].ToString());
            dm.AddParameteres("@Year", year);
            dm.AddParameteres("@Month", month);
            dm.AddParameteres("@CreatedBy", createdBy);
            dm.AddParameteres("@CreatedDate", createdDate);
            id = dm.ExecuteNonQuery("USP_Teacher_AttendenceInserted");
        }
        return id;
    }
    public int DeleteTeacherById(int ID, string updateBy, DateTime updateDate, bool isActive)
    {
        dm.AddParameteres("@PersonId", ID);
        dm.AddParameteres("@UpdatedBy", updateBy);
        dm.AddParameteres("@UpdatedDate", updateDate);
        dm.AddParameteres("@IsActive", isActive);
        return dm.ExecuteNonQuery("USP_Teacher_DeleteByPersonId");
    }

    public string UpdateUserName(string userName, string user, string lowerUser)
    {
        dm.AddParameteres("@UserName", userName);
        dm.AddParameteres("@User", user);
        dm.AddParameteres("@LoweredUserName", lowerUser);
        return dm.ExecuteNonQuery("USP_UpdateASPnet_UserByUserName").ToString();
    }


    public int InsertTeacherPayments(int teacherId, int monthId, int yearId, DateTime date, double amount, bool parital)
    {
        dm.AddParameteres("@TeacherId", teacherId);
        dm.AddParameteres("@MonthId", monthId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@Amount", amount);
        dm.AddParameteres("@IsPartialPay", parital);
        return dm.ExecuteNonQuery("USP_InsertTeacherPayments");
    }


    public DataTable GetEmployeeByDesignation(int campusId, int designationId)
    {
        dm.AddParameteres("CampusId", campusId);
        dm.AddParameteres("DesignationId", designationId);
        return dm.ExecuteQuery("USP_GetEmployeeByDesignation");
    }

    public DataTable GetAllEmployeeSalaryStructureByCriteria(int campusId, int designationId)
    {
      //  dm.AddParameteres("@CampusId", campusId);
     //   dm.AddParameteres("@DesignationId", designationId);
        string sql = @"Select * from dbo.tbl_EmployeeSalary
        Inner join dbo.tr_Teacher on tbl_EmployeeSalary.TeacherId=tr_Teacher.Id
        Inner join dbo.st_Person on tr_Teacher.PersonId=st_Person.Id
        Inner join dbo.bs_Month on tbl_EmployeeSalary.MonthId=bs_Month.Id
        Inner join dbo.bs_Year on tbl_EmployeeSalary.YearId=bs_Year.Id

where CampusId = "
        + campusId + " and DesignationId = "+designationId;
        return dm.NewExecuteQuery(sql);
    }
}



