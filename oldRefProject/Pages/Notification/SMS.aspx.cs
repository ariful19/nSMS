using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_SMS : System.Web.UI.Page
{
    dalStudent obj = new dalStudent();
    dalEmailTemplete objEmail = new dalEmailTemplete();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (!IsPostBack)
        {
            CheckRollNoUse();
            btnSave.Visible = false;
            Load();
            LoadEmailTemplete();
            Clear();
        }
    }

    private void Clear()
    {
        lblErrorH.Text = "";
        lblError.Text = "";
    }

    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

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

        ddlEmailTemplte.DataSource = new dalSMS().GetNotDefault();
        ddlEmailTemplte.DataBind();

        rptTeacher.DataSource = (DataTable)new dalTeacher().GetAllTeacherByYearCampus(Convert.ToInt32(ddlCampus.SelectedValue));
        rptTeacher.DataBind();

       // rptAllStudent.DataSource = (DataTable)new dalStudent().GetAllStudents();

        rptAllStudent.DataSource = (DataTable)new dalStudent().GetAllStudentByCriteria(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue));
        rptAllStudent.DataBind();

    }
    protected void LoadEmailTemplete()
    {
        if (!string.IsNullOrEmpty(ddlEmailTemplte.SelectedValue))
        {
            DataTable dt = new dalSMS().GetById(Convert.ToInt32(ddlEmailTemplte.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                //lblSubject.Text = dt.Rows[0]["Subject"].ToString();
                //lblBody.Text = dt.Rows[0]["Body"].ToString();

                lblSubject.Text = dt.Rows[0]["Name"].ToString();
                lblBody.Text = dt.Rows[0]["Message"].ToString();
            }
        }
    }

    #endregion

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
               criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
            else
               criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and CampusId=" + ddlCampus.SelectedValue;
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
        criteria += " and ss_Student.IsActive='True'";
        criteria += " and ss_Student.StatusId= 1";
        return criteria;
    }

    #endregion
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
    protected void CheckRollNoUse()
    {
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            IsRollNoUse = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
        }
        //if (IsRollNoUse == "False")
        //{
        //    LabelRoll.Visible = false;
        //    tbxRoll.Visible = false;
        //}
        //else
        //{
        //    LabelReg.Visible = false;
        //    tbxReg.Visible = false;
        //}

    }
    protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            if (IsRollNoUse == "False")
            {
                Label LabelRoll = (Label)e.Item.FindControl("LabelRoll");
                LabelRoll.Visible = false;
            }
            else
            {
                Label LabelReg = (Label)e.Item.FindControl("LabelReg");
                LabelReg.Visible = false;
            }
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (IsRollNoUse == "False")
            {

                Label lblRoll = (Label)e.Item.FindControl("lblRoll");
                lblRoll.Visible = false;
            }
            else
            {
                Label lblReg = (Label)e.Item.FindControl("lblReg");
                lblReg.Visible = false;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        Clear();
        string criteria = GetCriteria();
        DataTable dt = obj.GetByCriteria(criteria);
        if (dt.Rows.Count > 0)
        {
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
            btnSave.Visible = true;
        }
        else
        {
            rptStudent.DataSource = null;
            rptStudent.DataBind();
            btnSave.Visible = false;
            MessageController.Show("No Student Found!!!", MessageType.Warning, Page);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        Clear();
        string message = "";
        int no = 1;
        try
        {
            List<int> lstPersonIds = new List<int>();
            DataTable dtMMobile = MissingMobileNo();

            if (Convert.ToInt32(rdList.SelectedValue) == 2)
            {
                for (int i = 0; i < rptStudent.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptStudent.Items[i].FindControl("chkrow");
                    if (chk.Checked)
                    {
                        HiddenField lblReportID = (HiddenField)rptStudent.Items[i].FindControl("hdnStudentId");
                        lstPersonIds.Add(Convert.ToInt32(lblReportID.Value));
                    }
                }
            }
            if (Convert.ToInt32(rdList.SelectedValue) == 1)
            {
                for (int i = 0; i < rptAllStudent.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptAllStudent.Items[i].FindControl("chkrow");
                    if (chk.Checked)
                    {
                        HiddenField lblReportID = (HiddenField)rptAllStudent.Items[i].FindControl("hdnStudentId");
                        lstPersonIds.Add(Convert.ToInt32(lblReportID.Value));
                    }
                }            
            }
            if (Convert.ToInt32(rdList.SelectedValue) ==3)
            {
                for (int i = 0; i < rptTeacher.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptTeacher.Items[i].FindControl("chkrow");
                    if (chk.Checked)
                    {
                        HiddenField lblReportID = (HiddenField)rptTeacher.Items[i].FindControl("hdnPersonID");
                        lstPersonIds.Add(Convert.ToInt32(lblReportID.Value));
                    }
                }
            }

            foreach (int personID in lstPersonIds)
            {
                int msgCount = 0;
                decimal smsPrice = (decimal)0.55;
                decimal tPrice = 0;

                DataTable udt = new dalPerson().GetByPersonID(personID);
                string name = udt.Rows[0]["NameEng"].ToString();
                string mobile = udt.Rows[0]["Mobile"].ToString();
                string userName = udt.Rows[0]["UserName"].ToString();
                

                if (mobile=="" || mobile.Length < 11)
                {
                    dtMMobile.Rows.Add(name,mobile,userName);
                    message += no + ": " + name + " has no Mobile No." + "<br>";
                    no++;
                }
                else
                {
                    DataTable dtPass = new dalUser().GetUsedPass(userName);
                    MembershipUser u = Membership.GetUser(userName, false);
                    DataTable dt = new dalMessageCredit().GetAllMessageCredit();
                    MessageCredit msgCredit = new MessageCredit();
                 

                    if (dt.Rows.Count > 0)
                    {
                        msgCredit.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                        msgCredit.SendQuantity = Convert.ToInt32(dt.Rows[0]["SendQuantity"]);
                        msgCredit.AvailableBalance = Convert.ToDecimal(dt.Rows[0]["AvailableBalance"]);
                        if (msgCredit.AvailableBalance >= (decimal)0.55)
                        {
                            
                            string messageBody = GetPlainTextFromHtml(lblBody.Text.Replace("@Name", name)).Replace("@UserName", userName);
                            var text = HttpUtility.HtmlEncode (messageBody);
                            new MessageGateway().SendSms(mobile, text);
                            MessageController.Show("Message Send successfully!!!", MessageType.Confirmation, Page);

                            MessageLog mgslog = new MessageLog();
                            mgslog.SendDate = DateTime.Now;
                            mgslog.MobileNumber = mobile;
                            mgslog.MessageBody = messageBody;
                            mgslog.Status = true;
                            mgslog.Sender = Page.User.Identity.Name;
                            mgslog.MessageType = "Notification";

                            new dalMessageLog().MessageLogInsert(mgslog);

                            msgCount = lblBody.Text.Length <= 160 ? 1 : lblBody.Text.Length > 160 && lblBody.Text.Length <= 320 ? 2 : lblBody.Text.Length > 320 && lblBody.Text.Length <= 480 ? 3 : lblBody.Text.Length > 480 && lblBody.Text.Length <= 640 ? 4 : 5;
                            tPrice = (msgCount == 1) ? smsPrice : (msgCount == 2) ? smsPrice * 2 : (msgCount == 3) ? smsPrice * 3 : (msgCount == 4) ? smsPrice * 4 : smsPrice * 5;
                            msgCredit.SendQuantity += msgCount;
                            msgCredit.AvailableBalance -= tPrice;
                            new dalMessageCredit().UpdateCredit(msgCredit);

                        }
                        else
                        {
                            MessageController.Show("Sorry!!!! Your Balance is insaficient. Please purchage SMS.", MessageType.Error, Page);
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

        }
        catch (Exception ex)
        {
            MessageController.Show("Message Not Send!!!", MessageType.Error, Page);
        }

        if (message != "")
        {
            
           
            lblErrorH.Text = "Mobile No Eror List.";
            lblError.Text = message;
        }
    }

    private DataTable MissingMobileNo()
    {
        DataTable dtMMobileNo = new DataTable();
        dtMMobileNo.Columns.Add("Name", typeof(string));
        dtMMobileNo.Columns.Add("MobileNo", typeof(string));
        dtMMobileNo.Columns.Add("UserName", typeof(string));
        
        //dtMMobileNo.Columns.Add("RegNo", typeof(string));
        //dtMMobileNo.Columns.Add("RollNo", typeof(string));    

        return dtMMobileNo;
    }
    private string GetPlainTextFromHtml(string htmlString)
    {
        string htmlTagPattern = "<.*?>";
        var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        htmlString = regexCss.Replace(htmlString, string.Empty);
        htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
        htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
        htmlString = htmlString.Replace("&nbsp;", string.Empty);

        return htmlString;
    }
    protected void ddlEmailTemplte_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmailTemplete();
    }

    protected void ddlYear_SelectedIndexChange(object sender, EventArgs e)
    {

        rptAllStudent.DataSource = (DataTable)new dalStudent().GetAllStudentByCriteria(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue));
        rptAllStudent.DataBind();
    }

    protected void ddlCampus_SelectedIndexChange(object sender, EventArgs e)
    {
        rptTeacher.DataSource = (DataTable)new dalTeacher().GetAllTeacherByYearCampus(Convert.ToInt32(ddlCampus.SelectedValue));
        rptTeacher.DataBind();

        rptAllStudent.DataSource = (DataTable)new dalStudent().GetAllStudentByCriteria(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue));
        rptAllStudent.DataBind();
    }

}

