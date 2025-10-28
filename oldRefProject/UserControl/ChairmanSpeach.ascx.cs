using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ChairmanSpeach : System.Web.UI.UserControl
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

       // LoadData();
    }
    protected void LoadData()
    {
        DataTable dt = new Common().GetAll("tblChairmanContent");
        if(dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["Titel"].ToString();
            lblName.Text = dt.Rows[0]["Name"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblAddress.Text = dt.Rows[0]["Address"].ToString();
            img.ImageUrl = "../../Images/Person/" + dt.Rows[0]["Photo"].ToString();
        }
       
    }
    protected void LoadDataBangla()
    {
        DataTable dt = new Common().GetAll("tblChairmanContent");
        if (dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["TitleBangla"].ToString();
            lblName.Text = dt.Rows[0]["NameBangla"].ToString();
            lblDesignation.Text = dt.Rows[0]["DesignationBangla"].ToString();
            lblAddress.Text = dt.Rows[0]["AddressBangla"].ToString();
            HyperLink2.Text = dt.Rows[0]["TitleBangla"].ToString();
            img.ImageUrl = "../Images/Person/" + dt.Rows[0]["Photo"].ToString();
        }
    }
    protected void LoadDataEnglish()
    {
        DataTable dt = new Common().GetAll("tblChairmanContent");
        if (dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["Titel"].ToString();
            lblName.Text = dt.Rows[0]["Name"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblAddress.Text = dt.Rows[0]["Address"].ToString();
            HyperLink2.Text = dt.Rows[0]["Titel"].ToString();
            img.ImageUrl = "../Images/Person/" + dt.Rows[0]["Photo"].ToString();
        }
       
    }


    

}