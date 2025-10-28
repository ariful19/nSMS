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

public partial class Pages_Menu_AboutUsContent : System.Web.UI.Page
{
    dalChairmanContent obj = new dalChairmanContent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  BindData();
            Clear();
        }

    }

    protected void BindDataAboutUs()
    {

        DataTable dt = obj.GetData("1");
        if (dt.Rows.Count > 0)
        {
            hdnContentId.Value = dt.Rows[0]["AboutUsID"].ToString();

            tbxDetails.Text = dt.Rows[0]["AboutUsEnglish"].ToString();
            tbxDetailsEditorInBangla.Text = dt.Rows[0]["AboutUsbangla"].ToString();
        
        }
       
    }

    protected void BindDataSpecialField()
    {

        DataTable dt = obj.GetData("2");
        if (dt.Rows.Count > 0)
        {
            hdnContentId.Value = dt.Rows[0]["AboutUsID"].ToString();

            tbxDetails.Text = dt.Rows[0]["AboutUsEnglish"].ToString();
            tbxDetailsEditorInBangla.Text = dt.Rows[0]["AboutUsbangla"].ToString();

        }

    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "1")
        {
            BindDataAboutUs();
        }
        else
            BindDataSpecialField();
    }

    public void Clear()
    {
        this.hdnContentId.Value = "";
        tbxDetails.Text = "";
        tbxDetailsEditorInBangla.Text = "";
        this.ddlType.SelectedIndex = -1;
        
    }

    protected bool ValidImage(FileUpload file)
    {
        bool flag = false;
        string extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == "jpeg" || extension == ".jpg" || extension == ".png")
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
        int contentId = 0;

        int id;

        var sdf = string.IsNullOrEmpty(hdnContentId.Value) ? "" : hdnContentId.Value.ToString();

        if (sdf != "")
        {
            contentId = Convert.ToInt32(sdf);
        }


        if (contentId > 0)
        {
           id = obj.UpdateAboutUs(contentId, tbxDetails.Text, tbxDetailsEditorInBangla.Text,"",ddlType.SelectedItem.Value);

           if (id > 0)
            {
                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            }
            else
            {
                MessageController.Show("Information save failed. Please try anothor.", MessageType.Error, Page);
            }

            Clear();
          //  BindData();
        }
        else
        {
            id = obj.InsertAboutUs(tbxDetails.Text, tbxDetailsEditorInBangla.Text, "", ddlType.SelectedItem.Value);
            if (id > 0)
            {
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            }
            else
            {
                MessageController.Show("Information save failed. Please try anothor.", MessageType.Error, Page);
            }

            Clear();
           // BindData();

        }

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