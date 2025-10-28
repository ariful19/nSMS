using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Journal
/// </summary>
public class Journal
{
    public int JrTranId { get; set; }
    public DateTime TDate { get; set; }
    public string VNo { get; set; }
    public string MrNo { get; set; }
    public string Purpose { get; set; }
    public int AId { get; set; }
    public string AccountCodeId { get; set; }
    public Int64 HeadCodeId { get; set; }
    public Int64 DrAmount { get; set; }
    public Int64 CrAmount { get; set; }
    public string Flag { get; set; }
    public string Remarks { get; set; }
    public string VType { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime TentaiveDate { get; set; }
    public string ChequeNo { get; set; }
    public int TrnId { get; set; }
    public DateTime CDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsLgledger { get; set; }
}