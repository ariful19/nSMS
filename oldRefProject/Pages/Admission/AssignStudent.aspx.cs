using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admission_AssignStudent : BasePage
{
    public static int count = 0;
    dalPerson objPerson = new dalPerson();
    dalStudent objStudent = new dalStudent();
    dalClass objClass = new dalClass();

    protected int cls;
    public static int PersonId;
    public static int StudentId;
    int fIncome;
    int mIncome;
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            string ss = Common.SessionInfo.Panel;
            LoadDropdown();
            LoadAssignment();
            LoadDefault();
            Session["Password"] = null;
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
    protected void LoadDefault()
    {
        //imgStudent.ImageUrl = "~/Images/Common/student.png";
        //imgFather.ImageUrl = "~/Images/Common/father.png";
        //imgMother.ImageUrl = "~/Images/Common/mother.png";
    }

    #region Load All Dropdown
    protected void LoadDropdown()
    {
        ddlGender.DataSource = new Common().GetAll("bs_Gender");
        ddlGender.DataBind();

        ddlReligion.DataSource = new Common().GetAll("bs_Religion");
        ddlReligion.DataBind();

        ddlBlood.DataSource = new Common().GetAll("bs_BloodGroup");
        ddlBlood.DataBind();

        Common.DropdownByCondition(ddlserialno, "tblStudentAdmission", "IsPayment", "1", 1, 0);

    }

    #endregion
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

    #region index change

    protected void ddlserialno_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProfile();
    }

    #endregion

    #region Student Assign
    protected void LoadAssignment()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
        ddlYear.DataBind();

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

    }
    #endregion
    protected bool ValidImage(FileUpload file)
    {
        bool flag = false;
        string extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == "jpeg" || extension == ".jpg" || extension == ".png")
        {
            if (file.PostedFile.ContentLength < 6218595)
            {
                flag = true;
            }
        }
        return flag;
    }
    private Person GetPersonData()
    {
        var id = this.hdfStudentAdmissionID.Value;
        int admissionId = Convert.ToInt32(id);

        DataTable dt = objStudent.GetStuByAdmisssionId(admissionId);

        Person person = new Person();

        if (dt.Rows.Count > 0)
        {
          //  DateTime dateOfBirth = DateTime.ParseExact(tbxDateOfBirth.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);

            string s = string.IsNullOrEmpty(dt.Rows[0]["SurName"].ToString()) ? "" : dt.Rows[0]["SurName"].ToString();
            string f = string.IsNullOrEmpty(dt.Rows[0]["FirstName"].ToString()) ? "" : dt.Rows[0]["FirstName"].ToString();

            person.NameEng = s +""+ f;
            
            person.NameBan = string.IsNullOrEmpty(dt.Rows[0]["NameBanlga"].ToString()) ? "" : dt.Rows[0]["NameBanlga"].ToString();
            
            person.FatherNameEng = string.IsNullOrEmpty(dt.Rows[0]["FatherNameEng"].ToString()) ? "" : dt.Rows[0]["FatherNameEng"].ToString();
            
            person.MotherNameEng = string.IsNullOrEmpty(dt.Rows[0]["MotherNameEng"].ToString()) ? "" : dt.Rows[0]["MotherNameEng"].ToString();
            
            person.GenderId = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["GenderId"].ToString()) ? "" : dt.Rows[0]["GenderId"].ToString());
            
            person.ReligionId = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["ReligionId"].ToString()) ? "" : dt.Rows[0]["ReligionId"].ToString());
           // person.DateofBirth = dateOfBirth.ToString("yyyy/MM/dd");
            
            person.DateofBirth = string.IsNullOrEmpty(dt.Rows[0]["DateofBirth"].ToString()) ? "" : dt.Rows[0]["DateofBirth"].ToString();
            
            person.BirthCertificate = string.IsNullOrEmpty(dt.Rows[0]["BirthCertificate"].ToString()) ? "" : dt.Rows[0]["BirthCertificate"].ToString();
            
            person.PhoneNo = string.IsNullOrEmpty(dt.Rows[0]["PhoneNo"].ToString()) ? "" : dt.Rows[0]["PhoneNo"].ToString();
            
            person.Mobile = tbxMobile.Text;
            
            person.PhoneHome = string.IsNullOrEmpty(dt.Rows[0]["PhoneHome"].ToString()) ? "" : dt.Rows[0]["PhoneHome"].ToString();
            
            person.MobileHome = string.IsNullOrEmpty(dt.Rows[0]["MobileHome"].ToString()) ? "" : dt.Rows[0]["MobileHome"].ToString();
            
            person.BloodGroup = string.IsNullOrEmpty(dt.Rows[0]["BloodGroup"].ToString()) ? "" : dt.Rows[0]["BloodGroup"].ToString();
           
            person.FatherNId = string.IsNullOrEmpty(dt.Rows[0]["FatherNId"].ToString()) ? "" : dt.Rows[0]["FatherNId"].ToString();
            
            person.MotherNId = string.IsNullOrEmpty(dt.Rows[0]["MotherNId"].ToString()) ? "" : dt.Rows[0]["MotherNId"].ToString();
            
            person.IsFreedomFighter = Convert.ToBoolean(string.IsNullOrEmpty(dt.Rows[0]["IsFreedomFighter"].ToString()) ? "0" : dt.Rows[0]["IsFreedomFighter"].ToString());
           
            person.IsTribal = Convert.ToBoolean(string.IsNullOrEmpty(dt.Rows[0]["IsTribal"].ToString()) ? "0" : dt.Rows[0]["IsTribal"].ToString());
            
            person.IsPhysicallyDefect = Convert.ToBoolean(string.IsNullOrEmpty(dt.Rows[0]["IsPhysicallyDefect"].ToString()) ? "0" : dt.Rows[0]["IsPhysicallyDefect"].ToString());
           
            person.FatherIncome = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["FatherIncome"].ToString()) ? "0" : dt.Rows[0]["FatherIncome"].ToString());
            
            person.MotherIncome = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["MotherIncome"].ToString()) ? "0" : dt.Rows[0]["MotherIncome"].ToString());
            
            person.FatherPhone = string.IsNullOrEmpty(dt.Rows[0]["FatherPhone"].ToString()) ? "" : dt.Rows[0]["FatherPhone"].ToString();
            
            person.MotherPhone = string.IsNullOrEmpty(dt.Rows[0]["MotherPhone"].ToString()) ? "" : dt.Rows[0]["MotherPhone"].ToString();

            person.FatherQualificationId = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["FatherQualificationId"].ToString()) ? "" : dt.Rows[0]["FatherQualificationId"].ToString());
            
            person.FatherProfessionId = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["FatherProfessionId"].ToString()) ? "" : dt.Rows[0]["FatherProfessionId"].ToString());
            
            person.MotherQualificationId = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["MotherQualificationId"].ToString()) ? "" : dt.Rows[0]["MotherQualificationId"].ToString());
            
            person.MotherProfessionId = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["MotherProfessionId"].ToString()) ? "" : dt.Rows[0]["MotherProfessionId"].ToString());

            person.FatherImage = string.IsNullOrEmpty(dt.Rows[0]["FatherImage"].ToString()) ? "" : dt.Rows[0]["FatherImage"].ToString();
            
            person.MotherImage = string.IsNullOrEmpty(dt.Rows[0]["MotherImage"].ToString()) ? "" : dt.Rows[0]["MotherImage"].ToString();
            
            person.PersonImage = string.IsNullOrEmpty(dt.Rows[0]["PersonImage"].ToString()) ? "" : dt.Rows[0]["PersonImage"].ToString();

            person.NameLocalGuardian1 = string.IsNullOrEmpty(dt.Rows[0]["NameLocalGuardian1"].ToString()) ? "" : dt.Rows[0]["NameLocalGuardian1"].ToString();

            person.NameLocalGuardian2 = string.IsNullOrEmpty(dt.Rows[0]["NameLocalGuardian2"].ToString()) ? "" : dt.Rows[0]["NameLocalGuardian2"].ToString();

            person.LocalGuardian1Mobile = string.IsNullOrEmpty(dt.Rows[0]["LocalGuardian1Mobile"].ToString()) ? "" : dt.Rows[0]["LocalGuardian1Mobile"].ToString();

            person.LocalGuardian2Mobile = string.IsNullOrEmpty(dt.Rows[0]["LocalGuardian2Mobile"].ToString()) ? "" : dt.Rows[0]["LocalGuardian2Mobile"].ToString();

            person.UserName = Request.Form[tbxRegNo.UniqueID];

          
        }
        return person;
    }

    protected void LoadProfile()
    {
        var id = ddlserialno.SelectedItem.Value;
        int admissionId = Convert.ToInt32(id);

        DataTable dt = objStudent.GetStuByAdmisssionId(admissionId);

        if (dt.Rows.Count > 0)
        {
            //txtClassAppling.Text = string.IsNullOrEmpty(dt.Rows[0]["ClassApplyingFor"].ToString()) ? "" : dt.Rows[0]["ClassApplyingFor"].ToString();

            cls = Convert.ToInt32(dt.Rows[0]["ClassApplyingFor"].ToString());

            DataTable dtClass = objClass.GetById(cls);
            if (dtClass.Rows.Count > 0)
            {
                txtClassAppling.Text = dtClass.Rows[0]["ClassName"].ToString();
            }
            #region Load Textbox

            string s = string.IsNullOrEmpty(dt.Rows[0]["SurName"].ToString()) ? "" : dt.Rows[0]["SurName"].ToString();
            string f = string.IsNullOrEmpty(dt.Rows[0]["FirstName"].ToString()) ? "" : dt.Rows[0]["FirstName"].ToString();

            hdfStudentAdmissionID.Value = dt.Rows[0]["StudentAdmissionID"].ToString();
           // tbxRegNo.Text = dt.Rows[0]["RegNo"].ToString();
            tbxNameEng.Text = s + ""+ f;
            tbxNameBan.Text = dt.Rows[0]["NameBanlga"].ToString();
            ddlGender.SelectedItem.Value = dt.Rows[0]["GenderId"].ToString();
            ddlReligion.SelectedValue = dt.Rows[0]["ReligionId"].ToString();
            if (dt.Rows[0]["DateofBirth"].ToString() != "")
                tbxDateOfBirth.Text = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString()).ToString("dd/MM/yyyy");
            tbxBirthCertificate.Text = dt.Rows[0]["BirthCertificate"].ToString();
            tbxPhone.Text = string.IsNullOrEmpty(dt.Rows[0]["PhoneNo"].ToString()) ? "" : dt.Rows[0]["PhoneNo"].ToString();
            tbxMobile.Text = string.IsNullOrEmpty(dt.Rows[0]["Mobile"].ToString()) ? "" : dt.Rows[0]["Mobile"].ToString();
            tbxPhnHome.Text = string.IsNullOrEmpty(dt.Rows[0]["PhoneHome"].ToString()) ? "" : dt.Rows[0]["PhoneHome"].ToString();
            tbxMobHome.Text = string.IsNullOrEmpty(dt.Rows[0]["MobileHome"].ToString()) ? "" : dt.Rows[0]["MobileHome"].ToString();
            ddlBlood.SelectedValue = string.IsNullOrEmpty(dt.Rows[0]["BloodGroup"].ToString()) ? "" : dt.Rows[0]["BloodGroup"].ToString();


           

            
            #endregion
        }


    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    Regex regex = new Regex(@"[1-9]");
    private bool IsInteger(string str)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (!regex.IsMatch(str))
            {
                return false;
            }

            return true;

        }
        catch (Exception ex)
        {
            MessageController.Show("Roll No. Must be Neumeric!!!", MessageType.Warning, Page);
        }
        return false;
    }

    protected void tbxRoll_TextChanged(object sender, EventArgs e)
    {
        
            if (!string.IsNullOrEmpty(tbxRoll.Text))
            {
               // objStudent.GetRoll(tbxRoll.Text);
            }
        
    }

    public bool ValidationCheck()
    {
            if (tbxRoll.Text == "")
            {
                MessageController.Show("Roll No Must be Needed!!!", MessageType.Warning, Page);
                return true;
            }
        
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidationCheck())
        {
            return;
        }
       
        string imgStudent = "";
        string imgFather = "";
        string imgMother = "";
        string password = Request.Form[tbxPassword.UniqueID];

        Person person = new Person();
        person = GetPersonData();

        MembershipCreateStatus msc;

        //Set images
        //person.FatherImage = imgFather;
        //person.MotherImage = imgMother;
        //person.PersonImage = imgStudent;

        int studentRoll = (Convert.ToInt32(string.IsNullOrEmpty(tbxRoll.Text) ? "0" : tbxRoll.Text));
        PersonId = objPerson.Insert(person);
        if (PersonId != -1)
        {
        
        var id = ddlserialno.SelectedItem.Value;
        int admissionId = Convert.ToInt32(id);

        DataTable dt = objStudent.GetStuByAdmisssionId(admissionId);

        if (dt.Rows.Count > 0)
        {

            objPerson.PresentAddressInsert(PersonId, Convert.ToInt32(dt.Rows[0]["DivisionId"].ToString()), Convert.ToInt32(dt.Rows[0]["DistrictId"].ToString()), Convert.ToInt32(dt.Rows[0]["ThanaId"].ToString()), dt.Rows[0]["PostOffice"].ToString(), dt.Rows[0]["PostalCode"].ToString(), dt.Rows[0]["Address"].ToString());
            objPerson.PermanentAddressInsert(PersonId, Convert.ToInt32(dt.Rows[0]["DivisionId"].ToString()), Convert.ToInt32(dt.Rows[0]["DistrictId"].ToString()), Convert.ToInt32(dt.Rows[0]["ThanaId"].ToString()), dt.Rows[0]["PostOffice"].ToString(), dt.Rows[0]["PostalCode"].ToString(), dt.Rows[0]["Address"].ToString());
        }
        StudentId = objStudent.Insert(PersonId, Request.Form[tbxRegNo.UniqueID], DateTime.Now, DateTime.Now.Year.ToString(), Page.User.Identity.Name, DateTime.Now, true, 2, AdaborId, ZigatolaId);
            if (StudentId != -1)
            {
                Membership.CreateUser(Request.Form[tbxRegNo.UniqueID], password);
                Roles.AddUserToRole(Request.Form[tbxRegNo.UniqueID], Controller.RoleNameById(2));
                Controller.UpdateRegistrationNo();
                
                    int rollNumber = 0;
                    int.TryParse(tbxRoll.Text, out rollNumber);
                    //objStudent.StudentToClass(StudentId, ddlYear.SelectedValue,Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue),
                    //    Convert.ToInt32(ddlSection.SelectedValue), rollNumber);
              
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

                if (!string.IsNullOrEmpty(tbxMobile.Text))
                {
                    string messageBody = @"Dear " + tbxNameEng.Text + ", Welcome to our school. Your user id and password is user name: " + Request.Form[tbxRegNo.UniqueID] + " password: " + tbxPassword.Text;
                    new MessageGateway().SendSms(tbxMobile.Text, messageBody);
                }

                tbxRegNo.Text = Controller.RegistrationNo().ToString();
                tbxUserName.Text = Controller.RegistrationNo().ToString();
            }
            else
            {
                MessageController.Show("This Registration no. already exists. Please try another.", MessageType.Error, Page);
                return;
            }
        }
        else
        {
            MessageController.Show("This user name already exists. Please try another.", MessageType.Error, Page);
        }

    }

    
}
