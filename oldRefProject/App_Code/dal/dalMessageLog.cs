using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalMessageLog
/// </summary>
public class dalMessageLog
{
    DatabaseManager dm = new DatabaseManager();
	public dalMessageLog()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void MessageLogInsert(MessageLog messageLog)
    {
        dm.AddParameteres("@SendDate", messageLog.SendDate);
        dm.AddParameteres("@MobileNumber", messageLog.MobileNumber);
        dm.AddParameteres("@MessageBody", messageLog.MessageBody);
        dm.AddParameteres("@Status", messageLog.Status);
        dm.AddParameteres("@Sender", messageLog.Sender);
        dm.AddParameteres("@MessageType", messageLog.MessageType);

        DataTable dt = dm.ExecuteQuery("USP_MessageLogInsert");

    }

    public int MessageLogUpdate(MessageLog messageLog)
    {
        dm.AddParameteres("@MessageLogID", messageLog.MessageLogID);
        dm.AddParameteres("@SendDate", messageLog.SendDate);
        dm.AddParameteres("@MobileNumber", messageLog.MobileNumber);
        dm.AddParameteres("@MessageBody", messageLog.MessageBody);
        dm.AddParameteres("@Status", messageLog.Status);
        dm.AddParameteres("@Sender", messageLog.Sender);

        return dm.ExecuteNonQuery("USP_MessageLogUpdate");
    }
    public DataTable GetAllMessageLogByDate()
    {
        //dm.AddParameteres("@FromDate", fromDate);
        //dm.AddParameteres("@ToDate", toDate);

        return dm.ExecuteQuery("USP_GetAllMessageLogByDate");
    }


    public DataTable GetMessageLogByDate(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_MessageLog_GetByCriteria");
    }
}