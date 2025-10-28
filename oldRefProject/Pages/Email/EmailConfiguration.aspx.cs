using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Email_EmailConfiguration : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            LoadConfiguration();
        }

    }
    protected void LoadConfiguration()
    {
        DataTable dt = new Common().GetAll("EmailConfig");
        if(dt.Rows.Count>0)
        {
            if (dt.Rows[0]["SSL"].ToString() != "")
                chkSSL.Checked = Convert.ToBoolean(dt.Rows[0]["SSL"].ToString());
            if (dt.Rows[0]["Authentication"].ToString() != "")
                chkAuthentication.Checked = Convert.ToBoolean(dt.Rows[0]["Authentication"].ToString());
            tbxName.Text = dt.Rows[0]["DisplayName"].ToString();
            tbxDisplayEmail.Text = dt.Rows[0]["DisplayEmail"].ToString();
            tbxReply.Text = dt.Rows[0]["ReplyToEmail"].ToString();
            tbxServer.Text = dt.Rows[0]["SMTPServer"].ToString();
            tbxPort.Text = dt.Rows[0]["Port"].ToString();
            tbxUserName.Text = dt.Rows[0]["UserName"].ToString();
            tbxPassword.Text = dt.Rows[0]["Password"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            new dalEmailTemplete().ConfigurationUpdate(tbxName.Text.Trim(), tbxDisplayEmail.Text.Trim(), tbxReply.Text.Trim(), tbxServer.Text.Trim(), Convert.ToInt32(tbxPort.Text.Trim()),
                chkSSL.Checked, chkAuthentication.Checked, tbxUserName.Text.Trim(), tbxPassword.Text.Trim(), false);
            MessageController.Show(MessageCode.UpdateSucceeded,MessageType.Information, Page);
        }
        catch (Exception ex)
        {
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
}