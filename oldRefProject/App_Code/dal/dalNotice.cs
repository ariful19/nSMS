﻿using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNotice
/// </summary>
public class dalNotice
{
    DatabaseManager dm = new DatabaseManager();
	public dalNotice()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string title, string shorDescription, string details, DateTime date, string createdBy,DateTime createdDate)
    {
        dm.AddParameteres("@Title", title);
        dm.AddParameteres("@ShortDescription", shorDescription);
        dm.AddParameteres("@Details", details);
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        DataTable dt=  dm.ExecuteQuery("USP_Notice_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int InsertForAllClass(int year,int mediumId,int campusId, int classId,int noticeId)
    {
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@NoticeId", noticeId);
        return dm.ExecuteNonQuery("USP_Notice_ForAllClass");
    }
    public int InsertForSpecificStudent(DataTable dt)
    {
        DataSet ds = new DataSet("dsPerson");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        return dm.ExecuteNonQuery("USP_Notice_ForSpecificStudent");
    }

    public int InsertForSpecificTeacher(DataTable dt)
    {
        DataSet ds = new DataSet("dsPerson");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        return dm.ExecuteNonQuery("USP_Notice_ForSpecificTeacher");
    }

    public DataTable UnseenNoticeForTeacher(int teacherId)
    {
        dm.AddParameteres("@TeacherId", teacherId);
        return dm.ExecuteQuery("USP_Notice_UnseenNoticeForTeacher");
    }

    public DataTable GetById(int Id)
    {
        dm.AddParameteres("@Id", Id);
        return dm.ExecuteQuery("USP_Notice_GetById");
    }
    public DataTable GetNoticeForStudent(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Notice_GetNoticeForStudent");
    }

    public int UpdateSeenInfoStu(int id, bool isSeen)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@IsSeen", isSeen);
        return dm.ExecuteNonQuery("USP_Notice_Stu_UpdateSeenInfo");
    }

    public int UpdateSeenInfoTeach(int id, bool isSeen)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@IsSeen", isSeen);
        return dm.ExecuteNonQuery("USP_Notice_Teacher_UpdateSeenInfo");
    }
}