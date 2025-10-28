using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalPaymentType
/// </summary>
public class dalPaymentType
{
    public dalPaymentType()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    DatabaseManager dm = new DatabaseManager();
    public int Insert(string name,bool isMonthly, bool isDefault, string CreatedBy)
    {
        dm.AddParameteres("@Type", name);
        dm.AddParameteres("@IsMonthly", isMonthly);
        dm.AddParameteres("@IsDefault", isDefault);
        dm.AddParameteres("@CreatedBy", CreatedBy);       
        dm.AddParameteres("@CreatedDate", DateTime.Now);
        DataTable dt = dm.ExecuteQuery("USP_PaymentType_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name, bool isMonthly, bool isDefault, string UpdatedBy)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Type", name);
        dm.AddParameteres("@IsMonthly", isMonthly);
        dm.AddParameteres("@IsDefault", isDefault);        
        dm.AddParameteres("@UpdatedBy", UpdatedBy);
        dm.AddParameteres("@UpdatedDate", DateTime.Now);
        return dm.ExecuteNonQuery("USP_PaymentType_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_PaymentType_GetById");
    }

    public DataTable GetByYear(int year, string month, int classId)
    {
        dm.AddParameteres("@YearId", year);
        dm.AddParameteres("@Month", month);
        dm.AddParameteres("@ClassId", classId);
        return dm.ExecuteQuery("USP_PaymentType_GetByYear");
    }
    public DataTable PaymentForStudentByCriteria(int StudentId, string criteria)
    {
        dm.AddParameteres("@StudentId", StudentId);
        dm.AddParameteres("@Criteria", criteria);
      
        return dm.ExecuteQuery("USP_Payment_Student_GetByCriteria");
    }


