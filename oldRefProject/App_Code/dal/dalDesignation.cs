using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalDesignation
/// </summary>
public class dalDesignation
{
    DatabaseManager dm = new DatabaseManager();
	public dalDesignation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string name)
    {
        dm.AddParameteres("@Designation", name);
        return dm.ExecuteNonQuery("USP_Designation_Insert");
    }

   public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Designation", name);
        return dm.ExecuteNonQuery("USP_Designation_Update");
    }   

    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Designation_GetById");
    }
}
