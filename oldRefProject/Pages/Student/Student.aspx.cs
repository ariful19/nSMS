using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_Student : System.Web.UI.Page
{
    dalStudent objStudent = new dalStudent();
    protected void Page_Load(object sender, EventArgs e)
    {
        tbxRegNo.Text = Controller.RegistrationNo().ToString();
        if (!IsPostBack)
        {
            string ss = Common.SessionInfo.Panel;
            LoadDropdown();
           // LoadAssignment();
            Session["Password"] = null;
        }
    }

    int PersonId
    {
        set { ViewState["PersonId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["PersonId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int StudentId
    {
        set { ViewState["StudentId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["StudentId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int AdaborId
    {
        set { ViewState["AdaborId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["AdaborId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int ZigatolaId
    {
        set { ViewState["ZigatolaId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["ZigatolaId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    #region Load All Dropdown
    protected void LoadDropdown()
    {
        Common.LoadDropdown(ddlGender, "bs_Gender", 1, 0);
        Common.LoadDropdown(ddlReligion, "bs_Religion", 1, 0);
        Common.LoadDropdown(ddlFatherEdu, "bs_Qualification", 1, 0);
        Common.LoadDropdown(ddlFatherPro, "bs_Profession", 1, 0);
        Common.LoadDropdown(ddlMotherEdu, "bs_Qualification", 1, 0);
        Common.LoadDropdown(ddlMotherPro, "bs_Profession", 1, 0);
        Common.LoadDropdown(ddlPresentDiv, "bs_Division", 1, 0);
        Common.LoadDropdown(ddlPermanentDiv, "bs_Division", 1, 0);
        Common.LoadDropdown(ddlBlood, "bs_BloodGroup", 1, 0);

        Common.LoadDropdown(ddlYear, "bs_Year", 1, 0);
        Common.LoadDropdown(ddlClass, "bs_ClassName", 1, 0);
        Common.LoadDropdown(ddlGroup, "bs_Group", 1, 0);
        Common.LoadDropdown(ddlShift, "bs_Shift", 1, 0);
        Common.LoadDropdown(ddlSection, "bs_Section", 1, 0);
        DataTable dt = new dalYear().GetDefault();
        if(dt.Rows.Count>0)
        {
            ddlYear.SelectedValue = dt.Rows[0][0].ToString();
        }
    }

    [WebMethod]
    public static ArrayList LoadDistrictByDivision(int divId)
    {
        ArrayList list = new ArrayList();
        DataTable dt = new dalDistrict().GetByDivisionId(divId);
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new ListItem(
                   row["District"].ToString(),
                   row["Id"].ToString()
                    ));
        }

        return list;
    }
    [WebMethod]
    public static ArrayList LoadThanaByDistrict(int disId)
    {
        ArrayList list = new ArrayList();
        DataTable dt = new dalThana().GetByDistrictId(disId);
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new ListItem(
                   row["Thana"].ToString(),
                   row["Id"].ToString()
                    ));
        }
        return list;
    }

    protected void LoadPresentDistrictByDivision()
    {
        if (ddlPresentDiv.SelectedValue != "")
        {
            DataTable dt = new dalDistrict().GetByDivisionId(Convert.ToInt32(ddlPresentDiv.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                ddlPresentDis.DataSource = dt;
                ddlPresentDis.DataBind();
            }
            else
            {
                MessageController.Show("There is no district in this division. Please insert District first.", MessageType.Error, Page);
                ddlPresentDis.DataSource = null;
                ddlPresentDis.DataBind();
                ddlPresentDis.BorderColor = System.Drawing.Color.Red;
            }

        }
    }

    protected void LoadPresentThanaByDistrict()
    {
        if (ddlPresentDis.SelectedValue != "")
        {
            DataTable dt = new dalThana().GetByDistrictId(Convert.ToInt32(ddlPresentDis.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                ddlPresentThana.DataSource = dt;
                ddlPresentThana.DataBind();
            }
            else
            {
                MessageController.Show("There is no Thana in this district. Please insert Thana first.", MessageType.Error, Page);
                ddlPresentThana.DataSource = null;
                ddlPresentThana.DataBind();
            }
        }
    }

    protected void LoadPermanentDistrictByDivision()
    {
        if (ddlPermanentDiv.SelectedValue != "")
        {
            DataTable dt = new dalDistrict().GetByDivisionId(Convert.ToInt32(ddlPermanentDiv.SelectedValue));
            ddlPermanentDis.DataSource = dt;
            ddlPermanentDis.DataBind();
        }
    }

    protected void LoadPermanentThanaByDistrict()
    {
        if (ddlPermanentDis.SelectedValue != "")
        {
            DataTable dt = new dalThana().GetByDistrictId(Convert.ToInt32(ddlPermanentDis.SelectedValue));
            ddlPermanentThana.DataSource = dt;
            ddlPermanentThana.DataBind();
        }
    }
    #endregion

    #region Student Assign
    protected void LoadAssignment()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
        ddlYear.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

    }
    #endregion
    protected void btnBasic_Click(object sender, EventArgs e)
    {
        MembershipCreateStatus msc;
        PersonId = objStudent.StudentBasicInformationInsert(tbxNameEng.Text, tbxNameBan.Text, Convert.ToInt32(ddlGender.SelectedValue), Convert.ToInt32(ddlReligion.SelectedValue), DateTime.ParseExact(tbxDateOfBirth.Text, "dd/MM/yyyy", null), tbxBirthCertificate.Text,
            tbxPhone.Text, tbxMobile.Text, tbxPhnHome.Text, tbxMobHome.Text, Convert.ToInt32(ddlBlood.SelectedValue),tbxUserName.Text);
        if(PersonId!=-1)
        {
            Membership.CreateUser(tbxUserName.Text, tbxPassword.Text);
            Roles.AddUserToRole(tbxUserName.Text, Controller.RoleNameById(1));
            StudentId = objStudent.Insert(PersonId, tbxRegNo.Text, DateTime.Now, DateTime.Now.Year.ToString(), Page.User.Identity.Name, DateTime.Now, true, Convert.ToInt32(1), AdaborId, ZigatolaId);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "buttonclick('#class');", true);
        }
        else
        {
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);
        }
    }
}