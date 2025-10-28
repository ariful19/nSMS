using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalDashbord
/// </summary>
public class dalDashbord
{
    DatabaseManager dm = new DatabaseManager();
	public dalDashbord()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int GetStudent()
    {
        DataTable dt= dm.ExecuteQuery("USP_Dashboard_GetStudent");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int GetTeacher()
    {
        DataTable dt = dm.ExecuteQuery("USP_Dashboard_GetTeacher");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int GetUniqueVisitor()
    {
        DataTable dt = dm.ExecuteQuery("USP_Dashboard_GetUniqueVisit");
        int visitor = (dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0][0]) : 1;
        return visitor ;
    }
    public int UpdateUniqueVisitor()
    {
       return dm.ExecuteNonQuery("USP_Dashboard_UpdateUniqueVisit");
    }

    public DataTable GetAllStudent()
    {
        return dm.ExecuteQuery("USP_Get_AllStudent");
    }

    public DataTable GetAllTeacher()
    {
        return dm.ExecuteQuery("USP_Teacher_GetAll");
    }
}