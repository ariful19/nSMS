using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MessageCredit
/// </summary>
public class MessageCredit
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int PurchaseQuantity { get; set; }
    public int SendQuantity { get; set; }
    public decimal Balance { get; set; }
    public decimal AvailableBalance { get; set; }
    public string TransactionNumber { get; set; }
    public bool Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDefault { get; set; }

}