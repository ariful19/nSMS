using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_NewsMarquee : System.Web.UI.UserControl
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
    
    protected void LoadDataBangla()
    {
        DataTable dt = obj.GetLatestBangla();

        string s1;
        s1 = " ***  ";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            s1 += dt.Rows[i][1].ToString() + "";
          s1 += " || ";
        }
        s1 += "  *** ";
        ltrlMarque.Text = s1.ToString();
      
    }
    protected void LoadDataEnglish()
    {
        DataTable dt = obj.GetLatestEnglish();

        string s1;
        s1 = " ***  ";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            s1 += dt.Rows[i][1].ToString() + "";
            s1 += " || ";
        }
        s1 += "  *** ";
        ltrlMarque.Text = s1.ToString();

    }


    

}
