using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;
using System.Data;

/// <summary>
/// Summary description for dalYear
/// </summary>
public class dalYear
{
    DatabaseManager dm = new DatabaseManager();
	public dalYear()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string year,bool isDefault)
    {
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@IsDefault", isDefault);
        DataTable dt= dm.ExecuteQuery("USP_Year_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id,string year, bool isDefault)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@IsDefault", isDefault);
        return dm.ExecuteNonQuery("USP_Year_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Year_GetById");
    }

    public DataTable GetMonthById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Month_GetById");
    }
    public int GetNameId(int id)
    {
        dm.AddParameteres("@Id", id);
        DataTable dt = dm.ExecuteQuery("USP_Year_GetById");
        return Convert.ToInt32(dt.Rows[0]["Year"]);
    }

    public DataTable GetDefault()
    {
        return dm.ExecuteQuery("USP_Year_GetDefault");
    }
}