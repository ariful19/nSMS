using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_EditStudent : System.Web.UI.Page
{
    dalPerson objPerson = new dalPerson();
    dalStudent objStudent = new dalStudent();
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckRollNoUse();
            Load();
            LoadPromotionData();
            
            if (Page.User.Identity.Name != "")
            {
               
            }
        }
    }

    #region Load All Dropdown
    protected void LoadDropdownData()
    {
        ddlGender.DataSource = new Common().GetAll("bs_Gender");
        ddlGender.DataBind();

        ddlReligion.DataSource = new Common().GetAll("bs_Religion");
        ddlReligion.DataBind();

        ddlFatherEdu.DataSource = new Common().GetAll("bs_Qualification");
        ddlFatherEdu.DataBind();

        ddlFatherPro.DataSource = new Common().GetAll("bs_Profession");
        ddlFatherPro.DataBind();

        ddlMotherEdu.DataSource = new Common().GetAll("bs_Qualification");
        ddlMotherEdu.DataBind();

        ddlMotherPro.DataSource = new Common().GetAll("bs_Profession");
        ddlMotherPro.DataBind();

        ddlPresentDiv.DataSource = new Common().GetAll("bs_Division");
        ddlPresentDiv.DataBind();

        ddlPermanentDiv.DataSource = new Common().GetAll("bs_Division");
        ddlPermanentDiv.DataBind();

        ddlBlood.DataSource = new Common().GetAll("bs_BloodGroup");
        ddlBlood.DataBind();

    }

    [WebMethod]
    public static ArrayList LoadPresentDistrictByDivision(int divId)
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
    public static ArrayList LoadPresentThanaByDistrict(int disId)
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

    #region Load Data
    protected void Load()
    {   
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        Common.LoadDropdown(ddlMedium, "bs_Medium", 1, 0);
        Common.LoadDropdown(ddlCampus, "bs_CampusName", 1, 0);

        //Common.LoadDropdown(ddlClass, "bs_ClassName", 1, 0);

        //ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        //ddlClass.DataBind();
    }
    protected void LoadPromotionData()
    {
        //ddlPromotionYear.DataSource = new Common().GetAll("bs_Year");
        //ddlPromotionYear.DataBind();

        //ddlPromotionMedium.DataSource = new Common().GetAll("bs_Medium");
        //ddlPromotionMedium.DataBind();

        //ddlPromotionCampus.DataSource = new Common().GetAll("bs_CampusName");
        //ddlPromotionCampus.DataBind();

        //ddlPromotionClass.DataSource = new Common().GetAll("bs_ClassName");
        //ddlPromotionClass.DataBind();

        //ddlPromotionGroup.DataSource = new Common().GetAll("bs_Group");
        //ddlPromotionGroup.DataBind();

        //ddlPromotionShift.DataSource = new Common().GetAll("bs_Shift");
        //ddlPromotionShift.DataBind();

        //ddlPromotionSection.DataSource = new Common().GetAll("bs_Section");
        //ddlPromotionSection.DataBind();
        ddlStatus.DataSource = new Common().GetAll("bs_Status");
        ddlStatus.DataBind();

    }
    #endregion

    #region index change
	  protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.LoadDropdown(ddlClass, "bs_CampusName", 1, 0);

        this.ddlCampus.SelectedIndex = -1;
        this.ddlClass.SelectedIndex = -1;
        this.ddlGroup.SelectedIndex = -1;
        this.ddlSection.SelectedIndex = -1;
        this.ddlShift.SelectedIndex = -1;
        this.ddlRollNo.SelectedIndex = -1;
        this.ddlRegNo.SelectedIndex = -1;

        this.ddlGroup.Enabled = false;
        this.ddlSection.Enabled = false;
        this.ddlShift.Enabled = false;
        this.ddlRollNo.Enabled = false;
        this.ddlRegNo.Enabled = false;
        studentInfoDiv.Visible = false;
    }
    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.LoadDropdown(ddlClass, "bs_ClassName", 1, 0);

        this.ddlClass.SelectedIndex = -1;
        this.ddlGroup.SelectedIndex = -1;
        this.ddlSection.SelectedIndex = -1;
        this.ddlShift.SelectedIndex = -1;
        this.ddlRollNo.SelectedIndex = -1;
        this.ddlRegNo.SelectedIndex = -1;

        this.ddlGroup.Enabled = false;
        this.ddlSection.Enabled = false;
        this.ddlShift.Enabled = false;
        this.ddlRollNo.Enabled = false;
        this.ddlRegNo.Enabled = false;
        studentInfoDiv.Visible = false;
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.LoadDropdown(ddlGroup, "bs_Group", 1, 0);
        
        this.ddlGroup.SelectedIndex = -1;
        this.ddlSection.SelectedIndex = -1;
        this.ddlShift.SelectedIndex = -1;
        this.ddlRollNo.SelectedIndex = -1;
        this.ddlRegNo.SelectedIndex = -1;

        this.ddlGroup.Enabled = true;
        this.ddlSection.Enabled = false;
        this.ddlShift.Enabled = false;
        this.ddlRollNo.Enabled = false;
        this.ddlRegNo.Enabled = false;
        studentInfoDiv.Visible = false;
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.LoadDropdown(ddlShift, "bs_Shift", 1, 0);
        
        this.ddlShift.SelectedIndex = -1;
        this.ddlSection.SelectedIndex = -1;
        this.ddlRollNo.SelectedIndex = -1;
        this.ddlRegNo.SelectedIndex = -1;

        this.ddlShift.Enabled = true;
        this.ddlSection.Enabled = false;
        this.ddlRollNo.Enabled = false;
        this.ddlRegNo.Enabled = false;
        studentInfoDiv.Visible = false;
    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.LoadDropdown(ddlSection, "bs_Section", 1, 0);

        this.ddlSection.SelectedIndex = -1;
        this.ddlRollNo.SelectedIndex = -1;
        this.ddlRegNo.SelectedIndex = -1;

        this.ddlSection.Enabled = true;
        this.ddlRollNo.Enabled = false;
        this.ddlRegNo.Enabled = false;
        studentInfoDiv.Visible = false;
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClass.SelectedItem.Text == "---Select---" || ddlGroup.SelectedItem.Text == "---Select---" || ddlShift.SelectedItem.Text == "---Select---" || ddlSection.SelectedItem.Text == "---Select---")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please Select data from list');", true);
        }
        else
        {
            Common.DropdownByConditionMulti(ddlRollNo, "vwStudentRoll", "CampusId", "MediumId", "ClassId", "ShiftId", "SectionId", "GroupId", "Year", "RollNo", ddlCampus.SelectedItem.Value, ddlMedium.SelectedItem.Value, ddlClass.SelectedItem.Value, ddlShift.SelectedItem.Value, ddlSection.SelectedItem.Value, ddlGroup.SelectedItem.Value, ddlYear.SelectedItem.Value, 1, 0);
            Common.DropdownByConditionMulti(ddlRegNo, "vwStudentRoll", "CampusId", "MediumId", "ClassId", "ShiftId", "SectionId", "GroupId", "Year", "RegNo", ddlCampus.SelectedItem.Value, ddlMedium.SelectedItem.Value, ddlClass.SelectedItem.Value, ddlShift.SelectedItem.Value, ddlSection.SelectedItem.Value, ddlGroup.SelectedItem.Value, ddlYear.SelectedItem.Value, 10, 0,true);
        }
        studentInfoDiv.Visible = false;
        if (ddlRollNo.Visible)
        {
            this.ddlRollNo.SelectedIndex = -1;
            this.ddlRegNo.SelectedIndex = -1;
            this.ddlRollNo.Enabled = true;
            this.ddlRegNo.Enabled = true;
        }
        else
        {
            this.ddlRegNo.SelectedIndex = -1;
            this.ddlRegNo.Enabled = true;
        }

    }

    protected void ddlRollNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        studentInfoDiv.Visible = true;
        LoadDropdownData();
        LoadProfile();
    }

    protected void ddlRegNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        studentInfoDiv.Visible = true;
        LoadDropdownData();
        LoadProfile();
    }
    #endregion

    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Year=" + ddlYear.SelectedItem.Text;
            else
                criteria += " Year=" + ddlYear.SelectedItem.Text;
        }

        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ClassId=" + ddlClass.SelectedValue;
            else
                criteria += " and ClassId=" + ddlClass.SelectedValue;
        }
        if (ddlGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and GroupId=" + ddlGroup.SelectedValue;
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ShiftId=" + ddlShift.SelectedValue;
            else
                criteria += " and ShiftId=" + ddlShift.SelectedValue;
        }
        if (ddlSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "SectionId=" + ddlSection.SelectedValue;
            else
                criteria += " and SectionId=" + ddlSection.SelectedValue;
        }
        return criteria;
    }

   

    #region Variable
    string RegNo
    {
        set { ViewState["RegNo"] = value; }
        get
        {
            try
            {
                return ViewState["RegNo"].ToString();
            }
            catch
            {
                return null;
            }
        }
    }
    int ID
    {
        set { ViewState["ID"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["ID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int StudentToClassId
    {
        set { ViewState["StudentToClassId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["StudentToClassId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    string IsRollNoUse
    {
        set { ViewState["StudentId"] = value; }
        get
        {
            try
            {
                return ViewState["StudentId"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    #endregion

    #region Load Profile

    protected void LoadPerson()
    {
        int personId = 0;
        if (ddlRollNo.Visible)
        {
            if (this.ddlRollNo.SelectedIndex > -1)
            {
                var id = ddlRollNo.SelectedItem.Value;
                personId = Convert.ToInt32(id);
            }
        }
        else
        {
            var id = ddlRegNo.SelectedItem.Value;
            personId = Convert.ToInt32(id);
        }

        DataTable dt = objPerson.GetStudentByPersonId(personId);

        if (dt.Rows.Count > 0)
        {
            #region Load Label
            hdnPersonId.Value = dt.Rows[0]["Id"].ToString();
            hdnPersonImage.Value = dt.Rows[0]["PersonImage"].ToString();
            hdnFatherImage.Value = dt.Rows[0]["FatherImage"].ToString();
            hdnMotherImage.Value = dt.Rows[0]["MotherImage"].ToString();

            imgPerson.ImageUrl = "../../Images/Student/" + dt.Rows[0]["PersonImage"].ToString();
            imgFather.ImageUrl = "../../Images/Father/" + dt.Rows[0]["FatherImage"].ToString();
            imgMother.ImageUrl = "../../Images/Mother/" + dt.Rows[0]["MotherImage"].ToString();

            lblNameEng.Text = dt.Rows[0]["NameEng"].ToString();
            lblNameBan.Text = dt.Rows[0]["NameBan"].ToString();
            lblGender.Text = dt.Rows[0]["Gender"].ToString();
            lblBlood.Text = dt.Rows[0]["BloodGroupName"].ToString();
            lblBirthCertificate.Text = dt.Rows[0]["BirthCertificate"].ToString();
            lblReligion.Text = dt.Rows[0]["Religion"].ToString();
            if (dt.Rows[0]["DateofBirth"].ToString() != "")
                lblDoB.Text = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString()).ToString("dd-MMM-yyyy");
            lblPhn.Text = dt.Rows[0]["PhoneNo"].ToString();
            lblMob.Text = dt.Rows[0]["Mobile"].ToString();
            lblPhnHome.Text = dt.Rows[0]["PhoneHome"].ToString();
            lblMobHome.Text = dt.Rows[0]["MobileHome"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();

            lblPresentDiv.Text = dt.Rows[0]["presentDivName"].ToString();
            lblPresentDis.Text = dt.Rows[0]["presentDisName"].ToString();
            lblPresentThana.Text = dt.Rows[0]["presentThanaName"].ToString();
            lblPresentPO.Text = dt.Rows[0]["presentPO"].ToString();
            lblPresentPC.Text = dt.Rows[0]["presentPC"].ToString();
            lblPresentAddress.Text = dt.Rows[0]["presentAddress"].ToString();

            lblPermanentDiv.Text = dt.Rows[0]["DivName"].ToString();
            lblPermanentDis.Text = dt.Rows[0]["DisName"].ToString();
            lblPermanentThana.Text = dt.Rows[0]["Thana"].ToString();
            lblPermanentPO.Text = dt.Rows[0]["PostOffice"].ToString();
            lblPermanentPC.Text = dt.Rows[0]["PostalCode"].ToString();
            lblPermanentAddress.Text = dt.Rows[0]["Address"].ToString();

            lblFatherName.Text = dt.Rows[0]["FatherNameEng"].ToString();
            lblFatherEdu.Text = dt.Rows[0]["FatherEdu"].ToString();
            lblFatherPro.Text = dt.Rows[0]["FatherProfession"].ToString();
            lblFatherNId.Text = dt.Rows[0]["FatherNId"].ToString();
            lblFatherIncome.Text = dt.Rows[0]["FatherIncome"].ToString();
            lblFatherPhn.Text = dt.Rows[0]["FatherPhone"].ToString();

            lblMotherEdu.Text = dt.Rows[0]["MotherEdu"].ToString();
            lblMotherPro.Text = dt.Rows[0]["MotherProfession"].ToString();
            lblMotherName.Text = dt.Rows[0]["MotherNameEng"].ToString();
            lblMotherNId.Text = dt.Rows[0]["MotherNId"].ToString();
            lblMotherIncome.Text = dt.Rows[0]["MotherIncome"].ToString();
            lblMotherPhn.Text = dt.Rows[0]["MotherPhone"].ToString();
            #endregion
        }
    }
    protected void LoadProfile()
    {
        var id = "";
        if (ddlRollNo.Visible)
            id = ddlRollNo.SelectedItem.Value;
        else
            id = ddlRegNo.SelectedItem.Value;
            int personId = Convert.ToInt32(id);
        
        DataTable dt = objPerson.GetStudentByPersonId(personId);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (item["AcademicYear"].ToString() == ddlYear.SelectedValue)
                {
                    
                }
            }
            #region Load Label
            hdnPersonId.Value = dt.Rows[0]["Id"].ToString();
            StudentToClassId = Convert.ToInt32(dt.Rows[0]["StudentToClassId"].ToString());
            hdnPersonImage.Value = dt.Rows[0]["PersonImage"].ToString();
            hdnFatherImage.Value = dt.Rows[0]["FatherImage"].ToString();
            hdnMotherImage.Value = dt.Rows[0]["MotherImage"].ToString();
            hdnStudentId.Value = dt.Rows[0]["StudentId"].ToString();

            imgPerson.ImageUrl = "../../Images/Student/" + dt.Rows[0]["PersonImage"].ToString();
            imgFather.ImageUrl = "../../Images/Father/" + dt.Rows[0]["FatherImage"].ToString();
            imgMother.ImageUrl = "../../Images/Mother/" + dt.Rows[0]["MotherImage"].ToString();

            lblYear.Text = dt.Rows[0]["AcademicYear"].ToString();
            lblMedium.Text = dt.Rows[0]["Medium"].ToString();
            lblCampus.Text = dt.Rows[0]["Campus"].ToString();
            lblClass.Text = dt.Rows[0]["Class"].ToString();
            lblGroup.Text = dt.Rows[0]["stuGroup"].ToString();
            lblShift.Text = dt.Rows[0]["stuShift"].ToString();
            lblSection.Text = dt.Rows[0]["Section"].ToString();
            lblRoll.Text = dt.Rows[0]["RegNo"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();

            lblRegNo.Text = dt.Rows[0]["RegNo"].ToString();
            lblNameEng.Text = dt.Rows[0]["NameEng"].ToString();
            lblNameBan.Text = dt.Rows[0]["NameBan"].ToString();
            lblGender.Text = dt.Rows[0]["Gender"].ToString();
            lblBlood.Text = dt.Rows[0]["BloodGroupName"].ToString();
            lblBirthCertificate.Text = dt.Rows[0]["BirthCertificate"].ToString();
            lblReligion.Text = dt.Rows[0]["Religion"].ToString();
            if (dt.Rows[0]["DateofBirth"].ToString() != "")
                lblDoB.Text = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString()).ToString("dd-MMM-yyyy");

            lblPhn.Text = dt.Rows[0]["PhoneNo"].ToString();
            lblMob.Text = dt.Rows[0]["Mobile"].ToString();
            lblPhnHome.Text = dt.Rows[0]["PhoneHome"].ToString();
            lblMobHome.Text = dt.Rows[0]["MobileHome"].ToString();

            lblPresentDiv.Text = dt.Rows[0]["presentDivName"].ToString();
            lblPresentDis.Text = dt.Rows[0]["presentDisName"].ToString();
            lblPresentThana.Text = dt.Rows[0]["presentThanaName"].ToString();
            lblPresentPO.Text = dt.Rows[0]["presentPO"].ToString();
            lblPresentPC.Text = dt.Rows[0]["presentPC"].ToString();
            lblPresentAddress.Text = dt.Rows[0]["presentAddress"].ToString();

            lblPermanentDiv.Text = dt.Rows[0]["DivName"].ToString();
            lblPermanentDis.Text = dt.Rows[0]["DisName"].ToString();
            lblPermanentThana.Text = dt.Rows[0]["Thana"].ToString();
            lblPermanentPO.Text = dt.Rows[0]["PostOffice"].ToString();
            lblPermanentPC.Text = dt.Rows[0]["PostalCode"].ToString();
            lblPermanentAddress.Text = dt.Rows[0]["Address"].ToString();

            lblFatherName.Text = dt.Rows[0]["FatherNameEng"].ToString();
            lblFatherEdu.Text = dt.Rows[0]["FatherEdu"].ToString();
            lblFatherPro.Text = dt.Rows[0]["FatherProfession"].ToString();
            lblFatherNId.Text = dt.Rows[0]["FatherNId"].ToString();
            lblFatherIncome.Text = dt.Rows[0]["FatherIncome"].ToString();
            lblFatherPhn.Text = dt.Rows[0]["FatherPhone"].ToString();

            lblMotherEdu.Text = dt.Rows[0]["MotherEdu"].ToString();
            lblMotherPro.Text = dt.Rows[0]["MotherProfession"].ToString();
            lblMotherName.Text = dt.Rows[0]["MotherNameEng"].ToString();
            lblMotherNId.Text = dt.Rows[0]["MotherNId"].ToString();
            lblMotherIncome.Text = dt.Rows[0]["MotherIncome"].ToString();
            lblMotherPhn.Text = dt.Rows[0]["MotherPhone"].ToString();

            lblLocalGuardian1.Text = dt.Rows[0]["NameLocalGuardian1"].ToString();
            lblLocalG1MobileNo.Text = dt.Rows[0]["LocalGuardian1Mobile"].ToString();
            lblLocalGuardian2.Text = dt.Rows[0]["NameLocalGuardian2"].ToString();
            lblLocalG2MobileNo.Text = dt.Rows[0]["LocalGuardian2Mobile"].ToString();

            #endregion

            #region Load Textbox
            //ddlPromotionYear.SelectedValue = dt.Rows[0]["Year"].ToString();
            //ddlPromotionMedium.SelectedValue = dt.Rows[0]["MediumId"].ToString();
            //ddlPromotionCampus.SelectedValue = dt.Rows[0]["CampusId"].ToString();
            //ddlPromotionClass.SelectedValue = dt.Rows[0]["ClassId"].ToString();
            //ddlPromotionGroup.SelectedValue = dt.Rows[0]["GroupId"].ToString();
            //ddlPromotionShift.SelectedValue = dt.Rows[0]["ShiftId"].ToString();
            //ddlPromotionSection.SelectedValue = dt.Rows[0]["SectionId"].ToString();
            //tbxRoll.Text = dt.Rows[0]["RegNo"].ToString();

            tbxRegNo.Text = dt.Rows[0]["RegNo"].ToString();
            tbxNameEng.Text = dt.Rows[0]["NameEng"].ToString();
            tbxNameBan.Text = dt.Rows[0]["NameBan"].ToString();
            ddlGender.SelectedItem.Value = dt.Rows[0]["GenderId"].ToString();
            ddlReligion.SelectedValue = dt.Rows[0]["ReligionId"].ToString();
            if (dt.Rows[0]["DateofBirth"].ToString() != "")
                tbxDateOfBirth.Text = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString()).ToString("dd/MM/yyyy");
            tbxBirthCertificate.Text = dt.Rows[0]["BirthCertificate"].ToString();
            tbxPhone.Text = dt.Rows[0]["PhoneNo"].ToString();
            tbxMobile.Text = dt.Rows[0]["Mobile"].ToString();
            tbxPhnHome.Text = dt.Rows[0]["PhoneHome"].ToString();
            tbxMobHome.Text = dt.Rows[0]["MobileHome"].ToString();
            ddlBlood.SelectedValue = dt.Rows[0]["BloodGroup"].ToString();

            if (string.IsNullOrEmpty(ddlPermanentDiv.SelectedValue))
            {
                ddlPermanentDiv.SelectedValue = dt.Rows[0]["DivisionId"].ToString();
            }
            if (string.IsNullOrEmpty(ddlPermanentDis.SelectedValue))
            {
                ddlPermanentDis.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            }
            if (string.IsNullOrEmpty(ddlPermanentThana.SelectedValue))
            {
                ddlPermanentThana.SelectedValue = dt.Rows[0]["ThanaId"].ToString();
            }
            tbxPermanentPost.Text = dt.Rows[0]["PostOffice"].ToString();
            tbxPermanentPostCode.Text = dt.Rows[0]["PostalCode"].ToString();
            tbxPermanentAddress.Text = dt.Rows[0]["Address"].ToString();

            if (string.IsNullOrEmpty(ddlPresentDiv.SelectedValue))
            {
                ddlPresentDiv.SelectedItem.Value = dt.Rows[0]["presentDiv"].ToString();
            }

            if (!string.IsNullOrEmpty(ddlPresentDiv.SelectedValue))
            {
                DataTable dtf = new dalDistrict().GetByDivisionId(Convert.ToInt32(ddlPresentDiv.SelectedValue));
                ddlPresentDis.DataSource = dtf;
                ddlPresentDis.DataBind();
            }

            if (string.IsNullOrEmpty(ddlPresentDis.SelectedValue))
            {
                ddlPresentDis.SelectedItem.Value = dt.Rows[0]["presentDis"].ToString();
            }

            if (!string.IsNullOrEmpty(ddlPresentDis.SelectedValue))
            {
                DataTable dtthana = new dalThana().GetByDistrictId(Convert.ToInt32(ddlPresentDis.SelectedValue));
                ddlPresentThana.DataSource = dtthana;
                ddlPresentThana.DataBind();
            }

            if (string.IsNullOrEmpty(ddlPresentThana.SelectedValue))
            {
                ddlPresentThana.SelectedValue = dt.Rows[0]["presentThana"].ToString();
            }
            tbxPostOffice.Text = dt.Rows[0]["presentPO"].ToString();
            tbxPostalCode.Text = dt.Rows[0]["presentPC"].ToString();
            tbxPresentAddress.Text = dt.Rows[0]["presentAddress"].ToString();

            tbxFathername.Text = dt.Rows[0]["FatherNameEng"].ToString();
            ddlFatherEdu.SelectedValue = dt.Rows[0]["FatherEduId"].ToString();
            ddlFatherPro.SelectedValue = dt.Rows[0]["FatherProId"].ToString();
            tbxFatherNID.Text = dt.Rows[0]["FatherNId"].ToString();
            tbxFatherIncome.Text = dt.Rows[0]["FatherIncome"].ToString();
            tbxFatherPhn.Text = dt.Rows[0]["FatherPhone"].ToString();

            ddlMotherEdu.SelectedValue = dt.Rows[0]["MotherEduId"].ToString();
            ddlMotherPro.SelectedValue = dt.Rows[0]["MotherProfessionId"].ToString();
            tbxMotherName.Text = dt.Rows[0]["MotherNameEng"].ToString();
            tbxMotherNID.Text = dt.Rows[0]["MotherNId"].ToString();
            tbxMotherIncome.Text = dt.Rows[0]["MotherIncome"].ToString();
            tbxMotherPhn.Text = dt.Rows[0]["MotherPhone"].ToString();

            tbxLocalGuardian1.Text = dt.Rows[0]["NameLocalGuardian1"].ToString();
            tbxLocG1MobileNo.Text = dt.Rows[0]["LocalGuardian1Mobile"].ToString();
            tbxLocalGuardian2.Text = dt.Rows[0]["NameLocalGuardian2"].ToString();
            tbxLocG2MobileNo.Text = dt.Rows[0]["LocalGuardian2Mobile"].ToString();


            #endregion

            #region Load CheckBox
            chkFreedom.Checked = Convert.ToBoolean(dt.Rows[0]["IsFreedomFighter"].ToString());
            chkPhyDef.Checked = Convert.ToBoolean(dt.Rows[0]["IsPhysicallyDefect"].ToString());
            chkTribal.Checked = Convert.ToBoolean(dt.Rows[0]["IsTribal"].ToString());
            #endregion
        }


    }
    #endregion

    
    protected void CheckRollNoUse()
    {
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            IsRollNoUse = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
        }
        if (IsRollNoUse == "False")
        {
            LabelRoll.Visible = false;
            ddlRollNo.Visible = false;
        }
        else
        {
            LabelReg.Visible = false;
            ddlRegNo.Visible = false;
        }
    }
    protected void btnAcademicUpdate_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        try
        {
            int ID = objPerson.UpdateStudentShip(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(hdnStudentId.Value));
            LoadPerson();
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Confirmation, Page);
        }
        catch (Exception)
        {

            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }

    //    int ID = objPerson.AcademicInfoUpdate(StudentToClassId, ddlPromotionYear.SelectedValue, Convert.ToInt32(ddlPromotionMedium.SelectedValue),
    //                        Convert.ToInt32(ddlPromotionCampus.SelectedValue),
    //                        Convert.ToInt32(ddlPromotionClass.SelectedValue),
    //                        Convert.ToInt32(ddlPromotionGroup.SelectedValue),
    //                        Convert.ToInt32(ddlPromotionShift.SelectedValue),
    //                        Convert.ToInt32(ddlPromotionSection.SelectedValue), tbxRoll.Text);
       
        //if (ID != -1)
        //{
            
        //}
        //else
        //    MessageController.Show("This Roll already exists. Please try anothor.", MessageType.Error, Page);
    }
    protected void btnPersonal_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        int pId = Convert.ToInt32(this.hdnPersonId.Value);
        string email = "";
        string fax = "";
        string NId = "";
        if (!Page.User.IsInRole("Student"))
        {
            email = "email.com";
            fax = "";
            NId = "";
        }
        objPerson.Update(pId, tbxNameEng.Text, tbxNameBan.Text, Convert.ToInt32(ddlGender.SelectedValue), Convert.ToInt32(ddlReligion.SelectedValue), NId, DateTime.ParseExact(tbxDateOfBirth.Text, "dd/MM/yyyy", null), tbxBirthCertificate.Text,
            tbxPhone.Text, tbxMobile.Text, tbxPhnHome.Text, tbxMobHome.Text, email, fax, Convert.ToInt32(ddlBlood.SelectedValue));

        LoadPerson();
        

    }
    protected bool ValidImage(FileUpload file)
    {
        bool flag = false;
        string exe = Path.GetExtension(file.FileName).ToLower();
        if (exe == "jpeg" || exe == ".jpg" || exe == ".png")
        {
            if (file.PostedFile.ContentLength < 6218595)
            {
                flag = true;
            }
        }
        return flag;
    }
    protected void btnPersoImage_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        string imgPer = "";
        if (hdnPersonId.Value != "")
        {
            Controller.DeleteFile(Server.MapPath("~/Images/Student/") + hdnPersonImage.Value);
            if (uploderStudent.HasFile)
            {
                if (ValidImage(uploderStudent) == true)
                {
                    imgPer = Guid.NewGuid() + "-" + uploderStudent.FileName;
                    System.Drawing.Image image = System.Drawing.Image.FromStream(uploderStudent.FileContent);
                    System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                    string MediumImagePath = Server.MapPath("~/Images/Student/" + imgPer);
                    image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
                }
            }
            objPerson.PersonImageUpdate(Convert.ToInt32(hdnPersonId.Value), imgPer);
            imgPerson.ImageUrl = "../../Images/Student/" + imgPer;
        }
    }
    protected void btnFatherImage_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        string fatherPhoto = "";
        if (hdnPersonId.Value != "")
        {
            Controller.DeleteFile(Server.MapPath("~/Image/Father/") + hdnFatherImage.Value);
            if (upFather.HasFile)
            {
                if (ValidImage(upFather) == true)
                {
                    fatherPhoto = Guid.NewGuid() + "-" + upFather.FileName;
                    System.Drawing.Image image = System.Drawing.Image.FromStream(upFather.FileContent);
                    System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                    string MediumImagePath = Server.MapPath("~/Images/Father/" + fatherPhoto);
                    image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
                }
            }
            objPerson.FatherImageUpdate(Convert.ToInt32(hdnPersonId.Value), fatherPhoto);
            imgFather.ImageUrl = "../../Images/Father/" + fatherPhoto;
        }
    }
    protected void btnFatherInfo_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        int pId = Convert.ToInt32(this.hdnPersonId.Value);
        int income = 0;
        try
        {
            if (tbxFatherIncome.Text != "")
                income = Convert.ToInt32(tbxFatherIncome.Text);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Income Amount is too large');", true);

        }
        objPerson.UpdateFatherInfo(pId, tbxFathername.Text, "", tbxFatherNID.Text, income, tbxFatherPhn.Text, Convert.ToInt32(ddlFatherEdu.SelectedValue), Convert.ToInt32(ddlFatherPro.SelectedValue));

        LoadPerson();
    }
    protected void btnMother_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        string motherPhoto = "";
        if (hdnPersonId.Value != "")
        {
            Controller.DeleteFile(Server.MapPath("~/Image/Mother/") + hdnMotherImage.Value);
            if (upMother.HasFile)
            {
                if (ValidImage(upMother) == true)
                {
                    motherPhoto = Guid.NewGuid() + "-" + upMother.FileName;
                    System.Drawing.Image image = System.Drawing.Image.FromStream(upMother.FileContent);
                    System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                    string MediumImagePath = Server.MapPath("~/Images/Mother/" + motherPhoto);
                    image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
                }
            }
            objPerson.MotherImageUpdate(Convert.ToInt32(hdnPersonId.Value), motherPhoto);
            imgMother.ImageUrl = "../../Images/Mother/" + motherPhoto;
        }
    }
    protected void btnMotherInfo_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        int pId = Convert.ToInt32(this.hdnPersonId.Value);
        int income = 0;
        try
        {
            if (tbxMotherIncome.Text != "")
                income = Convert.ToInt32(tbxMotherIncome.Text);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Income Amount is too large');", true);

        }
        objPerson.UpdateMotherInfo(pId, tbxMotherName.Text, "", tbxMotherNID.Text, income, tbxMotherPhn.Text, Convert.ToInt32(ddlMotherEdu.SelectedValue),
            Convert.ToInt32(ddlMotherPro.SelectedValue));

        LoadPerson();
    }
    protected void btnPresentAddress_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        int pId = Convert.ToInt32(this.hdnPersonId.Value);
        string selectedDis = Request.Form[ddlPresentDis.UniqueID];
        string selectedThana = Request.Form[ddlPresentThana.UniqueID];
        objPerson.PresentAddressUpdate(pId, Convert.ToInt32(ddlPresentDiv.SelectedValue), Convert.ToInt32(selectedDis), Convert.ToInt32(selectedThana),
            tbxPostOffice.Text, tbxPostalCode.Text, tbxPresentAddress.Text);

        LoadPerson();
    }
    protected void btnPermanentAddress_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        int pId = Convert.ToInt32(this.hdnPersonId.Value);
        string selectedDis = Request.Form[ddlPermanentDis.UniqueID];
        string selectedThana = Request.Form[ddlPermanentThana.UniqueID];
        objPerson.PermanentAddressUpdate(pId, Convert.ToInt32(ddlPermanentDiv.SelectedValue), Convert.ToInt32(selectedDis), Convert.ToInt32(selectedThana),
            tbxPermanentPost.Text, tbxPermanentPostCode.Text, tbxPermanentAddress.Text);

        LoadPerson();
    }
    protected void btnLocalGuardianInfo_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        int pId = Convert.ToInt32(this.hdnPersonId.Value);
        objPerson.LocalGuardianUpdate(pId, tbxLocalGuardian1.Text, tbxLocalGuardian2.Text, tbxLocG1MobileNo.Text,
            tbxLocG2MobileNo.Text);
        tbxLocalGuardian1.Text = string.Empty;
        tbxLocalGuardian2.Text = string.Empty;
        tbxLocG1MobileNo.Text = string.Empty;
        tbxLocG2MobileNo.Text = string.Empty;
        LoadProfile();
    }
    protected void btnOtherInfo_Click(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        int pId = Convert.ToInt32(this.hdnPersonId.Value);
        bool isFreedomFighter = Convert.ToBoolean(CheckBox1.Checked);
        bool isTribal = Convert.ToBoolean(CheckBox2.Checked);
        bool isPhysicallyDefect = Convert.ToBoolean(CheckBox3.Checked);

        objPerson.OtherInfoUpdate(pId, isFreedomFighter, isTribal, isPhysicallyDefect);
        LoadProfile();
        //Response.Redirect("~/Pages/User/Profile.aspx");
    }
}

