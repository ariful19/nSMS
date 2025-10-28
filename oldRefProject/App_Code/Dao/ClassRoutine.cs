using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassRoutine
/// </summary>
public class ClassRoutine
{
    public int Id { get; set; }
    public int Year { get; set; }
    public int ClassId { get; set; }
    public int GroupId { get; set; }
    public int ShiftId { get; set; }
    public int SectionId { get; set; }
    public int Period { get; set; }
    public int SubjectId { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Day { get; set; }
    public int TeacherId { get; set; }

}