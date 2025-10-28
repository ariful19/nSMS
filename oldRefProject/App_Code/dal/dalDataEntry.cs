using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalDataEntry
/// </summary>
public class dalDataEntry
{
    DatabaseManager dm = new DatabaseManager();
	public dalDataEntry()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetStudentInfo()
    {
        //return dm.ExecuteQuery("USP_GetAllStudent_Queen");
        return dm.ExecuteQuery("USP_GetAllStudent_Queen_ZTL");
    }
    public int InsertStudent1(int PersonId, string regNo, DateTime admissionDate, string createBy, DateTime createDate)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@RegNo", regNo);
        dm.AddParameteres("@AddmissionDate", admissionDate);
        dm.AddParameteres("@CreatedBy", createBy);
        dm.AddParameteres("@CreatedDate", createDate);
        DataTable dt = dm.ExecuteQuery("USP_StudentData_Insert_Queen");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int InsertStudent(string name, string fName, string mName, string regNo, DateTime dob, int genderId,int religionId, string bloodGroup,bool freedomFighter,bool tribal,bool pDefect,int FqId,int FproId,int MqId,int MproId)
    {
        dm.AddParameteres("@NameEng", name);
        dm.AddParameteres("@FatherNameEng", fName);
        dm.AddParameteres("@MotherNameEng", mName);
        dm.AddParameteres("@UserName", regNo);
        dm.AddParameteres("@DateofBirth", dob);
        dm.AddParameteres("@GenderId", genderId);
        dm.AddParameteres("@ReligionId", religionId);
        dm.AddParameteres("@BloodGroup", bloodGroup);
        dm.AddParameteres("@IsFreedomFighter", freedomFighter);
        dm.AddParameteres("@IsTribal", tribal);
        dm.AddParameteres("@IsPhysicallyDefect", pDefect);
        dm.AddParameteres("@FatherQualificationId", FqId);
        dm.AddParameteres("@FatherProfessionId", FproId);
        dm.AddParameteres("@MotherQualificationId", MqId);
        dm.AddParameteres("@MotherProfessionId", MproId);

        DataTable dt = dm.ExecuteQuery("USP_PersonData_Insert_Queen");
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    public int InsertAddress(int PersonId, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PresentAddressInsert_Queen");
    }
}