    public DataTable GetAllClassPaymentByYear(int year, string month, int classId)
    {
        string sql = @"	select *,
	(select SUM(Amount) from fee_PaymentForClass where fee_PaymentForClass.YearId='{0}' and fee_PaymentForClass.ClassId={1} and fee_PaymentForClass.Month < {2}) as Total
	 from fee_PaymentForClass 
	inner join fee_PaymentType on fee_PaymentForClass.PaymentTypeId=fee_PaymentType.Id 
	where fee_PaymentForClass.YearId='{0}' and fee_PaymentForClass.ClassId={1} and fee_PaymentForClass.Month < {2}";
        string filter = string.Format(sql, year, classId, month);
        return dm.NewExecuteQuery(filter);
    }
    public int PaymentInsert(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {
        DataSet ds = new DataSet("dsPayment");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@CreatedDate", CreatedDate);
        dm.AddParameteres("@CreatedBy", CreatedBy);
        dm.AddParameteres("@UpdateID", CreatedBy);
        return dm.ExecuteNonQuery("USP_Payment_InsertForClass");
    }

    public int PaymentInformationInsert(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {
        int result = 0;
        foreach (DataRow row in dt.Rows)
        {
            int paymentForClassID = int.Parse(row["UpdatedID"].ToString());
            decimal amount = int.Parse(row["Amount"].ToString());
            int month = int.Parse(row["MID"].ToString());
            int year = int.Parse(row["YId"].ToString());
            int SubjectId = int.Parse(row["SID"].ToString());
            int paymentTypeID = int.Parse(row["PayID"].ToString());
            int startMonth = int.Parse(row["StrMonth"].ToString());
            int endMonth = int.Parse(row["EndMonth"].ToString());
            

            if (paymentForClassID > 0)
            {
                dm.ClearParameters();

                dm.AddParameteres("@id", paymentForClassID);
                dm.AddParameteres("@YearID", year);
                dm.AddParameteres("@Month", month);
                dm.AddParameteres("@Amount", amount);
                
                dm.AddParameteres("@UpdatedBy", CreatedBy);
                dm.AddParameteres("@UpdatedDate", DateTime.Now);
                result= dm.ExecuteNonQuery("USP_PaymentForClassUpdate");
            }
            else
            {
                dm.ClearParameters();

                dm.AddParameteres("@YearID", year);
                dm.AddParameteres("@Month", month);
                dm.AddParameteres("@Amount", amount);
                dm.AddParameteres("@PaymentTypeID", paymentTypeID);
                dm.AddParameteres("@SubjectId", SubjectId);
                dm.AddParameteres("@StartMonth", startMonth);
                dm.AddParameteres("@EndMonth", endMonth);
                dm.AddParameteres("@CreatedBy", CreatedBy);
                dm.AddParameteres("@CreatedDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Insert_fee_PaymentForClass");
            }

        }
        return result;
    }
    public int PaymentFeeInsert(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {
        int result = 0;
        foreach (DataRow row in dt.Rows)
        {
            int paymentForClassID = int.Parse(row["UpdatedID"].ToString());
            decimal amount = decimal.Parse(row["Amount"].ToString());
            int paymentTypeID = int.Parse(row["PayID"].ToString());
            int year = int.Parse(row["YearID"].ToString());
            int mediumID = int.Parse(row["MediumId"].ToString());
            int campus = int.Parse(row["CampusID"].ToString());
            int classId = int.Parse(row["ClassID"].ToString());
            int groupId = int.Parse(row["GroupID"].ToString());
            int shiftId = int.Parse(row["ShiftID"].ToString());
            int sectionId = int.Parse(row["SectionID"].ToString());
            int startMonth = int.Parse(row["StrMonth"].ToString());
            int endMonth = int.Parse(row["EndMonth"].ToString());           

            if (paymentForClassID > 0)
            {
                dm.ClearParameters();
                dm.AddParameteres("@id", paymentForClassID);
                dm.AddParameteres("@YearId", year);
                dm.AddParameteres("@MediumId", mediumID);
                dm.AddParameteres("@CampusId", campus);
                dm.AddParameteres("@ClassId", classId);
                dm.AddParameteres("@GroupId", groupId);
                dm.AddParameteres("@ShiftId", shiftId);
                dm.AddParameteres("@SectionId", sectionId);  
                dm.AddParameteres("@Amount", amount);
                dm.AddParameteres("@StartMonth", startMonth);
                dm.AddParameteres("@EndMonth", endMonth);                
                dm.AddParameteres("@UpdateBy", CreatedBy);
                dm.AddParameteres("@UpdateDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Payment_FeeUpdate");
            }
            else
            {
                dm.ClearParameters();
                dm.AddParameteres("@YearId", year);
                dm.AddParameteres("@MediumId", mediumID);
                dm.AddParameteres("@CampusId", campus);
                dm.AddParameteres("@ClassId", classId);
                dm.AddParameteres("@GroupId", groupId);
                dm.AddParameteres("@ShiftId", shiftId);
                dm.AddParameteres("@SectionId", sectionId);
                dm.AddParameteres("@PaymentTypeId", paymentTypeID);
                dm.AddParameteres("@Amount", amount);
                dm.AddParameteres("@StartMonth", startMonth);
                dm.AddParameteres("@EndMonth", endMonth);                
                dm.AddParameteres("@CreatedBy", CreatedBy);
                dm.AddParameteres("@CreatedDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Payment_Insertfee");
            }

        }
        return result;
    }

    public int StudentPaymentInformationInsert(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {
        int result = 0;
        foreach (DataRow row in dt.Rows)
        {
            int paymentForClassID = int.Parse(row["UpdatedID"].ToString());
            decimal amount = decimal.Parse(row["Amount"].ToString());
            int month = int.Parse(row["MID"].ToString());            
            int paymentTypeID = int.Parse(row["PayID"].ToString());
            int studentToClassID = int.Parse(row["StuSId"].ToString());          
            int insertUpdateId = int.Parse(row["InsertUpdateID"].ToString());
            int startMonth = int.Parse(row["StrMonth"].ToString());
            int endMonth = int.Parse(row["EndMonth"].ToString());
            string remarks = row["Remarks"].ToString();

            if (insertUpdateId > 0)
            {
                dm.ClearParameters();

                dm.AddParameteres("@id", paymentForClassID);              
                dm.AddParameteres("@Month", month);             
                dm.AddParameteres("@Amount", amount); 
                dm.AddParameteres("@StartMonth", startMonth);
                dm.AddParameteres("@EndMonth", endMonth);
                dm.AddParameteres("@Remarks", remarks);
                dm.AddParameteres("@UpdatedBy", CreatedBy);
                dm.AddParameteres("@UpdatedDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_PaymentForStudentUpdate");
            }
            else
            {
                dm.ClearParameters();

                dm.AddParameteres("@StudentToClassId", studentToClassID);             
                dm.AddParameteres("@Month", month);
                dm.AddParameteres("@Amount", amount);         
                dm.AddParameteres("@PaymentTypeID", paymentTypeID);       
                dm.AddParameteres("@StartMonth", startMonth);
                dm.AddParameteres("@EndMonth", endMonth);
                dm.AddParameteres("@Remarks", remarks);
                dm.AddParameteres("@CreatedBy", CreatedBy);
                dm.AddParameteres("@CreatedDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Insert_fee_PaymentForStudent");
            }

        }
        return result;
    }
    public DataTable GetPaymentByYearMonthAndSubject(int year, string month, int subjectID, int studentId, int termId)
    {
         DataTable dt =new DataTable();
         dm.AddParameteres("@StudentId", studentId);
         dm.AddParameteres("@YearId", year);
         dm.AddParameteres("@Month", month);
         dm.AddParameteres("@SubjectId", subjectID);
         dm.AddParameteres("@TermId", termId);

         dt = dm.ExecuteQuery("USP_PaymentType_GetByStudentId");
        
              if (dt.Rows.Count == 0)
              {
                  string sql = "Select '0' as PaymentToClassID, Id , PaymentType, '0' as Amount from fee_PaymentType";
                  dt = dm.NewExecuteQuery(sql);
              }
            

        return dt;
    }
    public DataTable GetPaymentByYearMonthAndClass(int year, string month, int subjectID)
    {
        string sql = @" Select * from (
                    select fee_PaymentForClass.Id as PaymentToClassID, PaymentTypeID as Id, PaymentType, Amount,(select SUM(Amount) from fee_PaymentForClass where fee_PaymentForClass.YearId='{0}' and fee_PaymentForClass.SubjectId={1} and fee_PaymentForClass.Month={2}) as Total
	                     from fee_PaymentForClass 
                    join fee_PaymentType on fee_PaymentForClass.PaymentTypeId=fee_PaymentType.Id 	
	                    where fee_PaymentForClass.YearId='{0}' and fee_PaymentForClass.SubjectId={1} and fee_PaymentForClass.Month={2}
	                    union
	                    Select '0' PaymentToClassID , Id, PaymentType, '0' as Amount ,'0' as Total from fee_PaymentType
	                    Where id Not in (Select PaymentTypeId from fee_PaymentForClass where fee_PaymentForClass.YearId='{0}' and fee_PaymentForClass.SubjectId={1} and fee_PaymentForClass.Month={2})
	                    ) tbl Order by Id";

        string sqlFilter = string.Format(sql, year, subjectID, month);

        DataTable dt = dm.NewExecuteQuery(sqlFilter);

        if (dt.Rows.Count == 0)
        {
            sql = "Select '0' as PaymentToClassID, Id , PaymentType, '0' as Amount from fee_PaymentType";
            dt = dm.NewExecuteQuery(sql);
        }

        return dt;
    }
    public DataTable GetPayment(int year,int mediumId,int campusId,int classId, int groupId, int shiftId, int sectionId)
    {
          DataTable dt =new DataTable();
          dm.AddParameteres("@YearId", year);
          dm.AddParameteres("@MediumId", mediumId);
          dm.AddParameteres("@CampusId", campusId);
          dm.AddParameteres("@ClassId", classId);
          dm.AddParameteres("@GroupId", groupId);
          dm.AddParameteres("@ShiftId", shiftId);
          dm.AddParameteres("@SectionId", sectionId);

          dt = dm.ExecuteQuery("USP_PaymentFee_Get");
        
              if (dt.Rows.Count == 0)
              {
                  string sql = "Select '0' as PaymentToClassID, Id , PaymentType, '0' as Amount from fee_PaymentType";
                  dt = dm.NewExecuteQuery(sql);
              }
            

        return dt;
    }


    public DataTable GetDefault()
    {
        return dm.ExecuteQuery("USP_PaymentType_GetDefault");
    }

    public DataTable GetPaymentWithoutDefault(int StudentToClassId, int year, int mediumId, int campusId, int classId, int groupId, int shiftId, int sectionId, bool IsDefault, bool IsMonthly)
    {
        dm.AddParameteres("@YearId", year);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@ShiftId", shiftId);
        dm.AddParameteres("@SectionId", sectionId);
        dm.AddParameteres("@StudentToClassId", StudentToClassId);
        dm.AddParameteres("@IsDefault", IsDefault);
        dm.AddParameteres("@IsMonthly", IsMonthly);
        return dm.ExecuteQuery("USP_PaymentFee_GetWithouDefault");
    }

    public DataTable GetTermId(int yearId, int classId, int studentId)
    {
        string sql = "Select MediumId,GroupId,ShiftId,SectionId,CampusId from er_StudentToClass where Id=" + studentId + " and ClassId=" + classId + " and Year=" + yearId;
        DataTable dt= dm.NewExecuteQuery(sql);
        return dt;
    }
    public DataTable GetPaymentTypeByIsDefault(bool IsMonthly,bool IsDefault)
    {
        dm.AddParameteres("@IsMonthly", IsMonthly);
        dm.AddParameteres("@IsDefault", IsDefault);
        return dm.ExecuteQuery("USP_PaymentType_GetByIsDefault");
    }
    public DataTable GetPaymentTypeByIsMonthly(bool IsMonthly)
    {
        dm.AddParameteres("@IsMonthly", IsMonthly);
        return dm.ExecuteQuery("USP_PaymentType_GetByIsMonthly");
    }
}