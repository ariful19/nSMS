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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + ".aspx";
            DataTable dt = new dalPageContent().GetByName(pageName);
            if (dt.Rows.Count > 0)
            {               
                mainContent.InnerText = dt.Rows[0]["Content"].ToString();
            }
        }
    }
}