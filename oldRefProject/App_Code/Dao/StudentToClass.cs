using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentToClass
/// </summary>
public class StudentToClass
{
    public int StudentToClassID { get; set; }
    public int StudentId { get; set; }
    public int MediumId { get; set; }
    public int ClassId { get; set; }
    public int CampusId { get; set; }
    public int GroupId { get; set; }
    public int ShiftId { get; set; }
    public int SectionId { get; set; }
    public string Year { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public string UpdatedDate { get; set; }
    public int RollNo { get; set; }

}