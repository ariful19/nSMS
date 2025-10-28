using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;
using System.Data;

/// <summary>
/// Summary description for dalJournal
/// </summary>
public class dalJournal
{
    DatabaseManager dm = new DatabaseManager();
	public dalJournal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void InserJournal(Journal aJournal)
    {
        dm.AddParameteres("@JrTranId", aJournal.JrTranId);
        dm.AddParameteres("@AccountCodeId", aJournal.AccountCodeId);
        dm.AddParameteres("@ChequeNo", aJournal.ChequeNo);
        dm.AddParameteres("@DrAmount", aJournal.DrAmount);
        dm.AddParameteres("@CrAmount", aJournal.CrAmount);
        dm.AddParameteres("@HeadCodeId", aJournal.HeadCodeId);
        dm.AddParameteres("@Purpose", aJournal.Purpose);
        dm.AddParameteres("@Remarks", aJournal.Remarks);
        dm.AddParameteres("@TDate", aJournal.TDate);
        dm.AddParameteres("@TentaiveDate", aJournal.TentaiveDate);
        dm.AddParameteres("@VNo", aJournal.VNo);
        dm.AddParameteres("@VType", aJournal.VType);
        dm.AddParameteres("@AId", aJournal.AId);
        dm.AddParameteres("@CDate", aJournal.CDate);
        dm.AddParameteres("@Flag", aJournal.Flag);
        dm.AddParameteres("@MrNo", aJournal.MrNo);
        dm.AddParameteres("@PurchaseDate", aJournal.PurchaseDate);
        dm.AddParameteres("@TrnId", aJournal.TrnId);
        dm.AddParameteres("@CreatedBy", aJournal.CreatedBy);
        dm.AddParameteres("@CreatedDate", aJournal.CreatedDate);
        dm.AddParameteres("@IsLgledger", aJournal.IsLgledger);

        dm.ExecuteQuery("USP_Journal_Insert");
    }

    public DataTable GetJournalByCriteria()
    {
        //dm.AddParameteres("@Criteria", criteria);

        return dm.ExecuteQuery("USP_Journal_GetAllJournal");
    }

    public void InserLedger(long SrNo, string AccountCodeId, double OpeningBalance, double DrAmount, double CrAmount, double ClBalance, DateTime TDate, int JrTranId, string CreatedBy, DateTime CreatedDate)
    {
        dm.AddParameteres("@SrNo", SrNo);
        dm.AddParameteres("@JrTranId",JrTranId);
        dm.AddParameteres("@TDate",TDate);
        dm.AddParameteres("@AccountCodeId",AccountCodeId);
        dm.AddParameteres("@OpeningBalance",OpeningBalance);
        dm.AddParameteres("@DrAmount",DrAmount);
        dm.AddParameteres("@CrAmount",CrAmount);
        dm.AddParameteres("@ClBalance",ClBalance);
        dm.AddParameteres("@CreatedBy",CreatedBy);
        dm.AddParameteres("@CreatedDate",CreatedDate);
    

        dm.ExecuteQuery("USP_Ledger_Insert");
    }

    public void DeleteJournal(int ID)
    {
        dm.AddParameteres("@Id", ID);
        dm.ExecuteQuery("USP_Journal_Delete");
    }

    public DataTable GetDataForEditById()
    {
        //dm.AddParameteres("@Id", ID);

        return dm.ExecuteQuery("USP_Journal_GetJournalDataById");
    }

    public void UpdateJournal(int ID,Journal aJournal)
    {
        dm.AddParameteres("@JrTranId", ID);
       dm.AddParameteres("@AccountCodeId", aJournal.AccountCodeId);
       dm.AddParameteres("@ChequeNo", aJournal.ChequeNo);
       dm.AddParameteres("@DrAmount", aJournal.DrAmount);
       dm.AddParameteres("@CrAmount", aJournal.CrAmount);
       dm.AddParameteres("@HeadCodeId", aJournal.HeadCodeId);
       dm.AddParameteres("@Purpose", aJournal.Purpose);
       dm.AddParameteres("@Remarks", aJournal.Remarks);
       dm.AddParameteres("@TDate", aJournal.TDate);
       dm.AddParameteres("@TentaiveDate", aJournal.TentaiveDate);
       dm.AddParameteres("@VNo", aJournal.VNo);
       dm.AddParameteres("@VType", aJournal.VType);
       dm.AddParameteres("@AId", aJournal.AId);
       dm.AddParameteres("@CDate", aJournal.CDate);
       dm.AddParameteres("@Flag", aJournal.Flag);
       dm.AddParameteres("@MrNo", aJournal.MrNo);
       dm.AddParameteres("@PurchaseDate", aJournal.PurchaseDate);
       dm.AddParameteres("@TrnId", aJournal.TrnId);
       dm.AddParameteres("@UpdateBy", aJournal.UpdateBy);
       dm.AddParameteres("@UpdateDate", aJournal.UpdateDate);
       dm.AddParameteres("@IsLgledger", aJournal.IsLgledger);

       dm.ExecuteQuery("USP_Journal_Update");

    }

    public DataTable GetJournalById(int getJrTranId)
    {
        dm.AddParameteres("@Id", getJrTranId);

        return dm.ExecuteQuery("USP_Journal_GetJournalById");
    }

    public DataTable GetAccountTypeByAccTypeId(int acc_ACCTYPID)
    {
        dm.AddParameteres("@AccTypeId", acc_ACCTYPID);

        return dm.ExecuteQuery("USP_AccountType_GetDataByAccountTypeId");
    }

    public DataTable GetSrNoFromLedger()
    {
        return dm.ExecuteQuery("USP_GetLedgerSrNo");
    }

    public DataTable GetProfitByJrTranId(int getJrTranId)
    {
        dm.AddParameteres("@Id", getJrTranId);

        return dm.ExecuteQuery("USP_GetProfitByJrTranId");
    }

    public DataTable GetProfitPayableByAccTypeCId(int acctp_ACCTYPID)
    {
        dm.AddParameteres("@AccTypeId", acctp_ACCTYPID);

        return dm.ExecuteQuery("USP_GetProfitPayableByAccTypeCId");
        
    }

    public DataTable GetProfitToByAccTypeCId(int acctp_ACCTYPID)
    {
        dm.AddParameteres("@AccTypeId", acctp_ACCTYPID);

        return dm.ExecuteQuery("USP_GetProfitToByAccTypeCId");
    }

    public void UpdateJournalFlag(int getJrTranId, string FLAG)
    {
        dm.AddParameteres("@JrTranId", getJrTranId);
        dm.AddParameteres("@FLAG", FLAG);

        dm.ExecuteQuery("USP_Journal_FlagUpdate");
    }

    public DataTable GetJrIdFromJournal()
    {
        return dm.ExecuteQuery("USP_GetLedgerJrTranId");
    }
}