
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

public partial class Pages_User_TeacherProfile : BasePage
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


            lblinfo.Text = "Teacher Information";
            pnlForTeacher.Visible = true;
        if (!IsPostBack)
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
                        lblinfo.Text = "Teacher Information";
                    }
                }
                else
                {
                    UserName = Page.User.Identity.Name;
                    ID = Controller.PersonIdByUserName(UserName);
                }
                if (!Page.User.IsInRole("Student") && !Page.User.IsInRole("Admin"))
                {
                    UserName = Request.QueryString["UserName"];
                    ID = Controller.PersonIdByUserName(UserName);
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


    #region Load Profile
    protected void LoadProfile()
    {
        int roleID = 0;
        DataTable dt = objPerson.GetByUserName(UserName);
        DataTable rdt = new dalRole().GetIdByUserName(UserName);

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
            if (dt.Rows[0]["DateofBirth"].ToString() != "")
                lblDoB.Text = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString()).ToString("dd/MM/yyyy");
            lblPhn.Text = dt.Rows[0]["PhoneNo"].ToString();
            lblMob.Text = dt.Rows[0]["Mobile"].ToString();
            lblPhnHome.Text = dt.Rows[0]["PhoneHome"].ToString();
            lblMobHome.Text = dt.Rows[0]["MobileHome"].ToString();

            lblJoinDate.Text = Convert.ToDateTime(dt.Rows[0]["Joindate"].ToString()).ToString("dd/MM/yyyy");
            lblEmail.Text = dt.Rows[0]["Email"].ToString();
            lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblNationality.Text = dt.Rows[0]["Nationality"].ToString();
            lblNationalId.Text = dt.Rows[0]["NId"].ToString();

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

            #region CheckedBox

            string isFreedomFighter = dt.Rows[0]["IsFreedomFighter"].ToString();
            string isTribal = dt.Rows[0]["IsTribal"].ToString();
            string isPhysicallyDefect = dt.Rows[0]["IsPhysicallyDefect"].ToString();

            if (isFreedomFighter == "True")
            {
                chkFreedom.Checked = true;
            }
            if (isTribal == "True")
            {
                chkTribal.Checked = true;

            }
            if (isPhysicallyDefect == "True")
            {
                chkPhyDef.Checked = true;
            }

            #endregion
        }


            lblReg.Text = "Teacher Pin";
            DataTable dtTeacher = objTeacher.GetByPersonId(ID);

            if (dtTeacher.Rows.Count > 0)
            {
                Session["TeacherID"] = dtTeacher.Rows[0]["Id"];
                lblRegNo.Text = dtTeacher.Rows[0]["TeacherPin"].ToString();
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
        if (lblinfo.Text == "Teacher Information")
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
                    lblinfo.Text = "Teacher Information";
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
        if (techerId != 0)
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

