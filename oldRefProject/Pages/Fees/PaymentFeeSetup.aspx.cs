using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Fees_PaymentFeeSetup : System.Web.UI.Page
{
    dalPaymentType obj = new dalPaymentType();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
  Common.CanAccess(this);
            LoadData();
                 
        }
    }


    #region LoadData
    protected void LoadData()
    {
        //ddlYear.DataSource = new dalYear().GetDefault();
        //ddlYear.DataBind();

        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();

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

        ddlStartMonth.DataSource = new Common().GetAll("bs_Month");
        ddlStartMonth.DataBind();
        ddlEndMonth.DataSource = new Common().GetAll("bs_Month");
        ddlEndMonth.DataBind();

        ddlStartMonth.Items.Insert(0, "---Select---");        
        ddlEndMonth.Items.Insert(0, "---Select---");
        //ddlEndMonth.Enabled = false;
        //ddlStartMonth.Enabled = false;
        //if (ddlCourse.SelectedIndex != 0)
        //    LoadSubjectTerm(Convert.ToInt32(ddlCourse.SelectedValue));
        //LoadSessionInfo();
    }
    //private void LoadSessionInfo()
    //{
    //    try
    //    {
    //        if (Common.SessionInfo.TeacherId > 0)
    //        {
    //            //ddlCampus.SelectedValue = Common.SessionInfo.CampusId.ToString();
    //            ddlCourse.SelectedValue = Common.SessionInfo.CourseId.ToString();
    //            LoadSubjectTerm(Convert.ToInt32(ddlCourse.SelectedValue));
    //            ddlSubject.SelectedValue = Common.SessionInfo.SubjectId.ToString();

    //            // ddlCampus.Enabled = false;
    //            ddlCourse.Enabled = false;
    //            ddlSubject.Enabled = false;
    //        }
    //    }
    //    catch
    //    {
    //        Response.Redirect("~/Login.aspx");
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
    protected DataTable Payment()
    {
        DataTable dt = new DataTable("dtPayment");
        dt.Columns.Add("UpdatedID", typeof(int));
        dt.Columns.Add("PayID", typeof(int));
        dt.Columns.Add("YearID", typeof (int));
        dt.Columns.Add("MediumID", typeof(int));
        dt.Columns.Add("CampusID", typeof (int));
        dt.Columns.Add("ClassID", typeof (int));
        dt.Columns.Add("GroupID", typeof (int));
        dt.Columns.Add("ShiftID", typeof(int));
        dt.Columns.Add("SectionID", typeof (int));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("StrMonth", typeof(int));
        dt.Columns.Add("EndMonth", typeof(int));       
        return dt;
    }
    #endregion
    #region ddl
    //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    MessageController.Clear(this);
    //    this.ddlSubject.Enabled = true;
    //    ddlEndMonth.Enabled = false;
    //    ddlStartMonth.Enabled = false;
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
    //            LoadPaymentByClass();
    //            ddlEndMonth.Enabled = true;
    //            ddlStartMonth.Enabled = true;
    //        }

    //    }
    //    else
    //    {
    //        MessageController.Show("Please select Term first....", MessageType.Warning, Page);
    //        ddlSubject.SelectedIndex = 0;
    //        ddlEndMonth.Enabled = false;
    //        ddlStartMonth.Enabled = false;
    //    }

    //}

    #endregion
    #region Function
    protected void LoadPaymentByClass()
    {

        DataTable dt = obj.GetPayment(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue),Convert.ToInt32(ddlCampus.SelectedValue),Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

        if (dt.Rows.Count > 0)
        {
            rptPaymentType.DataSource = null;
            rptPaymentType.DataSource = dt;
            rptPaymentType.DataBind();            
            ddlStartMonth.SelectedValue = string.IsNullOrEmpty(dt.Rows[0]["Startmonth"].ToString()) ? "---Select---" : dt.Rows[0]["Startmonth"].ToString();
            ddlEndMonth.SelectedValue = string.IsNullOrEmpty(dt.Rows[0]["Endmonth"].ToString()) ? "---Select---" : dt.Rows[0]["Endmonth"].ToString();
       }
       else
        {
            rptPaymentType.DataSource = null;
            rptPaymentType.DataBind();
        }
    }
    protected bool GetDefault()
    {
        bool flag;
        DataTable dt = obj.GetDefault();
        if (dt.Rows.Count > 0)
            flag = true;
        else
            flag = false;
        return flag;
    }
    #endregion
    
    #region Button

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadPaymentByClass();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkDefault.Checked && GetDefault() == true)
        {
            MessageController.Show("A Payment Type already set as default. Only one Type can be set as default Payment Type. Please unchecked the box.", MessageType.Error, Page);
            return;
        }

        ID = obj.Insert(tbxName.Text, chkMonthly.Checked, chkDefault.Checked, Page.User.Identity.Name);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This payment type name already exists. Please try anothor.", MessageType.Error, Page);
        tbxName.Text = "";
        LoadPaymentByClass();
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        DataTable dt = Payment();
        if (ddlStartMonth.SelectedIndex == 0 && ddlEndMonth.SelectedIndex == 0)
        {
            MessageController.Show("Please select Month correctly.", MessageType.Error, Page);
            return;
        }

        foreach (RepeaterItem item in rptPaymentType.Items)
        {
            string amount = "0";
            TextBox tbxAmmount = (TextBox)item.FindControl("tbxPaymentType");
            TextBox txtPaymentToClassID = (TextBox)item.FindControl("txtPaymentToClassID");
            TextBox tbxIsDefault = (TextBox)item.FindControl("tbxIsDefault");
            HiddenField hdnId = (HiddenField)item.FindControl("hdnId");


            if (tbxAmmount.Text != "" )
            {
                amount = tbxAmmount.Text;
            }
            dt.Rows.Add(int.Parse(txtPaymentToClassID.Text), Convert.ToInt32(hdnId.Value),Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue),Convert.ToInt32(ddlSection.SelectedValue), amount, Convert.ToInt32(ddlStartMonth.SelectedValue), Convert.ToInt32(ddlEndMonth.SelectedValue));
        }


        if (dt.Rows.Count > 0)
        {           
            obj.PaymentFeeInsert(dt, Page.User.Identity.Name, DateTime.Now);
            LoadPaymentByClass();
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
        {
            MessageController.Show("No data available for entry", MessageType.Information, Page);
        }
    }
    #endregion
}

