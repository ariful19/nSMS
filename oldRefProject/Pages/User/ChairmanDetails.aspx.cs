using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_ChairmanDetails : BasePage
{
    dalChairmanContent obj = new dalChairmanContent();
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
   
    protected void LoadDataBangla()
    {
        DataTable dt = new Common().GetAll("tblChairmanContent");
        if (dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["TitleBangla"].ToString();
            lblDescription.Text = dt.Rows[0]["DescriptionBangla"].ToString();
        }
    }
    protected void LoadDataEnglish()
    {
        DataTable dt = new Common().GetAll("tblChairmanContent");
        if (dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["Titel"].ToString();
            lblDescription.Text = dt.Rows[0]["Description"].ToString();
            
        }

    }


    
}