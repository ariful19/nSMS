using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administration_TeacherAttendence : BasePage
{
    dalTeacher obj = new dalTeacher();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            tbxDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            LoadDate();
            LoadTeacher();
        }
    }
    public List<DateTime> getAllDates(int year, int month)
    {
        var ret = new List<DateTime>();
        for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
        {
            DateTime dt = new DateTime(year, month, i);
            ret.Add(dt);
        }
        return ret;
    }
    protected void LoadDate()
    {
        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();
        rptDate.DataSource = getAllDates(DateTime.Now.Year, DateTime.Now.Month);
        rptDate.DataBind();
    }

   

    Regex regex = new Regex(@"[1-9]");
    private bool IsInteger(string str)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (!regex.IsMatch(str))
            {
                return false;
            }

            return true;

        }
        catch (Exception ex)
        {
            MessageController.Show("Value Must be Neumeric!!!", MessageType.Warning, Page);
        }
        return false;
    }

    protected void LoadTeacher()
    {

        string criteria = "Campus=" + ddlCampus.SelectedValue;
        DataTable dt = obj.GetTeacherByCriteria(criteria);
        ddlEmployee.DataSource = dt;
        ddlEmployee.DataBind();

        rptTeacher.DataSource = dt;
        rptTeacher.DataBind();
    }
    protected DataTable Attendence()
    {
        DataTable dtAttendence = new DataTable("dtAttendence");
        dtAttendence.Columns.Add("Date", typeof(DateTime));
        dtAttendence.Columns.Add("TId", typeof(int));
        dtAttendence.Columns.Add("timeIn", typeof(string));
        dtAttendence.Columns.Add("OutTime", typeof(string));
        return dtAttendence;
    }
    public bool ValidationCheck()
    {
        
        if (tbxDate.Text == "")
        {
            string myStringVariable = "Date Must be needed!!!";
            MessageController.Show("Date Must be needed!!!", MessageType.Warning, Page);
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
           return true;
        }
        if (DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null) > DateTime.Now)
        {
            MessageController.Show("Selected date must be smaller then current date.", MessageType.Error, Page);
            string myStringVariable = "Selected date must be smaller then current date.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            return true;
        }
        return false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidationCheck())
        {
            return;
        }
        DataTable dt = Attendence();
        string year = "";
        string month = "";
        if (rdList.SelectedValue == "1")
        {
            year = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null).Year.ToString();
            month = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null).Month.ToString();
            int r = rptTeacher.Items.Count;
            foreach (RepeaterItem item in rptTeacher.Items)
            {
                HiddenField hdnId = (HiddenField)item.FindControl("hdnId");
                TextBox tbxIn = (TextBox)item.FindControl("tbxIn");
                TextBox tbxOut = (TextBox)item.FindControl("tbxOut");
                if (tbxIn.Text != "" && tbxOut.Text != "")
                {
                    dt.Rows.Add(DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null), Convert.ToInt32(hdnId.Value), tbxIn.Text, tbxOut.Text);
                }
            }
        }
        else
        {
            foreach (RepeaterItem ri in rptDate.Items)
            {
                HiddenField hdnDate = (HiddenField)ri.FindControl("hdnDate");
                TextBox tbxIn = (TextBox)ri.FindControl("intime");
                TextBox tbxOut = (TextBox)ri.FindControl("tbxOut");
                year = Convert.ToDateTime(hdnDate.Value).Year.ToString();
                month = Convert.ToDateTime(hdnDate.Value).Month.ToString();
                if (tbxIn.Text != "" && tbxOut.Text != "")
                {
                    dt.Rows.Add(hdnDate.Value, Convert.ToInt32(ddlEmployee.SelectedValue), tbxIn.Text, tbxOut.Text);
                }

            }
        }
        if (dt.Rows.Count > 0)
        {
            obj.AttendenceInsertByDt(dt, year, month, Page.User.Identity.Name, DateTime.Now);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
        {
            MessageController.Show("No Data Found to Insert.", MessageType.Warning, Page);
        }

    }
    protected void tbxDate_TextChanged(object sender, EventArgs e)
    {
        if (tbxDate.Text != "")
        {
            if (DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null) > DateTime.Now)
            {
                MessageController.Show("Selected date must be smaller then current date.", MessageType.Error, Page);
                return;
            }
            else
            {
                DataTable dt = obj.GetByDate(DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null));
                rptTeacher.DataSource = dt;
                rptTeacher.DataBind();
            }
        }
        else
        {
            string myStringVariable = "Date Must be needed!!!";
            MessageController.Show("Date Must be needed!!!", MessageType.Warning, Page);
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
        }
        
    }
    protected void tbxArchive_TextChanged(object sender, EventArgs e)
    {
        if (tbxArchive.Text != "")
        {
            try
            {
                DateTime.Parse(tbxArchive.Text);
            }
            catch
            {
                tbxArchive.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
    }


    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdList.SelectedIndex = -1;
        LoadTeacher();
    }
}