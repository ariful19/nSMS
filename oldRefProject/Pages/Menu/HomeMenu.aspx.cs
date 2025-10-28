using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Menu_HomeMenu : System.Web.UI.Page
{
    dalTaskManager objTask = new dalTaskManager();
   protected int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParent();
            GetContent();

        }
        BindData();
    }

    //protected void ClearAll()
    //{
    //    tbxTitleEng.Text = string.Empty;
    //    tbxTitleBan.Text = string.Empty;
    //    ddlContent.SelectedItem.Text = string.Empty;
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var sdf = string.IsNullOrEmpty(hdfMenuId.Value) ? "" : hdfMenuId.Value.ToString();
        int menuId = 0;
        HomeMenu homeMenu = new HomeMenu();
        if(sdf != "")
        {
            menuId = Convert.ToInt32(sdf);
         }
        homeMenu.TextEng = tbxTitleEng.Text;
        homeMenu.TextBan = tbxTitleBan.Text;
        string fileName = string.Empty;
        string path = Server.MapPath(fileName);

        string pageName = tbxTitleEng.Text.Replace(" ", "") + ".aspx";

        homeMenu.URL = "../../Pages/Menu/PageForCopy.aspx";
        homeMenu.ParentId = Convert.ToInt32(ddlParent.SelectedValue);
        homeMenu.CreatedBy = Page.User.Identity.Name;
        homeMenu.CreatedDate = DateTime.Now.ToString("yyyy/MM/dd");
        homeMenu.PageContentID = Convert.ToInt32(ddlContent.SelectedValue);
       
        if ( menuId > 0)
        {
            homeMenu.UpdatedBy = Page.User.Identity.Name;
            homeMenu.UpdatedDate = DateTime.Now.ToString("yyyy/MM/dd");
            homeMenu.MenuID = menuId;
            ID = new dalHomeMenu().UpdateHomeMenu(homeMenu);

            
            //ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data successfully updated');", true);
            BindData();
            ClearAll();
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            //Response.Redirect("~/Pages/Menu/HomeMenu.aspx");
            
           
        }
        else
        {
            homeMenu.CreatedBy = Page.User.Identity.Name;
            homeMenu.CreatedDate = DateTime.Now.ToString("yyyy/MM/dd");
            ID = new dalHomeMenu().InsertHomeMenu(homeMenu);

            
            Response.Redirect("~/Pages/Menu/HomeMenu.aspx");         
            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data successfully saved');", true);
            ClearAll();
        }
       
    }

    private void CreatePage(string pageName)
    {
        //string directory = pageName.Substring(0, pageName.LastIndexOf("/"));

        //if (!Directory.Exists(Server.MapPath(directory)))
        //{
        //    Directory.CreateDirectory(Server.MapPath(directory));
        //}

        if (!File.Exists(Server.MapPath(pageName)))
        {
            File.Copy(Server.MapPath("PageForCopy.aspx"), (Server.MapPath(pageName)));
            File.Copy(Server.MapPath("PageForCopy.aspx.cs"), (Server.MapPath(pageName + ".cs")));
        }

        // File.Copy(Server.MapPath("") + "\\Submit.aspx.cs", (Server.MapPath("") + "\\" + TextBox1.Text + ".aspx.cs"));
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string icon = Request.Form[tbxIconPicker.UniqueID];
        string pageName = tbxTitleEng.Text.Replace(" ", "") + ".aspx";
        string url = "../../Pages/Menu/PageForCopy.aspx";
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        GetParent();
        GetContent();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void GetParent()
    {
        DataTable dt = new dalHomeMenu().GetAllParanet();
        ddlParent.Items.Clear();
        ListItem li = new ListItem("Root", "0");
        ddlParent.Items.Add(li);
        foreach (DataRow row in dt.Rows)
        {
            ListItem item = new ListItem();
            item.Text = row["TextEng"].ToString();
            item.Value = (row["MenuID"].ToString());
            ddlParent.Items.Add(item);

        }
    }

    protected void GetContent()
    {
        Common.LoadDropdown(ddlContent, "PageContent", 1, 0);
        Common.LoadDropdown(ddlParentContent, "PageContent", 1, 0);
    }
    protected void ClearAll()
    {
        tbxTitleEng.Text = "";
        tbxTitleBan.Text = "";
       
    }
    protected void BindData()
    {
        rptTask.DataSource = null;
        DataTable dt = new dalHomeMenu().GetAllHomeMenu();
        rptTask.DataSource = dt;
        rptTask.DataBind();

    }

    protected void btnEdit1_Command(object sender, CommandEventArgs e)
    {
        Session["MenuId"] = Convert.ToInt32(e.CommandArgument);
        var menuid = Session["MenuId"];

        ID = Convert.ToInt32(menuid);
        
        DataTable dt = new dalHomeMenu().GetHomeMenuById(ID);
        if (dt.Rows.Count > 0)
        {
            this.hdfMenuId.Value = dt.Rows[0]["MenuID"].ToString();
            tbxTitleEng.Text = dt.Rows[0]["TextEng"].ToString();
            tbxTitleBan.Text = dt.Rows[0]["TextBan"].ToString();

            ddlContent.SelectedValue = dt.Rows[0]["PageContentID"].ToString();
            ddlParent.SelectedValue = dt.Rows[0]["ParentId"].ToString();
            tbxIconPicker.Text = dt.Rows[0]["Icon"].ToString();
        }

    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
       int Id = Convert.ToInt32(e.CommandArgument);
       new dalHomeMenu().Delete(Id);

        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        
        Response.Redirect("~/Pages/Menu/HomeMenu.aspx");
    }
    protected void btnParentSave_Click(object sender, EventArgs e)
    {
        int mID = 0;
        HomeMenu homeMenu = new HomeMenu();
        int order = 0;
        int.TryParse(tbxOrder.Text, out order);

        homeMenu.TextEng = tbxName.Text;
        homeMenu.TextBan = txtParantBangla.Text;
        string pageName = tbxName.Text.Replace(" ", "") + ".aspx";
        
        homeMenu.URL = "../../Pages/Menu/PageForCopy.aspx";
        homeMenu.ParentId = 0;
        homeMenu.Order = order;
        homeMenu.CreatedBy = Page.User.Identity.Name;
        homeMenu.CreatedDate = DateTime.Now.ToString("yyyy/MM/dd");

        if (ddlParentContent.SelectedItem.Text != "---Select---")
        {
            homeMenu.PageContentID = Convert.ToInt32(ddlParentContent.SelectedValue); ;

            mID = new dalHomeMenu().InsertHomeMenu(homeMenu);
            if (mID > 0)
            {
                Response.Redirect("~/Pages/Menu/HomeMenu.aspx");

            }
        }
        else
        {
            mID = new dalHomeMenu().InsertHomeMenuParent(homeMenu);
            if (mID > 0)
            {
                Response.Redirect("~/Pages/Menu/HomeMenu.aspx");

            }
        }

    }
}