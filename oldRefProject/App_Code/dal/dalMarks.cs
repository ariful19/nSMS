using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalMarks
/// </summary>
public class dalMarks
{
    DatabaseManager dm = new DatabaseManager();
    public dalMarks()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertMarks(DataTable dt, int year, double baseMarks, int examTypeId, string createdBy, bool IsPublishMarks, bool IsFinishedMarks)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML",xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ExamType", examTypeId);
        dm.AddParameteres("@BaseMarks", baseMarks);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@IsPublishMarks", IsPublishMarks);
        dm.AddParameteres("@IsFinishMarks", IsFinishedMarks);
        return dm.ExecuteNonQuery("USP_Marks_InserXml");
    }
    public int UpdateMarks(DataTable dt, int year, double baseMarks, int examTypeId, string updatedBy, bool IsPublishMarks, bool IsFinishedMarks)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@BaseMarks", baseMarks);
        dm.AddParameteres("@ExamType", examTypeId);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        dm.AddParameteres("@IsPublishMarks", IsPublishMarks);
        dm.AddParameteres("@IsFinishMarks", IsFinishedMarks);
        return dm.ExecuteNonQuery("USP_Marks_UpdateXml");
    }
    public int UpdateFinishedMarks(DataTable dt, int year, double baseMarks, int examTypeId, string updatedBy, bool IsPublishMarks, bool IsFinishMarks)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@BaseMarks", baseMarks);
        dm.AddParameteres("@ExamType", examTypeId);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        dm.AddParameteres("@IsPublishMarks", IsPublishMarks);
        dm.AddParameteres("@IsFinishMarks", IsFinishMarks);
        return dm.ExecuteNonQuery("USP_Marks_FinishedMarksUpdateXml");
    }
    public int UpdatePublishedViewMarks(DataTable dt, int year, double baseMarks, int examTypeId, string updatedBy, bool IsPublishMarks, bool IsFinishMarks)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@BaseMarks", baseMarks);
        dm.AddParameteres("@ExamType", examTypeId);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        dm.AddParameteres("@IsPublishMarks", IsPublishMarks);
        dm.AddParameteres("@IsFinishMarks", IsFinishMarks);       
        return dm.ExecuteNonQuery("USP_Marks_PublishedMarksUpdateXml");
    }

    public DataTable GetStudentMarks(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("[USP_GetStudentResult]");
    }

   public DataTable GetStudentMarksForView(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_View_Marksheet_SubjectWise");
    }

  public DataTable GetStudentMarksForViewForJunior(string criteria)
   {
       dm.AddParameteres("@Criteria", criteria);
       return dm.ExecuteQuery("USP_View_Marksheet_SubjectWiseForJunior");
   }
   public DataTable GetStudentMarksforHigherClass(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_GetHigherClassStudentResult");
    }

    public DataTable GetSubjectWiseMarks(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("[USP_GetStudentResult]");
    }
    public DataTable GetResult(string SQLQuery)
    {
        dm.AddParameteres("@Criteria", SQLQuery);
        return dm.ExecuteQuery("");
    }


    public DataTable  GetMarksSheetHeaderInfo(string headerCriteria)
    {
        dm.AddParameteres("@Criteria", headerCriteria);
        return dm.ExecuteQuery("[USP_GetMarksSheetHeaderInfo]");
    }
    public DataTable GetTabulationSheetHeaderInfo(string headerCriteria)
    {
        dm.AddParameteres("@Criteria", headerCriteria);
        return dm.ExecuteQuery("[USP_GetMarksSheetHeaderInfoForTSheet]");
    }
    public int InsertPromotion(DataTable dt, string Year, string Class, string Shift, string Section, bool IsPromption, string CreatedBy, DateTime CreatedDate)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", Year);
        dm.AddParameteres("@Class", Class);
        dm.AddParameteres("@Shift", Shift);
        dm.AddParameteres("@NewSection", Section);
        dm.AddParameteres("@IsPromotion", IsPromption);
        dm.AddParameteres("@CreatedBy", CreatedBy);
        dm.AddParameteres("@CreatedDate", CreatedDate);
        return dm.ExecuteNonQuery("USP_Promotion_InserXml");
    }
    public int UpdateStudent(DataTable dtStu, string year, int Class, int shift, int section)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dtStu);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ClassId", Class);
        dm.AddParameteres("@ShiftId", shift);
        dm.AddParameteres("@SectionId", section);
        return dm.ExecuteNonQuery("USP_StudentPromotion_Update");
    }
    
 public int Insert_ProfileLog(int StudentId, string Year,int mediumId, int Campus, int Class,int Group, int Section, int Shift, string previousRoll, bool IsPromotion, double SubjectiveMarks, double ObjectiveMarks, double PracticalMarks, double OtherMarks, double AttendanceMarks, double MonthlyMarks, double TotalMarks, string NewRoll, string CreatedBy, DateTime CreatedDate)
    {

        dm.AddParameteres("@StudentToClassId", StudentId);
        dm.AddParameteres("@Year", Year);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@ClassId", Class);
        dm.AddParameteres("@CampusId", Campus);
        dm.AddParameteres("@GroupId", Group);
        dm.AddParameteres("@SectionId", Section);
        dm.AddParameteres("@ShiftId", Shift);
        dm.AddParameteres("@previousRoll", previousRoll);
        dm.AddParameteres("@IsPromotion", IsPromotion);
        dm.AddParameteres("@SubjectiveMarks", SubjectiveMarks);
        dm.AddParameteres("@ObjectiveMarks", ObjectiveMarks);
        dm.AddParameteres("@PracticalMarks", PracticalMarks);
        dm.AddParameteres("@OtherMarks", OtherMarks);
        dm.AddParameteres("@AttendanceMarks", AttendanceMarks);
        dm.AddParameteres("@MonthlyMarks", MonthlyMarks);
        dm.AddParameteres("@TotalMarks", TotalMarks);
        dm.AddParameteres("@NewRoll", NewRoll);
        dm.AddParameteres("@CreatedBy", CreatedBy);
        dm.AddParameteres("@CreatedDate", CreatedDate);
        return dm.ExecuteNonQuery("USP_Insertst_ProfileLog");
    }
   
   
