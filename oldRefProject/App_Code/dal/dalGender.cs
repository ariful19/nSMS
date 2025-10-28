using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalGender
/// </summary>
public class dalGender
{
	public dalGender()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DatabaseManager dm = new DatabaseManager();
    public int Insert(string name)
    {
        dm.AddParameteres("@Gender", name);
        DataTable dt=dm.ExecuteQuery("USP_Gender_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Gender", name);
        return dm.ExecuteNonQuery("USP_Gender_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Gender_GetById");
    }
}