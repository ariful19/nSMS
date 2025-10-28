using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalPayrollType
/// </summary>
public class dalPayrollType
{
    DatabaseManager dm = new DatabaseManager();
	public dalPayrollType()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string name,int scaleId, double basic)
    {
        dm.AddParameteres("@Type", name);
        dm.AddParameteres("@ScaleId", scaleId);
        dm.AddParameteres("@Basic", basic);
        DataTable dt = dm.ExecuteQuery("USP_Payroll_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name, int scaleId, double basic)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Type", name);
        dm.AddParameteres("@ScaleId", scaleId);
        dm.AddParameteres("@Basic", basic);
        return dm.ExecuteNonQuery("USP_Payroll_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_GetById");
    }
}