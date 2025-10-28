using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MainVoucher
/// </summary>
public class MainVoucher
{
    public int Id { get; set; }
    public string VoucherCodeId { get; set; }
    public string VoucherName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }
}