using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admission_StudentList : System.Web.UI.Page
{
    protected static int ID;
    protected static string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack )
        {
            LoadAllDropdownList();
        }
    }
    protected void btnViewStudent_Click(object sender, EventArgs e)
    {
        LoadStudentInformation();
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
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;

        }
        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.ClassID=" + ddlClass.SelectedValue;
            else
                criteria += " and er_StudentToClass.ClassID=" + ddlClass.SelectedValue;
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.GroupID=" + ddlGroup.SelectedValue;
            else
                criteria += " and er_StudentToClass.GroupID=" + ddlGroup.SelectedValue;
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
            else
                criteria += " and er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
        }
        if (ddlSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
            else
                criteria += " and er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
        }
        //criteria += " and ss_Student.IsActive='True'";
          criteria += " and ss_Student.StatusId= 1";

        return criteria;     
    }

    protected void LoadAllDropdownList()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();

        //ddlYear.DataSource = new dalYear().GetDefault();
        //ddlYear.DataBind();

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

    }


    private void CheckParallel(DataTable dt)
    {
       
    }
    private void LoadStudentInformation()
    {
        DataTable dt = new dalStudent().GetByStudentCriteria(GetCriteria());

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
    protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hf = e.Item.FindControl("hdnStudent") as HiddenField;
        if (hf != null)
        {
            string val = hf.Value;
            Image img = e.Item.FindControl("Image1") as Image;
            img.ImageUrl = "~/Images/Student/" + val;
        }
    }
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        if (Page.User.IsInRole("Admin"))
        {
            MessageController.Clear(this);
            userName = e.CommandArgument.ToString();
            Response.Redirect("~/Pages/User/StudentProfile.aspx?UserName=" + userName);
        }

    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        MessageController.Clear(this);
        try
        {
            userName = e.CommandArgument.ToString();
            ID = Controller.PersonIdByUserName(userName);
            if (ID > 0)
            {
                new dalStudent().DeleteStudentById(ID, Page.User.Identity.Name, DateTime.Now, false);
            }
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            Response.Redirect("~/Pages/Admission/StudentList.aspx");
        }
        catch (Exception ex)
        {
          MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
}

