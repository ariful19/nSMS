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

public partial class Pages_Library_AddBook : System.Web.UI.Page
{
    dalLibraryBasic obj = new dalLibraryBasic();
    dalLibrary objLibrary = new dalLibrary();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            LoadSubCategory();
            if (Request.QueryString["Id"] != null)
            {
                ID = Convert.ToInt32(Request.QueryString["Id"]);
                LoadDefault();
                btnSave.Visible = false;
                btnEdit.Visible = true;
            }

        }
    }
    #region Load Data

    protected void LoadDefault()
    {
        DataTable dt = objLibrary.GetBookById(ID);
        if (dt.Rows.Count > 0)
        {
            ddlCategory.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
            ddlSubCategory.SelectedValue = dt.Rows[0]["SubCategoryId"].ToString();
            ddlCountry.SelectedValue = dt.Rows[0]["CountryId"].ToString();
            ddlLanguage.SelectedValue = dt.Rows[0]["LanguageId"].ToString();
            ddlPublisher.SelectedValue = dt.Rows[0]["PublisherId"].ToString();
            ddlEdtion.SelectedValue = dt.Rows[0]["EditionId"].ToString();
            tbxISBN.Text = dt.Rows[0]["ISBN"].ToString();
            tbxVolume.Text = dt.Rows[0]["VolumeNo"].ToString();
            tbxSelfNo.Text = dt.Rows[0]["SelfNo"].ToString();
            tbxCellNo.Text = dt.Rows[0]["CellNo"].ToString();
            tbxStock.Text = dt.Rows[0]["Stock"].ToString();
            tbxTitle.Text = dt.Rows[0]["Title"].ToString();
            tbxAuthor.Text = dt.Rows[0]["Author"].ToString();
            tbxSubTitle.Text = dt.Rows[0]["SubTitle"].ToString();
            tbxKeyWord.Text = dt.Rows[0]["KeyWords"].ToString();
            tbxDescription.Text = dt.Rows[0]["Description"].ToString();
            chkAvailable.Checked = dt.Rows[0]["Status"].ToString() == "True" ? true : false;
            if (dt.Rows[0]["CoverPhoto"].ToString() == "")
            {
                imgCover.ImageUrl = "~/Images/Common/no-photo.jpg";
            }
            else
            {
                imgCover.ImageUrl = "~/Images/Book/" + dt.Rows[0]["CoverPhoto"].ToString();
                lblImage.Text = dt.Rows[0]["CoverPhoto"].ToString();
                imgCover.Visible = true;
                lblBrowse.Text = "Change Photo";
            }

        }
    }
    protected void Load()
    {
        ddlCategory.DataSource = new Common().GetAll("lb_Category");
        ddlCategory.DataBind();

        ddlLanguage.DataSource = new Common().GetAll("lb_Language");
        ddlLanguage.DataBind();

        ddlPublisher.DataSource = new Common().GetAll("lb_Publisher");
        ddlPublisher.DataBind();

        ddlCountry.DataSource = new Common().GetAll("lb_Country");
        ddlCountry.DataBind();

        ddlEdtion.DataSource = new Common().GetAll("lb_Edition");
        ddlEdtion.DataBind();
    }
    protected void LoadSubCategory()
    {
        DataTable dt = obj.GetByCategoryId(Convert.ToInt32(ddlCategory.SelectedValue));
        ddlSubCategory.DataSource = dt;
        ddlSubCategory.DataBind();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubCategory();
    }
    #endregion
    protected bool ValidImage(FileUpload file)
    {
        string extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == "jpeg" || extension == ".jpg" || extension == ".png")
        {
            if (file.PostedFile.ContentLength < 6218595)
            {
                return true;
            }
        }
        return false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int stock = 0;
        string coverPhoto = "";
        if (chkAvailable.Checked)
        {
            if (tbxStock.Text != "")
                stock = Convert.ToInt32(tbxStock.Text);
            else
            {
                MessageController.Show("Stock can not be empty", MessageType.Warning, Page);
                return;
            }
        }
        if (fuCoverPhoto.HasFile)
        {
            if (ValidImage(fuCoverPhoto))
            {
                coverPhoto = Guid.NewGuid() + "-" + fuCoverPhoto.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(fuCoverPhoto.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(150, 210));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Book/" + coverPhoto);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
        }
        objLibrary.InsertBook(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlCountry.SelectedValue),
            Convert.ToInt32(ddlPublisher.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlEdtion.SelectedValue), tbxTitle.Text, tbxAuthor.Text,
            tbxISBN.Text, tbxVolume.Text, tbxSelfNo.Text, tbxCellNo.Text, Convert.ToBoolean(chkAvailable.Checked), stock, tbxSubTitle.Text, tbxKeyWord.Text, tbxDescription.Text, coverPhoto, DateTime.Now, Page.User.Identity.Name, DateTime.Now);
        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int stock = 0;
        string coverPhoto = "";
        if (chkAvailable.Checked)
        {
            if (tbxStock.Text != "")
                stock = Convert.ToInt32(tbxStock.Text);
            else
            {
                MessageController.Show("Stock can not be empty", MessageType.Warning, Page);
                return;
            }
        }
        if (fuCoverPhoto.HasFile)
        {
            if (ValidImage(fuCoverPhoto))
            {
                coverPhoto = Guid.NewGuid() + "-" + fuCoverPhoto.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(fuCoverPhoto.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(150, 210));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Book/" + coverPhoto);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
            if (lblImage.Text != "")
                File.Delete(Server.MapPath("~/Images/Book/" + lblImage.Text));
        }
        else
        {
            if (lblImage.Text != "")
                coverPhoto = lblImage.Text;
        }
        objLibrary.UpdateBook(ID,Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlCountry.SelectedValue),
            Convert.ToInt32(ddlPublisher.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlEdtion.SelectedValue), tbxTitle.Text, tbxAuthor.Text,
            tbxISBN.Text, tbxVolume.Text, tbxSelfNo.Text, tbxCellNo.Text, Convert.ToBoolean(chkAvailable.Checked), stock, tbxSubTitle.Text, tbxKeyWord.Text, tbxDescription.Text, coverPhoto, DateTime.Now, Page.User.Identity.Name, DateTime.Now);
        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxVolume.Text = "";
        tbxCellNo.Text = "";
        tbxDescription.Text = "";
        tbxISBN.Text = "";
        tbxKeyWord.Text = "";
        tbxStock.Text = "";
        tbxSubTitle.Text = "";
        tbxTitle.Text = "";
        tbxSelfNo.Text = "";
        tbxAuthor.Text = "";
    }
}