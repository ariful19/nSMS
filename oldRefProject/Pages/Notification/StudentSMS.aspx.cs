using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Notification_StudentSMS : System.Web.UI.Page
{
    dalStudent obj = new dalStudent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Visible = false;
            Load();
            LoadEmailTemplete();
        }
    }

    protected void Load()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();

        ddlEmailTemplte.DataSource = new dalSMS().GetNotDefault();
        ddlEmailTemplte.DataBind();

    }
    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "Year=" + ddlYear.SelectedValue;
                criteria += " Year=" + ddlYear.SelectedValue;
        } 
        return criteria;
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //string criteria = "";
        //DataTable dt = obj.GetByStudentCriteria(criteria);
        LoadAllStudent();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        try
        {
            List<int> lstPersonIds = new List<int>();
            
                for (int i = 0; i < rptUnassign.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptUnassign.Items[i].FindControl("chkrow");
                    if (chk.Checked)
                    {
                        HiddenField lblReportID = (HiddenField)rptUnassign.Items[i].FindControl("hdnPersonId");
                        lstPersonIds.Add(Convert.ToInt32(lblReportID.Value));
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

                    if (msgCredit.AvailableBalance > (decimal)0.44)
                    {
                        if (!String.IsNullOrEmpty(lblBody.Text))
                        {
                            string messageBody = GetPlainTextFromHtml(lblBody.Text.Replace("@Name", name)).Replace("@UserName", userName);
                            new MessageGateway().SendSms(mobile, messageBody);
                            MessageController.Show("Message Send successfully!!!", MessageType.Confirmation, Page);

                            MessageLog mgslog = new MessageLog();
                            mgslog.SendDate = DateTime.Now;
                            mgslog.MobileNumber = mobile;
                            mgslog.MessageBody = messageBody;
                            mgslog.Status = true;
                            mgslog.Sender = Page.User.Identity.Name;
                            mgslog.MessageType = "Notification";

                            new dalMessageLog().MessageLogInsert(mgslog);


                            msgCredit.SendQuantity += 1;
                            msgCredit.AvailableBalance -= (decimal)0.45;
                            new dalMessageCredit().UpdateCredit(msgCredit);
                        }
                        else
                        {
                            MessageController.Show("Sorry!!!! Your can not select any SMS Template..", MessageType.Error, Page);
                            return;
                        }
                        LoadAllStudent();
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
    protected void LoadEmailTemplete()
    {
        if (!string.IsNullOrEmpty(ddlEmailTemplte.SelectedValue))
        {
            DataTable dt = new dalSMS().GetById(Convert.ToInt32(ddlEmailTemplte.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                lblSubject.Text = dt.Rows[0]["Name"].ToString();
                lblBody.Text = dt.Rows[0]["Message"].ToString();
            }
        }
    }

    protected void LoadAllStudent()
    {
        DataTable dt = obj.GetUnassignStudent(Convert.ToInt32(ddlYear.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            rptUnassign.DataSource = dt;
            rptUnassign.DataBind();
            btnSave.Visible = true;
        }
        else
        {
            rptUnassign.DataSource = null;
            rptUnassign.DataBind();
            btnSave.Visible = false;
            MessageController.Show("No Student Found!!!", MessageType.Warning, Page);
        }
    }

}