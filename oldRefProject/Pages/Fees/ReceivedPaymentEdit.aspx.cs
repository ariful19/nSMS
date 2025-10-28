using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Fees_ReceivedPaymentEdit : System.Web.UI.Page
{
    dalPayment objPayment = new dalPayment();
    dalPaymentType objPType = new dalPaymentType();
    dalStudent objStudent = new dalStudent();
    List<StudentPayment> lstStudentPayment = new List<StudentPayment>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
  Common.CanAccess(this);
            divPaymentUpdate.Visible = false;
            btnPrint.Visible = false;
        }
    }

    int PaymentId
    {
        set { ViewState["PaymentId"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["PaymentId"]);
            }
            catch
            {
                return 0;
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
    public DataTable MonthYear()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Month", typeof(string));
        dt.Columns.Add("Id", typeof(int));
        return dt;
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
    private void Load()
    {
        ddlSession.DataSource = new Common().GetAll("bs_Year");
        ddlSession.DataBind();
        ddlMonth.DataSource = new Common().GetAll("bs_Month");
        ddlMonth.DataBind();
    }
    private void LoadMonth(DataTable dt)
    {
        int yearId = Convert.ToInt32(dt.Rows[0]["Year1"]);
        int classId = Convert.ToInt32(dt.Rows[0]["ClassId"]);
        int StudentToClassId = Convert.ToInt32(dt.Rows[0]["StudentToClassId"]);

        DataTable dtId = objPType.GetTermId(yearId, classId, StudentToClassId);
        int mediumId = Convert.ToInt32(dtId.Rows[0]["MediumId"]);
        int groupId = Convert.ToInt32(dtId.Rows[0]["GroupId"]);
        int shiftId = Convert.ToInt32(dtId.Rows[0]["ShiftId"]);
        int sectionId = Convert.ToInt32(dtId.Rows[0]["SectionId"]);
        int campusId = Convert.ToInt32(dtId.Rows[0]["CampusId"]);


        DataTable dtss = objPType.GetPaymentWithoutDefault(StudentToClassId, yearId, mediumId, campusId, classId, groupId, shiftId, sectionId, false, false);
        if (dtss.Rows.Count > 0)
        {
            StartMonth = Convert.ToInt32(dtss.Rows[0]["StartMonth"]);
            EndMonth = Convert.ToInt32(dtss.Rows[0]["EndMonth"]);
        }

        DataTable dts = objPType.GetPayment(yearId, mediumId, campusId, classId, groupId, shiftId, sectionId);

        ddlPaymentType.DataSource = dts;
        ddlPaymentType.DataBind();
        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();
        Load();
        LoadMonthYear();
    }
    private void LoadMonthYear()
    {

        int roll = 0;
        int year = DateTime.Now.Year;
        List<int> lstmonthId = new List<int>();
        dtMonthYear = MonthYear();

        if (StartMonth < EndMonth)
        {
            for (int i = StartMonth; EndMonth + 1 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthYear);
            }
        }
        else
        {
            for (int i = StartMonth; 13 > i; i++)
            {
                roll = i;
                LoadMonthRpt(roll, year, dtMonthYear);
            }
            for (int i = 1; EndMonth + 1 > i; i++)
            {
                roll = i;
                year = DateTime.Now.Year + 1;
                LoadMonthRpt(roll, year, dtMonthYear);
            }
        }

        if (dtMonthYear.Rows.Count > 0)
        {
            dtMonthYear.Rows.Add("Academic", 0);
            ddlMonth.DataSource = dtMonthYear;
            ddlMonth.DataBind();

        }
    }
    private void LoadMonthRpt(int monthId, int year, DataTable dt)
    {
        string month = "";

        DataTable dtMonth = objPayment.GetMonthById(monthId);

        month = dtMonth.Rows[0]["Month"].ToString();

        dt.Rows.Add(month + "-" + year, monthId);
    }
    private bool ValidationCheck()
    {
        if (tbxBillNo.Text == "")
        {
            MessageController.Show("Bill No Must be Needed!!!", MessageType.Warning, Page);
            return true;
        }
        return false;
    }
    private void LoadPaymentHistory()
    {

        string userName = User.Identity.Name;
        string role = new Common().GetRolesByUserName(userName);
        DataTable dtCampus = new Common().GetCampusByUserName(userName);
        DataTable dt = objPayment.GetPaymentHistoryByBillNo(tbxBillNo.Text);
        if( role =="Admin" || (Convert.ToInt32(dtCampus.Rows[0]["Id"]) == Convert.ToInt32 (dt.Rows[0]["CampusId"])))
        {
        divSearch.Visible = true;
        divPaymentUpdate.Visible = false;
        if (dt.Rows.Count > 0)
        {
            rptHistory.DataSource = dt;
            rptHistory.DataBind();
            LoadMonth(dt);
            btnPrint.Visible = true;
        }
        else
        {
            rptHistory.DataSource = null;
            rptHistory.DataBind();
            btnPrint.Visible = true;
            MessageController.Show("No record found!!!", MessageType.Warning, Page);
        }
        }
        else
        {
            MessageController.Show("You are not Authorised.", MessageType.Warning,Page);
        }
            
       
    }

    protected void btnHistory_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        BillNo = "";
        if (ValidationCheck())
        {
            return;
        }

        LoadPaymentHistory();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        StudentPayment studentPayment = new StudentPayment();
        decimal total = Convert.ToDecimal(tbxAmount.Text);
        decimal totalReceived = Convert.ToDecimal(tbxGivenAmount.Text);
        if (BillNo != tbxBillEdit.Text)
        {
            int billNoExist = objPayment.GetBillNoExist(tbxBillEdit.Text);
            if (billNoExist != 1)
            {
                MessageController.Show("Bill No already Exists. Please Select new Bill No....", MessageType.Error, Page);
                return;
            }
        }


        if (total >= totalReceived)
        {
            studentPayment.PaymentID = PaymentId;
            studentPayment.BillNo = tbxBillEdit.Text;
            studentPayment.Amount = total;
            studentPayment.TotalGiven = totalReceived;
            studentPayment.DueAmount = total - totalReceived;
            studentPayment.PaymentTypeId = Convert.ToInt32(ddlPaymentType.SelectedValue);
            studentPayment.ClassId = Convert.ToInt32(ddlClass.SelectedValue);
            studentPayment.Year = Convert.ToInt32(ddlSession.SelectedValue);
            studentPayment.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            studentPayment.MonthYear = ddlMonth.SelectedItem.Text;
            studentPayment.Date = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
            studentPayment.Remarks = tbxRemarks.Text;
            studentPayment.UpdatedBy = Page.User.Identity.Name;
            lstStudentPayment.Add(studentPayment);
            try
            {
                int result = objPayment.UpdateStudentPaymentAll(lstStudentPayment);

                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
                PaymentId = 0;

            }
            catch (Exception)
            {
                MessageController.Show(MessageCode.UpdateFailed, MessageType.Warning, Page);
            }
        }
        else
        {
            MessageController.Show("Given amount must be equal or smaller than Total amount.", MessageType.Error, Page);
        }
        LoadPaymentHistory();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        if (ValidationCheck())
        {
            return;
        }
        Response.Redirect("../../Report/Viewer/ReportView.aspx?report=StudentPayment&type=" + tbxBillNo.Text);

    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        divSearch.Visible = false;
        divPaymentUpdate.Visible = true;
        PaymentId = Convert.ToInt32(e.CommandArgument);
        DataTable dt = objPayment.GetPaymentHistoryById(PaymentId);
        if (dt.Rows.Count > 0)
        {
            BillNo = dt.Rows[0]["BillNo"].ToString();
            tbxBillEdit.Text = BillNo;
            tbxAmount.Text = dt.Rows[0]["Amount"].ToString();
            tbxGivenAmount.Text = dt.Rows[0]["TotalGiven"].ToString();
            ddlSession.SelectedValue = dt.Rows[0]["Year"].ToString();
            ddlMonth.SelectedValue = dt.Rows[0]["Month"].ToString();
            ddlClass.SelectedValue = dt.Rows[0]["ClassId"].ToString();
            ddlPaymentType.SelectedValue = dt.Rows[0]["PaymentTypeId"].ToString();

            tbxRemarks.Text = string.IsNullOrEmpty(dt.Rows[0]["Remarks"].ToString()) ? "" : dt.Rows[0]["Remarks"].ToString();
            tbxDate.Text = string.IsNullOrEmpty(dt.Rows[0]["Date"].ToString()) ? "" : (Convert.ToDateTime(dt.Rows[0]["Date"])).ToString("dd/MM/yyyy");
        }
        else
        {
            BillNo = "";
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        PaymentId = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("ss_Payment", PaymentId);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);

        divPaymentUpdate.Visible = false;
        rptHistory.DataSource = null;
        rptHistory.DataBind();
    }


}

