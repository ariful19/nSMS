using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Enrollment_StudentToClass : BasePage
{
    dalStudent obj = new dalStudent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckRollNoUse();
            LoadAssignment();
            LoadAllStudent();
            
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            obj.UnassignStudent(Convert.ToInt32(e.CommandArgument));
            MessageController.Show("Student Unassign successfully!!!", MessageType.Confirmation, Page);
            //Controller.Delete("er_StudentToClass", Convert.ToInt32(e.CommandArgument));
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("statement conflicted with the REFERENCE constraint"))
            {
                lblErrorMessage.Text = "You can not delete this student, better to edit this.";
            }
            else
            {
                lblErrorMessage.Text = string.Empty;
            }
        }

        LoadAssign();
        LoadAllStudent();
    }

    #region Student Assign
    protected void LoadAssignment()
    {
        //ddlYear.DataSource = new dalYear().GetDefault();
        //ddlYear.DataBind();

        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

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
                criteria = "er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
            else
                criteria += " and er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
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
                criteria = "RollNo=" + tbxRoll.Text;
            else
                criteria += " and RollNo=" + tbxRoll.Text;
        }
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
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
    protected void rptCurrent_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
    protected void rptUnassign_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            if (IsRollNoUse == "False")
            {
                var column = e.Item.FindControl("thRoll") as Control;
                column.Visible = false;
            }

        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (IsRollNoUse == "False")
            {

                HtmlTableCell tdTableCell = (HtmlTableCell)e.Item.FindControl("tdRoll");
                tdTableCell.Visible = false;
            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadAssign();
        //Response.Redirect("~\\Pages\\Enrollment\\StudentToClass.aspx");
    }
    protected void LoadAssign()
    {
        MessageController.Clear(this);
        rptCurrent.DataSource = null;
        string criteria = GetCriteria();
        DataTable dt = obj.GetByCriteria(criteria);
        if (dt.Rows.Count > 0)
        {
            rptCurrent.DataSource = dt;
            rptCurrent.DataBind();
        }
        else
        {
            rptCurrent.DataSource = null;
            rptCurrent.DataBind();
            MessageController.Show("No Data Found!!!", MessageType.Warning, Page);

        }
        LoadAllStudent();
    }

    protected void LoadAllStudent()
    {
        DataTable dt = obj.GetUnassignStudent(Convert.ToInt32(ddlYear.SelectedValue));
        rptUnassign.DataSource = dt;
        rptUnassign.DataBind();
    }

    protected DataTable Assign()
    {
        DataTable dt = new DataTable("dtAssign");
        dt.Columns.Add("StId", typeof(int));
        return dt;
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (ValidateAssignStudent())
        {
            if (CheckExistingRollNumber())
            {
                List<StudentToClass> lstStudentToClass = new List<StudentToClass>();
                DataTable dt = Assign();
                foreach (RepeaterItem item in rptUnassign.Items)
                {
                    CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
                    HiddenField hdnId = (HiddenField)item.FindControl("hdnStudentId");
                    TextBox txtClassRoll = (TextBox)item.FindControl("txtStudentID");

                    if (chkRow.Checked)
                    {
                        int rollNo = (string.IsNullOrEmpty(txtClassRoll.Text) ? 0 : Convert.ToInt32(txtClassRoll.Text));
                        StudentToClass studentToClass = new StudentToClass();
                        studentToClass.StudentId = Convert.ToInt32(hdnId.Value);
                        studentToClass.RollNo = rollNo;
                        studentToClass.Year = ddlYear.SelectedValue;
                        studentToClass.MediumId = Convert.ToInt32(ddlMedium.SelectedValue);
                        studentToClass.ClassId = Convert.ToInt32(ddlClass.SelectedValue);
                        studentToClass.CampusId = Convert.ToInt32(ddlCampus.SelectedValue);
                        studentToClass.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
                        studentToClass.ShiftId = Convert.ToInt32(ddlShift.SelectedValue);
                        studentToClass.SectionId = Convert.ToInt32(ddlSection.SelectedValue);
                        studentToClass.CreatedBy = Page.User.Identity.Name;
                        lstStudentToClass.Add(studentToClass);

                        dt.Rows.Add(Convert.ToInt32(hdnId.Value));
                    }
                }
                if (lstStudentToClass.Count > 0)
                {
                    obj.InsertStudentToClassInformation(lstStudentToClass,false);
                   
                    LoadAssignment();
                    LoadAssign();
                    LoadAllStudent();
                    MessageController.Show("Student Assign Successfully", MessageType.Information, Page);
                }
                else
                {
                    MessageController.Show("No Data Found to Insert.", MessageType.Warning, Page);
                }
            }
        }
    }
    private bool ValidateAssignStudent()
    {
        bool result = true;
        if(tbxRoll.Visible)
        foreach (RepeaterItem item in rptUnassign.Items)
        {
            CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
            HiddenField hdnId = (HiddenField)item.FindControl("hdnStudentId");
            TextBox txtClassRoll = (TextBox)item.FindControl("txtStudentID");

            if (chkRow.Checked)
            {
                if (string.IsNullOrEmpty(txtClassRoll.Text))
                {
                    txtClassRoll.BackColor = Color.Red;
                    result = false;
                }
                else
                {
                    txtClassRoll.BackColor = Color.White;
                }
            }
        }
        if (!result)
        {
            MessageController.Show("You need to enter student roll.", MessageType.Warning, Page);
        }

        return result;
    }
    private bool CheckExistingRollNumber()
    {
        bool result = true;
        Dictionary<int, int> dicStudents = new Dictionary<int, int>();

        //check duplicate from database.
        if (tbxRoll.Visible)
        foreach (RepeaterItem item in rptUnassign.Items)
        {
            CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
            HiddenField hdnId = (HiddenField)item.FindControl("hdnStudentId");
            TextBox txtClassRoll = (TextBox)item.FindControl("txtStudentID");

            if (chkRow.Checked)
            {
                DataTable dt = obj.GetStudentToClassByRollNo(ddlYear.SelectedValue,Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(txtClassRoll.Text));
                if (dt.Rows.Count > 0)
                {
                    result = false;
                }
            }
        }


        //check duplicate in control.
        int value = 0;
        if (tbxRoll.Visible)
        for (int i = 0; i < rptUnassign.Items.Count; i++)
        {
            TextBox txtClassRoll = (TextBox)rptUnassign.Items[i].FindControl("txtStudentID");
            CheckBox chkRow = (CheckBox)rptUnassign.Items[i].FindControl("chkrow");
            if (chkRow.Checked)
            {
                value = Convert.ToInt32(txtClassRoll.Text);

                for (int j = i + 1; j < rptUnassign.Items.Count; j++)
                {
                    CheckBox chkRow1 = (CheckBox)rptUnassign.Items[j].FindControl("chkrow");
                    TextBox txtClassRoll1 = (TextBox)rptUnassign.Items[j].FindControl("txtStudentID");
                    if (chkRow1.Checked)
                    {
                        if (value == Convert.ToInt32(txtClassRoll1.Text))
                        {
                            txtClassRoll.BackColor = Color.Yellow;
                            txtClassRoll1.BackColor = Color.Yellow;
                            result = false;
                        }
                        else
                        {
                            txtClassRoll1.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        if (!result)
        {
            MessageController.Show("Duplicate roll number!", MessageType.Warning, Page);
        }

        return result;
    }

    Regex regex = new Regex(@"[1-9]");

    private bool IsInteger(string str)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (!regex.IsMatch(str))
            {
                return false;
            }
            return true;
        }
        catch (Exception)
        {
            MessageController.Show("Please input Roll no!!!", MessageType.Warning, Page);
        }
        return false;
    }
}
