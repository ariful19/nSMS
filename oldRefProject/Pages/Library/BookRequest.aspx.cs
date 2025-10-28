using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Library_BookRequest : BasePage
{
    dalLibrary obj = new dalLibrary();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            LoadBookList();
        }
    }
    protected void LoadBookList()
    {
        string criteria = "";
        DataTable dt = obj.GetBook(criteria);
        rpt.DataSource = dt;
        rpt.DataBind();
    }
    protected void OnRatingChanged(object sender, RatingEventArgs e)
    {

    }
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetBookById(ID);
        if (dt.Rows.Count > 0)
        {
            lblCategory.Text = dt.Rows[0]["Category"].ToString();
            lblSubCategory.Text = dt.Rows[0]["SubCategory"].ToString();
            lblCountry.Text = dt.Rows[0]["Country"].ToString();
            lblLanguage.Text = dt.Rows[0]["Language"].ToString();
            lblPublisher.Text = dt.Rows[0]["Publisher"].ToString();
            lblEdtion.Text = dt.Rows[0]["Edition"].ToString();
            lblISBN.Text = dt.Rows[0]["ISBN"].ToString();
            lblVolume.Text = dt.Rows[0]["VolumeNo"].ToString();
            lblSelfNo.Text = dt.Rows[0]["SelfNo"].ToString();
            lblCellNo.Text = dt.Rows[0]["CellNo"].ToString();
            lblStock.Text = dt.Rows[0]["Stock"].ToString();
            lblTitle.Text = dt.Rows[0]["Title"].ToString();
            lblAuthor.Text = dt.Rows[0]["Author"].ToString();
            lblSubTitle.Text = dt.Rows[0]["SubTitle"].ToString();
            lblKeyWord.Text = dt.Rows[0]["KeyWords"].ToString();
            lblDescription.Text = dt.Rows[0]["Description"].ToString();
            lblAvailable.Text = dt.Rows[0]["Status"].ToString() == "True" ? "Yes" : "No";
            if (dt.Rows[0]["CoverPhoto"].ToString() == "")
            {
                imgCover.ImageUrl = "~/Images/Common/no-photo.jpg";
            }
            else
            {
                imgCover.ImageUrl = "~/Images/Book/" + dt.Rows[0]["CoverPhoto"].ToString();
            }

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
    }
    
}