using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Viewer_StudentSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
        }
    }
    protected void Load()
    {

        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        //var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        //ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        AddBlank();
    }

    private void AddBlank()
    {
        var ddls = new[] { ddlYear, ddlMedium, ddlCampus, ddlClass, ddlGroup, ddlShift, ddlSection };
        foreach (var item in ddls)
        {
            item.Items.Insert(0, new ListItem("--Select--", ""));
            item.SelectedIndex= 0;
        }
    }

    [WebMethod]
    public static string GetData(string name)
    {
        return name;
    }
}