using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_MessageCredit : System.Web.UI.Page
{
    dalMessageCredit obj = new dalMessageCredit();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        tbxDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        tbxTranscation.Text = Controller.TransactionNumber().ToString();
        if (!IsPostBack)
        {
            btnSave.CssClass = Common.SessionInfo.Button;
            btnEdit.CssClass = Common.SessionInfo.Button;
            btnReset.CssClass = Common.SessionInfo.Button;
            BindData();
        }
    }
    protected void ClearAll()
    {
        tbxDate.Text = string.Empty;
        tbxQuantity.Text = string.Empty;
        tbxAmount.Text = string.Empty;
        tbxTranscation.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;

    }
    protected bool GetDefault()
    {
        bool flag;
        DataTable dt = obj.GetDefault();
        if (dt.Rows.Count > 0)
            flag = true;
        else
            flag = false;
        return flag;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (chkDefault.Checked && GetDefault() == true)
        {
            MessageController.Show("A credit already set as default. Only one credit can be set as default. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        MessageCredit msgCredit = new MessageCredit();

        msgCredit.PurchaseDate = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        msgCredit.PurchaseQuantity = Convert.ToInt32(tbxQuantity.Text);
        msgCredit.SendQuantity = 0;
        msgCredit.Balance = Convert.ToDecimal(tbxAmount.Text);
        msgCredit.AvailableBalance = Convert.ToDecimal(tbxAmount.Text);
        msgCredit.TransactionNumber = tbxTranscation.Text;
        msgCredit.Status = true;
        msgCredit.CreatedBy = Page.User.Identity.Name;
        msgCredit.CreatedDate = DateTime.Now;
        if (chkDefault.Checked == true)
        {
            msgCredit.IsDefault = true;
        }
        if (chkDefault.Checked == false)
        {
            msgCredit.IsDefault = false;
        }

        ID=obj.MessageCreditInsert(msgCredit);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This Defult already exists. Please try anothor.", MessageType.Error, Page);

        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        ClearAll();
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("MessageCredit");
        rptMessageCredit.DataSource = dt;
        rptMessageCredit.DataBind();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        if (chkDefault.Checked && GetDefault() == true)
        {
            MessageController.Show("A credit already set as default. Only one credit can be set as default. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        MessageCredit msgCredit = new MessageCredit();

        msgCredit.PurchaseDate = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        msgCredit.PurchaseQuantity = Convert.ToInt32(tbxQuantity.Text);
        msgCredit.Balance = Convert.ToDecimal(tbxAmount.Text);
        msgCredit.TransactionNumber = tbxTranscation.Text;
        msgCredit.Status = true;
        if (chkDefault.Checked == true)
        {
            msgCredit.IsDefault = true;
        }
        if (chkDefault.Checked == false)
        {
            msgCredit.IsDefault = false;
        }

        new dalMessageCredit().MessageCreditUpdate(ID, msgCredit);
        tbxDate.Text = string.Empty;
        tbxQuantity.Text = string.Empty;
        tbxAmount.Text = string.Empty;
        tbxTranscation.Text = string.Empty;
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["PurchaseDate"]).ToString("dd/MM/yyyy");
            tbxQuantity.Text = Convert.ToInt32(dt.Rows[0]["PurchaseQuantity"]).ToString();
            tbxAmount.Text = Convert.ToDecimal(dt.Rows[0]["Balance"]).ToString();
            tbxTranscation.Text = dt.Rows[0]["TransactionNumber"].ToString();
            if (chkDefault.Checked==true)
            {
                chkDefault.Checked = Convert.ToBoolean(dt.Rows[0]["IsDefault"].ToString());
            }
            if (chkDefault.Checked == false)
            {
                chkDefault.Checked = Convert.ToBoolean(dt.Rows[0]["IsDefault"].ToString());
            }
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("MessageCredit", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}