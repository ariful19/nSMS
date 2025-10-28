using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalThana
/// </summary>
public class dalThana
{
    DatabaseManager dm = new DatabaseManager();
	public dalThana()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(int districtId ,string name)
    {
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@Thana", name);
        DataTable dt= dm.ExecuteQuery("USP_Thana_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, int districtId, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@Thana", name);
        return dm.ExecuteNonQuery("USP_Thana_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Thana_GetById");
    }

    public DataTable GetByDistrictId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Thana_GetByDistrictId");
    }
}