using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalPayrollBasic
{
    DatabaseManager dm = new DatabaseManager();
	public dalPayrollBasic()
	{
	}
    #region Type
    public int TypeInsert(string name, int scaleId, double basic)
    {
        dm.AddParameteres("@Type", name);
        dm.AddParameteres("@ScaleId", scaleId);
        dm.AddParameteres("@Basic", basic);
        DataTable dt = dm.ExecuteQuery("USP_Payroll_TypeInsert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int TypeUpdate(int id, string name, int scaleId, double basic)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Type", name);
        dm.AddParameteres("@ScaleId", scaleId);
        dm.AddParameteres("@Basic", basic);
        return dm.ExecuteNonQuery("USP_Payroll_TypeUpdate");
    }
    public DataTable TypeGetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_TypeGetById");
    }
    #endregion

    #region Scale
    public int ScaleInsert(string scale, string description)
    {
        dm.AddParameteres("@Scale", scale);
        dm.AddParameteres("@Description", description);
        DataTable dt = dm.ExecuteQuery("USP_Payroll_SalaryScaleInsert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int ScaleUpdate(int id, string scale, string description)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Scale", scale);
        dm.AddParameteres("@Description", description);
        return dm.ExecuteNonQuery("USP_Payroll_SalaryScaleUpdate");
    }
    public DataTable ScaleGetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_SalaryScaleGetById");
    }
    #endregion

    #region Allowance
    public int AllowanceInsert(string name)
    {
        dm.AddParameteres("@Allowance", name);
        DataTable dt = dm.ExecuteQuery("USP_Payroll_AllowanceInsert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int AllowanceUpdate(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Allowance", name);
        return dm.ExecuteNonQuery("USP_Payroll_AllowanceUpdate");
    }
    public DataTable AllowanceGetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_AllowanceGetById");
    }

    public DataTable AllowanceGetByTypeId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_AllowanceGetByTypeId");
    }

    public DataTable AllowanceDeleteByTypeId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_AllowanceDeleteByTypeId");
    }
    #endregion

    #region Deduction
    public int DeductionInsert(string name)
    {
        dm.AddParameteres("@Deduction", name);
        DataTable dt = dm.ExecuteQuery("USP_Payroll_DeductionInsert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int DeductionUpdate(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Deduction", name);
        return dm.ExecuteNonQuery("USP_Payroll_DeductionUpdate");
    }
    public DataTable DeductionGetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_DeductionGetById");
    }

    public DataTable DeductionGetByTypeId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_DeductionGetByTypeId");
    }

    public DataTable DeductionDeleteByTypeId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Payroll_DeductionDeleteByTypeId");
    }
    #endregion

}