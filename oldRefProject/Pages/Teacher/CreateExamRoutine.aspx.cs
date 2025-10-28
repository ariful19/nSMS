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

public partial class Pages_Teacher_CreateExamRoutine : System.Web.UI.Page
{
    dalSubject obj = new dalSubject();
    dalRoutine objRoutine = new dalRoutine();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
            LoadCriteria();
            SetInitialRow();
            LoadExistingRutine();
        }
    }
    #region Dynamic Row
    
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
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Id", typeof(int)));
        dt.Columns.Add(new DataColumn("PeriodNo", typeof(int)));
        dt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
        dt.Columns.Add(new DataColumn("StartTime", typeof(string)));
        dt.Columns.Add(new DataColumn("EndTime", typeof(string)));
        dt.Columns.Add(new DataColumn("ExamDate", typeof(DateTime)));
        dr = dt.NewRow();
        dr["Id"] = 0;
        dr["PeriodNo"] = 1;
        dr["StartTime"] = string.Empty;
        dr["EndTime"] = string.Empty;
        dt.Rows.Add(dr);
        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;
        //Bind the Gridview   
        Gridview1.DataSource = dt;
        Gridview1.DataBind();
        ShowDeleteButton();
        //After binding the gridview, we can then extract and fill the DropDownList with Data   
        DropDownList ddl1 = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("ddlSubject");
        FillDropDownList(ddl1);
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {                              

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                                        
                    //extract the TextBox values   

                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox2");

                    //extract the DropDownList Selected Items   

                    DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddlSubject");
                    TextBox tbxDate = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("tbxDate");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["PeriodNo"] = dtCurrentTable.Rows.Count + 1;

                    //extract the TextBox values
                    dtCurrentTable.Rows[i - 1]["StartTime"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["EndTime"] = box2.Text;

                    // Update the DataRow with the DDL Selected Items   

                    dtCurrentTable.Rows[i-1]["SubjectId"] = ddl1.SelectedValue;
                    dtCurrentTable.Rows[i-1]["ExamDate"] = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    foreach (System.Data.DataColumn col in dtCurrentTable.Columns) col.ReadOnly = false;
                    drCurrentRow["Id"] = 0;


                    rowIndex++;
                }
                //add new row to DataTable  

                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   

                ViewState["CurrentTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
                ShowDeleteButton();
               
               
            }
        }
        else
        {
            Response.Write("ViewState is null");

        }
        //Set Previous Data on Postbacks   
        SetPreviousData();
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

                    if (i < dt.Rows.Count)
                    {
                        //Assign the value from DataTable to the TextBox   
                        box1.Text = dt.Rows[i]["StartTime"].ToString();
                        box2.Text = dt.Rows[i]["EndTime"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        ddl1.ClearSelection();
                        ddl1.SelectedValue = dt.Rows[i]["SubjectId"].ToString();
                        string id = dt.Rows[i]["ExamDate"].ToString();
                        if (dt.Rows[i]["ExamDate"].ToString() != "")
                            tbxDate.Text = Convert.ToDateTime(dt.Rows[i]["ExamDate"].ToString()).ToString("dd/MM/yyyy");
                       
                    }
                    

                    rowIndex++;
                }
            }
        }
    }

    private void ShowDeleteButton()
    {
        foreach (GridViewRow rows in Gridview1.Rows)
        {
            LinkButton lb = (LinkButton)rows.FindControl("LinkButton1");
            LinkButton lbEdit = (LinkButton)rows.FindControl("btnEdit");
            HiddenField hdExamID = (HiddenField)rows.Cells[2].FindControl("hdnExamRoutineId");
            // Compare this row with the currently selected row using the SelectedRow property
            // SelectedRow might be null, the logic would work anyway

            if (hdExamID.Value == "0")
            {
                lbEdit.Visible = false;
                lb.Visible = true;                
            }

        }
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

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

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
    
    private void LoadExistingRutine()
    {
        DataTable dtCriteria = GetCriteria();
        int year = (!string.IsNullOrEmpty(ddlYear.SelectedItem.Text)) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
        int mediumID = (!string.IsNullOrEmpty(ddlMedium.SelectedValue)) ? Convert.ToInt32(ddlMedium.SelectedValue) : 0;
        int campusID = (!string.IsNullOrEmpty(ddlCampus.SelectedValue)) ? Convert.ToInt32(ddlCampus.SelectedValue) : 0;
        int classID = (!string.IsNullOrEmpty(ddlClass.SelectedItem.Text)) ? Convert.ToInt32(ddlClass.SelectedValue) : 0;
        int ExamTypeID = (!string.IsNullOrEmpty(ddlExamType.SelectedItem.Text)) ? Convert.ToInt32(ddlExamType.SelectedValue) : 0;
        int groupID = (!string.IsNullOrEmpty(ddlGroup.SelectedItem.Text)) ? Convert.ToInt32(ddlGroup.SelectedValue) : 0;
        int shifID = (!string.IsNullOrEmpty(ddlShift.SelectedItem.Text)) ? Convert.ToInt32(ddlShift.SelectedValue) : 0;
        dtCriteria.Rows.Add(year,mediumID,campusID, classID,ExamTypeID, groupID, shifID);
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
            
        }
        else
        {
            SetInitialRow();
            
        }

        
    }
  
    
    #endregion

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        AddNewRowToGrid();
       
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
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
                {
                    
                    DataTable dts = (DataTable)ViewState["Criteria"];
                    Session["mgs"] = "Save Successfully";
                    Session["Criteria"] = dts;
                    Response.Redirect("~/Pages/Teacher/ExamRoutineEdit.aspx");
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                }                 
                else
                    MessageController.Show("Failed to save information. Please try anothor.", MessageType.Error, Page);
            }
        } 
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataTable dts = (DataTable)ViewState["Criteria"];
        Session["Criteria"] = dts;
        Response.Redirect("~/Pages/Teacher/ExamRoutineEdit.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = new DataTable();
            DataRow drCurrentRow = null;

            HiddenField hdExamID = (HiddenField)gvRow.Cells[2].FindControl("hdnExamRoutineId");
            int Id = (string.IsNullOrEmpty(hdExamID.Value) ? 0 : Convert.ToInt32(hdExamID.Value));
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
                ShowDeleteButton();
            }
        }

        //Set Previous Data on Postbacks  
        SetPreviousData();
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
            TextBox txtStartTime = (TextBox) Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
            TextBox txtEndTime = (TextBox) Gridview1.Rows[i].Cells[3].FindControl("TextBox2");
            TextBox txtDate = (TextBox) Gridview1.Rows[i].Cells[4].FindControl("tbxDate");
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
    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dt.Rows.Count - 1)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }
    protected void Gridview1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hdnId = new HiddenField();
        hdnId = (HiddenField)Gridview1.Rows[Gridview1.EditIndex].FindControl("SubjectId");

        HiddenField hdnTeacherId = new HiddenField();
        hdnTeacherId = (HiddenField)Gridview1.Rows[Gridview1.EditIndex].FindControl("TeacherId");

        DropDownList ddlSubject = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("ddlSubject");
        DropDownList ddlTeacher = (DropDownList)Gridview1.Rows[0].Cells[4].FindControl("ddlTeacher");

        DataTable dt = obj.GetByClassAndGroupId(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));


        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "SubjectName";
        ddlSubject.DataValueField = "Id";
        ddlSubject.SelectedValue = hdnId.Value;
        ddlTeacher.SelectedValue = hdnTeacherId.Value;

    }
    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //HiddenField hdnId = new HiddenField();
        //hdnId = (HiddenField)Gridview1.Rows[Gridview1.EditIndex].FindControl("SubjectId");

        //HiddenField hdnTeacherId = new HiddenField();
        //hdnTeacherId = (HiddenField)Gridview1.Rows[Gridview1.EditIndex].FindControl("TeacherId");

        //DropDownList ddlSubject = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("ddlSubject");
        //DropDownList ddlTeacher = (DropDownList)Gridview1.Rows[0].Cells[4].FindControl("ddlTeacher");

        //DataTable dt = obj.GetByClassId(Convert.ToInt32(ddlClass.SelectedValue));


        //ddlSubject.DataSource = dt;
        //ddlSubject.DataTextField = "SubjectName";
        //ddlSubject.DataValueField = "Id";
        //ddlSubject.SelectedValue = hdnId.Value;
        //ddlTeacher.SelectedValue = hdnTeacherId.Value;
    }


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
    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        LoadExistingRutine();
    }
}