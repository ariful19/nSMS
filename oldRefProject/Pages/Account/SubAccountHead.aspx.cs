using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_SubAccountHead : System.Web.UI.Page
{
    dalAccountHead obj = new dalAccountHead();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            BindData();
            ddlMainCode.Enabled = false;
            tbxCodeName.ReadOnly = true;
            tbxHeadCode.ReadOnly = true;
            tbxDescription.ReadOnly = true;
        }
    }
    #region Load Data
    protected void Load()
    {
        ddlMainCode.DataSource = new Common().GetAll("tbl_AccountHead");
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
        SubAccountHead subAccountHead = new SubAccountHead();
        subAccountHead.SubHeadCodeId = Convert.ToInt32(tbxHeadCode.Text);
        subAccountHead.SubHeadName = tbxCodeName.Text;
        subAccountHead.Description = tbxDescription.Text;
        subAccountHead.HeadCodeId = Convert.ToInt32(ddlMainCode.SelectedItem.Text);
        subAccountHead.CreatedBy = Page.User.Identity.Name;
        subAccountHead.CreatedDate = DateTime.Now;

        ID = obj.InsertSubAccountHead(subAccountHead);
        if (ID != -1)
        {
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Sub Account Head already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }

    protected void BindData()
    {
        DataTable dt = new Common().GetAll("tbl_Sub_AccounHead");

        rptAccountHead.DataSource = dt;
        rptAccountHead.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        SubAccountHead subAccountHead = new SubAccountHead();
        subAccountHead.SubHeadCodeId = Convert.ToInt32(tbxHeadCode.Text.Trim());
        subAccountHead.SubHeadName = tbxCodeName.Text.Trim();
        subAccountHead.Description = tbxDescription.Text.Trim();
        subAccountHead.HeadCodeId = Convert.ToInt32(ddlMainCode.SelectedItem.Text.Trim());
        subAccountHead.UpdateBy = Page.User.Identity.Name;
        subAccountHead.UpdateDate = DateTime.Now;


        ID = obj.UpdateSubAccountHead(ID, subAccountHead);

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
        DataTable dt = obj.GetSubAccountHeadById(ID);
        if (dt.Rows.Count > 0)
        {
            ddlMainCode.SelectedItem.Text = dt.Rows[0]["HeadCodeId"].ToString();
            tbxHeadCode.Text = dt.Rows[0]["SubHeadCodeId"].ToString();
            tbxCodeName.Text = dt.Rows[0]["SubHeadName"].ToString();
            tbxDescription.Text = dt.Rows[0]["Description"].ToString();
        }
        ddlMainCode.Enabled = false;
        tbxHeadCode.ReadOnly = true;
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("tbl_Sub_AccounHead", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}