using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var headTxt = "";
        var bodyTxt = "";
        if (Session["HeadText"] == null)
        {
            headTxt = Request.Form["HeadText"];
            bodyTxt = Request.Form["BodyText"];
            Session["HeadText"] = headTxt;
            Session["BodyText"] = bodyTxt;
        }
        else
        {
            headTxt = Session["HeadText"].ToString();
            bodyTxt = Session["BodyText"].ToString();
            Session["HeadText"] = null;
            Session["BodyText"] = null;
        }
        litHead.Text = Encoding.UTF8.GetString(Convert.FromBase64String(headTxt));
        litBody.Text = Encoding.UTF8.GetString(Convert.FromBase64String(bodyTxt));

        var cmd = Request.QueryString["cmd"];
        if (cmd != null)
        {
            switch (cmd)
            {
                case "UpdPmtSlpNo":
                    Common.GetPaymentSlipNo(true);
                    break;
                default:
                    break;
            }
        }
    }
}