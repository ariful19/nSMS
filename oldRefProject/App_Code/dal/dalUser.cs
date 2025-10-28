using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;

/// <summary>
/// Summary description for dalUser
/// </summary>
public class dalUser
{
    DatabaseManager dm = new DatabaseManager();   
	public dalUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetAll(string role)
    {
        dm.AddParameteres("@Role",role);
        return dm.ExecuteQuery("USP_User_GetAll");
    }
    public DataTable GetUserByRole(string role)
    {
        dm.AddParameteres("@Role",role);
        return dm.ExecuteQuery("USP_User_GetUserByRole");
    } 
    public DataTable GetUsersByRole1(string role)
    {        
        return dm.ExecuteQueryDt("SELECT aspnet_Users.UserId, \r\n\t\t\taspnet_Users.UserName, \r\n\t\t\taspnet_Membership.*,\r\n\t\t\taspnet_Roles.RoleName \t\t\t\r\n\t\tFROM aspnet_Users \r\n\t\t\tINNER JOIN aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId \r\n\t\t\tINNER JOIN aspnet_UsersInRoles ON aspnet_Users.UserId = aspnet_UsersInRoles.UserId \r\n\t\t\tINNER JOIN aspnet_Roles ON aspnet_UsersInRoles.RoleId = aspnet_Roles.RoleId \r\n\t\twhere aspnet_Roles.RoleName = '"+role+"'");
    } 
    public DataTable GetAllAdminUser()
    {
        return dm.ExecuteQuery("USP_User_GetAllAdminUser");
    }
    public DataTable GetUsedPass(string  userName)
    {
        dm.AddParameteres("@UserName",userName);
        return dm.ExecuteQuery("USP_GetUserPasswordByUserName");
    }


}