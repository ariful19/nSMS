using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalCommon
/// </summary>
public class dalCommon
{
    DatabaseManager dm = new DatabaseManager();
    public dalCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetRegistrationNo()
    {
        return dm.ExecuteQuery("USP_RegistrationNo");
    }
    public DataTable GetSerialNo()
    {
        return dm.ExecuteQuery("USP_SerialNo");
    }
    public DataTable GetTeacherPin()
    {
        return dm.ExecuteQuery("USP_TeacherPIN");
    }
    public DataTable GetStudentId()
    {
        return dm.ExecuteQuery("USP_GetAutoStudentID");
    }

    public DataTable GetStudentIdforZigatola()
    {
        return dm.ExecuteQuery("USP_GetAutoStudentIDforZigatola");
    }

 public DataTable GetStudentIdForCollegeScience()
    {
        return dm.ExecuteQuery("USP_GetAutoCollegeStudentScienceId");
    }

    public DataTable GetStudentIdForCollegeBusiness()
    {
        return dm.ExecuteQuery("USP_GetAutoCollegeStudentBusinessId");
    }

 public DataTable GetAutoStudentIdforAdaborEnglishMedium()
    {
        return dm.ExecuteQuery("USP_GetAutoStudentIDAdaborEMedium");
    }

    public DataTable GetAutoStudentIdforZigatolaEnglishMedium()
    {
        return dm.ExecuteQuery("USP_GetAutoStudentIDZigatolaEMedium");
    }

