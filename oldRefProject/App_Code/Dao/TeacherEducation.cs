using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TeacherEducation
/// </summary>
public class TeacherEducation
{
    public int Id { get; set; }
    public string DegreeName { get; set; }
    public string Board { get; set; }
    public string Grade { get; set; }
    public string PassingYear { get; set; }
    public string Subject { get; set; }
    public string GPAScale { get; set; }
    public int TeacherId { get; set; }
    public string ResultDivision { get; set; }
}