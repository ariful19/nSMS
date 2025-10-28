using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Banner : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDataList();
           // LoadBanner();
        
        }
            
    }

    protected void BindDataList()
    {
        dalGalleryImage gImage = new dalGalleryImage();
        var lstImage = gImage.GetDataTrue("Banner");
        if (lstImage.Rows.Count > 0)
        {
            rptImage.DataSource = lstImage;
            rptImage.DataBind();
        }
    }


    private void LoadBanner()
    {
        List<string> Folder = new List<string>();
        string[] FolderName = new string[10];
        string PathName = HttpContext.Current.Server.MapPath("~/Images/Banner/");

        foreach (string files in Directory.GetFiles(PathName))
        {
            FileInfo info = new FileInfo(files);
            string fileName = Path.GetFileName(info.FullName);
            Folder.Add(fileName);
        }

        rptImage.DataSource = Folder;
        rptImage.DataBind();
    }
}