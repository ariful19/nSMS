using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalContactInfo
/// </summary>
public class dalContactInfo
{
    DatabaseManager dm = new DatabaseManager();
	public dalContactInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertContactInfo(string name, string email, string mobile, string subject, string message)
    {
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@Email", email);
        dm.AddParameteres("@Mobile", mobile);
        dm.AddParameteres("@Subject", subject);
        dm.AddParameteres("@Message", message);
        DataTable dt = dm.ExecuteQuery("USP_ContactInfo_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetContactInfoByDate(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_ContactInfo_GetByCriteria");
    }
}