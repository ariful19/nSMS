using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalHomeMenu
/// </summary>
public class dalHomeMenu
{
    DatabaseManager dm = new DatabaseManager();


    public int InsertHomeMenu(HomeMenu homeMenu)
    {
        dm.AddParameteres("@TextEng", homeMenu.TextEng);
        dm.AddParameteres("@TextBan", homeMenu.TextBan);
        dm.AddParameteres("@URL", homeMenu.URL);
        dm.AddParameteres("@ParentId", homeMenu.ParentId);
        dm.AddParameteres("@CreatedBy", homeMenu.CreatedBy);
        dm.AddParameteres("@CreatedDate", homeMenu.CreatedDate);
        dm.AddParameteres("@Order", homeMenu.Order);
        dm.AddParameteres("@PageContentID", homeMenu.PageContentID);
        DataTable dt = dm.ExecuteQuery("[USP_HomeMenuInsert]");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int InsertHomeMenuParent(HomeMenu homeMenu)
    {
        dm.AddParameteres("@TextEng", homeMenu.TextEng);
        dm.AddParameteres("@TextBan", homeMenu.TextBan);
        dm.AddParameteres("@URL", homeMenu.URL);
        dm.AddParameteres("@ParentId", homeMenu.ParentId);
        dm.AddParameteres("@CreatedBy", homeMenu.CreatedBy);
        dm.AddParameteres("@CreatedDate", homeMenu.CreatedDate);
        dm.AddParameteres("@Order", homeMenu.Order);
        DataTable dt = dm.ExecuteQuery("[USP_HomeMenuInsertParent]");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int UpdateHomeMenu(HomeMenu homeMenu)
    {
        dm.AddParameteres("@MenuID", homeMenu.MenuID);
        dm.AddParameteres("@TextEng", homeMenu.TextEng);
        dm.AddParameteres("@TextBan", homeMenu.TextBan);
        dm.AddParameteres("@URL", homeMenu.URL);
        dm.AddParameteres("@ParentId", homeMenu.ParentId);
        dm.AddParameteres("@UpdatedBy", homeMenu.CreatedBy);
        dm.AddParameteres("@UpdatedDate", homeMenu.CreatedDate);
        dm.AddParameteres("@Order", homeMenu.Order);
        dm.AddParameteres("@PageContentID", homeMenu.PageContentID);
        dm.ExecuteQuery("USP_HomeMenuUpdate");
        return homeMenu.MenuID;
    }

    public int UpdateHomeMenuPublish(HomeMenu homeMenu)
    {
        dm.AddParameteres("@MenuID", homeMenu.MenuID);
        dm.AddParameteres("@IsPublish", homeMenu.IsPublish);
        dm.AddParameteres("@PublishStartDate", homeMenu.PublishStartDate);
        dm.AddParameteres("@PublishEndDate", homeMenu.PublishEndDate);

        dm.ExecuteQuery("USP_HomeMenuUpdatePublish");
        return homeMenu.MenuID;
    }

    public DataTable GetAllHomeMenu()
    {
        return dm.NewExecuteQuery(@"Select dt.*, dt1.TextEng as Parent from dbo.HomeMenu dt join HomeMenu dt1 on dt.MenuID=dt1.MenuID order by MenuID desc");
    }

    public DataTable GetPublishedHomeMenu()
    {
        return dm.NewExecuteQuery(@"Select dt.*, dt1.TextEng as Parent from dbo.HomeMenu dt join HomeMenu dt1 on dt.MenuID=dt1.MenuID where dt.IsPublish = 1 order by MenuID desc");
    }

    public DataTable GetHomeMenuById(int id)
    {
        return dm.NewExecuteQuery("Select * from dbo.HomeMenu where MenuID=" + id);
    }


    public DataTable GetAllParanet()
    {
       // return dm.NewExecuteQuery("Select * from dbo.HomeMenu where ParentID=0");

        return dm.NewExecuteQuery(" Select * from dbo.HomeMenu where ParentID=0 and IsPublish is null  union all select *  from HomeMenu where IsPublish=1 and ParentID=0  and PublishStartDate <= GETDATE()  and PublishEndDate >= GETDATE()");
    }

    public DataTable GetParanet()
    {
        return dm.ExecuteQuery("USP_PageContent");
    }


    public void Delete(int ID)
    {
        dm.NewExecuteQuery("Delete HomeMenu where MenuID=" + ID);
    }

    public DataTable GetAllChildByParentID(int parentID)
    {
        return dm.NewExecuteQuery("Select * from dbo.HomeMenu where ParentID=" + parentID);
    }

    //USP_PageContentChild

    public DataTable GetChildByParentID(int parentID)
    {
        dm.AddParameteres("@ParentId", parentID);
        return dm.ExecuteQuery("USP_PageContentChild");
    }
}