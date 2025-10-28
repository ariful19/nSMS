using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalAttendenceType
/// </summary>
public class dalAttendenceType
{
    DatabaseManager dm = new DatabaseManager();
	public dalAttendenceType()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string attendenceType)
    {
        dm.AddParameteres("AttendenceType", attendenceType);
        DataTable dt = dm.ExecuteQuery("USP_AttendenceType_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string attendenceType)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@AttendenceType", attendenceType);
        return dm.ExecuteNonQuery("USP_AttendenceType_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_AttendenceType_GetById");
    }
}