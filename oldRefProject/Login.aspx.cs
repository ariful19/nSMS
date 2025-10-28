using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nano.DataAccessLayer;
using System.Data.SqlClient;

public partial class Login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }       
    }
    dalUser objUser = new dalUser();
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        // check balance

        using (var dm =new DatabaseManager())
        {
            var dt = dm.ExecuteQueryDt("select * from wr_Date where Id=1");

            DateTime date = dt.Rows.Count>0  ? dt.Rows[0].Field<DateTime>("ExpireDate") : DateTime.Now.AddDays(1);

            if (date < DateTime.Now )
            {
              
               FormsAuthentication.SignOut();    
            }
            else
            {
                Response.Redirect("~\\Pages\\Admin\\Dashboard.aspx");
            }

        }
        //Response.Redirect("~/Pages/Admin/Dashboard.aspx");
    }
   
}



