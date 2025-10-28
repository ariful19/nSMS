using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;
using System.Data;

/// <summary>
/// Summary description for dalSubject
/// </summary>
public class dalSubject
{
    DatabaseManager dm=new DatabaseManager();
	public dalSubject()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    

    public int Insert(int yearId, int mediumId, int campusId, int classId, int groupId, int subjectId, int categoryId,int orderBy, int paperNo,bool isOptional ,string createdBy)
    {
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ClassId",classId);
        dm.AddParameteres("@GroupId",groupId);
        dm.AddParameteres("@SubjectId",subjectId);
        dm.AddParameteres("@CategoryId",categoryId);
        dm.AddParameteres("@OrderBy", orderBy);
        dm.AddParameteres("@PaperNo",paperNo);
        dm.AddParameteres("@IsOPtional", isOptional);
        dm.AddParameteres("@CreatedBy", createdBy);
        DataTable dt = dm.ExecuteQuery("USP_SubjectToClass_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, int yearId, int mediumId, int campusId, int classId, int groupId, int subjectId, int categoryId, int orderBy, int paperNo, bool isOptional, string updateBy)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@SubjectId", subjectId);
        dm.AddParameteres("@CategoryId", categoryId);
        dm.AddParameteres("@OrderBy", orderBy);
        dm.AddParameteres("@PaperNo", paperNo);
        dm.AddParameteres("@IsOPtional", isOptional);
        dm.AddParameteres("@UpdatedBy", updateBy);
        return dm.ExecuteNonQuery("USP_SubjectToClass_Update");      
    }
    //public DataTable GetByClassId(int classId, int mediumId,int groupId)
    //{
    //    dm.AddParameteres("@ClassId", classId);
    //    dm.AddParameteres("@MediumId", mediumId);
    //    dm.AddParameteres("@GroupId", groupId);
    //    return dm.ExecuteQuery("USP_Subject_GetByClassId");
    //}

    public DataTable GetByClassAndGroupId(int yearId ,int mediumId,int classId,int groupId)
    {
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        return dm.ExecuteQuery("USP_Subject_GetByGetByClassAndGroupId");
    }

  public DataTable GetSubjectByClassAndGroupId(int mediumId, int classId, int groupId)
    {        
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        return dm.ExecuteQuery("USP_GetSubjectByGetByClassAndGroupId");
    }

    public int SubjectToStudentInsert(string createdBy,DataTable dt)
    {
        DataSet ds = new DataSet("dsSubject");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@XML", xml);
        return dm.ExecuteNonQuery("USP_Subject_SubjectToStudentInsert");
    }

    public DataTable GetSubjectToClass(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("[USP_Report_SubjectToClass]");
    }

    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_SubjectToClass_GetById");
    }
 public DataTable GetSubjectById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_GetSubjectById");
    }

 public DataTable GetOptionalSubject(string criteria)
 {
    dm.AddParameteres("Criteria", criteria);
    return dm.ExecuteQuery("USP_GetOptionalSubjectByCriteria");
 }

 public DataTable GetSetMarksById(int id , int examType)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@ExamtypeId", examType);
        return dm.ExecuteQuery("USP_GetSetMarksById");
    }

public DataTable GetByClassAndGroupIdforTestByCriteria(string criteria)
 {
     dm.AddParameteres("@Criteria", criteria);
     return dm.ExecuteQuery("GetByClassAndGroupIdforTestByCriteria");
 }
      

}