    public int UpdateRegistrationNo()
    {
        return dm.ExecuteNonQuery("USP_RegistrationNo_Update");
    }
    public DataTable DropdownByCondition(string table, string tableColumn, string whereby)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column", tableColumn);
        dm.AddParameteres("@Condition", whereby);
        return dm.ExecuteQuery("USP_GetById");
    }

    public DataTable DropdownByConditionWithTwo(string table, string column1, string column2, string whereby1, string whereby2)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column1", column1);
        dm.AddParameteres("@Column2", column2);
        dm.AddParameteres("@Condition1", whereby1);
        dm.AddParameteres("@Condition2", whereby2);
        return dm.ExecuteQuery("USP_GetByIdWithTwo");
    }

    public DataTable DropdownByConditionWithThree(string table, string column1, string column2, string column3, string whereby1, string whereby2, string whereby3)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column1", column1);
        dm.AddParameteres("@Column2", column2);
        dm.AddParameteres("@Column3", column3);
        dm.AddParameteres("@Condition1", whereby1);
        dm.AddParameteres("@Condition2", whereby2);
        dm.AddParameteres("@Condition3", whereby3);
        return dm.ExecuteQuery("USP_GetByIdWithThree");
    }
    public DataTable LoadDropdown(string table)
    {
        dm.AddParameteres("@Table", table);
        return dm.ExecuteQuery("USP_Get_All");
    }
    public DataTable DropdownByCondition(string table, string column1, string column2, string column3, string column4, string whereby1, string whereby2, string whereby3, string whereby4)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column1", column1);
        dm.AddParameteres("@Column2", column2);
        dm.AddParameteres("@Column3", column3);
        dm.AddParameteres("@Column4", column4);
        dm.AddParameteres("@Condition1", whereby1);
        dm.AddParameteres("@Condition2", whereby2);
        dm.AddParameteres("@Condition3", whereby3);
        dm.AddParameteres("@Condition4", whereby4);
        return dm.ExecuteQuery("USP_GetByIdWithFour");
    }
    public int InsertPrefix(int id, string prefix, int yearFormat, int styleId, bool isRegNoUse)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Prefix", prefix);
        dm.AddParameteres("@YearFormat", yearFormat);
        dm.AddParameteres("@StyleId", styleId);
        dm.AddParameteres("@IsRegNoUse", isRegNoUse);
        
        DataTable dt = dm.ExecuteQuery("USP_Medium_Update");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int InsertUseRollNo(int id, bool isRollNoUse)
    {
        dm.AddParameteres("@Id", id);

        dm.AddParameteres("@IsRollNoUse", isRollNoUse);

        DataTable dt = dm.ExecuteQuery("USP_UseRollNo_Update");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetMediumInfo(string tableName)
    {
        dm.AddParameteres("@Table", tableName);
        return dm.ExecuteQuery("USP_Get_All"); 
    }

    public DataTable GetMediumById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Medium_GetById");        
    }

  public DataTable GetVersionById(int id)
    {
        dm.AddParameteres("@Id",id);
        return dm.ExecuteQuery("USP_Version_GetById");
    }

    public DataTable GetTransactionNumber()
    {
        return dm.ExecuteQuery("USP_TransactionNumber");
    }

    public DataTable GetMedium()
    {
        
        return dm.ExecuteQuery("USP_Medium_GetInfo"); 
    }
    public DataTable GetTeacherPinFormat()
    {

        return dm.ExecuteQuery("USP_TeacherPin_GetInfo"); 
    }
    public string RegistrationNoPrefix(int mId)
    {
        string regNo = "";
        string idNo = Controller.RegistrationNo().ToString();
        DataTable dt = new dalCommon().GetMediumById(mId);
        //if (dtNo.Rows.Count > 0)
        //    idNo = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? "1" : dt.Rows[0][0].ToString());
        //else
        //{
        //    idNo = "1";
        //}



        regNo = idNo;
        if (dt.Rows.Count > 0)
        {
            string prefix = string.IsNullOrEmpty(dt.Rows[0]["Prefix"].ToString()) ? "" : dt.Rows[0]["Prefix"].ToString();

            string year = string.IsNullOrEmpty(dt.Rows[0]["YearFormate"].ToString()) ? "" : dt.Rows[0]["YearFormate"].ToString();
            int styleId = (!string.IsNullOrEmpty(dt.Rows[0]["StyleId"].ToString())) ? Convert.ToInt32(dt.Rows[0]["StyleId"]) : 0;
            if (styleId == 1)
                regNo = prefix + "-" + idNo;

            else if (styleId == 2)
                regNo = prefix + "" + idNo;
            else if (styleId == 3)
                regNo = prefix + "-" + year + "-" + idNo;
            else if (styleId == 4)
                regNo = prefix + "" + year + "-" + idNo;
            else if (styleId == 5)
                regNo = prefix + "" + year + "" + idNo;
            else if (styleId == 6)
                regNo = prefix + "-" + idNo + "-" + year;
            else if (styleId == 7)
                regNo = prefix + "-" + idNo + "" + year;
            else if (styleId == 8)
                regNo = prefix + "" + idNo + "" + year;
            else if (styleId == 9)
                regNo = prefix + "-" + year + "-" + idNo;
            else if (styleId == 10)
                regNo = prefix + "" + year + "-" + idNo;
            else if (styleId == 11)
                regNo = prefix + "" + year + "" + idNo;
            else if (styleId == 12)
                regNo = prefix + "-" + idNo + "-" + year;
            else if (styleId == 13)
                regNo = prefix + "-" + idNo + "" + year;
            else if (styleId == 14)
                regNo = prefix + "" + idNo + "" + year;
            else
                regNo = idNo;

        }

        return regNo;
    }
    public DataTable DropdownByCondition(string table, string column1, string column2, string column3, string column4, string column5, string whereby1, string whereby2, string whereby3, string whereby4, string whereby5)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column1", column1);
        dm.AddParameteres("@Column2", column2);
        dm.AddParameteres("@Column3", column3);
        dm.AddParameteres("@Column4", column4);
        dm.AddParameteres("@Column5", column5);
        dm.AddParameteres("@Condition1", whereby1);
        dm.AddParameteres("@Condition2", whereby2);
        dm.AddParameteres("@Condition3", whereby3);
        dm.AddParameteres("@Condition4", whereby4);
        dm.AddParameteres("@Condition5", whereby5);
        return dm.ExecuteQuery("USP_GetByIdWithFive");
    }
    public DataTable DropdownByCondition(string table, string column, string column1, string column2, string column3, string column4, string column5, string column6, string column7, string whereby, string whereby1, string whereby2, string whereby3, string whereby4, string whereby5, string whereby6)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column", column);
        dm.AddParameteres("@Column1", column1);
        dm.AddParameteres("@Column2", column2);
        dm.AddParameteres("@Column3", column3);
        dm.AddParameteres("@Column4", column4);
        dm.AddParameteres("@Column5", column5);
        dm.AddParameteres("@Column6", column6);
        dm.AddParameteres("@Column7", column7);
        dm.AddParameteres("@Condition", whereby);
        dm.AddParameteres("@Condition1", whereby1);
        dm.AddParameteres("@Condition2", whereby2);
        dm.AddParameteres("@Condition3", whereby3);
        dm.AddParameteres("@Condition4", whereby4);
        dm.AddParameteres("@Condition5", whereby5);
        dm.AddParameteres("@Condition6", whereby6);

        return dm.ExecuteQuery("USP_GetByIdWithSix");
    }




    public int InsertTeacherPin(int id, string prefix, int yearFormat, int styleId, bool isRegNoUse)
    {      DataTable dt=new DataTable(); 
        dm.AddParameteres("@Prefix", prefix);
        dm.AddParameteres("@YearFormat", yearFormat);
        dm.AddParameteres("@StyleId", styleId);
        dm.AddParameteres("@IsRegNoUse", isRegNoUse);
        if (id == 0)
            dt = dm.ExecuteQuery("USP_TeacherPin_Insert");
        else
        {
            dm.AddParameteres("@Id", id);
            dt = dm.ExecuteQuery("USP_TeacherPin_Update");
        }
        return Convert.ToInt32(dt.Rows[0][0]);
    }
  public DataTable GetBillNo()
    {
        return dm.ExecuteQuery("USP_GetBillNo");
    }
}




