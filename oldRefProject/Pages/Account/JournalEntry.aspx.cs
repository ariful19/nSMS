using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_JournalEntry : System.Web.UI.Page
{
    dalJournal obj = new dalJournal();
    dalAccount objAccount = new dalAccount();
    dalAccountHead objAccountHead= new dalAccountHead();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Load();
            //LoadYear();
           
            btnSave.Visible = false;
            btnReset.Visible = false;
            btnEdit.Visible = false;
            btnposttoledger.Visible = false;
            Bind();

        }
    }
    //#region Load Data
    //private void LoadYear()
    //{
    //    int curyr = DateTime.Now.Year;
    //    for (int i = curyr; i >= curyr - 5; i--)
    //    {
    //        ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //    }
    //    ddlYear.Items.Insert(0, new ListItem("--Year--", string.Empty));
    //}
    //protected void Load()
    //{
    //    ddlDesignation.DataSource = new Common().GetAll("bs_Designation");
    //    ddlDesignation.DataBind();
    //}

 
    //#endregion

    protected void Bind()
    {
        //string criteria = "IsLgledger='False' and Year(TDate)='" + ddlYear.SelectedValue + "'" + " and Month(TDate)='" + ddlMonth.SelectedValue + "'";
        DataTable dt = obj.GetJournalByCriteria();
        GvShowJournal.DataSource = dt;
        GvShowJournal.DataBind();

       

        if (dt.Rows.Count > 0)
        {
            btnposttoledger.Visible = true;
            btnEdit.Visible = true;
            btnSave.Visible = false;
        }

        //GvJournal.DataSource = null;
        //GvJournal.DataBind();
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SetInitialRow();
        btnSave.Visible = true;
        btnReset.Visible = true;
        ButtonAdd.Visible = true;
        journalDiv.Visible = true;
        btnInsert.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (IsBlankData())
        //{
        //    MessageController.Show("Some thing wrong!!!!", MessageType.Warning, Page);
        //    return ;
        //}
        try
        {
            Int64 totalDrAmount = 0, totalCrAmount = 0;
            string vouchertype = "", checkno = "";

            foreach (GridViewRow gridrow in GvJournal.Rows)
            {
                var txtdramount = (TextBox)gridrow.Cells[4].FindControl("txtdramount");
                var txtcramount = (TextBox)gridrow.Cells[5].FindControl("txtcramount");
                checkno = ((TextBox)gridrow.Cells[7].FindControl("txtcheckno")).Text;
                vouchertype = ((TextBox)gridrow.Cells[9].FindControl("txtvouchertype")).Text;
                if (txtdramount.Text == "")
                {
                    txtdramount.Text = "0";
                }
                if (txtcramount.Text == "")
                {
                    txtcramount.Text = "0";
                }
                totalCrAmount = totalCrAmount + Convert.ToInt64(txtcramount.Text);
                totalDrAmount = totalDrAmount + Convert.ToInt64(txtdramount.Text);

            }
            if (totalDrAmount==totalCrAmount)
            {
                if (vouchertype != "")
                {
                    if (checkno != "")
                    {
                        int hid = 0;

                        foreach (GridViewRow gr in GvJournal.Rows)
                        {
                            int JrTranId = 0;
                            var hfjrnlid = (HiddenField)gr.Cells[0].FindControl("hfjrnlid");
                            var txtaccid = (TextBox)gr.Cells[0].FindControl("txtaccid");
                            var txtaccname = (TextBox)gr.Cells[1].FindControl("txtaccname");
                            var txtheadid = (TextBox)gr.Cells[2].FindControl("txtsubcodeid");
                            var txtpurpose = (TextBox)gr.Cells[3].FindControl("txtpurpose");
                            var txtdramount = (TextBox)gr.Cells[4].FindControl("txtdramount");
                            var txtcramount = (TextBox)gr.Cells[5].FindControl("txtcramount");
                            var txtmrno = (TextBox)gr.Cells[6].FindControl("txtmrno");
                            var txtcheckno = (TextBox)gr.Cells[7].FindControl("txtcheckno");
                            var txtvoucherno = (TextBox)gr.Cells[8].FindControl("txtvoucherno");
                            var txtvouchertype = (TextBox)gr.Cells[9].FindControl("txtvouchertype");
                            var txttrdate = (TextBox)gr.Cells[10].FindControl("txttrdate");
                            var txtremarks = (TextBox)gr.Cells[11].FindControl("txtremarks");

                            Journal aJournal = new Journal();

                            aJournal.AccountCodeId = txtaccid.Text;
                            aJournal.ChequeNo = txtcheckno.Text;
                            aJournal.CrAmount = Convert.ToInt64(txtcramount.Text);
                            aJournal.DrAmount = Convert.ToInt64(txtdramount.Text);
                            aJournal.HeadCodeId = Convert.ToInt64(txtheadid.Text);
                            aJournal.Purpose = txtpurpose.Text;
                            aJournal.Remarks = txtremarks.Text;
                            aJournal.TDate = Convert.ToDateTime(txttrdate.Text);
                            aJournal.TentaiveDate = Convert.ToDateTime(txttrdate.Text);
                            aJournal.VNo = txtvoucherno.Text;
                            aJournal.VType = txtvouchertype.Text;

                            aJournal.AId = 1;
                            aJournal.CDate = Convert.ToDateTime(txttrdate.Text);
                            aJournal.Flag = "N";
                            aJournal.MrNo = txtmrno.Text;
                            aJournal.PurchaseDate = DateTime.Now;
                            aJournal.TrnId = 1;
                            aJournal.CreatedBy = Page.User.Identity.Name;
                            aJournal.CreatedDate = DateTime.Now;
                            aJournal.UpdateBy = Page.User.Identity.Name;
                            aJournal.UpdateDate = DateTime.Now;
                            aJournal.IsLgledger = false;

                            DataTable dtJrId = obj.GetJrIdFromJournal();

                            if (dtJrId.Rows[0][0].ToString() != "")
                                JrTranId = Convert.ToInt32(dtJrId.Rows[0][0].ToString()) + 1;
                            else
                                JrTranId = 1000;

                            

                            if (JrTranId != 0)
                            {
                                aJournal.JrTranId = JrTranId;
                                obj.InserJournal(aJournal);
                                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                                btnInsert.Visible = true;
                            }
                            else
                            {
                                ID = Convert.ToInt32(JrTranId);
                                obj.UpdateJournal(ID, aJournal);
                                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
                                journalDiv.Visible = false;
                                ButtonAdd.Visible = false;
                                btnInsert.Visible = true;
                            }
                        }
                        hid = 0;
                        Bind();
                        //LoadJoural();
                    }
                    else
                    {
                        //lblmsg.Text = "Check No required ..!!";
                        MessageController.Show("Check No required ..!!!", MessageType.Warning, Page);
                    }
                }
                else
                {
                    MessageController.Show("Voucher type required..!!!", MessageType.Warning, Page);
                }
            }
            else
            {
                MessageController.Show("Dr Amount And Cr Amount Is not Equal....!!", MessageType.Warning, Page);
            }
        }
        catch (Exception)
        {
                
            throw;
        }
        Bind();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\Pages\\Account\\JournalEntry.aspx");
    }

    public void TransferLedger(int getJrTranId)
    {
        double amt;
        Int32 acctypeid;
        double profit;
        Int32 pftPay;
        Int32 pftTo;
        Int32 headId;
        Int64 id;
        Int32 flag;
        Int32 flg_Dr = 0;
        Int32 flg_Cr = 0;
        double openamt;
        double closeamt;

        Int32 jr_JrTranId = 0;
        DateTime jr_TDate;
        Int32 jr_VNo;
        Int32 jr_MrNo;
        string jr_Purpose;
        Int32 jr_AId;
        string jr_AccountCodeId = "";
        Int32 jr_HeadCodeId;
        double jr_DrAmount = 0;
        double jr_CrAmount= 0;
        string jr_Flag;
        string jr_Remarks;
        //table journal data diclare end


        DataTable dtJr = obj.GetJournalById(getJrTranId);


        if (dtJr.Rows[0][8].ToString() != "0")
            flg_Dr = 1;
        else if (dtJr.Rows[0][9].ToString() != "0")
            flg_Cr = 1;

        if (dtJr.Rows[0][0].ToString() != "")
            jr_JrTranId = Convert.ToInt32(dtJr.Rows[0][0].ToString());
        //if (dtJr.Rows[0][1].ToString() != "")
        jr_TDate = Convert.ToDateTime(dtJr.Rows[0][1].ToString());
        if (dtJr.Rows[0][2].ToString() != "")
            jr_VNo = Convert.ToInt32(dtJr.Rows[0][2].ToString());
        if (dtJr.Rows[0][3].ToString() != "")
            jr_MrNo = Convert.ToInt32(dtJr.Rows[0][3].ToString());
        if (dtJr.Rows[0][4].ToString() != "")
            jr_Purpose = dtJr.Rows[0][4].ToString();
        if (dtJr.Rows[0][5].ToString() != "")
            jr_AId = Convert.ToInt32(dtJr.Rows[0][5].ToString());
        if (dtJr.Rows[0][6].ToString() != "")
            jr_AccountCodeId = dtJr.Rows[0][6].ToString();
        if (dtJr.Rows[0][7].ToString() != "")
            jr_HeadCodeId = Convert.ToInt32(dtJr.Rows[0][7].ToString());
        if (dtJr.Rows[0][8].ToString() != "")
            jr_DrAmount = Convert.ToDouble(dtJr.Rows[0][8].ToString());
        if (dtJr.Rows[0][9].ToString() != "")
            jr_CrAmount = Convert.ToDouble(dtJr.Rows[0][9].ToString());
        if (dtJr.Rows[0][10].ToString() != "")
            jr_Flag = dtJr.Rows[0][10].ToString();
        if (dtJr.Rows[0][11].ToString() != "")
            jr_Remarks = dtJr.Rows[0][11].ToString();
        //------------------------------------------------
        //--table account data diclare
        string acc_ACCID = "";
        string acc_ACCNAME;
        Int32 acc_AID;
        Int32 acc_HEADID = 0;
        Int32 acc_APPID;
        string acc_DESCRIPTION;
        string acc_STATUS;
        Int32 acc_ACCTYPID = 0;
        double acc_BALANCE = 0;

        DataTable dtAccount = objAccount.GetAccountByAccId(jr_AccountCodeId);



        if (dtAccount.Rows[0][1].ToString() != "")
            acc_ACCID = dtAccount.Rows[0][1].ToString();
        if (dtAccount.Rows[0][2].ToString() != "")
            acc_ACCNAME = dtAccount.Rows[0][2].ToString();
        if (dtAccount.Rows[0][3].ToString() != "")
            acc_AID = Convert.ToInt32(dtAccount.Rows[0][3].ToString());
        if (dtAccount.Rows[0][4].ToString() != "")
            acc_HEADID = Convert.ToInt32(dtAccount.Rows[0][4].ToString());
        if (dtAccount.Rows[0][5].ToString() != "")
            acc_APPID = Convert.ToInt32(dtAccount.Rows[0][5].ToString());
        if (dtAccount.Rows[0][6].ToString() != "")
            acc_DESCRIPTION = dtAccount.Rows[0][6].ToString();
        if (dtAccount.Rows[0][7].ToString() != "")
            acc_STATUS = dtAccount.Rows[0][7].ToString();
        if (dtAccount.Rows[0][8].ToString() != "")
            acc_ACCTYPID = Convert.ToInt32(dtAccount.Rows[0][8].ToString());
        if (dtAccount.Rows[0][9].ToString() != "")
            acc_BALANCE = Convert.ToDouble(dtAccount.Rows[0][9].ToString());

        //------------------------------------------------------------------------------
        if (dtAccount.Rows[0][8].ToString() != "")
            acctypeid = 1;
        else
            acctypeid = 0;


        headId = objAccountHead.GetMainHeadCodeByAccHeadId(acc_HEADID);


        //-----------------------------------------------------------
        //----table accounttype data diclare
        Int32 acctp_ACCTYPID = 0;
        string acctp_ACCTYPENAME;
        Int32 acctp_NOFINSTALLMENT;
        Int32 acctp_AID;
        double acctp_GROSSAMOUNT;
        double acctp_PERDAYFINE;
        double acctp_INSTALLMENTAMT;
        double acctp_PROFITEQUATION = 0;
        Int32 acctp_PROFITTO = 0;
        Int32 acctp_PROFITPAYABLE = 0;
        //----table payaccountdata diclare
        string payacc_ACCID = "";
        string payacc_ACCNAME;
        Int32 payacc_AID;
        Int32 payacc_HEADID;
        Int32 payacc_APPID;
        string payacc_DESCRIPTION;
        string payacc_STATUS;
        Int32 payacc_ACCTYPID;
        double payacc_BALANCE = 0;
        //----table pftaccount diclare
        string pftacc_ACCID = "";
        string pftacc_ACCNAME;
        Int32 pftacc_AID;
        Int32 pftacc_HEADID;
        Int32 pftacc_APPID;
        string pftacc_DESCRIPTION;
        string pftacc_STATUS;
        Int32 pftacc_ACCTYPID;
        double pftacc_BALANCE = 0;

        if (acctypeid == 1)
        {
            DataTable dtAccountType = obj.GetAccountTypeByAccTypeId(acc_ACCTYPID);


            flag = 0;
            if (dtAccountType.Rows.Count != 0)
            {
                if (dtAccountType.Rows[0][1].ToString() != null)
                    acctp_ACCTYPID = Convert.ToInt32(dtAccountType.Rows[0][1].ToString());
                if (dtAccountType.Rows[0][2].ToString() != "")
                    acctp_ACCTYPENAME = dtAccountType.Rows[0][2].ToString();
                if (dtAccountType.Rows[0][3].ToString() != "")
                    acctp_NOFINSTALLMENT = Convert.ToInt32(dtAccountType.Rows[0][3].ToString());
                if (dtAccountType.Rows[0][4].ToString() != "")
                    acctp_AID = Convert.ToInt32(dtAccountType.Rows[0][4].ToString());
                if (dtAccountType.Rows[0][5].ToString() != "")
                    acctp_GROSSAMOUNT = Convert.ToDouble(dtAccountType.Rows[0][5].ToString());
                if (dtAccountType.Rows[0][6].ToString() != "")
                    acctp_PERDAYFINE = Convert.ToDouble(dtAccountType.Rows[0][6].ToString());
                if (dtAccountType.Rows[0][7].ToString() != "")
                    acctp_INSTALLMENTAMT = Convert.ToDouble(dtAccountType.Rows[0][7].ToString());
                if (dtAccountType.Rows[0][8].ToString() != "")
                    acctp_PROFITEQUATION = Convert.ToDouble(dtAccountType.Rows[0][8].ToString());
                if (dtAccountType.Rows[0][9].ToString() != "")
                    acctp_PROFITTO = Convert.ToInt32(dtAccountType.Rows[0][9].ToString());
                if (dtAccountType.Rows[0][10].ToString() != "")
                    acctp_PROFITPAYABLE = Convert.ToInt32(dtAccountType.Rows[0][10].ToString());

                if (dtAccountType.Rows[0][9].ToString() != "")
                    pftTo = 1;
                else
                    pftTo = 0;
                if (dtAccountType.Rows[0][10].ToString() != "")
                    pftPay = 1;
                else
                    pftPay = 0;
                if (pftTo == 1 || pftPay == 1)
                    flag = 1;
                else
                    flag = 0;
            }
        }
        else
            flag = 0;
        //-----------------------------------------------------------------
        if (acctypeid == 0 || flag == 0 || flg_Dr == 1)
        {

            flag = 1;

            DataTable dtOpenAmount = objAccount.GetBalanceByJrTranId(getJrTranId);

            if (dtOpenAmount.Rows[0][0].ToString() != "")
                openamt = Convert.ToDouble(dtOpenAmount.Rows[0][0].ToString());
             else
                openamt = 0;
            if (headId == 5000 || headId == 7000)
            {
                if (headId == 7000 && flg_Dr == 1)
                {
                    objAccount.UpdateBalanceForDrAmount1(openamt, getJrTranId);
                }
                if (headId == 7000 && flg_Cr == 1)
                {
                    
                    objAccount.UpdateBalanceForCrAmount1(openamt, getJrTranId);
                }
                if (headId == 5000)
                {
                    objAccount.UpdateBalanceForCrAmount1(openamt, getJrTranId);
                }
            }
            else if (headId == 9000 || headId == 4000)
            {
                if (flg_Dr == 1)
                {
                    objAccount.UpdateBalanceForDrAmount2(openamt, getJrTranId);
                }
                if (flg_Cr == 1)
                {
                    objAccount.UpdateBalanceForCrAmount2(openamt, getJrTranId);
                }
            }

            DataTable dtSrNo = obj.GetSrNoFromLedger();

            if (dtSrNo.Rows[0][0].ToString() != "")
                id = Convert.ToInt64(dtSrNo.Rows[0][0].ToString()) + 1;
            else
                id = 1000000;

            DataTable dtCloseAmount = objAccount.GetBalanceByJrTranId(getJrTranId);
            if (dtCloseAmount.Rows[0][0].ToString() != "")
                closeamt = Convert.ToDouble(dtCloseAmount.Rows[0][0].ToString());
            else
                closeamt = 0;

            if (getJrTranId != 0)
            {
                obj.InserLedger(id, acc_ACCID, openamt, jr_DrAmount, jr_CrAmount, closeamt, jr_TDate, getJrTranId, Page.User.Identity.Name, DateTime.Now);
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            }
        }
        else
        {
            DataTable dtAccountType = obj.GetAccountTypeByAccTypeId(acc_ACCTYPID);
            if (dtAccountType.Rows[0][1].ToString() != null)
                acctp_ACCTYPID = Convert.ToInt32(dtAccountType.Rows[0][1].ToString());
            if (dtAccountType.Rows[0][2].ToString() != "")
                acctp_ACCTYPENAME = dtAccountType.Rows[0][2].ToString();
            if (dtAccountType.Rows[0][3].ToString() != "")
                acctp_NOFINSTALLMENT = Convert.ToInt32(dtAccountType.Rows[0][3].ToString());
            if (dtAccountType.Rows[0][4].ToString() != "")
                acctp_AID = Convert.ToInt32(dtAccountType.Rows[0][4].ToString());
            if (dtAccountType.Rows[0][5].ToString() != "")
                acctp_GROSSAMOUNT = Convert.ToDouble(dtAccountType.Rows[0][5].ToString());
            if (dtAccountType.Rows[0][6].ToString() != "")
                acctp_PERDAYFINE = Convert.ToDouble(dtAccountType.Rows[0][6].ToString());
            if (dtAccountType.Rows[0][7].ToString() != "")
                acctp_INSTALLMENTAMT = Convert.ToDouble(dtAccountType.Rows[0][7].ToString());
            if (dtAccountType.Rows[0][8].ToString() != "")
                acctp_PROFITEQUATION = Convert.ToDouble(dtAccountType.Rows[0][8].ToString());
            if (dtAccountType.Rows[0][9].ToString() != "")
                acctp_PROFITTO = Convert.ToInt32(dtAccountType.Rows[0][9].ToString());
            if (dtAccountType.Rows[0][10].ToString() != "")
                acctp_PROFITPAYABLE = Convert.ToInt32(dtAccountType.Rows[0][10].ToString());                                   

            
            DataTable dtProfit = obj.GetProfitByJrTranId(getJrTranId);

            amt = Convert.ToDouble(dtProfit.Rows[0][0].ToString());
            profit = amt * acctp_PROFITEQUATION;


            DataTable dtProfitPayable = obj.GetProfitPayableByAccTypeCId(acctp_ACCTYPID);
            pftPay = Convert.ToInt32(dtProfitPayable.Rows[0][0].ToString());
            if (pftPay == 1)
            {

                //string sql16 = "select * from TBLACCOUNT where ACCID='" + acctp_PROFITPAYABLE + "'";
                DataTable dtAccount2 = objAccount.GetAccountByAccId((acctp_PROFITPAYABLE).ToString());
                if (dtAccount2.Rows[0][1].ToString() != "")
                    payacc_ACCID = dtAccount2.Rows[0][1].ToString();
                if (dtAccount2.Rows[0][2].ToString() != "")
                    payacc_ACCNAME = dtAccount2.Rows[0][2].ToString();
                if (dtAccount2.Rows[0][3].ToString() != "")
                    payacc_AID = Convert.ToInt32(dtAccount2.Rows[0][3].ToString());
                if (dtAccount2.Rows[0][4].ToString() != "")
                    payacc_HEADID = Convert.ToInt32(dtAccount2.Rows[0][4].ToString());
                if (dtAccount2.Rows[0][5].ToString() != "")
                    payacc_APPID = Convert.ToInt32(dtAccount2.Rows[0][5].ToString());
                if (dtAccount2.Rows[0][6].ToString() != "")
                    payacc_DESCRIPTION = dtAccount2.Rows[0][6].ToString();
                if (dtAccount2.Rows[0][7].ToString() != "")
                    payacc_STATUS = dtAccount2.Rows[0][7].ToString();
                if (dtAccount2.Rows[0][8].ToString() != "")
                    payacc_ACCTYPID = Convert.ToInt32(dtAccount2.Rows[0][8].ToString());
                if (dtAccount2.Rows[0][9].ToString() != "")
                    payacc_BALANCE = Convert.ToDouble(dtAccount2.Rows[0][9].ToString());

                openamt = payacc_BALANCE;
                closeamt = openamt + profit;
                flag = 0;

                objAccount.UpdateBalanceForProfit(payacc_ACCID, payacc_BALANCE, profit);

                objAccount.UpdateBalanceForProfitAndCrAmount(jr_AccountCodeId, acc_BALANCE, jr_CrAmount, profit);


                DataTable dtSrNo = obj.GetSrNoFromLedger();

                if (dtSrNo.Rows[0][0].ToString() != "")
                    id = Convert.ToInt64(dtSrNo.Rows[0][0].ToString()) + 1;
                else
                    id = 1000000;

                if (getJrTranId != 0)
                {
                    obj.InserLedger(id, acc_ACCID, openamt, 0, profit, closeamt, jr_TDate, getJrTranId, Page.User.Identity.Name, DateTime.Now);
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);   
                }
            }

            DataTable dtProfitTo = obj.GetProfitToByAccTypeCId(acctp_ACCTYPID);

            pftTo = Convert.ToInt32(dtProfitTo.Rows[0][0].ToString());

            if (pftTo == 1)
            {
                DataTable dt21 = objAccount.GetAccountByAccId(acctp_PROFITTO.ToString());

                if (dt21.Rows[0][1].ToString() != "")
                    pftacc_ACCID = dt21.Rows[0][1].ToString();
                if (dt21.Rows[0][2].ToString() != "")
                    pftacc_ACCNAME = dt21.Rows[0][2].ToString();
                if (dt21.Rows[0][3].ToString() != "")
                    pftacc_AID = Convert.ToInt32(dt21.Rows[0][3].ToString());
                if (dt21.Rows[0][4].ToString() != "")
                    pftacc_HEADID = Convert.ToInt32(dt21.Rows[0][4].ToString());
                if (dt21.Rows[0][5].ToString() != "")
                    pftacc_APPID = Convert.ToInt32(dt21.Rows[0][5].ToString());
                if (dt21.Rows[0][6].ToString() != "")
                    pftacc_DESCRIPTION = dt21.Rows[0][6].ToString();
                if (dt21.Rows[0][7].ToString() != "")
                    pftacc_STATUS = dt21.Rows[0][7].ToString();
                if (dt21.Rows[0][8].ToString() != "")
                    pftacc_ACCTYPID = Convert.ToInt32(dt21.Rows[0][8].ToString());
                if (dt21.Rows[0][9].ToString() != "")
                    pftacc_BALANCE = Convert.ToDouble(dt21.Rows[0][9].ToString());

                flag = 1;
                openamt = pftacc_BALANCE;
                closeamt = openamt + profit;//  -- add as expenses..

                objAccount.UpdateBalanceForProfit(pftacc_ACCID, pftacc_BALANCE, profit);
                DataTable dtSrNo = obj.GetSrNoFromLedger();

                if (dtSrNo.Rows[0][0].ToString() != "")
                    id = Convert.ToInt64(dtSrNo.Rows[0][0].ToString()) + 1;
                else
                    id = 1000000;

                if (getJrTranId != 0)
                {
                    obj.InserLedger(id, acc_ACCID, openamt, profit, 0, closeamt, jr_TDate, getJrTranId, Page.User.Identity.Name, DateTime.Now);
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                }              
                         
                
            }
            if (flag == 1)  // -- savings scheme ..
            {
                openamt = acc_BALANCE;
                closeamt = openamt + amt;

                objAccount.UpdateBalanceForProfit(acc_ACCID, acc_BALANCE, amt);
                
                DataTable dtSrNo = obj.GetSrNoFromLedger();

                if (dtSrNo.Rows[0][0].ToString() != "")
                    id = Convert.ToInt64(dtSrNo.Rows[0][0].ToString()) + 1;
                else
                    id = 1000000;

                if (getJrTranId != 0)
                {
                    obj.InserLedger(id, acc_ACCID, openamt, 0, amt, closeamt, jr_TDate, getJrTranId, Page.User.Identity.Name, DateTime.Now);
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                }          

               

            }

            else //-- loan
            {
                openamt = acc_BALANCE;
                closeamt = openamt - amt + profit;
                objAccount.UpdateBalanceForProfitAndCrAmount(acc_ACCID, acc_BALANCE, amt, profit);



                DataTable dtSrNo = obj.GetSrNoFromLedger();

                if (dtSrNo.Rows[0][0].ToString() != "")
                    id = Convert.ToInt64(dtSrNo.Rows[0][0].ToString()) + 1;
                else
                    id = 1000000;

                if (getJrTranId != 0)
                {
                    obj.InserLedger(id, acc_ACCID, openamt, 0, (amt-profit), closeamt, jr_TDate, getJrTranId, Page.User.Identity.Name, DateTime.Now);
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                }   
                            
            }
        }
        string FLAG = "Y";
        obj.UpdateJournalFlag(getJrTranId, FLAG);       

    }

    protected void btnposttoledger_Click(object sender, EventArgs e)
    {
        try
        {
            
            foreach (GridViewRow gr in GvShowJournal.Rows)
            {
                CheckBox chkBox = (CheckBox)gr.Cells[0].FindControl("chkBox");
                string journalid = ((HiddenField)gr.Cells[1].FindControl("hfjrnlid")).Value;
                if(chkBox.Checked)
                TransferLedger(Convert.ToInt32(journalid));
            }
            MessageController.Show("Post to Ledger Successfully!!!", MessageType.Confirmation, Page);

            Bind();
        }
        catch (Exception ex)
        {
            MessageController.Show("Somthing Is Worng!!!!", MessageType.Warning, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = obj.GetDataForEditById();

            if (dt.Rows.Count > 0)
            {
                GvJournal.DataSource = dt;
                GvJournal.DataBind();
                journalDiv.Visible = true;
                btnposttoledger.Visible = false;
                btnSave.Visible = true;
                btnReset.Visible = true;
            }
            else
            {
                GvJournal.DataSource = null;
                GvJournal.DataBind();
                btnposttoledger.Visible = false;
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            MessageController.Show("Somthing Is Worng!!!!", MessageType.Warning, Page);
        }
    }

    #region GridviewFunction
    
    private void SetInitialRow()
    {
        var dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("JrTranId", typeof(string)));
        dt.Columns.Add(new DataColumn("AccountName", typeof(string)));
        dt.Columns.Add(new DataColumn("AccountCodeId", typeof(string)));
        dt.Columns.Add(new DataColumn("VNo", typeof(string)));
        dt.Columns.Add(new DataColumn("MrNo", typeof(string)));
        dt.Columns.Add(new DataColumn("Purpose", typeof(string)));

        dt.Columns.Add(new DataColumn("AId", typeof(string)));
        dt.Columns.Add(new DataColumn("HeadCodeId", typeof(string)));
        dt.Columns.Add(new DataColumn("DrAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("CrAmount", typeof(string)));

        dt.Columns.Add(new DataColumn("TDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
        dt.Columns.Add(new DataColumn("VType", typeof(string)));
        dt.Columns.Add(new DataColumn("PurchaseDate", typeof(string)));

        dt.Columns.Add(new DataColumn("TentaiveDate", typeof(string)));
        dt.Columns.Add(new DataColumn("ChequeNo", typeof(string)));
        dt.Columns.Add(new DataColumn("TrnId", typeof(string)));
        dt.Columns.Add(new DataColumn("CDate", typeof(string)));

        dr = dt.NewRow();
        dr["JrTranId"] = string.Empty;
        dr["AccountName"] = string.Empty;
        dr["AccountCodeId"] = string.Empty;
        dr["VNo"] = string.Empty;
        dr["MrNo"] = string.Empty;
        dr["Purpose"] = string.Empty;
        dr["AId"] = string.Empty;
        dr["HeadCodeId"] = string.Empty;
        dr["DrAmount"] = string.Empty;
        dr["CrAmount"] = string.Empty;
        dr["TDate"] = string.Empty;
        dr["Remarks"] = string.Empty;
        dr["VType"] = string.Empty;
        dr["PurchaseDate"] = string.Empty;
        dr["TentaiveDate"] = string.Empty;
        dr["ChequeNo"] = string.Empty;
        dr["TrnId"] = string.Empty;
        dr["CDate"] = string.Empty;

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        GvJournal.DataSource = dt;
        GvJournal.DataBind();

    }
    protected void Btnaddnewrow_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    HiddenField hfjrnlid = (HiddenField)GvJournal.Rows[rowIndex].Cells[0].FindControl("hfjrnlid");
                    TextBox box0 = (TextBox)GvJournal.Rows[rowIndex].Cells[0].FindControl("txtaccid");
                    TextBox box1 = (TextBox)GvJournal.Rows[rowIndex].Cells[1].FindControl("txtaccname");
                    TextBox box2 = (TextBox)GvJournal.Rows[rowIndex].Cells[2].FindControl("txtsubcodeid");
                    TextBox box3 = (TextBox)GvJournal.Rows[rowIndex].Cells[3].FindControl("txtpurpose");
                    TextBox box4 = (TextBox)GvJournal.Rows[rowIndex].Cells[4].FindControl("txtdramount");
                    TextBox box5 = (TextBox)GvJournal.Rows[rowIndex].Cells[5].FindControl("txtcramount");
                    TextBox box6 = (TextBox)GvJournal.Rows[rowIndex].Cells[6].FindControl("txtmrno");
                    TextBox box7 = (TextBox)GvJournal.Rows[rowIndex].Cells[7].FindControl("txtcheckno");
                    TextBox box8 = (TextBox)GvJournal.Rows[rowIndex].Cells[8].FindControl("txtvoucherno");
                    TextBox box9 = (TextBox)GvJournal.Rows[rowIndex].Cells[9].FindControl("txtvouchertype");
                    TextBox box10 = (TextBox)GvJournal.Rows[rowIndex].Cells[10].FindControl("txttrdate");
                    TextBox box11 = (TextBox)GvJournal.Rows[rowIndex].Cells[11].FindControl("txtremarks");

                    drCurrentRow = dtCurrentTable.NewRow();

                    dtCurrentTable.Rows[i - 1]["JrTranId"] = hfjrnlid.Value;
                    dtCurrentTable.Rows[i - 1]["AccountCodeId"] = box0.Text;
                    dtCurrentTable.Rows[i - 1]["AccountName"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["HeadCodeId"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Purpose"] = box3.Text;

                    dtCurrentTable.Rows[i - 1]["DrAmount"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["CrAmount"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["MrNo"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["ChequeNo"] = box7.Text;
                    dtCurrentTable.Rows[i - 1]["VNo"] = box8.Text;

                    dtCurrentTable.Rows[i - 1]["VType"] = box9.Text;
                    dtCurrentTable.Rows[i - 1]["TDate"] = box10.Text;
                    dtCurrentTable.Rows[i - 1]["Remarks"] = box11.Text;


                    rowIndex++;

                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                GvJournal.DataSource = dtCurrentTable;
                GvJournal.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousData();
    }

    private void SetPreviousData()
    {

        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HiddenField hfjrnlid = (HiddenField)GvJournal.Rows[rowIndex].Cells[0].FindControl("hfjrnlid");
                    TextBox box0 = (TextBox)GvJournal.Rows[rowIndex].Cells[0].FindControl("txtaccid");
                    TextBox box1 = (TextBox)GvJournal.Rows[rowIndex].Cells[1].FindControl("txtaccname");
                    TextBox box2 = (TextBox)GvJournal.Rows[rowIndex].Cells[2].FindControl("txtsubcodeid");
                    TextBox box3 = (TextBox)GvJournal.Rows[rowIndex].Cells[3].FindControl("txtpurpose");
                    TextBox box4 = (TextBox)GvJournal.Rows[rowIndex].Cells[4].FindControl("txtdramount");
                    TextBox box5 = (TextBox)GvJournal.Rows[rowIndex].Cells[5].FindControl("txtcramount");
                    TextBox box6 = (TextBox)GvJournal.Rows[rowIndex].Cells[6].FindControl("txtmrno");
                    TextBox box7 = (TextBox)GvJournal.Rows[rowIndex].Cells[7].FindControl("txtcheckno");
                    TextBox box8 = (TextBox)GvJournal.Rows[rowIndex].Cells[8].FindControl("txtvoucherno");
                    TextBox box9 = (TextBox)GvJournal.Rows[rowIndex].Cells[9].FindControl("txtvouchertype");
                    TextBox box10 = (TextBox)GvJournal.Rows[rowIndex].Cells[10].FindControl("txttrdate");
                    TextBox box11 = (TextBox)GvJournal.Rows[rowIndex].Cells[11].FindControl("txtremarks");

                    hfjrnlid.Value = dt.Rows[i]["JrTranId"].ToString();
                    box0.Text = dt.Rows[i]["AccountCodeId"].ToString();
                    box1.Text = dt.Rows[i]["AccountName"].ToString();

                    box2.Text = dt.Rows[i]["HeadCodeId"].ToString();
                    box3.Text = dt.Rows[i]["Purpose"].ToString();
                    box4.Text = dt.Rows[i]["DrAmount"].ToString();

                    box5.Text = dt.Rows[i]["CrAmount"].ToString();
                    box6.Text = dt.Rows[i]["MrNo"].ToString();
                    box7.Text = dt.Rows[i]["ChequeNo"].ToString();
                    box8.Text = dt.Rows[i]["VNo"].ToString();

                    box9.Text = dt.Rows[i]["VType"].ToString();
                    box10.Text = dt.Rows[i]["TDate"].ToString();
                    box11.Text = dt.Rows[i]["Remarks"].ToString();


                    rowIndex++;
                }
            }
        }
    }

    #endregion
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        obj.DeleteJournal(ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        Bind();
    }

    private bool IsBlankData()
    {
        for (int i = 0; i < GvJournal.Rows.Count; i++)
        {
            TextBox txtaccid = (TextBox)GvJournal.Rows[i].Cells[0].FindControl("txtaccid");
            TextBox txtaccname = (TextBox)GvJournal.Rows[i].Cells[1].FindControl("txtaccname");
            TextBox txtsubcodeid = (TextBox)GvJournal.Rows[i].Cells[2].FindControl("txtsubcodeid");
            TextBox txtpurpose = (TextBox)GvJournal.Rows[i].Cells[3].FindControl("txtpurpose");
            TextBox txtdramount = (TextBox)GvJournal.Rows[i].Cells[4].FindControl("txtdramount");
            TextBox txtcramount = (TextBox)GvJournal.Rows[i].Cells[5].FindControl("txtcramount");
            TextBox txtmrno = (TextBox)GvJournal.Rows[i].Cells[6].FindControl("txtmrno");
            TextBox txtcheckno = (TextBox)GvJournal.Rows[i].Cells[7].FindControl("txtcheckno");
            TextBox txtvoucherno = (TextBox)GvJournal.Rows[i].Cells[6].FindControl("txtvoucherno");
            TextBox txtvouchertype = (TextBox)GvJournal.Rows[i].Cells[7].FindControl("txtvouchertype");
            TextBox txttrdate = (TextBox)GvJournal.Rows[i].Cells[8].FindControl("txttrdate");
            TextBox txtremarks = (TextBox)GvJournal.Rows[i].Cells[9].FindControl("txtremarks");

            if (string.IsNullOrEmpty(txtaccid.Text) || string.IsNullOrEmpty(txtaccname.Text) || string.IsNullOrEmpty(txtsubcodeid.Text) || string.IsNullOrEmpty(txtdramount.Text) || string.IsNullOrEmpty(txtcramount.Text) ||
                string.IsNullOrEmpty(txtvouchertype.Text) || string.IsNullOrEmpty(txttrdate.Text))
            {
                GvJournal.Rows[i].BorderColor = Color.Red;
                return true;
            }           
        }
        return false;
    }
}