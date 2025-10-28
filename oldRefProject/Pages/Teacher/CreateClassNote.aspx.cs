using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Teacher_CreateClassNote : System.Web.UI.Page
{
    dalNotes obj = new dalNotes();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Load();
            tbxDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            BindData();
        }

    }

    int SubjectToClassId
    {
        set { ViewState["SubjectToClassId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["SubjectToClassId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    protected void Load()
    {
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

        //ddlShift.DataSource = new Common().GetAll("bs_Shift");
        //ddlShift.DataBind();

        //ddlSection.DataSource = new Common().GetAll("bs_Section");
        //ddlSection.DataBind();

    }  
    protected bool ValidImage(FileUpload file)
    {
        bool flag = false;
        string extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == ".jpeg" || extension == ".jpg" || extension == ".png")
        {
            if (file.PostedFile.ContentLength < 6218595)
            {
                flag = true;
            }
        }
        return flag;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        News news = new News();
        news.Id = Convert.ToInt32(ddlSubject.SelectedValue);
        news.TitleInBangla = tbxNameBangla.Text;
        news.Title = tbxName.Text;
        news.Date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        news.ShortDescriptionInBangla = tbxShortDescriptionInBangla.Text;
        news.ShortDescription = tbxShortDescription.Text;
        news.DetailsInBangla = tbxDetailsBanglaEditor.Text;
        news.Details = tbxDetails.Text;
        news.CreatedBy = Page.User.Identity.Name;
        news.CreatedDate = DateTime.Now;

        ID = obj.Insert(news);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This news already exists in selected date. Please try anothor title.", MessageType.Error, Page);

        LoadNotes();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        News news = new News();
        news.TitleInBangla = tbxNameBangla.Text;
        news.Title = tbxName.Text;
        news.Date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        news.ShortDescriptionInBangla = tbxShortDescriptionInBangla.Text;
        news.ShortDescription = tbxShortDescription.Text;
        news.DetailsInBangla = tbxDetailsBanglaEditor.Text;
        news.Details = tbxDetails.Text;
        news.Id = Convert.ToInt32(ddlSubject.SelectedValue);
        news.UpdatedBy = Page.User.Identity.Name;
        news.UpdatedDate = DateTime.Now;

        obj.Update(ID, news);
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);

        LoadNotes();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxNameBangla.Text = "";
        tbxName.Text = "";
        tbxDate.Text = "";
        tbxShortDescription.Text = "";
        tbxDetails.Text = string.Empty;
        tbxShortDescriptionInBangla.Text = "";
        tbxDetailsBanglaEditor.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
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
        ClearAll();
        DataTable dt=new DataTable();
        string criteria = "SubjectToClassId=" + ddlSubject.SelectedValue;
        if(ddlSubject.SelectedValue!="")
         dt= obj.GetByCriteria(criteria);
        if (dt.Rows.Count > 0)
        {
            rptNote.DataSource = dt;
            rptNote.DataBind();
        }
        else
        {
            rptNote.DataSource = null;
            rptNote.DataBind();
        }
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {

            //string fileName = dt.Rows[0]["Photo"].ToString();
            //Session["UpdatedFileName"] = fileName;

            //image.ImageUrl = "../../Images/Person/" + dt.Rows[0]["Photo"].ToString();

            tbxNameBangla.Text = dt.Rows[0]["TitleInBangla"].ToString();
            tbxName.Text = dt.Rows[0]["Title"].ToString();
            tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString("dd/MM/yyyy");
            tbxShortDescriptionInBangla.Text = dt.Rows[0]["ShortDescriptionInBangla"].ToString();
            tbxShortDescription.Text = dt.Rows[0]["ShortDescription"].ToString();
            tbxDetailsBanglaEditor.Text = dt.Rows[0]["DetailsInBangla"].ToString();
            tbxDetails.Text = dt.Rows[0]["Details"].ToString();
            ddlSubject.SelectedValue = dt.Rows[0]["SubjectToClassId"].ToString();

        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("bs_Notes", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        LoadNotes();
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
  
}