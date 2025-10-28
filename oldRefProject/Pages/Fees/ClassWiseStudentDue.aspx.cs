using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Fees_ClassWiseStudentDue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            divDueList.Visible = false;
            divPaymentHistory.Visible = false;
        }
    }

    #region LoadData

    protected void Load()
    {
        SecondaryClassIds = "";
        HigherClassIds = "";
        DataTable dtClass=new Common().GetAll("bs_ClassName");
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = dtClass;
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();
        //ddlPaymentType.DataSource = new dalPaymentType().GetPaymentTypeByIsMonthly(true);
        ddlPaymentType.DataSource = new Common().GetAll("fee_PaymentType");
        ddlPaymentType.DataBind();

        ddlPaymentType.Items.Insert(0, "---Select---");
        ddlPaymentType.SelectedIndex = 0;

        ddlMedium.Items.Insert(0, "---All---");
        ddlMedium.SelectedIndex = 0;

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
    }
    #endregion

    #region Get Criteria

    
    protected string GetCriterias()
    {
        string criteria = "";

        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
                criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
            else
                criteria += " er_StudentToClass.Year=" + ddlYear.SelectedValue;
        }
        if (ddlMedium.SelectedIndex !=0 && ddlMedium.SelectedValue!="")
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

    public DataTable DtPayment()
    {
        DataTable dtpayment = new DataTable("TotalPayment");
        dtpayment.Columns.Add("MonthId", typeof(string));
        dtpayment.Columns.Add("Month", typeof(string));
        dtpayment.Columns.Add("CampusName", typeof(string));
        dtpayment.Columns.Add("MediumName", typeof(string));
        dtpayment.Columns.Add("ClassName", typeof(string));
        dtpayment.Columns.Add("Year", typeof(string));
        //dt.Columns.Add("Month", typeof(string));
        dtpayment.Columns.Add("PaymentType", typeof(string));
        dtpayment.Columns.Add("Amount", typeof(double));
        dtpayment.Columns.Add("TotalGiven", typeof(double));
        dtpayment.Columns.Add("DueAmonunt", typeof(double));
        return dtpayment;
    }
    public DataTable TotalDue()
    {
        
        DataTable dt = new DataTable("TotalDue");
        dt.Columns.Add("StudentId", typeof(int));
        dt.Columns.Add("NameEng", typeof(string));
        dt.Columns.Add("RegNo", typeof(string));
        dt.Columns.Add("ClassName", typeof(string));
        dt.Columns.Add("Year", typeof(string));
        dt.Columns.Add("Amount", typeof(double));
        dt.Columns.Add("TotalGiven", typeof(double));
        dt.Columns.Add("DueAmount", typeof(double));
        dt.Columns.Add("Mobile", typeof(string));
        return dt;
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        divDueList.Visible = false;
        divPaymentHistory.Visible = false;
        Repeaterall.DataSource = null;
        Repeaterall.DataBind();
        rptHistory.DataSource = null;
        rptHistory.DataBind();

        if (rdList.SelectedValue == "1")
        {
            if (ddlPaymentType.SelectedIndex != 0)
            {
                LoadStudentPaymentMonthWise();
            }
            else
            {
                MessageController.Show("Please select Payment Type first!!!", MessageType.Warning, Page);
            }
        }
        else
        {
            LoadStudentPayment();
        }
        
    }
    private void LoadStudentPayment()
    {
        
        decimal amount = 0;
        decimal totalAmount = 0;
        decimal totalDue = 0;
        decimal totalReceived = 0;

        int paymentTypeId = 0;

        dtTotalDue = TotalDue();
        string criteria = GetCriterias();
        if (criteria != "")
        {

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
                string mobile = dtStudent.Rows[i]["Mobile"].ToString();

                DataTable dtAcademicPayment = new dalPaymentType().GetPaymentWithoutDefault(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), mediumId, campusId, classId, groupId, shiftId, sectionId, false, false);

                if (dtAcademicPayment.Rows.Count > 0)
                {
                    int monthId = 0;
                    MonthlyReceived = 0;
                    int year = Convert.ToInt32(ddlYear.SelectedItem.Text);
                    int StartMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["StartMonth"]);
                    int EndMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["EndMonth"]);
                    if (StartMonth < EndMonth)
                    {
                        for (int j = StartMonth; EndMonth + 1 > j; j++)
                        {
                            monthId = j;
                            dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId);
                        }
                    }
                    else
                    {
                        for (int j = StartMonth; 13 > j; j++)
                        {
                            monthId = j;
                            dueMonth += LoadMonthRpt(StudentToClassId, year, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId);
                        }
                        for (int j = 1; EndMonth + 1 > j; j++)
                        {
                            monthId = j;
                            int nextYear = year + 1;
                            dueMonth += LoadMonthRpt(StudentToClassId, nextYear, mediumId, campusId, classId, groupId, shiftId, sectionId, monthId);
                        }
                    }
                    total += dueMonth;
                    received += MonthlyReceived;
                    foreach (DataRow row in dtAcademicPayment.Rows)
                    {

                        amount = Convert.ToDecimal(row["Amount"]);
                        total += amount;
                        paymentTypeId = Convert.ToInt32(row["PaymentTypeId"]);
                        DataTable dtPaid = new dalPayment().GetOthersPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), classId, paymentTypeId);

                        if (dtPaid.Rows.Count > 0)
                        {
                            foreach (DataRow rows in dtPaid.Rows)
                            {
                                dueAmount = Convert.ToDecimal(rows["DueAmount"]);
                                received += Convert.ToDecimal(rows["TotalGiven"]);
                            }
                        }
                    }
                    totalAmount += total;

                    dtTotalDue.Rows.Add(StudentToClassId, name, regNo, className, ddlYear.SelectedItem.Text, total, received, total - received, mobile);
                    totalDue += (total - received);
                    totalReceived += received;

                }
            }

            if (dtTotalDue.Rows.Count > 0)
            {
                divPaymentHistory.Visible = false;
                divDueList.Visible = true;
                rptHistory.DataSource = dtTotalDue;
                rptHistory.DataBind();
                Label lblTotal = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblTotal");
                Label lblReceived = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblReceived");
                Label lblDue = (Label)rptHistory.Controls[rptHistory.Controls.Count - 1].FindControl("lblDue");
                lblTotal.Text = totalAmount.ToString();
                lblReceived.Text = totalReceived.ToString();
                lblDue.Text = totalDue.ToString();
            }
            else
            {

                divDueList.Visible = false;
                rptHistory.DataSource = null;
                rptHistory.DataBind();
                MessageController.Show("No Data Found!!!", MessageType.Warning, Page);
            }
        }
        else
        {
            MessageController.Show("This class Type not allow for this option!!!", MessageType.Warning, Page);
        }

    }
    private decimal LoadMonthRpt(int StudentToClassId, int year, int mediumId, int campusId, int classId, int groupId, int shiftId, int sectionId, int monthId)
    {
        string month = "";
        int paymentTypeId = 0;
        decimal amount = 0;
        decimal dueMonth = 0;
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        DataTable dtMonthly = new dalPaymentType().GetPaymentTypeByIsMonthly(true);

        foreach (DataRow row in dtMonthly.Rows)
        {
            paymentTypeId = Convert.ToInt32(row["Id"]);
            DataTable dtMonth = new dalPayment().GetMonthById(monthId);
            DataTable dtMonthlyFee = new dalPayment().GetMonthlyFees(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), mediumId, campusId, classId, groupId, shiftId, sectionId, monthId, paymentTypeId);

            if (dtMonthlyFee.Rows.Count > 0)
            {
                month = dtMonth.Rows[0]["Month"].ToString();
                amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
                paymentTypeId = Convert.ToInt32(dtMonthlyFee.Rows[0]["PaymentTypeId"]);
                if (amount != 0)
                {
                    DataTable dtPaid = new dalPayment().GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), classId, paymentTypeId, monthId);
                    if (dtPaid.Rows.Count > 0)
                    {
                        decimal dueAmount = 0;
                        decimal totalReceive = 0;
                        foreach (DataRow rows in dtPaid.Rows)
                        {
                            if (currentMonth >= Convert.ToInt32(monthId))
                                dueAmount = Convert.ToDecimal(rows["Amount"]);
                            totalReceive += Convert.ToDecimal(rows["TotalGiven"]);

                        }
                        MonthlyReceived += totalReceive;

                    }
                    if (currentMonth >= monthId && year == currentYear)
                    {
                       
                    }
                    dueMonth += amount;
                }
            }
        }
        return dueMonth;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rptHistory.Items.Count > 0)
        {
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriterias()) + "&report=classWiseStudentDueList");
        }

        else
        {
            MessageController.Show("Please select payment type first!!!", MessageType.Warning, Page);
        }
    }
    protected void btnMonthReport_Click(object sender, EventArgs e)
    { MessageController.Clear(this);
        if (ddlPaymentType.SelectedIndex!=0)
        {
           
            Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriterias()) + "&report=DueAllStudentPaymentMonthly&Type=" + ddlPaymentType.SelectedValue+"&SelectId="+ddlClass.SelectedItem.Text);
        }

        else
        {
            MessageController.Show("Please select payment type first!!!", MessageType.Warning, Page);
        }
    }
    protected void btnAllReport_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (ddlPaymentType.SelectedIndex != 0)
        {
            DataTable dtStudent = new dalStudent().GetByCriteria(GetCriterias());
            if (dtStudent.Rows.Count > 0)
            {
                if (rdList.SelectedValue == "1")
                {
                    Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriterias()) + "&report=DueAllStudentPaymentMonthly&Type=" + ddlPaymentType.SelectedValue + "&SelectId=" + ddlClass.SelectedItem.Text);
                }
                else
                {
                    Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriterias()) + "&report=classWiseStudentDueList");
                }
            }
            else
            {
                MessageController.Show("No data Found!!!", MessageType.Warning, Page);
            }
        }

        else
        {
            MessageController.Show("Please select payment type first!!!", MessageType.Warning, Page);
        }
    }
    private void LoadStudentPaymentMonthWise()
    {

        decimal amount = 0;
        decimal totalAmount = 0;
        decimal totalDue = 0;
        decimal totalReceived = 0;

        int paymentTypeId = Convert.ToInt32(ddlPaymentType.SelectedValue);

        DataTable dtPaymentType = new dalPaymentType().GetById(paymentTypeId);
        bool isMonthly = Convert.ToBoolean(dtPaymentType.Rows[0]["IsMonthly"]);

        dtTotalDue = DtPayment();
        string criteria = GetCriterias();


        DataTable dtStudent = new dalStudent().GetByCriteria(criteria);

        if (dtStudent.Rows.Count > 0)
        {
            decimal total = 0;
            decimal received = 0;
            decimal dueAmount = 0;
            decimal dueMonth = 0;
            StudentToClassId = Convert.ToInt32(dtStudent.Rows[0]["StudentToClassId"]);
            int classId = Convert.ToInt32(dtStudent.Rows[0]["ClassId"]);
            int mediumId = Convert.ToInt32(dtStudent.Rows[0]["MediumId"]);
            int campusId = Convert.ToInt32(dtStudent.Rows[0]["CampusId"]);
            int groupId = Convert.ToInt32(dtStudent.Rows[0]["GroupId"]);
            int shiftId = Convert.ToInt32(dtStudent.Rows[0]["ShiftId"]);
            int sectionId = Convert.ToInt32(dtStudent.Rows[0]["SectionId"]);

            DataTable dtAcademicPayment = new dalPaymentType().GetPaymentWithoutDefault(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), mediumId, campusId, classId, groupId, shiftId, sectionId, false, false);
            if (dtAcademicPayment.Rows.Count > 0)
            {

                int monthId = 0;
                MonthlyReceived = 0;
                int year = Convert.ToInt32(ddlYear.SelectedItem.Text);
                int StartMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["StartMonth"]);
                int EndMonth = Convert.ToInt32(dtAcademicPayment.Rows[0]["EndMonth"]);

                if (isMonthly)
                {
                    if (StartMonth < EndMonth)
                    {
                        for (int j = StartMonth; EndMonth + 1 > j; j++)
                        {
                            monthId = j;
                            dueMonth += LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue);
                        }
                    }
                    else
                    {
                        for (int j = StartMonth; 13 > j; j++)
                        {
                            monthId = j;
                            dueMonth += LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue);
                        }
                        for (int j = 1; EndMonth + 1 > j; j++)
                        {
                            monthId = j;
                            year = Convert.ToInt32(ddlYear.SelectedItem.Text) + 1;
                            dueMonth += LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue);
                        }
                    }
                }
                else
                {
                    dueMonth = LoadAllStudentMonthlyPayment(dtStudent, monthId, paymentTypeId, year, dtTotalDue);
                }

                total += dueMonth;
                received += MonthlyReceived;

                totalAmount += total;
                totalDue += (total - received);
                totalReceived += received;

                //dtTotalDue.Rows.Add(StudentToClassId, ddlYear.SelectedItem.Text,paymentType, total, received, total - received);


            }
        }



        if (dtTotalDue.Rows.Count > 0)
        {


            Repeaterall.DataSource = dtTotalDue;
            Repeaterall.DataBind();
            Label lblTotal = (Label)Repeaterall.Controls[Repeaterall.Controls.Count - 1].FindControl("lblTotal");
            Label lblReceived = (Label)Repeaterall.Controls[Repeaterall.Controls.Count - 1].FindControl("lblReceived");
            Label lblDue = (Label)Repeaterall.Controls[Repeaterall.Controls.Count - 1].FindControl("lblDue");

            lblTotal.Text = totalAmount.ToString();
            lblReceived.Text = totalReceived.ToString();
            lblDue.Text = totalDue.ToString();
            divPaymentHistory.Visible = true;
            divDueList.Visible = false;
        }
        else
        {

            divPaymentHistory.Visible = false;
            Repeaterall.DataSource = null;
            Repeaterall.DataBind();
            MessageController.Show("No Data Found!!!", MessageType.Warning, Page);
        }

    }

    private decimal LoadAllStudentMonthlyPayment(DataTable dtStudent, int monthId, int paymentTypeId, int year, DataTable dt)
    {
        string month = "";
        string className = "";
        string campusName = "";
        string mediumName = "";
        string paymentType = "";
        DataTable dtPaid =new DataTable();
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
                    if (monthId!=0)
                    {
                        // Monthly Paid Payment
                       
                        dtPaid= new dalPayment().GetMonthlyPayment(studentToClassId, Convert.ToInt32(ddlYear.SelectedValue), classId, paymentTypeId, monthId);
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


