using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_OutOfMarks : System.Web.UI.Page
{
    dalOutOfMarks obj = new dalOutOfMarks();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void ClearAll()
    {
        tbxOutOfMarks.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ID = obj.Insert(tbxOutOfMarks.Text.Trim());
        if (ID != -1)
        {
            tbxOutOfMarks.Text = string.Empty;
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Confirmation, Page);
        }
        else
        {
            MessageController.Show("This Out Of Mark already exists. Please try anothor.", MessageType.Error, Page);
        }
        BindData();
        ClearAll();
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("bs_OutOfMarks");
        rptOutOfMarks.DataSource = dt;
        rptOutOfMarks.DataBind();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        obj.Update(ID,tbxOutOfMarks.Text.Trim());
        tbxOutOfMarks.Text = string.Empty;
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information,Page);
        BindData();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxOutOfMarks.Text = dt.Rows[0]["OutOfMarks"].ToString();
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("bs_OutOfMarks", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}