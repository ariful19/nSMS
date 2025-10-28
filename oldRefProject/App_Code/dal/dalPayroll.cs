using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for dalPayroll
/// </summary>
public class dalPayroll
{
    DatabaseManager dm = new DatabaseManager();
    public dalPayroll()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InserAllowanceToType(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {
        DataSet ds = new DataSet("dsAllowance");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@CreatedDate", CreatedDate);
        dm.AddParameteres("@CreatedBy", CreatedBy);
        return dm.ExecuteNonQuery("USP_Payroll_InserAllowanceToType");
    }


    public int InserDeductionToType(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {
        DataSet ds = new DataSet("dsDeduction");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@CreatedDate", CreatedDate);
        dm.AddParameteres("@CreatedBy", CreatedBy);
        return dm.ExecuteNonQuery("USP_Payroll_InserDeductionToType");
    }
    public DataTable GetAllowanceBytypeId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_GetAllowanceBytypeId");
    }

    public DataTable GetPaymentBySomething(string payrollType, int yearId, int designationId)
    {
        dm.AddParameteres("@PayrollType", payrollType);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@DesignationId", designationId);
        return dm.ExecuteQuery("USP_GetPaymentBySomething");
    }
    public DataTable GetDeductionBytypeId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_GetDeductionBytypeId");
    }


    public DataTable GetPayrollForTeacher(string pin)
    {
        dm.AddParameteres("@Pin", pin);
        return dm.ExecuteQuery("USP_Payroll_GetPayrollForTeacher");
    }

    public int InsertEmployeeSalary(int personId, int teacherId, int employeeId, int campusId, string createdBy, decimal basicSalary, decimal allowance, decimal increment, decimal penalty, decimal dues, decimal payableAmount, string updatedBy, int monthId, int yearId, string remarks)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@TeacherId", teacherId);
        dm.AddParameteres("@EmployeeId", employeeId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@CreatedDate", DateTime.Now);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@Basic", basicSalary);
        dm.AddParameteres("@Allowance", allowance);
        dm.AddParameteres("@Increment", increment);
        dm.AddParameteres("@Penalty", penalty);
        dm.AddParameteres("@DueAmount", dues);
        dm.AddParameteres("@TotalPayable", payableAmount);
        dm.AddParameteres("@UpdatedDate", DateTime.Now);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        dm.AddParameteres("@MonthId", monthId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@Remarks", remarks);

        DataTable dt = dm.ExecuteQuery("USP_Insert_Employee_Salary");
        return Convert.ToInt32(dt.Rows[0][0]);
    }


