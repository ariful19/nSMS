using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalDistrict
/// </summary>
public class dalDistrict
{
    DatabaseManager dm = new DatabaseManager();
	public dalDistrict()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string name,int Division)
    {
        dm.AddParameteres("@District", name);
        dm.AddParameteres("@DivisionId", Division);
        DataTable dt = dm.ExecuteQuery("USP_District_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name,int Division)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@DivisionId", Division);
        dm.AddParameteres("@District", name);
        return dm.ExecuteNonQuery("USP_District_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_District_GetById");
    }
    public DataTable GetByDivisionId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_District_GetByDivisionId");
    }
}