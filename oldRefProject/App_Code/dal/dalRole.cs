using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalRole
/// </summary>
public class dalRole
{
    DatabaseManager dm = new DatabaseManager();
	public dalRole()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string roleId,string roleName,string createdBy, DateTime createDate)
    {
        dm.AddParameteres("@RoleId", roleId);
        dm.AddParameteres("@RoleName", roleName);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreateDate", createDate);
        DataTable dt = dm.ExecuteQuery("USP_Role_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(string roleId, string roleName, string updatedBy, DateTime updateDate)
    {
        dm.AddParameteres("@RoleId", roleId);
        dm.AddParameteres("@RoleName", roleName);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        dm.AddParameteres("@UpdateDate", updateDate);
        return dm.ExecuteNonQuery("USP_Role_Update");
        
    }
    public int UpdateRoles(string roleId,string roleName)
    {
        dm.AddParameteres("@RoleId", roleId);
        dm.AddParameteres("@RoleName", roleName);
        DataTable dt = dm.ExecuteQuery("aspnet_Roles_Update");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetById(string RoleId)
    {
        dm.AddParameteres("@Id", RoleId);
        return dm.ExecuteQuery("USP_Role_GetById");
    }

    public DataTable GetByRoleName(string RoleName)
    {
        dm.AddParameteres("@RoleName", RoleName);
        return dm.ExecuteQuery("USP_Role_GetByRoleName");
    }
    public DataTable GetAllRole()
    {
        return dm.ExecuteQuery("USP_Get_AllRole");
    }

    public DataTable GetAdminRole()
    {
        return dm.ExecuteQuery("USP_Roll_GetAdminRole");
    }
    public int Delete(string RoleName)
    {
        dm.AddParameteres("@Name", RoleName);
        return dm.ExecuteNonQuery("USP_Role_DeleteByRoleName");
    }

    public DataTable GetIdByUserName(string userName)
    {
        dm.AddParameteres("@UserName", userName);
        return dm.ExecuteQuery("USP_Role_IdByUserName");
    }

    public DataTable RoleNameById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Role_RoleNameById");
    }
}