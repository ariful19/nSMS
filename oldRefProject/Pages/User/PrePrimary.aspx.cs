using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_Pre_Primary : System.Web.UI.Page
{
    dalPageContent obj = new dalPageContent();

    public int id = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["CurrentLanguage"].Value == "bn-BD")
            {
                LoadDataEnglish();
            }
            else
            {
                LoadDataBangla();
            }
        }
    }

    protected void LoadDataEnglish()
    {
        DataTable dt = obj.GetDataEnglish(id);
        if (dt.Rows.Count > 0)
        {
            var data = dt.Rows[0]["PageContentAll"].ToString();
            lblPrePrimary.Text = data;
        }
    }

    protected void LoadDataBangla()
    {
        DataTable dt = obj.GetDataBangla(id);
        if (dt.Rows.Count > 0)
        {
            var data = dt.Rows[0]["PageContentAll"].ToString();
            lblPrePrimary.Text = data;
        }
    }
}