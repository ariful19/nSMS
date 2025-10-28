using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Fees_PaymentReceivedBafa : System.Web.UI.Page
{
    dalPaymentType objpaymentType = new dalPaymentType();
    dalStudent objStudent = new dalStudent();
    dalPayment objPayment = new dalPayment();
    public static List<Tuple<string, string>> calculatedMonth;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
  Common.CanAccess(this);
            dtPayment = Payment();
            dtTotalPayment = TotalPayment();
            LoadData();
            imgPerson.ImageUrl = "~/Images/Common/student.png";
            SelectCurrentYearMonth();
            //CheckRollNoUse();
            divStudentPayment.Visible = false;
            divPaymentHistory.Visible = false;
            //btnSearch.Visible = false;
        }
    }

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
      
        if (tbxReg.Text != "")
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + tbxReg.Text;
            else
                criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
        }
        if (classId!=0)
        {
            if (criteria == "")
                criteria = "er_StudentToClass.ClassId=" + classId;
            else
                criteria += " and er_StudentToClass.ClassId=" + classId;
        }           
      criteria += " and ss_Student.StatusId= 1";

        return criteria;
    }

    #endregion

    #region variable
    int StudentToClassId
    {
        set { ViewState["StudentToClassId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["StudentToClassId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int CampusId
    {
        set { ViewState["CampusId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["CampusId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int mediumId
    {
        set { ViewState["MediumId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["MediumId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int campusId
    {
        set { ViewState["CampusId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["CampusId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int classId
    {
        set { ViewState["ClassId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["ClassId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int groupId
    {
        set { ViewState["GroupId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["GroupId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int shiftId
    {
        set { ViewState["ShiftId"] = value; }
        get 
        {
            try
            {
                return Convert.ToInt32(ViewState["ShiftId"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    int sectionId
    {
        set { ViewState["SectionId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["SectionId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int StartMonth
    {
        set { ViewState["StartMonth"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["StartMonth"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int EndMonth
    {
        set { ViewState["EndMonth"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["EndMonth"]);
            }
            catch
            {
                return 0;
            }
        }
    }
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
    string BillNo
    {
        set { ViewState["BillNo"] = value; }
        get
        {
            try
            {
                return ViewState["BillNo"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    double Total
    {
        set { ViewState["Total"] = value; }
        get
        {
            try
            {
                return Convert.ToDouble(ViewState["Total"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    DataTable dtPayment
    {
        set { ViewState["dtPayment"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtPayment"];
            }
            catch
            {
                return null;
            }
        }
    }
    DataTable dtTotalPayment
    {
        set { ViewState["dtTotalPayment"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtTotalPayment"];
            }
            catch
            {
                return null;
            }
        }
    }
    DataTable dtPaymentReceive
    {
        set { ViewState["dtPaymentReceive"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtPaymentReceive"];
            }
            catch
            {
                return null;
            }
        }
    }
    DataTable dtMonthlyPayment
    {
        set { ViewState["dtMonthlyPayment"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtMonthlyPayment"];
            }
            catch
            {
                return null;
            }
        }
    }

    public DataTable Payment()
    {
        DataTable dt = new DataTable("PaymentForMonth");
        dt.Columns.Add("Year", typeof(int));
        dt.Columns.Add("Month", typeof(int));
        dt.Columns.Add("PaymentTypeId", typeof(int));
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("Amount", typeof(double));
        dt.Columns.Add("Total", typeof(double));
        return dt;
    }
    public DataTable PaymentReceive()
    {
        DataTable dt = new DataTable("PaymentReceive");
        dt.Columns.Add("MonthYear", typeof(string));
        dt.Columns.Add("Month", typeof(int));
        dt.Columns.Add("TotalDue", typeof(double));
        dt.Columns.Add("ClassId", typeof(int));
        dt.Columns.Add("PaymentTypeId", typeof(int));
        return dt;
    }
    public DataTable TotalPayment()
    {
        DataTable dt = new DataTable("TotalPayment");
        dt.Columns.Add("StudentId", typeof(int));
        dt.Columns.Add("Year", typeof(int));
        dt.Columns.Add("Month", typeof(int));
        dt.Columns.Add("Total", typeof(double));
        dt.Columns.Add("Paid", typeof(double));
        dt.Columns.Add("Due", typeof(double));
        return dt;
    }
    public DataTable DuePayment()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("ClassId", typeof(int));
        dt.Columns.Add("DueAmount", typeof(string));
        
        return dt;
    }
    public DataTable MonthlyPayment()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("MonthYear", typeof(string));
        dt.Columns.Add("Year", typeof(int));
        dt.Columns.Add("MonthId", typeof(int));        
        dt.Columns.Add("PaymentTypeId", typeof(int));
        dt.Columns.Add("Total", typeof(decimal));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("Status", typeof(string));
        return dt;
    }

    #endregion

    #region Function
    protected void ClearPopup()
    {
        tbxAmount.Text = "";
        tbxBillNo.Text = "";
    }
    private bool CheckValidation()
    {
        bool result = true;
        pnlStudentInfo.Visible = false;
        divStudentPayment.Visible = false;
        divPaymentHistory.Visible = false;
        rptHistory.DataSource = null;
        rptHistory.DataBind();
        rptPaymentOthers.DataSource = null;
        rptPaymentOthers.DataBind();
        rptPaymentMonth.DataSource = null;
        rptPaymentMonth.DataBind();
        if (tbxReg.Text == "")
        {
            tbxReg.BorderColor = System.Drawing.Color.Red;
            MessageController.Show(" Enter Reg. No First....", MessageType.Warning, Page);
           result = false;
        }
        return result;
    }
    //protected void CheckRollNoUse()
    //{
    //    DataTable dtRoll = new Common().GetAll("bs_UseRoll");
    //    if (dtRoll.Rows.Count > 0)
    //    {
    //        IsRollNoUse = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
    //    }
    //    if (IsRollNoUse == "False")
    //    {
    //        LabelRoll.Visible = false;
    //        tbxRoll.Visible = false;
    //        lblRoll.Visible = false;
    //        LabelRollNo.Visible = false;
    //    }
    //    else
    //    {
    //        LabelReg.Visible = false;
    //        tbxReg.Visible = false;
    //    }
    //}
    private void SelectCurrentYearMonth()
    {
        ddlMonth.SelectedIndex = DateTime.Now.Month - 1;
    }

    #endregion

    #region LoadData
    protected void LoadData()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
        ddlYear.DataBind();       
        LoadSessionInfo();
    }
    private void LoadSessionInfo()
    {
        try
        {
            if (Common.SessionInfo.TeacherId > 0)
            {
                //ddlCampus.SelectedValue = Common.SessionInfo.CampusId.ToString();
                //ddlCourse.SelectedValue = Common.SessionInfo.CourseId.ToString();
                //LoadSubjectTerm(Convert.ToInt32(ddlCourse.SelectedValue));
                //ddlSubject.SelectedValue = Common.SessionInfo.SubjectId.ToString();

                //ddlCampus.Enabled = false;
                //ddlCourse.Enabled = false;
                //ddlSubject.Enabled = false;
            }
        }
        catch
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    private void LoadSubject()
    {
        string criteria = GetCriteria();
        string dueStatus = "";
        DataTable dtDue = DuePayment();
        DataTable dt = objStudent.GetStudentInformation(criteria);
        if (dt.Rows.Count > 0)
        {    
            foreach (DataRow row in dt.Rows)
            {
                CampusId = Convert.ToInt32(row["CampusId"]);
                mediumId = Convert.ToInt32(row["MediumId"]);
                classId = Convert.ToInt32(row["ClassId"]);
                groupId = Convert.ToInt32(row["GroupId"]);
                shiftId = Convert.ToInt32(row["ShiftId"]);
                sectionId = Convert.ToInt32(row["SectionId"]);
                StudentToClassId = Convert.ToInt32(row["StudentToClassId"]);
                decimal due = LoadOtherPayment();
                if (due > -1)
                    dueStatus = due.ToString();
                else
                    dueStatus = "Not Set";

                dtDue.Rows.Add(row["ClassName"].ToString(), classId, dueStatus);
            }
            gvSubject.DataSource = dtDue;
            gvSubject.DataBind();
        }
        else
        {
            classId = 0;
            MessageController.Show("No class found..", MessageType.Error, Page);
        }
    }
    protected void LoadStudent()
    {
        string criteria = GetCriteria();
        DataTable dtStudent = objStudent.GetStudentInformation(criteria);

        if (dtStudent.Rows.Count > 0)
        {
            lblMessage.Text = "";
            divStudentPayment.Visible = true;
            pnlStudentInfo.Visible = true;
            btnSubmit.Visible = true;
            StudentToClassId = Convert.ToInt32(dtStudent.Rows[0]["StudentToClassId"].ToString());
            Session["StudentToClassId"] = StudentToClassId;

            lblNameEng.Text = dtStudent.Rows[0]["NameEng"].ToString();
            lblNameBan.Text = dtStudent.Rows[0]["NameBan"].ToString();
            lblCampusName.Text = dtStudent.Rows[0]["CampusName"].ToString();
            lblClass.Text = dtStudent.Rows[0]["ClassName"].ToString();
            lblGroup.Text = dtStudent.Rows[0]["GroupName"].ToString();
            lblYear.Text = dtStudent.Rows[0]["Year"].ToString();
            lblShift.Text = dtStudent.Rows[0]["Shift"].ToString();
            lblSection.Text = dtStudent.Rows[0]["Section"].ToString();
            lblFName.Text = dtStudent.Rows[0]["FatherNameEng"].ToString();
            lblMName.Text = dtStudent.Rows[0]["MotherNameEng"].ToString();
            lblMobile.Text = dtStudent.Rows[0]["Mobile"].ToString();
            lblAddress.Text = dtStudent.Rows[0]["Address"].ToString();
            lblRegNo.Text = dtStudent.Rows[0]["RegistrationNo"].ToString();
            if (dtStudent.Rows[0]["PersonImage"].ToString() != "")
                imgPerson.ImageUrl = "~/Images/Student/" + dtStudent.Rows[0]["PersonImage"].ToString();
            else
                imgPerson.ImageUrl = "~/Images/Common/student.png";

            //Bind for Print
            printName.Text = dtStudent.Rows[0]["NameEng"].ToString();
            PrintClass.Text = dtStudent.Rows[0]["CampusName"].ToString();
            printGroup.Text = dtStudent.Rows[0]["GroupName"].ToString();
            printSection.Text = dtStudent.Rows[0]["Section"].ToString();

            //printReg.Text = tbxRoll.Text;
        }
        else
        {
            btnSubmit.Visible = false;
            pnlStudentInfo.Visible = false;
            tbxReg.Text = "";
            //tbxRoll.Text = "";
            MessageController.Show("No Student found. Please try another.", MessageType.Warning, Page);
            //lblMessage.Text = "No Student found. Please try another.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    private decimal LoadOtherPayment()
    {
        decimal total = 0;
        decimal amount = 0;
        decimal due = 0;
        decimal totalRecive = 0;
        int paymentTypeId = 0;
        dtPaymentReceive = PaymentReceive();
        DataTable dt = objpaymentType.GetPaymentWithoutDefault(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(mediumId), Convert.ToInt32(campusId), Convert.ToInt32(classId), Convert.ToInt32(groupId), Convert.ToInt32(shiftId), Convert.ToInt32(sectionId), false,false);
        dt.Columns.Add("Status");
        dt.Columns.Add("Dues");

        if (dt.Rows.Count > 0)
        {
            StartMonth = Convert.ToInt32(dt.Rows[0]["StartMonth"]);
            EndMonth = Convert.ToInt32(dt.Rows[0]["EndMonth"]);
            foreach (DataRow row in dt.Rows)
            {
                string status = "Not Paid";
                decimal dueAmount = 0;
                amount = Convert.ToDecimal(row["Amount"]);
                total += amount;
                paymentTypeId = Convert.ToInt32(row["PaymentTypeId"]);
                DataTable dtPaid = objPayment.GetOthersPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(classId), paymentTypeId);

                if (dtPaid.Rows.Count > 0)
                {

                    foreach (DataRow rows in dtPaid.Rows)
                    {
                        dueAmount = Convert.ToDecimal(rows["DueAmount"]);
                        totalRecive += Convert.ToDecimal(rows["TotalGiven"]);
                    }
                    if (dueAmount == 0)
                    {
                        status = "Paid";
                    }
                    else if (dueAmount == Convert.ToDecimal(row["Amount"].ToString()))
                    {
                        status = "Not Paid";
                    }
                    else
                    {
                        status = "Partially Paid";
                    }
                }
                else
                {

                    if (amount == 0)
                    {
                        status = "N/A";
                    }
                    else
                    {
                        dueAmount = amount;
                    }
                }
                row["Status"] = status;
                row["Dues"] = dueAmount.ToString();
            }

            rptPaymentOthers.DataSource = dt;
            rptPaymentOthers.DataBind();
            Label lblto = (Label)rptPaymentOthers.Controls[rptPaymentOthers.Controls.Count - 1].FindControl("lblTotal");
            Label lblDue = (Label)rptPaymentOthers.Controls[rptPaymentOthers.Controls.Count - 1].FindControl("lblPreviousDue");
            
            due = total - totalRecive;
            lblto.Text = total.ToString();
            lblDue.Text = due.ToString();
            LoadMonthlyPayment();             
        }
        else
        {
            rptPaymentOthers.DataSource = null;
            rptPaymentOthers.DataBind();
            rptPaymentMonth.DataSource = null;
            rptPaymentMonth.DataBind();
            lblTotal.Text = "";
            divStudentPayment.Visible = false;
            due = -2;
            ///MessageController.Show("No Payment setup yet...", MessageType.Warning, Page);
        }        
        lblTotal.Text = total.ToString();       
        lblGrandTotal.Text = due.ToString();
        lblPreviousDue.Text = due.ToString();
        lblTotal.Text = total.ToString();
        popPreviousDue.Text = lblPreviousDue.Text;
        popLateFee.Text = lblFine.Text;
        popScholar.Text = lblScholar.Text;
        popTotal.Text = lblTotal.Text;
        popGrandTotal.Text = lblGrandTotal.Text;
        popPaying.Text = totalRecive.ToString();
        due += LoadMonthlyDue();
        return due;
    }
    private void LoadMonthlyPayment()
    {

        int roll = 0;
        int year = DateTime.Now.Year;
        List<int> lstmonthId = new List<int>();
        dtMonthlyPayment = MonthlyPayment();

        if (StartMonth < EndMonth)
        {
            for (int i = StartMonth; EndMonth + 1 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthlyPayment);
            }
        }
        else
        {
            for (int i = StartMonth; 13 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthlyPayment);
            }
            for (int i = 1; EndMonth + 1 > i; i++)
            {
                roll = i;
                year = DateTime.Now.Year + 1;
                LoadMonthRpt(roll, year, dtMonthlyPayment);
            }
        }

        if (dtMonthlyPayment.Rows.Count > 0)
        {
            rptPaymentMonth.DataSource = dtMonthlyPayment;
            rptPaymentMonth.DataBind();
        }
        else
        {
            rptPaymentMonth.DataSource = null;
            rptPaymentMonth.DataBind();
        }
    }
    private void LoadMonthRpt(int monthId, int year, DataTable dt)
    {
        string month = "";
        string status = "Applicable";
        int paymentTypeId = 0;
        decimal amount = 0;
         DataTable dtMonthly = objpaymentType.GetPaymentTypeByIsDefault(true, true);
         foreach (DataRow row in dtMonthly.Rows)
         {
             paymentTypeId = Convert.ToInt32(row["Id"]);
             DataTable dtMonth = objPayment.GetMonthById(monthId);
             DataTable dtMonthlyFee = objPayment.GetMonthlyFees(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(mediumId), Convert.ToInt32(campusId), Convert.ToInt32(classId), Convert.ToInt32(groupId), Convert.ToInt32(shiftId), Convert.ToInt32(sectionId), monthId, paymentTypeId);
             if (dtMonthlyFee.Rows.Count > 0)
             {
                 month = dtMonth.Rows[0]["Month"].ToString();
                 amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
                 paymentTypeId = Convert.ToInt32(dtMonthlyFee.Rows[0]["PaymentTypeId"]);
                 if (amount == 0)
                 {
                     status = "N/A";
                 }
                 dt.Rows.Add(month + "-" + year, year, monthId, paymentTypeId, 0, amount, status);
             }
         }
    }
    private decimal LoadMonthlyDue()
    {
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        decimal due = 0;
        foreach (RepeaterItem item in rptPaymentMonth.Items)
        {
            CheckBox chkMonthlyPayment = (CheckBox)item.FindControl("chkMonthlyPayment");
            HiddenField hdnMonthId = (HiddenField)item.FindControl("hdnMonthId");
            Label hdnTypeId = (Label)item.FindControl("hdnPaymentTypeId");
            Label lblMonthYear = (Label)item.FindControl("lblMonthYear");
            Label lblYear = (Label)item.FindControl("lblYear");
            Label lblStatus = (Label)item.FindControl("lblStatus");
            Label lblAmount = (Label)item.FindControl("lblAmount");
           
            int monthId = Convert.ToInt32(hdnMonthId.Value);
            int year = Convert.ToInt32(lblYear.Text);
            if (lblStatus.Text == "N/A")
            {
                lblAmount.Visible = false;
                lblStatus.Visible = true;
                chkMonthlyPayment.Enabled = false;
            }

            DataTable dtPaid = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(classId), Convert.ToInt32(hdnTypeId.Text), monthId);
            if (dtPaid.Rows.Count > 0)
            {
                decimal dueAmount = 0;
                decimal totalReceive = 0;
                foreach (DataRow rows in dtPaid.Rows)
                {
                    dueAmount = Convert.ToDecimal(rows["DueAmount"]);
                    totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                }
                if (dueAmount == 0)
                {
                    chkMonthlyPayment.Checked = true;
                    chkMonthlyPayment.Enabled = false;
                }
                else
                {

                    chkMonthlyPayment.Checked = false;
                    chkMonthlyPayment.Enabled = true;
                    if (currentMonth >= Convert.ToInt32(hdnMonthId.Value))
                        due += dueAmount;
                    lblAmount.Attributes.Add("style", "background-color: #e3f28e;");
                    lblMonthYear.Attributes.Add("style", "background-color: #e3f28e;");
                }
            }
            else
            {
                if (chkMonthlyPayment.Enabled && !chkMonthlyPayment.Checked)
                    if (currentMonth >= monthId && year == currentYear)
                    {
                        due += Convert.ToDecimal(lblAmount.Text);
                        lblAmount.Attributes.Add("style", "background-color: #f26d6d;");
                        lblMonthYear.Attributes.Add("style", "background-color: #f26d6d;");
                    }
            }
            Label lblDue = (Label)rptPaymentMonth.Controls[rptPaymentMonth.Controls.Count - 1].FindControl("lblMonthlyDue");
            lblDue.Text = due.ToString();
        }
        
        return due;
    }
    protected void LoadPaymentHistory()
    {
        DataTable dt = objPayment.GetPaymentHistoryByStudentSubjectId(StudentToClassId, Convert.ToInt32(classId));
            if (dt.Rows.Count > 0)
            {
                rptHistory.DataSource = dt;
                rptHistory.DataBind();
                divPaymentHistory.Visible = true;
            }
            else
            {
                divPaymentHistory.Visible = false;
            }      
   
    }

    #endregion

    #region Button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        classId = 0;
        BillNo = "";
        divPaymentHistory.Visible = false;
        divStudentPayment.Visible = false;
        //divClassInfo.Visible = false;
        //ddlSubject.Enabled = false;
        if (CheckValidation())
        {
            ClearPopup();
            LoadSubject();            
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        BillNo = "";
        if (tbxAmount.Text == "")
        {
            MessageController.Show("  Insert Amount First....", MessageType.Warning, Page);
            return;
        }
        if (tbxBillNo.Text == "")
        {
            MessageController.Show(" Please Insert BillNo....", MessageType.Warning, Page);
            return;
        }

        int billNoExist = objPayment.GetBillNoExist(tbxBillNo.Text);
        if (billNoExist != 1)
        {
            MessageController.Show("Bill No already Exists. Please Select new Bill No....", MessageType.Error, Page);
            return;
        }
        decimal paid = decimal.Parse(tbxAmount.Text);
        decimal due = 0;
        List<StudentPayment> lstStudentPayment = new List<StudentPayment>();



        if (popPreviousDue.Text != "")
        {
            due = Convert.ToDecimal(lblPreviousDue.Text);

        }
        else
        {
            MessageController.Show("  No Dues for " + ddlMonth.SelectedItem.Text + "....", MessageType.Warning, Page);
            return;
        }
        if (tbxReg.Text != "")
        {
            foreach (DataRow row in dtPaymentReceive.Rows)
            {
                StudentPayment studentPayment = new StudentPayment();
                studentPayment.Amount = decimal.Parse(row["TotalDue"].ToString());
                studentPayment.TotalGiven = 0;
                if (paid != 0)
                    if (paid > studentPayment.Amount)
                    {
                        paid -= studentPayment.Amount;
                        studentPayment.TotalGiven = studentPayment.Amount;
                    }
                    else
                    {
                        studentPayment.TotalGiven = paid;
                        paid -= paid;
                    }
                if (studentPayment.TotalGiven != 0)
                {
                    studentPayment.MonthYear = row["MonthYear"].ToString();
                    studentPayment.Month = Convert.ToInt32(row["Month"]);
                    studentPayment.Date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
                    studentPayment.DueAmount = studentPayment.Amount - studentPayment.TotalGiven;
                    studentPayment.ClassId = Convert.ToInt32(row["ClassId"]);
                    studentPayment.PaymentTypeId = Convert.ToInt32(row["PaymentTypeId"]);
                    studentPayment.StudentToClassId = StudentToClassId;
                    studentPayment.Year = int.Parse(ddlYear.Text);
                    studentPayment.BillNo = tbxBillNo.Text;
                    studentPayment.CreatedBy = Page.User.Identity.Name;
                    studentPayment.CreatedDate = DateTime.Now;
                    BillNo = tbxBillNo.Text;
                    lstStudentPayment.Add(studentPayment);
                }

            }

        }

        try
        {
            int result = objPayment.InsertStudentPayment(lstStudentPayment);
            if (result != -1)
            {
                ClearPopup();
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            }
            else
            {
                ClearPopup();
                MessageController.Show("Bill No already exists...", MessageType.Error, Page);
            }

        }
        catch (Exception)
        {
            MessageController.Show(MessageCode.SaveFailed, MessageType.Warning, Page);
        }
        LoadStudent();
        LoadOtherPayment();
        LoadPaymentHistory();
        LoadSubject();
    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        #region PDF
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");

        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //pnlPdf.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();
        #endregion
        if (BillNo != "")
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?report=StudentPayment&type=" + BillNo);
        }
        else
        {
            MessageController.Show(" Please Receive Payment 1st...", MessageType.Warning, Page);
        }
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        BillNo = "";
        Response.Redirect("~/Pages/Fees/PaymentReceivedBafa.aspx");
    }

    #endregion

    #region DDL
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //MessageController.Clear(this);
        //btnSearch.Visible = false;
        //this.ddlSubject.Enabled = true;
        //divStudentPayment.Visible = false;

        //if (ddlCourse.SelectedIndex != 0)
        //    LoadSubjectTerm(Convert.ToInt32(ddlCourse.SelectedValue));
        //else
        //    this.ddlSubject.Enabled = false;
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);       
        divStudentPayment.Visible = false;
        if (CheckValidation())
        {
            LoadStudent();
            LoadPaymentHistory();
            LoadOtherPayment();
            if (lblTotal.Text == "")    
            
            MessageController.Show("No Payment setup yet...", MessageType.Warning, Page);
        }
    }
    protected void chkOtherPayment_OnCheckedChanged(object sender, EventArgs e)
    {

        decimal grantTotal = 0;
        decimal total = 0;
        dtPaymentReceive = PaymentReceive();
        MessageController.Clear(this);
        if (grantTotal == 0 && lblPreviousDue.Text != "0.00")
        {

            foreach (RepeaterItem item in rptPaymentMonth.Items)
            {
                CheckBox chkMonthlyPayment = (CheckBox)item.FindControl("chkMonthlyPayment");
                if (chkMonthlyPayment.Enabled)
                    if (chkMonthlyPayment.Checked)
                    {
                        MessageController.Show("  Please pay other amounts first.....", MessageType.Warning, Page);
                        chkMonthlyPayment.Checked = false;
                        return;
                    }
            }
        }
        foreach (RepeaterItem item in rptPaymentOthers.Items)
        {
            Label lblAmount = (Label)item.FindControl("lblAmount");
            CheckBox chkOtherPayment = (CheckBox)item.FindControl("chkOtherPayment");
            Label hdnTypeId = (Label)item.FindControl("hdnPaymentTypeId");
            total = Convert.ToDecimal(lblAmount.Text);
            if (chkOtherPayment.Enabled)
                if (chkOtherPayment.Checked)
                {
                    grantTotal += total;
                    dtPaymentReceive.Rows.Add(Convert.ToInt32(ddlMonth.SelectedValue), total, Convert.ToInt32(classId), Convert.ToInt32(hdnTypeId.Text));
                }

        }
        foreach (RepeaterItem item in rptPaymentMonth.Items)
        {
            Label lblAmount = (Label)item.FindControl("lblAmount");
            CheckBox chkMonthlyPayment = (CheckBox)item.FindControl("chkMonthlyPayment");
            HiddenField hdnMonthId = (HiddenField)item.FindControl("hdnMonthId");
            Label hdnTypeId = (Label)item.FindControl("hdnPaymentTypeId");

            total = Convert.ToDecimal(lblAmount.Text);
            if (chkMonthlyPayment.Enabled)
                if (chkMonthlyPayment.Checked)
                {
                    grantTotal += total;
                    dtPaymentReceive.Rows.Add(Convert.ToInt32(hdnMonthId.Value), total, Convert.ToInt32(classId), Convert.ToInt32(hdnTypeId.Text));
                }


        }
        lblGrandTotal.Text = grantTotal.ToString();
        popPreviousDue.Text = lblPreviousDue.Text;
        popLateFee.Text = lblFine.Text;
        popScholar.Text = lblScholar.Text;
        popTotal.Text = lblTotal.Text;
        popGrandTotal.Text = lblGrandTotal.Text;

    }
    protected void chkMonthlyPayment_OnCheckedChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        decimal grantTotal = 0;
        decimal total = 0;
        int countChecked = 0;
        int countUnchecked = 0;
        // get Footer tamplate item control....
        Label lblPreviousDue = (Label)rptPaymentOthers.Controls[rptPaymentOthers.Controls.Count - 1].Controls[0].FindControl("lblPreviousDue");

        grantTotal = Convert.ToDecimal(lblPreviousDue.Text);

        for (int i = 0; rptPaymentMonth.Items.Count > i; i++)
        {
            Label lblAmount = (Label)rptPaymentMonth.Items[i].FindControl("lblAmount");
            CheckBox chkMonthlyPayment = (CheckBox)rptPaymentMonth.Items[i].FindControl("chkMonthlyPayment");
            HiddenField hdnMonthId = (HiddenField)rptPaymentMonth.Items[i].FindControl("hdnMonthId");
            Label hdnTypeId = (Label)rptPaymentMonth.Items[i].FindControl("hdnPaymentTypeId");
            Label lblMonthYear = (Label)rptPaymentMonth.Items[i].FindControl("lblMonthYear");
            if (chkMonthlyPayment.Checked && chkMonthlyPayment.Enabled)
            {
                for (int j = i - 1; rptPaymentMonth.Items.Count > j; j--)
                {
                    if (j > -1)
                    {
                        CheckBox chk1 = (CheckBox)rptPaymentMonth.Items[j].FindControl("chkMonthlyPayment");
                        Label lblMonthYear1 = (Label)rptPaymentMonth.Items[j].FindControl("lblMonthYear");

                        if (chk1.Enabled && !chk1.Checked)
                        {
                            chkMonthlyPayment.Checked = false;
                            MessageController.Show("Please pay " + lblMonthYear1.Text + " dues 1st. Then select " + lblMonthYear.Text, MessageType.Error, Page);
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                DataTable dtPaid = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(classId), Convert.ToInt32(hdnTypeId.Text), Convert.ToInt32(hdnMonthId.Value));
                if (dtPaid.Rows.Count > 0)
                {
                    if (dtPaid.Rows[0]["DueAmount"].ToString() != "0.00")
                    {
                        total = Convert.ToDecimal(dtPaid.Rows[0]["DueAmount"]);
                    }
                }
                else
                    total = Convert.ToDecimal(lblAmount.Text);

                grantTotal += total;
                dtPaymentReceive.Rows.Add(lblMonthYear.Text, Convert.ToInt32(hdnMonthId.Value), total, Convert.ToInt32(classId), Convert.ToInt32(hdnTypeId.Text));
            }
        }
       
        DataTable dtDistinct = dtPaymentReceive.DefaultView.ToTable(true, new string[] { "MonthYear", "Month", "TotalDue", "ClassId", "PaymentTypeId" });
        dtPaymentReceive = dtDistinct;

        lblGrandTotal.Text = grantTotal.ToString();
        popPreviousDue.Text = lblPreviousDue.Text;
        popLateFee.Text = lblFine.Text;
        popScholar.Text = lblScholar.Text;
        popTotal.Text = lblTotal.Text;
        popGrandTotal.Text = lblGrandTotal.Text;

    }
    protected void rptPaymentOthers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        decimal grantTotal = 0;
        decimal total = 0;
        if (e.Item.ItemType == ListItemType.Header)
        {

        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            Label lblPaymentId = (Label)e.Item.FindControl("lblPaymentId");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            Label lblDues = (Label)e.Item.FindControl("lblDues");
            total = Convert.ToDecimal(lblDues.Text);
            if (lblStatus.Text != "Paid" && lblStatus.Text != "N/A")
            {
                grantTotal += total;
                dtPaymentReceive.Rows.Add("Academic", 0, total, Convert.ToInt32(classId), Convert.ToInt32(lblPaymentId.Text));
            }
        }
    }
    protected void rptPaymentMonthly_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {

        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chkMonthlyPayment = (CheckBox)e.Item.FindControl("chkMonthlyPayment");
            HiddenField hdnMonthId = (HiddenField)e.Item.FindControl("hdnMonthId");
            Label hdnTypeId = (Label)e.Item.FindControl("hdnPaymentTypeId");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");

            if (lblStatus.Text == "N/A")
            {
                lblAmount.Visible = false;
                lblStatus.Visible = true;
                chkMonthlyPayment.Enabled = false;
            }

            DataTable dtPaid = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(classId), Convert.ToInt32(hdnTypeId.Text), Convert.ToInt32(hdnMonthId.Value));
            if (dtPaid.Rows.Count > 0)
            {
                if (dtPaid.Rows[0]["DueAmount"].ToString() == "0.00")
                {
                    chkMonthlyPayment.Checked = true;
                    chkMonthlyPayment.Enabled = false;
                }
                else
                {
                    chkMonthlyPayment.Checked = false;
                    chkMonthlyPayment.Enabled = true;
                }
            }

        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

        }

    }
    protected void gvSubject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Find")
        {
             try{
                 MessageController.Clear(this);
                 BillNo = "";
                 divStudentPayment.Visible = false;
                 classId = 0;
                 if (CheckValidation())
                 {
                     classId = Convert.ToInt32(e.CommandArgument);
                     LoadStudent();
                     LoadPaymentHistory();
                     LoadOtherPayment();
                     if (lblTotal.Text == "0")
                     MessageController.Show("No Payment setup yet...", MessageType.Warning, Page);
                 }
                }
             catch (Exception ex)
                {
                    MessageController.Show("Some problem occures...", MessageType.Error, Page);
                }
        }
    }

    #endregion
    protected void imageButtonDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MessageController.Clear(this);
            divStudentPayment.Visible = false;
            classId = 0;
            if (CheckValidation())
            {
                classId = Convert.ToInt32(e.ToString());
                LoadStudent();
                LoadPaymentHistory();
                LoadOtherPayment();
                if (lblTotal.Text == "")
                    MessageController.Show("No Payment setup yet...", MessageType.Warning, Page);
            }
        }
        catch (Exception ex)
        {
        }
    }
}

