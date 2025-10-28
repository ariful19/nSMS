using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalExamType
/// </summary>
public class dalExamType
{
	public dalExamType()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DatabaseManager dm = new DatabaseManager();
    public int Insert(string name, bool isDailyOrWeekly)
    {
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@IsDailyOrWeekly", isDailyOrWeekly);
        DataTable dt = dm.ExecuteQuery("USP_ExamType_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name, bool isDailyOrWeekly)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@IsDailyOrWeekly", isDailyOrWeekly);
        return dm.ExecuteNonQuery("USP_ExamType_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_ExamType_GetById");
    }

    public DataTable GetDailyOrWeeklyExam()
    {
        return dm.ExecuteQuery("USP_ExamType_GetDailyOrWeekly");
    }
}