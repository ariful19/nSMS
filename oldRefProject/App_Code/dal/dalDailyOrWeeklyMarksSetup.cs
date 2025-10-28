using System.Data;
using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalDailyOrWeeklyMarksSetup
/// </summary>
public class dalDailyOrWeeklyMarksSetup
{
    DatabaseManager dm = new DatabaseManager();
	public dalDailyOrWeeklyMarksSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string year, bool isDefault)
    {
        dm.AddParameteres("@DailyOrWeeklyMarks", year);
        dm.AddParameteres("@IsDefault", isDefault);
        DataTable dt = dm.ExecuteQuery("USP_DailyOrWeeklyMarks_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id, string year, bool isDefault)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@DailyOrWeeklyMarks", year);
        dm.AddParameteres("@IsDefault", isDefault);
        return dm.ExecuteNonQuery("USP_DailyOrWeeklyMarks_Update");
    }

    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_DailyOrWeeklyMarks_GetById");
    }

    public DataTable GetDefault()
    {
        return dm.ExecuteQuery("USP_DailyOrWeeklyMarks_GetDefault");
    }
}