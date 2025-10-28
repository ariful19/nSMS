using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_StaticContent : System.Web.UI.Page
{
    dalStaticContent obj = new dalStaticContent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ddlPage.DataSource = new Common().GetAll("bs_StaticContent");
            ddlPage.DataBind();
          //  LoadDefault();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        obj.update(ddlPage.SelectedItem.Text, tbxContent.Text);
        MessageController.Show(MessageCode.UpdateSucceeded,MessageType.Information,Page);
    }

    protected void LoadDefault()
    {
        DataTable dt = obj.GetByPageName(ddlPage.SelectedItem.Text);
        if(dt.Rows.Count>0)
        {
            tbxContent.Text = dt.Rows[0]["Contents"].ToString();
        }
    }

    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDefault();
    }
}