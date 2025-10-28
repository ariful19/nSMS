using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Teacher_ExamRoutineView : System.Web.UI.Page
{
    dalSubject obj = new dalSubject();
    dalRoutine objRoutine = new dalRoutine();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MessageController.Clear(this);
            Load();
            LoadCriteria();
            SetInitialRow();
            LoadExistingRutine();
        }
    }
    #region Dynamic Row
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        DropDownList ddlSubject = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("ddlSubject");
        ddlSubject.Items.Clear();
        FillDropDownList(ddlSubject);
        LoadExistingRutine();
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        DropDownList ddlSubject = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("ddlSubject");
        ddlSubject.Items.Clear();
        FillDropDownList(ddlSubject);
        LoadExistingRutine();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadExistingRutine();
    }
    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        DataTable dt = obj.GetByClassAndGroupId(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        foreach (DataRow dr in dt.Rows)
        {
            arr.Add(new ListItem(dr["SubjectName"].ToString(), dr["ID"].ToString()));
        }
        return arr;
    }
    private void FillDropDownList(DropDownList ddl)
    {
        ArrayList arr = GetDummyData();
        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
    }
    protected DataTable GetRoutine()
    {
        DataTable dt = new DataTable("dtRoutine");
        dt.Columns.Add(new DataColumn("PeriodNo", typeof(int)));
        dt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
        dt.Columns.Add(new DataColumn("StartTime", typeof(string)));
        dt.Columns.Add(new DataColumn("EndTime", typeof(string)));
        dt.Columns.Add(new DataColumn("ExamDate", typeof(DateTime)));
        return dt;
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        
        dt.Columns.Add(new DataColumn("Id", typeof(int)));
        dt.Columns.Add(new DataColumn("PeriodNo", typeof(int)));
        dt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
        dt.Columns.Add(new DataColumn("StartTime", typeof(string)));
        dt.Columns.Add(new DataColumn("EndTime", typeof(string)));
        dt.Columns.Add(new DataColumn("ExamDate", typeof(DateTime)));
      
        dt.Rows.Add(dt.NewRow());
        Gridview1.DataSource = dt;
        Gridview1.DataBind();
        int columncount = Gridview1.Rows[0].Cells.Count;
        Gridview1.Rows[0].Cells.Clear();
        Gridview1.Rows[0].Cells.Add(new TableCell());
        Gridview1.Rows[0].Cells[0].ColumnSpan = columncount;
        Gridview1.Rows[0].Cells[0].Text = "No Records Found";
        btnUpdate.Visible = false;       
        
    }
    private void SetPreviousData()
    {

        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox2");

                    DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddlSubject");
                    TextBox tbxDate = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("tbxDate");

                    FillDropDownList(ddl1);

                    if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox   
                        box1.Text = dt.Rows[i]["StartTime"].ToString();
                        box2.Text = dt.Rows[i]["EndTime"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        ddl1.ClearSelection();
                        ddl1.SelectedValue = dt.Rows[i]["SubjectId"].ToString();
                        tbxDate.Text = Convert.ToDateTime(dt.Rows[i]["ExamDate"].ToString()).ToString("dd/MM/yyyy");
                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = new DataTable();
            DataRow drCurrentRow = null;

            HiddenField hdClassID = (HiddenField)gvRow.Cells[2].FindControl("hdnExamRoutineId");
            int Id = (string.IsNullOrEmpty(hdClassID.Value) ? 0 : Convert.ToInt32(hdClassID.Value));
            if (Id > 0)
            {
                int result = objRoutine.ExamRoutineDelete(Id);

                if (result > 0)
                    MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
                else
                    MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
                Gridview1.DataSource = dt;
                Gridview1.DataBind();

                for (int i = 0; i < Gridview1.Rows.Count - 1; i++)
                {
                    Gridview1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                //SetPreviousData();
                //Response.Redirect("~/Pages/Teacher/CreateExamRoutine.aspx");
            }
            else
            {
                DataTable dts = (DataTable)ViewState["CurrentTable"];

                if (dts.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dts.Rows.Count)
                    {
                        //Remove the Selected Row data and reset row number  
                        //Gridview1.DeleteRow(Gridview1.SelectedIndex);
                        dts.Rows.Remove(dts.Rows[rowID]);
                        ResetRowID(dts);
                    }
                }
                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable"] = dts;

                //Re bind the GridView for the updated data  
                Gridview1.DataSource = dts;
                Gridview1.DataBind();
                              

            }
        }

        //Set Previous Data on Postbacks  
        LoadExistingRutine();
    }

   

    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }
    }
    #endregion

    #region Load Data
    protected void Load()
    {
        ddlYear.DataSource = new dalYear().GetDefault();
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

        ddlExamType.DataSource = new Common().GetAll("bs_ExamType");
        ddlExamType.DataBind();
    }

    protected void LoadCriteria()
    {
        if (((DataTable)Session["Criteria"]) != null)
        {
            DataTable dtCriteria = (DataTable)Session["Criteria"];

            ddlYear.SelectedValue = dtCriteria.Rows[0]["Year"].ToString();
            ddlMedium.SelectedValue = dtCriteria.Rows[0]["MediumId"].ToString();
            ddlCampus.SelectedValue = dtCriteria.Rows[0]["CampusId"].ToString();
            ddlClass.SelectedValue = dtCriteria.Rows[0]["ClassId"].ToString();
            ddlGroup.SelectedValue = dtCriteria.Rows[0]["GroupId"].ToString();
            ddlExamType.SelectedValue = dtCriteria.Rows[0]["ExamTypeID"].ToString();
            ddlShift.SelectedValue = dtCriteria.Rows[0]["ShiftId"].ToString();

            if (Session["mgs"] != null)
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            //Clear the session variable
            Session["Criteria"] = null;
            Session["mgs"] = null;
            // Enable some control to display the message (control is likely on the master page)



        }
    }
    protected DataTable GetCriteria()
    {
        DataTable dt = new DataTable("dtCriteria");
        dt.Columns.Add(new DataColumn("Year", typeof(int)));
        dt.Columns.Add(new DataColumn("MediumId", typeof(int)));
        dt.Columns.Add(new DataColumn("CampusId", typeof(int)));
        dt.Columns.Add(new DataColumn("ClassId", typeof(int)));
        dt.Columns.Add(new DataColumn("ExamTypeID", typeof(int)));
        dt.Columns.Add(new DataColumn("GroupId", typeof(int)));      
        dt.Columns.Add(new DataColumn("ShiftId", typeof(int)));
        return dt;
    }
    private void LoadExistingRutine()
    {
        DataTable dtCriteria = GetCriteria();
        int year = (!string.IsNullOrEmpty(ddlYear.SelectedValue)) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
        int mediumID = (!string.IsNullOrEmpty(ddlMedium.SelectedValue)) ? Convert.ToInt32(ddlMedium.SelectedValue) : 0;
        int campusID = (!string.IsNullOrEmpty(ddlCampus.SelectedValue)) ? Convert.ToInt32(ddlCampus.SelectedValue) : 0;
        int classID = (!string.IsNullOrEmpty(ddlClass.SelectedItem.Text)) ? Convert.ToInt32(ddlClass.SelectedValue) : 0;
        int ExamTypeID = (!string.IsNullOrEmpty(ddlExamType.SelectedItem.Text)) ? Convert.ToInt32(ddlExamType.SelectedValue) : 0;
        int groupID = (!string.IsNullOrEmpty(ddlGroup.SelectedItem.Text)) ? Convert.ToInt32(ddlGroup.SelectedValue) : 0;
        int shifID = (!string.IsNullOrEmpty(ddlShift.SelectedItem.Text)) ? Convert.ToInt32(ddlShift.SelectedValue) : 0;
        dtCriteria.Rows.Add(year,mediumID,campusID, classID, ExamTypeID, groupID, shifID);
        ViewState["Criteria"] = dtCriteria;

        DataTable dt = new dalClass().GetExamRoutine(year,mediumID,campusID, classID, ExamTypeID, groupID, shifID);
        Gridview1.DataSource = dt;
        Gridview1.DataBind();

        int rowIndex = 0;
        ViewState["CurrentTable"] = dt;
        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                TextBox box1 = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
                TextBox box2 = (TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox2");
                TextBox box3 = (TextBox)Gridview1.Rows[i].Cells[4].FindControl("tbxDate");

                DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddlSubject");

                FillDropDownList(ddl1);

                if (i <= dt.Rows.Count - 1)
                {

                    //Assign the value from DataTable to the TextBox   
                    box1.Text = dt.Rows[i]["StartTime"].ToString();
                    box2.Text = dt.Rows[i]["EndTime"].ToString();

                    //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                    ddl1.ClearSelection();
                    ddl1.SelectedValue = dt.Rows[i]["SubjectId"].ToString();
                    box3.Text = Convert.ToDateTime(dt.Rows[i]["ExamDate"]).ToString("dd/MM/yyyy");
                    //ShowDeleteButton(Gridview1.Rows[rowIndex]);
                }

                rowIndex++;
            }
            ViewState["CurrentTable"] = dt;
            btnUpdate.Visible = true;
        }
        else
        {
            SetInitialRow();
        }


    }
    private void ShowDeleteButton(GridViewRow row)
    {
        LinkButton lb = (LinkButton)row.FindControl("LinkButton1");
        if (lb != null)
        {
            lb.Visible = true;
        }
    }
    #endregion

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        if (ValidExamRoutineData())
        {
            DataTable dt = GetRoutine();

            List<ExamRoutine> ExamRoutineList = new List<ExamRoutine>();

            foreach (GridViewRow gr in Gridview1.Rows)
            {
                HiddenField hdExamID = (HiddenField)gr.Cells[2].FindControl("hdnExamRoutineId");
                TextBox box1 = (TextBox)gr.Cells[2].FindControl("TextBox1");
                TextBox box2 = (TextBox)gr.Cells[3].FindControl("TextBox2");
                DropDownList ddl1 = (DropDownList)gr.Cells[1].FindControl("ddlSubject");
                TextBox tbxDate = (TextBox)gr.Cells[4].FindControl("tbxDate");
                DateTime examDate = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DropDownList ddlSubject = (DropDownList)gr.Cells[1].FindControl("ddlSubject");

                ExamRoutine aExamRoutine = new ExamRoutine();
                aExamRoutine.Id = (string.IsNullOrEmpty(hdExamID.Value) ? 0 : Convert.ToInt32(hdExamID.Value));
                aExamRoutine.SubjectId = Convert.ToInt32(ddlSubject.SelectedValue);
                aExamRoutine.StartTime = box1.Text;
                aExamRoutine.EndTime = box2.Text;
                aExamRoutine.ExamDate = examDate;
                ExamRoutineList.Add(aExamRoutine);
            }
            if (ExamRoutineList.Count > 0)
            {
                int result = objRoutine.ExamRoutineInsert(ExamRoutineList, Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMedium.SelectedValue),Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue),
                    Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue));
                if (result > 0)                
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);                
                else
                    MessageController.Show("Failed to save information. Please try anothor.", MessageType.Error, Page);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        DataTable dts = (DataTable)ViewState["Criteria"];
        Session["Criteria"] = dts;

        Response.Redirect("~/Pages/Teacher/CreateExamRoutine.aspx");


    }
    private bool ValidExamRoutineData()
    {
        if (IsDuplicateSubject())
        {
            MessageController.Show("Duplicate subject entered. Please try anothor.", MessageType.Error, Page);
            return false;
        }
        if (IsBlankData())
        {
            MessageController.Show("Please check your input filed input field can not be blank.", MessageType.Error, Page);
            return false;
        }
        if (IsTimeConflict())
        {
            MessageController.Show("Time Conflict... Please Change Time...", MessageType.Error, Page);
            return false;
        }
        return true;
    }

    private bool IsDuplicateSubject()
    {
        bool result = false;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            DropDownList ddlSubject = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("ddlSubject");
            if (!string.IsNullOrEmpty(ddlSubject.SelectedValue))
            {
                if (Gridview1.Rows.Count > 0)
                {
                    for (int j = i + 1; j < Gridview1.Rows.Count; j++)
                    {
                        DropDownList ddlSubject1 = (DropDownList)Gridview1.Rows[j].Cells[1].FindControl("ddlSubject");
                        if (ddlSubject.SelectedValue == ddlSubject1.SelectedValue)
                        {
                            Gridview1.Rows[i].BackColor = Color.Red;
                            Gridview1.Rows[j].BackColor = Color.Red;
                            result = true;
                        }
                        else
                        {
                            Gridview1.Rows[i].BackColor = Color.White;
                            Gridview1.Rows[j].BackColor = Color.White;
                        }
                    }
                }
            }
        }
        return result;
    }
    private bool IsBlankData()
    {
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            DropDownList ddlSubject = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("ddlSubject");
            TextBox txtStartTime = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
            TextBox txtEndTime = (TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox2");
            TextBox txtDate = (TextBox)Gridview1.Rows[i].Cells[4].FindControl("tbxDate");
            if (ddlSubject.SelectedValue != null && Convert.ToInt32(ddlSubject.SelectedValue) == -1)
            {
                Gridview1.Rows[i].BackColor = Color.Red;
                return true;
            }
            if (string.IsNullOrEmpty(txtStartTime.Text))
            {
                Gridview1.Rows[i].BackColor = Color.Red;
                return true;
            }
            if (string.IsNullOrEmpty(txtEndTime.Text))
            {
                Gridview1.Rows[i].BackColor = Color.Red;
                return true;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                Gridview1.Rows[i].BackColor = Color.Red;
                return true;
            }
        }
        return false;
    }
    private bool IsTimeConflict()
    {
        bool result = false;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            TextBox eTime = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox2");
            TextBox sTime = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1");
            TextBox date = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("tbxDate");

            if (!string.IsNullOrEmpty(sTime.Text) && !string.IsNullOrEmpty(eTime.Text) && !string.IsNullOrEmpty(date.Text))
            {
                TimeSpan startTime = TimeSpan.Parse(sTime.Text);
                TimeSpan endTime = TimeSpan.Parse(eTime.Text);
                if (startTime < endTime)
                {
                    for (int j = i + 1; j < Gridview1.Rows.Count; j++)
                    {
                        TextBox sTime1 = (TextBox)Gridview1.Rows[j].Cells[1].FindControl("TextBox1");
                        TextBox eTime1 = (TextBox)Gridview1.Rows[j].Cells[1].FindControl("TextBox2");
                        TextBox date1 = (TextBox)Gridview1.Rows[j].Cells[1].FindControl("tbxDate");
                        TimeSpan startTime1 = TimeSpan.Parse(sTime1.Text);
                        TimeSpan endTime1 = TimeSpan.Parse(eTime1.Text);
                        if ((date.Text == date1.Text) && ((startTime1 > startTime && endTime > startTime1) || (endTime1 > startTime && endTime1 < endTime)))
                        {
                            Gridview1.Rows[i].BackColor = Color.Red;
                            Gridview1.Rows[j].BackColor = Color.Red;
                            result = true;
                        }
                        else
                        {
                            Gridview1.Rows[i].BackColor = Color.White;
                            Gridview1.Rows[j].BackColor = Color.White;
                        }
                    }
                }
                else
                {
                    Gridview1.Rows[i].BackColor = Color.Red;                   
                    string myStringVariables = "Start Time must be smaller than End Time....";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + myStringVariables + "');", true); 
                    result = true;
                }
                

            }
        }

        return result;

    }
    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadExistingRutine();
    }
   
}