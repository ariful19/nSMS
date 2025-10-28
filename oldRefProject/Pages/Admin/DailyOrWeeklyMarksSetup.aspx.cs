using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_DailyOrWeeklyMarksSetup : System.Web.UI.Page
{
    dalDailyOrWeeklyMarksSetup obj = new dalDailyOrWeeklyMarksSetup();
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
    protected bool GetDefault()
    {
        bool flag;
        DataTable dt = obj.GetDefault();
        if (dt.Rows.Count > 0)
            flag = true;
        else
            flag = false;
        return flag;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkDefault.Checked && GetDefault() == true)
        {
            MessageController.Show("A Daily Or Weekly Mark already set as default. Only one year can be set as default Daily Or Weekly Mark. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        ID = obj.Insert(tbxDailyMark.Text, chkDefault.Checked);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This Daily Or Weekly Mark already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (chkDefault.Checked && GetDefault() == true)
        {
            MessageController.Show("A Daily Or Weekly Mark already set as default. Only one year can be set as default Daily Or Weekly Mark. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        obj.Update(ID, tbxDailyMark.Text, chkDefault.Checked);
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
        tbxDailyMark.Text = "";
        chkDefault.Checked = false;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("bs_DailyOrWeeklyMarks");
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxDailyMark.Text = dt.Rows[0]["DailyOrWeeklyMarks"].ToString();
            chkDefault.Checked = dt.Rows[0]["IsDefault"].ToString() == "True" ? true : false;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("bs_DailyOrWeeklyMarks", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}