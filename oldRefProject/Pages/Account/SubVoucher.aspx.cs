using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_SubVoucher : System.Web.UI.Page
{
    dalVoucher obj = new dalVoucher();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            BindData();
            tbxSubVoucherName.ReadOnly = true;
            tbxSubVoucherCode.ReadOnly = true;
        }
    }
    #region Load Data
    protected void Load()
    {
        ddlMainVoucherCode.DataSource = new Common().GetAll("tbl_Main_Voucher");
        ddlMainVoucherCode.DataBind();
    }
    #endregion
    protected void ClearAll()
    {
        tbxSubVoucherCode.Text = string.Empty;
        tbxSubVoucherName.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
        //ddlMainVoucherCode.Enabled = true;
        //tbxSubVoucherCode.ReadOnly = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        ID = obj.InsertSubVoucher(ddlMainVoucherCode.SelectedItem.Text, tbxSubVoucherCode.Text.Trim(), tbxSubVoucherName.Text.Trim());
        if (ID != -1)
        {
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Sub Voucher Code already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }

    protected void BindData()
    {
        DataTable dt = new Common().GetAll("tbl_Sub_Voucher");

        rptSubVoucher.DataSource = dt;
        rptSubVoucher.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ID = obj.UpdateSubVoucher(ID, ddlMainVoucherCode.SelectedItem.Text, tbxSubVoucherCode.Text.Trim(), tbxSubVoucherName.Text.Trim());

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
        DataTable dt = obj.GetSubVoucherdById(ID);
        if (dt.Rows.Count > 0)
        {
            ddlMainVoucherCode.SelectedItem.Text = dt.Rows[0]["VoucherCodeId"].ToString();
            tbxSubVoucherCode.Text = dt.Rows[0]["SubVoucherId"].ToString();
            tbxSubVoucherName.Text = dt.Rows[0]["SubVoucherName"].ToString();
        }
        ddlMainVoucherCode.Enabled = false;
        tbxSubVoucherCode.ReadOnly = true;
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("tbl_Sub_Voucher", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }
}