using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalBloodGroup
/// </summary>
public class dalBloodGroup
{
	public dalBloodGroup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DatabaseManager dm = new DatabaseManager();
    public int Insert(string name)
    {
        dm.AddParameteres("@BloodGroup", name);
        DataTable dt = dm.ExecuteQuery("USP_BloodGroup_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@BloodGroup", name);
        return dm.ExecuteNonQuery("USP_BloodGroup_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_BloodGroup_GetById");
    }
}