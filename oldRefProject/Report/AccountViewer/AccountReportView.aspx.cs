using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nano.DataAccessLayer;

public partial class Report_Designer_AccountReportView : System.Web.UI.Page
{
    dalAccountReport obj = new dalAccountReport();
    ReportDocument rptDocument = new ReportDocument();
    public static List<Tuple<string, string, string>> resultInfo;
    public static List<Tuple<string, string>> GPA;
    List<string> regNo = new List<string>();
    DatabaseManager dm= new DatabaseManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReport();
        }
        else
        {
            LoadReport();
        }
        CRV.DisplayGroupTree = false;
        CRV.HasToggleGroupTreeButton = false;
    }
    protected void LoadReport()
    {
        string criteria = "";
        if (Request.QueryString["query"] != null)
            criteria = EncryptionDecryption.EncryptDecryptString(Request.QueryString["query"]);
        string report = Request.QueryString["report"];

        if (PreviousPage != null)
        {
            if (PreviousPage.IsCrossPagePostBack == true)
            {

            }
        }
        string user = System.Configuration.ConfigurationManager.AppSettings["User"];
        string password = System.Configuration.ConfigurationManager.AppSettings["password"];
        string server = System.Configuration.ConfigurationManager.AppSettings["Server"];
        string dbName = System.Configuration.ConfigurationManager.AppSettings["DBName"];

        DataTable dtSchool = new Common().GetAll("bs_SchoolInformation");   

        if (report == "journal")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";
            

            DataTable dtJournal = obj.GetJournalByCriteria(criteria);
           

            dtJournal.Columns.Add("SchoolName");
            dtJournal.Columns.Add("SchoolAddress");
            dtJournal.Columns.Add("SchoolCode");
            dtJournal.Columns.Add("FromDate");
            dtJournal.Columns.Add("ToDate");


            if (dtSchool.Rows.Count > 0 && dtJournal.Rows.Count > 0)
            {
                dtJournal.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtJournal.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtJournal.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtJournal.Rows[0]["FromDate"] = fromDate;
                dtJournal.Rows[0]["ToDate"] = toDate;
            }

            dtJournal.TableName = "Journal";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtJournal);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRJournal.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);
            
            CRV.ReportSource = rptDocument;
        }

        if (report == "ledger")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtLedger = obj.GetLedgerByCriteria(criteria);


            dtLedger.Columns.Add("SchoolName");
            dtLedger.Columns.Add("SchoolAddress");
            dtLedger.Columns.Add("SchoolCode");
            dtLedger.Columns.Add("FromDate");
            dtLedger.Columns.Add("ToDate");
          

            if (dtSchool.Rows.Count > 0 && dtLedger.Rows.Count > 0)
            {
                dtLedger.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtLedger.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtLedger.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtLedger.Rows[0]["FromDate"] = fromDate;
                dtLedger.Rows[0]["ToDate"] = toDate;
            }

            dtLedger.TableName = "Ledger";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtLedger);



            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRLedger.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }

        if (report == "recieveable")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtRecieveableAccounts = obj.GetRecieveableAccountsByCriteria(criteria);


            dtRecieveableAccounts.Columns.Add("SchoolName");
            dtRecieveableAccounts.Columns.Add("SchoolAddress");
            dtRecieveableAccounts.Columns.Add("SchoolCode");
            dtRecieveableAccounts.Columns.Add("FromDate");
            dtRecieveableAccounts.Columns.Add("ToDate");
          

            if (dtSchool.Rows.Count > 0 && dtRecieveableAccounts.Rows.Count > 0)
            {
                dtRecieveableAccounts.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtRecieveableAccounts.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtRecieveableAccounts.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtRecieveableAccounts.Rows[0]["FromDate"] = fromDate;
                dtRecieveableAccounts.Rows[0]["ToDate"] = toDate;
            }

            dtRecieveableAccounts.TableName = "Receive_PayableAcc";


            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtRecieveableAccounts);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRPayableAcounts.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }


        if (report == "payable")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtPayableAccounts = obj.GetPayableAccountsByCriteria(criteria);


            dtPayableAccounts.Columns.Add("SchoolName");
            dtPayableAccounts.Columns.Add("SchoolAddress");
            dtPayableAccounts.Columns.Add("SchoolCode");
            dtPayableAccounts.Columns.Add("FromDate");
            dtPayableAccounts.Columns.Add("ToDate");


            if (dtSchool.Rows.Count > 0 && dtPayableAccounts.Rows.Count > 0)
            {
                dtPayableAccounts.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtPayableAccounts.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtPayableAccounts.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtPayableAccounts.Rows[0]["FromDate"] = fromDate;
                dtPayableAccounts.Rows[0]["ToDate"] = toDate;
            }

            dtPayableAccounts.TableName = "Receive_PayableAcc";


            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtPayableAccounts);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRPayableAcounts.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }

        if (report == "statement")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtStatement = obj.GetAccStatementByCriteria(criteria);


            dtStatement.Columns.Add("SchoolName");
            dtStatement.Columns.Add("SchoolAddress");
            dtStatement.Columns.Add("SchoolCode");
            dtStatement.Columns.Add("FromDate");
            dtStatement.Columns.Add("ToDate");


            if (dtSchool.Rows.Count > 0 && dtStatement.Rows.Count > 0)
            {
                dtStatement.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtStatement.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtStatement.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtStatement.Rows[0]["FromDate"] = fromDate;
                dtStatement.Rows[0]["ToDate"] = toDate;
            }

            dtStatement.TableName = "Statement";


            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtStatement);
            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRStatement.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }

        if (report == "trialBalance")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtTrialBalance = obj.GetTrialBalanceByCriteria(criteria);


            dtTrialBalance.Columns.Add("SchoolName");
            dtTrialBalance.Columns.Add("SchoolAddress");
            dtTrialBalance.Columns.Add("SchoolCode");
            dtTrialBalance.Columns.Add("FromDate");
            dtTrialBalance.Columns.Add("ToDate");


            if (dtSchool.Rows.Count > 0 && dtTrialBalance.Rows.Count > 0)
            {
                dtTrialBalance.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtTrialBalance.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtTrialBalance.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtTrialBalance.Rows[0]["FromDate"] = fromDate;
                dtTrialBalance.Rows[0]["ToDate"] = toDate;
            }

            dtTrialBalance.TableName = "TrialBalance";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtTrialBalance);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRTrialBalance.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }


        if (report == "profitLoss")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtProfitLoss = obj.GetProfitLossByCriteria(criteria);


            dtProfitLoss.Columns.Add("SchoolName");
            dtProfitLoss.Columns.Add("SchoolAddress");
            dtProfitLoss.Columns.Add("SchoolCode");
            dtProfitLoss.Columns.Add("FromDate");
            dtProfitLoss.Columns.Add("ToDate");


            if (dtSchool.Rows.Count > 0 && dtProfitLoss.Rows.Count > 0)
            {
                dtProfitLoss.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtProfitLoss.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtProfitLoss.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtProfitLoss.Rows[0]["FromDate"] = fromDate;
                dtProfitLoss.Rows[0]["ToDate"] = toDate;
            }

            dtProfitLoss.TableName = "ProfitLoss";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtProfitLoss);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRProfitandLoss.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }

        if (report == "assetShedule")
        {
            var fromDate = Session["rptFromDate"];
            var toDate = Session["rptToDate"];

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtAssetShedule = obj.GetAssetSheduleByCriteria();


            dtAssetShedule.Columns.Add("SchoolName");
            dtAssetShedule.Columns.Add("SchoolAddress");
            dtAssetShedule.Columns.Add("SchoolCode");
            dtAssetShedule.Columns.Add("FromDate");
            dtAssetShedule.Columns.Add("ToDate");


            if (dtSchool.Rows.Count > 0 && dtAssetShedule.Rows.Count > 0)
            {
                dtAssetShedule.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtAssetShedule.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtAssetShedule.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];
                dtAssetShedule.Rows[0]["FromDate"] = fromDate;
                dtAssetShedule.Rows[0]["ToDate"] = toDate;
            }

            dtAssetShedule.TableName = "AssetShedule";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtAssetShedule);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRAssetShedule.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }
        if (report == "costOfGoods")
        {

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";

            DataTable dtCostOfGoods = obj.GetCostOfGoodsByCriteria();

            dtCostOfGoods.Columns.Add("SchoolName");
            dtCostOfGoods.Columns.Add("SchoolAddress");
            dtCostOfGoods.Columns.Add("SchoolCode");



            if (dtSchool.Rows.Count > 0 && dtCostOfGoods.Rows.Count > 0)
            {
                dtCostOfGoods.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtCostOfGoods.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtCostOfGoods.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];

            }

            dtCostOfGoods.TableName = "CostOfGoods";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtCostOfGoods);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRCOstofGoods.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }
        if (report == "balanceSheet")
        {

            DataSet reportDataSet = new DataSet();
            dtSchool.TableName = "School";


            DataTable dtBalanceSheet = obj.GetBalanceSheetByCriteria();


            dtBalanceSheet.Columns.Add("SchoolName");
            dtBalanceSheet.Columns.Add("SchoolAddress");
            dtBalanceSheet.Columns.Add("SchoolCode");



            if (dtSchool.Rows.Count > 0 && dtBalanceSheet.Rows.Count > 0)
            {
                dtBalanceSheet.Rows[0]["SchoolName"] = dtSchool.Rows[0]["Name"];
                dtBalanceSheet.Rows[0]["SchoolAddress"] = dtSchool.Rows[0]["Address"];
                dtBalanceSheet.Rows[0]["SchoolCode"] = dtSchool.Rows[0]["Code"];

            }


            dtBalanceSheet.TableName = "BalanceSheet";

            reportDataSet.Tables.Add(dtSchool);
            reportDataSet.Tables.Add(dtBalanceSheet);

            rptDocument.Load(Server.MapPath("~/Report/AccountReport/CRBalanceSheet.rpt").ToString());
            rptDocument.SetDatabaseLogon(user, password, server, dbName);

            rptDocument.SetDataSource(reportDataSet);

            CRV.ReportSource = rptDocument;
        }
    }
}