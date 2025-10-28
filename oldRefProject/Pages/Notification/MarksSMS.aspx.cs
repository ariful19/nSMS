using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_MarksSMS : System.Web.UI.Page
{
    dalStudent obj = new dalStudent();
    dalEmailTemplete objEmail = new dalEmailTemplete();
    dalSubject objSubject = new dalSubject();
    DataTable dtStu = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(this);
       
        if (!IsPostBack)
        {
            CheckRollNoUse();
            btnSave.Visible = false;
            Load();
            LoadEmailTemplete();
            LoadSubject();
            tbxDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
        ddlYear.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

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

        ddlExamType.DataSource = new dalMarks().GetDailyOrWeeklyExam();
        ddlExamType.DataBind();

        ddlEmailTemplte.DataSource = new dalSMS().GetNotDefaultMarks();
        ddlEmailTemplte.DataBind();
    }
    protected void LoadEmailTemplete()
    {
        if (!string.IsNullOrEmpty(ddlEmailTemplte.SelectedValue))
        {
            DataTable dt = new dalSMS().GetMarksSMSById(Convert.ToInt32(ddlEmailTemplte.SelectedValue));
            if (dt.Rows.Count > 0)
            {
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
      
            DateTime srtDate = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
            string tDate = srtDate.ToString("MM/dd/yyyy");
        
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Year=" + ddlYear.SelectedValue;
            else
                criteria += " Year=" + ddlYear.SelectedValue;
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
        if (ddlExamType.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ExamTypeId=" + ddlExamType.SelectedValue;
            else
                criteria += " and ExamTypeId=" + ddlExamType.SelectedValue;
        }
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainDailyMarks.ExamDate>='" + tDate + "'";
            else
                criteria += " and rs_ObtainDailyMarks.ExamDate>='" + tDate + "'";
        }
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "rs_ObtainDailyMarks.ExamDate<='" + tDate + "'";
            else
                criteria += " and rs_ObtainDailyMarks.ExamDate<='" + tDate + "'";
        }        
        if (ddlSubject.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "SubjectId=" + ddlSubject.SelectedValue;
            else
                criteria += " and SubjectId=" + ddlSubject.SelectedValue;
        }
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

                Label lblRoll = (Label)e.Item.FindControl("lblRollNo");
                lblRoll.Visible = false;
            }
            else
            {
                Label lblReg = (Label)e.Item.FindControl("lblReg");
                lblReg.Visible = false;
            }
        }
    }
    protected void LoadSubject()
    {
        DataTable dt = objSubject.GetByClassAndGroupId(Convert.ToInt32(ddlMedium.SelectedValue),Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        ddlSubject.DataSource = dt;
        ddlSubject.DataBind();
    }
   
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (tbxDate.Text != "")
        {
            LoadStudent();
        }
        else
        {
            MessageController.Show("No Date Selected!!!", MessageType.Warning, Page);
        }
        
    }

    private void LoadStudent()
    {
        string criteria = GetCriteria();
        DataTable dt = obj.GetDailyMarksStuByCriteria(criteria);
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
        try
        {
            List<int> lstPersonIds = new List<int>();
            DataTable dts = StudentDailyMarks();

            for (int i = 0; i < rptStudent.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rptStudent.Items[i].FindControl("chkrow");
                if (chk.Checked)
                {
                    Label lblName = (Label)rptStudent.Items[i].FindControl("lblName");
                    Label lblMobile = (Label)rptStudent.Items[i].FindControl("lblMobile");
                    HiddenField hdnUserName = (HiddenField)rptStudent.Items[i].FindControl("hdnUserName");
                    Label lblRollNo = (Label)rptStudent.Items[i].FindControl("lblRollNo");
                    Label lblReg = (Label)rptStudent.Items[i].FindControl("lblReg");
                    HiddenField hdnStudentId = (HiddenField)rptStudent.Items[i].FindControl("hdnStudentId");
                    Label lblDailyMarks = (Label)rptStudent.Items[i].FindControl("lblDailyMarks");
                    HiddenField hdnOutOfMarks = (HiddenField)rptStudent.Items[i].FindControl("hdnOutOfMarks");
                    HiddenField hdnSubject = (HiddenField)rptStudent.Items[i].FindControl("hdnSubject");

                    dts.Rows.Add(lblName.Text, lblMobile.Text, hdnUserName.Value, lblRollNo.Text,lblReg.Text, Convert.ToInt32(hdnStudentId.Value), lblDailyMarks.Text, hdnOutOfMarks.Value, hdnSubject.Value);

                    lstPersonIds.Add(Convert.ToInt32(hdnStudentId.Value));
                }
            }
            SendDailyMarksSMS(lstPersonIds, dts);
            LoadStudent();
            
        }
        catch (Exception ex)
        {
            MessageController.Show("Message Not Send!!!", MessageType.Error, Page);
        }
    }

    private void SendDailyMarksSMS(List<int> lstPersonIds, DataTable dts)
    {
        foreach (DataRow row  in dts.Rows)
        {          
            if(String.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                MessageController.Show("Sorry!!!! Some one have no Mobile Number.", MessageType.Error, Page);
                return;
            }
            string name = row["NameEng"].ToString();
            string mobile = row["Mobile"].ToString();
            string userName = row["UserName"].ToString();
            string getDailyMarks = row["DailyGetMarks"].ToString();
            string outOfMarks = row["OutOfMarks"].ToString();
            string rollNo = row["RollNo"].ToString();
            string regNo = row["RegNo"].ToString();
            string subName = row["SubjectName"].ToString();

            DataTable dtPass = new dalUser().GetUsedPass(userName);
            MembershipUser u = Membership.GetUser(userName, false);

            DataTable dt = new dalMessageCredit().GetAllMessageCredit();
            MessageCredit msgCredit = new MessageCredit();

            if (dt.Rows.Count > 0)
            {
                msgCredit.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                msgCredit.SendQuantity = Convert.ToInt32(dt.Rows[0]["SendQuantity"]);
                msgCredit.AvailableBalance = Convert.ToDecimal(dt.Rows[0]["AvailableBalance"]);

                if (msgCredit.AvailableBalance > (decimal)0.44)
                {
                    string messageBody = GetPlainTextFromHtml(lblBody.Text.Replace("@Name", name)).Replace("@UserName", userName).Replace("@DailyGetMarks ", getDailyMarks).Replace("@OutOfMarks", outOfMarks).Replace("@RollNo", rollNo).Replace("@RegNo", regNo).Replace("@SubjectName ", subName);
                    new MessageGateway().SendSms(mobile, messageBody);
                    MessageController.Show("Message Send successfully!!!", MessageType.Confirmation, Page);



                    MessageLog mgslog = new MessageLog();
                    mgslog.SendDate = DateTime.Now;
                    mgslog.MobileNumber = mobile;
                    mgslog.MessageBody = messageBody;
                    mgslog.Status = true;
                    mgslog.Sender = Page.User.Identity.Name;
                    mgslog.MessageType = "Daily Marks";

                    new dalMessageLog().MessageLogInsert(mgslog);


                    msgCredit.SendQuantity += 1;
                    msgCredit.AvailableBalance -= (decimal)0.45;
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

    public DataTable StudentDailyMarks()
    {
        dtStu.Columns.Add("NameEng", typeof(string));
        dtStu.Columns.Add("Mobile", typeof(string)); 
        dtStu.Columns.Add("UserName", typeof(string));
        dtStu.Columns.Add("RollNo", typeof(string));
        dtStu.Columns.Add("RegNo", typeof(string));
        dtStu.Columns.Add("PersonID", typeof(int));
        dtStu.Columns.Add("DailyGetMarks", typeof(string));
        dtStu.Columns.Add("OutOfMarks", typeof(string));
        dtStu.Columns.Add("SubjectName", typeof(string));      

        return dtStu;
    }


}