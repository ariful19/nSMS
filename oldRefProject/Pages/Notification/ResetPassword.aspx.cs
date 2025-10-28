using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_ResetPassword : System.Web.UI.Page
{
    dalStudent obj = new dalStudent();
    dalEmailTemplete objEmail = new dalEmailTemplete();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckRollNoUse();
            btnSave.Visible = false;
            Load();
            LoadEmailTemplete();
        }
    }

    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();

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

        ddlEmailTemplte.DataSource = new dalSMS().GetDefault();
        ddlEmailTemplte.DataBind();

        rptTeacher.DataSource = (DataTable)new dalTeacher().GetAllTeacher();
        rptTeacher.DataBind();

    }
    protected void LoadEmailTemplete()
    {
        if (!string.IsNullOrEmpty(ddlEmailTemplte.SelectedValue))
        {
            DataTable dt = new dalSMS().GetDefaultById(Convert.ToInt32(ddlEmailTemplte.SelectedValue));
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
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedValue;
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
        try
        {
            List<int> lstPersonIds = new List<int>();
            if (Convert.ToInt32(rdList.SelectedValue) == 1)
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
            else
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
                DataTable udt = new dalPerson().GetByPersonID(personID);
                string name = udt.Rows[0]["NameEng"].ToString();
                string mobile = udt.Rows[0]["Mobile"].ToString();
                string userName = udt.Rows[0]["UserName"].ToString();

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
                        if (u != null)
                        {
                            string pass = u.ResetPassword();

                            string messageBody = GetPlainTextFromHtml(lblBody.Text.Replace("@Name", name)).Replace("@UserName", userName).Replace("@Password", pass);

                            var text = HttpUtility.HtmlEncode (messageBody);
                            new MessageGateway().SendSms(mobile, text);                            

                            MessageController.Show("Message Send successfully!!!", MessageType.Confirmation, Page);

                            MessageLog mgslog = new MessageLog();
                            mgslog.SendDate = DateTime.Now;
                            mgslog.MobileNumber = mobile;
                            mgslog.MessageBody = messageBody;
                            mgslog.Status = true;
                            mgslog.Sender = Page.User.Identity.Name;
                            mgslog.MessageType = "Reset Password";

                            new dalMessageLog().MessageLogInsert(mgslog);

                        }       
                        msgCredit.SendQuantity += 1;
                        msgCredit.AvailableBalance -= (decimal)0.55;
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
        catch (Exception ex)
        {
            MessageController.Show("Message Not Send!!!", MessageType.Error, Page);
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
}

