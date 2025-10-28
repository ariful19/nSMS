using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalReport
/// </summary>
public class dalReport
{
    DatabaseManager dm = new DatabaseManager();
	public dalReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetStudentAttendence(string criteria)
    {
        dm.AddParameteres("@Criteria",criteria);
        return dm.ExecuteQuery("USP_Report_GetStudentAttendence");

    }
    public DataTable GetMarks(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetMarks");
    }

    public DataTable GetSubjectMarks(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetSubjectMarks");
    }
    public DataTable GetStudentforIdCard(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetforStudentIdCard");
    }
    public DataTable GetTeacherforIdCard()
    {
        //dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetforTeacherIdCard");
    }
    public DataTable GetStudentInfoByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("[USP_Report_GetStudentInformation]");
    }
    public DataTable GetAllAssignStudentInfoByCriteria()
    {
        //dm.AddParameteres("@Year", year);
        return dm.ExecuteQuery("[USP_Report_GetAssignStudentInformation]");
    }
    public DataTable GetUnassignStudent(int year)
    {
        dm.AddParameteres("@Year", year);
        return dm.ExecuteQuery("USP_Student_UnassignStudentWithoutYear");
    }
    public DataTable GetStudentMarksInfoByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        //return dm.ExecuteQuery("[USP_Report_StudentGetForMarksEdit]");
        return dm.ExecuteQuery("[USP_Report_StudentGetForMarksDetails]");
    }
    public DataTable GetStudentDuepayment(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Get_Student_DuePement");
    }

    public DataTable GetStudentDetails(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("[USP_Student_GetInformationByCriteria]");
    }

    public DataTable GetTabulationSheetHeaderInfo(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_GetTabulationSheetHeaderInfo");
    }

    public DataTable GetSchoolInformation()
    {
        return dm.ExecuteQuery("USP_Report_GetALLSchoolInformation");
    }
 public DataTable GetStudentforAdmitCard(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetforStudentAdmitCard");
    }
}

