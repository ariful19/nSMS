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

public partial class Pages_Menu_ChairmanContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
          //  Clear();
        }

    }

    protected void BindData()
    {
        
        DataTable dt = new Common().GetAll("tblChairmanContent");
        if (dt.Rows.Count > 0)
        {
            hdnContentId.Value = dt.Rows[0]["ChairmanContentID"].ToString();
            image.ImageUrl = "../../Images/Person/" + dt.Rows[0]["Photo"].ToString();
            txtTitle.Text = dt.Rows[0]["Titel"].ToString();
            txtTitleBangla.Text = dt.Rows[0]["TitleBangla"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtNameBangla.Text = dt.Rows[0]["NameBangla"].ToString();
            txtDesignation.Text = dt.Rows[0]["Designation"].ToString();
            txtDesignationBangla.Text = dt.Rows[0]["DesignationBangla"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtAddressBanlga.Text = dt.Rows[0]["AddressBangla"].ToString();
            tbxDetails.Text = dt.Rows[0]["Description"].ToString();
            tbxDetailsEditorInBangla.Text = dt.Rows[0]["DescriptionBangla"].ToString();
        
        }
       
    }

    public void Clear()
    {
        //this.hdfPageContentId.Value = "";
        tbxDetails.Text = "";
        tbxDetailsEditorInBangla.Text = "";
        this.txtTitle.Text = "";
        this.txtTitleBangla.Text = "";
        this.txtName.Text = "";
        this.txtNameBangla.Text = "";
        this.txtDesignation.Text = "";
        this.txtDesignationBangla.Text = "";
        this.txtAddress.Text = "";
        this.txtAddressBanlga.Text = "";
        this.image.ImageUrl = null;
        
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
        int chairmanContentId = 0;
        string img = "";

        var sdf = string.IsNullOrEmpty(hdnContentId.Value) ? "" : hdnContentId.Value.ToString();

        if (sdf != "")
        {
            chairmanContentId = Convert.ToInt32(sdf);
        }

        if (uploderPhoto.HasFile)
        {
            if (ValidImage(uploderPhoto) == true)
            {
                img = Guid.NewGuid() + "-" + uploderPhoto.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploderPhoto.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Person/" + img);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Sorry! Image is not correct extention.');", true);
        }

        if (chairmanContentId > 0)
        {
            if (UpdateChairmanContent(chairmanContentId,img) > 0)
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
            if (InsertChairmanContent(img) > 0)
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

    private int InsertChairmanContent(string img)
    {
        ChairmanContent chairmanContent = new ChairmanContent();
        chairmanContent.Titel = txtTitle.Text;
        chairmanContent.TitleBangla = txtTitleBangla.Text;
        chairmanContent.Name = txtName.Text;
        chairmanContent.NameBangla = txtNameBangla.Text;
        chairmanContent.Designation = txtDesignation.Text;
        chairmanContent.DesignationBangla = txtDesignationBangla.Text;
        chairmanContent.Address = txtAddress.Text;
        chairmanContent.AddressBangla = txtAddressBanlga.Text;
        chairmanContent.Description = tbxDetails.Text;
        chairmanContent.DescriptionBangla = tbxDetailsEditorInBangla.Text;
        chairmanContent.Photo = img;

        return new dalChairmanContent().Insert(chairmanContent);
    }

    private int UpdateChairmanContent(int chairmanContentId, string img)
    {
        ChairmanContent chairmanContent = new ChairmanContent();
        chairmanContent.ChairmanContentID = chairmanContentId;
        chairmanContent.Titel = txtTitle.Text;
        chairmanContent.TitleBangla = txtTitleBangla.Text;
        chairmanContent.Name = txtName.Text;
        chairmanContent.NameBangla = txtNameBangla.Text;
        chairmanContent.Designation = txtDesignation.Text;
        chairmanContent.DesignationBangla = txtDesignationBangla.Text;
        chairmanContent.Address = txtAddress.Text;
        chairmanContent.AddressBangla = txtAddressBanlga.Text;
        chairmanContent.Description = tbxDetails.Text;
        chairmanContent.DescriptionBangla = tbxDetailsEditorInBangla.Text;

        if (img == "")
        {
             DataTable dt = new Common().GetById("tblChairmanContent","ChairmanContentID",chairmanContentId);
             if (dt.Rows.Count > 0)
             {
                chairmanContent.Photo = dt.Rows[0]["Photo"].ToString();
             }
        }
        else
            chairmanContent.Photo = img;
        

        return new dalChairmanContent().Update(chairmanContent);
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