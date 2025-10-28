using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalOutOfMarks
/// </summary>
public class dalOutOfMarks
{
    DatabaseManager dm = new DatabaseManager();
	public dalOutOfMarks()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string outOfMarks)
    {
        dm.AddParameteres("@OutOfMarks", outOfMarks);
        DataTable dt = dm.ExecuteQuery("USP_OutOfMarks_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_OutOfMarks_GetById");
    }

    public int Update(int id, string outOfMarks)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@OutOfMarks", outOfMarks);

        return dm.ExecuteNonQuery("USP_OutOfMarks_Update");
    }
}