public int PromotionUpdateStudent(string year, int mediumId, int Campus, int Class, int group, int shift, int section, int studnetId, string updateBy)
    {

        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", Campus);
        dm.AddParameteres("@ClassId", Class);
        dm.AddParameteres("@GroupId", group);
        dm.AddParameteres("@ShiftId", shift);
        dm.AddParameteres("@SectionId", section);
        dm.AddParameteres("@studentId", studnetId); dm.AddParameteres("@UpdatedBy", updateBy);
        return dm.ExecuteNonQuery("USP_StudentUpdateToPromotion");
    }


    public int InsertDailyMarks(DataTable dt, int year, double outofmarks, int examType, DateTime marksDate, string createBy,DateTime createDate)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ExamType", examType);
        dm.AddParameteres("@OutOfMarks", outofmarks);
        dm.AddParameteres("@ExamDate", marksDate);
        dm.AddParameteres("@CreatedBy", createBy);
        dm.AddParameteres("@CreatedDate", createDate);
        return dm.ExecuteNonQuery("USP_DailyMarks_InserXml");
    }
    public int UpdateDailyMarks(DataTable dt, int year, double outofmarks, int examType, string updateBy, DateTime updateDate)
    {
        DataSet ds = new DataSet("dsMarks");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ExamType", examType);
        dm.AddParameteres("@OutOfMarks", outofmarks);
        dm.AddParameteres("@UpdatedBy", updateBy);
        dm.AddParameteres("@UpdatedDate", updateDate);
        return dm.ExecuteNonQuery("USP_DailyMarks_UpdateXml");
    }

    public object GetDailyOrWeeklyExam()
    {
        return dm.ExecuteQuery("USP_ExamType_GetDailyOrWeeklyType");
    }

    public DataTable GetHigherStudentMarks(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("[USP_GetStudentResultForHigher]");
    }
  public DataTable GetMeritPositionByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Marks_StudentMeritPositionByCriteria");
    }


  public DataTable GetMeritPositionByCriteriaForHigher(string criteria)
  {
      dm.AddParameteres("@Criteria", criteria);
      return dm.ExecuteQuery("USP_Marks_StudentMeritPositionByCriteriaForHigher");
  }

  public int DeleteMarks(string id)
  {
      dm.AddParameteres("@Id", id);
      return dm.ExecuteNonQuery("USP_Marks_DeleteMarks");
  }

}