    public int InsertPayrolType(string name, string createdBy)
    {
        dm.AddParameteres("@Type", name);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", DateTime.Now);
        DataTable dt = dm.ExecuteQuery("USP_InsertPayrolType");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int PayrollAmountInsert(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {
        int result = 0;
        foreach (DataRow row in dt.Rows)
        {
            int payrollForDesignationID = int.Parse(row["UpdatedID"].ToString());
            decimal amount = decimal.Parse(row["Amount"].ToString());
            int payrollTypeID = int.Parse(row["PayID"].ToString());
            int year = int.Parse(row["YearID"].ToString());
            int campus = int.Parse(row["CampusID"].ToString());
            int gradeId = int.Parse(row["GradeId"].ToString());
            int levelId = int.Parse(row["LevelId"].ToString());
            int startMonth = int.Parse(row["StrMonth"].ToString());
            int endMonth = int.Parse(row["EndMonth"].ToString());

            if (payrollForDesignationID > 0)
            {
                dm.ClearParameters();
                dm.AddParameteres("@id", payrollForDesignationID);
                dm.AddParameteres("@YearId", year);
                dm.AddParameteres("@CampusId", campus);
                dm.AddParameteres("@GradeId", gradeId);
                dm.AddParameteres("@LevelId", levelId);
                dm.AddParameteres("@PayrollTypeID", payrollTypeID);
                dm.AddParameteres("@Amount", amount);
                dm.AddParameteres("@StartMonth", startMonth);
                dm.AddParameteres("@EndMonth", endMonth);
                dm.AddParameteres("@UpdateBy", CreatedBy);
                dm.AddParameteres("@UpdateDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Upadte_PayrollAmount");
              //  result = dm.ExecuteNonQuery("USP_Payroll_AmountUpdate");
            }
            else
            {
                dm.ClearParameters();
                dm.AddParameteres("@YearId", year);
                dm.AddParameteres("@CampusId", campus);
                dm.AddParameteres("@GradeId", gradeId);
                dm.AddParameteres("@LevelId", levelId);
                dm.AddParameteres("@PayrollTypeID", payrollTypeID);
                dm.AddParameteres("@Amount", amount);
                dm.AddParameteres("@StartMonth", startMonth);
                dm.AddParameteres("@EndMonth", endMonth);
                dm.AddParameteres("@CreatedBy", CreatedBy);
                dm.AddParameteres("@CreatedDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Payroll_InsertAmount");
               // result = dm.ExecuteNonQuery("USP_Insert_EmployeePayment");
            }

        }
        return result;
    }

    public DataTable GetPaymentforPayroll(int year, int campusId, int gradeId, int levelId)
    {
        DataTable dt = new DataTable();
        dm.AddParameteres("@YearId", year);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@GradeId", gradeId);
        dm.AddParameteres("@LevelId", levelId);

        dt = dm.ExecuteQuery("USP_PayrollAmount_Get");

        if (dt.Rows.Count == 0)
        {
            string sql = "Select '0' as PayrollToDesignationID, Id , PayrollType, '0' as Amount from tbl_PayrollType";
            dt = dm.NewExecuteQuery(sql);
        }


        return dt;
    }


    public int EmployeePaymentIndividualInsert(DataTable dt, int yearId, int campusId, int designationId, string CreatedBy, DateTime CreatedDate)
    {

        using (var conn = DatabaseManager.Connection)
        {
            conn.Open();
            int result = 0;
            foreach (DataRow row in dt.Rows)
            {
                int pId = int.Parse(row["PersonId"].ToString());
                int empId = int.Parse(row["EmployeeId"].ToString());
                int payrollForDesignationID = int.Parse(row["UpdatedID"].ToString());
                decimal amount = decimal.Parse(row["Amount"].ToString());
                int month = int.Parse(row["MID"].ToString());
                int payrollTypeID = int.Parse(row["PayrollID"].ToString());
                int insertUpdateId = int.Parse(row["InsertUpdateID"].ToString());

                int startMonth = int.Parse(row["StrMonth"].ToString());
                int endMonth = int.Parse(row["EndMonth"].ToString());
                int gradeId = int.Parse(row["GradeId"].ToString());
                int levelId = int.Parse(row["LevelId"].ToString());

                var cmd = new SqlCommand("", conn);
                if (insertUpdateId > 0)
                {
                    //dm.ClearParameters();
                    cmd.CommandText = @"Update tbl_EmployeeSalary Set

 Amount=@Amount,
 StartMonth=@StartMonth,
 EndMonth=@EndMonth,
 UpdatedBy=@UpdatedBy,
 UpdatedDate=@UpdatedDate
 
 where EmployeeId=@Id and PayrollTypeId=@PayrollTypeId and MonthId=@MonthId and YearId=@YearId";

                    cmd.Parameters.AddWithValue("@Id", empId);
                    cmd.Parameters.AddWithValue("@MonthId", month);
                    cmd.Parameters.AddWithValue("@YearId", yearId);
                    cmd.Parameters.AddWithValue("@PayrollTypeId", payrollTypeID);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@StartMonth", startMonth);
                    cmd.Parameters.AddWithValue("@EndMonth", endMonth);
                    cmd.Parameters.AddWithValue("@UpdatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                    result += cmd.ExecuteNonQuery();
                }
                else
                {
                    string sql = @"insert into tbl_EmployeeSalary(PersonId,EmployeeId,MonthId,YearId,StartMonth,EndMonth,PayrollTypeId,Amount,DesignationId,CampusId,CreatedBy,CreatedDate,GradeId,LevelId) values (@PersonId,@EmployeeId,@MonthId,@YearId,@StartMonth,@EndMonth,@PayrollTypeId,@Amount,@DesignationId,@CampusId,@CreatedBy,@CreatedDate,@GradeId,@LevelId)";
                    cmd.Parameters.AddWithValue("@PersonId", pId);
                    cmd.Parameters.AddWithValue("@EmployeeId", empId);
                    cmd.Parameters.AddWithValue("@MonthId", month);
                    cmd.Parameters.AddWithValue("@YearId", yearId);
                    cmd.Parameters.AddWithValue("@StartMonth", startMonth);
                    cmd.Parameters.AddWithValue("@EndMonth", endMonth);
                    cmd.Parameters.AddWithValue("@PayrollTypeId", payrollTypeID);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@DesignationId", designationId);
                    cmd.Parameters.AddWithValue("@CampusId", campusId);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    cmd.Parameters.AddWithValue("@GradeId", gradeId);
                    cmd.Parameters.AddWithValue("@LevelId", levelId);


                    var exists = new SqlCommand("select top(1) * from tbl_EmployeeSalary where EmployeeId=@EmployeeId and MonthId=@MonthId and YearId=@YearId and PayrollTypeId=@PayrollTypeId", conn);
                    exists.Parameters.AddWithValue("@EmployeeId", empId);
                    exists.Parameters.AddWithValue("@MonthId", month);
                    exists.Parameters.AddWithValue("@YearId", yearId);
                    exists.Parameters.AddWithValue("@PayrollTypeId", payrollTypeID);
                    var x = exists.ExecuteReader();
                    bool hasRows = x.HasRows;
                    x.Close();

                    if (!hasRows)
                    {
                        cmd.CommandText = sql;
                        result += cmd.ExecuteNonQuery();
                    }
                }
            }
            conn.Close();
            return result;
        }
    }




    public DataTable GetPaymentSetupByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_GetPaymentSetupByCriteria");
    }

    public DataTable GetPaymentByPayroll(int payrollTypeId, int monthId, int yearId, int gradeId, int levelId, int campusId, int employeeId)
    {
        dm.AddParameteres("@PayrollTypeId", payrollTypeId);
        dm.AddParameteres("@MonthId", monthId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@GradeId", gradeId);
        dm.AddParameteres("@LevelId", levelId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@EmployeeId", employeeId);
        return dm.ExecuteQuery("USP_GetPaymentByPayroll");
    }

    public DataTable GetPaymentByIndividual(int employeeId, int yearId, int gradeId,int levelId, int campusId, int monthId, int payrollTypeId)
    {

        dm.AddParameteres("@EmployeeId", employeeId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@GradeId", gradeId);
        dm.AddParameteres("@LevelId", levelId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@MonthId", monthId);
        dm.AddParameteres("@PayrollTypeId", payrollTypeId);
        return dm.ExecuteQuery("USP_Payment_GetPaymentIndividual");
    }

    public DataTable GetPaymentByEmployee(int empId, int yearId)
    {
        dm.AddParameteres("EmployeeId", empId);
        dm.AddParameteres("YearId", yearId);
        return dm.ExecuteQuery("USP_GetPaymentByEmployee");
    }

    public DataTable GetEmployeeSalaryMonthly(int empId, int yearId, int monthId, int gradeId, int levelId)
    {
        dm.AddParameteres("EmployeeId", empId);
        dm.AddParameteres("YearId", yearId);
        dm.AddParameteres("MonthId", monthId);
        dm.AddParameteres("GradeId", gradeId);
        dm.AddParameteres("LevelId", levelId);
        return dm.ExecuteQuery("USP_Get_EmployeeSalaryMonthly");
    }
    public int InsertEmployeeSalaryPaid(int empId, int gradeId,int levelId,int campusId,int monthId,int yearId,int payrollTypeId ,string accountNum, decimal total, decimal givenAmount, decimal due ,string billNo, string createdBy, DateTime createdDate)
    {
        dm.AddParameteres("EmployeeId", empId);
        dm.AddParameteres("GradeId", gradeId);
        dm.AddParameteres("LevelId", levelId);
        dm.AddParameteres("CampusId", campusId);
        dm.AddParameteres("MonthId", monthId);
        dm.AddParameteres("YearId", yearId);
        dm.AddParameteres("PayrollTypeId", payrollTypeId);
        dm.AddParameteres("AccountNumber", accountNum);
        dm.AddParameteres("TotalAmount", total);
        dm.AddParameteres("GivenAmount", givenAmount);
        dm.AddParameteres("DueAmount", due);
        dm.AddParameteres("BillNo", billNo);
        dm.AddParameteres("CreatedBy", createdBy);
        dm.AddParameteres("CreatedDate", createdDate);
        return dm.ExecuteNonQuery("USP_InsertEmployeeSalaryPaid");
    }

    public DataTable GetEmployeePaidSalary(string criteria)
    {
        dm.AddParameteres("Criteria", criteria);
        return dm.ExecuteQuery("USP_GetEmployeePaidSalary");
    }

    public DataTable GetEmployeePaidSalaryMonthly(int employeeId, int yearId, int monthId)
    {
        dm.AddParameteres("EmployeeId", employeeId);
        dm.AddParameteres("YearId", yearId);
        dm.AddParameteres("MonthId", monthId);
        return dm.ExecuteQuery("USP_Get_EmployeePaidSalaryMonthly");
    }

    public DataTable GetEmployeeSalaryDetails(int yearId, int campusId, int gradeId)
    {
        dm.AddParameteres("YearId", yearId);
        dm.AddParameteres("CampusId", campusId);
        dm.AddParameteres("GradeId", gradeId);
        return dm.ExecuteQuery("USP_GetEmployeeSalaryDetails");
    }

    public DataTable GetSalaryDetails(int yearId, int monthId, int campusId)
    {
        dm.AddParameteres("YearId", yearId);
        dm.AddParameteres("MonthId", monthId);
        dm.AddParameteres("CampusId", campusId);
        return dm.ExecuteQuery("USP_GetSalaryDEtails");
    }
    public DataTable GetGradeByDesignation(int designationId)
    {
        dm.AddParameteres("DesignationId", designationId);
        return dm.ExecuteQuery("USP_GetGradeByDesignation");
    }
     
}