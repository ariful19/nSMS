using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admission_AdmissionInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAllDropdownList();
        }
    }

    protected void btnViewStudent_Click(object sender, EventArgs e)
    {
        LoadStudentAdmissionInformation();
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=AdmissionInformation&SelectId=" + ddlMedium.SelectedItem.Text + "&Type=" + ddlCampus.SelectedItem.Text + "&AnonymusType=" +GetCriterias());
            Response.Redirect("~/Report/../Report/Viewer/ReportView.aspx");

        }
    }

    protected string GetCriteria()
    {
        string criteria = "";

        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
            else
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedValue != "" && ddlMedium.SelectedIndex!=0)
        {
            if (criteria == "")
                criteria = "er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "" && ddlCampus.SelectedIndex != 0)
        {
            if (criteria == "")
                criteria = "er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;

        }
       
        criteria += " and ss_Student.StatusId= 1";

        return criteria;
    }

  protected string GetCriterias()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + (Convert.ToInt32(ddlYear.SelectedValue) - 1);
            else
                criteria += " er_StudentToClass.Year=" + (Convert.ToInt32(ddlYear.SelectedValue) - 1);
        }
        if (ddlMedium.SelectedValue != "" && ddlMedium.SelectedIndex != 0)
        {
            if (criteria == "")
                criteria = "er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "" && ddlCampus.SelectedIndex != 0)
        {
            if (criteria == "")
                criteria = "er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;

        }

        criteria += " and ss_Student.StatusId= 1";
        return criteria;
    }

    protected void LoadAllDropdownList()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();


        ddlCampus.Items.Insert(0, new ListItem("  All  ", string.Empty));
        ddlCampus.SelectedIndex = 0;

        ddlMedium.Items.Insert(0, new ListItem("  All  ", string.Empty));
        ddlMedium.SelectedIndex = 0;
        //ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        //ddlClass.DataBind();

        //ddlGroup.DataSource = new Common().GetAll("bs_Group");
        //ddlGroup.DataBind();

        //ddlShift.DataSource = new Common().GetAll("bs_Shift");
        //ddlShift.DataBind();

        //ddlSection.DataSource = new Common().GetAll("bs_Section");
        //ddlSection.DataBind();

        //ddlGender.DataSource = new Common().GetAll("bs_Gender");
        //ddlGender.DataBind();

    }
    public DataTable AdmissionDt()
    {
        DataTable dt = new DataTable("AdmissionDt");
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("StudentCount", typeof(int));
        dt.Columns.Add("Science", typeof(int));
        dt.Columns.Add("Commerce", typeof(int));
        dt.Columns.Add("None", typeof(int));
        dt.Columns.Add("Boys", typeof(int));
        dt.Columns.Add("Girls", typeof(int));
        dt.Columns.Add("Other", typeof(int));
        return dt;
    }

    private void LoadStudentAdmissionInformation()
    {
       DataTable dt = new dalStudent().GetAllAdmittedStudentInfo(GetCriteria());

        if (dt.Rows.Count > 0)
        {
            MessageController.Clear(this);
            pnlAssignStudent.Visible = true;
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
        }
        else
        {
            pnlAssignStudent.Visible = false;
            MessageController.Show("No Student Found.", MessageType.Error, Page);
        }

    }



}






