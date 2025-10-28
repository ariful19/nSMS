using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RoleManagement_TaskManager : BasePage
{
    dalTaskManager objTask = new dalTaskManager();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParent();
        }
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string icon = Request.Form[tbxIconPicker.UniqueID];
        ID = objTask.Insert(tbxTitleEng.Text, tbxTitleBan.Text, tbxURL.Text, Convert.ToInt32(ddlParent.SelectedValue), Page.User.Identity.Name, DateTime.Now, icon);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This task name already exists. Please try anothor.", MessageType.Error, Page);

        BindData();
        GetParent();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string icon = Request.Form[tbxIconPicker.UniqueID];
        objTask.Update(ID, tbxTitleEng.Text, tbxTitleBan.Text, tbxURL.Text, Convert.ToInt32(ddlParent.SelectedValue), Page.User.Identity.Name, DateTime.Now, icon);
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        GetParent();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void GetParent()
    {
        DataTable dt = objTask.GetParanet(4);
        ddlParent.Items.Clear();
        ListItem li = new ListItem("Root", "0");
        ddlParent.Items.Add(li);
        foreach (DataRow row in dt.Rows)
        {
            ListItem item = new ListItem();
            item.Text = row["TextEng"].ToString();
            item.Value = (row["Id"].ToString());
            ddlParent.Items.Add(item);

        }
    }
    protected void ClearAll()
    {
        tbxTitleEng.Text = "";
        tbxTitleBan.Text = "";
        tbxURL.Text = "";
    }
    protected void BindData()
    {
        DataTable dt = objTask.GetAllTask();
        rptTask.DataSource = dt;
        rptTask.DataBind();

    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            ID = Convert.ToInt32(e.CommandArgument);
            DataTable dt = objTask.GetById(ID);
            if (dt.Rows.Count > 0)
            {
                tbxTitleEng.Text = dt.Rows[0]["TextEng"].ToString();
                tbxTitleBan.Text = dt.Rows[0]["TextBan"].ToString();
                tbxURL.Text = dt.Rows[0]["URL"].ToString();
                ddlParent.SelectedValue = dt.Rows[0]["ParentId"].ToString();
                tbxIconPicker.Text = dt.Rows[0]["Icon"].ToString();
            }
            btnEdit.Visible = true;
            btnSave.Visible = false;
        }
        catch (Exception)
        {
        }

    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("sa_TaskManager", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}