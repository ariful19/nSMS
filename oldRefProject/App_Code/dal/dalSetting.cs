using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalSetting
/// </summary>
public class dalSetting
{
    DatabaseManager dm = new DatabaseManager();
	public dalSetting()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int UpdateSchoolInfo(SchoolInfo schoolInfo)
    {
        dm.AddParameteres("@Name", schoolInfo.Name);
        dm.AddParameteres("@Code", schoolInfo.Code);
        dm.AddParameteres("@Logo", schoolInfo.Logo);
        dm.AddParameteres("@Year", schoolInfo.EstablishedYear);
        dm.AddParameteres("@EstdBy", schoolInfo.EstablishedBy);
        dm.AddParameteres("@Address", schoolInfo.Address);
        dm.AddParameteres("@Description", schoolInfo.Description);
        return dm.ExecuteNonQuery("USP_Setting_SchoolInfoUpdate");
    }

    public int InsertNotification(string title, bool sendEmail, bool sendSMS)
    {
        dm.AddParameteres("@Title", title);
        dm.AddParameteres("@SendEmail", sendEmail);
        dm.AddParameteres("@SendSMS", sendSMS);
        return dm.ExecuteNonQuery("USP_Setting_NotificationInset");
    }

    public int UpdateSMSGetway(string url, string userName, string pass)
    {
        dm.AddParameteres("@URL", url);
        dm.AddParameteres("@UserName", userName);
        dm.AddParameteres("@Password", pass);
        return dm.ExecuteNonQuery("USP_Setting_SMSGetwayUpdate");
    }

    public int UpdateGeneralSetting(string theme, string dateFormat, string timeZone, string button, string panel)
    {
        dm.AddParameteres("@Theme", theme);
        dm.AddParameteres("@DateFormat", dateFormat);
        dm.AddParameteres("@TimeZone", timeZone);
        dm.AddParameteres("@Button", button);
        dm.AddParameteres("@Panel", panel);
        return dm.ExecuteNonQuery("USP_Setting_GeneralSettingUpdate");
    }
    public DataTable GetNotificationById(int Id)
    {
        dm.AddParameteres("@Id", Id);
        return dm.ExecuteQuery("USP_Setting_GetNotificationById");
    }
}