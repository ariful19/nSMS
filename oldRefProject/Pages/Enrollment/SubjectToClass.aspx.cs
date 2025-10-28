using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Enrollment_SubjectToClass : BasePage
{
    dalSubject obj = new dalSubject();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAll();
            BindData();
        }
    }
    protected void LoadAll()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();
       
        ddlSubject.DataSource = new Common().GetAll("bs_SubjectName");
        ddlSubject.DataBind();

        ddlCategory.DataSource = new Common().GetAll("bs_SubjectCategory");
        ddlCategory.DataBind();

        rptCategory.DataSource = new Common().GetAll("bs_SubjectCategory");
        rptCategory.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
            if (ddlCategory.SelectedValue == "2" || ddlCategory.SelectedValue == "3")
            {
                for (int i = 1; i <= 2; i++)
                {
                    ID = obj.Insert(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue),
                Convert.ToInt32(ddlCategory.SelectedValue),Convert.ToInt32(tbxOrderBy.Text), i, Convert.ToBoolean(chkOPtional.Checked), Page.User.Identity.Name);
                }
            }
            else
            {
                    ID = obj.Insert(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue),
                   Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(tbxOrderBy.Text), 0, Convert.ToBoolean(chkOPtional.Checked), Page.User.Identity.Name);         
            }
            if (ID != -1)
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            else
                MessageController.Show("This subject or Order By already exists in this class. Please try anothor.", MessageType.Error, Page);
        BindData();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        obj.Update(ID, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue),
                   Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(tbxOrderBy.Text), 0, Convert.ToBoolean(chkOPtional.Checked), Page.User.Identity.Name);
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        btnSave.Text = "Save";
        //((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
        tbxOrderBy.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            ddlYear.SelectedValue = dt.Rows[0]["YearId"].ToString();
            ddlMedium.SelectedValue = dt.Rows[0]["MediumId"].ToString();
            ddlCampus.SelectedValue = dt.Rows[0]["CampusId"].ToString();
            ddlClass.SelectedValue = dt.Rows[0]["ClassId"].ToString();
            ddlGroup.SelectedValue = dt.Rows[0]["GroupId"].ToString();
            ddlSubject.SelectedValue = dt.Rows[0]["SubjectId"].ToString();
            tbxOrderBy.Text = dt.Rows[0]["OrderBy"].ToString();
            chkOPtional.Checked = Convert.ToBoolean(dt.Rows[0]["IsOptional"].ToString());
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void BindData()
    {
        DataTable dt = obj.GetByClassAndGroupId(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        rptSubject.DataSource = dt;
        rptSubject.DataBind();
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("er_SubjectToClass", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
       BindData();
    }
}

