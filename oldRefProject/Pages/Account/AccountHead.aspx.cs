using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_AccountHead : System.Web.UI.Page
{
    dalAccountHead obj = new dalAccountHead();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            BindData();
            tbxCodeName.ReadOnly = true;
            tbxHeadCode.ReadOnly = true;
            tbxDescription.ReadOnly = true;
        }
    }
    #region Load Data
    protected void Load()
    {
        ddlMainCode.DataSource = new Common().GetAll("tbl_MainHeadCode");
        ddlMainCode.DataBind();
    }
    #endregion
    protected void ClearAll()
    {
        tbxCodeName.Text = string.Empty;
        tbxHeadCode.Text = string.Empty;
        tbxDescription.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        AccountHead accountHead = new AccountHead();
        accountHead.HeadCodeId = Convert.ToInt32(tbxHeadCode.Text.Trim());
        accountHead.HeadName = tbxCodeName.Text.Trim();
        accountHead.AId = 102;
        accountHead.Description = tbxDescription.Text.Trim();
        accountHead.MainCodeId = Convert.ToInt32(ddlMainCode.SelectedItem.Text.Trim());
        accountHead.UpdateOn = DateTime.Now;
        accountHead.Balance = 0;

        ID = obj.InsertMainCode(accountHead);
        if (ID != -1)
        {
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Account Head Code already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }

    protected void BindData()
    {
        DataTable dt = new Common().GetAll("tbl_AccountHead");

        rptAccountHead.DataSource = dt;
        rptAccountHead.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        AccountHead accountHead = new AccountHead();
        accountHead.HeadCodeId = Convert.ToInt32(tbxHeadCode.Text.Trim());
        accountHead.HeadName = tbxCodeName.Text.Trim();
        accountHead.AId = 102;
        accountHead.Description = tbxDescription.Text.Trim();
        accountHead.MainCodeId = Convert.ToInt32(ddlMainCode.SelectedItem.Text.Trim());
        accountHead.UpdateOn = DateTime.Now;
        accountHead.Balance = 0;


        ID = obj.UpdateMainCode(ID, accountHead);

        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);

        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    //protected void btnEdit_Command(object sender, CommandEventArgs e)
    //{
    //    ID = Convert.ToInt32(e.CommandArgument);
    //    DataTable dt = obj.GetMainHeadCodeById(ID);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlMainCode.SelectedItem.Text = dt.Rows[0]["MainCodeId"].ToString();
    //        tbxHeadCode.Text = dt.Rows[0]["HeadCodeId"].ToString();
    //        tbxCodeName.Text = dt.Rows[0]["HeadName"].ToString();
    //        tbxDescription.Text = dt.Rows[0]["Description"].ToString();
    //    }
    //    ddlMainCode.Enabled = false;
    //    tbxHeadCode.ReadOnly = true;
    //    btnSave.Visible = false;
    //    btnEdit.Visible = true;
    //}
    //protected void btnDelete_Command(object sender, CommandEventArgs e)
    //{
    //    ID = Convert.ToInt32(e.CommandArgument);
    //    new Common().Delete("tbl_AccountHead", ID);
    //    MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
    //    BindData();
    //}
}