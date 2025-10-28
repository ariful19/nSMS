using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalMainHeadCode
/// </summary>
public class dalMainHeadCode
{
    DatabaseManager dm= new DatabaseManager();
	public dalMainHeadCode()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertMainCode(MainHeadCode aMainHeadCode)
    {
        dm.AddParameteres("@MainCodeId", aMainHeadCode.MainCodeId);
        dm.AddParameteres("@CodeName", aMainHeadCode.CodeName);
        dm.AddParameteres("@Description", aMainHeadCode.Description);
        dm.AddParameteres("@AId", aMainHeadCode.AId);
        DataTable dt = dm.ExecuteQuery("USP_MainHeadCode_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetMainHeadCodeById(int ID)
    {
        dm.AddParameteres("@Id", ID);
        return dm.ExecuteQuery("USP_MainHeadCode_GetById");
    }

    public int UpdateMainCode(int ID, MainHeadCode aMainHeadCode)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@MainCodeId", aMainHeadCode.MainCodeId);
        dm.AddParameteres("@CodeName", aMainHeadCode.CodeName);
        dm.AddParameteres("@Description", aMainHeadCode.Description);
        dm.AddParameteres("@AId", aMainHeadCode.AId);
        return dm.ExecuteNonQuery("USP_MainHeadCode_Update");
    }
}