using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentPayment
/// </summary>
public class StudentPayment
{
    public int PaymentID { get; set; }
    public int StudentId { get; set; }
    public int StudentToClassId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthYear { get; set; }
    public DateTime Date { get; set; }
    public decimal DueAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalGiven { get; set; }
    public string BillNo { get; set; }
    public int PaymentTypeId { get; set; }
    public int ClassId { get; set; }
    public string Remarks { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }

}
