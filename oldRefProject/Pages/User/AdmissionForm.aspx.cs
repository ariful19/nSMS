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

public partial class Pages_User_AdmissionForm : BasePage
{
    public static int count = 0;
    dalPerson objPerson = new dalPerson();
    dalStudent objStudent = new dalStudent();
    public static int PersonId;
    public static int StudentId;
    public static int StudentAdmissionId;
    int fIncome;
    int mIncome;
    protected void Page_Load(object sender, EventArgs e)
    {
        //tbxRegNo.Text = Controller.RegistrationNo().ToString();
        //tbxUserName.Text = Controller.RegistrationNo().ToString();

        if (!IsPostBack)
        {
            string ss = Common.SessionInfo.Panel;
            LoadDropdown();
           
            LoadDefault();
           // Session["Password"] = null;
        }
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

        ddlSiblingGender.DataSource = new Common().GetAll("bs_Gender");
        ddlSiblingGender.DataBind();

        ddlSiblingOccupation.DataSource = new Common().GetAll("bs_Profession");
        ddlSiblingOccupation.DataBind();

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

        ddlRefEduQuali.DataSource = new Common().GetAll("bs_Qualification");
        ddlRefEduQuali.DataBind();

        ddlReferenceProfession.DataSource = new Common().GetAll("bs_Profession");
        ddlReferenceProfession.DataBind();

        ddlPresentDiv.DataSource = new Common().GetAll("bs_Division");
        ddlPresentDiv.DataBind();

        ddlPermanentDiv.DataSource = new Common().GetAll("bs_Division");
        ddlPermanentDiv.DataBind();

        ddlBlood.DataSource = new Common().GetAll("bs_BloodGroup");
        ddlBlood.DataBind();

        ddlClassApplying.DataSource = new Common().GetAll("bs_ClassName");
        ddlClassApplying.DataBind();

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
   
    private StudentAdmission GetStudentAdmissionData()
    {

        DateTime dateOfBirth = DateTime.ParseExact(tbxDateOfBirth.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);

        StudentAdmission stu = new StudentAdmission();
        stu.SurName = tbxSurName.Text;
        stu.FirstName = tbxNameEng.Text;
        stu.NameBangla = txtNameBan.Text;
        stu.FatherNameEng = tbxFathername.Text;
        stu.MotherNameEng = tbxMotherName.Text;
        stu.GenderId = Convert.ToInt32(ddlGender.SelectedValue);
        stu.ReligionId = Convert.ToInt32(ddlReligion.SelectedValue);
        stu.DateofBirth = dateOfBirth;
        //stu.DateofBirth = dateOfBirth.ToString("yyyy/MM/dd");
        //stu.BirthCertificate = tbxBirthCertificate.Text;
        //person.PhoneNo = tbxPhone.Text;
        //person.Mobile = tbxMobile.Text;
        //person.PhoneHome = tbxPhnHome.Text;
        //person.MobileHome = tbxMobHome.Text;

        stu.BirthCertificate = "";
        stu.PhoneNo = "";
        stu.Mobile = "";
        stu.PhoneHome = "";
        stu.MobileHome = "";

        stu.BloodGroup = Convert.ToInt32(ddlBlood.SelectedValue).ToString();
        stu.FatherNId = tbxFatherNID.Text;
        stu.MotherNId = tbxMotherNID.Text;
        stu.IsFreedomFighter = Convert.ToBoolean(chkFreedom.Checked);
        stu.IsTribal = Convert.ToBoolean(chkTribal.Checked);
        stu.IsPhysicallyDefect = Convert.ToBoolean(chkPhyDef.Checked);
        stu.FatherIncome = fIncome;
        stu.MotherIncome = mIncome;
        stu.FatherPhone = tbxFatherPhn.Text;
        stu.MotherPhone = tbxMotherPhn.Text;
        stu.FatherQualificationId = Convert.ToInt32(ddlFatherEdu.SelectedValue);
        stu.FatherProfessionId = Convert.ToInt32(ddlFatherPro.SelectedValue);
        stu.MotherQualificationId = Convert.ToInt32(ddlMotherEdu.SelectedValue);
        stu.MotherProfessionId = Convert.ToInt32(ddlMotherPro.SelectedValue);

        stu.ClassApplyingFor = ddlClassApplying.SelectedItem.Value;
        stu.PresentDivisionId = Convert.ToInt32(Request.Form[ddlPresentDiv.UniqueID]);
        stu.PresentDistrictId = Convert.ToInt32(Request.Form[ddlPresentDis.UniqueID]);
        stu.PresentThanaId = Convert.ToInt32(Request.Form[ddlPresentThana.UniqueID]);
        stu.PresentPostOffice = tbxPostOffice.Text;
        stu.PresentPostalCode = tbxPostalCode.Text;
        stu.PresentAddress = tbxPresentAddress.Text;
        stu.DivisionId = Convert.ToInt32(Request.Form[ddlPermanentDiv.UniqueID]);
        stu.DistrictId = Convert.ToInt32(Request.Form[ddlPermanentDis.UniqueID]);
        stu.ThanaId = Convert.ToInt32(Request.Form[ddlPermanentThana.UniqueID]);
        stu.PostOffice = tbxPermanentPost.Text;
        stu.PostalCode = tbxPermanentPostCode.Text;
        stu.Address = tbxPermanentAddress.Text;
        stu.AvoidGames = ddlGameAvoid.SelectedItem.Text;
        stu.AvoidGamesDetails = txtGameAvoidDetail.Text;
        stu.AdmissionYear = DateTime.Now.Year.ToString();
        
        stu.Remarks = "";
        if(rdoBanglaMedium.Checked)
        {
            stu.Version = this.rdoBanglaMedium.Text;
        }
        else if(rdoEnglishMedium.Checked)
        {
            stu.Version = this.rdoEnglishMedium.Text;
        }
        else
            stu.Version = this.rdoEnglishVersion.Text;

        return stu;
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
        string imgSignature = "";


        // Person person = new Person();
        // person = GetPersonData();

        StudentAdmission student = new StudentAdmission();
        student = GetStudentAdmissionData();

        #region Check Null
        if (tbxFatherIncome.Text == "")
        {
            fIncome = 0;
        }
        if (tbxMotherIncome.Text == "")
        {
            mIncome = 0;
        }
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
        }

        if (uploadSignature.HasFile)
        {
            if (ValidImage(uploadSignature) == true)
            {
                imgSignature = Guid.NewGuid() + "-" + uploadSignature.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploderStudent.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Signature/" + imgSignature);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
        }

        #endregion
        //   MembershipCreateStatus msc;

        //Set images
        student.FatherImage = imgFather;
        student.MotherImage = imgMother;
        student.PersonImage = imgStudent;
        student.SignatureImage = imgSignature;

        StudentAdmissionId = objStudent.InsertStudentAdmission(student);
        if (StudentAdmissionId != -1)
        {
            objStudent.InsertSchoolFeedback(StudentAdmissionId, ddlSchoolFeedback.SelectedItem.Text, txtChooseReason.Text, txtExpectation.Text, this.ddlInstiteKnown.SelectedItem.Text, txtInsKnownName.Text, txtInsKnownDesignation.Text);

            objStudent.InsertPreviousStudy(StudentAdmissionId, txtPreSchoolName.Text, txtPreAddress.Text, txtpreTelephon.Text, txtPreEmail.Text, txtPreFax.Text, txtPreLastGrade.Text, txtPreResult.Text);

            objStudent.InsertReference(StudentAdmissionId, txtReferenceName.Text, txtReferenceNationality.Text, txtRefNationalId.Text, txtRefRelation.Text, ddlRefEduQuali.SelectedItem.Value.ToString(), txtRefDesignation.Text, ddlReferenceProfession.SelectedItem.Value.ToString(), txtReferenceMobile.Text, txtRefeEmail.Text, txtRefTearlyIncome.Text, txtReferenceAddress.Text);

            objStudent.InsertSibling(StudentAdmissionId, txtSiblingName.Text, ddlSiblingGender.SelectedItem.Value.ToString(), txtSiblingAge.Text, txtSibInstituteName.Text, ddlSiblingOccupation.SelectedItem.Value.ToString(), "");

            DateTime healthDate = DateTime.Now;

            if (txtHealthDate.Text != "")
            {
                 healthDate = DateTime.ParseExact(txtHealthDate.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            }
            objStudent.InsertHealth(StudentAdmissionId, txtHeight.Text, txtWeight.Text, healthDate, ddlPhysicalDrawback.SelectedItem.Text, txtDrawbackDetails.Text, ddlAlergyProb.SelectedItem.Text, txtAvoidable.Text, txtMedicinesGroup.Text);

        }
        else
        {
            MessageController.Show("This Registration no. already exists. Please try another.", MessageType.Error, Page);
            return;
        }

        //}
        //else
        //{
        //    MessageController.Show("This user name already exists. Please try another.", MessageType.Error, Page);
        //}


        DataTable dt = new dalCommon().GetSerialNo();
        int serialNo = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 1 : Convert.ToInt32(dt.Rows[0][0]));


        MessageController.Show("Admission Form Successfully Added. Your Serial No is <b>" +  serialNo +"</b>", MessageType.Confirmation, Page);
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

    protected void ddlPhysicalDrawback_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPhysicalDrawback.SelectedValue == "1")
        {
            this.txtDrawbackDetails.Enabled = true;
        }
        else
            this.txtDrawbackDetails.Enabled = false;
    }

    protected void ddlAlergyProb_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAlergyProb.SelectedValue == "1")
        {
            this.txtAvoidable.Enabled = true;
        }
        else
            this.txtAvoidable.Enabled = false;

    }

    protected void chkReference_CheckedChanged(object sender, EventArgs e)
    {
        if(chkReference.Checked)
        {
            this.pnlReference.Visible = true;
        }
        else
            this.pnlReference.Visible = false;
    }
    protected void chkPreviousStudy_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPreviousStudy.Checked)
        {
            this.pnlPreviousStudy.Visible = true;
        }
        else
            this.pnlPreviousStudy.Visible = false;

    }
    #endregion



   
}