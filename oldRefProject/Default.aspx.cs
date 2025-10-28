using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.Security;
using System.Globalization;

public partial class _Default : BasePage
{
     dalPageContent obj = new dalPageContent();


    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];
            if (cookie == null && cookie.Value == null)
            {
                string lang = "en-US";
                CultureInfo Cul = CultureInfo.CreateSpecificCulture(lang);
                System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
                System.Threading.Thread.CurrentThread.CurrentCulture = Cul;

                HttpCookie cookie_new = new HttpCookie("Language");
                cookie_new.Value = lang;
                cookie_new.Expires = DateTime.Now.AddMonths(6);
                Response.SetCookie(cookie_new);
            }
            else
            {

                //if (Request.Cookies["CurrentLanguage"].Value == "bn-BD")
                //{
                //    LoadDataBangla();

                //}
                //else
                //{
                //    LoadDataEnglish();

                //}


                //string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + ".aspx";
                //DataTable dt = new dalPageContent().GetByName(pageName);
                //if (dt.Rows.Count > 0)
                //{
                //    mainContent.InnerHtml = dt.Rows[0]["PageContent"].ToString();


                //}
            }
        }
    }


    }

