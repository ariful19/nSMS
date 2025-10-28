using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admission_Teacher : BasePage
{
    public static int count = 0;
    dalPerson objPerson = new dalPerson();
    dalTeacher objTeacher = new dalTeacher();
    public static int PersonId;
    public static int TeacherId;
    int fIncome;
    int mIncome;
    protected void Page_Load(object sender, EventArgs e)
    {
        tbxRegNo.Text = Controller.TeacherPin().ToString(); 
        if (!IsPostBack)
        {
            tbxUserName.Text = string.Empty;
            this.BindGrid();
            this.LoadTraining();
            dtEdu = new DataTable("dtEducation");
            dtTrin = new DataTable("dtTraining");
            LoadDropdown();
            LoadDefault();
            TeacherPin();
            //LoadPresentDistrictByDivision();
            //LoadPresentThanaByDistrict();
            //LoadPermanentDistrictByDivision();
            //LoadPermanentThanaByDistrict();
            //Education();
            //Training();          
        }
    }

    DataTable dtEdu
    {
        set { ViewState["dtEdu"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtEdu"];
            }
            catch
            {
                return null;
            }
        }
    }

    DataTable dtTrin
    {
        set { ViewState["dtTrin"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtTrin"];
            }
            catch
            {
                return null;
            }
        }
    }
    /***************************************************************************/
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
    private void BindGrid()
    {
        DataTable dt = objTeacher.EducationByTeacherId(0);
        if (dt.Rows.Count == 0)
        {
            //If no records then add a dummy row.
            dt.Rows.Add();
        }

        gvEducation.DataSource = dt;
        gvEducation.DataBind();
    }
    private void LoadTraining()
    {
        DataTable dt = objTeacher.TrainingByTeacherId(0);
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add();
        }
        gvTraining.DataSource = dt;
        gvTraining.DataBind();
    }
    private void TeacherPin()
    {
        string isAutomatic = "";
        DataTable dtPin = new dalCommon().GetTeacherPinFormat();
        if (dtPin.Rows.Count > 0)
        {
            isAutomatic = dtPin.Rows[0]["IsAutomatic"].ToString();
        }
        if (isAutomatic == "True")
        {
            tbxRegNo.Text = Controller.TeacherPin().ToString();
        }
        else
        {
            tbxRegNo.Text = "";
            tbxRegNo.ReadOnly=false;
        }
        

    }
    /***************************************************************************/
    protected void LoadDefault()
    {
        imgTeacher.ImageUrl = "~/Images/Common/Teacher.png";
        imgFather.ImageUrl = "~/Images/Common/father.png";
        imgMother.ImageUrl = "~/Images/Common/mother.png";
    }
    #region Load All Dropdown
    protected void LoadDropdown()
    {
        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlDesignation.DataSource = new Common().GetAll("bs_Designation");
        ddlDesignation.DataBind();

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

        ddlRole.DataSource = new dalRole().GetAllRole();
        ddlRole.DataBind();

        ddlBlood.DataSource = new Common().GetAll("bs_BloodGroup");
        ddlBlood.DataBind();

        ddlPayScaleGrade.DataSource = new Common().GetAll("pr_Type");
        ddlPayScaleGrade.DataBind();

        ddlLevel.DataSource = new Common().GetAll("tbl_level");
        ddlLevel.DataBind();

        ddlDegree.DataSource = new Common().GetAll("bs_Qualification");
        ddlDegree.DataBind();

        LoadPayScale();
    }

    protected void LoadPayScale()
    {
        if (!string.IsNullOrEmpty(ddlPayScaleGrade.SelectedValue))
        {
            DataTable dt = new dalPayrollBasic().TypeGetById(Convert.ToInt32(ddlPayScaleGrade.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                lblPayScale.Text = dt.Rows[0]["Scale"].ToString();
                lblBasicSalary.Text = dt.Rows[0]["Basic"].ToString();
            }
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
                //MainView.ActiveViewIndex = 1;
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
    private void SendMail()
    {
        #region Email

        DataTable dtEmailTemplate = new dalEmailTemplete().GetById(1);
        if (dtEmailTemplate.Rows.Count > 0)
        {
            string Subject = dtEmailTemplate.Rows[0]["Subject"].ToString();
            string EmailTemplate = dtEmailTemplate.Rows[0]["Body"].ToString();
            EmailTemplate = EmailTemplate.Replace("@Name", tbxNameEng.Text);
            EmailTemplate = EmailTemplate.Replace("@UserName", tbxUserName.Text);
            EmailTemplate = EmailTemplate.Replace("@Password", tbxPassword.Text);
            Notify objNotify = new Notify(tbxEmail.Text, Subject, EmailTemplate);
            Thread threadNotify = new Thread(new ThreadStart(objNotify.runme));
            threadNotify.Name = "RegistrationNotification";
            threadNotify.Start();
        }

        #endregion
    }
    private Person GetPersonData()
    {
        Person person = new Person();
        person.NameEng = tbxNameEng.Text;
        person.NameBan = tbxNameBan.Text;
        person.FatherNameEng = tbxFathername.Text;
        person.MotherNameEng = tbxMotherName.Text;
        person.GenderId = Convert.ToInt32(ddlGender.SelectedValue);
        person.ReligionId = Convert.ToInt32(ddlReligion.SelectedValue);
        DateTime teacherDOB = DateTime.ParseExact(tbxDateOfBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        person.DateofBirth = teacherDOB.ToString("yyyy/MM/dd");
        person.Nationality = tbxNationality.Text;
        person.BirthCertificate = tbxBirthCertificate.Text;
        person.PhoneNo = tbxPhone.Text;
        person.Mobile = tbxMobile.Text;
        person.Email = tbxEmail.Text;
        person.Fax = tbxFax.Text;
        person.PhoneHome = tbxPhnHome.Text;
        person.MobileHome = tbxMobHome.Text;
        person.BloodGroup = Convert.ToInt32(ddlBlood.SelectedValue).ToString();
        person.FatherNId = tbxFatherNID.Text;
        person.MotherNId = tbxMotherNID.Text;
        person.IsFreedomFighter = Convert.ToBoolean(chkFreedom.Checked);
        person.IsTribal = Convert.ToBoolean(chkTribal.Checked);
        person.IsPhysicallyDefect = Convert.ToBoolean(chkPhyDef.Checked);
        person.FatherIncome = tbxFatherIncome.Text!=""? Convert.ToInt32(tbxFatherIncome.Text): 0;
        person.MotherIncome = tbxFatherIncome.Text != "" ? Convert.ToInt32(tbxMotherIncome.Text) : 0; 
        person.FatherPhone = tbxFatherPhn.Text;
        person.MotherPhone = tbxMotherPhn.Text;
        person.FatherQualificationId = (!string.IsNullOrEmpty(ddlFatherEdu.SelectedValue)) ? Convert.ToInt32(ddlFatherEdu.SelectedValue) : 0;
        person.FatherProfessionId = (!string.IsNullOrEmpty(ddlFatherPro.SelectedValue)) ? Convert.ToInt32(ddlFatherPro.SelectedValue) : 0;
        person.MotherQualificationId = (!string.IsNullOrEmpty(ddlMotherEdu.SelectedValue)) ? Convert.ToInt32(ddlMotherEdu.SelectedValue) : 0;
        person.MotherProfessionId = (!string.IsNullOrEmpty(ddlMotherPro.SelectedValue)) ? Convert.ToInt32(ddlMotherPro.SelectedValue) : 0;
        person.NameLocalGuardian1 = "NULL";
        person.NameLocalGuardian2 = "NULL";
        person.LocalGuardian1Mobile = "NULL";
        person.LocalGuardian2Mobile = "NULL";


        person.UserName = tbxUserName.Text;
        return person;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string imgStudent = "";
        string imgFather = "";
        string imgMother = "";       
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
        if (uploderTeacher.HasFile)
        {
            if (ValidImage(uploderTeacher) == true)
            {
                imgStudent = Guid.NewGuid() + "-" + uploderTeacher.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploderTeacher.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Person/" + imgStudent);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }

            else
            {
                MessageController.Show("Select teacher Image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
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
                MessageController.Show("Select father Image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
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

        Person person = new Person();
        person = GetPersonData();
        //Set images
        person.FatherImage = imgFather;
        person.MotherImage = imgMother;
        person.PersonImage = imgStudent;

        MembershipCreateStatus msc;
        PersonId = objPerson.Insert(person);
        

        dtEdu = Education();
        dtTrin = Training();
        if (!string.IsNullOrEmpty(Request.Form["degree"]))
        {
            string[] degree = Request.Form["degree"].Split(',');
            string[] board = Request.Form["board"].Split(',');
            string[] grade = Request.Form["grade"].Split(',');
            string[] year = Request.Form["year"].Split(',');
            string[] subject = Request.Form["subject"].Split(',');
            string[] scale = Request.Form["scale"].Split(',');
            string[] result = Request.Form["division"].Split(',');


            for (int i = 0; i < degree.Length; i++)
            {
                dtEdu.Rows.Add(degree[i], board[i], grade[i], year[i], subject[i], scale[i], result[i]);
            }
        }

        if (!string.IsNullOrEmpty(Request.Form["trainingName"]))
        {
            DateTime? dt = null;     
            string duration = "";
            string[] training = Request.Form["trainingName"].Split(',');
            string[] institute = Request.Form["institute"].Split(',');
            string[] stDate = Request.Form["startDate"].Split(',');
            string[] enDate = Request.Form["endDate"].Split(',');
            string[] topics = Request.Form["topics"].Split(',');
            for (int i = 0; i < training.Length; i++)
            {

                DateTime? dt1 = string.IsNullOrEmpty(stDate[i]) ? dt : DateTime.ParseExact(stDate[i], "dd/MM/yyyy", null);
                DateTime? dt2 = string.IsNullOrEmpty(enDate[i]) ? dt : DateTime.ParseExact(enDate[i], "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(stDate[i]) && !string.IsNullOrEmpty(enDate[i]))
                {
                    
                    DateTime startDate = DateTime.ParseExact(stDate[i], "dd/MM/yyyy", null);
                    DateTime endDate = DateTime.ParseExact(enDate[i], "dd/MM/yyyy", null);
                   
                    TimeSpan ts = endDate - startDate;

                    int days = ts.Days;
                    int month = Math.Abs((endDate.Month - startDate.Month) + 12 * (endDate.Year - startDate.Year));
                    duration = days + " Days";
                    if (month>0)
                    {
                        if (days%30 != 0)
                            duration = month + " Month " + days%30 + " Days";
                        duration = month + " Month";
                    }
                }
                dtTrin.Rows.Add(training[i], institute[i], dt1, dt2, topics[i], duration);
            }
        }
        if (PersonId != -1)
        {
            var date = tbxJoinDate.Text;
            string category = (ddlCatagory.SelectedValue == "0") ? "N/A" : ddlCatagory.SelectedItem.Text; 
            DateTime newDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            try
            {
                objPerson.PresentAddressInsert(PersonId, Convert.ToInt32(Request.Form[ddlPresentDiv.UniqueID]), Convert.ToInt32(Request.Form[ddlPresentDis.UniqueID]), Convert.ToInt32(Request.Form[ddlPresentThana.UniqueID]), tbxPostOffice.Text, tbxPostalCode.Text, tbxPresentAddress.Text);
                objPerson.PermanentAddressInsert(PersonId, Convert.ToInt32(Request.Form[ddlPermanentDiv.UniqueID]), Convert.ToInt32(Request.Form[ddlPermanentDis.UniqueID]), Convert.ToInt32(Request.Form[ddlPermanentThana.UniqueID]), tbxPermanentPost.Text, tbxPermanentPostCode.Text, tbxPermanentAddress.Text);
                objTeacher.InsertPayScale(Convert.ToInt32(ddlPayScaleGrade.SelectedValue), tbxRegNo.Text);

                TeacherId = objTeacher.Insert(PersonId, Convert.ToInt32(ddlDesignation.SelectedValue),Convert.ToInt32(ddlPayScaleGrade.SelectedValue),Convert.ToInt32(ddlLevel.SelectedValue),Convert.ToInt32(ddlCampus.SelectedValue), tbxRegNo.Text, tbxEmpId.Text, tbxNID.Text, newDate, Page.User.Identity.Name, DateTime.Now, true, dtEdu, dtTrin);

                if (TeacherId != -1)
                {
                    
 objTeacher.BankAccountInsert(PersonId, Convert.ToInt32(ddlDesignation.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue),true, tbxBankAccount.Text, Page.User.Identity.Name, DateTime.Now);

                    tbxRegNo.Text = "";
                    Membership.CreateUser(tbxUserName.Text, tbxPassword.Text, tbxEmail.Text);
                    Roles.AddUserToRole(tbxUserName.Text, ddlRole.SelectedItem.Text);
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

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
                                string messageBody = @"Dear " + tbxNameEng.Text + ", Welcome to " + schoolName + ". Your User Id and Password is User Name: " + tbxUserName.Text + "," + " Password: " + tbxPassword.Text;
                                new MessageGateway().SendSms(tbxMobile.Text, messageBody);

                                MessageLog mgslog = new MessageLog();
                                mgslog.SendDate = DateTime.Now;
                                mgslog.MobileNumber = tbxMobile.Text;
                                mgslog.MessageBody = messageBody;
                                mgslog.Status = true;
                                mgslog.Sender = Page.User.Identity.Name;
                                mgslog.MessageType = "Add Teacher";

                                new dalMessageLog().MessageLogInsert(mgslog);

                                msgCredit.SendQuantity += 1;
                                msgCredit.AvailableBalance -= (decimal)0.45;
                                new dalMessageCredit().UpdateCredit(msgCredit);

                            }
                            else
                            {
                                MessageController.Show("Sorry!!!! Your Balance is insufficient. Please purchage SMS.", MessageType.Error, Page);
                                return;
                            }
                        }
                        else
                        {
                            MessageController.Show("Sorry!!!! Credit problem. Please contact with Admin.", MessageType.Error, Page);
                            return;
                        }
                    }
                }
                else
                    MessageController.Show("This PIN No. already exists. Please try anothor.", MessageType.Error, Page);
            }
            catch
            {
                new dalPerson().DeletePresentAdd(PersonId);
                new dalPerson().DeletePermanentAdd(PersonId);
                new dalPerson().DeleteTeacherByPersonId(PersonId);
                new Common().Delete("st_Person", PersonId);
                
                Controller.DeleteFile(Server.MapPath("~/Images/Teacher/" + imgStudent));
                Controller.DeleteFile(Server.MapPath("~/Images/Father/" + imgStudent));
                Controller.DeleteFile(Server.MapPath("~/Images/Mother/" + imgStudent));
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);
            }
        }

        TeacherPin();
    }
    protected bool ValidImage(FileUpload file)
    {
        bool flag = false;
        string exe = Path.GetExtension(file.FileName);
        if (exe == "jpeg" || exe == ".jpg" || exe == ".png")
        {
            if (file.PostedFile.ContentLength < 1048576)
            {
                flag = true;
            }
        }
        return flag;
    }

    #region Dropdown selected index change
    protected void ddlPermanentDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPermanentDistrictByDivision();

    }
    protected void ddlPermanentDis_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPermanentThanaByDistrict();

    }
    protected void ddlPresentDis_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPresentThanaByDistrict();

    }
    protected void ddlPresentDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPresentDistrictByDivision();

    }
    #endregion
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        divCatagory.Visible = ddlDesignation.SelectedItem.Text=="Assistant Teacher";
    }
    public DataTable Education()
    {
        for (int index = dtEdu.Rows.Count - 1; index >= 0; index--)
        {
            dtEdu.Rows.RemoveAt(index);
        }
        for (int index = dtEdu.Columns.Count - 1; index >= 0; index--)
        {
            dtEdu.Columns.RemoveAt(index);
        }
        dtEdu.Columns.Add("Degree", typeof(string));
        dtEdu.Columns.Add("Brd", typeof(string));
        dtEdu.Columns.Add("Grade", typeof(string));
        dtEdu.Columns.Add("Year", typeof(string));
        dtEdu.Columns.Add("Sub", typeof(string));
        dtEdu.Columns.Add("scale", typeof(string));
        dtEdu.Columns.Add("DivId", typeof(string));

        return dtEdu;
    }
    public DataTable Training()
    {
        for (int index = dtTrin.Rows.Count - 1; index >= 0; index--)
        {
            dtTrin.Rows.RemoveAt(index);
        }
        for (int index = dtTrin.Columns.Count - 1; index >= 0; index--)
        {
            dtTrin.Columns.RemoveAt(index);
        }
        dtTrin.Columns.Add("Name", typeof(string));
        dtTrin.Columns.Add("Ins", typeof(string));
        dtTrin.Columns.Add("StD", typeof(DateTime));
        dtTrin.Columns.Add("EndD", typeof(DateTime));
        dtTrin.Columns.Add("Topic", typeof(string));
        dtTrin.Columns.Add("Du", typeof(string));
        return dtTrin;
    }
    protected void btnEducation_Click(object sender, EventArgs e)
    {
        dtEdu.Rows.Add(ddlDegree.SelectedItem.Text, tbxSubject.Text, tbxBoard.Text, tbxYear.Text, tbxGrade.Text, tbxScale.Text, ddlResult.SelectedItem.Text);
        LoadTemporaryData();
        ClearEducation();
    }

    protected void ClearEducation()
    {
        ddlDegree.SelectedValue = "1";
        tbxSubject.Text = "";
        tbxBoard.Text = "";
        tbxYear.Text = "";
        ddlResult.SelectedValue = "1";
        tbxGrade.Text = "";
        tbxScale.Text = "";
    }
    public void ClearTraining()
    {
        tbxTrainingName.Text = "";
        tbxInstitute.Text = "";
        tbxStartDate.Text = "";
        tbxEndDate.Text = "";
        tbxTopics.Text = "";
    }
    protected void btnTraining_Click(object sender, EventArgs e)
    {
        DateTime d1 = tbxStartDate.Text != string.Empty ? Convert.ToDateTime(tbxStartDate.Text) : DateTime.MinValue;
        DateTime d2 = tbxEndDate.Text != string.Empty ? Convert.ToDateTime(tbxEndDate.Text) : DateTime.MinValue;
        TimeSpan tspan = d2 - d1;
        int duration = Convert.ToInt32(tspan.TotalDays.ToString());
        dtTrin.Rows.Add(tbxTrainingName.Text, tbxInstitute.Text, d1, d2, tbxTopics.Text, duration);
        LoadTemporaryData();
        ClearTraining();
    }

    protected void LoadTemporaryData()
    {
        //rptEdu.DataSource = dtEdu;
        //rptEdu.DataBind();

        //rptTraining.DataSource = dtTrin;
        //rptTraining.DataBind();
    }
    protected void btnedu_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Request.Form["degree"]))
        //{
        //    DataTable dttttt = Education();
        //    string[] degree = Request.Form["degree"].Split(',');
        //    string[] board = Request.Form["board"].Split(',');
        //    string[] subject = Request.Form["subject"].Split(',');
        //    string[] year = Request.Form["year"].Split(',');
        //    string[] result = Request.Form["division"].Split(',');
        //    string[] grade = Request.Form["grade"].Split(',');
        //    string[] scale = Request.Form["scale"].Split(',');
        //    for (int i = 0; i < degree.Length; i++)
        //    {
        //        dttttt.Rows.Add(degree[i], subject[i], board[i], year[i], grade[i], scale[i], result[i]);
        //    }
        //}
    }

    protected void GetEducation()
    {
        //if (!string.IsNullOrEmpty(Request.Form["degree"]))
        //{
        //    dtEdu = Education();
        //    string[] degree = Request.Form["degree"].Split(',');
        //    string[] board = Request.Form["board"].Split(',');
        //    string[] subject = Request.Form["subject"].Split(',');
        //    string[] year = Request.Form["year"].Split(',');
        //    string[] result = Request.Form["division"].Split(',');
        //    string[] grade = Request.Form["grade"].Split(',');
        //    string[] scale = Request.Form["scale"].Split(',');
        //    for (int i = 0; i < degree.Length; i++)
        //    {
        //        dtEdu.Rows.Add(degree[i], subject[i], board[i], year[i], grade[i], scale[i], result[i]);
        //    }
        //}
    }

    protected void GetTraining()
    {
        //if (!string.IsNullOrEmpty(Request.Form["trainingName"]))
        //{
        //    dtTrin = Training();
        //    string[] training = Request.Form["trainingName"].Split(',');
        //    string[] institute = Request.Form["institute"].Split(',');
        //    string[] startDate = Request.Form["startDate"].Split(',');
        //    string[] endDate = Request.Form["endDate"].Split(',');
        //    string[] topics = Request.Form["topics"].Split(',');
        //    for (int i = 0; i < training.Length; i++)
        //    {
        //        dtTrin.Rows.Add(training[i], institute[i], startDate[i], endDate[i], topics[i], 0);
        //    }
        //}
    }
    protected void ddlPayScaleGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPayScale();
    }
}
