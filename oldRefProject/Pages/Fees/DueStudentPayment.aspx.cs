using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Fees_DueStudentPayment : System.Web.UI.Page
{
    dalStudent dalstu = new dalStudent();
    dalPaymentType objpaymentType = new dalPaymentType();
    dalPayment objPayment = new dalPayment();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LoadData();
            //btnGenerateReport.Visible = false;
            divPaymentHistory.Visible = false;
        }
    }

    #region LoadData
    protected void LoadData()
    {

        //ddlYear.DataSource = new dalYear().GetDefault();
        //ddlYear.DataBind();
        SecondaryClassIds = "";
        HigherClassIds = "";
        DataTable dtClass = new Common().GetAll("bs_ClassName");

        ddlClass.DataSource = dtClass;
        ddlClass.DataBind();

        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlClass.Items.Insert(0, "All Secondary Classes");
        ddlClass.SelectedIndex = 0;
        ddlClass.Items.Insert(1, "All Higher Classes");
        //ddlClass.SelectedIndex = 1;
        foreach (DataRow row in dtClass.Rows)
        {
            if (row["ClassName"].ToString().ToUpper().Contains("college".ToUpper()))
            {
                if (HigherClassIds != "")
                    HigherClassIds += ",";
                HigherClassIds += row["Id"].ToString();
            }
            else
            {
                if (SecondaryClassIds != "")
                    SecondaryClassIds += ",";
                SecondaryClassIds += row["Id"].ToString();
            }
        }

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        ddlMonth.DataSource = new Common().GetAll("bs_Month");
        ddlMonth.DataBind();

        ddlPaymentType.DataSource = new Common().GetAll("fee_PaymentType");
        ddlPaymentType.DataBind();

        ddlPaymentType.Items.Insert(0, new ListItem("---All---", "-1"));
        ddlPaymentType.Items.Insert(0, "---Select---");
        ddlPaymentType.SelectedIndex = 0;

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
            if (ddlClass.SelectedItem.Text.ToUpper().Contains("All".ToUpper()))
            {
                if (rdList.SelectedValue == "1")
                {
                    if (ddlClass.SelectedItem.Text.ToUpper().Contains("Higher".ToUpper()))
                    {
                        if (criteria == "")
                            criteria = "er_StudentToClass.ClassId in (" + HigherClassIds + ")";
                        else
                            criteria += " and er_StudentToClass.ClassId in (" + HigherClassIds + ")";
                    }
                    else
                    {
                        if (criteria == "")
                            criteria = "er_StudentToClass.ClassId in (" + SecondaryClassIds + ")";
                        else
                            criteria += " and er_StudentToClass.ClassId in (" + SecondaryClassIds + ")";
                    }
                }
                else
                {
                    criteria = "";

                }
            }
            else
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
                else
                    criteria += " and er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
            }
        }


        if (rdList.SelectedValue == "2")
        {
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
            if (tbxRegNo.Text != "")
            {
                if (criteria == "")
                    criteria = "ss_Student.RegNo=" + tbxRegNo.Text;
                else
                    criteria += " and ss_Student.RegNo='" + tbxRegNo.Text + "'";
            }
        }

        criteria += " and ss_Student.StatusId= 1";

        return criteria;
    }

    #endregion
    #region Variable
    int StudentToClassId
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
    decimal MonthlyReceived
    {
        set { ViewState["MonthlyReceived"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["MonthlyReceived"]);
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
    DataTable dtTotalDue
    {
        set { ViewState["dtTotalDue"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtTotalDue"];
            }
            catch
            {
                return null;
            }
        }
    }

    string HigherClassIds
    {
        set { ViewState["HigherClassIds"] = value; }
        get
        {
            try
            {
                return ViewState["HigherClassIds"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    string SecondaryClassIds
    {
        set { ViewState["SecondaryClassIds"] = value; }
        get
        {
            try
            {
                return ViewState["SecondaryClassIds"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }

    public DataTable StudentDue()
    {
        DataTable dt = new DataTable("TotalDue");
        dt.Columns.Add("StudentId", typeof(int));
        dt.Columns.Add("RegNo", typeof(string));
        dt.Columns.Add("NameEng", typeof(string));
        dt.Columns.Add("CampusName", typeof(string));
        dt.Columns.Add("MediumName", typeof(string));
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("Year", typeof(string));
        //dt.Columns.Add("Month", typeof(string));
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("Amount", typeof(double));
        dt.Columns.Add("TotalGiven", typeof(double));
        dt.Columns.Add("DueAmount", typeof(double));
        return dt;
    }
    public DataTable TotalDue()
    {
        DataTable dt = new DataTable("TotalDue");
        dt.Columns.Add("StudentToClassId", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("RegNo", typeof(string));
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("Year", typeof(string));
        dt.Columns.Add("Total", typeof(double));
        dt.Columns.Add("Paid", typeof(double));
        dt.Columns.Add("Due", typeof(double));
        dt.Columns.Add("Month", typeof(string));
        return dt;
    }
    private void Clear()
    {
        tbxRegNo.Text = "";
        tbxRoll.Text = "";
        ddlGroup.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
    }
    protected void LoadStudent()
    {
        string criteria = GetCriteria();
        DataTable dt = new DataTable();
        int studentId = 0;
        DataTable dts = new dalStudent().GetByCriteria(criteria);
        if (dts.Rows.Count == 1)
        {
            studentId = Convert.ToInt32(dts.Rows[0]["StudentId"]);
        }

    }
    #endregion
    #region Button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (CheckValidation())
        {
            if (ddlSelect.SelectedValue == "1")
            {
                LoadStudentPaymentDue();
            }
            else
            {
                LoadStudentPayment();
            }
            //LoadStudentPayment();
            //Clear();
        }
    }
    private void LoadStudentPaymentDue()
    {
        string criteria = GetCriteria();

        decimal total = 0;
        decimal totalGiven = 0;
        decimal totalDue = 0;
        int monthId = 0;
        DataTable dtDue = StudentDue();
        DataTable dtPaymentType = ddlPaymentType.SelectedIndex > 1 ? new dalPaymentType().GetById(Convert.ToInt32(ddlPaymentType.SelectedValue)) : new DataTable();

        bool isMonthly = dtPaymentType.Rows.Count > 0 ? Convert.ToBoolean(dtPaymentType.Rows[0]["IsMonthly"]) : true;
        if (dtPaymentType.Rows.Count > 0)
        {
            if (dtPaymentType.Rows[0]["IsMonthly"].ToString() == "True")
                monthId = Convert.ToInt32(ddlMonth.SelectedValue);
        }
        DataTable dtStudent = new dalStudent().GetByCriteria(criteria);
        if (isMonthly)
        {
            if (ddlMonth.SelectedValue != "")
            {
                criteria += " and ss_Payment.Month=" + ddlMonth.SelectedValue;
            }
        }

        if (ddlPaymentType.SelectedIndex > 1 && ddlPaymentType.SelectedValue != "")
        {
            criteria += " and ss_Payment.PaymentTypeId=" + ddlPaymentType.SelectedValue;
        }

        DataTable dtPayment = new dalPayment().GetStudentDueByCriteria(criteria);


        if (dtStudent.Rows.Count > 0)
        {
            foreach (DataRow item in dtStudent.Rows)
            {
                string studentToClassId = item["StudentToClassId"].ToString();
                string regNo = item["RegNo"].ToString();
                string nameEng = item["NameEng"].ToString();
                string campusName = item["CampusName"].ToString();
                string mediumName = item["MediumName"].ToString();
                string className = item["ClassName"].ToString();
                int classId = Convert.ToInt32(item["ClassId"]);
                string year = item["Year"].ToString();
                string paymentType = "";

                int count = 0;
                decimal monthlyAmount = 0;
                decimal monthlyGiven = 0;
                decimal monthlyDue = 0;

                DataTable dtMonthlyDue = new dalPayment().GetMonthlyDues(Convert.ToInt32(item["StudentToClassId"]),
                        Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue),
                        Convert.ToInt32(ddlCampus.SelectedValue), classId,
                        monthId, Convert.ToInt32(ddlPaymentType.SelectedValue));

                if (dtMonthlyDue.Rows.Count > 0)
                {
                    monthlyAmount = Convert.ToDecimal(dtMonthlyDue.Rows[0]["Amount"]);

                    foreach (DataRow dataRow in dtPayment.Rows)
                    {
                        if (studentToClassId == dataRow["StudentToClassId"].ToString())
                        {
                            monthlyGiven += Convert.ToDecimal(dataRow["TotalGiven"]);
                        }
                    }

                }

                dtDue.Rows.Add(studentToClassId, regNo, nameEng, campusName, mediumName, className, ddlYear.SelectedItem.Text, ddlPaymentType.SelectedIndex > 1 ? dtMonthlyDue.Rows[0]["PaymentType"] : "All", monthlyAmount, monthlyGiven, monthlyAmount - monthlyGiven);
                total += monthlyAmount;
                totalGiven += monthlyGiven;
            }

            rptHistory.DataSource = dtDue;
            rptHistory.DataBind();
            Label lblTotal = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblTotal");
            Label lblTotalGiven = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblReceived");
            Label lblTotalDue = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblDue");

            lblTotal.Text = total.ToString();
            lblTotalGiven.Text = totalGiven.ToString();
            lblTotalDue.Text = (total - totalGiven).ToString();
            divPaymentHistory.Visible = true;
        }
        else
        {
            MessageController.Show("No record found!!!", MessageType.Warning, Page);
            rptHistory.DataSource = null;
            rptHistory.DataBind();
            divPaymentHistory.Visible = false;
        }
    }
    //protected void btnGenerateReport_Click(object sender, EventArgs e)
    //{
    //    if (rptHistory.Items.Count > 0)
    //    {
    //        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=classWiseStudentDueList&Type=" + ddlSelect.SelectedValue);
    //    }

    //    else
    //    {
    //        MessageController.Show("Please search first!!!", MessageType.Warning, Page);
    //    }

    //}
    #endregion
    private bool CheckValidation()
    {
        bool result = true;
        if (rdList.SelectedValue == "2")
            if (tbxRegNo.Text == "")
            {
                tbxRegNo.BorderColor = System.Drawing.Color.Red;
                MessageController.Show(" Enter Student ID First....", MessageType.Warning, Page);
                result = false;
            }
        if (ddlPaymentType.SelectedIndex == 0)
        {
            MessageController.Show("Please Select Payment Type....", MessageType.Warning, Page);
            result = false;
        }
        return result;
    }

    private void LoadStudentPayment()
    {

        decimal amount = 0;
        decimal totalAmount = 0;
        decimal totalDue = 0;
        decimal totalReceived = 0;

        int paymentTypeId = ddlPaymentType.SelectedIndex > 1 ? Convert.ToInt32(ddlPaymentType.SelectedValue) : -1;
        string paymentType = ddlPaymentType.SelectedIndex > 1 ? ddlPaymentType.SelectedItem.Text : "All";

        dtTotalDue = StudentDue();
        string criteria = GetCriteria();
        DataTable dtAcademicPayment = new DataTable();

        DataTable dtStudent = new dalStudent().GetByCriteria(criteria);

        for (int i = 0; dtStudent.Rows.Count > i; i++)
        {

            decimal total = 0;
            decimal received = 0;
            decimal dueAmount = 0;
            decimal dueMonth = 0;
            StudentToClassId = Convert.ToInt32(dtStudent.Rows[i]["StudentToClassId"]);
            int classId = Convert.ToInt32(dtStudent.Rows[i]["ClassId"]);
            int mediumId = Convert.ToInt32(dtStudent.Rows[i]["MediumId"]);
            int campusId = Convert.ToInt32(dtStudent.Rows[i]["CampusId"]);
            int groupId = Convert.ToInt32(dtStudent.Rows[i]["GroupId"]);
            int shiftId = Convert.ToInt32(dtStudent.Rows[i]["ShiftId"]);
            int sectionId = Convert.ToInt32(dtStudent.Rows[i]["SectionId"]);
            string name = dtStudent.Rows[i]["NameEng"].ToString();
            string regNo = dtStudent.Rows[i]["RegNo"].ToString();
            string className = dtStudent.Rows[i]["ClassName"].ToString();
            string campusName = dtStudent.Rows[i]["CampusName"].ToString();
            string mediumName = dtStudent.Rows[i]["MediumName"].ToString();
            string mobile = dtStudent.Rows[i]["Mobile"].ToString();

            DataTable dtPaymentType = ddlPaymentType.SelectedIndex > 1 ? new dalPaymentType().GetById(Convert.ToInt32(ddlPaymentType.SelectedValue)) : new DataTable();

            bool isMonthly = ddlPaymentType.SelectedIndex > 1 ? Convert.ToBoolean(dtPaymentType.Rows[0]["IsMonthly"]) : true;

            dtAcademicPayment = new dalPaymentType().GetPaymentWithoutDefault(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), mediumId, campusId, classId, groupId, shiftId, sectionId, false, false);

            if (dtAcademicPayment.Rows.Count > 0)
            {
                int monthId = 0;
                MonthlyReceived = 0;
                int year = DateTime.Now.Year;
                int StartMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["StartMonth"]);
                int EndMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["EndMonth"]);



                if (StartMonth < EndMonth)
                {
                    for (int j = StartMonth; EndMonth + 1 > j; j++)
                    {
                        monthId = j;
                        dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);
                    }
                }
                else
                {
                    for (int j = StartMonth; 13 > j; j++)
                    {
                        monthId = j;
                        dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);
                    }
                    for (int j = 1; EndMonth + 1 > j; j++)
                    {
                        monthId = j;
                        year = DateTime.Now.Year + 1;
                        dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);
                    }
                }






                total += dueMonth;
                received += MonthlyReceived;

                totalAmount += total;

                dtTotalDue.Rows.Add(StudentToClassId, regNo, name, campusName, mediumName, className, ddlYear.SelectedItem.Text, paymentType, total, received, total - received);
                totalDue += (total - received);
                totalReceived += received;

            }
        }

        if (dtTotalDue.Rows.Count > 0)
        {


            rptHistory.DataSource = dtTotalDue;
            rptHistory.DataBind();
            Label lblTotal = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblTotal");
            Label lblReceived = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblReceived");
            Label lblDue = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblDue");
            lblTotal.Text = totalAmount.ToString();
            lblReceived.Text = totalReceived.ToString();
            lblDue.Text = totalDue.ToString();
            divPaymentHistory.Visible = true;
        }
        else
        {

            divPaymentHistory.Visible = false;
            rptHistory.DataSource = null;
            rptHistory.DataBind();
            MessageController.Show("No Data Found!!!", MessageType.Warning, Page);
        }

    }
    private decimal LoadMonthRpt(int StudentToClassId, int year, int mediumId, int campusId, int classId, int groupId, int shiftId, int sectionId, int monthId, int paymentTypeId)
    {
        string month = "";
        decimal amount = 0;
        decimal dueMonth = 0;
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;

        DataTable dtMonth = new dalPayment().GetMonthById(monthId);
        DataTable dtMonthlyFee = new dalPayment().GetMonthlyFees(StudentToClassId,
        Convert.ToInt32(ddlYear.SelectedValue), mediumId, campusId, classId, groupId, shiftId, sectionId,
        monthId, paymentTypeId);

        if (dtMonthlyFee.Rows.Count > 0)
        {
            month = dtMonth.Rows[0]["Month"].ToString();
            amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
            paymentTypeId = ddlPaymentType.SelectedIndex > 1 ? Convert.ToInt32(dtMonthlyFee.Rows[0]["PaymentTypeId"]) : -1;
            if (amount != 0)
            {
                decimal totalReceive = 0;
                DataTable dtPaid = new dalPayment().GetMonthlyPayment(StudentToClassId,
                    Convert.ToInt32(ddlYear.SelectedValue), classId, paymentTypeId, monthId);
                if (dtPaid.Rows.Count > 0)
                {
                    decimal dueAmount = 0;
                    foreach (DataRow rows in dtPaid.Rows)
                    {
                        dueAmount = Convert.ToDecimal(rows["Amount"]);
                        totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                    }
                }
                if (ddlSelect.SelectedValue == "2")
                {
                    if (currentMonth >= monthId && year == currentYear)
                    {
                        dueMonth += amount;
                        MonthlyReceived += totalReceive;
                    }
                }
                else
                {
                    dueMonth += amount;
                    MonthlyReceived += totalReceive;
                }


            }
        }

        return dueMonth;
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
            tbxRoll.Visible = false;
            lblRoll.Visible = false;

        }
        else
        {
            LabelReg.Visible = false;
            tbxRegNo.Visible = false;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (rptHistory.Items.Count > 0)
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=DueStudentPaymentList&Type=" + ddlPaymentType.SelectedValue + "&MonthId=" + ddlMonth.SelectedValue + "&SelectId=" + ddlSelect.SelectedItem.Text);

        }

        else
        {
            MessageController.Show("Please search first!!!", MessageType.Warning, Page);
        }
    }


    private decimal LoadAllStudentMonthlyPayment(DataTable dtStudent, int monthId, int paymentTypeId, int year, DataTable dt)
    {
        string month = "";
        string className = "";
        string campusName = "";
        string mediumName = "";
        string paymentType = "";
        DataTable dtPaid = new DataTable();
        decimal dueMonth = 0;
        decimal monthlyReceived = 0;
        foreach (DataRow item in dtStudent.Rows)
        {
            int studentToClassId = Convert.ToInt32(item["StudentToClassId"]);
            int classId = Convert.ToInt32(item["ClassId"]);
            int mediumId = Convert.ToInt32(item["MediumId"]);
            int campusId = Convert.ToInt32(item["CampusId"]);
            int groupId = Convert.ToInt32(item["GroupId"]);
            int shiftId = Convert.ToInt32(item["ShiftId"]);
            int sectionId = Convert.ToInt32(item["SectionId"]);
            className = ddlClass.SelectedItem.Text; ;
            campusName = item["CampusName"].ToString();
            mediumName = item["MediumName"].ToString();


            decimal amount = 0;

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            if (monthId != 0)
            {
                DataTable dtMonth = new dalPayment().GetMonthById(monthId);
                month = dtMonth.Rows[0]["Month"].ToString();
            }
            else
            {
                month = "Academic";
            }
            DataTable dtMonthlyFee = new dalPayment().GetMonthlyFees(studentToClassId, Convert.ToInt32(ddlYear.SelectedValue), mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);

            if (dtMonthlyFee.Rows.Count > 0)
            {

                amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
                paymentType = dtMonthlyFee.Rows[0]["PaymentType"].ToString();
                if (amount != 0)
                {
                    decimal totalReceive = 0;
                    if (monthId != 0)
                    {
                        // Monthly Paid Payment

                        dtPaid = new dalPayment().GetMonthlyPayment(studentToClassId, Convert.ToInt32(ddlYear.SelectedValue), classId, paymentTypeId, monthId);
                    }
                    else
                    {
                        // Academic Paid Payment 
                        dtPaid = new dalPayment().GetOthersPayment(studentToClassId, Convert.ToInt32(ddlYear.SelectedValue), classId, paymentTypeId);
                    }

                    if (dtPaid.Rows.Count > 0)
                    {
                        decimal dueAmount = 0;
                        foreach (DataRow rows in dtPaid.Rows)
                        {
                            dueAmount = Convert.ToDecimal(rows["Amount"]);
                            totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                        }
                    }

                    dueMonth += amount;
                    monthlyReceived += totalReceive;
                    //if (ddlSelect.SelectedValue == "2")
                    //{
                    //    if (currentMonth >= monthId && year == currentYear)
                    //    {

                    //    }
                    //}
                    //else
                    //{
                    //    dueMonth += amount;
                    //    MonthlyReceived += totalReceive;
                    //}
                }
            }


        }

        dt.Rows.Add(monthId, month + "-" + year, campusName, mediumName, className, year, paymentType, dueMonth, monthlyReceived, dueMonth - monthlyReceived);
        MonthlyReceived += monthlyReceived;
        return dueMonth;

    }



}



