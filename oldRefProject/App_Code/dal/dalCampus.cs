using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalCampus
/// </summary>
public class dalCampus
{
    DatabaseManager dm = new DatabaseManager();
	public dalCampus()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertCampus(string CampusName)
    {
        dm.AddParameteres("@CampusName", CampusName);
        DataTable dt = dm.ExecuteQuery("USP_Campus_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public DataTable GetById(int ID)
    {
        dm.AddParameteres("@Id", ID);
        return dm.ExecuteQuery("USP_Campus_GetById");
    }
    public int Update(int ID, string CampusName)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@CampusName", CampusName);
        return dm.ExecuteNonQuery("USP_Campus_Update");
    }
}