using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_GalleryForSiteMaster : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDataList();
            // LoadGalleryImages();
        }
    }

    protected void BindDataList()
    {
        dalGalleryImage gImage = new dalGalleryImage();
        var lstImage = gImage.GetDataLast("gallery");

        if (lstImage.Rows.Count > 0)
        {
            dlImages.DataSource = lstImage;
            dlImages.DataBind();
        }
    }


    private void LoadGalleryImages()
    {
        string[] filePaths = Directory.GetFiles(Server.MapPath(@"Images\Gallery\"));
        foreach (string fileName in filePaths)
        {
            string imageName = fileName.Substring(fileName.LastIndexOf("\\"));

            Image image = new Image();
            image.Width = Unit.Pixel(100);
            image.ImageUrl = "Images/gallery/" + imageName;
            dlImages.Controls.Add(image);
        }


    }
}