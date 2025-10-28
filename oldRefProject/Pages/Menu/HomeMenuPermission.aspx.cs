using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Menu_HomeMenuPermission : System.Web.UI.Page
{
    dalTaskManager objTask = new dalTaskManager();
   protected int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.LoadDropdown(ddlMenu, "HomeMenu", 1, 0);

        }
        BindData();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        var sdf = this.ddlMenu.SelectedItem.Value;
        if (sdf == "")
        {
            sdf = string.IsNullOrEmpty(hdfMenuId.Value) ? "" : hdfMenuId.Value.ToString();
        }

        DateTime startDate = DateTime.ParseExact(tbxPublishStartDate.Text,"dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(tbxPublishEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        //var sdf = string.IsNullOrEmpty(hdfMenuId.Value) ? "" : hdfMenuId.Value.ToString();

        int menuId = 0;
        HomeMenu homeMenu = new HomeMenu();
        if(sdf != "")
        {
            menuId = Convert.ToInt32(sdf);
        }
        
            

        if(this.chkIsPublish.Checked)
        {
            homeMenu.IsPublish = true;
        }
        else
            homeMenu.IsPublish = false;


        homeMenu.PublishStartDate = startDate;
        homeMenu.PublishEndDate = endDate;

       
        if ( menuId > 0)
        {
            homeMenu.MenuID = menuId;
            ID = new dalHomeMenu().UpdateHomeMenuPublish(homeMenu);

            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data successfully updated');", true);
            Response.Redirect("~/Pages/Menu/HomeMenuPermission.aspx");
           
        }
        else
        {
            //homeMenu.CreatedBy = Page.User.Identity.Name;
            //homeMenu.CreatedDate = DateTime.Now.ToString("yyyy/MM/dd");
            //ID = new dalHomeMenu().InsertHomeMenu(homeMenu);

            
            //Response.Redirect("~/Pages/Menu/HomeMenu.aspx");
            //ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data successfully saved');", true);
           
            
        }
       
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }


  
    protected void ClearAll()
    {
        tbxPublishEndDate.Text = "";
        tbxPublishStartDate.Text = "";
       
    }
    protected void BindData()
    {
        rptTask.DataSource = null;
        DataTable dt = new dalHomeMenu().GetPublishedHomeMenu();
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
            this.ddlMenu.SelectedItem.Text = dt.Rows[0]["TextBan"].ToString();
            tbxPublishStartDate.Text = dt.Rows[0]["PublishStartDate"].ToString();
            tbxPublishEndDate.Text = dt.Rows[0]["PublishEndDate"].ToString();

            var ispublish = dt.Rows[0]["IsPublish"].ToString();
            if(ispublish == "True")
            {
                this.chkIsPublish.Checked = true;
            }
            else
            {
                this.chkIsPublish.Checked = false;
            }
            
        }

    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
       int Id = Convert.ToInt32(e.CommandArgument);
       new dalHomeMenu().Delete(Id);

        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);

        Response.Redirect("~/Pages/Menu/HomeMenuPermission.aspx");
    }
   
}