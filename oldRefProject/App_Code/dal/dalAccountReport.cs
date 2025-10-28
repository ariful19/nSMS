using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalAccountReport
/// </summary>
public class dalAccountReport
{
    DatabaseManager dm= new DatabaseManager();
	public dalAccountReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public System.Data.DataTable GetJournalByCriteria(string criteria)
    {
   
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Journal_GetByCriteria");
    }

    public System.Data.DataTable GetLedgerByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Ledger_GetByCriteria");
    }

    public System.Data.DataTable GetRecieveableAccountsByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_AccReceiveable");
    }

    public System.Data.DataTable GetPayableAccountsByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_AccPayable");
    }

    public System.Data.DataTable GetAccStatementByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_AccStatement");
    }

    public System.Data.DataTable GetTrialBalanceByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_AccTrialBalance");
    }

    public System.Data.DataTable GetProfitLossByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_ProfitAndLoss");
    }

    public System.Data.DataTable GetAssetSheduleByCriteria()
    {
        
        return dm.ExecuteQuery("USP_Report_AssetsSeduleNew");
    }

    public System.Data.DataTable GetCostOfGoodsByCriteria()
    {
        return dm.ExecuteQuery("Usp_Report_CostOfGoodsSold");
    }

    public System.Data.DataTable GetCashFlowByCriteria(string criteria)
    {
        throw new NotImplementedException();
    }

    public System.Data.DataTable GetBalanceSheetByCriteria()
    {
        return dm.ExecuteQuery("USP_Report_BalanceSheet");
    }
}