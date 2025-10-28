using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalRoutine
/// </summary>
public class dalRoutine
{
    DatabaseManager dm = new DatabaseManager();
    public dalRoutine()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int ClassRoutineInsert(DataTable dt, int year,int campusId, int classId, int groupId, int shiftId, int sectionId, string day)
    {
        DataSet ds = new DataSet("dsRoutine");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@ShiftId", shiftId);
        dm.AddParameteres("@SectionId", sectionId);
        dm.AddParameteres("@Day", day);
        return dm.ExecuteNonQuery("USP_Routine_ClassRoutineInsert");
    }

    public int ClassRoutineInsert(List<ClassRoutine> lstClassRoutine, int year, int mediumId, int campusId, int classId, int groupId, int shiftId, int sectionId, string day)
    {
        int id = 0;
        foreach (ClassRoutine classRoutine in lstClassRoutine)
        {
            dm.AddParameteres("@PeriodNo", classRoutine.Period);
            dm.AddParameteres("@SubjectId", classRoutine.SubjectId);
            dm.AddParameteres("@StartTime", classRoutine.StartTime);
            dm.AddParameteres("@EndTime", classRoutine.EndTime);
            dm.AddParameteres("@Teacher", classRoutine.TeacherId);
            dm.AddParameteres("@Year", year);
            dm.AddParameteres("@MediumId", mediumId);
            dm.AddParameteres("@CampusId", campusId);
            dm.AddParameteres("@ClassId", classId);
            dm.AddParameteres("@GroupId", groupId);
            dm.AddParameteres("@ShiftId", shiftId);
            dm.AddParameteres("@SectionId", sectionId);
            dm.AddParameteres("@Day", day);

            if (classRoutine.Id > 0)
            {
                dm.AddParameteres("@Id", classRoutine.Id);
                id = dm.ExecuteNonQuery("USP_UpdateClassRoutine");
                
            }
            else
            {
                id = dm.ExecuteNonQuery("USP_InsertClassRoutine");
            }
            dm.ClearParameters();
        }

        return id;
    }

    public int ExamRoutineInsert(List<ExamRoutine> ExamRoutineList, int year,int mediumId,int campusId, int classId, int groupId, int shiftId, int examTypeId)
    {

        int id = 0;
        foreach (ExamRoutine aExamRoutine in ExamRoutineList)
        {
            dm.AddParameteres("@SubjectId", aExamRoutine.SubjectId);
            dm.AddParameteres("@StartTime", aExamRoutine.StartTime);
            dm.AddParameteres("@EndTime", aExamRoutine.EndTime);
            dm.AddParameteres("@Year", year);
            dm.AddParameteres("@MediumId", mediumId);
            dm.AddParameteres("@CampusId", campusId);
            dm.AddParameteres("@ClassId", classId);
            dm.AddParameteres("@GroupId", groupId);
            dm.AddParameteres("@ShiftId", shiftId);
            dm.AddParameteres("@ExamTypeId", examTypeId);
            dm.AddParameteres("@ExamDate", aExamRoutine.ExamDate);

            if (aExamRoutine.Id > 0)
            {
                dm.AddParameteres("@Id", aExamRoutine.Id);
                id = dm.ExecuteNonQuery("USP_UpdateExamRoutine");

            }
            else
            {
                id = dm.ExecuteNonQuery("USP_InsertExamRoutine");
            }
            dm.ClearParameters();
        }
        return id;
    }
    public DataTable GetByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Routine_GetByCriteria");
    }

    public DataTable GetClassRoutineByCriteria(int year,int mediumId, int campusID, int classID, int groupID, int shiftId, int sectionID)
    {
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", campusID);
        dm.AddParameteres("@ClassID", classID);
        dm.AddParameteres("@GoupID", groupID);
        dm.AddParameteres("@ShiftID", shiftId);
        dm.AddParameteres("@SectionID", sectionID);
        return dm.ExecuteQuery("USP_GetClassRoutine");
    }
    
    public DataTable GetExamRoutineByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Routine_GetExamRoutineByCriteria");
    }
    public int ClassRoutineDelete(int classRoutinId)
    {
        int id = 0;
        if (classRoutinId > 0)
        {
            dm.AddParameteres("@Id", classRoutinId);
            id = dm.ExecuteNonQuery("USP_DeleteClassRoutine");
        }


        return id;
    }

    public int ExamRoutineDelete(int examRoutinId)
    {
        int id = 0;
        if (examRoutinId > 0)
        {

            dm.AddParameteres("@Id", examRoutinId);
            id = dm.ExecuteNonQuery("USP_Delete_ExamRoutine");
        }


        return id;
    }
}