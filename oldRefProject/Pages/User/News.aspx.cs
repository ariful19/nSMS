using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_News : BasePage
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

}