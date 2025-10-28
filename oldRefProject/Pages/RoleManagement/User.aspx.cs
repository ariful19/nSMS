using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Pages_RoleManagement_User : BasePage
{
    dalUser objUser = new dalUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRole();
            //LoadUser();
            updatedUser.Value = string.Empty;
            btnAddEditUser.Text = "Create User";          
        }
    }

    private void BindRole()
    {
        // DropDownList lbxAssignRole = (DropDownList)CreateUserWizardStep_1.ContentTemplateContainer.FindControl("lbxAssignRole");
        lbxAssignRole.DataSource = new dalRole().GetAllRole();
        lbxAssignRole.DataBind();

        ddlRoles.DataSource= new dalRole().GetAllRole();
        ddlRoles.DataBind();
    }

    private void LoadControls(string userName)
    {
        MembershipUser mu = Membership.GetUser(userName);
        UserName.Text = mu.UserName;
        Email.Text = mu.Email;
        lbxAssignRole.SelectedValue = Roles.GetRolesForUser(userName)[0];
    }

    protected void LoadUser()
    {
 var role = ddlRoles.SelectedItem.Value;
        DataTable dt = objUser.GetUsersByRole1(role);
        if (dt != null)
        {
            var na = dt.Rows.OfType<DataRow>().FirstOrDefault(o => o["UserName"].ToString() == "nano1");
            if (na != null)
            {
                dt.Rows.Remove(na);
            }
        }
        rptUser.DataSource = dt;
        rptUser.DataBind();
    }

    protected void btnAddEditUser_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(updatedUser.Value.ToString())) // Create
        {
            if (Membership.FindUsersByName(UserName.Text).Count != 0)
            {
                MessageController.Show("User Name Already Exists", MessageType.Error, Page);
                return;
            }
            // Create user
            MembershipCreateStatus mcsStatus;
            Membership.CreateUser(UserName.Text, Password.Text, Email.Text, "color", "black", true, out mcsStatus);

            // Insert role
            Roles.AddUserToRole(UserName.Text, lbxAssignRole.SelectedValue);
            new dalPerson().UserInsert(UserName.Text, Email.Text);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else // Update
        {
            MembershipUser mu = Membership.GetUser(UserName.Text);

            mu.Email = Email.Text;
            if (Password.Text.Length > 2)
            {
                string pass = mu.ResetPassword();
                mu.ChangePassword(pass, Password.Text);
            }
            Membership.UpdateUser(mu);

            Roles.RemoveUserFromRole(UserName.Text, Roles.GetRolesForUser(UserName.Text)[0]);
            Roles.AddUserToRole(UserName.Text, lbxAssignRole.Text);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            LoadUser();
            Response.Redirect("User.aspx");
        }
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        lblMessage.Text = "";
        UserName.Enabled = false;
        //Password.Enabled = false;
        //ConfirmPassword.Enabled = false;
        PasswordRequired.ValidationGroup = "";
       // ConfirmPasswordRequired.ControlToValidate = "";
        ConfirmPasswordRequired.ValidationGroup = "";
        LoadControls(e.CommandArgument.ToString());
        btnAddEditUser.Text = "Edit User";
        updatedUser.Value = e.CommandArgument.ToString();

    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        LoadUser();
    }
}