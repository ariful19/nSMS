using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Administration_ExamAttendence : System.Web.UI.Page
{
    dalStudent obj = new dalStudent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckRollNoUse();
            btnSave.Visible = false;
            tbxDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            Load();
            LoadEmailTemplete();
        }
    }

    #region Load Data

    protected void LoadAttendence()
    {

    }
    protected void Load()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
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
        if (tbxRoll.Text != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.RollNo=" + tbxRoll.Text;
            else
                criteria += " and er_StudentToClass.RollNo=" + tbxRoll.Text;
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
        }
        return criteria;
    }
    protected string GetCriteriass()
    {
        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
            else
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
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
        if (tbxRoll.Text != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.RollNo=" + tbxRoll.Text;
            else
                criteria += " and er_StudentToClass.RollNo=" + tbxRoll.Text;
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
        }
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Date>='" + DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null) + "'";
            else
                criteria += " and ss_Attendence.Date>='" + DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null) + "'";
        }
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Date<='" + DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null) + "'";
            else
                criteria += " and ss_Attendence.Date<='" + DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null) + "'";
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
        if (IsRollNoUse == "False")
        {
            LabelRoll.Visible = false;
            tbxRoll.Visible = false;
        }
        else
        {
            LabelReg.Visible = false;
            tbxReg.Visible = false;
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

        double t = 0;
        string criteria = GetCriteria();
        DataTable dt = obj.GetByCriteria(criteria);
        string criteriass = GetCriteriass();
        int roll = (!string.IsNullOrEmpty(tbxRoll.Text)) ? Convert.ToInt32(tbxRoll.Text) : 0;
        DataTable presentDt = obj.GetAttendenceByCriteria(criteriass, roll);

        double diffTime = TimeDifference(presentDt);
        t = Math.Round(45 - diffTime);
        DataTable finalDt = RemoveDuplicateRows(presentDt, "StudentToClassId");

        if (presentDt.Rows.Count > 0)
        {

            if (diffTime > 45)
            {
                rptStudent.DataSource = presentDt;
                rptStudent.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                rptStudent.DataSource = null;
                rptStudent.DataBind();
                btnSave.Visible = false;
                MessageController.Show("Allready Attendence Counted. You have to Wait " + t + " Minitue for New Attendence.", MessageType.Warning, Page);
            }
        }
        else
        {
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
                MessageController.Show("No Student Found.", MessageType.Error, Page);
            }
        }




    }
    protected Double TimeDifference(DataTable presentDt)
    {
        Double diffTime = 0;
        for (int i = 0; i < presentDt.Rows.Count; i++)
        {
            DateTime timeSt = DateTime.Parse(presentDt.Rows[i]["CreatedTime"].ToString());
            DateTime time = DateTime.Now;
            diffTime = (time - timeSt).TotalMinutes;
        }
        return diffTime;
    }
    protected DataTable Attendence()
    {
        DataTable dt = new DataTable("dtAttendence");
        dt.Columns.Add("StId", typeof(int));
        dt.Columns.Add("RollNo", typeof(int));
        dt.Columns.Add("Present", typeof(bool));
        dt.Columns.Add("TypeAtt", typeof(Int16));
        dt.Columns.Add("NameEng", typeof(string));
        dt.Columns.Add("Mobile", typeof(string));
        dt.Columns.Add("UserName", typeof(string));
        return dt;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        DataTable dt = Attendence();
        DateTime date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        string month = date.Month.ToString();
        string year = ddlYear.SelectedValue;
        int roll = (!string.IsNullOrEmpty(tbxRoll.Text)) ? Convert.ToInt32(tbxRoll.Text) : 0;
        string criteriass = GetCriteriass();
        DataTable presentDt = obj.GetAttendenceByCriteria(criteriass, roll);
        double diffTime = TimeDifference(presentDt);
        double t = Math.Round(45 - diffTime);
        DataTable finalDt = RemoveDuplicateRows(presentDt, "StudentToClassId");

        if (rdList.SelectedValue != "")
        {
            if (finalDt.Rows.Count > 0)
            {

                if (diffTime > 45)
                {
                    foreach (RepeaterItem item in rptStudent.Items)
                    {
                        CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
                        Label hdnStudentId = (Label)item.FindControl("hdnStudentId");
                        Label hdnMobile = (Label)item.FindControl("hdnMobile");
                        Label lblName = (Label)item.FindControl("lblName");
                        Label hdnUser = (Label)item.FindControl("hdnUser");
                        Label lblRoll = (Label)item.FindControl("lblRoll");
                        string name = string.IsNullOrEmpty(lblName.Text) ? "" : lblName.Text;
                        string mobile = string.IsNullOrEmpty(hdnMobile.Text) ? "" : hdnMobile.Text;
                        string userName = string.IsNullOrEmpty(hdnUser.Text) ? "" : hdnUser.Text;
                        string rollNo = string.IsNullOrEmpty(lblRoll.Text) ? "" : lblRoll.Text;
                        if (hdnStudentId.Text != "" && name != "" && rollNo != "")
                            dt.Rows.Add(Convert.ToInt32(hdnStudentId.Text), Convert.ToInt32(rollNo), Convert.ToBoolean(chkRow.Checked), Convert.ToInt32(rdList.SelectedValue), name, mobile, userName);

                    }
                    if (dt.Rows.Count > 0)
                    {
                        obj.AttendenceInsert(dt, year, month, Page.User.Identity.Name, DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null));

                        if (chkSMS.Checked)
                        {
                            for (int i = 0; dt.Rows.Count > i; i++)
                            {

                                SendSms(dt);
                            }
                        }
                        else
                        {
                            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                        }            
                    }
                    else
                    {
                        MessageController.Show("No record found to save.", MessageType.Warning, Page);
                    }

                }
                else
                {
                    rptStudent.DataSource = null;
                    rptStudent.DataBind();
                    btnSave.Visible = false;
                    MessageController.Show("Allready Attendence Counted. You have to Wait " + t + " Minitue for New Attendence.", MessageType.Warning, Page);
                }
            }
            else
            {
                foreach (RepeaterItem item in rptStudent.Items)
                {
                    CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
                    Label hdnStudentId = (Label)item.FindControl("hdnStudentId");
                    Label hdnMobile = (Label)item.FindControl("hdnMobile");
                    Label lblName = (Label)item.FindControl("lblName");
                    Label lblRoll = (Label)item.FindControl("lblRoll");
                    Label hdnUser = (Label)item.FindControl("hdnUser");


                    string name = string.IsNullOrEmpty(lblName.Text) ? "" : lblName.Text;
                    string mobile = string.IsNullOrEmpty(hdnMobile.Text) ? "" : hdnMobile.Text;
                    string userName = string.IsNullOrEmpty(hdnUser.Text) ? "" : hdnUser.Text;
                    string rollNo = string.IsNullOrEmpty(lblRoll.Text) ? "" : lblRoll.Text;
                    if (hdnStudentId.Text != "" && name != "" && rollNo != "")
                        dt.Rows.Add(Convert.ToInt32(hdnStudentId.Text), Convert.ToInt32(rollNo), Convert.ToBoolean(chkRow.Checked), Convert.ToInt32(rdList.SelectedValue), name, mobile, userName);

                }
                if (dt.Rows.Count > 0)
                {
                    obj.AttendenceInsert(dt, year, month, Page.User.Identity.Name, DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null));

                    if (Validation())
                    {
                        SendSms(dt);
                    }
                    else
                    {
                        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                    }        
                }
                else
                {
                    MessageController.Show("No record found to save.", MessageType.Warning, Page);
                }
            }

        }
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

    public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
    {
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();

        //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
        //And add duplicate item value in arraylist.
        foreach (DataRow drow in dTable.Rows)
        {
            if (hTable.Contains(drow[colName]))
                duplicateList.Add(drow);
            else
                hTable.Add(drow[colName], string.Empty);
        }

        //Removing a list of duplicate items from datatable.
        foreach (DataRow dRow in duplicateList)
            dTable.Rows.Remove(dRow);

        //Datatable which contains unique records will be return as output.
        return dTable;
    }
    protected void LoadEmailTemplete()
    {
        string criteria = "Variable='Absent' or Variable='Present'";

        if (!string.IsNullOrEmpty(ddlClass.SelectedValue))
        {
            DataTable dt = new dalSMS().GetByCriteria(criteria);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Variable"].ToString() == "Present")
                    {
                        lblSubject.Text = row["Name"].ToString();
                        lblBody.Text = row["Message"].ToString();
                    }
                    else
                    {
                        lblSub.Text = row["Name"].ToString();
                        lblbdy.Text = row["Message"].ToString();
                    }
                }
            }
        }
    }
    protected void SendSms(DataTable dt)
    {
        string messageBody = "";
        string errorMgs = "";
        MessageCredit msgCredit = new MessageCredit();
        for (int i = 0; dt.Rows.Count > i; i++)
        {
            DataTable dts = new dalMessageCredit().GetAllMessageCredit();
            string name = dt.Rows[i]["NameEng"].ToString();
            string mobile = dt.Rows[i]["Mobile"].ToString();
            string rollNo = dt.Rows[i]["RollNo"].ToString();
            string userName = dt.Rows[i]["UserName"].ToString();
            string presntStatus = dt.Rows[i]["Present"].ToString();
            string type = dt.Rows[i]["TypeAtt"].ToString();
            if (!String.IsNullOrEmpty(mobile))
            {
                if (dts.Rows.Count > 0)
                {
                    msgCredit.Id = Convert.ToInt32(dts.Rows[0]["Id"]);
                    msgCredit.SendQuantity = Convert.ToInt32(dts.Rows[0]["SendQuantity"]);
                    msgCredit.AvailableBalance = Convert.ToDecimal(dts.Rows[0]["AvailableBalance"]);

                    if (msgCredit.AvailableBalance > (decimal)0.44)
                    {
                        if (rdlSMS.SelectedValue != "")
                            if (rdlSMS.SelectedValue == "1")
                            {
                                if (presntStatus == "True")
                                {
                                    messageBody = GetPlainTextFromHtml(lblBody.Text.Replace("@Name", name)).Replace("@UserName", userName);
                                }
                                else if (presntStatus == "False")
                                {
                                    messageBody = GetPlainTextFromHtml(lblbdy.Text.Replace("@Name", name)).Replace("@UserName", userName);
                                }
                            }
                            else if (rdlSMS.SelectedValue == "2")
                            {
                                if (presntStatus == "False")
                                {
                                    messageBody = GetPlainTextFromHtml(lblbdy.Text.Replace("@Name", name)).Replace("@UserName", userName);
                                }
                            }
                            else
                            {
                                if (presntStatus == "True")
                                {
                                    messageBody = GetPlainTextFromHtml(lblBody.Text.Replace("@Name", name)).Replace("@UserName", userName);
                                }
                            }
                        if (messageBody != "")
                        {
                            new MessageGateway().SendSms(mobile, messageBody);
                            MessageController.Show("Attendence Save & Message Send successfully!!!", MessageType.Confirmation, Page);
                            MessageLog mgslog = new MessageLog();
                            mgslog.SendDate = DateTime.Now;
                            mgslog.MobileNumber = mobile;
                            mgslog.MessageBody = messageBody;
                            mgslog.Status = true;
                            mgslog.Sender = Page.User.Identity.Name;
                            mgslog.MessageType = "Attendence";

                            new dalMessageLog().MessageLogInsert(mgslog);


                            msgCredit.SendQuantity += 1;
                            msgCredit.AvailableBalance -= (decimal)0.45;
                            new dalMessageCredit().UpdateCredit(msgCredit);
                        }
                        else
                        {
                            MessageController.Show("Attendence Save successfully!!!", MessageType.Confirmation, Page);
                        }

                    }
                    else
                    {
                        MessageController.Show("Attendence Save.. But Your Balance is insufficient to send SMS. Please purchage SMS.", MessageType.Warning, Page);
                        return;
                    }
                }
                else
                {
                    MessageController.Show("Attendence Save.. But Credits problem to send SMS. Please contact with Admin.", MessageType.Warning, Page);
                    return;
                }
            }
            else
            {
                errorMgs += name + " Roll No: " + rollNo + ", has no Mobile Number to send SMS.<br>";
                Label7.Text = errorMgs;
                Label7.ForeColor = Color.Red;
            }
        }
    }
    protected bool Validation()
    {
        if (chkSMS.Checked)
        {
            if (rdlSMS.SelectedValue != "")
                return true;
        }
        return false;
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
    protected void rptStudentRoll_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

}