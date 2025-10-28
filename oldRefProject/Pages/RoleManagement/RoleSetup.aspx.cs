using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nano.DataAccessLayer;
using System.Data;

public partial class Pages_RoleManagement_RoleSetup : BasePage
{
    dalRole objRole = new dalRole();
    public static string RoleId;
    public static string RoleName;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            try
            {
                Roles.CreateRole(tbxRole.Text);
                DataTable dt = objRole.GetByRoleName(tbxRole.Text);
                if (dt.Rows.Count > 0)
                {
                    objRole.Insert(dt.Rows[0]["RoleId"].ToString(), tbxRole.Text, Page.User.Identity.Name, DateTime.Now);
                }
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
        }
        else
        {
            int Id = objRole.UpdateRoles(RoleId, tbxRole.Text);
            if (Id > -1)
            {
                objRole.Update(RoleId, tbxRole.Text, Page.User.Identity.Name, DateTime.Now);
                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
                BindData();
            }
            else
            {
                MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
                return;
            }
        }
        tbxRole.Text = "";
    }

    protected void BindData()
    {
        DataTable dt = objRole.GetAdminRole();
        rptRole.DataSource = dt;
        rptRole.DataBind();
    }

    protected void ClearAll()
    {
        tbxRole.Text = "";
        btnSave.Text = "Save";
        ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        RoleId = e.CommandArgument.ToString();
        DataTable dt = objRole.GetById(RoleId);
        if (dt.Rows.Count > 0)
        {
            tbxRole.Text = dt.Rows[0]["RoleName"].ToString();
        }
        btnSave.Text = "Edit";
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            RoleName=e.CommandArgument.ToString();
            Roles.DeleteRole(RoleName);
            objRole.Delete(RoleName);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        }
        catch(Exception ex)
        {
            MessageController.Show(ex.Message,MessageType.Error,Page);
        }
    }

}