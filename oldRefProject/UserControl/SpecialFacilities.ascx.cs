using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_SpecialFacilities : System.Web.UI.UserControl
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
        DataTable dt = obj.GetData("2");
        if (dt.Rows.Count > 0)
        {

            lblSpecialFacilities.Text = dt.Rows[0]["AboutUsbangla"].ToString();
           
        }
    }
    protected void LoadDataEnglish()
    {
        DataTable dt = obj.GetData("2");
        if (dt.Rows.Count > 0)
        {

            lblSpecialFacilities.Text = dt.Rows[0]["AboutUsEnglish"].ToString();
          
        }
       
    }


    

}