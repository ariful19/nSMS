using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalPageContent
/// </summary>
public class dalGalleryImage
{
    DatabaseManager dm = new DatabaseManager();
    public int Insert(GalleryImage gallaryImage)
    {
        dm.AddParameteres("@ImageData", gallaryImage.ImageData);
        dm.AddParameteres("@ImageDescription", gallaryImage.ImageDescription);
        dm.AddParameteres("@ImageName", gallaryImage.ImageName);
        dm.AddParameteres("@ImagePath", gallaryImage.ImagePath);
        dm.AddParameteres("@ImageSize", gallaryImage.ImageSize);
        dm.AddParameteres("@ImageTitle", gallaryImage.ImageTitle);
        dm.AddParameteres("@ImageUsedDriv", gallaryImage.ImageUsedDriv);
        dm.AddParameteres("@ImageIsActive", gallaryImage.ImageIsActive);
        dm.AddParameteres("@CreateBy", gallaryImage.CreateBy);
        dm.AddParameteres("@Remarks", gallaryImage.Remarks);

        DataTable dt = dm.ExecuteQuery("USP_InsertGalleryImage");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetDataTrue(string driv)
    {
        string sql = "Select * from tblGalleryImage Where ImageUsedDriv= " + "'" + driv + "' AND ImageIsActive = 'True' ";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetDataLast(string driv)
    {
        string sql = "Select top(6) * from tblGalleryImage Where ImageUsedDriv= 'gallery' AND ImageIsActive = 'True' ORDER BY GalleryImageID DESC";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetData(string driv)
    {
        string sql = "Select * from tblGalleryImage Where ImageUsedDriv= " + "'" + driv + "'";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetById(string table, string column, long condition)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column", column);
        dm.AddParameteres("@Condition", condition);
        return dm.ExecuteQuery("USP_GetById");
    }

    public DataTable GetData()
    {
        string sql = "Select * from tblGalleryImage Where ImageIsActive='True' ";
        return dm.NewExecuteQuery(sql);
    }
    public void Delete(long ID)
    {
        dm.NewExecuteQuery("Delete tblGalleryImage where GalleryImageID=" + ID);
    }

    public DataTable Update(long ID, bool isActive)
    {
        dm.AddParameteres("@GalleryImageID", ID);
        dm.AddParameteres("@ImageIsActive", isActive);
        return dm.ExecuteQuery("USP_UpdateGalleryImage");

      //  DataTable dt = dm.ExecuteQuery("USP_InsertGalleryImage");
        
       // dm.NewExecuteQuery("Update tblGalleryImage Set ImageIsActive = 'false' where GalleryImageID=" + ID);
    }

   
}