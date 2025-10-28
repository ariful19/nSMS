using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Result_Promotion : System.Web.UI.Page
{

    dalStudent objStudent = new dalStudent();
    dalSubject objSubject = new dalSubject();
    dalMarks objMarks = new dalMarks();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PopulateSection(e,);
            Load();
            LoadPromotionData();
            pnlStudent.Visible = false;
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
    }

    protected void LoadPromotionData()
    {
        ddlPromotionYear.DataSource = new dalYear().GetDefault();
        ddlPromotionYear.DataBind();

        ddlPromotionMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlPromotionMedium.DataBind();

        ddlPromotionCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlPromotionCampus.DataBind();

        ddlPromotionClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlPromotionClass.DataBind();

        ddlPromotionGroup.DataSource = new Common().GetAll("bs_Group");
        ddlPromotionGroup.DataBind();

        ddlPromotionShift.DataSource = new Common().GetAll("bs_Shift");
        ddlPromotionShift.DataBind();

        ddlPromotionSection.DataSource = new Common().GetAll("bs_Section");
        ddlPromotionSection.DataBind();
    }

    #endregion

    #region Get Criteria
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
            else
                criteria += "er_StudentToClass.Year=" + ddlYear.SelectedValue;
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

        if (criteria != "")
        {
            criteria += " and IsPromoted='False'";
        }  
      
        return criteria;
    }
    protected string GetCriteriaForPromotion()
    {
        string criteria = "";
        if (ddlPromotionYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlPromotionYear.SelectedValue;
            else
                criteria += "er_StudentToClass.Year=" + ddlPromotionYear.SelectedValue;
        }
        if (ddlPromotionMedium.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "MediumId=" + ddlPromotionMedium.SelectedValue;
            else
                criteria += " and MediumId=" + ddlPromotionMedium.SelectedValue;
        }
        if (ddlPromotionCampus.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "CampusId=" + ddlPromotionCampus.SelectedValue;
            else
                criteria += " and CampusId=" + ddlPromotionCampus.SelectedValue;
        }
        if (ddlPromotionClass.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ClassId=" + ddlPromotionClass.SelectedValue;
            else
                criteria += " and ClassId=" + ddlPromotionClass.SelectedValue;
        }
        if (ddlPromotionGroup.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "GroupId=" + ddlPromotionGroup.SelectedValue;
            else
                criteria += " and GroupId=" + ddlPromotionGroup.SelectedValue;
        }
        if (ddlPromotionShift.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "ShiftId=" + ddlPromotionShift.SelectedValue;
            else
                criteria += " and ShiftId=" + ddlPromotionShift.SelectedValue;
        }

        if (ddlPromotionSection.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "SectionId=" + ddlPromotionSection.SelectedValue;
            else
                criteria += " and SectionId=" + ddlPromotionSection.SelectedValue;
        }

       
           criteria += " and ss_Student.StatusId= 1";
        return criteria;
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void BindData()
    {
        string criteria = GetCriteria();
        //log criteria to a file for debugging
        if (criteria != null)
        {
            //if the dir not exist, create it
            string dir = Server.MapPath("~/Logs");
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            System.IO.File.AppendAllText(Server.MapPath("~/Logs/PromotionLog.txt"), DateTime.Now + ": " + criteria + Environment.NewLine);
        }

        DataTable dt = objStudent.GetStudentForPromotion(criteria);
        if (dt.Rows.Count > 0)
        {
            rptStu.DataSource = dt;
            rptStu.DataBind();
            pnlStudent.Visible = true;
            ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
        }
        else
        {
            MessageController.Show("No student found.", MessageType.Warning, Page);
            pnlStudent.Visible = false;
        }
    }
    protected void Bind()
    {
        string criteria = GetCriteria();

       
        DataTable dt = objStudent.GetStudentForPromotion(criteria);
        if (dt.Rows.Count > 0)
        {
            rptStu.DataSource = dt;
            rptStu.DataBind();
            pnlStudent.Visible = true;
        }
        else
        {
            pnlStudent.Visible = false;
        }
    }
    protected DataTable GetMarks()
    {
        DataTable dt = new DataTable("dtMarks");
        dt.Columns.Add("ObtainMarksId", typeof(int));
        dt.Columns.Add("StudentId", typeof(int));
        dt.Columns.Add("TotalMarks", typeof(double));
        dt.Columns.Add("PreviousRoll", typeof(string));
        dt.Columns.Add("NewRoll", typeof(string));
        return dt;
    }
    protected DataTable UpdateStudent()
    {
        DataTable dtStu = new DataTable("dtMarks");
        dtStu.Columns.Add("MarksId", typeof(int));
        dtStu.Columns.Add("Roll", typeof(int));
        return dtStu;
    }
    private bool Validation()
    {
        int ch = 0;
        for (int i = 0; i < rptStu.Items.Count; i++)
        {
            CheckBox cb = (CheckBox)rptStu.Items[i].FindControl("chkrow");
            if (cb.Checked)
            {
                ch = ch + 1;
            }
        }
        if (ch == 0)
        {

            MessageController.Show("Please select at least one checkbox and Input New Roll!!!", MessageType.Warning, Page);
            return false;
        }

        if (rptStu.Items.Count > 0)
        {
            for (int i = 0; i < rptStu.Items.Count; i++)
            {
                if (((TextBox)rptStu.Items[i].FindControl("tbxNewRoll")).Text == "")
                {

                    MessageController.Show("Please Input New Roll!!!", MessageType.Warning, Page);
                    return false;
                }
            }
        }
        return true;
    }
    protected void ClearAll()
    {
        for (int i = 0; i < rptStu.Items.Count; i++)
        {
            //if (((TextBox)rptStu.Items[i].FindControl("tbxNewRoll")).Text == "") ;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int count = 0;
        double otherMarks = 0;
        double practpractical = 0;
        double objective = 0;
        double subjective = 0;
        double monthlty = 0;
        double attendence = 0;
        double totalMarks = 0;
        string previousRoll = "";
        string newRoll = "";
        int studentToClassId = 0;
        bool ispromotion=true;       
        int execInsert = 0;
        int execUpdate = 0;

            if (CheckExistingRollNumber())
            {
                if (rdlList.SelectedValue == "2")
                {
                    foreach (RepeaterItem item in rptStu.Items)
                    {
                        CheckBox chkrow = (CheckBox) item.FindControl("chkrow");
                        if (chkrow.Checked)
                        {
                            count++;
                            HiddenField hdStudentToClassId = (HiddenField)item.FindControl("hdStudentToClassId");

                            Label lblRollNo = (Label) item.FindControl("lblRollNo");
                            TextBox tbxNewRoll = (TextBox) item.FindControl("tbxNewRoll");

                            if (lblRollNo.Text == "") previousRoll = "";
                            else previousRoll = lblRollNo.Text;

                            newRoll = previousRoll;

                            ////int rollNo = (string.IsNullOrEmpty(tbxNewRoll.Text) ? 0 : Convert.ToInt32(tbxNewRoll.Text));
                            //if (tbxNewRoll.Text == "")
                            //{
                            //    MessageController.Show("Please Input New Roll!!!", MessageType.Warning, Page);

                            //}
                            //else newRoll = tbxNewRoll.Text;

                            studentToClassId = Convert.ToInt32(hdStudentToClassId.Value);
                            execInsert += objMarks.Insert_ProfileLog(studentToClassId, ddlYear.SelectedValue,
                                Convert.ToInt32(ddlMedium.SelectedValue),
                                Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue),
                                Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue),
                                Convert.ToInt32(ddlSection.SelectedValue), previousRoll, ispromotion, subjective,
                                objective,
                                practpractical, otherMarks, attendence, monthlty, totalMarks, newRoll,
                                Page.User.Identity.Name,
                                DateTime.Now);
                                execUpdate += objMarks.PromotionUpdateStudent(ddlPromotionYear.SelectedValue,
                                Convert.ToInt32(ddlPromotionMedium.SelectedValue),
                                Convert.ToInt32(ddlPromotionCampus.SelectedValue),
                                Convert.ToInt32(ddlPromotionClass.SelectedValue),
                                Convert.ToInt32(ddlPromotionGroup.SelectedValue),
                                Convert.ToInt32(ddlPromotionShift.SelectedValue),
                                Convert.ToInt32(ddlPromotionSection.SelectedValue), studentToClassId, Page.User.Identity.Name);
                        }
                    }

                    if (execInsert == execUpdate)
                    {
                        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                        Bind();
                        ClearAll();
                    }
                }
                else
                {

                    List<StudentToClass> lstStudentToClass = new List<StudentToClass>();
                    int doubleEntry = 0;
                    foreach (RepeaterItem item in rptStu.Items)
                    {
                        CheckBox chkRow = (CheckBox) item.FindControl("chkrow");
                        HiddenField hdnId = (HiddenField)item.FindControl("hdnStudentId");
                        HiddenField hdStudentToClassId = (HiddenField)item.FindControl("hdStudentToClassId");
                        TextBox txtClassRoll = (TextBox)item.FindControl("tbxNewRoll");

                        if (chkRow.Checked)
                        {
                            int rollNo = 0;
                            StudentToClass studentToClass = new StudentToClass();
                            studentToClass.StudentToClassID = Convert.ToInt32(hdStudentToClassId.Value);
                            studentToClass.StudentId = Convert.ToInt32(hdnId.Value);
                            studentToClass.RollNo = rollNo;
                            studentToClass.Year = ddlPromotionYear.SelectedValue;
                            studentToClass.MediumId = Convert.ToInt32(ddlPromotionMedium.SelectedValue);
                            studentToClass.ClassId = Convert.ToInt32(ddlPromotionClass.SelectedValue);
                            studentToClass.CampusId = Convert.ToInt32(ddlPromotionCampus.SelectedValue);
                            studentToClass.GroupId = Convert.ToInt32(ddlPromotionGroup.SelectedValue);
                            studentToClass.ShiftId = Convert.ToInt32(ddlPromotionShift.SelectedValue);
                            studentToClass.SectionId = Convert.ToInt32(ddlPromotionSection.SelectedValue);
                            studentToClass.CreatedBy = Page.User.Identity.Name;

                            // check same year registration...
                            string criteria = "er_StudentToClass.StudentId=" + studentToClass.StudentId + " and er_StudentToClass.Year="+ddlPromotionYear.SelectedValue;
                            DataTable dtExists = new dalStudent().GetByCriteria(criteria);
                            if (dtExists.Rows.Count > 0)
                            {
                                doubleEntry ++;
                                break;
                            }
                            else
                            {
                                lstStudentToClass.Add(studentToClass);
                            }
                        }
                    }
                    if (doubleEntry == 0)
                    {
                        if (lstStudentToClass.Count > 0)
                        {
                            new dalStudent().InsertStudentToClassInformation(lstStudentToClass, true);
                            Bind();
                            ClearAll();
                            MessageController.Show("Student Promoted Successfully", MessageType.Information, Page);
                        }
                        else
                        {
                            MessageController.Show("No Data Found to Insert.", MessageType.Warning, Page);
                        }
                    }
                    else
                    {
                        MessageController.Show("Already promoted. You need to Transfer Class.",
                            MessageType.Warning, Page);
                    }

                }
            }
    }
    private bool ValidateAssignStudent()
    {
        int ch = 0;
        for (int i = 0; i < rptStu.Items.Count; i++)
        {
            CheckBox cb = (CheckBox)rptStu.Items[i].FindControl("chkrow");
            if (cb.Checked)
            {
                ch = ch + 1;
            }
        }
        if (ch == 0)
        {

            MessageController.Show("Please select at least one checkbox and Input New Roll!!!", MessageType.Warning, Page);
            return false;
        }

        if (rptStu.Items.Count > 0)
        {
            for (int i = 0; i < rptStu.Items.Count; i++)
            {
                if (((TextBox)rptStu.Items[i].FindControl("tbxNewRoll")).Text == "")
                {

                    MessageController.Show("Please Input New Roll!!!", MessageType.Warning, Page);
                    return false;
                }
            }
        }
        return true;
    }
    private bool CheckExistingRollNumber()
    {
        bool result = true;
        Dictionary<int, int> dicStudents = new Dictionary<int, int>();

        //check duplicate from database.
        string criteria = GetCriteriaForPromotion();

        DataTable dt = objStudent.GetStudentForPromotion(criteria);

            foreach (RepeaterItem item in rptStu.Items)
            {
                CheckBox chkRow = (CheckBox)item.FindControl("chkrow");
                HiddenField hdnStudentId = (HiddenField)item.FindControl("hdnStudentId");
                //TextBox txtClassRoll = (TextBox)item.FindControl("tbxNewRoll");

                if (chkRow.Checked)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["StudentId"].ToString() == hdnStudentId.Value)
                        {
                            result = false;
                        }
                    }
                    
                }
            }


     
        if (!result)
        {
            MessageController.Show("This student already "+rdlList.SelectedItem.Text+" into this class!", MessageType.Warning, Page);
        }

        return result;
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
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
        }
        catch (Exception ex)
        {
            MessageController.Show("Value Must be Neumeric!!!", MessageType.Warning, Page);
        }
        return true;
    }


    protected void btnNotPromoted_Click(object sender, EventArgs e)
    {
        string criteria = GetCriteria();
        DataTable dt = objStudent.GetStudentForPromotion(criteria);
        if (dt.Rows.Count > 0)
        {
            MessageController.Clear(this);
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=notPromotedlist");
        }
    }
}
