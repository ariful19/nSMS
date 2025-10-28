using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SMS_SMSTemplete : BasePage
{
    dalSMS obj = new dalSMS();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            if (chkDefault.Checked && GetDefault() == true)
            {
                MessageController.Show("A Template Name already set as default. Only one Template Name can be set as default. Please unchecked the box.", MessageType.Error, Page);
                return;
            }
            ID = obj.Insert(txtTemplateName.Text, tbxVariables.Text, tbxDetails.Text, chkDefault.Checked);
            if (ID != -1)
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            else
                MessageController.Show("This templete name already exists. Please try anothor.", MessageType.Error, Page);
        }
        else
        {
            if (chkDefault.Checked && GetDefault() == true)
            {
                MessageController.Show("A Template Name already set as default. Only one Template Name can be set as default. Please unchecked the box.", MessageType.Error, Page);
                return;
            }
            obj.Update(ID, txtTemplateName.Text, tbxVariables.Text, tbxDetails.Text, chkDefault.Checked);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
        ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
    }
    protected void ClearAll()
    {
        txtTemplateName.Text = string.Empty;
        tbxDetails.Text = string.Empty;
        tbxVariables.Text = string.Empty;
        tbxVariables.Enabled = true;
        chkDefault.Checked = false;
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("SMSTemplete");
        rptSMS.DataSource = dt;
        rptSMS.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            txtTemplateName.Text = dt.Rows[0]["Name"].ToString();
            chkDefault.Checked = dt.Rows[0]["IsDefault"].ToString() == "True" ? true : false;
            tbxVariables.Text = dt.Rows[0]["Variable"].ToString();
            tbxDetails.Text = dt.Rows[0]["Message"].ToString();
        }
        btnSave.Text = "Edit";
        tbxVariables.Enabled = false;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("SMSTemplete", ID);
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
}