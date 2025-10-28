
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Menu_PageForCopy : System.Web.UI.Page
{
    dalPageContent obj = new dalPageContent();
   
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            String data = Request.QueryString["id"];
            if(data != "")
            {

                int id = Convert.ToInt32(data);

                if (Request.Cookies["CurrentLanguage"].Value == "bn-BD")
                {
                  
                    DataTable dt = obj.GetDataBangla(id);
                    if (dt.Rows.Count > 0)
                    {
                        mainContent.InnerHtml = dt.Rows[0]["PageContentAll"].ToString();
                    }
                }
                else
                {
                  
                    DataTable dt = obj.GetDataEnglish(id);
                    if (dt.Rows.Count > 0)
                    {
                        mainContent.InnerHtml = dt.Rows[0]["PageContentAll"].ToString();
                    }
                }


                //DataTable dt = new dalPageContent().GetById(id);
                //if (dt.Rows.Count > 0)
                //{
                //    mainContent.InnerHtml = dt.Rows[0]["PageContent"].ToString();


                //}
            }

            //string url = Path.GetFileName(Page.AppRelativeVirtualPath);

            //string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + ".aspx";
           
            //DataTable dt = new dalPageContent().GetByName(pageName);
            //if (dt.Rows.Count > 0)
            //{
            //    mainContent.InnerHtml = dt.Rows[0]["PageContent"].ToString();
              

            //}
        }
    }

}