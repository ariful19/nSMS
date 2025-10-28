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

public partial class Pages_Menu_GalleryManager : System.Web.UI.Page
{

    dalGalleryImage obj = new dalGalleryImage();

    #region Methods

    protected void BindDataList()
    {
        dalGalleryImage gImage = new dalGalleryImage();
        var lstImage = gImage.GetData("gallery");


        if (lstImage.Rows.Count > 0)
        {
            dlImages.DataSource = lstImage;
            dlImages.DataBind();
        }
    }

    private void Clear()
    {
        this.txtDesc.Text = "";
        this.txtTitle.Text = "";
        this.chkIsActive.Checked = false;
    }

    protected bool ValidImage(FileUpload file)
    {
        bool flag = false;
        string exe = Path.GetExtension(file.FileName).ToLower();
        if (exe == "jpeg" || exe == ".jpg" || exe == ".png")
        {
            if (file.PostedFile.ContentLength < 6218595)
            {
                flag = true;
            }
        }
        
        return flag;
    }

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        if (!IsPostBack)
        {
            BindDataList();
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string imgPer = "";

        if (fileuploadimages.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please Select Image');", true);

            return;
        }

        //string filename = Path.GetFileName(fileuploadimages.PostedFile.FileName);


        if (ValidImage(fileuploadimages) == true)
        {
            imgPer = Guid.NewGuid() + "-" + fileuploadimages.FileName;
            System.Drawing.Image image = System.Drawing.Image.FromStream(fileuploadimages.FileContent);
            System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
            string MediumImagePath = Server.MapPath("~/Images/gallery/" + imgPer);
            image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);


            GalleryImage gallaryImage = new GalleryImage();
            gallaryImage.ImageData = "";
            gallaryImage.ImageSize = "";
            gallaryImage.ImageUsedDriv = "gallery";
            gallaryImage.ImageDescription = txtDesc.Text;
            gallaryImage.ImageTitle = txtTitle.Text;
            gallaryImage.ImageIsActive = this.chkIsActive.Checked;
            gallaryImage.ImagePath = "../Images/gallery/" + imgPer;
            gallaryImage.ImageName = imgPer;
            gallaryImage.Remarks = "";
            gallaryImage.CreateBy = User.Identity.Name;
            new dalGalleryImage().Insert(gallaryImage);
        }
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Sorry! Image is invalid :)');", true);

        BindDataList();
        Clear();
    }




    protected void ibSave_Click(object sender, EventArgs e)
    {

        var btn = (ImageButton)sender;
        var id = long.Parse(btn.CommandArgument);

        if (id > 0)
        {
            GalleryImage gImage = new GalleryImage();

            if (this.chkIsActive.Checked == true)
            {
                gImage.ImageIsActive = true;
            }
            else
                gImage.ImageIsActive = false;

            obj.Update(id, gImage.ImageIsActive);

            this.txtDesc.Text = string.Empty;
            this.txtTitle.Text = string.Empty;
            BindDataList();
        }
        else
        {

        }


    }

    protected void ibDelete_Click(object sender, EventArgs e)
    {
        var btn = (ImageButton)sender;
        var id = long.Parse(btn.CommandArgument);
        //GalleryImage gImage = new GalleryImage();
        var ImageInformation = obj.GetById("tblGalleryImage", "GalleryImageID", id);
        if (ImageInformation == null)
        { return; }
       // string path = Server.MapPath(gImage.ImagePath);
        string path = Server.MapPath(ImageInformation.Rows[0]["ImagePath"].ToString());
         obj.Delete(id);

        if (id > 0)
        {

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            this.txtDesc.Text = string.Empty;
            this.txtTitle.Text = string.Empty;
            BindDataList();
        }
        else
        {
          
        }
    }


  
    #endregion


}