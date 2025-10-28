using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Result_FailSystem : BasePage
{
    dalSubject objSubject = new dalSubject();
    dalFailSystem obj = new dalFailSystem();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            LoadSubject();
            BindData();
        }
    }
    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlExamtype.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamtype.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();
    }
    protected void BindData()
    {
        DataTable dt = obj.GetFailSystem(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlExamtype.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        rptFailSystem.DataSource = dt;
        rptFailSystem.DataBind();
    }
    
   protected void LoadSubject()
    {
        DataTable dt = objSubject.GetByClassAndGroupId(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        ddlSubject.DataSource = dt;
        ddlSubject.DataBind();
    }
    #endregion

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
        BindData();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
        BindData();
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
        BindData();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int id = 0;
        if (btnSave.Text == "Save")
        {
            id = obj.Insert(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlExamtype.SelectedValue),Convert.ToInt32(ddlSubject.SelectedValue), Convert.ToDouble(tbxSubjective.Text), Convert.ToDouble(tbxObjective.Text), Page.User.Identity.Name);
            if (id != -1)
            {
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            }
            else
            {
                MessageController.Show("Marks already exists in this subject. Please select another subject.", MessageType.Error, Page);
            }
        }
        else
        {
            obj.Update(ID, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlExamtype.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue), Convert.ToDouble(tbxSubjective.Text), Convert.ToDouble(tbxObjective.Text), Page.User.Identity.Name);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        BindData();
        tbxSubjective.Text = "";
        tbxObjective.Text = "";
        btnSave.Text = "Save";
    }
    
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            ddlClass.SelectedValue = dt.Rows[0]["ClassId"].ToString();
            ddlGroup.SelectedValue = dt.Rows[0]["GroupId"].ToString();
            ddlSubject.SelectedValue = dt.Rows[0]["SubjectToClassId"].ToString();
            tbxSubjective.Text = dt.Rows[0]["SubjectiveFailMarks"].ToString();
            tbxObjective.Text = dt.Rows[0]["ObjectiveFailMarks"].ToString();
        }
        btnSave.Text = "Edit";
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("rs_FailSystem", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
}

