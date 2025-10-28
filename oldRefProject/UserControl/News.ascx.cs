using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_News : System.Web.UI.UserControl
{
    dalNews obj = new dalNews();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            if (Request.Cookies["CurrentLanguage"].Value == "bn-BD")
            {
                LoadDataBangla();
            }
            else
            {
                LoadDataEnglish();
            }
      //  LoadData();
    }
    protected void LoadData()
    {
        DataTable dt = obj.GetLatest();
        rptNews.DataSource = dt;
        rptNews.DataBind();
    }
    protected void LoadDataBangla()
    {
        DataTable dt = obj.GetLatestBangla();
        rptNews.DataSource = dt;
        rptNews.DataBind();
    }
    protected void LoadDataEnglish()
    {
        DataTable dt = obj.GetLatestEnglish();
        rptNews.DataSource = dt;
        rptNews.DataBind();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hf = e.Item.FindControl("HiddenField1") as HiddenField;
        if (hf != null)
        {
            string val = hf.Value;
            Image img = e.Item.FindControl("Image1") as Image;
            img.ImageUrl = "~/Images/Common/" + val;
        }
    }

}