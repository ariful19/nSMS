using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_EmployeeSalaryReport : System.Web.UI.Page
{
    dalPayroll objpayroll = new dalPayroll();
    dalTeacher objTeacher = new dalTeacher();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
        }
    }

    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlMonth.DataSource = new Common().GetAll("bs_Month");
        ddlMonth.DataBind();

        ddlGrade.DataSource = new Common().GetAll("tbl_Grade");
        ddlGrade.DataBind();

        ddlLevel.DataSource = new Common().GetAll("tbl_Level");
        ddlLevel.DataBind();
    }

    DataTable dtTotal
    {
        set { ViewState["dtTotal"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtTotal"];
            }
            catch
            {
                return null;
            }
        }
    }
    public DataTable SalaryDetails()
    {
        DataTable dt = new DataTable("Salary");
        dt.Columns.Add("EmployeeId", typeof(int));
        dt.Columns.Add("NameEng", typeof(string));
        dt.Columns.Add("CampusName", typeof(string));
        dt.Columns.Add("Designation", typeof(string));
        dt.Columns.Add("Year", typeof(string));
        dt.Columns.Add("Month", typeof(string));
        dt.Columns.Add("Amount", typeof(double));
        return dt;
    }
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "YearId=" + ddlYear.SelectedValue;
            else
                criteria += " and YearId=" + ddlYear.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "bs_CampusName.Id=" + ddlCampus.SelectedValue;
            else
                criteria += " and bs_CampusName.Id=" + ddlCampus.SelectedValue;
        }

        if (ddlMonth.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "MonthId=" + ddlMonth.SelectedValue;
            else
                criteria += " and MonthId=" + ddlMonth.SelectedValue;
        }

        if (ddlGrade.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "tr_Teacher.GradeId=" + ddlGrade.SelectedValue;
            else
                criteria += " and tr_Teacher.GradeId=" + ddlGrade.SelectedValue;
        }

        if (ddlLevel.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "tr_Teacher.LevelId=" + ddlLevel.SelectedValue;
            else
                criteria += " and tr_Teacher.LevelId=" + ddlLevel.SelectedValue;
        }

        return criteria;
    }



    protected void btnSalaryCash_Click(object sender, EventArgs e)
    {
        string details = "salaryCash";
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=EmployeeSalary&Type=" + details + "&SelectId=" + ddlGrade.SelectedValue +"&LevelId="+ddlLevel.SelectedValue +"&CampusId=" + ddlCampus.SelectedValue+"&YearId="+ddlYear.SelectedValue+"&MonthId="+ddlMonth.SelectedValue );
        Response.Redirect("../../Report/Viewer/ReportView.aspx");
    }

    protected void btnSalaryBank_Click(object sender, EventArgs e)
    {
        string details = "salaryBank";
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=EmployeeSalary&Type=" + details + "&SelectId=" + ddlGrade.SelectedValue + "&LevelId=" + ddlLevel.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&MonthId=" + ddlMonth.SelectedValue);
    }
    protected void btnSalarySheet_Click(object sender, EventArgs e)
    {
        string details = "details";
        //EmployeeSalary();
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=EmployeeSalary&Type=" + details + "&SelectId=" + ddlGrade.SelectedValue + "&LevelId=" + ddlLevel.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&MonthId=" + ddlMonth.SelectedValue);
    }
    protected void btnSignatureBank_Click(object sender, EventArgs e)
    {
        string details = "signatureBank";
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=EmployeeSalary&Type=" + details + "&SelectId=" + ddlGrade.SelectedValue + "&LevelId=" + ddlLevel.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&MonthId=" + ddlMonth.SelectedValue);
    }

    protected void btnSignatureCash_Click(object sender, EventArgs e)
    {
        string details = "signatureCash";
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=EmployeeSalary&Type=" + details + "&SelectId=" + ddlGrade.SelectedValue + "&LevelId=" + ddlLevel.SelectedValue + "&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&MonthId=" + ddlMonth.SelectedValue);
    }

    protected void btnSummary_Click(object sender, EventArgs e)
    {
        string details = "Summary";
        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=EmployeeSalary&Type="+details +"&CampusId=" + ddlCampus.SelectedValue + "&YearId=" + ddlYear.SelectedValue + "&MonthId=" + ddlMonth.SelectedValue);
    }


    

}