using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_AttendenceType : System.Web.UI.Page
{
    dalAttendenceType obj = new dalAttendenceType();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.CssClass = Common.SessionInfo.Button;
            btnEdit.CssClass = Common.SessionInfo.Button;
            btnReset.CssClass = Common.SessionInfo.Button;
            BindData();
        }
    }
    protected void ClearAll()
    {
        tbxName.Text = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ID = obj.Insert(tbxName.Text.Trim());
        if (ID != -1)
        {
            tbxName.Text = string.Empty;
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Attendence Type already exists. Please try anothor.", MessageType.Error,Page);
        BindData();
        ClearAll();

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        obj.Update(ID, tbxName.Text);
        tbxName.Text = string.Empty;
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }

    protected void BindData()
    {
        DataTable dt = new Common().GetAll("bs_AttendenceType");
        rptAttendenceType.DataSource = dt;
        rptAttendenceType.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["AttendenceType"].ToString();
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }

    //protected void btnDelete_Command(object sender, CommandEventArgs e)
    //{
    //    ID = Convert.ToInt32(e.CommandArgument);
    //    new Common().Delete("bs_AttendenceType", ID);
    //    MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
    //    BindData();
    //}
}