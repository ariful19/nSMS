using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalStatus
/// </summary>
public class dalStatus
{
	public dalStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DatabaseManager dm = new DatabaseManager();
    public int Insert(string name)
    {
        dm.AddParameteres("@Status", name);
        DataTable dt = dm.ExecuteQuery("USP_Status_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Status", name);
        return dm.ExecuteNonQuery("USP_Status_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Status_GetById");
    }





}