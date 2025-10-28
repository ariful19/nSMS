using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Fees_PaymentType : BasePage
{
    dalPaymentType obj = new dalPaymentType();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
  Common.CanAccess(this);
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkDefault.Checked && GetDefault() == true)
        {
            MessageController.Show("A Payment Type already set as default. Only one Type can be set as default Payment Type. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        if (btnSave.Text == "Save")
        {
            ID = obj.Insert(tbxName.Text,chkMonthly.Checked, chkDefault.Checked, Page.User.Identity.Name);
            if (ID != -1)
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            else
                MessageController.Show("This payment type name already exists. Please try anothor.", MessageType.Error, Page);
        }
        else
        {
            obj.Update(ID, tbxName.Text, chkMonthly.Checked, chkDefault.Checked, Page.User.Identity.Name);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        BindData();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxName.Text = "";
        btnSave.Text = "Save";
        chkDefault.Checked = false;
        ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("fee_PaymentType");
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {

        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["PaymentType"].ToString();
            chkMonthly.Checked = dt.Rows[0]["IsMonthly"].ToString() == "True" ? true : false;
            chkDefault.Checked = dt.Rows[0]["IsDefault"].ToString() == "True" ? true : false;
        }
        btnSave.Text = "Edit";
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("fee_PaymentType", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
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
    protected void rptPaymentType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
     
        if (e.Item.ItemType == ListItemType.Header)
        {

        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblMonthly = (Label)e.Item.FindControl("lblMonthly");

            if (lblMonthly.Text != "True")
            {
                lblMonthly.Text = "Academic";
            }
            else
            {
                lblMonthly.Text = "Monthly";
            }
        }
    }
}
