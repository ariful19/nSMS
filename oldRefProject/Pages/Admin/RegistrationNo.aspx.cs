using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_RegistrationNo : System.Web.UI.Page
{
    dalCommon obj = new dalCommon();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int rollId = 0;
        int mediumId = 0;
        int yearFormat = 0;
        int styleId = 0;
        bool isRegNoUse = false;
        bool isRollNoUse = false;
        DataTable dt = new Common().GetAll("bs_Medium");
        DataTable dtRoll = new Common().GetAll("bs_UseRoll");
        if (dtRoll.Rows.Count > 0)
        {
            rollId = Convert.ToInt32(dtRoll.Rows[0]["Id"]);
        }

        if (rdlReg.SelectedValue == "1")
        {
            isRegNoUse = true;
        }
        if (chkRoll.Checked == true)
        {
            isRollNoUse = true;
        }
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
            mediumId=3;
        }
        if (chkYear.Checked)
        {
            if(rdlSelectYear.SelectedValue=="1")
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
        
        
        ID = obj.InsertPrefix(mediumId,tbxPrefix.Text,yearFormat,styleId,isRegNoUse);
        obj.InsertUseRollNo(rollId, isRollNoUse);
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
    protected void BindData()
    {
        DataTable dt = obj.GetMedium();
        dt.Columns.Add("Year");
        foreach(DataRow row in dt.Rows)
        {
            //row["Year"] = "Null";
            if (row["YearStyle"].ToString() == "1")
                row["Year"] = "Short";
            if (row["YearStyle"].ToString() == "2")
                row["Year"] = "Long";
        }
        
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int styleId  = 0;
        ID = Convert.ToInt32(e.CommandArgument);        
        DataTable dt = obj.GetMediumById(ID);
        if (!string.IsNullOrEmpty(dt.Rows[0]["StyleId"].ToString()))
        {
            styleId = Convert.ToInt32(dt.Rows[0]["StyleId"]);
        }
      
        ClearAll();
        if (dt.Rows.Count > 0)
        {

            rdlMedium.SelectedValue = dt.Rows[0]["Id"].ToString();
            rdlMedium.Enabled = false;
            tbxPrefix.Text = dt.Rows[0]["Prefix"].ToString();
            if (dt.Rows[0]["YearStyle"].ToString() != "")
            {
                chkYear.Checked = true;
                rdlSelectYear.SelectedValue = dt.Rows[0]["YearStyle"].ToString();
                pnlYear.Visible = true;
            }
            if (styleId >= 1 && styleId <= 2)
            {
                //rdlStyleMedium.Visible = true;
                //rdlStyelLong.Visible = false;
                //rdlStyleYearShort.Visible = false;
                rdlStyleMedium.SelectedValue = styleId.ToString();
            }
            else if (styleId >= 3 && styleId <= 8)
            {
                //rdlStyleMedium.Visible = false;
                //rdlStyelLong.Visible = false;
                //rdlStyleYearShort.Visible = true;
                rdlStyleYearShort.SelectedValue = styleId.ToString();
            }
            else if (styleId >= 9 && styleId <= 14)
            {
                //rdlStyleMedium.Visible = false;
                //rdlStyelLong.Visible = true;
                //rdlStyleYearShort.Visible = false;
                rdlStyelLong.SelectedValue = styleId.ToString();
            }
            else
            {
                rdlStyleMedium.Visible = true;
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
    }
    protected void ClearAll()
    {
        tbxPrefix.Text = "";
        chkYear.Checked = false;
        //pnlYear.Visible = false;
    }
}