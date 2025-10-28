using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            btnSave.Visible = false;
         
            Load();
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
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
            else
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
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

        return criteria;
    }
    protected string GetCriteriass()
    {
        DateTime srtDate = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        string sDate = srtDate.ToString("MM/dd/yyyy");

        string criteria = "";
        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
            else
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedItem.Text;
        }
        //if (ddlYear.SelectedValue != "")
        //{
        //    if (criteria == "")
        //        criteria = "ss_Attendence.Year=" + ddlYear.SelectedItem.Text;
        //    else
        //        criteria += " and ss_Attendence.Year=" + ddlYear.SelectedItem.Text;
        //} 

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
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Date>='" + sDate + "'";
            else
                criteria += " and ss_Attendence.Date>='" + sDate + "'";
        }
        if (tbxDate.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Attendence.Date<='" + sDate + "'";
            else
                criteria += " and ss_Attendence.Date<='" + sDate + "'";
        }

        return criteria;
    }

    #endregion
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
        dt.Columns.Add("Present", typeof(bool));
        dt.Columns.Add("TypeAtt", typeof(Int16));
        return dt;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        DataTable dt = Attendence();
        DateTime date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        string month = date.Month.ToString();
        string year = date.Year.ToString();
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
                        if (hdnStudentId.Text != "")
                            dt.Rows.Add(Convert.ToInt32(hdnStudentId.Text), Convert.ToBoolean(chkRow.Checked), Convert.ToInt32(rdList.SelectedValue));

                    }
                    if (dt.Rows.Count > 0)
                    {
                        obj.AttendenceInsert(dt, year, month, Page.User.Identity.Name, DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null));

                        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                        pnlAssignStudent.Visible = false;
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
                    if (hdnStudentId.Text != "")
                        dt.Rows.Add(Convert.ToInt32(hdnStudentId.Text), Convert.ToBoolean(chkRow.Checked), Convert.ToInt32(rdList.SelectedValue));

                }
                if (dt.Rows.Count > 0)
                {
                    obj.AttendenceInsert(dt, year, month, Page.User.Identity.Name, DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null));

                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                    pnlAssignStudent.Visible = false;
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
}