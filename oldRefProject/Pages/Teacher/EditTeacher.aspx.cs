using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Teacher_EditTeacher : System.Web.UI.Page
{
    protected static int ID;
    protected static string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.CanAccess(this);
            LoadData();       
        }
    }

    private void LoadData()
    {
        DataTable dt = new dalTeacher().GetAllTeacher();
        rptTeacher.DataSource = dt;
            //(DataTable)new dalTeacher().GetAllTeacher();
        rptTeacher.DataBind();
        
    }

    //protected void rptTeacher_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        ImageButton btnEdit = (ImageButton)e.Item.FindControl("imgBtn");
    //        ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
    //        btnEdit.Visible = (Page.User.IsInRole("Admin")) ? true : false;
    //        btnDelete.Visible = (Page.User.IsInRole("Admin")) ? true : false;                     
    //    }
    //}

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        MessageController.Clear(this);
        userName = e.CommandArgument.ToString();
        Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + userName);
    }
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        MessageController.Clear(this);
        userName = e.CommandArgument.ToString();
        Response.Redirect("~/Pages/User/TeacherProfile.aspx?UserName=" + userName);
    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        MessageController.Clear(this);
        try
        {
            userName = e.CommandArgument.ToString();
            ID = Controller.PersonIdByUserName(userName);
            if (ID > 0)
            {
                new dalTeacher().DeleteTeacherById(ID, Page.User.Identity.Name, DateTime.Now, false);
            }
            new dalTeacher().UpdateUserName(userName, "NULL", "NULL");

            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            Response.Redirect("~/Pages/Teacher/EditTeacher.aspx");
        }
        catch (Exception ex)
        {
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
}