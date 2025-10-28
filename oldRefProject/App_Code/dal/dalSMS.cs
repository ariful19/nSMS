using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;
using System.Data;

/// <summary>
/// Summary description for dalSMS
/// </summary>
public class dalSMS
{
    DatabaseManager dm = new DatabaseManager();
	public dalSMS()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string name, string variable, string message, bool IsDefault)
    {
        dm.AddParameteres("@Name",name);
        dm.AddParameteres("@Variable", variable);
        dm.AddParameteres("@Message", message);
        dm.AddParameteres("@IsDefault", IsDefault);
        DataTable dt = dm.ExecuteQuery("USP_SMSTemplete_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id, string name, string variable, string message, bool IsDefault)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@Variable", variable);
        dm.AddParameteres("@Message", message);
        dm.AddParameteres("@IsDefault", IsDefault);
        return dm.ExecuteNonQuery("USP_SMSTemplete_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id",id);
        return dm.ExecuteQuery("USP_SMSTemplete_GetById");
    }
    public DataTable GetEditById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_SMSTemplete_GetEditById");
    }
    public DataTable GetNotDefault()
    {
        return dm.ExecuteQuery("USP_SMSTemplete_GetNotDefault");
    }
    public DataTable GetDefault()
    {
        return dm.ExecuteQuery("USP_SMSTemplete_GetDefault");
    }
    public DataTable GetDefaultById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_SMSTemplete_GetDefaultById");
    }
    public DataTable GetByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_SMSTemplete_GetBycriteria");
    }
    public DataTable GetNotDefaultMarks()
    {
        return dm.ExecuteQuery("USP_SMSTemplete_GetNotDefaultMarks");
    }
    public DataTable GetMarksSMSById(int id)
    {
        dm.AddParameteres("@Id", id);  
        return dm.ExecuteQuery("USP_SMSTemplete_GetMarksSMSById");
    }
}