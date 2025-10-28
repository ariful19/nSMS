using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ledger
/// </summary>
public class Ledger
{

    public int SrNo { get; set; }
    public DateTime TDate { get; set; }
    public string AccountCodeId { get; set; }
    public string Description { get; set; }
    public Int64 OpeningBalance { get; set; }
    public Int64 DrAmount { get; set; }
    public Int64 CrAmount { get; set; }    
    public Int64 ClBalance { get; set; }
    public int AId { get; set; }  
    public int JrTranId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }
   

}