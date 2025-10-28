using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalSubjectName
/// </summary>
public class dalSubjectName
{
	public dalSubjectName()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DatabaseManager dm = new DatabaseManager();
    public int Insert(string name)
    {
        dm.AddParameteres("@Name", name);
        DataTable dt = dm.ExecuteQuery("USP_SubjectName_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Name", name);
        return dm.ExecuteNonQuery("USP_SubjectName_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_SubjectName_GetById");
    }
}