using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Setting_SiteConfiguration : BasePage
{
    dalSetting obj = new dalSetting();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string ss = Common.SessionInfo.Panel;
            LoadSchoolInfo();
            LoadNotification();
            LoadGetway();
            LoadGeneralSetting();
        }
    }

    #region Load Section
    protected void LoadSchoolInfo()
    {
        DataTable dt = new Common().GetAll("bs_SchoolInformation");
        if (dt.Rows.Count > 0)
        {
            tbxSchoolInfo.Text = dt.Rows[0]["Name"].ToString();
            tbxSchoolCode.Text = dt.Rows[0]["Code"].ToString();
            tbxEstBy.Text = dt.Rows[0]["EstablishedBy"].ToString();
            tbxEstd.Text = dt.Rows[0]["EstablishedYear"].ToString();
            tbxDetails.Text = dt.Rows[0]["Description"].ToString();
            tbxAddress.Text = dt.Rows[0]["Address"].ToString();
        }
    }
    protected void LoadNotification()
    {
        DataTable dt = new Common().GetAll("bs_Notification");
        rptNotification.DataSource = dt;
        rptNotification.DataBind();
    }
    protected void LoadGetway()
    {
        DataTable dt = new Common().GetAll("bs_SMSGetway");
        if(dt.Rows.Count>0)
        {
            tbxUrl.Text = dt.Rows[0]["URL"].ToString();
            tbxUserName.Text = dt.Rows[0]["UserName"].ToString();
            tbxPassword.Text = EncryptionDecryption.EncryptDecryptString(dt.Rows[0]["PassWord"].ToString());
        }
    }
    protected void LoadGeneralSetting()
    {
        DataTable dt = new Common().GetAll("bs_GeneralSetting");
        if (dt.Rows.Count > 0)
        {
            ddlTheme.SelectedItem.Text=dt.Rows[0]["Theme"].ToString();
            ddldateformat.SelectedItem.Text = dt.Rows[0]["DateFormat"].ToString();
            ddlTimeZone.SelectedValue = dt.Rows[0]["TimeZone"].ToString();
            ddlButton.SelectedValue= dt.Rows[0]["Button"].ToString();
            ddlPanel.SelectedValue = dt.Rows[0]["Panel"].ToString();
        }
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
    protected void btnSchoolInfo_Click(object sender, EventArgs e)
    {
        string imgSchool = "";


        #region ImageUpload

        if (uploderStudent.HasFile)
        {
            if (ValidImage(uploderStudent) == true)
            {
                imgSchool = Guid.NewGuid() + "-" + uploderStudent.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(uploderStudent.FileContent);
                System.Drawing.Image image2 = Controller.resizeImage(image, new Size(140, 140));
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                string MediumImagePath = Server.MapPath("~/Images/Common/" + imgSchool);
                image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
            }
            else
            {
                MessageController.Show("Select Schoool image .jpg/.png/.jpeg less than 1 mb", MessageType.Error, Page);
                return;
            }
        }
        #endregion ImageUpload

        SchoolInfo schoolInfo = new SchoolInfo();
        schoolInfo.Name = tbxSchoolInfo.Text;
        schoolInfo.Code = tbxSchoolCode.Text;
        schoolInfo.Address = tbxAddress.Text;
        schoolInfo.Logo = imgSchool;
        schoolInfo.EstablishedBy = tbxEstBy.Text;
        schoolInfo.EstablishedYear = tbxEstd.Text;
        schoolInfo.Description = tbxDetails.Text;
        try
        {
            obj.UpdateSchoolInfo(schoolInfo);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        catch (Exception ex)
        {
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void btnNotification_Click(object sender, EventArgs e)
    {
        if (btnNotification.Text == "Save" || btnNotification.Text == "সংরক্ষণ করুন")
        {
            obj.InsertNotification(tbxTitle.Text, Convert.ToBoolean(chkEmail.Checked), Convert.ToBoolean(chkSMS.Checked));
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
    }
    protected void btnGetway_Click(object sender, EventArgs e)
    {
        try
        {
            obj.UpdateSMSGetway(tbxUrl.Text, tbxUserName.Text, EncryptionDecryption.EncryptDecryptString(tbxPassword.Text));
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        catch
        {
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Information, Page);
        }
    }
    protected void btnGeneral_Click(object sender, EventArgs e)
    {
        try
        {
          //  Common.SessionInfo = null;
            obj.UpdateGeneralSetting(ddlTheme.SelectedItem.Text,ddldateformat.SelectedItem.Text,ddlTimeZone.SelectedValue,ddlButton.SelectedValue,ddlPanel.SelectedValue);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        catch
        {
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Information, Page);
        }
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetNotificationById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxTitle.Text = dt.Rows[0]["Title"].ToString();
            chkEmail.Checked = dt.Rows[0]["SendEmail"].ToString() == "True" ? true : false;
            chkSMS.Checked = dt.Rows[0]["SendSMS"].ToString() == "True" ? true : false;
        }
    }
}