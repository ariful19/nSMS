using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Fees_PaymentStudentEdit : System.Web.UI.Page
{
    dalPaymentType obj = new dalPaymentType();
    dalPayment objPayment = new dalPayment();
    dalStudent objStudent = new dalStudent();
    List<StudentPayment> lstStudentPayment = new List<StudentPayment>();
    List<StudentPayment> lstStudentPayments = new List<StudentPayment>();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.CanAccess(this);
            LoadData();          
            pnlNew.Visible = false;        
            btnEdit.Visible = false;
            btnUpdate.Visible = false;
            lblRemarks.Visible = false;
            tbxRemarks.Visible = false;
            SelectCurrentYearMonth();
            CheckRollNoUse();
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
// criteria += " and ss_Student.StatusId= 1";
        return criteria;
    }

    #endregion

    #region Load Data
    protected void LoadData()
    {
        //ddlYear.DataSource = new dalYear().GetDefault();
        //ddlYear.DataBind();
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

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();
        

    }
    //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    MessageController.Clear(this);
    //    this.ddlSubject.Enabled = true;
      

    //    if (ddlCourse.SelectedIndex != 0)
    //        LoadSubjectTerm(Convert.ToInt32(ddlCourse.SelectedValue));
    //    else
    //        this.ddlSubject.Enabled = false;
    //}

    //protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    MessageController.Clear(this);
    //    if (ddlTerm.SelectedIndex != 0)
    //    {
    //        if (ddlSubject.SelectedIndex != 0)
    //        {
    //            btnSearch.Visible = true;
               
    //        }
    //        else
    //        {
    //            btnSearch.Visible = false;
               
    //        }
    //    }
    //    else
    //    {
    //        MessageController.Show("Please select Term first....", MessageType.Warning, Page);
    //        ddlSubject.SelectedIndex = 0;
    //        btnSearch.Visible = false;
            
    //    }

    //}

    //private void LoadSubjectTerm(int id)
    //{
    //    ddlSubject.DataSource = new dalSubjectName().GetSubjectByCourseId(id);
    //    ddlSubject.DataBind();
    //    ddlTerm.DataSource = new dalTerm().GetTermByCourseId(id);
    //    ddlTerm.DataBind();
    //    ddlSubject.Items.Insert(0, "---Select---");
    //    ddlSubject.SelectedIndex = 0;
    //    ddlTerm.Items.Insert(0, "---Select---");
    //    ddlTerm.SelectedIndex = 0;
    //}
    #endregion  

    #region Variable

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
    string Flag
    {
        set { ViewState["Flag"] = value; }
        get
        {
            try
            {
                return ViewState["Flag"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
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
    int UpdateId
    {
        set { ViewState["UpdateId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["UpdateId"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    int PaymentToClassId
    {
        set { ViewState["PaymentToClassId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["PaymentToClassId"]);
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
    DataTable dtMonthYear
    {
        set { ViewState["dtMonth"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtMonth"];
            }
            catch
            {
                return null;
            }
        }
    }
    DataTable dtMonthYear2
    {
        set { ViewState["dtMonth2"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtMonth2"];
            }
            catch
            {
                return null;
            }
        }
    }
    public DataTable MonthYear()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("MonthYear", typeof(string));
        dt.Columns.Add("MonthId", typeof(int));
        dt.Columns.Add("PaymentToClassID", typeof(int));
        dt.Columns.Add("PaymentTypeId", typeof(int));
        dt.Columns.Add("UpdateId", typeof(int));
        dt.Columns.Add("Amount", typeof(decimal));
        
        return dt;
    }
    public DataTable MonthYear2()
    {
        DataTable dt = new DataTable();     
        dt.Columns.Add("MonthYear", typeof(string)); 
        return dt;
    }
    protected DataTable Payment()
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

    #endregion

    #region Function
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
    private bool CheckValidation()
    {
        bool result = true;
        if (tbxRoll.Visible)
        {
            if (tbxRoll.Text == "")
            {
                tbxRoll.BorderColor = System.Drawing.Color.Red;
                MessageController.Show(" Enter Roll No First....", MessageType.Warning, Page);
                result = false;
            }
        }
        else
        {
            if (tbxReg.Text == "")
            {
                tbxReg.BorderColor = System.Drawing.Color.Red;
                MessageController.Show(" Enter Student ID First....", MessageType.Warning, Page);
                result = false;
            }
        }
        return result;
    }
    private void Clear()
    {
        tbxReg.Text = "";
        tbxRoll.Text = "";
        rptStudent.DataSource = null;
        rptStudent.DataBind();
        rptHistory.DataSource = null;
        rptHistory.DataBind();
        pnlNew.Visible = false;
        btnEdit.Visible = false;
    }  
    private void SelectCurrentYearMonth()
    {
       // ddlMonth.SelectedIndex = DateTime.Now.Month - 1;
        //ddlYear.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
    }  
    private void LoadStudent()
    {
        StudentToClassId = 0;
        string criteria = GetCriteria();
        DataTable dt = objStudent.GetByCriteria(criteria);

        if (dt.Rows.Count > 0)
        {

            StudentToClassId = Convert.ToInt32(dt.Rows[0]["StudentToClassId"]);

            DataTable dts = objPayment.GetRemarksByStudent(StudentToClassId);

            if (dts.Rows.Count > 0)
            {
                tbxRemarks.Text = dts.Rows[0]["Remarks"].ToString();
            }
            else
                tbxRemarks.Text = "";

            rptStudent.DataSource = dt;
            rptStudent.DataBind();
            pnlNew.Visible = true;
            pnlEdit.Visible = true;
            btnEdit.Visible = true;
            lblRemarks.Visible = true;
            tbxRemarks.Visible = true;
        }
        else
        {
            rptStudent.DataSource = null;
            rptStudent.DataBind();
            rptHistory.DataSource = null;
            rptHistory.DataBind();
            pnlNew.Visible = false;
            pnlEdit.Visible = false;
            btnEdit.Visible = false;
            lblRemarks.Visible = false;
            tbxRemarks.Visible = false;
            MessageController.Show("No Student Found!!!", MessageType.Error, Page);
        }
    }  
    protected void LoadPaymentType()
    {
        DataTable dt = new Common().GetAll("fee_PaymentType");
        dt.Columns.Add("Amount");
        rptPaymentType.DataSource = dt;
        rptPaymentType.DataBind();
    }
    protected void LoadPaymentBySubject()
    {
        DataTable dt = obj.GetPaymentWithoutDefault(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), false,false);
       
        if (dt.Rows.Count > 0)
        {
            StartMonth = Convert.ToInt32(dt.Rows[0]["StartMonth"]);
            EndMonth = Convert.ToInt32(dt.Rows[0]["EndMonth"]);
            YearId = Convert.ToInt32(dt.Rows[0]["YearId"]);
            rptPaymentType.DataSource = null;
            rptPaymentType.DataSource = dt;
            rptPaymentType.DataBind();
            //btnPayment.Visible = false;
            btnEdit.Visible = true;
            LoadMonthYear();
        }
        else
        {
            rptPaymentType.DataSource = null;
            rptPaymentType.DataBind();
            btnEdit.Visible = false;
            pnlEdit.Visible = false;
            //btnPayment.Visible = true;
        }
    }
    private void LoadMonthYear()
    {
        int roll = 0;
        int index = 0;
        int paymentTypeId = 0;
        string paymentType = "";
        int year = new dalYear().GetNameId(YearId);
        List<int> lstmonthId = new List<int>();
        dtMonthYear = MonthYear();
        dtMonthYear2 = MonthYear2();
        DataTable dtIsMonthly = obj.GetPaymentTypeByIsDefault(true, true);
        foreach (DataRow row in dtIsMonthly.Rows)
        {
            int count = 0;
            paymentTypeId = Convert.ToInt32(row["Id"]);
            //paymentType = row["PaymentType"].ToString();
            //dtMonthYear2.Columns.Add(paymentType);
            if (StartMonth < EndMonth)
            {
                for (int i = StartMonth; EndMonth + 1 > i; i++)
                {
                    roll = i;
                   //if(index==0)
                   // dtMonthYear2.Rows.Add(roll+"-" + year);
                   // dtMonthYear2.Rows[count][paymentType] = LoadMonthRpt(roll, year, dtMonthYear, paymentTypeId).ToString();
                   // count++;
                    LoadMonthRpt(roll, year, dtMonthYear, paymentTypeId);
                }
            }
            else
            {
                for (int i = StartMonth; 13 > i; i++)
                {
                    roll = i;
                    LoadMonthRpt(roll, year, dtMonthYear, paymentTypeId);
                }
                year = year + 1;
                for (int i = 1; EndMonth + 1 > i; i++)
                {
                    roll = i;
                    LoadMonthRpt(roll, year, dtMonthYear, paymentTypeId);
                }
            }
            index++;
        }
       
        if (dtMonthYear.Rows.Count > 0)
        {
            pnlEdit.Visible = true;
            btnUpdate.Visible = true;
            rptPaymentMonth.DataSource = dtMonthYear;
            rptPaymentMonth.DataBind();
            //GridView2.DataSource = dtMonthYear2;
            //GridView2.DataBind();
        }
        else
        {
            rptPaymentMonth.DataSource = null;
            rptPaymentMonth.DataBind();
            btnUpdate.Visible = false;
            pnlEdit.Visible = true;
        }
    }
    private decimal LoadMonthRpt(int monthId, int year, DataTable dt, int paymentTypeId)
    {
        string month = "";
        string paymentType = "";
        int updateId = 0;        
        int paymentToClassId = 0;
        decimal amount = 0;
        DataTable dtMonth = objPayment.GetMonthById(monthId);
        DataTable dtMonthlyFee = objPayment.GetMonthlyFees(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), monthId, paymentTypeId);
        month = dtMonth.Rows[0]["Month"].ToString();
        amount = Convert.ToInt32(dtMonthlyFee.Rows[0]["Amount"]);
        paymentToClassId = Convert.ToInt32(dtMonthlyFee.Rows[0]["Id"]);
        paymentType = dtMonthlyFee.Rows[0]["PaymentType"].ToString();
        updateId = Convert.ToInt32(dtMonthlyFee.Rows[0]["StudentToClassId"]);
        dt.Rows.Add(paymentType,month + "-" + year, monthId, paymentToClassId, paymentTypeId, updateId, amount);
        return amount;
    }
    protected void LoadPaymentHistory()
    {
        DataTable dt = objPayment.GetPaymentHistoryByStudentSubjectId(StudentToClassId, Convert.ToInt32(ddlClass.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            rptHistory.DataSource = dt;
            rptHistory.DataBind();
        }
        else
        {
            rptHistory.DataSource = null;
            rptHistory.DataBind();
        }     
    }

    #endregion

    #region Button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (CheckValidation())
        {
            LoadStudent();
            LoadPaymentBySubject();
            LoadPaymentHistory();

        }
        else
        {
            rptStudent.DataSource = null;
            rptStudent.DataBind();
            rptHistory.DataSource = null;
            rptHistory.DataBind();
            rptPaymentMonth.DataSource = null;
            rptPaymentMonth.DataBind();
            rptPaymentType.DataSource = null;
            rptPaymentType.DataBind();
            pnlNew.Visible = false;
            pnlEdit.Visible = false;
            btnEdit.Visible = false;
        }

    }
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        DataTable dt = Payment();
        int studentToSubjectId = 0;
       
        DataTable dtPaid = new DataTable();
       
        foreach (RepeaterItem items in rptStudent.Items)
        {
            
            CheckBox chkRow = (CheckBox)items.FindControl("chkrow");
            HiddenField hdnStudentSId = (HiddenField)items.FindControl("hdnStudentSId");
            if (chkRow.Checked)
            {
                studentToSubjectId = Convert.ToInt32(hdnStudentSId.Value);
                foreach (RepeaterItem item in rptPaymentType.Items)
                {
                    string amount = "0";
                    decimal totalAmount = 0;
                    decimal totalReceive = 0;
                    TextBox tbxAmmount = (TextBox)item.FindControl("tbxPaymentAmount");
                    TextBox txtPaymentToClassID = (TextBox)item.FindControl("txtPaymentToClassID");
                    HiddenField hdnPaymentTypeId = (HiddenField)item.FindControl("hdnId");
                    HiddenField insertUpdateId = (HiddenField)item.FindControl("hdnInsertUpdate");
                    Label lblStartMonth = (Label)item.FindControl("lblStartMonth");
                    Label lblEndMonth = (Label)item.FindControl("lblEndMonth");

                    dtPaid = objPayment.GetOthersPayment(studentToSubjectId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(hdnPaymentTypeId.Value));
                    if (dtPaid.Rows.Count > 0)
                    {

                        foreach (DataRow rows in dtPaid.Rows)
                        {
                         
                            totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                        }
                    }
                    
                    if (tbxAmmount.Text != "")
                    {
                        amount = tbxAmmount.Text;
                    }
                    if (decimal.Parse(amount) >= totalReceive)
                    {
                        totalAmount = decimal.Parse(amount);

                        dt.Rows.Add(studentToSubjectId, int.Parse(txtPaymentToClassID.Text), Convert.ToInt32(hdnPaymentTypeId.Value), 0,  amount, Convert.ToInt32(insertUpdateId.Value), lblStartMonth.Text, lblEndMonth.Text,tbxRemarks.Text);                        

                        for (int i = 0; dtPaid.Rows.Count > i; i++)
                        {                                           
                            decimal recieved = Convert.ToDecimal(dtPaid.Rows[i]["TotalGiven"]);                           
                            StudentPayment studentPayment = new StudentPayment();
                            studentPayment.PaymentID = Convert.ToInt32(dtPaid.Rows[i]["Id"]);
                            studentPayment.Amount = totalAmount;                          
                            studentPayment.DueAmount = studentPayment.Amount - recieved;
                            studentPayment.UpdatedDate = DateTime.Now;
                            studentPayment.UpdatedBy = Page.User.Identity.Name;
                            studentPayment.Remarks = "Fee Edit.";
                            lstStudentPayment.Add(studentPayment);
                            totalAmount = studentPayment.DueAmount;                          
                        }
                    }
                    else
                    {
                        MessageController.Show("Payment History should be change 1st.", MessageType.Warning, Page);
                        return;
                    }
                }


                          
                
            }
        }             
        if (dt.Rows.Count > 0)
        {
           
            int result= obj.StudentPaymentInformationInsert(dt, Page.User.Identity.Name, DateTime.Now);
            if (result == 1)
            {                
                objPayment.UpdateStudentPayment(lstStudentPayment);
                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);

                // Test purpose

                DataTable dts = objPayment.UpdateRemarks(studentToSubjectId, tbxRemarks.Text);  
            }
            LoadStudent();
            LoadPaymentBySubject();
            LoadPaymentHistory();
        }
        else
        {
            MessageController.Show("No data available for entry", MessageType.Warning, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int studentToClassId = 0;
        DataTable dt = Payment(); 
        foreach (RepeaterItem items in rptStudent.Items)
        {
            CheckBox chkRow = (CheckBox)items.FindControl("chkrow");
            HiddenField hdnStudentSId = (HiddenField)items.FindControl("hdnStudentSId");
            if (chkRow.Checked)
            {
                studentToClassId = Convert.ToInt32(hdnStudentSId.Value);
                foreach (RepeaterItem item in rptPaymentMonth.Items)
                {               
                    TextBox tbxAmount = (TextBox)item.FindControl("tbxPaymentAmount");
                    TextBox txtPaymentToClassID = (TextBox)item.FindControl("txtPaymentToClassID");
                    Label lblPaymentTypeId = (Label)item.FindControl("lblPaymentTypeId");
                    Label lblUpdateId = (Label)item.FindControl("lblUpdateId");
                    HiddenField hdnMonthId = (HiddenField)item.FindControl("hdnMonthId");
                    CheckBox chkMonthlyFee = (CheckBox)item.FindControl("chkMonthlyFee");
                    TextBox tbxItem1 = (TextBox)item.FindControl("tbxItem1");
                    TextBox tbxItem2 = (TextBox)item.FindControl("tbxItem2");
                    TextBox tbxItem3 = (TextBox)item.FindControl("tbxItem3");
                    TextBox tbxItem4 = (TextBox)item.FindControl("tbxItem4");
                    TextBox tbxItem5 = (TextBox)item.FindControl("tbxItem5");
                    Label lblItem1 = (Label)item.FindControl("lblItem1");
                    Label lblItem2 = (Label)item.FindControl("lblItem2");
                    Label lblItem3 = (Label)item.FindControl("lblItem3");
                    Label lblItem4 = (Label)item.FindControl("lblItem4");
                    Label lblItem5 = (Label)item.FindControl("lblItem5");
                    Label lblUpdateId1 = (Label)item.FindControl("lblUpdateId1");
                    Label lblUpdateId2 = (Label)item.FindControl("lblUpdateId2");
                    Label lblUpdateId3 = (Label)item.FindControl("lblUpdateId3");
                    Label lblUpdateId4 = (Label)item.FindControl("lblUpdateId4");
                    Label lblUpdateId5 = (Label)item.FindControl("lblUpdateId5");
                    Label lblPaymentToClassId1 = (Label)item.FindControl("lblPaymentToClassId1");
                    Label lblPaymentToClassId2 = (Label)item.FindControl("lblPaymentToClassId2");
                    Label lblPaymentToClassId3 = (Label)item.FindControl("lblPaymentToClassId3");
                    Label lblPaymentToClassId4 = (Label)item.FindControl("lblPaymentToClassId4");
                    Label lblPaymentToClassId5 = (Label)item.FindControl("lblPaymentToClassId5");
                    if (chkMonthlyFee.Checked)
                    {
                      LoadPaymentFee(dt,studentToClassId, Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(lblPaymentTypeId.Text), tbxAmount.Text, Convert.ToInt32(lblUpdateId.Text), Convert.ToInt32(txtPaymentToClassID.Text));
                        if(tbxItem1.Visible)
                            LoadPaymentFee(dt, studentToClassId, Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(lblItem1.Text), tbxItem1.Text, Convert.ToInt32(lblUpdateId1.Text), Convert.ToInt32(lblPaymentToClassId1.Text));
                        if (tbxItem2.Visible)
                            LoadPaymentFee(dt, studentToClassId, Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(lblItem2.Text), tbxItem2.Text, Convert.ToInt32(lblUpdateId2.Text), Convert.ToInt32(lblPaymentToClassId2.Text));
                        if (tbxItem3.Visible)
                            LoadPaymentFee(dt, studentToClassId, Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(lblItem3.Text), tbxItem3.Text, Convert.ToInt32(lblUpdateId3.Text), Convert.ToInt32(lblPaymentToClassId3.Text));
                        if (tbxItem4.Visible)
                            LoadPaymentFee(dt, studentToClassId, Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(lblItem4.Text), tbxItem4.Text, Convert.ToInt32(lblUpdateId4.Text), Convert.ToInt32(lblPaymentToClassId4.Text));
                        if (tbxItem5.Visible)
                            LoadPaymentFee(dt, studentToClassId, Convert.ToInt32(hdnMonthId.Value), Convert.ToInt32(lblItem5.Text), tbxItem5.Text, Convert.ToInt32(lblUpdateId5.Text), Convert.ToInt32(lblPaymentToClassId5.Text));
                    }
                }
            }
        }
        if (dt.Rows.Count > 0)
        {
            int result = obj.StudentPaymentInformationInsert(dt, Page.User.Identity.Name, DateTime.Now);
            if (result == 1)
            {
                objPayment.UpdateStudentPayment(lstStudentPayment);
                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);

                // Test purpose

                DataTable dts = objPayment.UpdateRemarks(studentToClassId, tbxRemarks.Text);     
            }
            LoadStudent();
            LoadPaymentBySubject();
            LoadPaymentHistory();
        }
        else
        {
            MessageController.Show("No data available for entry", MessageType.Warning, Page);
        }
        
    }
    private void LoadPaymentFee(DataTable dt,int studentToClassId, int monthId, int paymentTypeId, string tbxAmount, int updateId, int PaymentToClassID)
    {
        string amount = "0";
        decimal totalAmount = 0;
        decimal totalReceive = 0;               

        DataTable dtPaid = new DataTable();

        dtPaid = objPayment.GetMonthlyPayment(studentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), paymentTypeId, monthId);
                           
                        if (dtPaid.Rows.Count > 0)
                        {

                            foreach (DataRow rows in dtPaid.Rows)
                            {

                                totalReceive += Convert.ToDecimal(rows["TotalGiven"]);
                            }
                        }

                        if (tbxAmount != "")
                        {
                            amount = tbxAmount;
                        }
                        if (decimal.Parse(amount) >= totalReceive)
                        {
                            totalAmount = decimal.Parse(amount);

                            dt.Rows.Add(studentToClassId, PaymentToClassID, paymentTypeId, monthId, amount, updateId, StartMonth, EndMonth);
                            for (int i = 0; dtPaid.Rows.Count > i; i++)
                            {

                                decimal recieved = Convert.ToDecimal(dtPaid.Rows[i]["TotalGiven"]);
                                StudentPayment studentPayment = new StudentPayment();
                                studentPayment.PaymentID = Convert.ToInt32(dtPaid.Rows[i]["Id"]);
                                studentPayment.Amount = totalAmount;
                                studentPayment.DueAmount = studentPayment.Amount - recieved;
                                studentPayment.UpdatedDate = DateTime.Now;
                                studentPayment.UpdatedBy = Page.User.Identity.Name;
                                studentPayment.Remarks = "Fee Edit.";
                                lstStudentPayment.Add(studentPayment);
                                totalAmount = studentPayment.DueAmount;

                            }
                        }
                        else
                        {
                            MessageController.Show("Payment History should be change 1st.", MessageType.Warning, Page);
                            return;
                        }
                           
    }
    #endregion

    #region DDL
    //protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadPaymentBySubject();
    //    LoadStudent();
    //    LoadPaymentHistory();
    //}
    //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadPaymentBySubject();
    //    LoadStudent();
    //    LoadPaymentHistory();
    //}
    protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

    #endregion

    protected void rptPaymentMonthly_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dtMonthly = obj.GetPaymentTypeByIsDefault(true, false);

        if (e.Item.ItemType == ListItemType.Header)
        {
            Label lblHeader1 = (Label)e.Item.FindControl("lblHeader1");
            Label lblHeader2 = (Label)e.Item.FindControl("lblHeader2");
            Label lblHeader3 = (Label)e.Item.FindControl("lblHeader3");
            Label lblHeader4 = (Label)e.Item.FindControl("lblHeader4");
            Label lblHeader5 = (Label)e.Item.FindControl("lblHeader5");

            if (dtMonthly.Rows.Count > 0)
            {
                lblHeader1.Visible = true;
                lblHeader1.Text = dtMonthly.Rows[0]["PaymentType"].ToString();
                if (dtMonthly.Rows.Count > 1)
                {
                    lblHeader2.Visible = true;
                    lblHeader2.Text = dtMonthly.Rows[1]["PaymentType"].ToString();
                    if (dtMonthly.Rows.Count > 2)
                    {
                        lblHeader3.Visible = true;
                        lblHeader3.Text = dtMonthly.Rows[2]["PaymentType"].ToString();
                        if (dtMonthly.Rows.Count > 3)
                        {
                            lblHeader4.Visible = true;
                            lblHeader4.Text = dtMonthly.Rows[3]["PaymentType"].ToString();
                            if (dtMonthly.Rows.Count > 4)
                            {
                                lblHeader5.Visible = true;
                                lblHeader5.Text = dtMonthly.Rows[4]["PaymentType"].ToString();
                            }
                            else
                            {
                                lblHeader5.Visible = false;
                            }
                        }
                        else
                        {
                            lblHeader4.Visible = false;
                            lblHeader5.Visible = false;
                        }
                    }
                    else
                    {
                        lblHeader3.Visible = false;
                        lblHeader4.Visible = false;
                        lblHeader5.Visible = false;
                    }
                }
                else
                {
                    lblHeader2.Visible = false;
                    lblHeader3.Visible = false;
                    lblHeader4.Visible = false;
                    lblHeader5.Visible = false;
                }
            }
            else
            {
                lblHeader1.Visible = false;
                lblHeader2.Visible = false;
                lblHeader3.Visible = false;
                lblHeader4.Visible = false;
                lblHeader5.Visible = false;
            }
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnMonthId = (HiddenField)e.Item.FindControl("hdnMonthId");

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
            Label lblUpdateId1 = (Label)e.Item.FindControl("lblUpdateId1");
            Label lblUpdateId2 = (Label)e.Item.FindControl("lblUpdateId2");
            Label lblUpdateId3 = (Label)e.Item.FindControl("lblUpdateId3");
            Label lblUpdateId4 = (Label)e.Item.FindControl("lblUpdateId4");
            Label lblUpdateId5 = (Label)e.Item.FindControl("lblUpdateId5");
            Label lblPaymentToClassId1 = (Label)e.Item.FindControl("lblPaymentToClassId1");
            Label lblPaymentToClassId2 = (Label)e.Item.FindControl("lblPaymentToClassId2");
            Label lblPaymentToClassId3 = (Label)e.Item.FindControl("lblPaymentToClassId3");
            Label lblPaymentToClassId4 = (Label)e.Item.FindControl("lblPaymentToClassId4");
            Label lblPaymentToClassId5 = (Label)e.Item.FindControl("lblPaymentToClassId5");
            if (dtMonthly.Rows.Count > 0)
            {
                tbxItem1.Visible = true;
                lblItem1.Text = dtMonthly.Rows[0]["Id"].ToString();
                tbxItem1.Text = LoadMonthlyOthersPayment(lblItem1.Text, hdnMonthId.Value).ToString();
                lblUpdateId1.Text = UpdateId.ToString();
                lblPaymentToClassId1.Text = PaymentToClassId.ToString();
                
                if (Flag != "N")
                    tbxItem1.ReadOnly = true;
                else
                    tbxItem1.ReadOnly = false;
                if (dtMonthly.Rows.Count > 1)
                {
                    tbxItem2.Visible = true;
                    lblItem2.Text = dtMonthly.Rows[1]["Id"].ToString();
                    tbxItem2.Text = LoadMonthlyOthersPayment(lblItem2.Text, hdnMonthId.Value).ToString();
                    lblUpdateId2.Text = UpdateId.ToString();
                    lblPaymentToClassId2.Text = PaymentToClassId.ToString();
                    if (dtMonthly.Rows.Count > 2)
                    {
                        tbxItem3.Visible = true;
                        lblItem3.Text = dtMonthly.Rows[2]["Id"].ToString();
                        tbxItem3.Text = LoadMonthlyOthersPayment(lblItem3.Text, hdnMonthId.Value).ToString();
                        lblUpdateId3.Text = UpdateId.ToString();
                        lblPaymentToClassId3.Text = PaymentToClassId.ToString();
                        if (dtMonthly.Rows.Count > 3)
                        {
                            tbxItem4.Visible = true;
                            lblItem4.Text = dtMonthly.Rows[3]["Id"].ToString();
                            tbxItem4.Text = LoadMonthlyOthersPayment(lblItem4.Text, hdnMonthId.Value).ToString();
                            lblUpdateId4.Text = UpdateId.ToString();
                            lblPaymentToClassId4.Text = PaymentToClassId.ToString();
                            if (dtMonthly.Rows.Count > 4)
                            {
                                tbxItem5.Visible = true;
                                lblItem5.Text = dtMonthly.Rows[4]["Id"].ToString();
                                tbxItem5.Text = LoadMonthlyOthersPayment(lblItem5.Text, hdnMonthId.Value).ToString();
                                lblUpdateId5.Text = UpdateId.ToString();
                                lblPaymentToClassId5.Text = PaymentToClassId.ToString();
                            }
                            else
                            {
                                tbxItem5.Visible = false; 
                            }
                        }
                        else
                        {
                            tbxItem4.Visible = false;
                            tbxItem5.Visible = false;
                        }
                    }
                    else
                    {
                        tbxItem3.Visible = false;
                        tbxItem4.Visible = false;
                        tbxItem5.Visible = false; 
                    }
                }
                else
                {
                    tbxItem2.Visible = false;
                    tbxItem3.Visible = false;
                    tbxItem4.Visible = false;
                    tbxItem5.Visible = false;
                }
            }
            else
            {
                tbxItem1.Visible = false;
                tbxItem2.Visible = false;
                tbxItem3.Visible = false;
                tbxItem4.Visible = false;
                tbxItem5.Visible = false; 
            }
        }
    }
    private decimal LoadMonthlyOthersPayment(string paymentTypeId, string monthId)
    {
        UpdateId = 0;
        decimal amount = 0;
        decimal given = 0;
       
        DataTable dtMonthlyFee = objPayment.GetMonthlyFees(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(monthId), Convert.ToInt32(paymentTypeId));
        if (dtMonthlyFee.Rows.Count > 0)
        {
            amount = Convert.ToDecimal(dtMonthlyFee.Rows[0]["Amount"]);
            UpdateId=Convert.ToInt32(dtMonthlyFee.Rows[0]["StudentToClassId"]);
            PaymentToClassId = Convert.ToInt32(dtMonthlyFee.Rows[0]["Id"]);
        }

        DataTable dtPaid = objPayment.GetMonthlyPayment(StudentToClassId, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(paymentTypeId), Convert.ToInt32(monthId));
        if (dtMonthlyFee.Rows.Count > 0)
        {
            foreach (DataRow row in dtPaid.Rows)
            {
                given += Convert.ToDecimal(row["TotalGiven"]);
                
            }
            
            if (amount == given && amount!=0)
            {
                Flag = "Y";
            }
            else
            {
                Flag = "N";
            }
                     

        }
        return amount;
    }
}


