using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ExamRoutine
/// </summary>
public class ExamRoutine
{
    public int Id { get; set; }
    public int Year { get; set; }
    public int ClassId { get; set; }
    public int GroupId { get; set; }
    public int ShiftId { get; set; }
    public DateTime ExamDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int SubjectId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ExamTypeId { get; set; }

}