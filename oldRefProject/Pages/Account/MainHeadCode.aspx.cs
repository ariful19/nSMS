using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_MainHeadCode : System.Web.UI.Page
{
    dalMainHeadCode obj = new dalMainHeadCode();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            tbxCodeName.ReadOnly = true;
            tbxMainCode.ReadOnly = true;
            tbxDescription.ReadOnly = true;
        }
    }
    protected void ClearAll()
    {
        tbxCodeName.Text = string.Empty;
        tbxMainCode.Text = string.Empty;
        tbxDescription.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        MainHeadCode aMainHeadCode = new MainHeadCode();
        aMainHeadCode.MainCodeId = Convert.ToInt32(tbxMainCode.Text.Trim());
        aMainHeadCode.CodeName = tbxCodeName.Text.Trim();
        aMainHeadCode.Description = tbxDescription.Text.Trim();
        aMainHeadCode.AId = 102;

        ID=obj.InsertMainCode(aMainHeadCode);
        if (ID != -1)
        {
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Main Head Code already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }

    protected void BindData()
    {
        DataTable dt = new Common().GetAll("tbl_MainHeadCode");

        rptMainHead.DataSource = dt;
        rptMainHead.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MainHeadCode aMainHeadCode = new MainHeadCode();
        aMainHeadCode.MainCodeId = Convert.ToInt32(tbxMainCode.Text.Trim());
        aMainHeadCode.CodeName = tbxCodeName.Text.Trim();
        aMainHeadCode.Description = tbxDescription.Text.Trim();
        aMainHeadCode.AId = 102;


        ID=obj.UpdateMainCode(ID,aMainHeadCode);

        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
       
        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    //protected void btnEdit_Command(object sender, CommandEventArgs e)
    //{
    //    ID = Convert.ToInt32(e.CommandArgument);
    //    DataTable dt = obj.GetMainHeadCodeById(ID);
    //    if (dt.Rows.Count > 0)
    //    {
    //        tbxMainCode.Text = dt.Rows[0]["MainCodeId"].ToString();
    //        tbxCodeName.Text = dt.Rows[0]["CodeName"].ToString();
    //        tbxDescription.Text = dt.Rows[0]["Description"].ToString();
    //    }
    //    tbxMainCode.ReadOnly = true;
    //    btnSave.Visible = false;
    //    btnEdit.Visible=true;
    //}
    //protected void btnDelete_Command(object sender, CommandEventArgs e)
    //{
    //    ID = Convert.ToInt32(e.CommandArgument);
    //    new Common().Delete("tbl_MainHeadCode", ID);
    //    MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
    //    BindData();
    //}
}