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

public partial class Pages_Admin_News : BasePage
{
    dalNews obj = new dalNews();
    protected static int ID;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbxDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //btnSave.CssClass = Common.SessionInfo.Button;
            //btnEdit.CssClass = Common.SessionInfo.Button;
            //btnReset.CssClass = Common.SessionInfo.Button;
            BindData();
        }

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
        string imageNews = "";


        #region ImageUpload

        if (uploderPhoto.HasFile)
        {
            if (ValidImage(uploderPhoto) == true)
            {
                imageNews = Guid.NewGuid() + "-" + uploderPhoto.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploderPhoto.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Common/" + imageNews);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
            else
            {
                MessageController.Show("Select Schoool image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
                return;
            }
        }
        #endregion ImageUpload
        News news = new News();
        news.TitleInBangla = tbxNameBangla.Text;
        news.Title = tbxName.Text;
        news.Date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        news.ShortDescriptionInBangla = tbxShortDescriptionInBangla.Text;
        news.ShortDescription = tbxShortDescription.Text;
        news.DetailsInBangla = tbxDetailsBanglaEditor.Text;
        news.Details = tbxDetails.Text;
        news.Photo = imageNews;
        news.CreatedBy = Page.User.Identity.Name;
        news.CreatedDate = DateTime.Now;

        ID = obj.Insert(news);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This news already exists in selected date. Please try anothor title.", MessageType.Error, Page);

        BindData();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string imageNews = "";

        #region ImageUpload

        if (uploderPhoto.HasFile)
        {
            if (ValidImage(uploderPhoto) == true)
            {
                imageNews = Guid.NewGuid() + "-" + uploderPhoto.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploderPhoto.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Common/" + imageNews);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
            else
            {
                MessageController.Show("Select Schoool image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
                return;
            }
        }
        #endregion ImageUpload

        News news = new News();
        news.TitleInBangla = tbxNameBangla.Text;
        news.Title = tbxName.Text;
        news.Date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        news.ShortDescriptionInBangla = tbxShortDescriptionInBangla.Text;
        news.ShortDescription = tbxShortDescription.Text;
        news.DetailsInBangla = tbxDetailsBanglaEditor.Text;
        news.Details = tbxDetails.Text;
        news.Photo = imageNews;
        news.UpdatedBy = Page.User.Identity.Name;
        news.UpdatedDate = DateTime.Now;

        obj.Update(ID,news);
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
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("bs_News");
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {

            string fileName = dt.Rows[0]["Photo"].ToString();
            Session["UpdatedFileName"] = fileName;

            //image.ImageUrl = "../../Images/Person/" + dt.Rows[0]["Photo"].ToString();

            tbxNameBangla.Text = dt.Rows[0]["TitleInBangla"].ToString();
            tbxName.Text = dt.Rows[0]["Title"].ToString();
            tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString("dd/MM/yyyy");
            tbxShortDescriptionInBangla.Text = dt.Rows[0]["ShortDescriptionInBangla"].ToString();
            tbxShortDescription.Text = dt.Rows[0]["ShortDescription"].ToString();
            tbxDetailsBanglaEditor.Text = dt.Rows[0]["DetailsInBangla"].ToString();
            tbxDetails.Text = dt.Rows[0]["Details"].ToString();

        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("bs_News", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}