using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Math;

/// <summary>
/// Summary description for Account
/// </summary>
public class Account
{
    public int Id { get; set; }
    public string AccountCodeId { get; set; }
    public string AccountName { get; set; }
    public int AId { get; set; }
    public int SubHeadId { get; set; }
    public int AppPId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int AccountTypeId { get; set; }
    public float Balance { get; set; }
    public DateTime OpenDate { get; set; }
    public string ReferanceNo { get; set; }
    public float Incentive_AMT { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }
}