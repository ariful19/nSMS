using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PayRoll_AllowanceToType : BasePage
{
    dalPayrollBasic obj = new dalPayrollBasic();
    dalPayroll objPayroll = new dalPayroll();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAllowance();
            LoadAllowanceToType();
        }
    }
    protected void LoadAllowance()
    {
        ddlType.DataSource = new Common().GetAll("pr_Type");
        ddlType.DataBind();

        rpt.DataSource = new Common().GetAll("pr_Allowance");
        rpt.DataBind();
    }
    protected DataTable Allowance()
    {
        DataTable dt = new DataTable("dtAllowance");
        dt.Columns.Add("TId", typeof(int));
        dt.Columns.Add("AId", typeof(int));
        dt.Columns.Add("Amount", typeof(int));
        return dt;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = Allowance();
        foreach (RepeaterItem item in rpt.Items)
        {
            TextBox tbxAmmount = (TextBox)item.FindControl("tbxAmount");
            HiddenField hdnId = (HiddenField)item.FindControl("hdnId");
            if (tbxAmmount.Text != "")
            {
                dt.Rows.Add(Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(hdnId.Value), Convert.ToInt32(tbxAmmount.Text));
            }
        }
        objPayroll.InserAllowanceToType(dt, Page.User.Identity.Name, DateTime.Now);
        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ID = obj.AllowanceInsert(tbxName.Text);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This allowance name already exists. Please try anothor.", MessageType.Error, Page);

        LoadAllowance();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllowanceToType();
    }
    protected void LoadAllowanceToType()
    {
        DataTable dt = obj.AllowanceGetByTypeId(Convert.ToInt32(ddlType.SelectedValue));
        if(dt.Rows.Count>0)
        {
            foreach (RepeaterItem item in rpt.Items)
            {
                TextBox tbxAmmount = (TextBox)item.FindControl("tbxAmount");
                HiddenField hdnId = (HiddenField)item.FindControl("hdnId");
                foreach (DataRow dr in dt.Rows)
                {
                    if(hdnId.Value.ToString()==dr["AllowanceId"].ToString())
                    {
                        tbxAmmount.Text = dr["AllowancePercent"].ToString();
                    }
                }
            }
            btnSave.Visible = false;
            btnEdit.Visible = true;
        }
        else
        {
            foreach (RepeaterItem item in rpt.Items)
            {
                TextBox tbxAmmount = (TextBox)item.FindControl("tbxAmount");
                HiddenField hdnId = (HiddenField)item.FindControl("hdnId");
                tbxAmmount.Text = "";
            }
            btnSave.Visible = true;
            btnEdit.Visible = false;
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        obj.AllowanceDeleteByTypeId(Convert.ToInt32(ddlType.SelectedValue));
        DataTable dt = Allowance();
        foreach (RepeaterItem item in rpt.Items)
        {
            TextBox tbxAmmount = (TextBox)item.FindControl("tbxAmount");
            HiddenField hdnId = (HiddenField)item.FindControl("hdnId");
            if (tbxAmmount.Text != "")
            {
                dt.Rows.Add(Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(hdnId.Value), Convert.ToInt32(tbxAmmount.Text));
            }
        }
        objPayroll.InserAllowanceToType(dt, Page.User.Identity.Name, DateTime.Now);
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
    }
}