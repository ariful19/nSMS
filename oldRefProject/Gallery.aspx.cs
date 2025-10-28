using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gallery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGalleryImages();
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
            pnlGallery.Controls.Add(image);
        }


    }
}