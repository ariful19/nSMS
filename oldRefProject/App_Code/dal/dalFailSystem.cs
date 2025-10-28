using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalFailSystem
/// </summary>
public class dalFailSystem
{
    DatabaseManager dm = new DatabaseManager();
	public dalFailSystem()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(int yearId,int campusId, int examTypeId, int subjectToClassId,double theory, double objective, string createdBy)
    {
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ExamTypeId", examTypeId);
        dm.AddParameteres("@SubjectToClassId",subjectToClassId);
        dm.AddParameteres("@Theory", theory);
        dm.AddParameteres("@Objective", objective);
        dm.AddParameteres("@CreatedBy", createdBy);
        DataTable dt=dm.ExecuteQuery("USP_FailSystem_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id, int yearId, int campusId, int examTypeId, int subjectToClassId, double theory, double objective, string updatedBy)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@YearId",yearId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ExamTypeId", examTypeId);
        dm.AddParameteres("@SubjectToClassId", subjectToClassId);
        dm.AddParameteres("@Theory", theory);
        dm.AddParameteres("@Objective", objective);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        return dm.ExecuteNonQuery("USP_FailSystem_Update");
    }
    public DataTable GetFailSystem(int yearId,int campusId, int examtypeId,int mediumId,int classId,int groupId)
    {
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ExamTypeId", examtypeId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        return dm.ExecuteQuery("USP_FailSystem_GetByClassAndGroupId");
    }
    public DataTable GetById(int Id)
    {
        dm.AddParameteres("@Id", Id);
        return dm.ExecuteQuery("USP_FailSystem_GetById");
    }
    public DataTable GetBySubjectId(int subjectId)
    {
        dm.AddParameteres("@Id", subjectId);
        return dm.ExecuteQuery("USP_FailSystem_GetBySubjectId");
    }
    public DataTable GetByFailBySubjectAndClassId(int yearId,int campusId,int examTypeId,int subjectToClassId, int classId, int mediumId, int groupId)
    {
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ExamTypeId", examTypeId);
        dm.AddParameteres("@SubjectToClassId", subjectToClassId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@GroupId", groupId);
        return dm.ExecuteQuery("USP_FailSystem_GetBySubjectAndClassId");
    }

}
