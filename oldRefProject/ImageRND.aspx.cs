using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImageRND : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
       // System.Drawing.Image image = System.Drawing.Image.FromStream(attachmentUpload.PostedFile.InputStream);

        string filename;
        if (attachmentUpload.HasFile)
        {
            filename = attachmentUpload.PostedFile.FileName;
          //  filename = "~/Logos/" + "objLogo.UserID" + ".jpg";
            string targetPath = Server.MapPath(filename);
            Stream strm = attachmentUpload.PostedFile.InputStream;
            var targetFile = targetPath;
            //Based on scalefactor image size will vary
            GenerateThumbnails(0.5, strm, targetFile);

        }
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
       
    }


    private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
    {
        using (var img = System.Drawing.Image.FromStream(sourcePath))
        {
            // can given width of image as we want
            var newWidth = 700;
            // can given height of image as we want
            var newHeight = (newWidth * img.Height) / img.Width;
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(img, imageRectangle);
            thumbnailImg.Save(targetPath, img.RawFormat);
        }
    }
}