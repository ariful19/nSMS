using AjaxControlToolkit;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;

public partial class Pages_Library_IssueAndReturnBooks : System.Web.UI.Page
{
    protected static int ID;
    dalLibrary objLibrary = new dalLibrary();    
    dalStudent objStudent = new dalStudent();
    dalTeacher objTeacher = new dalTeacher();   
    int personId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBookList();
            LoadData();
            BindData();
            dtStu = new DataTable("dtStudent");
            tbxDate.Text = DateTime.Now.ToString("dd/MM/yyyy");            
            tbxDueDate.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            Clear();
            ////pnlAssignStudent.Visible = true;
            pnlAssignedBook.Visible = false;
            pnlAllBookList.Visible = false;
            pnlReturnBook.Visible = false;
            CheckRollNoUse();
        }
    }
    #region Load Data

    protected void LoadData()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
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

        rptTeacher.DataSource = (DataTable)new dalTeacher().GetAllTeacher();
        rptTeacher.DataBind();
        
    }
    protected void LoadBookList()
    {
        string criteria = "";
        DataTable dt = objLibrary.GetBook(criteria);
        rptBook.DataSource = dt;
        rptBook.DataBind();
        ViewState["dtBook"] = null;
    }
   
    #endregion

    #region Get Criteria

    protected string GetCriteria()
    {
        string criteria = "";
        if (rdList.SelectedValue == "1")
        {
            if (ddlYear.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.Year=" + ddlYear.SelectedValue;
                else
                    criteria += "er_StudentToClass.Year=" + ddlYear.SelectedValue;
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
                    criteria += "and er_StudentToClass.ClassId=" + ddlClass.SelectedValue;
            }
            if (ddlGroup.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
                else
                    criteria += "and er_StudentToClass.GroupId=" + ddlGroup.SelectedValue;
            }

            if (ddlShift.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
                else
                    criteria += "and er_StudentToClass.ShiftId=" + ddlShift.SelectedValue;
            }

            if (ddlSection.SelectedValue != "")
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
                else
                    criteria += "and er_StudentToClass.SectionId=" + ddlSection.SelectedValue;
            }
            if (tbxRollNo.Text != "")
            {
                if (criteria == "")
                    criteria = "er_StudentToClass.RollNo=" + tbxRollNo.Text;
                else
                    criteria += " and er_StudentToClass.RollNo=" + tbxRollNo.Text;
            }            
            else
            {
                DataTable dt = new dalStudent().GetStudentToClassByStudentID(Common.SessionInfo.StudentId, Convert.ToInt32(ddlClass.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    if (criteria == "")
                        criteria = "er_StudentToClass.RollNo=" + dt.Rows[0]["RollNo"].ToString();
                    else
                        criteria += " and er_StudentToClass.RollNo=" + dt.Rows[0]["RollNo"].ToString();
                }
            }

            if (tbxReg.Text != "")
            {
                if (criteria == "")
                    criteria = "ss_Student.RegNo=" + tbxReg.Text;
                else
                    criteria += " and ss_Student.RegNo='" + tbxReg.Text + "'";
            }

        }
        else
        {
            if (tbxTecherPin.Text != null)
            {
                if (criteria == "")
                    criteria = "TeacherPin=" + tbxTecherPin.Text;
                else
                    criteria += " and TeacherPin=" + tbxTecherPin.Text;
            }
        }
        
        
        ViewState["craiteria"] = criteria;
        return criteria;
    }

    #endregion      
   
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
            LabelRoll.Visible = false;
            tbxRollNo.Visible = false;
        }
        else
        {
            LabelReg.Visible = false;
            tbxReg.Visible = false;
        }

    }
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

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (IsRollNoUse == "False")
        {
            e.Row.Cells[2].Visible = false;
        }
    }
   private DataTable dtStu
    {
        set { ViewState["dtStu"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtStu"];
            }
            catch
            {
                return null;
            }
        }
    }
   private DataTable dtTeacher
    {
        set { ViewState["dtTeacher"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtTeacher"];
            }
            catch
            {
                return null;
            }
        }
    }
   private DataTable dtOther
    {
        set { ViewState["dtOther"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtOther"];
            }
            catch
            {
                return null;
            }
        }
    }    
    public DataTable Student()
    {
        for (int index = dtStu.Rows.Count - 1; index >= 0; index--)
        {
            dtStu.Rows.RemoveAt(index);
        }
        for (int index = dtStu.Columns.Count - 1; index >= 0; index--)
        {
            dtStu.Columns.RemoveAt(index);
        }
        
        dtStu.Columns.Add("PersonId", typeof(int));
        dtStu.Columns.Add("RegNo", typeof(string));        
        dtStu.Columns.Add("NameEng", typeof(string));
        dtStu.Columns.Add("RollNo", typeof(int));
        dtStu.Columns.Add("Mobile", typeof(string));     


        return dtStu;
    }
    public DataTable Other()
    {
        if (!string.IsNullOrEmpty(Request.Form["NameEng"]))
        {
            dtOther.Columns.Add("NameEng", typeof(string));
            dtOther.Columns.Add("Designation", typeof(string));
            dtOther.Columns.Add("Mobile", typeof(string));
            dtOther.Columns.Add("Address", typeof(string));

        }
        return dtOther;
    }
    public DataTable Teacher()
    {
        dtTeacher.Columns.Add("Name", typeof(string));
        dtTeacher.Columns.Add("TeacherPinNo", typeof(string));
        dtTeacher.Columns.Add("Mobile", typeof(string));
        dtTeacher.Columns.Add("Address", typeof(string));
        return dtTeacher;
    }
    public DataTable Book()
    {
        DataTable dtBook = new DataTable();
        dtBook.Columns.Add("Id", typeof(int));
        dtBook.Columns.Add("Title", typeof(string));
        dtBook.Columns.Add("Author", typeof(string));
        dtBook.Columns.Add("Publisher", typeof(string));
        dtBook.Columns.Add("DueDate", typeof(string));
        dtBook.Columns.Add("IsChecked", typeof(bool));
        return dtBook;
    }
    public DataTable IssueBook()
    {
        DataTable dtIssue = new DataTable("dtIssueBook");
        dtIssue.Columns.Add("Id", typeof(int));       
        dtIssue.Columns.Add("IsReturn", typeof(bool));
        return dtIssue;
    }

    
    private DataTable GetIssuedBooks(string criteria)
    {
        DataTable dtIssedBook=new DataTable();
        bool isReturn = false;
        string book = "Book Name: ";

        if (rdList.SelectedValue == "1")
            dtIssedBook = objLibrary.GetStudentIssedBook(criteria, isReturn);
        else if (rdList.SelectedValue == "2")
            dtIssedBook = objLibrary.GetTeacherIssuedBook(criteria, isReturn);
        else
            return null;

        if (dtIssedBook.Rows.Count > 0)
        {
            pnlReturnBook.Visible = true;
            gvBookReturn.DataSource = dtIssedBook;
            gvBookReturn.DataBind();
            //btnReturn.Visible = true;

        }

        for (int i = 0; dtIssedBook.Rows.Count > i; i++)
        {
            
            DateTime timeSt = Convert.ToDateTime(dtIssedBook.Rows[i]["IssueDate"].ToString());
            DateTime time = DateTime.Now;
            double diffTime = (time - timeSt).TotalDays;

            
            if ( diffTime > 7)
            {
                book += dtIssedBook.Rows[i]["Title"].ToString() + "; ";
                gvBookReturn.Rows[i].BackColor = Color.Coral;
                //gvBookReturn.Rows[i].Cells[5].BackColor = Color.Coral;
                //gvBookReturn.Rows[i].Cells[5].BackColor = Color.LemonChiffon;                
                
                MessageController.Show("Already passed Return Date..."+book, MessageType.Warning, Page);
                //lblReturn.Text=("Already passed Return Date for " + book);
            }
            else if (diffTime > 5)
            {
                
                book += dtIssedBook.Rows[i]["Title"].ToString() + "; ";
                gvBookReturn.Rows[i].BackColor = Color.Plum;
                MessageController.Show("You have " + Math.Round(diffTime) + " Days to Return Issed Books..." + book, MessageType.Warning, Page);
            }
            else
            {
                gvBookReturn.Rows[i].BackColor = Color.PaleGreen; 
            }
            gvBookReturn.HeaderRow.BackColor = Color.BurlyWood;
           
        }
            
        return dtIssedBook;
    }   
    public DataTable Get_EmptyOther()
    {
        DataTable dtEmptyOther = new DataTable();
        //Here ensure that you have added all the column available in your gridview
        dtEmptyOther.Columns.Add("NameEng", typeof(string));
        dtEmptyOther.Columns.Add("Designation", typeof(string));
        dtEmptyOther.Columns.Add("Address", typeof(string));
        dtEmptyOther.Columns.Add("Mobile", typeof(string));
        DataRow datatRow = dtEmptyOther.NewRow();

        
        dtEmptyOther.Rows.Add(datatRow);
        return dtEmptyOther;
    }
    public DataTable Get_Book()
    {
        DataTable dtEmptyOther = Book();
      
        DataRow datatRow = dtEmptyOther.NewRow();


        dtEmptyOther.Rows.Add(datatRow);
        return dtEmptyOther;
    }

    //protected void GetStudent()
    //{
    //    if (!string.IsNullOrEmpty(Request.Form["year"]))
    //    {
    //        dtStu = Student();
    //        string[] year = Request.Form["year"].Split(',');
    //        string[] classes = Request.Form["class"].Split(',');
    //        string[] group = Request.Form["group"].Split(',');
    //        string[] shift = Request.Form["shift"].Split(',');
    //        string[] section = Request.Form["section"].Split(',');
    //        string[] roll = Request.Form["roll"].Split(',');
    //        for (int i = 0; i < year.Length; i++)
    //        {
    //            dtStu.Rows.Add(year[i], classes[i], group[i], shift[i], section[i], roll[i]);
    //        }
    //    }
    //}     
    public bool ValidationCheck()
    {
        //if (tbxRollNo.Text == "")
        //{
        //    MessageController.Show("Roll No Must be Needed!!!", MessageType.Warning, Page);
        //    return true;
        //}

        return false;
    }    
    public void Clear()
    {
        tbxOtherName.Text = "";
        tbxOtherMobile.Text = "";
        tbxOtherDsg.Text = "";
        tbxOtherAddress.Text = "";

        tbxRollNo.Text = "";
        tbxTecherPin.Text = "";

    }

    protected void BindData()
    {

        //Emty Row for gvOther........... 
        gvOther.DataSource = Get_EmptyOther();
        gvOther.DataBind();
        gvBookIssue.DataSource = Get_Book();
        gvBookIssue.DataBind();
    }
    protected void BindStudent()
    {
        string criteria = GetCriteria();
        DataTable dt = objStudent.GetByCriteria(criteria);
        //DataTable dtIssuedBook = GetIssuedBooks(criteria);

        if (dt.Rows.Count > 0)
        {
            personId = Convert.ToInt32(dt.Rows[0]["PersonId"].ToString());
            rptStudent.DataSource = dt;
            rptStudent.DataBind();           

        }
        else
        {
            MessageController.Show("No student found.", MessageType.Warning, Page);
            gvStudent.DataSource = null;
            gvStudent.DataBind();
            //pnlAssignStudent.Visible = true;
            pnlAllBookList.Visible = false;
        }

        gvTeacher.DataSource = null;
        gvTeacher.DataBind();
        gvOther.DataSource = null;
        gvOther.DataBind();

    }
    protected void BindTeacher(int teacherPin)
    {
        MessageController.Clear(this);

        string criteria = criteria = "TeacherPin=" + teacherPin;
        ViewState["craiteria"] = criteria;
        DataTable dt = objTeacher.GetTeacherByCriteria(criteria);
        DataTable dtIssedBook = GetIssuedBooks(criteria);

        if (dt.Rows.Count > 0)
        {
            personId = Convert.ToInt32(dt.Rows[0]["PersonId"].ToString());
            gvTeacher.DataSource = dt;
            gvTeacher.DataBind();
            gvTeacher.HeaderRow.BackColor = Color.PaleTurquoise;
            gvTeacher.RowStyle.BackColor = Color.Lavender;
            pnlAllBookList.Visible = true;
        }
        else
        {
            MessageController.Show("No Teacher found.", MessageType.Warning, Page);
            gvTeacher.DataSource = null;
            gvTeacher.DataBind();
            pnlAllBookList.Visible = false;

        }

        gvStudent.DataSource = null;
        gvStudent.DataBind();
        gvOther.DataSource = null;
        gvOther.DataBind();
        gvBookIssue.DataSource = null;
        gvBookIssue.DataBind();


    }
  

    //[WebMethod]
    //public static Other[] BindDatatable()
    //{
    //    DataTable dt = new DataTable();
    //    List<Other> details = new List<Other>();
       
    //            foreach (DataRow dtrow in dt.Rows)
    //            {
    //                Other dept = new Other();
    //                dept.Name = dtrow["NameEng"].ToString();
    //                dept.Designation = dtrow["Designation"].ToString();
    //                dept.Address = dtrow["Address"].ToString();
    //                dept.Mobile = dtrow["Mobile"].ToString();
    //                details.Add(dept);
    //            }
            
        
    //    return details.ToArray();
    //}

    protected void OnRatingChanged(object sender, RatingEventArgs e)
    {

    }

   protected void RepeaterGetSelectedID(object source, RepeaterCommandEventArgs e)
    {
        MessageController.Clear(this);

        DataTable dt = new DataTable();
        
        if (e.CommandName.Equals("Select"))
        {

            HiddenField hiddenBookID = (HiddenField)e.Item.FindControl("HiddenBookID");
            Label lblAuthor = (Label)e.Item.FindControl("lblAuthor");
            Label lblPublisher = (Label)e.Item.FindControl("lblPublisher");
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblStock = (Label)e.Item.FindControl("lblStock");

            int bookId  =Convert.ToInt32( hiddenBookID.Value);
            string title = lblTitle.Text;
            string author = lblAuthor.Text;
            string publihser = lblPublisher.Text;

            
            dt = Book();
            if(ViewState["dtBook"]!=null)
            dt = (DataTable) ViewState["dtBook"];
            if (Convert.ToInt32(lblStock.Text) > 0)
            {
                if (gvBookReturn.Rows.Count > 0)
                {
                    for (int i = 0; gvBookReturn.Rows.Count > i; i++)
                    {
                        Label lblBookId = (Label)gvBookReturn.Rows[i].FindControl("lblBookId");
                        if (Convert.ToInt32(lblBookId.Text) == bookId)
                        {
                            bookId = 0;
                        }

                    }

                }

                if (bookId != 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt32(row["Id"]) == bookId)
                        {
                            bookId = 0;
                        }
                    }

                    if (bookId > 0)
                        dt.Rows.Add(bookId, title, author, publihser, tbxDueDate.Text, false);
                    else
                    {
                        string myStringVariable = "This Book Already Exist in Selected BookList..";
                        MessageController.Show("This Book Already Exist in Selected BookList..", MessageType.Warning, Page);
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                    }

                }
                else
                {
                    string myStringVariable = "This Book Already Issued..";
                    MessageController.Show("This Book Already Issued..", MessageType.Warning, Page);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Myscript1", @"alert('This Book Already Issued..');", true);
                    return;
                }
            }
            else
            {
                string myStringVariable = "This Book is out of Stock..";
                MessageController.Show("This Book is out of Stock..", MessageType.Warning, Page);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Myscript1", @"alert('This Book Already Issued..');", true);
                return;
            }
            
            

            if (dt.Rows.Count > 0)
            {
                pnlAssignedBook.Visible = true;
                ViewState["dtBook"] = dt;
               
            }
            gvBookIssue.DataSource = dt;
            gvBookIssue.DataBind();
            gvBookIssue.HeaderRow.BackColor = Color.BurlyWood;
            gvBookIssue.RowStyle.BackColor = Color.Moccasin;
            
        }

    }

    protected void StockCounting(DataTable dt)
    {
        bool isAvailable = true;
        bool isReturn;
        int bookId = 0;
        int stock=0;
        for (int i = 0; dt.Rows.Count > i; i++)
        {
            isReturn = Convert.ToBoolean(dt.Rows[i]["isReturn"]);
            bookId = Convert.ToInt32(dt.Rows[i]["Id"]);
            DataTable dtBook = objLibrary.GetBookById(bookId);

            if (isReturn == false)
            {
                stock = Convert.ToInt32(dtBook.Rows[0]["Stock"])-1;
            }
            else
                stock = Convert.ToInt32(dtBook.Rows[0]["Stock"]) + 1;
            if(stock>0)
            {
                objLibrary.UpdateBookStock(bookId,stock, isAvailable);
            }
            else if(stock==0)
            {
                objLibrary.UpdateBookStock(bookId, stock, false);
            }
 
        }
        

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        //---Null Previous Data----\\

        ViewState["dtBook"] = null;
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        gvBookReturn.DataSource = null;
        gvBookReturn.DataBind();
        gvBookIssue.DataSource = null;
        gvBookIssue.DataBind();
        pnlReturnBook.Visible = false;
        pnlAssignedBook.Visible = false;
        pnlAllBookList.Visible = false;

        if (ValidationCheck())
        {
            return;
        }

        BindStudent();

        Clear();

    }
    protected void btnStudent_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        
        string criteria = "";
        DataTable dt = Student();
       for (int i = 0; i < rptStudent.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rptStudent.Items[i].FindControl("chkrow");
                if (chk.Checked)
                {
                    HiddenField hdnPersonId = (HiddenField)rptStudent.Items[i].FindControl("hdnPersonId");
                    Label lblRegNo = (Label)rptStudent.Items[i].FindControl("lblRegNo");
                    Label lblNameEng = (Label)rptStudent.Items[i].FindControl("lblNameEng");
                    Label lblRollNo = (Label)rptStudent.Items[i].FindControl("lblRollNo");
                    Label lblMobile = (Label)rptStudent.Items[i].FindControl("lblMobile");                    
                    criteria+="st_Person.Id="+hdnPersonId.Value;
                    dt.Rows.Add(Convert.ToInt32(hdnPersonId.Value), lblRegNo.Text, lblNameEng.Text, Convert.ToInt32(lblRollNo.Text), lblMobile.Text);
                }
            }
       if (dt.Rows.Count > 0)
       {
           gvStudent.DataSource = dt;
           gvStudent.DataBind();
           gvStudent.HeaderRow.BackColor = Color.PaleTurquoise;
           gvStudent.RowStyle.BackColor = Color.Lavender;
           rptStudent.DataSource = null;
           rptStudent.DataBind();
           pnlAllBookList.Visible = true;
           DataTable dtIssedBook = GetIssuedBooks(criteria);
       }
        
       
        Clear();

    }
    protected void btnTeacher_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        int teacherPin = 0;
        ViewState["dtBook"] = null;
        gvTeacher.DataSource = null;
        gvTeacher.DataBind();
        gvBookReturn.DataSource = null;
        gvBookReturn.DataBind();
        rptStudent.DataSource = null;
        rptStudent.DataBind();
        //gvBookIssue.DataSource = null;
        //gvBookIssue.DataBind();
        pnlAssignedBook.Visible = false;
        pnlReturnBook.Visible = false;

        for (int i = 0; i < rptTeacher.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rptTeacher.Items[i].FindControl("chkrow");
            if (chk.Checked)
            {
                Label lblTeacherPin = (Label)rptTeacher.Items[i].FindControl("lblTeacherPin");
                teacherPin = Convert.ToInt32(lblTeacherPin.Text);
                          
            }
            chk.Checked = false;
        }

        //if (tbxTecherPin.Text == "")
        //{
        //    MessageController.Show("Teacher Pin No Must be Needed!!!", MessageType.Warning, Page);
        //    pnlAllBookList.Visible = false;
        //    return;
        //}
        BindTeacher(teacherPin);
        
        Clear();


    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);

        BindData();
        Clear();

    }
    protected void btnIssue_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        DataTable dtSelectBook = (DataTable)ViewState["dtBook"];
        string craiteria = (string)ViewState["craiteria"];

        DataTable dt = IssueBook();
        int bookId = 0;
        if (rdList.SelectedValue != null)
        {
            if (rdList.SelectedValue == "1")
            {
                HiddenField hdnStuPersonId = (HiddenField)gvStudent.Rows[0].FindControl("hdnStuPersonId");
                personId = Convert.ToInt32(hdnStuPersonId.Value);
            }
            else if (rdList.SelectedValue == "2")
            {
                HiddenField hdnTechPersonId = (HiddenField)gvTeacher.Rows[0].FindControl("hdnTechPersonId");
                personId = Convert.ToInt32(hdnTechPersonId.Value);
            }

            for (int i = 0; gvBookIssue.Rows.Count > i; i++)
            {
                Label lblBookId = (Label)gvBookIssue.Rows[i].FindControl("lblBookId");
                CheckBox checkBox = (CheckBox)gvBookIssue.Rows[i].FindControl("chkBook");

                if (checkBox.Checked)
                {
                    if (lblBookId.Text != "")
                    {
                        bookId = Convert.ToInt32(lblBookId.Text);
                        dt.Rows.Add(bookId, false);
                        
                        //for (int j = dtSelectBook.Rows.Count - 1; j >= 0; j--)
                        //{
                        //    DataRow dr = dtSelectBook.Rows[j];
                            
                        //    if (Convert.ToInt32(dr["Id"]) == bookId)
                        //        dr.Delete();
                        //}
                    }
                     
                }           
                          
            }
            

            if (dt.Rows.Count > 0)
            {
                int result = objLibrary.IssueBookInsert(dt, personId, DateTime.Now, DateTime.Now.AddDays(7), Page.User.Identity.Name);
                if (result > 0)
                {
                    StockCounting(dt);
                    MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                    gvBookIssue.DataSource = null;
                    gvBookIssue.DataBind();
                    gvStudent.DataSource = null;
                    gvStudent.DataBind();
                    gvTeacher.DataSource = null;
                    gvTeacher.DataBind();
                    pnlAssignedBook.Visible = false;
                    pnlReturnBook.Visible = false;
                    pnlAllBookList.Visible = false;
                    LoadData();
                }              
                               
            }
            else
            {
                MessageController.Show("Please Checked atleast One Book for Issue....", MessageType.Warning, Page);
            }
 
        }
        else
        {
            MessageController.Show("Please selecte any type..", MessageType.Warning, Page);
        }

        //if (dtSelectBook.Rows.Count > 0)
        //{
        //    gvBookIssue.DataSource = dtSelectBook;
        //    gvBookIssue.DataBind();
        //}
        //else
        //{
        //    gvBookIssue.DataSource = null;
        //    gvBookIssue.DataBind();
        //    pnlAssignedBook.Visible = false;
        //}

        //DataTable dtReturn = GetIssuedBooks(craiteria);
        //if (dtReturn.Rows.Count > 0)
        //{
        //    pnlReturnBook.Visible = true;
        //}
        //else
        //{
        //    gvBookReturn.DataSource = null;
        //    gvBookReturn.DataBind();
            
        //}

        LoadBookList();
        
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        string craiteria = (string)ViewState["craiteria"];
        DataTable dt = IssueBook();

        List<IssueBook> issuedBookList = new List<IssueBook>();

        for(int i = 0; gvBookReturn.Rows.Count > i; i++)
        {
            IssueBook issuedBook = new IssueBook();
            Label lblIssedBookId = (Label)gvBookReturn.Rows[i].FindControl("lblIssuedBookId");
            Label lblBookId = (Label)gvBookReturn.Rows[i].FindControl("lblBookId");
            CheckBox checkBox = (CheckBox)gvBookReturn.Rows[i].FindControl("chkReturnBook");
           
            if(checkBox.Checked)
            {
                issuedBook.Id=(string.IsNullOrEmpty(lblIssedBookId.Text) ? 0 : Convert.ToInt32(lblIssedBookId.Text));
                issuedBook.BookId=(string.IsNullOrEmpty(lblBookId.Text) ? 0 : Convert.ToInt32(lblBookId.Text));
                issuedBook.IsReturn=true;
                issuedBook.ReceivedBy=Page.User.Identity.Name;
                issuedBook.ReceivedDate=DateTime.Now.Date;

                issuedBookList.Add(issuedBook);

                dt.Rows.Add(issuedBook.BookId, issuedBook.IsReturn);
            }
            else
            {

            }           
            
        }

        if (issuedBookList.Count > 0)
        {
            int result = objLibrary.ReturnBook(issuedBookList);
             StockCounting(dt);
             MessageController.Show("Selected Book Return successfully..", MessageType.Information, Page); 
      
                       
        }
        else
        {
            MessageController.Show("Please selecte a Book first..", MessageType.Warning, Page);
        }
        DataTable dtIssued = GetIssuedBooks(craiteria);
        if (dtIssued.Rows.Count > 0)
        {
      
        }
        else
        {
            gvBookReturn.DataSource = null;
            gvBookReturn.DataBind();
            pnlReturnBook.Visible = false;
        }
        LoadBookList();
        
      }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        MessageController.Clear(this);
        ViewState["dtBook"] = null;
        gvBookIssue.DataSource = null;
        gvBookIssue.DataBind();
        pnlAssignedBook.Visible = false;
    }
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = objLibrary.GetBookById(ID);
        if (dt.Rows.Count > 0)
        {
            lblCategory.Text = dt.Rows[0]["Category"].ToString();
            lblSubCategory.Text = dt.Rows[0]["SubCategory"].ToString();
            lblCountry.Text = dt.Rows[0]["Country"].ToString();
            lblLanguage.Text = dt.Rows[0]["Language"].ToString();
            lblPublisher.Text = dt.Rows[0]["Publisher"].ToString();
            lblEdtion.Text = dt.Rows[0]["Edition"].ToString();
            lblISBN.Text = dt.Rows[0]["ISBN"].ToString();
            lblVolume.Text = dt.Rows[0]["VolumeNo"].ToString();
            lblSelfNo.Text = dt.Rows[0]["SelfNo"].ToString();
            lblCellNo.Text = dt.Rows[0]["CellNo"].ToString();
            lblStock.Text = dt.Rows[0]["Stock"].ToString();
            lblTitle.Text = dt.Rows[0]["Title"].ToString();
            lblAuthor.Text = dt.Rows[0]["Author"].ToString();
            lblSubTitle.Text = dt.Rows[0]["SubTitle"].ToString();
            lblKeyWord.Text = dt.Rows[0]["KeyWords"].ToString();
            lblDescription.Text = dt.Rows[0]["Description"].ToString();
            lblAvailable.Text = dt.Rows[0]["Status"].ToString() == "True" ? "Yes" : "No";
            if (dt.Rows[0]["CoverPhoto"].ToString() == "")
            {
                imgCover.ImageUrl = "~/Images/Common/no-photo.jpg";
            }
            else
            {
                imgCover.ImageUrl = "~/Images/Book/" + dt.Rows[0]["CoverPhoto"].ToString();
            }

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
    }
        
    }

 