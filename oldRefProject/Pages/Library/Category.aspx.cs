using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Library_Categoy : System.Web.UI.Page
{
    dalLibraryBasic obj = new dalLibraryBasic();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnSave.CssClass = Common.SessionInfo.Button;
            //btnEdit.CssClass = Common.SessionInfo.Button;
            //btnReset.CssClass = Common.SessionInfo.Button;
            BindData();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ID = obj.InsertCategory(tbxName.Text);
        if (ID != -1)
        {
            tbxName.Text = "";
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This category already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        obj.UpdateCategory(ID, tbxName.Text);
        tbxName.Text = "";
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxName.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;

    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("lb_Category");
        rpt.DataSource = dt;
        rpt.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById("lb_Category", ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["Category"].ToString();
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("lb_Category", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}