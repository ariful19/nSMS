
using iTextSharp.text;
using iTextSharp.text.pdf;
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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
[System.Web.Script.Services.ScriptService]

public partial class Pages_User_Profile : BasePage
{
    dalPerson objPerson = new dalPerson();
    dalStudent objStudent = new dalStudent();
    dalTeacher objTeacher = new dalTeacher();
    int techerId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        var schoolname = new Common().GetAll("bs_SchoolInformation").AsEnumerable();
        var name = schoolname.First()["Name"].ToString();
        var code = schoolname.First()["Code"].ToString();
        lblSchool.Text = name;
        lblcode.Text = "School Code:" + code;


        if (Page.User.IsInRole("Student"))
        {
            pnlForTeacher.Visible = false;
            localDiv.Visible = true;
            lblinfo.Text = "Student Information";
           
        }
        else
        {
            pnlForTeacher.Visible = true;
            localDiv.Visible = false;
            divJoinDate.Visible = true;
            divEmail.Visible = true;
            divJoin.Visible = true;
            divEmailAddress.Visible = true;
            lblinfo.Text = "Employee Information";
        }
        if (!IsPostBack)
        {

            LoadDropdown();
            if (Page.User.Identity.Name != "")
            {
                if (Request.QueryString["UserName"] != null)
                {
                    if (Page.User.IsInRole("Admin") || Page.User.IsInRole("Office Exicutive"))
                    {
                        UserName = Request.QueryString["UserName"];
                        ID = Controller.PersonIdByUserName(UserName);                       
                        techerId = Controller.TecherIdByPersonalId(ID);
                        pnlForTeacher.Visible = true;
                        localDiv.Visible = false;
                        lblinfo.Text = "Employee Information";
                        btnPersonal.Enabled = true;
                        btnFatherInfo.Enabled = true;
                        btnMotherInfo.Enabled = true;
                        btnLocalGuardianInfo.Enabled = true;
                        btnPresentAddress.Enabled = true;
                        btnPermanentAddress.Enabled = true;
                    }
                    else
                    {
                        btnPersonal.Enabled = false;
                        btnFatherInfo.Enabled = false;
                        btnMotherInfo.Enabled = false;
                        btnLocalGuardianInfo.Enabled = false;
                        btnPresentAddress.Enabled = false;
                        btnPermanentAddress.Enabled = false;

                    }
                }
                else
                {
                    UserName = Page.User.Identity.Name;
                    ID = Controller.PersonIdByUserName(UserName);
                }
                if (!Page.User.IsInRole("Student") && !Page.User.IsInRole("Admin"))
                {
                    techerId = Controller.TecherIdByPersonalId(ID);
                }
                hdnPersonId.Value = ID.ToString();
                LoadProfile();
                this.BindGrid();
            }
        }
       
    }
    #region Variable
    string UserName
    {
        set { ViewState["UserName"] = value; }
        get
        {
            try
            {
                return ViewState["UserName"].ToString();
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
    #endregion

    #region Load All Dropdown
    protected void LoadDropdown()
    {
        //ddlDesignation.DataSource = new Common().GetAll("bs_Designation");
        //ddlDesignation.DataBind();

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

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlGrade.DataSource = new Common().GetAll("tbl_Grade");
        ddlGrade.DataBind();

        ddlLevel.DataSource = new Common().GetAll("tbl_Level");
        ddlLevel.DataBind();

        ddlDesignation.DataSource = new Common().GetAll("bs_Designation");
        ddlDesignation.DataBind();

        ddlStatus.DataSource = new Common().GetAll("tr_Status");
        ddlStatus.DataBind();

    }
    [WebMethod]
    public static ArrayList LoadPresentDistrictByDivision(int divId)
    {
        ArrayList list = new ArrayList();
        DataTable dt = new dalDistrict().GetByDivisionId(divId);
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new System.Web.UI.WebControls.ListItem(
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
            list.Add(new System.Web.UI.WebControls.ListItem(
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

    #region Load Profile
    protected void LoadProfile()
    {
        int roleID = 0;
        DataTable dt = objPerson.GetByUserName(UserName);
        DataTable rdt = new dalRole().GetIdByUserName(UserName);

        //DataTable dtEdu = objTeacher.EducationByTeacherId(techerId);
        //DataTable dtTrainings = objTeacher.TrainingByTeacherId(techerId);

        //var trainingId = Session["TrainingID"];
        //DataTable dtTraining = objTeacher.TrainingByTrainingId(Convert.ToInt32(trainingId));

        roleID = (string.IsNullOrEmpty(rdt.Rows[0]["Id"].ToString())) ? 0 : Convert.ToInt32(rdt.Rows[0]["Id"].ToString());

        if (dt.Rows.Count > 0)
        {
            #region Load Label
            hdnPersonId.Value = dt.Rows[0]["Id"].ToString();
            hdnPersonImage.Value = dt.Rows[0]["PersonImage"].ToString();
            hdnFatherImage.Value = dt.Rows[0]["FatherImage"].ToString();
            hdnMotherImage.Value = dt.Rows[0]["MotherImage"].ToString();
            if (roleID == 13)
            {
                imgPerson.ImageUrl = "../../Images/Student/" + dt.Rows[0]["PersonImage"].ToString();
            }
            else
            {
                imgPerson.ImageUrl = "../../Images/Person/" + dt.Rows[0]["PersonImage"].ToString();
            }
            //  imgPerson.ImageUrl = "../../Images/Person/" + dt.Rows[0]["PersonImage"].ToString();
            imgFather.ImageUrl = "../../Images/Father/" + dt.Rows[0]["FatherImage"].ToString();
            imgMother.ImageUrl = "../../Images/Mother/" + dt.Rows[0]["MotherImage"].ToString();
            lblNameEng.Text = dt.Rows[0]["NameEng"].ToString();
            lblNameBan.Text = dt.Rows[0]["NameBan"].ToString();
            lblGender.Text = dt.Rows[0]["Gender"].ToString();
            lblBlood.Text = dt.Rows[0]["BloodGroupName"].ToString();
            lblBirthCertificate.Text = dt.Rows[0]["BirthCertificate"].ToString();
            lblReligion.Text = dt.Rows[0]["Religion"].ToString();
            lblCampus.Text = dt.Rows[0]["Campus"].ToString();
            if (dt.Rows[0]["DateofBirth"].ToString() != "")
                lblDoB.Text = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString()).ToString("dd/MM/yyyy");
            lblPhn.Text = dt.Rows[0]["PhoneNo"].ToString();
            lblMob.Text = dt.Rows[0]["Mobile"].ToString();
            lblPhnHome.Text = dt.Rows[0]["PhoneHome"].ToString();
            lblMobHome.Text = dt.Rows[0]["MobileHome"].ToString();
            if (divJoinDate.Visible==true && divEmail.Visible == true)
            {
                lblJoinDate.Text = Convert.ToDateTime(dt.Rows[0]["Joindate"].ToString()).ToString("dd/MM/yyyy");
                lblEmail.Text = dt.Rows[0]["Email"].ToString();
                lblNationality.Text = dt.Rows[0]["Nationality"].ToString();
                lblNationalId.Text = dt.Rows[0]["NId"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            }

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
            lblLocalGuardian2.Text = dt.Rows[0]["NameLocalGuardian2"].ToString();
            lblLocalG1MobileNo.Text = dt.Rows[0]["LocalGuardian1Mobile"].ToString();
            lblLocalG2MobileNo.Text = dt.Rows[0]["LocalGuardian2Mobile"].ToString();

            #endregion

            #region Load Textbox
            tbxNameEng.Text = dt.Rows[0]["NameEng"].ToString();
            tbxNameBan.Text = dt.Rows[0]["NameBan"].ToString();
            ddlGender.SelectedValue = dt.Rows[0]["GenderId"].ToString();
            ddlReligion.SelectedValue = dt.Rows[0]["ReligionId"].ToString();
            if (dt.Rows[0]["DateofBirth"].ToString() != "")
                tbxDateOfBirth.Text = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString()).ToString("dd/MM/yyyy");
            tbxBirthCertificate.Text = dt.Rows[0]["BirthCertificate"].ToString();
            tbxPhone.Text = dt.Rows[0]["PhoneNo"].ToString();
            tbxMobile.Text = dt.Rows[0]["Mobile"].ToString();
            tbxPhnHome.Text = dt.Rows[0]["PhoneHome"].ToString();
            tbxMobHome.Text = dt.Rows[0]["MobileHome"].ToString();
            ddlBlood.SelectedValue = dt.Rows[0]["BloodGroup"].ToString();
            ddlCampus.SelectedValue = dt.Rows[0]["CampusIds"].ToString();
            DataTable dtT = objTeacher.GetByPersonId(ID);
            tbxBankAccount.Text = (dtT.Rows[0]["AccountNumber"].ToString() == null) ? "" : dtT.Rows[0]["AccountNumber"].ToString();
            ddlStatus.SelectedValue = dtT.Rows[0]["IsActive"].ToString();
            ddlGrade.SelectedValue = dtT.Rows[0]["GradeId"].ToString();
            ddlLevel.SelectedValue = dtT.Rows[0]["LevelId"].ToString();

            if (divJoin.Visible==true && divEmailAddress.Visible==true)
            {
                tbxJoinDate.Text = Convert.ToDateTime(dt.Rows[0]["Joindate"].ToString()).ToString("dd/MM/yyyy");
                tbxEmail.Text = dt.Rows[0]["Email"].ToString();
                tbxNationality.Text = dt.Rows[0]["Nationality"].ToString();
                tbxNationalId.Text = dt.Rows[0]["NId"].ToString();
                ddlDesignation.SelectedValue = dt.Rows[0]["DesignationId"].ToString();
            }

            ddlPermanentDiv.SelectedValue = dt.Rows[0]["DivisionId"].ToString();
            ddlPermanentDis.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            ddlPermanentThana.SelectedValue = dt.Rows[0]["ThanaId"].ToString();
            tbxPermanentPost.Text = dt.Rows[0]["PostOffice"].ToString();
            tbxPermanentPostCode.Text = dt.Rows[0]["PostalCode"].ToString();
            tbxPermanentAddress.Text = dt.Rows[0]["Address"].ToString();

            ddlPresentDiv.SelectedValue = dt.Rows[0]["presentDiv"].ToString();

            if (!string.IsNullOrEmpty(ddlPresentDiv.SelectedValue))
            {
                DataTable dtf = new dalDistrict().GetByDivisionId(Convert.ToInt32(ddlPresentDiv.SelectedValue));
                ddlPresentDis.DataSource = dtf;
                ddlPresentDis.DataBind();
            }
            ddlPresentDis.SelectedValue = dt.Rows[0]["presentDis"].ToString();
            if (!string.IsNullOrEmpty(ddlPresentDis.SelectedValue))
            {
                DataTable dtthana = new dalThana().GetByDistrictId(Convert.ToInt32(ddlPresentDis.SelectedValue));
                ddlPresentThana.DataSource = dtthana;
                ddlPresentThana.DataBind();
            }
            ddlPresentThana.SelectedValue = dt.Rows[0]["presentThana"].ToString();
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
            tbxLocalGuardian2.Text = dt.Rows[0]["NameLocalGuardian2"].ToString();
            tbxLocG1MobileNo.Text = dt.Rows[0]["LocalGuardian1Mobile"].ToString();
            tbxLocG2MobileNo.Text = dt.Rows[0]["LocalGuardian2Mobile"].ToString();
                  
            #endregion

            #region CheckedBox

            string isFreedomFighter = dt.Rows[0]["IsFreedomFighter"].ToString();
            string isTribal = dt.Rows[0]["IsTribal"].ToString();
            string isPhysicallyDefect = dt.Rows[0]["IsPhysicallyDefect"].ToString();

            if (isFreedomFighter == "True")
            {
                chkFreedom.Checked = true;
                chkmFreedomFighter.Checked = true;
            }
            if (isTribal == "True")
            {
                chkTribal.Checked = true;
                chkmTribal.Checked = true;
            }
            if (isPhysicallyDefect == "True")
            {
                chkPhyDef.Checked = true;
                chkmPhyDef.Checked = true;
            }

            #endregion
        }
        if (Page.User.IsInRole("Student"))
        {
            lblReg.Text = "Enter Student ID";
            empId.Visible = false;
            divEmpId.Visible = false;
            DataTable dtst = objStudent.GetByPersonId(ID);
            if (dtst.Rows.Count > 0)
            {
                lblRegNo.Text = dtst.Rows[0]["RegNo"].ToString();
                tbxRegNo.Text = dtst.Rows[0]["RegNo"].ToString();
                tbxRegNo.Enabled = false;               
            }
        }
        else
        {
            lblReg.Text = "Employee Pin";
            lblPIN.Text = "Employee Pin";
            lblEmployee.Text = "Employee ID";
            lblEmp.Text = "Employee ID";
            tbxMobile.ReadOnly = false;
            empId.Visible = true;
            divEmpId.Visible = true;
            DataTable dtTeacher = objTeacher.GetByPersonId(ID);

            if (dtTeacher.Rows.Count > 0)
            {
                Session["TeacherID"] = dtTeacher.Rows[0]["Id"];
                lblRegNo.Text = dtTeacher.Rows[0]["TeacherPin"].ToString();
                tbxRegNo.Text = dtTeacher.Rows[0]["TeacherPin"].ToString();
                lblEmpId.Text = dtTeacher.Rows[0]["EmployeeId"].ToString();
                tbxEmplId.Text = dtTeacher.Rows[0]["EmployeeId"].ToString();
                lblGrade.Text = dtTeacher.Rows[0]["Type"].ToString() == null ? "" : dtTeacher.Rows[0]["Type"].ToString();
                lblCategory.Text = dtTeacher.Rows[0]["LevelName"].ToString() == null ? "N/A" : dtTeacher.Rows[0]["LevelName"].ToString();
                lblBankAccount.Text = dtTeacher.Rows[0]["AccountNumber"].ToString() == null ? "" : dtTeacher.Rows[0]["AccountNumber"].ToString();
                lblStatus.Text = dtTeacher.Rows[0]["Status"].ToString();
            }
        }

    }
    #endregion

    #region Image Change
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
        if (lblinfo.Text == "Student Information")
        {
            string imgPer = "";
            if (hdnPersonId.Value != "")
            {
                Controller.DeleteFile(Server.MapPath("~/Image/Student/") + hdnPersonImage.Value);
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
        if (lblinfo.Text == "Employee Information")
        {
            string imgPer = "";
            if (hdnPersonId.Value != "")
            {
                Controller.DeleteFile(Server.MapPath("~/Image/Person/") + hdnPersonImage.Value);
                if (uploderStudent.HasFile)
                {
                    if (ValidImage(uploderStudent) == true)
                    {
                        imgPer = Guid.NewGuid() + "-" + uploderStudent.FileName;
                        System.Drawing.Image image = System.Drawing.Image.FromStream(uploderStudent.FileContent);
                        System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                        EncoderParameters encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                        string MediumImagePath = Server.MapPath("~/Images/Person/" + imgPer);
                        image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
                    }
                }
                objPerson.PersonImageUpdate(Convert.ToInt32(hdnPersonId.Value), imgPer);
                imgPerson.ImageUrl = "../../Images/Person/" + imgPer;
            }
        }

    }
    protected void btnFatherImage_Click(object sender, EventArgs e)
    {
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
    protected void btnMother_Click(object sender, EventArgs e)
    {
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
    #endregion
    protected void btnPersonal_Click(object sender, EventArgs e)
    {
        string email = "";       
        string fax = "";
        string nationality = "";
        string NId = "";
        int desigId = 0;
        int gradeId = 0;
        int levelId = 0;
        int Campus = 0;
        if (!Page.User.IsInRole("Student"))
        {
            email = tbxEmail.Text;
            fax = "";
            nationality = tbxNationality.Text;
            NId = tbxNationalId.Text;
            desigId = Convert.ToInt32(ddlDesignation.SelectedValue);
            gradeId = Convert.ToInt32(ddlGrade.SelectedValue);
            levelId = Convert.ToInt32(ddlLevel.SelectedValue);
            Campus = Convert.ToInt32(ddlCampus.SelectedValue);
        }
        objPerson.UpdateTeacher(ID, NId, desigId, Campus,gradeId,levelId);
        objPerson.Update(ID, tbxNameEng.Text, tbxNameBan.Text, Convert.ToInt32(ddlGender.SelectedValue), Convert.ToInt32(ddlReligion.SelectedValue),nationality, DateTime.ParseExact(tbxDateOfBirth.Text, "dd/MM/yyyy", null), tbxBirthCertificate.Text,
            tbxPhone.Text, tbxMobile.Text, tbxPhnHome.Text, tbxMobHome.Text, email, fax, Convert.ToInt32(ddlBlood.SelectedValue));
        objTeacher.BankAccountInsert(Convert.ToInt32(hdnPersonId.Value), desigId, Convert.ToInt32(ddlCampus.SelectedValue),Convert.ToBoolean(ddlStatus.SelectedValue),tbxBankAccount.Text, Page.User.Identity.Name, DateTime.Now);
        
        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }
    protected void btnFatherInfo_Click(object sender, EventArgs e)
    {
        int income = 0;
        if (tbxFatherIncome.Text != "")
            income = Convert.ToInt32(tbxFatherIncome.Text);
        objPerson.UpdateFatherInfo(ID, tbxFathername.Text, "", tbxFatherNID.Text, income, tbxFatherPhn.Text, Convert.ToInt32(ddlFatherEdu.SelectedValue), Convert.ToInt32(ddlFatherPro.SelectedValue));
        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
        Response.Redirect("~/Pages/User/Profile.aspx");
    }
    protected void btnMotherInfo_Click(object sender, EventArgs e)
    {
        int income = 0;
        if (tbxMotherIncome.Text != "")
            income = Convert.ToInt32(tbxMotherIncome.Text);
        objPerson.UpdateMotherInfo(ID, tbxMotherName.Text, "", tbxMotherNID.Text, income, tbxMotherPhn.Text, Convert.ToInt32(ddlMotherEdu.SelectedValue),
            Convert.ToInt32(ddlMotherPro.SelectedValue));
        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }
    protected void btnLocalGuardianInfo_Click(object sender, EventArgs e)
    {
        objPerson.LocalGuardianUpdate(ID, tbxLocalGuardian1.Text, tbxLocalGuardian2.Text, tbxLocG1MobileNo.Text,
            tbxLocG2MobileNo.Text);
        tbxLocalGuardian1.Text = string.Empty;
        tbxLocalGuardian2.Text = string.Empty;
        tbxLocG1MobileNo.Text = string.Empty;
        tbxLocG2MobileNo.Text = string.Empty;
        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }
    protected void btnPresentAddress_Click(object sender, EventArgs e)
    {
        string selectedDis = Request.Form[ddlPresentDis.UniqueID];
        string selectedThana = Request.Form[ddlPresentThana.UniqueID];
        objPerson.PresentAddressUpdate(ID, Convert.ToInt32(ddlPresentDiv.SelectedValue), Convert.ToInt32(selectedDis), Convert.ToInt32(selectedThana),
            tbxPostOffice.Text, tbxPostalCode.Text, tbxPresentAddress.Text);
        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }
    protected void btnPermanentAddress_Click(object sender, EventArgs e)
    {
        string selectedDis = Request.Form[ddlPermanentDis.UniqueID];
        string selectedThana = Request.Form[ddlPermanentThana.UniqueID];
        objPerson.PermanentAddressUpdate(ID, Convert.ToInt32(ddlPermanentDiv.SelectedValue), Convert.ToInt32(selectedDis), Convert.ToInt32(selectedThana),
            tbxPermanentPost.Text, tbxPermanentPostCode.Text, tbxPermanentAddress.Text);
        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }

    protected void btnOtherInfo_Click(object sender, EventArgs e)
    {
        bool isFreedomFighter = Convert.ToBoolean(chkmFreedomFighter.Checked);
        bool isTribal = Convert.ToBoolean(chkmTribal.Checked);
        bool isPhysicallyDefect = Convert.ToBoolean(chkmPhyDef.Checked);

        objPerson.OtherInfoUpdate(ID, isFreedomFighter, isTribal, isPhysicallyDefect);

        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }

       //For Education//
    private void BindGrid()
    {
        if (Page.User.Identity.Name != "")
        {
            if (Request.QueryString["UserName"] != null)
            {
                if (Page.User.IsInRole("Admin"))
                {
                    UserName = Request.QueryString["UserName"];
                    ID = Controller.PersonIdByUserName(UserName);
                    techerId = Controller.TecherIdByPersonalId(ID);
                    pnlForTeacher.Visible = true;
                    localDiv.Visible = false;
                    lblinfo.Text = "Employee Information";
                }
            }
            else
            {
                UserName = Page.User.Identity.Name;
                ID = Controller.PersonIdByUserName(UserName);
            }
            
            if (!Page.User.IsInRole("Student") && !Page.User.IsInRole("Admin"))
            {
                techerId = Controller.TecherIdByPersonalId(ID);
            }
        }
        if (techerId!=0)
        {
            DataTable dtEdu = objTeacher.EducationByTeacherId(techerId);
            DataTable dtTraining = objTeacher.TrainingByTeacherId(techerId);
            if (dtEdu.Rows.Count == 0 && dtTraining.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dtEdu.Rows.Add();
                dtTraining.Rows.Add();
            }

            gvEducation.DataSource = dtEdu;
            gvEducation.DataBind();

            gvTraining.DataSource = dtTraining;
            gvTraining.DataBind();

        }
       
    }
   
    protected void EditTraining(object sender, GridViewEditEventArgs e)
    {
        gvTraining.EditIndex = e.NewEditIndex;
        this.BindGrid();
    }

    protected void CancelEditTraining(object sender, EventArgs e)
    {
        gvTraining.EditIndex = -1;
        this.BindGrid();
    }


    protected void OnPagingTraining(object sender, GridViewPageEventArgs e)
    {

        this.BindGrid();

        gvTraining.PageIndex = e.NewPageIndex;

        gvTraining.DataBind();

    }

    protected void UpdateTraining(object sender, GridViewUpdateEventArgs e)
    {

        DateTime startDate = DateTime.ParseExact(((TextBox)gvTraining.Rows[e.RowIndex].FindControl("tbxStartDate")).Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(((TextBox)gvTraining.Rows[e.RowIndex].FindControl("tbxEndDate")).Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
       


        TeacherTraining teacherTraining = new TeacherTraining();
        teacherTraining.TrainingID = Convert.ToInt32(((Label)gvTraining.Rows[e.RowIndex].FindControl("lblId")).Text);
        teacherTraining.TrainingName = ((TextBox)gvTraining.Rows[e.RowIndex].FindControl("tbxTrainingName")).Text;
        teacherTraining.InstituteName = ((TextBox)gvTraining.Rows[e.RowIndex].FindControl("tbxInstituteName")).Text;
        teacherTraining.StartDate = startDate;
        teacherTraining.EndDate = endDate;
        teacherTraining.Topics = ((TextBox)gvTraining.Rows[e.RowIndex].FindControl("tbxTopics")).Text;
        teacherTraining.Duration = ((TextBox)gvTraining.Rows[e.RowIndex].FindControl("tbxDuration")).Text;
        new dalTeacher().TrainingUpdate(teacherTraining);
        this.BindGrid();

        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }

    protected void EditEducation(object sender, GridViewEditEventArgs e)
    {
        gvEducation.EditIndex = e.NewEditIndex;
        this.BindGrid();
    }

    protected void CancelEditEducation(object sender, EventArgs e)
    {
        gvEducation.EditIndex = -1;
        this.BindGrid();
    }


    protected void OnPagingEducation(object sender, GridViewPageEventArgs e)
    {

        this.BindGrid();

        gvEducation.PageIndex = e.NewPageIndex;

        gvEducation.DataBind();

    }
    protected void UpdateEducation(object sender, GridViewUpdateEventArgs e)
    {
        TeacherEducation techerEdu = new TeacherEducation();
        techerEdu.Id = Convert.ToInt32(((Label)gvEducation.Rows[e.RowIndex].FindControl("lblIdEdu")).Text);
        techerEdu.DegreeName = ((TextBox)gvEducation.Rows[e.RowIndex].FindControl("tbxDegreeName")).Text;
        techerEdu.Subject = ((TextBox)gvEducation.Rows[e.RowIndex].FindControl("tbxSubject")).Text;
        techerEdu.Board = ((TextBox)gvEducation.Rows[e.RowIndex].FindControl("tbxBoard")).Text;
        techerEdu.PassingYear = ((TextBox)gvEducation.Rows[e.RowIndex].FindControl("tbxPassingYear")).Text;
        techerEdu.ResultDivision = ((TextBox)gvEducation.Rows[e.RowIndex].FindControl("tbxResultDivision")).Text;
        techerEdu.Grade = ((TextBox)gvEducation.Rows[e.RowIndex].FindControl("tbxGrade")).Text;
        techerEdu.GPAScale = ((TextBox)gvEducation.Rows[e.RowIndex].FindControl("tbxGPAScale")).Text;
        new dalTeacher().EducationUpdate(techerEdu);

        this.BindGrid();

        if (Request.QueryString["UserName"] != null)
        {
            if (Page.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Pages/User/Profile.aspx?UserName=" + Request.QueryString["UserName"]);
            }
        }
        else
            Response.Redirect("~/Pages/User/Profile.aspx");
    }


    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        logoPanal.Visible = true;

        StringWriter sw = new StringWriter();
        HtmlTextWriter w = new HtmlTextWriter(sw);
        HtmlForm f = new HtmlForm();
        //pdf.Controls.Remove(CalendarExtender1);
        //pdf.Controls.Remove(CalendarExtender2);
        //pdf.Controls.Remove(CalendarExtender3);
        this.Controls.Add(f);
        f.Controls.Add(pdf);
        f.RenderControl(w);


        var sbhtml = sw.GetStringBuilder();
        
        sw.Close();
        w.Close();
        sbhtml.Insert(0, @"<!DOCTYPE html><html><head><title>HTML5</title>    <meta charset='utf-8'/><script src='jquery-3.1.0.js'></script>    <link href='Styles/bootstrap.min.css' rel='stylesheet' /><link href='Styles/ApplicantsView.css' rel='stylesheet' /> <link href='Styles/Custom.css' rel='stylesheet' />  <script src='Scripts/jquery-2.1.3.min.js'></script>   <script src='Scripts/bootstrap.min.js'></script>
<script>
window.onload = function () {
    var tbls = document.getElementsByTagName('table');
    for (var i = 0; i < tbls.length; i++) {
        var tbl = tbls.item(i);
        var rows = tbl.querySelectorAll('tr');
        var lastRow = rows.item(rows.length - 1);
        lastRow.remove();
        var rows = tbl.querySelectorAll('tr');
        for (var j = 0; j < rows.length; j++) {
            var r = rows.item(j);
            var lastColumn = r.children.item(r.children.length - 1);
            lastColumn.remove();
        }
    }
};
</script>
</head><body>");

        sbhtml.Append("</body></html>");
        sbhtml.Replace("../../", "");
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\cpaa.html";

        var str = Regex.Replace(sbhtml.ToString(), "<button.*</button>", "");
        str = Regex.Replace(str, "<input.*type=\"submit\".*>", "");
        str = Regex.Replace(str, "<a.*</a>", "");
        System.IO.File.WriteAllText(path, str, System.Text.Encoding.UTF8);
        var uri = new System.Uri(path);
        var converted = uri.AbsoluteUri;

        SelectPdf.HtmlToPdf cnv = new SelectPdf.HtmlToPdf();
        cnv.Options.AutoFitHeight = SelectPdf.HtmlToPdfPageFitMode.AutoFit;
        cnv.Options.MarginTop = 30;
        cnv.Options.MarginLeft = 50;
        cnv.Options.MarginRight = 50;
        var doc = cnv.ConvertUrl(converted);
        doc.Save(Response, false, "sth.pdf");
        doc.Close();
    }
}
   



