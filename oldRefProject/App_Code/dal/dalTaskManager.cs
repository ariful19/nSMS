using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalTaskManager
/// </summary>
public class dalTaskManager
{
    DatabaseManager dm = new DatabaseManager();
    public dalTaskManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(string textEng, string textBan, string url, int parentId, string createdBy, DateTime createdDate, string icon)
    {
        dm.AddParameteres("@TextEng", textEng);
        dm.AddParameteres("@TextBan", textBan);
        dm.AddParameteres("@URL", url);
        dm.AddParameteres("@ParentId", parentId);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        dm.AddParameteres("@Icon", icon);
        DataTable dt = dm.ExecuteQuery("USP_Task_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id, string textEng, string textBan, string url, int parentId, string updateddBy, DateTime updatedDate, string icon)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@TextEng", textEng);
        dm.AddParameteres("@TextBan", textBan);
        dm.AddParameteres("@URL", url);
        dm.AddParameteres("@ParentId", parentId);
        dm.AddParameteres("@UpdatedBy", updateddBy);
        dm.AddParameteres("@UpdatedDate", updatedDate);
        dm.AddParameteres("@Icon", icon);
        return dm.ExecuteNonQuery("USP_Task_Update");
    }

    public DataTable GetParanet(int role)
    {
        dm.AddParameteres("@Role", role);
        DataTable dt = dm.ExecuteQuery("USP_Task_GetParent");
        return dt;
    }

    public DataTable GetAllParanet()
    {
        string sql = "select * from sa_TaskManager  where parentId=0";
        DataTable dt = dm.NewExecuteQuery(sql);
        return dt;
    }

    public DataTable GetChild(int ParentId, int roleId)
    {
        dm.AddParameteres("@ParentId", ParentId);
        dm.AddParameteres("@RoleId", roleId);
        return dm.ExecuteQuery("USP_Task_GetChild");
    }

    public DataTable GetAllChildByParentID(int ParentId)
    {
        string sql = "select * from sa_TaskManager 	where sa_TaskManager.ParentId=" + ParentId;
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetAllTask()
    {
        return dm.ExecuteQuery("USP_Task_GetAll");
    }

    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Task_GetById");
    }

    public int TaskToRoleInsert(DataTable dt)
    {
        DataSet ds = new DataSet("ds");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@Xml", xml);
        return dm.ExecuteNonQuery("USP_TaskToRole_Insert");
    }
    public int DeleteTaskToRole(int roleId)
    {
        dm.AddParameteres("RoleId", roleId);
        return dm.ExecuteNonQuery("USP_TaskToRole_Delete");
    }
}