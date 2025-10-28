using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IssueBook
/// </summary>
public class IssueBook
{
	public int Id { get; set; }
    public int BookId { get; set; }
    public bool IsReturn { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string IssueBy { get; set; }
    public string ReceivedBy { get; set; }
    public DateTime ReceivedDate { get; set; }
}