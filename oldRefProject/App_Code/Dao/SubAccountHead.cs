using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubAccountHead
/// </summary>
public class SubAccountHead
{
    public int Id { get; set; }
    public int SubHeadCodeId { get; set; }
    public string SubHeadName { get; set; }
    public int AId { get; set; }
    public string Description { get; set; }
    public int HeadCodeId { get; set; }
    public DateTime UpdateOn { get; set; }
    public decimal Balance { get; set; }
    public int AssetType { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }
}