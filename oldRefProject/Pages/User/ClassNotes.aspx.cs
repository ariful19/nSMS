using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_ClassNotes : BasePage
{
    dalNotes obj = new dalNotes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {         
            Load();
            BindData();
        }
    }
    protected void Load()
    {
        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();       

    }

    private void BindData()
    {
        DataTable dt = new dalSubject().GetByClassAndGroupId(Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            ddlSubject.DataSource = dt;
            ddlSubject.DataBind();
        }
        else
        {
            ddlSubject.DataSource = dt;
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, new ListItem("--No Subject Set Yet--", string.Empty));
            ddlSubject.SelectedIndex = 0;
        }
        LoadNotes();
    }
    private void LoadNotes()
    {
        DataTable dt = new DataTable();
        string criteria = "SubjectToClassId=" + ddlSubject.SelectedValue;
        if(ddlSubject.SelectedValue!="")
        if (Request.Cookies["CurrentLanguage"].Value == "bn-BD")
        {
            dt = obj.GetBanglaNotes(criteria);

        }
        else
        {
            dt = obj.GetByCriteria(criteria);

        }
        
        if (dt.Rows.Count > 0)
        {
            rptNotes.DataSource = dt;
            rptNotes.DataBind();
        }
        else
        {
            rptNotes.DataSource = null;
            rptNotes.DataBind();
        }
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        BindData();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadNotes();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadNotes();
    }
}