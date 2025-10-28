using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalPayment
/// </summary>
public class dalPayment
{
    DatabaseManager dm = new DatabaseManager();
    public dalPayment()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetPaymentHistoryByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Payment_GetPaymentHistoryByCriteria");
    }

    public DataTable GetStudentDueByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Payment_GetPaymentDueByCriteria");
    }
    public int StudentPaymentInsert(string year, string month, int studentId, double total, double given, string createdBy)
    {
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@Month", month);
        dm.AddParameteres("@StudentId", studentId);
        dm.AddParameteres("@Total", total);
        dm.AddParameteres("@GivenAmount", given);
        dm.AddParameteres("@CreatedBy", createdBy);
        return dm.ExecuteNonQuery("USP_Payment_StudentPaymentInsert");
    }
    public DataTable GetPreviousPaymentByStudentId(int studentId)
    {
        dm.AddParameteres("@StudentId", studentId);
        return dm.ExecuteQuery("USP_Payment_GetPreviousPaymentByStudentId");
    }
    public DataTable GetPaymentHistoryByStudentId(int StudentToClassId)
    {
        dm.AddParameteres("@StudentToClassId", StudentToClassId);
        return dm.ExecuteQuery("USP_Payment_GetPaymentHistoryByStudentId");
    }
    public DataTable GetPaymentHistoryByBillNo(string billNo)
    {
        dm.AddParameteres("@BillNo", billNo);
        return dm.ExecuteQuery("USP_Payment_GetPaymentHistoryByBillNo");
    }
    public DataTable GetPaymentHistoryByStudentSubjectId(int StudentToClassId, int classId)
    {
        dm.AddParameteres("@StudentToClassId", StudentToClassId);
        dm.AddParameteres("@ClassId", classId);
        return dm.ExecuteQuery("USP_Payment_GetPaymentHistoryByClassId");
    }    
    public int UpdateDues(int studentId)
    {
        dm.AddParameteres("@StudentId", studentId);
        return dm.ExecuteNonQuery("USP_Payment_UpdateDueByStudentId");
    }
    public int InsertStudentPayment(DataTable dt, string CreatedBy, DateTime CreatedDate)
    {

        DataSet ds = new DataSet("dsStudentPayment");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@CreatedDate", CreatedDate);
        dm.AddParameteres("@CreatedBy", CreatedBy);
        return dm.ExecuteNonQuery("USP_Payment_InsertStudentPayment");

    }

    public int InsertStudentPayment(List<StudentPayment> lstStudentPayment)
    {
        int result = 0;
        foreach (StudentPayment sPayment in lstStudentPayment)
        {

            dm.AddParameteres("@StudentToClassId", sPayment.StudentToClassId);
            dm.AddParameteres("@Year", sPayment.Year);
            dm.AddParameteres("@Month", sPayment.Month);
            dm.AddParameteres("@MonthYear", sPayment.MonthYear);
            dm.AddParameteres("@Date", sPayment.Date);
            dm.AddParameteres("@DueAmount", sPayment.DueAmount);
            dm.AddParameteres("@Amount", sPayment.Amount);
            dm.AddParameteres("@TotalGiven", sPayment.TotalGiven);
            dm.AddParameteres("@BillNo", sPayment.BillNo);
            dm.AddParameteres("@ClassId", sPayment.ClassId);
            dm.AddParameteres("@PaymentTypeId", sPayment.PaymentTypeId);
            dm.AddParameteres("@CreatedBy", sPayment.CreatedBy);
            dm.AddParameteres("@CreatedDate", sPayment.CreatedDate);
            DataTable dt = dm.ExecuteQuery("USP_Insertss_Payment");
            result = Convert.ToInt32(dt.Rows[0][0]);
        }

        return result;
    }
    public int UpdateStudentPayment(List<StudentPayment> lstStudentPayment)
    {
        int result = 0;

        foreach (StudentPayment sPayment in lstStudentPayment)
        {
            if (sPayment.PaymentID > 0)
            {
                dm.AddParameteres("@PaymentId", sPayment.PaymentID);             
                dm.AddParameteres("@DueAmount", sPayment.DueAmount);
                dm.AddParameteres("@Amount", sPayment.Amount);                       
                dm.AddParameteres("@Remarks", sPayment.Remarks);
                dm.AddParameteres("@UpdatedBy", sPayment.UpdatedBy);
                dm.AddParameteres("@UpdatedDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Payment_Update");
            }
        }
        
        return result;
    }

  

    public DataTable PaymentForStudentByCriteria(int StudentId, string yearId, int month)
    {
        dm.AddParameteres("@StudentId", StudentId);
        dm.AddParameteres("@Year", yearId);
        dm.AddParameteres("@Month", month);
        return dm.ExecuteQuery("USP_Payment_Student_Get");
    }

    public DataTable PaymentForStudentByMonthly(int StudentId, int year, int month)
    {
        dm.AddParameteres("@StudentId", StudentId);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@Month", month);

        return dm.ExecuteQuery("USP_Payment_Student_GetByMonthly");
    }

    public DataTable DueForStudentByMonthly(int StudentId, int year, int month)
    {
        dm.AddParameteres("@StudentId", StudentId);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@Month", month);

        return dm.ExecuteQuery("USP_Payment_Student_DueByMonthly");
    }

    public DataTable ExistStudentInPaymentByCriteria(string StudentId, string yearId, int month)
    {
        string sql = "select * from fee_PaymentForStudent where YearId=" + yearId + " and Month=" + month + " and StudentId  In (" + StudentId + ")";
        return dm.NewExecuteQuery(sql);
        //dm.AddParameteres("@StudentId", StudentId);
        //dm.AddParameteres("@YearId", yearId);
        //dm.AddParameteres("@Month", month);

        //return dm.ExecuteQuery("USP_Payment_ExistStudent");
    }

    public DataTable PaymentForClassByCriteria(int SubjectId, string yearId, int month)
    {

        dm.AddParameteres("@SubjectId", SubjectId);
        dm.AddParameteres("@Year", yearId);
        dm.AddParameteres("@Month", month);
        return dm.ExecuteQuery("USP_Payment_Class_Get");
    }

    public DataTable GetOthersPayment(int StudentToClassId, int yearId, int classId, int paymentTypeId)
    {
        dm.AddParameteres("@StudentToClassId", StudentToClassId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@PaymentTypeId", paymentTypeId);
        return dm.ExecuteQuery("USP_Payment_GetByPaymentType");
    }

    public DataTable GetMonthlyFees(int StudentToClassId, int yearId, int mediumId, int campusId, int classId, int groupId,int shiftId, int sectionId, int monthId, int paymentTypeId)
    {

        dm.AddParameteres("@StudentToClassId", StudentToClassId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@ShiftId", shiftId);
        dm.AddParameteres("@SectionId", sectionId);
        dm.AddParameteres("@MonthId", monthId);
        dm.AddParameteres("@PaymentTypeId", paymentTypeId);
        return dm.ExecuteQuery("USP_Payment_GetForMonth");
    }
    public DataTable GetMonthlyDues(int StudentToClassId, int yearId, int mediumId, int campusId, int classId, int monthId, int paymentTypeId)
    {

        dm.AddParameteres("@StudentToClassId", StudentToClassId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@ClassId", classId);
        
        dm.AddParameteres("@MonthId", monthId);
        dm.AddParameteres("@PaymentTypeId", paymentTypeId);
        return dm.ExecuteQuery("USP_Payment_GetForMonthlyForDue");
    }

    public DataTable GetMonthlyPayment(int StudentToClassId, int yearId, int classId, int paymentTypeId, int monthId)
    {
        dm.AddParameteres("@StudentToClassId", StudentToClassId);
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@PaymentTypeId", paymentTypeId);
        dm.AddParameteres("@MonthId", monthId);
        return dm.ExecuteQuery("USP_Payment_GetMonthlyPaymentByType");
    }


    public int GetBillNoExist(string BillNo)
    {
        dm.AddParameteres("@BillNo", BillNo);

        DataTable dt = dm.ExecuteQuery("USP_Payment_BillNoExist");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetMonthById(int monthId)
    {
        string sql = "Select Month from bs_Month where Id=" + monthId;
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetPaymentHistoryById(int ID)
    {
        string sql = "Select * from ss_Payment where Id=" + ID;
        return dm.NewExecuteQuery(sql);
    }

    public int UpdateStudentPaymentAll(List<StudentPayment> lstStudentPayment)
    {
        int result = 0;

        foreach (StudentPayment sPayment in lstStudentPayment)
        {
            if (sPayment.PaymentID > 0)
            {
                dm.AddParameteres("@PaymentId", sPayment.PaymentID);
                dm.AddParameteres("@Year", sPayment.Year);
                dm.AddParameteres("@Month", sPayment.Month);
                dm.AddParameteres("@MonthYear", sPayment.MonthYear);
                dm.AddParameteres("@Date", sPayment.Date);
                dm.AddParameteres("@DueAmount", sPayment.DueAmount);
                dm.AddParameteres("@Amount", sPayment.Amount);
                dm.AddParameteres("@TotalGiven", sPayment.TotalGiven);
                dm.AddParameteres("@BillNo", sPayment.BillNo);
                dm.AddParameteres("@ClassId", sPayment.ClassId);
                dm.AddParameteres("@PaymentTypeId", sPayment.PaymentTypeId);
                dm.AddParameteres("@Remarks", sPayment.Remarks);
                dm.AddParameteres("@UpdatedBy", sPayment.UpdatedBy);
                dm.AddParameteres("@UpdatedDate", DateTime.Now);
                result = dm.ExecuteNonQuery("USP_Payment_UpdateAll");
            }
        }

        return result;
    }

    public DataTable GetMonthlyDueListByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_MonthlyDue_GetByCriteria");
    }

    public DataTable GetTotalDueListByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Payment_GetDueListByCriteria");
    }

    public DataTable ClassWiseStudentList(string criteria)
    {
        throw new NotImplementedException();
    }

    public DataTable UpdateRemarks(int studentToClassId, string remarks)
    {
        dm.AddParameteres("@StudentToClassId", studentToClassId);
        dm.AddParameteres("@Remarks", remarks);
        return dm.ExecuteQuery("USP_UpdateRemarks");
    }

    public DataTable GetRemarksByStudent(int studentToClassId)
    {
        dm.AddParameteres("@StudentToClassId", studentToClassId);
        return dm.ExecuteQuery("USP_GetRemarksStudent");
    }
}