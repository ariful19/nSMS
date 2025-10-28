using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_MainVoucher : System.Web.UI.Page
{
    dalVoucher obj = new dalVoucher();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            tbxVoucherCode.ReadOnly = true;
            tbxVoucherName.ReadOnly = true;
        }
    }
    protected void ClearAll()
    {
        tbxVoucherCode.Text = string.Empty;
        tbxVoucherName.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
        //tbxVoucherCode.ReadOnly = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MainVoucher aMainVoucher = new MainVoucher();
        aMainVoucher.VoucherCodeId = tbxVoucherCode.Text.Trim();
        aMainVoucher.VoucherName = tbxVoucherName.Text.Trim();
        aMainVoucher.CreatedBy = Page.User.Identity.Name;
        aMainVoucher.CreatedDate = DateTime.Now;

       
        if (ID != -1)
        {
            ID = obj.InsertMainVoucher(aMainVoucher);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Main Voucher Code already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }

    protected void BindData()
    {
        DataTable dt = new Common().GetAll("tbl_Main_Voucher");

        rptMainVoucher.DataSource = dt;
        rptMainVoucher.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MainVoucher aMainVoucher = new MainVoucher();
        aMainVoucher.VoucherCodeId = tbxVoucherCode.Text.Trim();
        aMainVoucher.VoucherName = tbxVoucherName.Text.Trim();
        aMainVoucher.UpdateBy = Page.User.Identity.Name;
        aMainVoucher.UpdateDate = DateTime.Now;


        ID = obj.UpdateMainVoucher(ID, aMainVoucher);

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
        DataTable dt = obj.GetMainVoucherById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxVoucherCode.Text = dt.Rows[0]["VoucherCodeId"].ToString();
            tbxVoucherName.Text = dt.Rows[0]["VoucherName"].ToString();
        }
        tbxVoucherCode.ReadOnly = true;
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("tbl_Main_Voucher", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }
}