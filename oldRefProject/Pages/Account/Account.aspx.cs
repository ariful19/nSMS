using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_Account : System.Web.UI.Page
{
    dalAccount obj = new dalAccount();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Load();
        BindData();
    }

    #region Load Data
    protected void Load()
    {
        ddlSubAccountHead.DataSource = new Common().GetAll("tbl_Sub_AccounHead");
        ddlSubAccountHead.DataBind();
    }
    #endregion

    protected void ClearAll()
    {
        tbxAccountCode.Text = string.Empty;
        tbxAccountName.Text = string.Empty;
        tbxDescription.Text = string.Empty;
        tbxStatus.Text = string.Empty;
        tbxBalance.Text = string.Empty;
        tbxOpenDate.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Account aAccount = new Account();
        aAccount.SubHeadId = Convert.ToInt32(ddlSubAccountHead.SelectedItem.Text.Trim());
        aAccount.AccountCodeId = tbxAccountCode.Text.Trim();
        aAccount.AId = 102;
        aAccount.AccountName = tbxAccountName.Text.Trim();
        aAccount.Description = tbxDescription.Text.Trim();
        aAccount.Status = tbxStatus.Text.Trim();
        aAccount.Balance = Convert.ToSingle(tbxBalance.Text.Trim());
        aAccount.OpenDate = Convert.ToDateTime(tbxOpenDate.Text.Trim());
        aAccount.CreatedBy = Page.User.Identity.Name;
        aAccount.CreatedDate = DateTime.Now;

        ID = obj.InsertAccount(aAccount);
        if (ID != -1)
        {
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Account Code already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }
    Regex regex = new Regex(@"[1-9]");
    private bool IsInteger(string str)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (!regex.IsMatch(str))
            {
                return false;
            }

            return true;

        }
        catch (Exception ex)
        {
            MessageController.Show("Roll No Must be Neumeric!!!", MessageType.Warning, Page);
        }
        return false;
    }

    
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("tbl_Account");

        rptAccountHead.DataSource = dt;
        rptAccountHead.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Account aAccount = new Account();
        aAccount.SubHeadId = Convert.ToInt32(ddlSubAccountHead.SelectedItem.Text.Trim());
        aAccount.AccountCodeId = tbxAccountCode.Text.Trim();
        aAccount.AId = 102;
        aAccount.AccountName = tbxAccountName.Text.Trim();
        aAccount.Description = tbxDescription.Text.Trim();
        aAccount.Status = tbxStatus.Text.Trim();
        aAccount.Balance = Convert.ToSingle(tbxBalance.Text.Trim());
        aAccount.OpenDate = Convert.ToDateTime(tbxOpenDate.Text.Trim());
        aAccount.UpdateBy = Page.User.Identity.Name;
        aAccount.UpdateDate = DateTime.Now;


        ID = obj.UpdateAccount(ID, aAccount);

        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);

        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetAccountById(ID);
        if (dt.Rows.Count > 0)
        {
            ddlSubAccountHead.SelectedItem.Text = dt.Rows[0]["SubHeadId"].ToString();
            tbxAccountCode.Text = dt.Rows[0]["AccountCodeId"].ToString();
            tbxAccountName.Text = dt.Rows[0]["AccountName"].ToString();
            tbxDescription.Text = dt.Rows[0]["Description"].ToString();
            tbxStatus.Text = dt.Rows[0]["Status"].ToString();
            tbxOpenDate.Text = Convert.ToDateTime(dt.Rows[0]["OpenDate"]).ToString("dd/MM/yyyy");
            tbxBalance.Text = dt.Rows[0]["Balance"].ToString();
        }
        ddlSubAccountHead.Enabled = false;
        tbxAccountCode.ReadOnly = true;
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("tbl_Account", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}