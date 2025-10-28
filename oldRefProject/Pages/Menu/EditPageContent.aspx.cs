using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Menu_EditPageContent : System.Web.UI.Page
{
    protected static int ID=0;
    int menuid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            Clear();
        }

        if (Request.QueryString["preview"] == "1" && !string.IsNullOrEmpty(Request.QueryString["fileId"]))
        {
            var fileId = Request.QueryString["fileId"];
            var fileContents = (byte[])Session["fileContents_" + fileId];
            var fileContentType = (string)Session["fileContentType_" + fileId];
            if (fileContents != null)
            {
                Response.Clear();
                Response.ContentType = fileContentType;
                Response.BinaryWrite(fileContents);
                Response.End();
            }
        }
        
    }

    protected void BindData()
    {
        this.rptPageContent.DataSource = null;
        DataTable dt = new dalPageContent().GetData();
        this.rptPageContent.DataSource = dt;
        this.rptPageContent.DataBind();
    }

    public void Clear()
    {
        this.hdfPageContentId.Value = "";
        tbxDetails.Text = "";
        tbxDetailsEditorInBangla.Text = "";
        this.txtPageTitle.Text = "";
        this.txtPageName.Text = "";
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var sdf = string.IsNullOrEmpty(hdfPageContentId.Value) ? "" : hdfPageContentId.Value.ToString();
        int pageContentId = 0;
       
        if (sdf != "")
        {
            pageContentId = Convert.ToInt32(sdf);
        }

        if (pageContentId > 0)
        {
            if (UpdatePageContent(pageContentId) > 0)
            {
                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            }
            else
            {
                MessageController.Show("Information save failed. Please try anothor.", MessageType.Error, Page);
            }

            Clear();
            BindData();
        }
        else
        {
            if (InsertPageContent() > 0)
            {
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            }
            else
            {
                MessageController.Show("Information save failed. Please try anothor.", MessageType.Error, Page);
            }

            Clear();
            BindData();

        }

    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        
        ID = Convert.ToInt32(e.CommandArgument);

        DataTable dt = new dalPageContent().GetById(ID);
        if (dt.Rows.Count > 0)
        {
            hdfPageContentId.Value = dt.Rows[0]["PageContentID"].ToString();
            txtPageName.Text = dt.Rows[0]["PageName"].ToString();
            txtPageTitle.Text = dt.Rows[0]["PageTitel"].ToString();
            tbxDetails.Text = dt.Rows[0]["PageContent"].ToString();
            tbxDetailsEditorInBangla.Text = dt.Rows[0]["PageContentBangla"].ToString();
           
        }
        else
        {
            Clear();
        }

    }
    //protected void btnDelete_Command(object sender, CommandEventArgs e)
    //{
    //    int Id = Convert.ToInt32(e.CommandArgument);
    //    new dalPageContent().Delete(Id);

    //    MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        
    //    Response.Redirect("~/Pages/Menu/EditPageContent.aspx");
    //}

    private int InsertPageContent()
    {
        PageContent pageContent = new PageContent();
        pageContent.PageName = txtPageName.Text;
        pageContent.PageTitel = txtPageTitle.Text;
        pageContent.Content = tbxDetails.Text;
        pageContent.ContentBangla = tbxDetailsEditorInBangla.Text;

        return new dalPageContent().Insert(pageContent);
    }

    private int UpdatePageContent(int pageContentId)
    {
        PageContent pageContent = new PageContent();
        pageContent.PageContentID = pageContentId;
        pageContent.PageName = txtPageName.Text;
        pageContent.PageTitel = txtPageTitle.Text;
        pageContent.Content = tbxDetails.Text;
        pageContent.ContentBangla = tbxDetailsEditorInBangla.Text;
        return new dalPageContent().Update(pageContent);
    }


    protected void ajaxFileUpload_OnUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        if (e.ContentType.Contains("jpg") || e.ContentType.Contains("gif")
            || e.ContentType.Contains("png") || e.ContentType.Contains("jpeg"))
        {
            Session["fileContentType_" + e.FileId] = e.ContentType;
            Session["fileContents_" + e.FileId] = e.GetContents();
        }
        // Set PostedUrl to preview the uploaded file.
       // e.PostedUrl = string.Format("?preview=1&fileId={0}", e.FileId);
        e.PostedUrl = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(e.GetContents()));
    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        if (e.ContentType.Contains("jpg") || e.ContentType.Contains("gif")
            || e.ContentType.Contains("png") || e.ContentType.Contains("jpeg"))
        {
            Session["fileContentType_" + e.FileId] = e.ContentType;
            Session["fileContents_" + e.FileId] = e.GetContents();
        }
        
        e.PostedUrl = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(e.GetContents()));
    }  
}