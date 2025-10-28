using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Teacher_CreateNotice : System.Web.UI.Page
{
    dalStudent obj = new dalStudent();
    dalNotice objNotice = new dalNotice();
    public static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MessageController.Clear(this);
            tbxDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Load();
        }
    }

    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
        ddlYear.DataBind();

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

        ddlSpecificYear.DataSource = new dalYear().GetDefault();
        ddlSpecificYear.DataBind();

        ddlSpecificMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlSpecificMedium.DataBind();

        ddlSpecificCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlSpecificCampus.DataBind();

        chkSpecificClass.DataSource = new Common().GetAll("bs_ClassName");
        chkSpecificClass.DataBind();

        rptTeacher.DataSource = new dalTeacher().GetAll();
        rptTeacher.DataBind();
    }

    #endregion

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Year=" + ddlYear.SelectedValue;
            else
                criteria += " Year=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and CampusId=" + ddlCampus.SelectedValue;
        }
        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ClassId=" + ddlClass.SelectedValue;
            else
                criteria += " and ClassId=" + ddlClass.SelectedValue;
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and GroupId=" + ddlGroup.SelectedValue;
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ShiftId=" + ddlShift.SelectedValue;
            else
                criteria += " and ShiftId=" + ddlShift.SelectedValue;
        }
        if (ddlSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "SectionId=" + ddlSection.SelectedValue;
            else
                criteria += " and SectionId=" + ddlSection.SelectedValue;
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "and" + tbxReg.Text;
            else
                criteria += " and RegNo= " + tbxReg.Text;
        }
        criteria += " and ss_Student.IsActive='True'";
        return criteria;
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        string criteria = GetCriteria();
        DataTable dt = obj.GetByCriteria(criteria);
        if (dt.Rows.Count > 0)
        {
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
        }
        else
        {
            rptStudent.DataSource = null;
            rptStudent.DataBind();
            MessageController.Show("No Student Found!!!", MessageType.Warning, Page);
        }
    }
    public DataTable GetPerson()
    {
        DataTable dt = new DataTable("dtPerson");
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("NoticeId", typeof(int));
        return dt;
    }
    protected void btnNotice_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        ID = objNotice.Insert(tbxName.Text, tbxShortDescription.Text, tbxDetails.Text, DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), Page.User.Identity.Name, DateTime.Now);
        if (ID > 0)
        {
            #region Normal Notification

            if (rdList.SelectedValue == "1")
            {
                if (rdList1.SelectedValue == "1")
                {
                    DataTable dt = new dalYear().GetDefault();
                    if (dt.Rows.Count > 0)
                    {
                        objNotice.InsertForAllClass(Convert.ToInt32(dt.Rows[0]["Year"]),0, 0,0, ID);
                        MessageController.Show("Created Notice successfully send by All Students.", MessageType.Information, Page);
                    }
                }
                else if (rdList1.SelectedValue == "2")
                {
                    foreach (ListItem li in chkSpecificClass.Items)
                    {
                        if (li.Selected)
                        {
                            objNotice.InsertForAllClass(Convert.ToInt32(ddlSpecificYear.SelectedValue),Convert.ToInt32(ddlSpecificMedium.SelectedValue),Convert.ToInt32(ddlSpecificCampus.SelectedValue), Convert.ToInt32(li.Value), ID);
                            MessageController.Show("Created Notice successfully send by Selected Class Students.", MessageType.Information, Page);
                        }
                    }

                }
                else
                {
                    DataTable dtStudent = GetPerson();
                    foreach (RepeaterItem item in rptStudent.Items)
                    {
                        CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
                        HiddenField hdnId = (HiddenField)item.FindControl("hdnStudentId");
                        if (chkRow.Checked)
                        {
                            dtStudent.Rows.Add(Convert.ToInt32(hdnId.Value), ID);
                        }
                    }
                    objNotice.InsertForSpecificStudent(dtStudent);
                    MessageController.Show("Created Notice successfully send by Selected Student.", MessageType.Information, Page);
                }
            }
            else if (rdList.SelectedValue == "2")
            {
                DataTable dtPerson = GetPerson();
                foreach (RepeaterItem item in rptTeacher.Items)
                {
                    CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
                    HiddenField hdnId = (HiddenField)item.FindControl("hdnTeacherId");
                    if (chkRow.Checked)
                    {
                        dtPerson.Rows.Add(Convert.ToInt32(hdnId.Value), ID);
                    }
                }
                objNotice.InsertForSpecificTeacher(dtPerson);
                MessageController.Show("Created Notice successfully send by Selected Teacher.", MessageType.Information, Page);
            }
            #endregion
        }
    }
}