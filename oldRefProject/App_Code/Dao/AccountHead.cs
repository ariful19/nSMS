using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccountHead
/// </summary>
public class AccountHead
{
	public int Id { get; set; }
    public int HeadCodeId { get; set; }
    public string HeadName { get; set; }
    public int AId { get; set; }
    public string Description { get; set; }
    public int MainCodeId { get; set; }
    public DateTime UpdateOn { get; set; }
    public decimal Balance { get; set; }
    public int AssetType { get; set; }
}