using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Email_EmailTemplete : BasePage
{
    dalEmailTemplete obj = new dalEmailTemplete();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ID = obj.Insert(txtTemplateName.Text, txtSubject.Text, tbxVariables.Text, tbxDetails.Text);
            if (ID != -1)
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            else
                MessageController.Show("This templete name already exists. Please try anothor.", MessageType.Error, Page);
        }
        else
        {
            obj.Update(ID, txtTemplateName.Text, txtSubject.Text, tbxVariables.Text, tbxDetails.Text);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        txtTemplateName.Text = string.Empty;
        txtSubject.Text = string.Empty;
        tbxDetails.Text = string.Empty;
        tbxVariables.Text = string.Empty;
        ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("EmailTemplate");
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            txtTemplateName.Text = dt.Rows[0]["TemplateName"].ToString();
            txtSubject.Text = dt.Rows[0]["Subject"].ToString();
            tbxVariables.Text = dt.Rows[0]["Variables"].ToString();
            tbxDetails.Text = dt.Rows[0]["Body"].ToString();
        }
        btnSave.Text = "Edit";
        tbxVariables.Enabled = false;
    }
}