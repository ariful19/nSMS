using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Teacher_CreateClassRoutine : System.Web.UI.Page
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

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

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
            ddlSection.SelectedValue = dtCriteria.Rows[0]["SectionId"].ToString();
            ddlShift.SelectedValue = dtCriteria.Rows[0]["ShiftId"].ToString();
            ddlDay.SelectedValue = dtCriteria.Rows[0]["DayName"].ToString();

            if (Session["mgs"] != null)
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            //Clear the session variable
            Session["Criteria"] = null;
            Session["mgs"] = null;
            // Enable some control to display the message (control is likely on the master page)
        }
    }
    private void LoadTeacher(DropDownList ddl)
    {
        ArrayList arr = GetTeacher();
        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
    }
    private void LoadExistingRutine()
    {
        DataTable dtCriteria = GetCriteria();
        int year = (!string.IsNullOrEmpty(ddlYear.SelectedValue)) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
        int mediumID = (!string.IsNullOrEmpty(ddlMedium.SelectedValue)) ? Convert.ToInt32(ddlMedium.SelectedValue) : 0;
        int campusID = (!string.IsNullOrEmpty(ddlCampus.SelectedValue)) ? Convert.ToInt32(ddlCampus.SelectedValue) : 0;
        int classID = (!string.IsNullOrEmpty(ddlClass.SelectedItem.Text)) ? Convert.ToInt32(ddlClass.SelectedValue) : 0;
        int groupID = (!string.IsNullOrEmpty(ddlGroup.SelectedItem.Text)) ? Convert.ToInt32(ddlGroup.SelectedValue) : 0;
        int sectionID = (!string.IsNullOrEmpty(ddlSection.SelectedItem.Text)) ? Convert.ToInt32(ddlSection.SelectedValue) : 0;
        int shifID = (!string.IsNullOrEmpty(ddlShift.SelectedItem.Text)) ? Convert.ToInt32(ddlShift.SelectedValue) : 0;
        string dayName = (!string.IsNullOrEmpty(ddlDay.SelectedItem.Text)) ? ddlDay.SelectedItem.Text : string.Empty;
        dtCriteria.Rows.Add(year, mediumID, campusID, classID, groupID, sectionID, shifID, dayName);
        ViewState["Criteria"] = dtCriteria;

        DataTable dt = new dalClass().GetClassRoutine(year,mediumID,campusID, classID, groupID, sectionID, shifID, dayName);
        dt.Columns.Add("Teacher");
        Gridview1.DataSource = dt;
        Gridview1.DataBind();

        int rowIndex = 0;

        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                TextBox box1 = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
                TextBox box2 = (TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox2");
                LinkButton lb = (LinkButton)Gridview1.Rows[i].Cells[5].FindControl("LinkButton1");
                DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddlSubject");
                DropDownList ddl2 = (DropDownList)Gridview1.Rows[rowIndex].Cells[4].FindControl("ddlTeacher");

                if (lb != null)
                {
                    lb.Visible = false;
                }
                FillDropDownList(ddl1);
                LoadTeacher(ddl2);

                if (i <= dt.Rows.Count - 1)
                {

                    //Assign the value from DataTable to the TextBox   
                    box1.Text = dt.Rows[i]["StartTime"].ToString();
                    box2.Text = dt.Rows[i]["EndTime"].ToString();

                    //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                    ddl1.ClearSelection();
                    ddl1.SelectedValue = dt.Rows[i]["SubjectId"].ToString();
                    //ddl1.Items.FindByText(dt.Rows[i]["SubjectId"].ToString()).Selected = true;

                    ddl2.ClearSelection();
                    ddl2.SelectedValue = dt.Rows[i]["TeacherId"].ToString();
                    dt.Rows[i]["Teacher"] = dt.Rows[i]["TeacherId"].ToString();
                    //ddl2.Items.FindByText(dt.Rows[i]["Teacher"].ToString()).Selected = true;

                    //Show delete button in grid.
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
    #region Dynamic Row    
    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        DataTable dt = obj.GetByClassAndGroupId(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue));
        foreach (DataRow dr in dt.Rows)
        {
            arr.Add(new ListItem(dr["SubjectName"].ToString(), dr["SubjectId"].ToString()));
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

    private ArrayList GetTeacher()
    {
        ArrayList arr = new ArrayList();
        DataTable dt = new dalTeacher().GetAll();
        foreach (DataRow dr in dt.Rows)
        {
            arr.Add(new ListItem(dr["NameEng"].ToString(), dr["Id"].ToString()));
        }
        return arr;
    }    

    protected DataTable GetRoutine()
    {
        DataTable dt = new DataTable("dtRoutine");
        dt.Columns.Add(new DataColumn("ClassRoutineId", typeof(int)));
        dt.Columns.Add(new DataColumn("PeriodNo", typeof(int)));
        dt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
        dt.Columns.Add(new DataColumn("StartTime", typeof(string)));
        dt.Columns.Add(new DataColumn("EndTime", typeof(string)));
        dt.Columns.Add(new DataColumn("Teacher", typeof(int)));
        return dt;
    }
    protected DataTable GetCriteria()
    {
        DataTable dt = new DataTable("dtCriteria");
        dt.Columns.Add(new DataColumn("Year", typeof(int)));
        dt.Columns.Add(new DataColumn("MediumId", typeof (int)));
        dt.Columns.Add(new DataColumn("CampusId", typeof (int)));
        dt.Columns.Add(new DataColumn("ClassId", typeof(int)));
        dt.Columns.Add(new DataColumn("GroupId", typeof(int)));
        dt.Columns.Add(new DataColumn("SectionId", typeof(int)));
        dt.Columns.Add(new DataColumn("ShiftId", typeof(int)));
        dt.Columns.Add(new DataColumn("DayName", typeof(string)));
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
        dt.Columns.Add(new DataColumn("Teacher", typeof(int)));
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
        DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[4].FindControl("ddlTeacher");
        FillDropDownList(ddl1);
        LoadTeacher(ddl2);
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

                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");

                    //extract the DropDownList Selected Items   

                    DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("ddlSubject");
                    DropDownList ddl2 = (DropDownList)Gridview1.Rows[rowIndex].Cells[4].FindControl("ddlTeacher");


                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["PeriodNo"] = i + 1;
                    //drCurrentRow["PeriodNo"] = dtCurrentTable.Rows.Count + 1;

                    dtCurrentTable.Rows[i-1]["StartTime"] = box1.Text;
                    dtCurrentTable.Rows[i-1]["EndTime"] = box2.Text;

                   

                    // Update the DataRow with the DDL Selected Items   

                    dtCurrentTable.Rows[i-1]["SubjectId"] = ddl1.SelectedValue;
                    dtCurrentTable.Rows[i-1]["Teacher"] = ddl2.SelectedValue;

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
                    DropDownList ddl2 = (DropDownList)Gridview1.Rows[rowIndex].Cells[4].FindControl("ddlTeacher");

                    FillDropDownList(ddl1);
                    LoadTeacher(ddl2);

                    if (i < dt.Rows.Count - 1)
                    {
                    }

                        //Assign the value from DataTable to the TextBox   
                        box1.Text = dt.Rows[i]["StartTime"].ToString();
                        box2.Text = dt.Rows[i]["EndTime"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        ddl1.ClearSelection();
                        ddl1.SelectedValue = dt.Rows[i]["SubjectId"].ToString();
                        //ddl1.Items.FindByText(dt.Rows[i]["SubjectId"].ToString()).Selected = true;

                        ddl2.ClearSelection();
                        ddl2.SelectedValue = dt.Rows[i]["Teacher"].ToString();
                        //ddl2.Items.FindByText(dt.Rows[i]["Teacher"].ToString()).Selected = true;

                    

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
            HiddenField hdClassID = (HiddenField)rows.Cells[2].FindControl("hdnClassRoutineId");
            // Compare this row with the currently selected row using the SelectedRow property
            // SelectedRow might be null, the logic would work anyway

            if (hdClassID.Value == "0")
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
            foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;  
            foreach (DataRow row in dt.Rows)
            {
                
                row[0] = rowNumber;
                rowNumber++;
            } 
        }
    }

    #endregion
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        AddNewRowToGrid();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataTable dts = (DataTable)ViewState["Criteria"];
        Session["Criteria"] = dts;
        Response.Redirect("~/Pages/Teacher/ClassRoutineEdit.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidClassRoutineData())
        {

            int period = 1;
            DataTable dt = GetRoutine();
            List<ClassRoutine> lstClassRoutine = new List<ClassRoutine>();

            foreach (GridViewRow gr in Gridview1.Rows)
            {
                HiddenField hdClassID = (HiddenField)gr.Cells[2].FindControl("hdnClassRoutineId");
                TextBox txtStartTime = (TextBox)gr.Cells[2].FindControl("TextBox1");
                TextBox txtEndTime = (TextBox)gr.Cells[3].FindControl("TextBox2");
                DropDownList ddlSubject = (DropDownList)gr.Cells[1].FindControl("ddlSubject");
                DropDownList ddlTeacher = (DropDownList)gr.Cells[4].FindControl("ddlTeacher");
                // dt.Rows.Add(hdClassID.Value, period++, ddlSubject.SelectedValue, txtStartTime.Text, txtEndTime.Text, ddlTeacher.SelectedValue);

                ClassRoutine classRoutine = new ClassRoutine();
                classRoutine.Id = (string.IsNullOrEmpty(hdClassID.Value) ? 0 : Convert.ToInt32(hdClassID.Value));
                classRoutine.Period = period++;
                classRoutine.SubjectId = Convert.ToInt32(ddlSubject.SelectedValue);
                classRoutine.TeacherId = Convert.ToInt32(ddlTeacher.SelectedValue);
                classRoutine.StartTime = txtStartTime.Text;
                classRoutine.EndTime = txtEndTime.Text;
                lstClassRoutine.Add(classRoutine);

            }
            if (lstClassRoutine.Count > 0)
            {
                int result = objRoutine.ClassRoutineInsert(lstClassRoutine, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMedium.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue),
                    Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), ddlDay.SelectedItem.Text);
                if (result > 0)
                {
                    DataTable dts = (DataTable)ViewState["Criteria"];
                    Session["mgs"] = "Save Successfully";
                    Session["Criteria"] = dts;
                    Response.Redirect("~/Pages/Teacher/ClassRoutineEdit.aspx");
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                }

                else
                    MessageController.Show("Failed to save information. Please try anothor.", MessageType.Error, Page);
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

            HiddenField hdClassID = (HiddenField)gvRow.Cells[2].FindControl("hdnClassRoutineId");
            int Id = (string.IsNullOrEmpty(hdClassID.Value) ? 0 : Convert.ToInt32(hdClassID.Value));
            if (Id > 0)
            {
                int result = objRoutine.ClassRoutineDelete(Id);

                if (result > 0)
                    MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
                else if (Id == 0)
                {
                    if (dt.Rows.Count > 1)
                    {
                        if (gvRow.RowIndex < dt.Rows.Count)
                        {
                            //Remove the Selected Row data and reset row number  
                            //Gridview1.DeleteRow(Gridview1.SelectedIndex);
                            dt.Rows.Remove(dt.Rows[rowID]);
                            ResetRowID(dt);

                        }

                    }
                }
                else
                    MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
                Gridview1.DataSource = dt;
                Gridview1.DataBind();

                for (int i = 0; i < Gridview1.Rows.Count - 1; i++)
                {
                    Gridview1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                //SetPreviousData();
                //Response.Redirect("~/Pages/Teacher/CreateClassRoutine.aspx");
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

       
        ////Set Previous Data on Postbacks  

        SetPreviousData();
    }




    private bool ValidClassRoutineData()
    {
        if (IsBlankData())
        {
            MessageController.Show("Please check your input filed input field can not be blank.", MessageType.Error, Page);
            return false;
        }
        if (IsDuplicateSubject())
        {
            MessageController.Show("Duplicate subject entered. Please try anothor.", MessageType.Error, Page);
            return false;
        }
        if (IsTimeConflict())
        {
            MessageController.Show("Time Conflict... Please Change Time...", MessageType.Error, Page);
            return false;
        }
        if (IsTeacherValid())
        {
            MessageController.Show("Time Conflict for Teacher... One can not take Class at same time....", MessageType.Error, Page);
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
    private bool IsTimeConflict()
    {
        bool result = false;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
             TextBox eTime = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox2");
             TextBox sTime = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1");
             TimeSpan startTime=TimeSpan.Parse(sTime.Text);
             TimeSpan endTime=TimeSpan.Parse(eTime.Text);
             if (!string.IsNullOrEmpty(sTime.Text)&&!string.IsNullOrEmpty(eTime.Text))
            {
                 for (int j = i + 1; j < Gridview1.Rows.Count; j++)
                    {
                        TextBox sTime1 = (TextBox)Gridview1.Rows[j].Cells[1].FindControl("TextBox1");
                        TextBox eTime1 = (TextBox)Gridview1.Rows[j].Cells[1].FindControl("TextBox2");

                        TimeSpan startTime1=TimeSpan.Parse(sTime1.Text);
                        TimeSpan endTime1=TimeSpan.Parse(eTime1.Text);
                        if ((startTime1 > startTime && endTime > startTime1)|| (endTime1>startTime && endTime1<endTime))
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

        return result;

    }
    private bool IsBlankData()
    {
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            DropDownList ddlSubject = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("ddlSubject");
            TextBox txtStartTime = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
            TextBox txtEndTime = (TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox2");
            DropDownList ddlTeacher = (DropDownList)Gridview1.Rows[i].Cells[4].FindControl("ddlTeacher");
            if (ddlSubject.SelectedValue != null && Convert.ToInt32(ddlSubject.SelectedValue) == -1)
            {
                Gridview1.Rows[i].BackColor = Color.Red;
                return true;
            }

            if (ddlTeacher.SelectedValue != null && Convert.ToInt32(ddlTeacher.SelectedValue) == -1)
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
        }
        return false;
    }
    private bool IsTeacherValid()
    {
        bool result = false;

        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            DropDownList ddlTeacher = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("ddlTeacher");
            TextBox sTime = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1");
            TextBox eTime = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox2");
            TimeSpan startTime1 = TimeSpan.Parse(sTime.Text);
            TimeSpan endTime1 = TimeSpan.Parse(eTime.Text);
            string criteria = "Day='" + ddlDay.SelectedItem + "' and TeacherId=" + ddlTeacher.SelectedValue + " and Year=" + ddlYear.SelectedValue;
            DataTable dt = new dalClass().GetRoutineByCraiteria(criteria);
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                string classId = dt.Rows[j]["ClassId"].ToString();
                string groupId = dt.Rows[j]["GroupId"].ToString();
                string shiftId = dt.Rows[j]["ShiftId"].ToString();
                string sectionId = dt.Rows[j]["SectionId"].ToString();

                TimeSpan startTime = TimeSpan.Parse(dt.Rows[j]["StartTime"].ToString());
                TimeSpan endTime = TimeSpan.Parse(dt.Rows[j]["EndTime"].ToString());
                if (ddlTeacher.SelectedValue == dt.Rows[j]["TeacherId"].ToString())
                {
                    if (classId != ddlClass.SelectedValue || groupId != ddlGroup.SelectedValue || shiftId != ddlShift.SelectedValue || sectionId != ddlSection.SelectedValue || (startTime != startTime1 && endTime != endTime1))
                    {

                        if ((startTime1 >= startTime && endTime > startTime1) || (endTime1 > startTime && endTime1 <= endTime))
                        {
                            Gridview1.Rows[i].BackColor = Color.Red;
                            string myStringVariables = "One Teacher can not take 2 class at a time....";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + myStringVariables + "');", true);
                            result = true;
                        }
                        else
                        {
                            Gridview1.Rows[i].BackColor = Color.White;
                        }

                    }

                }
            }
        }
        return result;

    }



    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataTable dt = (DataTable)ViewState["CurrentTable"];
            //LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
            //if (lb != null)
            //{
            //    if (dt.Rows.Count > 1)
            //    {
            //        if (e.Row.RowIndex == dt.Rows.Count - 1)
            //        {
            //            lb.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        lb.Visible = false;
            //    }
            //}
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



    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        
        LoadExistingRutine();

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
    protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        LoadExistingRutine();

    }

}