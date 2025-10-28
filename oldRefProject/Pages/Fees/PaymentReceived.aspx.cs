using System.Activities.Statements;
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
using System.Web.UI.WebControls;

public partial class Pages_Fees_PaymentReceived : System.Web.UI.Page
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
            CheckRollNoUse();
            divStudentPayment.Visible = false; 
            divPaymentHistory.Visible = false;         
        }
    }

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
    int YearId
    {
        set { ViewState["YearId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["YearId"]);
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
    decimal DueAmount
    {
        set { ViewState["DueAmount"] = value; }
        get
        {
            try
            {
                return Convert.ToDecimal(ViewState["DueAmount"]);
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

    decimal totalAmount
    {
        set { ViewState["totalAmount"] = value; }
        get
        {
            try
            {
                return Convert.ToDecimal(ViewState["totalAmount"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    decimal totalRcv
    {
        set { ViewState["totalRcv"] = value; }
        get
        {
            try
            {
                return Convert.ToDecimal(ViewState["totalRcv"]);
            }
            catch
            {
                return 0;
            }
        }
    }

    decimal totalDue
    {
        set { ViewState["totalDue"] = value; }
        get
        {
            try
            {
                return Convert.ToDecimal(ViewState["totalDue"]);
            }
            catch
            {
                return 0;
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
    protected DataTable PaymentEdit()
    {
        DataTable dt = new DataTable("dtPayment");
        dt.Columns.Add("StuSId", typeof(int));
        dt.Columns.Add("UpdatedID", typeof(int));
        dt.Columns.Add("PayID", typeof(int));
        dt.Columns.Add("MID", typeof(int));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("InsertUpdateID", typeof(int));
        dt.Columns.Add("StrMonth", typeof(string));
        dt.Columns.Add("EndMonth", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
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
        dt.Columns.Add("PaymentType", typeof(string));
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

        dt.Columns.Add("Month", typeof(string));
        dt.Columns.Add("Total", typeof(double));
        dt.Columns.Add("Paid", typeof(double));
        dt.Columns.Add("Due", typeof(double));
        dt.Columns.Add("Status", typeof(string));
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
        dt.Columns.Add("PaymentType", typeof(string));
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
        if (ddlRollNo.Visible)
        {
            if (ddlRollNo.SelectedIndex == 0)
            {
                ddlRollNo.BorderColor = System.Drawing.Color.Red;
                MessageController.Show(" Select Roll No First....", MessageType.Warning, Page);
                LoadNull();
                result = false;
            }
        }
        else
        {
            if (ddlRegNo.SelectedIndex == 0)
            {
                ddlRegNo.BorderColor = System.Drawing.Color.Red;
                MessageController.Show(" Select Reg. No First....", MessageType.Warning, Page);
                LoadNull();
                result = false;
            }
        }
        return result;
    }

    private void LoadNull()
    {
        pnlStudentInfo.Visible = false;
        divStudentPayment.Visible = false;
        divPaymentHistory.Visible = false;
        rptHistory.DataSource = null;
        rptHistory.DataBind();
        rptPaymentOthers.DataSource = null;
        rptPaymentOthers.DataBind();
        rptPaymentMonth.DataSource = null;
        rptPaymentMonth.DataBind();
        rptReceivedPayment.DataSource = null;
        rptReceivedPayment.DataBind();
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
            ddlRollNo.Visible = false;
            //lblRoll.Visible = false;
            //LabelRollNo.Visible = false;
        }
        else
        {
            LabelReg.Visible = false;
            ddlRegNo.Visible = false;
        }
    }
    private void SelectCurrentYearMonth()
    {
        ddlMonth.SelectedIndex = DateTime.Now.Month - 1;
    }

    #endregion

    #region LoadData
    protected void LoadData()
    {
        string userName = User.Identity.Name;
        string role = new Common().GetRolesByUserName(userName);

        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        if (role == "Admin")
        {
            ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
            ddlCampus.DataBind();
        }
        else
        {
            ddlCampus.DataSource = new Common().GetCampusByUserName(userName);
            ddlCampus.DataBind();
        }

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();
        var items = ddlGroup.Items.FindByText("None");
        ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(items);

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();       
        //LoadSessionInfo();
        LoadRollRegNo();
    }

    private void LoadRollRegNo()
    {
        if (ddlMedium.SelectedItem.Text == "---Select---" || ddlCampus.SelectedItem.Text == "---Select---" || ddlClass.SelectedItem.Text == "---Select---" || ddlGroup.SelectedItem.Text == "---Select---" || ddlShift.SelectedItem.Text == "---Select---" || ddlSection.SelectedItem.Text == "---Select---")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please Select data from list');", true);
        }
        else
        {
            Common.DropdownByConditionMulti(ddlRollNo, "vwStudentRoll", "CampusId", "MediumId", "ClassId", "ShiftId", "SectionId", "GroupId", "Year", "RollNo", ddlCampus.SelectedItem.Value, ddlMedium.SelectedItem.Value, ddlClass.SelectedItem.Value, ddlShift.SelectedItem.Value, ddlSection.SelectedItem.Value, ddlGroup.SelectedItem.Value, ddlYear.SelectedItem.Value, 1, 0);
            Common.DropdownByConditionMulti(ddlRegNo, "vwStudentRoll", "CampusId", "MediumId", "ClassId", "ShiftId", "SectionId", "GroupId", "Year", "RegNo", ddlCampus.SelectedItem.Value, ddlMedium.SelectedItem.Value, ddlClass.SelectedItem.Value, ddlShift.SelectedItem.Value, ddlSection.SelectedItem.Value, ddlGroup.SelectedItem.Value, ddlYear.SelectedItem.Value, 10, 0, true);
        }
    }

  

    protected void LoadStudent()
    {
        LoadNull();
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
            lblYear.Text = dtStudent.Rows[0]["YearName"].ToString();
            lblShift.Text = dtStudent.Rows[0]["Shift"].ToString();
            lblSection.Text = dtStudent.Rows[0]["Section"].ToString();
            lblStatus.Text = dtStudent.Rows[0]["Status"].ToString();
            lblFName.Text = dtStudent.Rows[0]["FatherNameEng"].ToString();
            lblMName.Text = dtStudent.Rows[0]["MotherNameEng"].ToString();
            //lblRoll.Text = dtStudent.Rows[0]["RollNo"].ToString();
            lblMobile.Text = dtStudent.Rows[0]["Mobile"].ToString();
            lblAddress.Text = dtStudent.Rows[0]["Address"].ToString();
            lblRegNo.Text = dtStudent.Rows[0]["RegistrationNo"].ToString();

            // Test
            DataTable dts = objPayment.GetRemarksByStudent(StudentToClassId);

            if (dts.Rows.Count > 0)
            {
                lblRemarks.Text = dts.Rows[0]["Remarks"].ToString();
            }
            else
                lblRemarks.Text = "";

            if (dtStudent.Rows[0]["PersonImage"].ToString() != "")
                imgPerson.ImageUrl = "~/Images/Student/" + dtStudent.Rows[0]["PersonImage"].ToString();
            else
                imgPerson.ImageUrl = "~/Images/Common/student.png";

            //Bind for Print
            //printName.Text = dtStudent.Rows[0]["NameEng"].ToString();
            //PrintClass.Text = dtStudent.Rows[0]["CampusName"].ToString();
            //printGroup.Text = dtStudent.Rows[0]["GroupName"].ToString();
            //printSection.Text = dtStudent.Rows[0]["Section"].ToString();

            //printReg.Text = ddlRegNo.Text;
        }
        else
        {
            btnSubmit.Visible = false;
            pnlStudentInfo.Visible = false;
            ddlRegNo.SelectedIndex = 0;
            ddlRollNo.SelectedIndex = 0;
            MessageController.Show("No Student found. Please try another.", MessageType.Warning, Page);
            //lblMessage.Text = "No Student found. Please try another.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    private void LoadOtherPayment()
    {
        decimal total = 0;
        decimal amount = 0;
        decimal due = 0;
        decimal totalRecive = 0;
        int paymentTypeId = 0;
        dtPaymentReceive = PaymentReceive();
        DataTable dt = objpaymentType.GetPaymentWithoutDefault(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue),Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), false,false);
        dt.Columns.Add("Status");
        dt.Columns.Add("Dues");

        if (dt.Rows.Count > 0)
        {
            divMonthlyPayment.Visible = true;
            paymentDiv.Visible = true;
            StartMonth = Convert.ToInt32(dt.Rows[0]["StartMonth"]);
            EndMonth = Convert.ToInt32(dt.Rows[0]["EndMonth"]);
            YearId = Convert.ToInt32(dt.Rows[0]["YearId"]);
            foreach (DataRow row in dt.Rows)
            {
                string status = "Not Paid";
                decimal dueAmount = 0;
                amount = Convert.ToDecimal(row["Amount"]);
                total += amount;
                paymentTypeId = Convert.ToInt32(row["PaymentTypeId"]);
                DataTable dtPaid = objPayment.GetOthersPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), paymentTypeId);

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
            lblTotal.Text = total.ToString();
            lblto.Text = total.ToString();
            lblDue.Text = due.ToString();
            lblGrandTotal.Text = "0.00";
            lblPreviousDue.Text = due.ToString();
            lblTotal.Text = total.ToString();
            LoadMonthlyPayment();
        }
        else
        {
            divMonthlyPayment.Visible = false;
            paymentDiv.Visible = false;
            divPaymentHistory.Visible = false;
            rptPaymentOthers.DataSource = null;
            rptPaymentOthers.DataBind();
            rptPaymentMonth.DataSource = null;
            rptPaymentMonth.DataBind();
            lblTotal.Text = "";
            MessageController.Show("No Payment found. Please set Payment 1st..", MessageType.Warning, Page);
        }
        popPreviousDue.Text = lblPreviousDue.Text;
        popLateFee.Text = lblFine.Text;
        popScholar.Text = lblScholar.Text;
        popTotal.Text = lblTotal.Text;
        popGrandTotal.Text = lblGrandTotal.Text;
        popPaying.Text = totalRecive.ToString();
        Random r=new Random();
        tbxBillNo.Text = "100-" + Controller.GetBillNo().ToString() + r.Next(0,99).ToString().PadLeft(2,'0');
        LoadMonthlyDue();
    }
    private void LoadMonthlyPayment()
    {
        DueAmount = 0;

        int roll = 0;
        int year = new dalYear().GetNameId(YearId);
        List<int> lstmonthId = new List<int>();
        dtMonthlyPayment = MonthlyPayment();
   var endm = EndMonth == 11 ? 12 : EndMonth;
        if (StartMonth < endm)
        {
            for (int i = StartMonth; endm + 1 > i; i++)
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
            year = year + 1;
            for (int i = 1; endm + 1 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthlyPayment);
            }
        }

        if (dtMonthlyPayment.Rows.Count > 0)
        {
            rptPaymentMonth.DataSource = dtMonthlyPayment;
            rptPaymentMonth.DataBind();
            rptPaymentMonthlyOthers.DataSource = dtMonthlyPayment;
            rptPaymentMonthlyOthers.DataBind();
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
            string paymentType = dtMonthly.Rows[0]["PaymentType"].ToString(); 
            paymentTypeId = Convert.ToInt32(row["Id"]);
            DataTable dtMonth = objPayment.GetMonthById(monthId);
            DataTable dtMonthlyFee = objPayment.GetMonthlyFees(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), monthId, paymentTypeId);
            month = dtMonth.Rows[0]["Month"].ToString();
            amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
            
            if (amount == 0)
            {
                status = "N/A";
            }
            dt.Rows.Add(month + "-" + year, year, monthId, paymentTypeId, 0, amount, status, paymentType);
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
            Label lblDueAmount = (Label)item.FindControl("lblDueAmount");
            
            //Label lblItem1 = (Label)item.FindControl("lblItem1");
            //Label lblItem2 = (Label)item.FindControl("lblItem2");
            //Label lblItem3 = (Label)item.FindControl("lblItem3");
            //Label lblItem4 = (Label)item.FindControl("lblItem4");
            //Label lblItem5 = (Label)item.FindControl("lblItem5");
            //TextBox tbxItem1 = (TextBox)item.FindControl("tbxItem1");
            //TextBox tbxItem2 = (TextBox)item.FindControl("tbxItem2");
            //TextBox tbxItem3 = (TextBox)item.FindControl("tbxItem3");
            //TextBox tbxItem4 = (TextBox)item.FindControl("tbxItem4");
            //TextBox tbxItem5 = (TextBox)item.FindControl("tbxItem5");

            int monthId = Convert.ToInt32(hdnMonthId.Value);
            int year = Convert.ToInt32(lblYear.Text);
            if (lblStatus.Text == "N/A")
            {
                //lblAmount.Visible = false;
                //lblStatus.Visible = true;
                //chkMonthlyPayment.Enabled = false;
                lblAmount.Text = "0.00";
            }
            DataTable dtPaidMonthly =new DataTable();
            DataTable dtPaidHouseRent = new DataTable();


            if (lblAmount.Text != "0.00")
                dtPaidMonthly = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(hdnTypeId.Text), monthId);

            //if (tbxItem1.Text != "0.00" && !string.IsNullOrEmpty(tbxItem1.Text))
            //    dtPaidHouseRent = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(lblItem1.Text), monthId);
         
         
            decimal dueAmount = 0;
            if (dtPaidMonthly.Rows.Count > 0 || dtPaidHouseRent.Rows.Count > 0)
            {
               
                decimal totalReceive = 0;
                foreach (DataRow rows in dtPaidMonthly.Rows)
                {
                    dueAmount = Convert.ToDecimal(rows["DueAmount"]);
                    totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                }
                foreach (DataRow rows in dtPaidHouseRent.Rows)
                {
                    dueAmount = Convert.ToDecimal(rows["DueAmount"]);
                    totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                }

                if (dueAmount == 0)
                {
                    chkMonthlyPayment.Checked = false;
                    chkMonthlyPayment.Enabled = true;
                    //tbxItem1.Enabled = false;
                    //tbxItem2.Enabled = false;
                    //tbxItem3.Enabled = false;
                    //tbxItem4.Enabled = false;
                    //tbxItem5.Enabled = false;
                }
                else
                {
                    //tbxItem1.Enabled = true;
                    //tbxItem2.Enabled = true;
                    //tbxItem3.Enabled = true;
                    //tbxItem4.Enabled = true;
                    //tbxItem5.Enabled = true;
                    chkMonthlyPayment.Checked = false;
                    chkMonthlyPayment.Enabled = true;
                    due += dueAmount;
                    if (currentMonth >= Convert.ToInt32(hdnMonthId.Value))
                        
                    lblAmount.Attributes.Add("style", "background-color: #e3f28e;");
                    lblMonthYear.Attributes.Add("style", "background-color: #e3f28e;");
                }
            }
            else
            {
                dueAmount = Convert.ToDecimal(lblAmount.Text);
                due += dueAmount;
                if (chkMonthlyPayment.Enabled && !chkMonthlyPayment.Checked)
                    if (currentMonth >= monthId && year == currentYear)
                    {

                       
                        lblAmount.Attributes.Add("style", "background-color: #f26d6d;");
                        lblMonthYear.Attributes.Add("style", "background-color: #f26d6d;");
                    }
            }

            lblDueAmount.Text = due.ToString();

            Label lblDue = (Label)rptPaymentMonth.Controls[rptPaymentMonth.Controls.Count - 1].FindControl("lblMonthlyDue");
            lblDue.Text = due.ToString();
        }

        return due;
    }
    protected void LoadPaymentHistory()
    {
           //decimal totalAmount = 0;
           //decimal totalRcv=0;
           //decimal totalDue = 0;

        DataTable dt = objPayment.GetPaymentHistoryByStudentSubjectId(StudentToClassId, Convert.ToInt32(ddlClass.SelectedValue));

        //----Start Task

     
            var fee = dt.AsEnumerable().GroupBy(r => new { Id = r["StudentToClassId"], PaymentType = r["PaymentTypeId"], MonthYear = r["MonthYear"] })
                .Select(
                p => new
                {
                    Id = p.Key.Id,
                    GivenAmount = p.Sum(o => (decimal)o["TotalGiven"]),
                    Amount = p.Max(o => o["Amount"])
                }).ToList();

            foreach (var item in fee)
            {
                totalAmount += Convert.ToDecimal(item.Amount);
                totalRcv += Convert.ToDecimal(item.GivenAmount);
                totalDue += totalAmount - totalRcv;
            }

        //---End Task

        if (dt.Rows.Count > 0)
        {
            rptHistory.DataSource = dt;
            rptHistory.DataBind();
            divPaymentHistory.Visible = true;
        }
        else { divPaymentHistory.Visible = false; }
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
                criteria += "and er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
        }
        if (ddlRollNo.SelectedIndex != 0)
        {
            if (criteria == "")
                criteria = "RollNo=" + ddlRollNo.SelectedItem.Text;
            else
                criteria += " and RollNo=" + ddlRollNo.SelectedItem.Text;
        }
        if (ddlRegNo.SelectedIndex != 0)
        {
            if (criteria == "")
                criteria = "ss_Student.RegNo=" + ddlRegNo.SelectedItem.Text;
            else
                criteria += " and ss_Student.RegNo='" + ddlRegNo.SelectedItem.Text + "'";
        }

        criteria += " and ss_Student.StatusId=" + 1;

        return criteria;
    }

    #endregion   

    #region Button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        BillNo = "";
        if (CheckValidation())
        {
            ClearPopup();

            LoadStudent();
            LoadPaymentHistory();
            LoadOtherPayment();            
        }    
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

       
        if (tbxBillNo.Text == "")
        {
            MessageController.Show(" Please Insert BillNo....", MessageType.Warning, Page);            
            return;
        }
        if (tbxAmount.Text == "")
        {
            MessageController.Show("  Insert Amount First....", MessageType.Warning, Page);
            return;
        }
        if (Convert.ToDecimal(tbxAmount.Text)>Convert.ToDecimal(lblGrandTotal.Text))
        {
            MessageController.Show(" Paid Amount should Equal or less than Grand Total....", MessageType.Warning, Page);
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
        //if (tbxRoll.Text != "" || tbxReg.Text != "")
        //{ }
            foreach (DataRow row in dtPaymentReceive.Rows)
            {
                StudentPayment studentPayment = new StudentPayment();
                studentPayment.Amount = decimal.Parse(row["TotalDue"].ToString());
                studentPayment.TotalGiven = 0;
                if(paid!=0)
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
 
        try
        {
            if (CheckAmount(lstStudentPayment))
            {
                int result = objPayment.InsertStudentPayment(lstStudentPayment);
                if (result != -1)
                {
                    ClearPopup();
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                    rptReceivedPayment.DataSource = null;
                    rptReceivedPayment.DataBind();
                }
                else
                {
                    ClearPopup();
                    MessageController.Show("Bill No already exists...", MessageType.Error, Page);
                }
            }

        }
        catch (Exception)
        {
            MessageController.Show(MessageCode.SaveFailed, MessageType.Warning, Page);
        }
        LoadStudent();
        LoadOtherPayment(); 
        LoadPaymentHistory();
    }

    private bool CheckAmount(List<StudentPayment> lstStudentPayment)
    {
        bool res = true;
        DataTable dt = PaymentEdit();
        foreach (StudentPayment st in lstStudentPayment)
        {
            DataTable dtMonthlyOther = objpaymentType.GetPaymentTypeByIsDefault(true, false);
            if (dtMonthlyOther.Select("ID=" + st.PaymentTypeId).Length > 0)
            {
               DataTable dtGetFee= objPayment.GetMonthlyFees(st.StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue),
                    Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue),
                    Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue),
                    Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), st.Month,
                    st.PaymentTypeId);

                int paymentToClassId = Convert.ToInt32(dtGetFee.Rows[0]["Id"]);

                int updateId = Convert.ToInt32(dtGetFee.Rows[0]["StudentToClassId"]);


                decimal amount = Convert.ToDecimal(dtGetFee.Rows[0]["Amount"]);
                if (st.Amount>amount)
                {
                    dt.Rows.Add(st.StudentToClassId, paymentToClassId, st.PaymentTypeId, st.Month, st.Amount, updateId, StartMonth, EndMonth);
                }

                if (dt.Rows.Count > 0)
                {
                    int result = objpaymentType.StudentPaymentInformationInsert(dt, Page.User.Identity.Name, DateTime.Now);
                    if (result == 1)
                    {

                    }
                    else
                    {
                        res = false;
                    }
                }
            }
        }
        


        return res;
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
        MessageController.Clear(this);
        LoadNull();
        LoadRollRegNo();
    }
    protected void ddlRollNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        MessageController.Clear(this);
        BillNo = "";
        if (CheckValidation())
        {
            ClearPopup();
            LoadStudent();
            LoadPaymentHistory();
            LoadOtherPayment();
        }    
    }

    protected void ddlRegNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        DueAmount = 0;
        //BillNo = "";
        if (CheckValidation())
        {
            ClearPopup();
            LoadStudent();
            LoadPaymentHistory();
            LoadOtherPayment();
            tbxBillNo.ReadOnly = true;
        }    
    }
    protected void chkOtherPayment_OnCheckedChanged(object sender, EventArgs e)
    {
        
        decimal grantTotal = 0;
        decimal total = 0;
        dtPaymentReceive = PaymentReceive();
        MessageController.Clear(this);
        //if (grantTotal == 0 && lblPreviousDue.Text != "0.00")
        //{

            //foreach (RepeaterItem item in rptPaymentMonth.Items)
            //{
            //    CheckBox chkMonthlyPayment = (CheckBox)item.FindControl("chkMonthlyPayment");
            //    if (chkMonthlyPayment.Enabled)
            //        if (chkMonthlyPayment.Checked)
            //        {
            //            MessageController.Show("  Please pay other amounts first.....", MessageType.Warning, Page);
            //            chkMonthlyPayment.Checked = false;
            //            return;
            //        }
            //}
        //}
        foreach (RepeaterItem item in rptPaymentOthers.Items)
        {
            Label lblAmount = (Label)item.FindControl("lblAmount");
            CheckBox chkOtherPayment = (CheckBox)item.FindControl("chkOtherPayment");
            Label lblPaymentId = (Label)item.FindControl("lblPaymentId");
            Label lblPaymentType = (Label)item.FindControl("lblPaymentType");
            total = Convert.ToDecimal(lblAmount.Text);
            if(chkOtherPayment.Enabled)
            if (chkOtherPayment.Checked)
            {
                if (total != 0)
                {
                    DataTable dtPaid = objPayment.GetOthersPayment(StudentToClassId,
                        Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(lblPaymentId.Text));
                    if (dtPaid.Rows.Count > 0)
                    {
                        decimal dueAmount = 0;
                        foreach (DataRow rows in dtPaid.Rows)
                        {
                            dueAmount = Convert.ToDecimal(rows["DueAmount"]);
                        }
                        dtPaymentReceive.Rows.Add("Academic", Convert.ToInt32(ddlMonth.SelectedValue), dueAmount, Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(lblPaymentId.Text), lblPaymentType.Text);
                    }
                    else
                    {
                        dtPaymentReceive.Rows.Add("Academic", Convert.ToInt32(ddlMonth.SelectedValue), total, Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(lblPaymentId.Text), lblPaymentType.Text);
                        
                    }
                }
            }
        }
        CheckMonthlyPayment(); 
        CheckMonthlyOthersPayment();
        DataTable dtDistinct = dtPaymentReceive.DefaultView.ToTable(true, new string[] { "MonthYear", "Month", "TotalDue", "ClassId", "PaymentTypeId","PaymentType" });
        dtPaymentReceive = dtDistinct;
        foreach (DataRow row in dtPaymentReceive.Rows)
        {
            total = Convert.ToDecimal(row["TotalDue"]);
            grantTotal += total;
        }
        if (dtPaymentReceive.Rows.Count > 0)
        {
            rptReceivedPayment.DataSource = dtPaymentReceive;
            rptReceivedPayment.DataBind();

            Label lbltotal = (Label)rptReceivedPayment.Controls[rptReceivedPayment.Controls.Count - 1].FindControl("lblTotal");

            lbltotal.Text = grantTotal.ToString(); 
        }
        else
        {
             rptReceivedPayment.DataSource = null;
            rptReceivedPayment.DataBind();
        }
            lblGrandTotal.Text = grantTotal.ToString();            
            popPreviousDue.Text = lblPreviousDue.Text;
            popLateFee.Text = lblFine.Text;
            popScholar.Text = lblScholar.Text;
            popTotal.Text = lblTotal.Text;
            popGrandTotal.Text = lblGrandTotal.Text;     
    }

    private decimal CalculateMonthlyPayment(string amount, string hdnMonthId, string hdnTypeId, string monthYear, string paymentType)
    {
        decimal total = 0;
        DataTable dtPaid = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(hdnTypeId), Convert.ToInt32(hdnMonthId));
        if (dtPaid.Rows.Count > 0)
        {
            foreach (DataRow item in dtPaid.Rows)
            {
                if (item["DueAmount"].ToString() != "0.00")
                {
                    total = Convert.ToDecimal(item["DueAmount"]);
                } 
            }
            
        }
        else
            total = Convert.ToDecimal(amount);
       if(amount!="0.00")
           dtPaymentReceive.Rows.Add(monthYear, Convert.ToInt32(hdnMonthId), total, Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(hdnTypeId), paymentType);
        return total;
    }
    private void CheckMonthlyPayment()
    {
        for (int i = 0; rptPaymentMonth.Items.Count > i; i++)
        {
            Label lblAmount = (Label)rptPaymentMonth.Items[i].FindControl("lblAmount");
            CheckBox chkMonthlyPayment = (CheckBox)rptPaymentMonth.Items[i].FindControl("chkMonthlyPayment");
            HiddenField hdnMonthId = (HiddenField)rptPaymentMonth.Items[i].FindControl("hdnMonthId");
            Label hdnTypeId = (Label)rptPaymentMonth.Items[i].FindControl("hdnPaymentTypeId");
            Label lblMonthYear = (Label)rptPaymentMonth.Items[i].FindControl("lblMonthYear");
            Label lblPaymentType = (Label)rptPaymentMonth.Items[i].FindControl("lblPaymentType");
            if (chkMonthlyPayment.Checked && chkMonthlyPayment.Enabled)
            {
                //for (int j = i - 1; rptPaymentMonth.Items.Count > j; j--)
                //{
                //    if (j > -1)
                //    {
                //        CheckBox chk1 = (CheckBox)rptPaymentMonth.Items[j].FindControl("chkMonthlyPayment");
                //        Label lblMonthYear1 = (Label)rptPaymentMonth.Items[j].FindControl("lblMonthYear");

                //        if (chk1.Enabled && !chk1.Checked)
                //        {
                //            chkMonthlyPayment.Checked = false;
                //            MessageController.Show("Please pay " + lblMonthYear1.Text + " dues 1st. Then select " + lblMonthYear.Text, MessageType.Error, Page);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                CalculateMonthlyPayment(lblAmount.Text, hdnMonthId.Value, hdnTypeId.Text, lblMonthYear.Text, lblPaymentType.Text);
            }
        }
    }
    private void CheckMonthlyOthersPayment()
    {
        for (int i = 0; rptPaymentMonthlyOthers.Items.Count > i; i++)
        {

            CheckBox chkMonthlyPayment = (CheckBox)rptPaymentMonthlyOthers.Items[i].FindControl("chkMonthlyPayment");
            HiddenField hdnMonthId = (HiddenField)rptPaymentMonthlyOthers.Items[i].FindControl("hdnMonthId");
            Label lblMonthYear = (Label)rptPaymentMonthlyOthers.Items[i].FindControl("lblMonthYear");
            Label lblItem1 = (Label)rptPaymentMonthlyOthers.Items[i].FindControl("lblItem1");
            Label lblItem2 = (Label)rptPaymentMonthlyOthers.Items[i].FindControl("lblItem2");
            Label lblItem3 = (Label)rptPaymentMonthlyOthers.Items[i].FindControl("lblItem3");
            Label lblItem4 = (Label)rptPaymentMonthlyOthers.Items[i].FindControl("lblItem4");
            Label lblItem5 = (Label)rptPaymentMonthlyOthers.Items[i].FindControl("lblItem5");
            TextBox tbxItem1 = (TextBox)rptPaymentMonthlyOthers.Items[i].FindControl("tbxItem1");
            TextBox tbxItem2 = (TextBox)rptPaymentMonthlyOthers.Items[i].FindControl("tbxItem2");
            TextBox tbxItem3 = (TextBox)rptPaymentMonthlyOthers.Items[i].FindControl("tbxItem3");
            TextBox tbxItem4 = (TextBox)rptPaymentMonthlyOthers.Items[i].FindControl("tbxItem4");
            TextBox tbxItem5 = (TextBox)rptPaymentMonthlyOthers.Items[i].FindControl("tbxItem5");

            Label lblHeadr1 = (Label)rptPaymentMonthlyOthers.Controls[0].Controls[0].FindControl("lblHeadr1");
            Label lblHeadr2 = (Label)rptPaymentMonthlyOthers.Controls[0].Controls[0].FindControl("lblHeadr2");
            Label lblHeadr3 = (Label)rptPaymentMonthlyOthers.Controls[0].Controls[0].FindControl("lblHeadr3");
            Label lblHeadr4 = (Label)rptPaymentMonthlyOthers.Controls[0].Controls[0].FindControl("lblHeadr4");
            Label lblHeadr5 = (Label)rptPaymentMonthlyOthers.Controls[0].Controls[0].FindControl("lblHeadr5"); 

            if (chkMonthlyPayment.Checked && chkMonthlyPayment.Enabled)
            {
                if (!string.IsNullOrEmpty(tbxItem1.Text) && tbxItem1.Visible)
                {
                    CalculateMonthlyPayment(tbxItem1.Text, hdnMonthId.Value, lblItem1.Text, lblMonthYear.Text, lblHeadr1.Text);
                    if (!string.IsNullOrEmpty(tbxItem2.Text) && tbxItem2.Visible)
                    {
                        CalculateMonthlyPayment(tbxItem2.Text, hdnMonthId.Value, lblItem2.Text, lblMonthYear.Text, lblHeadr2.Text);
                        if (!string.IsNullOrEmpty(tbxItem3.Text) && tbxItem3.Visible)
                        {
                            CalculateMonthlyPayment(tbxItem3.Text, hdnMonthId.Value, lblItem3.Text, lblMonthYear.Text, lblHeadr3.Text);
                            if (!string.IsNullOrEmpty(tbxItem4.Text) && tbxItem4.Visible)
                            {
                                CalculateMonthlyPayment(tbxItem4.Text, hdnMonthId.Value, lblItem4.Text, lblMonthYear.Text, lblHeadr4.Text);
                                if (!string.IsNullOrEmpty(tbxItem5.Text) && tbxItem5.Visible)
                                {
                                    CalculateMonthlyPayment(tbxItem5.Text, hdnMonthId.Value, lblItem5.Text, lblMonthYear.Text, lblHeadr5.Text);
                                }
                            }
                        }
                    }
                }
            }
        }
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
            CheckBox chkOther = (CheckBox)e.Item.FindControl("chkOtherPayment");
            chkOther.Enabled = true;
            chkOther.Checked = false;
            if (lblStatus.Text == "N/A")
            {
                chkOther.Enabled = false;
                chkOther.Checked = false;
            }
            if (lblStatus.Text == "Paid")
            {
                chkOther.Enabled = false;
                chkOther.Checked = true;                
                //grantTotal += total;
                    //dtPaymentReceive.Rows.Add("Academic",0, total, Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(lblPaymentId.Text));                
            }
            
        }
    }
    protected void rptPaymentMonthly_ItemDataBound(object sender, RepeaterItemEventArgs e)  
    {

        DataTable dtMonthly = objpaymentType.GetPaymentTypeByIsDefault(true, false);

     
        if (e.Item.ItemType == ListItemType.Header)
        {
            Label lblHeadr1 = (Label)e.Item.FindControl("lblHeadr1");
            Label lblHeadr2 = (Label)e.Item.FindControl("lblHeadr2");
            Label lblHeadr3 = (Label)e.Item.FindControl("lblHeadr3");
            Label lblHeadr4 = (Label)e.Item.FindControl("lblHeadr4");
            Label lblHeadr5 = (Label)e.Item.FindControl("lblHeadr5");
           
            if (dtMonthly.Rows.Count > 0)
            {
                lblHeadr1.Visible = true;
                lblHeadr1.Text = dtMonthly.Rows[0]["PaymentType"].ToString();
                if (dtMonthly.Rows.Count > 1)
                {
                    lblHeadr2.Visible = true;
                    lblHeadr2.Text = dtMonthly.Rows[1]["PaymentType"].ToString();
                    if (dtMonthly.Rows.Count > 2)
                    {
                        lblHeadr3.Visible = true;
                        lblHeadr3.Text = dtMonthly.Rows[2]["PaymentType"].ToString();
                        if (dtMonthly.Rows.Count > 3)
                        {
                            lblHeadr4.Visible = true;
                            lblHeadr4.Text = dtMonthly.Rows[3]["PaymentType"].ToString();
                            if (dtMonthly.Rows.Count > 4)
                            {
                                lblHeadr5.Visible = true;
                                lblHeadr5.Text = dtMonthly.Rows[4]["PaymentType"].ToString();
                            }
                            else
                            {
                                lblHeadr5.Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                            }
                        }
                        else
                        {
                            lblHeadr4.Visible = false;
                            lblHeadr5.Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                        }
                    }
                    else
                    {
                        lblHeadr3.Visible = false;
                        lblHeadr4.Visible = false;
                        lblHeadr5.Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader3")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                    }
                }
                else
                {
                    lblHeadr2.Visible = false;
                    lblHeadr3.Visible = false;
                    lblHeadr4.Visible = false;
                    lblHeadr5.Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader2")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader3")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
                }
            }
            else
            {
                lblHeadr1.Visible = false;
                lblHeadr2.Visible = false;
                lblHeadr3.Visible = false;
                lblHeadr4.Visible = false;
                lblHeadr5.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader1")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader2")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader3")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader4")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdHeader5")).Visible = false;
            }
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chkMonthlyPayment = (CheckBox)e.Item.FindControl("chkMonthlyPayment");
            HiddenField hdnMonthId = (HiddenField)e.Item.FindControl("hdnMonthId");
            Label hdnTypeId = (Label)e.Item.FindControl("hdnPaymentTypeId");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            Label lblDueAmount = (Label)e.Item.FindControl("lblDueAmount");

            //if (lblStatus.Text == "N/A"){
            ////{
            ////    lblAmount.Visible = false;
            ////    lblStatus.Visible = true;
            ////    chkMonthlyPayment.Enabled = false;
            //}
            //Individual due calculate
            //DueAmount = 0;

            TextBox tbxItem1 = (TextBox)e.Item.FindControl("tbxItem1");
            TextBox tbxItem2 = (TextBox)e.Item.FindControl("tbxItem2");
            TextBox tbxItem3 = (TextBox)e.Item.FindControl("tbxItem3");
            TextBox tbxItem4 = (TextBox)e.Item.FindControl("tbxItem4");
            TextBox tbxItem5 = (TextBox)e.Item.FindControl("tbxItem5");            
            Label lblItem1 = (Label)e.Item.FindControl("lblItem1");
            Label lblItem2 = (Label)e.Item.FindControl("lblItem2");
            Label lblItem3 = (Label)e.Item.FindControl("lblItem3");
            Label lblItem4 = (Label)e.Item.FindControl("lblItem4");
            Label lblItem5 = (Label)e.Item.FindControl("lblItem5");
            
            if (dtMonthly.Rows.Count > 0)
            {
                tbxItem1.Visible = true;
                tbxItem1.ReadOnly = true;
                lblItem1.Text = dtMonthly.Rows[0]["Id"].ToString();
                
                tbxItem1.Text = LoadMonthlyOthersPayment(lblItem1.Text, hdnMonthId.Value).ToString();
                if (dtMonthly.Rows.Count > 1)
                {
                    tbxItem2.Visible = true;
                    lblItem2.Text = dtMonthly.Rows[1]["Id"].ToString();
                    tbxItem2.Text = LoadMonthlyOthersPayment(lblItem2.Text, hdnMonthId.Value).ToString();
                    if (dtMonthly.Rows.Count > 2)
                    {
                        tbxItem3.Visible = true;
                        lblItem3.Text = dtMonthly.Rows[2]["Id"].ToString();
                        tbxItem3.Text = LoadMonthlyOthersPayment(lblItem3.Text, hdnMonthId.Value).ToString();
                        if (dtMonthly.Rows.Count > 3)
                        {
                            tbxItem4.Visible = true;
                            lblItem4.Text = dtMonthly.Rows[3]["Id"].ToString();
                            tbxItem4.Text = LoadMonthlyOthersPayment(lblItem4.Text, hdnMonthId.Value).ToString();
                            if (dtMonthly.Rows.Count > 4)
                            {
                                tbxItem5.Visible = true;
                                lblItem5.Text = dtMonthly.Rows[4]["Id"].ToString();
                                tbxItem5.Text = LoadMonthlyOthersPayment(lblItem5.Text, hdnMonthId.Value).ToString();
                            }
                            else
                            {
                                tbxItem5.Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                            }
                        }
                        else
                        {                     
                            tbxItem4.Visible = false;
                            tbxItem5.Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                        }
                    }
                    else
                    {                      
                        tbxItem3.Visible = false;
                        tbxItem4.Visible = false;
                        tbxItem5.Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem3")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                    }

                }
                else
                {
                    tbxItem2.Visible = false;
                    tbxItem3.Visible = false;
                    tbxItem4.Visible = false;
                    tbxItem5.Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem2")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem3")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
                }
            }
            else
            {
                tbxItem1.Visible = false;
                tbxItem2.Visible = false;
                tbxItem3.Visible = false;
                tbxItem4.Visible = false;
                tbxItem5.Visible = false;

                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem1")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem2")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem3")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem4")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdItem5")).Visible = false;
            }

            lblDueAmount.Text = DueAmount.ToString();

        }
    }
    private decimal LoadMonthlyOthersPayment(string paymentTypeId,string monthId)
    {
        decimal amount = 0;
        decimal given = 0;
        string Flag="N";
        DataTable dtMonthlyFee = objPayment.GetMonthlyFees(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(monthId), Convert.ToInt32(paymentTypeId));
        if(dtMonthlyFee.Rows.Count>0)
        amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
       
        DataTable dtPaid = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(paymentTypeId), Convert.ToInt32(monthId));
        if (dtMonthlyFee.Rows.Count > 0)
        {            
            foreach (DataRow row in dtPaid.Rows)
            {
                given += Convert.ToDecimal(row["TotalGiven"]);
                //amount = given;
            }
            //if (amount == given)
            //{
            //    Flag = "Y";
            //}
            //else
            //{
            //    amount -= given;
            //}
        
        }

        DueAmount += amount - given; 
        

        return amount;
    }
    #endregion

    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (rptHistory.Items.Count > 0)
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=StudentReportHistory&Type=" + StudentToClassId + "&SelectId=" + ddlClass.SelectedValue + "&AnonymusType="+ ddlMedium.SelectedItem.Text + "&TotalAmount=" +totalAmount+"&TotalRcv="+totalRcv+"&TotalDue="+totalDue );       

        }

        else
        {
            MessageController.Show("Please search first!!!", MessageType.Warning, Page);
        }
    }
}





