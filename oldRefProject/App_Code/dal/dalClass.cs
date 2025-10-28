using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalClass
/// </summary>
public class dalClass
{
    DatabaseManager dm = new DatabaseManager();

    public dalClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(string name)
    {
        dm.AddParameteres("@Class", name);
        DataTable dt = dm.ExecuteQuery("USP_Class_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Class", name);
        return dm.ExecuteNonQuery("USP_Class_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Class_GetById");
    }

    public DataTable GetClassRoutine(int year,int mediumID,int campusID, int classID, int groupID, int sectionID, int shifID, string dayName)
    {
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@MediumId", mediumID);
        dm.AddParameteres("@CampusId", campusID);
        dm.AddParameteres("@ClassID", classID);
        dm.AddParameteres("@GroupID", groupID);
        dm.AddParameteres("@SectionID", sectionID);
        dm.AddParameteres("@ShiftID", shifID);
        dm.AddParameteres("@Day", dayName);
        return dm.ExecuteQuery("USP_GetExistingClassRoutine");
    }
    public DataTable GetExamRoutine(int year,int mediumID,int campusID, int classID, int ExamTypeID, int groupID, int shifID)
    {
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@MediumId", mediumID);
        dm.AddParameteres("@CampusId", campusID);
        dm.AddParameteres("@ClassID", classID);
        dm.AddParameteres("@ExamTypeID", ExamTypeID);
        dm.AddParameteres("@GroupID", groupID);
        dm.AddParameteres("@ShiftID", shifID);
        return dm.ExecuteQuery("USP_GetExistingExamRoutine");
    }

    public DataTable GetRoutineByCraiteria(string craiteria)
    {
        dm.AddParameteres("@Criteria", craiteria);

        return dm.ExecuteQuery("USP_Routine_GetClassRoutineByCriteria");
    }
}