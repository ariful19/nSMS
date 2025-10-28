using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HomeMenu
/// </summary>
public class HomeMenu
{
    public int MenuID { get; set; }
    public int PageContentID { get; set; }
    public string TextBan { get; set; }
    public string TextEng { get; set; }
    public string URL { get; set; }
    public int ParentId { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public string UpdatedDate { get; set; }
    public bool ForAll { get; set; }
    public int Order { get; set; }
    public string Icon { get; set; }
    public bool IsPublish { get; set; }
    public DateTime PublishStartDate { get; set; }
    public DateTime PublishEndDate { get; set; }
}