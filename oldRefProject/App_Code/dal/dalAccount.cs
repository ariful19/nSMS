using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalAccount
/// </summary>
public class dalAccount
{
    DatabaseManager dm = new DatabaseManager();
	public dalAccount()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Income
    public int InsertIncomeCategory(string name)
    {
        dm.AddParameteres("@Category", name);
        DataTable dt = dm.ExecuteQuery("USP_IncomeCategory_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdateIncomeCategory(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Category", name);
        return dm.ExecuteNonQuery("USP_IncomeCategory_Update");
    }
    public DataTable GetByIncomeCategoryId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_IncomeCategory_GetById");
    }
    public int IncomeInsert(int categoryId, DateTime date, double amount, string attachment, string note, string createdBy, DateTime createdDate)
    {
        dm.AddParameteres("@CategoryId",categoryId);
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@Amount", amount);
        dm.AddParameteres("@Attachment", attachment);
        dm.AddParameteres("@Note", note);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        return dm.ExecuteNonQuery("USP_Income_Insert");
    }

    public int IncomeUpdate(int Id, int categoryId, DateTime date, double amount, string attachment, string note, string updateBy, DateTime updateDate)
    {
        dm.AddParameteres("@Id", Id);
        dm.AddParameteres("@CategoryId", categoryId);
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@Amount", amount);
        dm.AddParameteres("@Attachment", attachment);
        dm.AddParameteres("@Note", note);
        dm.AddParameteres("@UpdatedBy", updateBy);
        dm.AddParameteres("@UpdatedDate", updateDate);
        return dm.ExecuteNonQuery("USP_Income_Update");
    }
    public DataTable GetIncome()
    {
        return dm.ExecuteQuery("USP_Income_GetAll");
    }
    public DataTable GetIncomeById(int id)
    {
        dm.AddParameteres("@Id",id);
        return dm.ExecuteQuery("USP_Income_GetById");
    }
    #endregion

    #region Expense
    public int InsertExpenseCategory(string name)
    {
        dm.AddParameteres("@Category", name);
        DataTable dt = dm.ExecuteQuery("USP_ExpenseCategory_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdateExpenseCategory(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Category", name);
        return dm.ExecuteNonQuery("USP_ExpenseCategory_Update");
    }
    public DataTable GetByExpenseCategoryId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_ExpenseCategory_GetById");
    }
    public int ExpenseInsert(int categoryId, DateTime date, double amount, string attachment, string note, string createdBy, DateTime createdDate)
    {
        dm.AddParameteres("@CategoryId", categoryId);
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@Amount", amount);
        dm.AddParameteres("@Attachment", attachment);
        dm.AddParameteres("@Note", note);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        return dm.ExecuteNonQuery("USP_Expense_Insert");
    }
    public int ExpenseUpdate(int Id, int categoryId, DateTime date, double amount, string attachment, string note, string updateBy, DateTime updateDate)
    {
        dm.AddParameteres("@Id", Id);
        dm.AddParameteres("@ExpenseCategoryId", categoryId);
        dm.AddParameteres("@ExpenseDate", date);
        dm.AddParameteres("@Amount", amount);
        dm.AddParameteres("@Attachment", attachment);
        dm.AddParameteres("@Purpose", note);
        dm.AddParameteres("@UpdatedBy", updateBy);
        dm.AddParameteres("@UpdatedDate", updateDate);
        return dm.ExecuteNonQuery("USP_Expence_Update");
    }
    public DataTable GetExpense()
    {
        return dm.ExecuteQuery("USP_Expense_GetAll");
    }
    public DataTable GetExpenseById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Expense_GetById");
    }
    #endregion

    public int InsertAccount(Account aAccount)
    {      
        dm.AddParameteres("@AccountCodeId", aAccount.AccountCodeId);
        dm.AddParameteres("@AccountName", aAccount.AccountName);
        dm.AddParameteres("@AId", aAccount.AId);
        dm.AddParameteres("@SubHeadId", aAccount.SubHeadId);
        dm.AddParameteres("@Description", aAccount.Description);
        dm.AddParameteres("@Status", aAccount.Status);
        dm.AddParameteres("@Balance", aAccount.Balance);
        dm.AddParameteres("@OpenDate", aAccount.OpenDate);
        dm.AddParameteres("@CreatedBy", aAccount.CreatedBy);
        dm.AddParameteres("@CreatedDate", aAccount.CreatedDate);

        DataTable dt = dm.ExecuteQuery("USP_Account_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetAccountById(int ID)
    {
        dm.AddParameteres("@Id", ID);
        return dm.ExecuteQuery("USP_Account_GetById");
    }

    public int UpdateAccount(int ID, Account aAccount)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@AccountCodeId", aAccount.AccountCodeId);
        dm.AddParameteres("@AccountName", aAccount.AccountName);
        dm.AddParameteres("@AId", aAccount.AId);
        dm.AddParameteres("@SubHeadId", aAccount.SubHeadId);
        dm.AddParameteres("@Description", aAccount.Description);
        dm.AddParameteres("@Status", aAccount.Status);
        dm.AddParameteres("@Balance", aAccount.Balance);
        dm.AddParameteres("@OpenDate", aAccount.OpenDate);
        dm.AddParameteres("@CreatedBy", aAccount.CreatedBy);
        dm.AddParameteres("@CreatedDate", aAccount.CreatedDate);
        return dm.ExecuteNonQuery("USP_Account_Update");
    }

    public DataTable GetAccountByAccId(string jr_AccountCodeId)
    {
        dm.AddParameteres("@AccId", jr_AccountCodeId);
        return dm.ExecuteQuery("USP_Account_GetByAccId");
    }

    public DataTable GetBalanceByJrTranId(int getJrTranId)
    {
        dm.AddParameteres("@JrTranId", getJrTranId);
        return dm.ExecuteQuery("USP_GetBalanceByJrTranId");
    }

    public void UpdateBalanceForDrAmount1(double openamt, int getJrTranId)
    {
        dm.AddParameteres("@openamt", openamt);
        dm.AddParameteres("@JrTranId", getJrTranId);
        dm.ExecuteQuery("USP_AccountBalance_UpdateForDrAmount");
    }

    public void UpdateBalanceForCrAmount1(double openamt, int getJrTranId)
    {
        dm.AddParameteres("@openamt", openamt);
        dm.AddParameteres("@JrTranId", getJrTranId);
        dm.ExecuteQuery("USP_AccountBalance_UpdateForCrAmount");
    }

    public void UpdateBalanceForDrAmount2(double openamt, int getJrTranId)
    {
        dm.AddParameteres("@openamt", openamt);
        dm.AddParameteres("@JrTranId", getJrTranId);
        dm.ExecuteQuery("USP_AccountBalance_UpdateForDrAmount2");
    }

    public void UpdateBalanceForCrAmount2(double openamt, int getJrTranId)
    {
        dm.AddParameteres("@openamt", openamt);
        dm.AddParameteres("@JrTranId", getJrTranId);
        dm.ExecuteQuery("USP_AccountBalance_UpdateForCrAmount2");
    }

    public void UpdateBalanceForProfit(string payacc_ACCID, double payacc_BALANCE, double profit)
    {
        dm.AddParameteres("@AccountCodeId", payacc_ACCID);
        dm.AddParameteres("@payacc_BALANCE", payacc_BALANCE);
        dm.AddParameteres("@Profit", profit);
        dm.ExecuteQuery("USP_AccountBalance_UpdateForProfit");
    }

    public void UpdateBalanceForProfitAndCrAmount(string jr_AccountCodeId, double acc_BALANCE, double jr_CrAmount, double profit)
    {
        dm.AddParameteres("@jr_AccountCodeId", jr_AccountCodeId);
        dm.AddParameteres("@acc_BALANCE", acc_BALANCE);
        dm.AddParameteres("@jr_CrAmount", jr_CrAmount);
        dm.AddParameteres("@Profit", profit);
        dm.ExecuteQuery("USP_AccountBalance_UpdateForCrAmountAndProfit");
    }
}
