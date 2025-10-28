using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Enrollment_SubjectToStudent : BasePage
{
    dalStudent obj = new dalStudent();
    dalSubject objSubject = new dalSubject();
    public string[] main;
    public string[] optional;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckRollNoUse();
            LoadAssignment();
            LoadAllStudent();
            LoadSubject();
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        Controller.Delete("er_StudentToClass", Convert.ToInt32(e.CommandArgument));
        LoadSubject();
        LoadAllStudent();
    }
    #region Student Assign
    protected void LoadAssignment()
    {
         ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

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
                criteria = "er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
            else
                criteria += " and er_StudentToClass.MediumId=" + ddlMedium.SelectedValue;
        }
        if (ddlCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
            else
                criteria += " and er_StudentToClass.CampusId=" + ddlCampus.SelectedValue;
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
                criteria = "er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
            else
                criteria += " and er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
        }
        if (ddlShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
            else
                criteria += " and er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
        }
        if (ddlSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
            else
                criteria += " and er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
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

        criteria += " and ss_Student.StatusId=1";
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
        LoadAssignedSubject();
    }

    protected void LoadAssignedSubject()
    {
        DataTable dt = objSubject.GetOptionalSubject(GetCriteria());
        if (dt.Rows.Count > 0)
        {
            rptSubject.DataSource = dt;
            rptSubject.DataBind();
        }
        else
        {
            rptSubject.DataSource = null;
            rptSubject.DataBind();
        }
       
    }

    protected void LoadSubject()
    {
        list1.DataSource = null;
        list1.DataBind();
        DataTable dt = objSubject.GetByClassAndGroupId(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue),Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            list1.Items.Clear();
            list1.DataSource = dt;
            list1.DataBind();
            //rptSubject.DataSource = dt;
            //rptSubject.DataBind();
        }
        else
        {
            //rptSubject.DataSource = null;
            //rptSubject.DataBind();
           // btnAssign.Visible = true;
        }
    }

    protected void LoadAllStudent()
    {
        string criteria = GetCriteria();
        DataTable dt = obj.GetByCriteria(criteria);
        if (dt.Rows.Count > 0)
        {
            MessageController.Clear(this);
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
            
        }
        else
        {
            rptStudent.DataSource = null;
            rptStudent.DataBind();
          //  btnAssign.Visible = false;
            MessageController.Show("No Student Found!!!", MessageType.Warning, Page);
        }
    }

    protected DataTable Assign()
    {
        DataTable dt = new DataTable("dtSubject");
        dt.Columns.Add("StuId", typeof(int));
        dt.Columns.Add("SubId", typeof(int));
        dt.Columns.Add("Optional", typeof(bool));
        return dt;
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        int count = 0;
        if (Request.Form[list2.UniqueID] == null && Request.Form[list3.UniqueID] == null)
        {
            list1.BorderColor = System.Drawing.Color.Red;
            list2.BorderColor = System.Drawing.Color.Red;
            MessageController.Show("Please select al least one subject.", MessageType.Warning, Page);
            return;
        }
        if (Request.Form[list2.UniqueID] != null)
            main = (Request.Form[list2.UniqueID]).Split(',');
        if (Request.Form[list3.UniqueID] != null)
            optional = (Request.Form[list3.UniqueID]).Split(',');
        DataTable dt = Assign();
        foreach (RepeaterItem item in rptStudent.Items)
        {
            CheckBox chkstudentRow = (CheckBox)item.FindControl("chkStudentRow");
            HiddenField hdnStudentToClassId = (HiddenField)item.FindControl("hdnStudentToClassId");
            if (chkstudentRow.Checked)
            {
                count++;
                if (main.Length > 0)
                {
                    for (int i = 0; i < main.Length; i++)
                    {
                        dt.Rows.Add(Convert.ToInt32(hdnStudentToClassId.Value), Convert.ToInt32(main[i]), false);
                    }
                }

                if (optional.Length > 0)
                {
                    for (int j = 0; j < optional.Length; j++)
                    {
                        dt.Rows.Add(Convert.ToInt32(hdnStudentToClassId.Value), Convert.ToInt32(optional[j]), true);
                    }
                }
            }
        }
        if (count > 0)
        {
            objSubject.SubjectToStudentInsert(Page.User.Identity.Name, dt);
            MessageController.Show("Successfully Assigned.", MessageType.Confirmation, Page);
        }
            
        else
        {
            
            MessageController.Show("Please select al least one student.", MessageType.Warning, Page);
        }
            
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllStudent();
        LoadSubject();
    }

    protected void ddlGrouo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllStudent();
        LoadSubject();
    }

    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllStudent();
        LoadSubject();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllStudent();
        LoadSubject();
    }
}
