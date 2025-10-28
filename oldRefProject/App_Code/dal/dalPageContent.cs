using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalPageContent
/// </summary>
public class dalPageContent
{
    DatabaseManager dm = new DatabaseManager();
    public int Insert(PageContent pageContent)
    {
        dm.AddParameteres("@PageName", pageContent.PageName);//@PageTitel
        dm.AddParameteres("@PageTitel", pageContent.PageTitel);
        dm.AddParameteres("@PageContent", pageContent.Content);
        dm.AddParameteres("@PageContentBangla", pageContent.ContentBangla);
       // dm.AddParameteres("@MenuID", pageContent.MenuID);
        DataTable dt = dm.ExecuteQuery("USP_InsertPageContent");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(PageContent pageContent)
    {
        dm.AddParameteres("@PageContentID", pageContent.PageContentID);
        dm.AddParameteres("@PageName", pageContent.PageName);
        dm.AddParameteres("@PageTitel", pageContent.PageTitel);
        dm.AddParameteres("@PageContent", pageContent.Content);
        dm.AddParameteres("@PageContentBangla", pageContent.ContentBangla);
       // dm.AddParameteres("@MenuID", pageContent.MenuID);
        DataTable dt = dm.ExecuteQuery("USP_UpdatePageContent");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public DataTable GetByName(string pageName)
    {
        string sql = "Select * from PageContent Where PageName='" + pageName + "'";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetById(int id)
    {
        string sql = "Select * from PageContent Where PageContentID='" + id + "'";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetData()
    {
        string sql = "Select * from PageContent";
        return dm.NewExecuteQuery(sql);
    }
    public DataTable GetDataBangla(int pageId)
    {
        dm.AddParameteres("@PageContentID", pageId);
        return dm.ExecuteQuery("USP_PageContentBangla");
    }
    public DataTable GetDataEnglish(int pageId)
    {
        dm.AddParameteres("@PageContentID", pageId);
        return dm.ExecuteQuery("USP_PageContentEnglish");
    }

  

}