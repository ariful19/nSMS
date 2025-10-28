using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalReligion
/// </summary>
public class dalReligion
{
    DatabaseManager dm = new DatabaseManager();
	public dalReligion()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string name)
    {
        dm.AddParameteres("@Religion", name);
        return dm.ExecuteNonQuery("USP_Religion_Insert");
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Religion", name);
        return dm.ExecuteNonQuery("USP_Religion_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Religion_GetById");
    }
}