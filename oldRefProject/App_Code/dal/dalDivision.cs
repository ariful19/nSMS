using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalDivision
/// </summary>
public class dalDivision
{
    DatabaseManager dm = new DatabaseManager();
	public dalDivision()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string name)
    {
        dm.AddParameteres("@Division", name);
        DataTable dt=dm.ExecuteQuery("USP_Division_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id,string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Division", name);
        return dm.ExecuteNonQuery("USP_Division_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Division_GetById");
    }
}