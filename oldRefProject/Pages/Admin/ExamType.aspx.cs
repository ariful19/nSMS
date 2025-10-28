using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_ExamType : BasePage
{
    dalExamType obj = new dalExamType();
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

    protected bool GetDailyOrWeeklyExam()
    {
        bool flag;
        DataTable dt = obj.GetDailyOrWeeklyExam();
        if (dt.Rows.Count > 0)
        {
            flag = true;
        }
        else flag = false;
        return flag;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkDefault.Checked && GetDailyOrWeeklyExam() == true)
        {
            MessageController.Show("A Type already set As Daily Or Weekly Type. Only one year can be set as Daily Or Weekly Type Exam. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        ID = obj.Insert(tbxName.Text, chkDefault.Checked);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This Exam Type already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (chkDefault.Checked && GetDailyOrWeeklyExam() == true)
        {
            MessageController.Show("A Type already set As Daily Or Weekly Type. Only one year can be set as Daily Or Weekly Type Exam. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        obj.Update(ID, tbxName.Text, chkDefault.Checked);
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
        DataTable dt = new Common().GetAll("bs_ExamType");
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["ExamType"].ToString();
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("bs_ExamType", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}