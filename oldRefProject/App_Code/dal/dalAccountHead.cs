using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalAccountHead
/// </summary>
public class dalAccountHead
{
    DatabaseManager dm= new DatabaseManager();
	public dalAccountHead()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertMainCode(AccountHead accountHead)
    {
        dm.AddParameteres("@HeadCodeId", accountHead.HeadCodeId);
        dm.AddParameteres("@HeadName", accountHead.HeadName);
        dm.AddParameteres("@AId", accountHead.AId);
        dm.AddParameteres("@Description", accountHead.Description);
        dm.AddParameteres("@MainCodeId", accountHead.MainCodeId);
        dm.AddParameteres("@UpdateOn", accountHead.UpdateOn);
        dm.AddParameteres("@Balance", accountHead.Balance);
        DataTable dt = dm.ExecuteQuery("USP_AccountHead_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int UpdateMainCode(int ID, AccountHead accountHead)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@HeadCodeId", accountHead.HeadCodeId);
        dm.AddParameteres("@HeadName", accountHead.HeadName);
        dm.AddParameteres("@AId", accountHead.AId);
        dm.AddParameteres("@Description", accountHead.Description);
        dm.AddParameteres("@MainCodeId", accountHead.MainCodeId);
        dm.AddParameteres("@UpdateOn", accountHead.UpdateOn);
        dm.AddParameteres("@Balance", accountHead.Balance);
        return dm.ExecuteNonQuery("USP_AccountHead_Update");
    }

    public DataTable GetMainHeadCodeById(int ID)
    {
        dm.AddParameteres("@Id", ID);
        return dm.ExecuteQuery("USP_AccountHead_GetById");
    }
    public int GetMainHeadCodeByAccHeadId(int ID)
    {
        dm.AddParameteres("@SubHeadId", ID);
        DataTable dt = dm.ExecuteQuery("USP_MainHeadCode_GetByAccHeadId");
        return Convert.ToInt32(dt.Rows[0][0]);

    }

    public int InsertSubAccountHead(SubAccountHead subAccountHead)
    {
        dm.AddParameteres("@SubHeadCodeId", subAccountHead.SubHeadCodeId);
        dm.AddParameteres("@SubHeadName", subAccountHead.SubHeadName);
        dm.AddParameteres("@Description", subAccountHead.Description);
        dm.AddParameteres("@HeadCodeId", subAccountHead.HeadCodeId);
        dm.AddParameteres("@CreatedBy", subAccountHead.CreatedBy);
        dm.AddParameteres("@CreatedDate", subAccountHead.CreatedDate);
        DataTable dt = dm.ExecuteQuery("USP_Sub_AccounHead_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int UpdateSubAccountHead(int ID, SubAccountHead subAccountHead)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@SubHeadCodeId", subAccountHead.SubHeadCodeId);
        dm.AddParameteres("@SubHeadName", subAccountHead.SubHeadName);
        dm.AddParameteres("@Description", subAccountHead.Description);
        dm.AddParameteres("@HeadCodeId", subAccountHead.HeadCodeId);
        dm.AddParameteres("@UpdateBy", subAccountHead.UpdateBy);
        dm.AddParameteres("@UpdateDate", subAccountHead.UpdateDate);
        return dm.ExecuteNonQuery("USP_Sub_AccounHead_Update");
    }

    public DataTable GetSubAccountHeadById(int ID)
    {
        dm.AddParameteres("@Id", ID);
        return dm.ExecuteQuery("USP_Sub_AccounHead_GetById");
    }
}