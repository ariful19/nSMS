using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for News
/// </summary>
public class News
{
    public int Id { get; set; }
    public string TitleInBangla { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string ShortDescriptionInBangla { get; set; }
    public string ShortDescription { get; set; }
    public string DetailsInBangla { get; set; }
    public string Details { get; set; }
    public string Photo { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}