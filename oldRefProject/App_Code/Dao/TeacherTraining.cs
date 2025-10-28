using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TeacherTraining
/// </summary>
public class TeacherTraining
{
    public int TrainingID { get; set; }
    public string TrainingName { get; set; }
    public string InstituteName { get; set; }
    public DateTime  StartDate { get; set; }
    public DateTime  EndDate { get; set; }
    public int TeacherId { get; set; }
    public string Topics { get; set; }
    public string Duration { get; set; }

}