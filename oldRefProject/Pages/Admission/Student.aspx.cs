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

public partial class Pages_Admission_Student : BasePage
{
    public static int count = 0;
    dalPerson objPerson = new dalPerson();
    dalStudent objStudent = new dalStudent();
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
            //CheckRegNo();
            //CheckMediumId();
            Session["Password"] = null;
        }
        GetRegUserNo();
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

    int CollegeScienceId
    {
        set { ViewState["CollegeScienceId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["CollegeScienceId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int CollegeBusinessId
    {
        set { ViewState["CollegeBusinessId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["CollegeBusinessId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int EmediumAdaborId
    {
        set { ViewState["EmediumAdaborId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["EmediumAdaborId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int EmediumZigatolaId
    {
        set { ViewState["EmediumZigatolaId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["EmediumZigatolaId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
  
protected void GetRegUserNo()
    {
        var res = "";
        AdaborId = 0;
        ZigatolaId = 0;
        CollegeScienceId = 0;
        CollegeBusinessId = 0;
        EmediumAdaborId = 0;
        EmediumZigatolaId = 0;
        var month = DateTime.Now.Month;
        if (ddlCampus.SelectedValue == "1")
        {

            if (ddlMedium.SelectedValue == "4")
            {

                EmediumAdaborId = Controller.AutoStudentIdforAdaborEnglishMedium();
                tbxRegNo.Text = "AE" + DateTime.Now.ToString("yy") + "-" + EmediumAdaborId.ToString();
                tbxUserName.Text = "AE" + DateTime.Now.ToString("yy") + "-" + EmediumAdaborId.ToString();

            }

            else
            {
                if (ddlClass.SelectedItem.Text == "College 1st Year" || ddlClass.SelectedItem.Text == "College 2nd Year")
                {
                    if (month >= 5)
                    {
                        if (ddlGroup.SelectedItem.Text == "Science")
                        {
                            CollegeScienceId = Controller.AutoCollegeScienceStudentId();
                            tbxRegNo.Text = "CS" + DateTime.Now.ToString("yy") + "-" + CollegeScienceId.ToString();
                            tbxUserName.Text = "CS" + DateTime.Now.ToString("yy") + "-" + CollegeScienceId.ToString();
                        }
                        else
                        {
                            CollegeBusinessId = Controller.AutoCollegeBusinessStudentId();
                            tbxRegNo.Text = "CB" + DateTime.Now.ToString("yy") + "-" + CollegeBusinessId.ToString();
                            tbxUserName.Text = "CB" + DateTime.Now.ToString("yy") + "-" + CollegeBusinessId.ToString();
                        }
                    }
                    else
                    {
                        if (ddlGroup.SelectedItem.Text == "Science")
                        {
                            CollegeScienceId = Controller.AutoCollegeScienceStudentId();
                            //tbxRegNo.Text = "CS" + DateTime.Now.AddYears(-1).ToString("yy") + "-" + CollegeScienceId.ToString();
                            //tbxUserName.Text = "CS" + DateTime.Now.AddYears(-1).ToString("yy") + "-" + CollegeScienceId.ToString();
                            tbxRegNo.Text = "CS" + DateTime.Now.ToString("yy") + "-" + CollegeScienceId.ToString();
                            tbxUserName.Text = "CS" + DateTime.Now.ToString("yy") + "-" + CollegeScienceId.ToString();
                        }
                        else
                        {
                            CollegeBusinessId = Controller.AutoCollegeBusinessStudentId();
                            //tbxRegNo.Text = "CB" + DateTime.Now.AddYears(-1).ToString("yy") + "-" + CollegeBusinessId.ToString();
                            //tbxUserName.Text = "CB" + DateTime.Now.AddYears(-1).ToString("yy") + "-" + CollegeBusinessId.ToString();
                            tbxRegNo.Text = "CB" + DateTime.Now.ToString("yy") + "-" + CollegeBusinessId.ToString();
                            tbxUserName.Text = "CB" + DateTime.Now.ToString("yy") + "-" + CollegeBusinessId.ToString();
                        }
                    }
                   
                }
                else
                {
                    
                    AdaborId = Controller.AutoStudentId();
                    var padded = (res= (month>11)? DateTime.Now.AddYears(1).ToString("yyyy") : DateTime.Now.ToString("yyyy")) + "-" + AdaborId.ToString().PadLeft(3, '0');
                    tbxRegNo.Text = padded;
                    tbxUserName.Text = padded;
                }
            }


        }
        else
        {
            if (ddlMedium.SelectedValue == "4")
            {
                EmediumZigatolaId = Controller.AutoStudentIdforZigatolaEnglishMedium();
                tbxRegNo.Text = "ZE" + DateTime.Now.ToString("yy") + "-" + EmediumZigatolaId.ToString();
                tbxUserName.Text = "ZE" + DateTime.Now.ToString("yy") + "-" + EmediumZigatolaId.ToString();

            }

            else
            {
                ZigatolaId = Controller.AutoStudentIdforZigatola();
                tbxRegNo.Text = "ZT" + (res= (month>10)? DateTime.Now.AddYears(1).ToString("yy"): DateTime.Now.ToString("yy")) + "-00" + ZigatolaId.ToString();
                tbxUserName.Text = "ZT" + DateTime.Now.ToString("yy") + "-00" + ZigatolaId.ToString();
            }

        }


    }


    protected void CheckRegNo()
    {
        tbxRoll.Text = "";
        tbxRegNo.Text = "";
        tbxUserName.Text = "";
        string isRollNoUSe = "True";
        int mId = Convert.ToInt32(rdlMedium.SelectedValue);
        // DataTable dt = new dalCommon().GetMediumById(mId);
        DataTable dt = new dalCommon().GetVersionById(mId);
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            isRollNoUSe = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
        }
        string rollNo = Controller.RegistrationNoPrefix(mId);
        string isRegNoUse = string.IsNullOrEmpty(dt.Rows[0]["IsRegNoUse"].ToString()) ? "" : dt.Rows[0]["IsRegNoUse"].ToString();
        tbxRoll.Visible = true;
        lblRoll.Visible = true;
        tbxRegNo.ReadOnly = true;

        if (isRegNoUse == "True")
        {
            tbxRegNo.Text = rollNo;
            tbxUserName.Text = rollNo;
            tbxRegNo.ReadOnly = true;
        }
        if (isRollNoUSe == "False")
        {
            tbxRoll.Visible = false;
            lblRoll.Visible = false;
        }
    }

    //[WebMethod]
    //public static string LoadRegistrationNo(string roll)
    //{
    //    int mId = Convert.ToInt32(roll);
    //    //int mId = (!String.IsNullOrEmpty(roll) ? Convert.ToInt32(roll) : 0);
    //    string rollNo = Controller.RegistrationNoPrefix(mId);
    //    //Insert it to our database       

    //    System.Threading.Thread.Sleep(3000);
    //    return rollNo;
    //}
    private void CheckMediumId()
    {
        int mId = Convert.ToInt32(rdlMedium.SelectedValue);
        //  DataTable dt = new dalCommon().GetMediumById(mId);
        DataTable dt = new dalCommon().GetVersionById(mId);
        ddlMedium.DataSource = dt;
        ddlMedium.DataBind();
    }
    protected void rdlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CheckRegNo();
        CheckMediumId();
        chkAssign.Checked = false;
        GetRegUserNo();
    }
    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegUserNo();
        chkAssign.Checked = false;
    }

    protected void ddlClass_SelectedIndexChanged(object ssender, EventArgs e)
    {
        GetRegUserNo();
        chkAssign.Checked = false;
    }
    protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegUserNo();
        chkAssign.Checked = false;
    }

    protected void ddlMedium_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegUserNo();
        chkAssign.Checked = false;
    }
    protected void LoadDefault()
    {
        imgStudent.ImageUrl = "~/Images/Common/student.png";
        imgFather.ImageUrl = "~/Images/Common/father.png";
        imgMother.ImageUrl = "~/Images/Common/mother.png";
    }

    #region Load All Dropdown
    protected void LoadDropdown()
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
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlStatus.DataSource = new Common().GetAll("bs_Status");
        ddlStatus.DataBind();

    }
    #endregion
    protected bool ValidImage(FileUpload file)
    {
        bool flag = false;
        string extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == "jpeg" || extension == ".jpg" || extension == ".png")
        {
            if (file.PostedFile.ContentLength < 1048576)
            {
                flag = true;
            }
        }
        return flag;
    }
    private Person GetPersonData()
    {

        DateTime dateOfBirth = DateTime.ParseExact(tbxDateOfBirth.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);

        Person person = new Person();
        person.NameEng = tbxNameEng.Text;
        person.NameBan = tbxNameBan.Text;
        person.FatherNameEng = tbxFathername.Text;
        person.MotherNameEng = tbxMotherName.Text;
        person.GenderId = Convert.ToInt32(ddlGender.SelectedValue);
        person.ReligionId = Convert.ToInt32(ddlReligion.SelectedValue);
        person.DateofBirth = dateOfBirth.ToString("yyyy/MM/dd");
        person.BirthCertificate = tbxBirthCertificate.Text;
        person.PhoneNo = tbxPhone.Text;
        person.Mobile = tbxMobile.Text;
        person.PhoneHome = tbxPhnHome.Text;
        person.MobileHome = tbxMobHome.Text;
        person.BloodGroup = Convert.ToInt32(ddlBlood.SelectedValue).ToString();
        person.FatherNId = tbxFatherNID.Text;
        person.MotherNId = tbxMotherNID.Text;
        person.IsFreedomFighter = Convert.ToBoolean(chkFreedom.Checked);
        person.IsTribal = Convert.ToBoolean(chkTribal.Checked);
        person.IsPhysicallyDefect = Convert.ToBoolean(chkPhyDef.Checked);
        person.FatherIncome = fIncome;
        person.MotherIncome = mIncome;
        person.FatherPhone = tbxFatherPhn.Text;
        person.MotherPhone = tbxMotherPhn.Text;
        person.FatherQualificationId = Convert.ToInt32(ddlFatherEdu.SelectedValue);
        person.FatherProfessionId = Convert.ToInt32(ddlFatherPro.SelectedValue);
        person.MotherQualificationId = Convert.ToInt32(ddlMotherEdu.SelectedValue);
        person.MotherProfessionId = Convert.ToInt32(ddlMotherPro.SelectedValue);
        person.NameLocalGuardian1 = tbxLocalGuardian1.Text;
        person.NameLocalGuardian2 = tbxLocalGuardian2.Text;
        person.LocalGuardian1Mobile = tbxLocG1MobileNo.Text;
        person.LocalGuardian2Mobile = tbxLocG2MobileNo.Text;
        return person;
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
            MessageController.Show("Roll No Must be Neumeric!!!", MessageType.Warning, Page);
        }
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        string imgStudent = "";
        string imgFather = "";
        string imgMother = "";
        string password = Request.Form[tbxPassword.UniqueID];

        #region Check Null
        if (tbxFatherIncome.Text == "")
        {
            fIncome = 0;
        }

        else
        {
            fIncome = Convert.ToInt32(tbxFatherIncome.Text);
        }

        if (tbxMotherIncome.Text == "")
        {
            mIncome = 0;
        }
        else
        {
            mIncome = Convert.ToInt32(tbxMotherIncome.Text);
        }

        Person person = new Person();
        person = GetPersonData();
        #endregion

        #region ImageUpload
        if (uploderStudent.HasFile)
        {
            if (ValidImage(uploderStudent) == true)
            {
                imgStudent = Guid.NewGuid() + "-" + uploderStudent.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploderStudent.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Student/" + imgStudent);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
            else
            {
                MessageController.Show("Select student image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
                return;

            }
        }

        if (uploadFather.HasFile)
        {
            if (ValidImage(uploadFather) == true)
            {
                imgFather = Guid.NewGuid() + "-" + uploadFather.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploadFather.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Father/" + imgFather);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }

            else
            {
                MessageController.Show("Select father image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
                return;

            }
        }

        if (uploadMother.HasFile)
        {
            if (ValidImage(uploadMother) == true)
            {
                imgMother = Guid.NewGuid() + "-" + uploadMother.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploadMother.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Mother/" + imgMother);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }

            else
            {
                MessageController.Show("Select mother Image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
                return;

            }
        }
        #endregion

        MembershipCreateStatus msc;

        //Set images
        person.FatherImage = imgFather;
        person.MotherImage = imgMother;
        person.PersonImage = imgStudent;
        string regNo = Request.Form[tbxRegNo.UniqueID];
        //if (chkAssign.Checked == true)
        //{
        //    if (tbxRoll.Visible)
        //    {
        //        try
        //        {
        //            if (tbxRoll.Text == "")
        //            {
        //                showMessageBox("please enter a Roll No!!!");
        //                return;
        //            }
        //            int studentRoll = (Convert.ToInt32(string.IsNullOrEmpty(tbxRoll.Text) ? "0" : tbxRoll.Text));

        //            if (!CheckExistingRollNumber(studentRoll))
        //            {
        //                MessageController.Show("Duplicate roll number!", MessageType.Warning, Page);
        //                return;
        //            }

        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //}
        //if (chkAssign.Checked == false)
        //{
        //    int studentRoll = (Convert.ToInt32(string.IsNullOrEmpty(tbxRoll.Text) ? "0" : tbxRoll.Text));
        //}
        GetRegUserNo();
        person.UserName = Request.Form[tbxRegNo.UniqueID];
        PersonId = objPerson.Insert(person);

        if (PersonId != -1)
        {
            StudentId = objStudent.Insert(PersonId, Request.Form[tbxRegNo.UniqueID], DateTime.Now, DateTime.Now.Year.ToString(), Page.User.Identity.Name, DateTime.Now, true, Convert.ToInt32(ddlStatus.SelectedValue), AdaborId, ZigatolaId, CollegeScienceId, CollegeBusinessId, EmediumAdaborId, EmediumZigatolaId);
            objPerson.PresentAddressInsert(PersonId, Convert.ToInt32(Request.Form[ddlPresentDiv.UniqueID]), Convert.ToInt32(Request.Form[ddlPresentDis.UniqueID]), Convert.ToInt32(Request.Form[ddlPresentThana.UniqueID]), tbxPostOffice.Text, tbxPostalCode.Text, tbxPresentAddress.Text);
            objPerson.PermanentAddressInsert(PersonId, Convert.ToInt32(Request.Form[ddlPermanentDiv.UniqueID]), Convert.ToInt32(Request.Form[ddlPermanentDis.UniqueID]), Convert.ToInt32(Request.Form[ddlPermanentThana.UniqueID]), tbxPermanentPost.Text, tbxPermanentPostCode.Text, tbxPermanentAddress.Text);

            if (StudentId != -1)
            {
                Membership.CreateUser(Request.Form[tbxRegNo.UniqueID], password);
                Roles.AddUserToRole(Request.Form[tbxRegNo.UniqueID], Controller.RoleNameById(13));
                Controller.UpdateRegistrationNo();
                if (chkAssign.Checked)
                {
                    int rollNumber = 0;
                    //if(tbxRoll.Visible)
                    //int.TryParse(tbxRoll.Text, out rollNumber);
                    if (CheckExistingRollNumber(rollNumber))
                    {
                        List<StudentToClass> lstStudentToClass = new List<StudentToClass>();
                        StudentToClass studentToClass = new StudentToClass();
                        studentToClass.StudentId = StudentId;
                        studentToClass.RollNo = rollNumber;
                        studentToClass.Year = ddlYear.SelectedValue;
                        studentToClass.MediumId = Convert.ToInt32(ddlMedium.SelectedValue);
                        studentToClass.ClassId = Convert.ToInt32(ddlClass.SelectedValue);
                        studentToClass.CampusId = Convert.ToInt32(ddlCampus.SelectedValue);
                        studentToClass.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
                        studentToClass.ShiftId = Convert.ToInt32(ddlShift.SelectedValue);
                        studentToClass.SectionId = Convert.ToInt32(ddlSection.SelectedValue);
                        studentToClass.CreatedBy = Page.User.Identity.Name;
                        lstStudentToClass.Add(studentToClass);
                        if (lstStudentToClass.Count > 0)
                        {
                            objStudent.InsertStudentToClassInformation(lstStudentToClass, false);
                        }
                    }
                }


                if (!string.IsNullOrEmpty(tbxMobile.Text))
                {

                    DataTable dt = new dalMessageCredit().GetAllMessageCredit();
                    MessageCredit msgCredit = new MessageCredit();

                    if (dt.Rows.Count > 0)
                    {
                        msgCredit.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                        msgCredit.SendQuantity = Convert.ToInt32(dt.Rows[0]["SendQuantity"]);
                        msgCredit.AvailableBalance = Convert.ToDecimal(dt.Rows[0]["AvailableBalance"]);

                        if (msgCredit.AvailableBalance > (decimal)0.44)
                        {
                            DataTable dtSchool = new Common().GetAll("bs_SchoolInformation");
                            string schoolName = dtSchool.Rows[0]["Name"].ToString();
                            string messageBody = @"Dear " + tbxNameEng.Text + ", Welcome to " + schoolName + ". Your User Id and Password is User Name: " + Request.Form[tbxRegNo.UniqueID] + "," + " Password: " + tbxPassword.Text;
                            //new MessageGateway().SendSms(tbxMobile.Text, messageBody);

                            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

                            MessageLog mgslog = new MessageLog();
                            mgslog.SendDate = DateTime.Now;
                            mgslog.MobileNumber = tbxMobile.Text;
                            mgslog.MessageBody = messageBody;
                            mgslog.Status = true;
                            mgslog.Sender = Page.User.Identity.Name;
                            mgslog.MessageType = "Add Student";

                            new dalMessageLog().MessageLogInsert(mgslog);


                            msgCredit.SendQuantity += 1;
                            msgCredit.AvailableBalance -= (decimal)0.45;
                            new dalMessageCredit().UpdateCredit(msgCredit);

                        }
                        else
                        {
                            MessageController.Show("Student information save successfully.But, Sorry!!!! Your Balance is insaficient. Please purchage SMS.", MessageType.Warning, Page);
                            return;
                        }
                    }
                    else
                    {
                        MessageController.Show("Student information save successfully.But, Sorry!!!! Credit problem. Please contact with Admin.", MessageType.Warning, Page);
                        return;
                    }

                }


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
        CheckRegNo();
        GetRegUserNo();
    }

    #region Dropdown selected index change
    protected void ddlPermanentDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPermanentDistrictByDivision();
        //MainView.ActiveViewIndex = 1;
    }
    protected void ddlPermanentDis_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPermanentThanaByDistrict();
        //MainView.ActiveViewIndex = 1;
    }
    protected void ddlPresentDis_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPresentThanaByDistrict();
        //MainView.ActiveViewIndex = 1;
    }
    protected void ddlPresentDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPresentDistrictByDivision();
        // MainView.ActiveViewIndex = 1;
    }
    #endregion


    private bool CheckExistingRollNumber(int studentRoll)
    {
        bool result = true;
        Dictionary<int, int> dicStudents = new Dictionary<int, int>();

        //check duplicate from database.      
        if (chkAssign.Checked)
        {
            if (tbxRoll.Visible)
            {
                DataTable dt = objStudent.GetStudentToClassByRollNo(ddlYear.SelectedValue, Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), studentRoll);
                if (dt.Rows.Count > 0)
                {
                    result = false;
                }
            }
        }
        return result;

    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {

    }
    protected void tbxRegNo_TextChanged(object sender, EventArgs e)
    {
        string getUserName = tbxRegNo.Text;
        tbxUserName.Text = getUserName;
    }


}




