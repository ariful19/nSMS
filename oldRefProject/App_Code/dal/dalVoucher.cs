using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalVoucher
/// </summary>
public class dalVoucher
{
    DatabaseManager dm = new DatabaseManager();
	public dalVoucher()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertMainVoucher(MainVoucher aMainVoucher)
    {
        dm.AddParameteres("@VoucherCodeId", aMainVoucher.VoucherCodeId);
        dm.AddParameteres("@VoucherName", aMainVoucher.VoucherName);
        dm.AddParameteres("@CreatedBy", aMainVoucher.CreatedBy);
        dm.AddParameteres("@CreatedDate", aMainVoucher.CreatedDate);
        DataTable dt = dm.ExecuteQuery("USP_Main_Voucher_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetMainVoucherById(int ID)
    {
        dm.AddParameteres("@Id", ID);
        return dm.ExecuteQuery("USP_Main_Voucher_GetById");
    }

    public int UpdateMainVoucher(int ID, MainVoucher aMainVoucher)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@VoucherCodeId", aMainVoucher.VoucherCodeId);
        dm.AddParameteres("@VoucherName", aMainVoucher.VoucherName);
        dm.AddParameteres("@UpdateBy", aMainVoucher.UpdateBy);
        dm.AddParameteres("@UpdateDate", aMainVoucher.UpdateDate);
        return dm.ExecuteNonQuery("USP_Main_Voucher_Update");
    }

    public int InsertSubVoucher(string mainVoucherCode, string subVoucherCode, string subVoucherName)
    {
        dm.AddParameteres("@VoucherCodeId", mainVoucherCode);
        dm.AddParameteres("@SubVoucherId", subVoucherCode);
        dm.AddParameteres("@SubVoucherName", subVoucherName);
        DataTable dt = dm.ExecuteQuery("USP_Sub_Voucher_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int UpdateSubVoucher(int ID, string mainVoucherCode, string subVoucherCode, string subVoucherName)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@SubVoucherId", mainVoucherCode);
        dm.AddParameteres("@VoucherCodeId", subVoucherCode);
        dm.AddParameteres("@SubVoucherName", subVoucherName);
        return dm.ExecuteNonQuery("USP_Sub_Voucher_Update");
    }

    public DataTable GetSubVoucherdById(int ID)
    {
        dm.AddParameteres("@Id", ID);
        return dm.ExecuteQuery("USP_Sub_Voucher_GetById");
    }
}