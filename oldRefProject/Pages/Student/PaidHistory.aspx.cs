using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_PaidHistory : BasePage
{
    dalPayment objPayment = new dalPayment();
    dalStudent objStudent = new dalStudent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Common.SessionInfo != null)
        {
            StudentId = Common.SessionInfo.StudentId;
            divSearch.Visible = (StudentId > 0) ? false : true;
            LoadPaymentHistory();
        }
    }
    int StudentId
    {
        set { ViewState["StudentId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["StudentId"]);
            }
            catch
            {
                return 0;
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
    protected void LoadPaymentHistory()
    {
        DataTable dt = objPayment.GetPaymentHistoryByStudentId(StudentId);
        rptHistory.DataSource = dt;
        rptHistory.DataBind();
    }
    public bool ValidationCheck()
    {
        //if (tbxRoll.Text == "")
        //{
        //    MessageController.Show("Roll No Must be Needed!!!", MessageType.Warning, Page);
        //    return true;
        //}
        if (tbxReg.Text == "")
        {
            MessageController.Show("Reg No Must be Needed!!!", MessageType.Warning, Page);
            return true;
        }
        return false;
    }

    protected string GetCriteria()
    {
        string criteria = "";
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo = '" + tbxReg.Text + "'";
            else
                criteria += "ss_Student.RegNo = '" + tbxReg.Text + "'";
        }

        return criteria;
    }

    protected void btnHistory_Click(object sender, EventArgs e)
    {
        if (ValidationCheck())
        {
            return;
        }
            //int studentRoll = (Convert.ToInt32(string.IsNullOrEmpty(tbxRoll.Text) ? "0" : tbxRoll.Text));
            //DataTable dtStudent = new dalPerson().GetStudentByRoll(studentRoll);
        //string criteria = "";
        //if (tbxReg.Text != "")
        //{
        //    if (criteria == "")
        //        criteria = "ss_Student.RegNo = '" + tbxReg.Text + "'";
        //    else
        //        criteria += " and ss_Student.RegNo = '" + tbxReg.Text + "'";
        //}
        DataTable dtStudent = objStudent.GetStudentInformation(GetCriteria());
            if (dtStudent.Rows.Count > 0)
            {
                int studentID = Convert.ToInt32(dtStudent.Rows[0]["StudentId"]);
                DataTable dt = objPayment.GetPaymentHistoryByStudentId(studentID);
                rptHistory.DataSource = dt;
                rptHistory.DataBind();
            }
    }
}