using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_Email : BasePage
{
    dalStudent obj = new dalStudent();
    dalEmailTemplete objEmail = new dalEmailTemplete();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Visible = false;
            Load();
            LoadEmailTemplete();
        }
    }

    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
        ddlYear.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        ddlEmailTemplte.DataSource = new Common().GetAll("EmailTemplate");
        ddlEmailTemplte.DataBind();

    }
    protected void LoadEmailTemplete()
    {
        if (!string.IsNullOrEmpty(ddlEmailTemplte.SelectedValue))
        {
            DataTable dt = objEmail.GetById(Convert.ToInt32(ddlEmailTemplte.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                lblSubject.Text = dt.Rows[0]["Subject"].ToString();
                lblBody.Text = dt.Rows[0]["Body"].ToString();
            }
        }

    }
    #endregion

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Year=" + ddlYear.SelectedItem.Text;
            else
                criteria += " Year=" + ddlYear.SelectedItem.Text;
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
        return criteria;
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string criteria = GetCriteria();
        DataTable dt = obj.GetByCriteria(criteria);
        if (dt.Rows.Count > 0)
        {
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
            btnSave.Visible = true;
        }
        else
        {
            rptStudent.DataSource = null;
            rptStudent.DataBind();
            btnSave.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void ddlEmailTemplte_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmailTemplete();
    }
}