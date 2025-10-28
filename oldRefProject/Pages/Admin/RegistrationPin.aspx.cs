using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_RegistrationPin : System.Web.UI.Page
{
    dalCommon obj = new dalCommon();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
        
        if (!IsPostBack)
        {
            CheckRollNoUse();
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
    protected void CheckRollNoUse()
    {
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            IsRollNoUse = string.IsNullOrEmpty(dtRoll.Rows[0]["IsUseRollNo"].ToString()) ? "True" : dtRoll.Rows[0]["IsUseRollNo"].ToString();
        }
        if (IsRollNoUse == "False")
        {
            chkRollNo.Checked = true;
            chkRollYes.Checked = false;
            lblMessege.Text = "Roll No number is not used.";
            lblMessege.ForeColor = Color.Red;
        }
        else
        {
            chkRollYes.Checked = true;
            chkRollNo.Checked = false;
            lblMessege.Text = "Roll No number is used";
            lblMessege.ForeColor=Color.Green;
        }

    }   
    protected void BindData()
    {
        string isAutomatic="";
        DataTable dt = obj.GetMedium();
        DataTable dtPin = obj.GetTeacherPinFormat();

        dt.Columns.Add("Year");
        dtPin.Columns.Add("PinFormat");
       
        foreach (DataRow row in dt.Rows)
        {
            //row["Year"] = "Null";
            if (row["YearStyle"].ToString() == "1")
                row["Year"] = "Short";
            if (row["YearStyle"].ToString() == "2")
                row["Year"] = "Long";
        }
        if (dtPin.Rows.Count > 0)
        {
            isAutomatic = dtPin.Rows[0]["IsAutomatic"].ToString();
            if (isAutomatic == "True")
                dtPin.Rows[0]["PinFormat"] = "Automatic";
            else
                dtPin.Rows[0]["PinFormat"] = "Manual";
        }
        
        rptYear.DataSource = dt;
        rptYear.DataBind();
        rptTeacher.DataSource = dtPin;
        rptTeacher.DataBind();
    }  
  
    
    protected void ClearAll()
    {
        tbxPrefix.Text = "";
        //chkYearYes.Checked = false;
        //pnlYear.Visible = false;
    }

    protected void chkRollYes_CheckedChanged(Object sender, EventArgs args)
    {
        int rollId = 0;       
        bool isRollNoUse = false;

        if (chkRollYes.Checked)
            isRollNoUse = true;

        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            rollId = Convert.ToInt32(dtRoll.Rows[0]["Id"]);
        }     
        int id= obj.InsertUseRollNo(rollId, isRollNoUse);
        if (id != 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Change Successfully')", true);
        }
        CheckRollNoUse();
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        int mediumId = 0;
        int yearFormat = 0;
        int styleId = 0;
        bool isRegNoUse = false;
        string prefix = "";
        DataTable dt = new Common().GetAll("bs_Medium");
        DataTable dtPin = new Common().GetAll("bs_TeacherPin");

        if (rdlReg.SelectedValue == "1")
        {
            isRegNoUse = true;
            prefix = tbxPrefix.Text;
        }
        if (rdlSelect.SelectedValue == "1")
        {
            if (rdlMedium.SelectedValue == "1")
            {
                mediumId = 1;
            }
            else if (rdlMedium.SelectedValue == "2")
            {
                mediumId = 2;
            }
            else
            {
                mediumId = 3;
            }
        }
        else
        {
            if (dtPin.Rows.Count > 0)
                mediumId = Convert.ToInt32(dtPin.Rows[0]["Id"]);
        }
        if (chkYearYes.Checked)
        {
            if (rdlSelectYear.SelectedValue == "1")
            {
                yearFormat = 1;
                styleId = (!string.IsNullOrEmpty(rdlStyleYearShort.SelectedValue)) ? Convert.ToInt32(rdlStyleYearShort.SelectedValue) : 0;
            }
            else
            {
                yearFormat = 2;
                styleId = (!string.IsNullOrEmpty(rdlStyelLong.SelectedValue)) ? Convert.ToInt32(rdlStyelLong.SelectedValue) : 0;
            }

        }
        else
        {
            styleId = (!string.IsNullOrEmpty(rdlStyleMedium.SelectedValue)) ? Convert.ToInt32(rdlStyleMedium.SelectedValue) : 0;
        }

        if (rdlSelect.SelectedValue == "1")
            ID = obj.InsertPrefix(mediumId, prefix, yearFormat, styleId, isRegNoUse);
        else
            ID = obj.InsertTeacherPin(mediumId, prefix, yearFormat, styleId, isRegNoUse);
        if (ID != -1)
        {
            tbxPrefix.Text = "";
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Information, Page);
        BindData();
        ClearAll();


    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        MessageController.Clear(this);
        int styleId = 0;
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetMediumById(ID);
        if (!string.IsNullOrEmpty(dt.Rows[0]["StyleId"].ToString()))
        {
            styleId = Convert.ToInt32(dt.Rows[0]["StyleId"]);
        }

        ClearAll();
        rdlReg.Visible = true;
        if (dt.Rows.Count > 0)
        {
            rdlSelect.SelectedValue = "1";

            if (ID == 4)
                rdlSelect.SelectedValue = "2";

            rdlSelect.Enabled = false;
            rdlMedium.SelectedValue = dt.Rows[0]["Id"].ToString();
            rdlMedium.Enabled = false;
            tbxPrefix.Text = dt.Rows[0]["Prefix"].ToString();
            chkYearYes.Checked = false;
            chkYearNo.Checked = true;
            rdlReg.SelectedValue = "2";
            if (dt.Rows[0]["YearStyle"].ToString() != "0")
            {
                chkYearYes.Checked = true;
                chkYearNo.Checked = false;
                rdlSelectYear.SelectedValue = dt.Rows[0]["YearStyle"].ToString();
                divYear.Visible = true;
            }
            if (styleId >= 1 && styleId <= 2)
            {
                rdlStyleMedium.Visible = true;
                rdlStyelLong.Visible = false;
                rdlStyleYearShort.Visible = false;
                rdlStyleMedium.SelectedValue = styleId.ToString();
                rdlReg.SelectedValue = "1";
            }
            else if (styleId >= 3 && styleId <= 8)
            {
                rdlStyleMedium.Visible = false;
                rdlStyelLong.Visible = false;
                rdlStyleYearShort.Visible = true;
                rdlStyleYearShort.SelectedValue = styleId.ToString();
                rdlReg.SelectedValue = "1";
            }
            else if (styleId >= 9 && styleId <= 14)
            {
                rdlStyleMedium.Visible = false;
                rdlStyelLong.Visible = true;
                rdlStyleYearShort.Visible = false;
                rdlStyelLong.SelectedValue = styleId.ToString();
                rdlReg.SelectedValue = "1";
            }
            else
            {
                rdlSelectYear.Visible = true;
                rdlSelectYear.SelectedIndex = 0;
                rdlStyleMedium.Visible = true;
                rdlStyelLong.Visible = true;
                rdlStyleYearShort.Visible = true;
                rdlReg.SelectedValue = "2";
            }
            btnSave.Visible = true;
            btnReset.Visible = true;
        }
        //btnSave.Visible = false;
        //btnEdit.Visible = true;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
        Response.Redirect("~/Pages/Admin/RegistrationPin.aspx");
    }
}