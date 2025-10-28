using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared.Json;

public partial class Pages_Admission_DataEntry : System.Web.UI.Page
{
    public static int PersonId;
    public static int StudentId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    private void DataBind()
    {
        DataTable dt = new dalDataEntry().GetStudentInfo();
        if (dt.Rows.Count > 0)
        {
            rptStudent.DataSource = dt;
            rptStudent.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int count = 0;
        DateTime DOB;
        DateTime admissionDate;
        int genderId = 0;
        string regNo = "";
        foreach (RepeaterItem item in rptStudent.Items)
        {
            CheckBox chkrow = (CheckBox)item.FindControl("chkrow");
            if (chkrow.Checked)
            {
                count ++;
                HiddenField hdnId = (HiddenField)item.FindControl("hdnId");
                Label lblName = (Label) item.FindControl("lblStuName");
                Label lblFatherName = (Label)item.FindControl("lblFather");
                Label lblMotherName = (Label)item.FindControl("lblMother");
                Label lblDOB = (Label)item.FindControl("lblBirthDate");
                Label lblID = (Label) item.FindControl("lblID");
                Label lblGender = (Label) item.FindControl("lblGender");

                Label lblAdmission = (Label)item.FindControl("lblAdmissionDate");
                Label lblAddress = (Label) item.FindControl("lblAddress");

                string name = lblName.Text;
                string FName = lblFatherName.Text;
                string MName = lblMotherName.Text;
                int religionId = 1;
                string bloodGroup = "11";
                bool freedomFighter = false;
                bool tribal = false;
                bool pDefect = false;
                int FqId = 1;
                int FproId = 1;
                int MqId = 1;
                int MproId = 1;

                if (lblDOB.Text == "") DOB = Convert.ToDateTime("01/01/1900");
                else DOB = Convert.ToDateTime(lblAdmission.Text);

                if (lblGender.Text == "Boy") genderId = 1;
                else if (lblGender.Text == "Girl") genderId = 2;


                if (lblID.Text == "") regNo = "";
                else regNo = lblID.Text;

                if (lblAdmission.Text == "") admissionDate = Convert.ToDateTime("01/01/1900");
                else admissionDate = Convert.ToDateTime(lblAdmission.Text);
                string address = lblAddress.Text;

                string password = "123";

                if (!string.IsNullOrEmpty(name))
                {
                    if (count > 0)
                    {
                        PersonId = new dalDataEntry().InsertStudent(name, FName, MName, regNo, DOB, genderId, religionId, bloodGroup, freedomFighter, tribal, pDefect,FqId, FproId, MqId, MproId);
                    }
                }
                if (PersonId != -1)
                {
                    StudentId = new dalDataEntry().InsertStudent1(PersonId, regNo, admissionDate, Page.User.Identity.Name, DateTime.Now);
                    new dalDataEntry().InsertAddress(PersonId, address);
                    if (StudentId != -1)
                    {
                        Membership.CreateUser(regNo, password);
                        Roles.AddUserToRole(regNo, Controller.RoleNameById(13));
                    }
                }
            }
        }
        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Confirmation, Page);
    }
}