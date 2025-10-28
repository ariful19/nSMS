using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalMessageCredit
/// </summary>
public class dalMessageCredit
{
    DatabaseManager dm = new DatabaseManager();
	public dalMessageCredit()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int MessageCreditInsert(MessageCredit messageCredit)
    {
        dm.AddParameteres("@PurchaseDate", messageCredit.PurchaseDate);
        dm.AddParameteres("@PurchaseQuantity", messageCredit.PurchaseQuantity);
        dm.AddParameteres("@SendQuantity", messageCredit.SendQuantity);
        dm.AddParameteres("@Balance", messageCredit.Balance);
        dm.AddParameteres("@AvailableBalance", messageCredit.AvailableBalance);
        dm.AddParameteres("@TransactionNumber", messageCredit.TransactionNumber);
        dm.AddParameteres("@Status", messageCredit.Status);
        dm.AddParameteres("@CreatedBy", messageCredit.CreatedBy);
        dm.AddParameteres("@CreatedDate", messageCredit.CreatedDate);
        dm.AddParameteres("@IsDefault", messageCredit.IsDefault);

        DataTable dt = dm.ExecuteQuery("USP_MessageCreditInsert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int MessageCreditUpdate(int id, MessageCredit messageCredit)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@PurchaseDate", messageCredit.PurchaseDate);
        dm.AddParameteres("@PurchaseQuantity", messageCredit.PurchaseQuantity);
        dm.AddParameteres("@Balance", messageCredit.Balance);
        dm.AddParameteres("@TransactionNumber", messageCredit.TransactionNumber);
        dm.AddParameteres("@Status", messageCredit.Status);
        dm.AddParameteres("@IsDefault", messageCredit.IsDefault);

        return dm.ExecuteNonQuery("USP_MessageCredit_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_MessageCredit_GetById");
    }

    public DataTable GetAllMessageCredit()
    {

        return dm.ExecuteQuery("USP_MessageCredit_GetDefault");
        //dm.AddParameteres("@Table", "MessageCredit");
        //return dm.ExecuteQuery("USP_Get_All");
    }

    public void UpdateCredit(MessageCredit msgCredit)
    {
        dm.AddParameteres("@Id", msgCredit.Id);
        dm.AddParameteres("@SendQuantity", msgCredit.SendQuantity);
        dm.AddParameteres("@AvailableBalance", msgCredit.AvailableBalance);
        dm.ExecuteQuery("USP_MessageCredit_BalanceUpdate");
    }
    public DataTable GetDefault()
    {
        return dm.ExecuteQuery("USP_MessageCredit_GetDefault");
    }

    public DataTable GetMessageCreditByDate(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_MessageCredit_GetByCriteria");
    }
}