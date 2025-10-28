using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalGrade
/// </summary>
public class dalGrade
{
    DatabaseManager dm = new DatabaseManager();
	public dalGrade()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string gradeName, double gradePoint, double starMarks, double endMarks, string remarks)
    {
        dm.AddParameteres("@GradeName",gradeName);
        dm.AddParameteres("@GradePoint", gradePoint);
        dm.AddParameteres("@StartMarks", starMarks);
        dm.AddParameteres("@EndMarks", endMarks);
        dm.AddParameteres("@Remarks", remarks);
        DataTable dt = dm.ExecuteQuery("USP_Grade_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id, string gradeName, double gradePoint, double starMarks, double endMarks, string remarks)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@GradeName", gradeName);
        dm.AddParameteres("@GradePoint", gradePoint);
        dm.AddParameteres("@StartMarks", starMarks);
        dm.AddParameteres("@EndMarks", endMarks);
        dm.AddParameteres("@Remarks", remarks);
        DataTable dt = dm.ExecuteQuery("USP_Grade_Update");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Grade_GetById");
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery("USP_Grade_GetAll");
    }
}