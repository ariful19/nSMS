using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
public partial class ShowTeacherAttendenceUI : System.Web.UI.Page
{
    dalTeacher obj = new dalTeacher();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTeacher();
            LoadYear();
        }
        ((Panel)Page.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
    }

    protected void LoadYear()
    {
        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
    }
    protected void LoadTeacher()
    {
        Common.LoadDropdown(ddlTeacher, "view_TeacherAttendance", 1, 0);
    }

    //protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void btnTeacherAttendenceReport_Click(object sender, EventArgs e)
    {

        Response.Redirect("../../Report/Viewer/ReportView.aspx?query=" + EncryptionDecryption.EncryptDecryptString(GetCriteria()) + "&report=TeacherAttendence");

        //if (loadTeacherGridView.Rows.Count > 0)
        //{
        //    GeneratePDF();
        //}
        //else
        //{
        //    MessageController.Show("Please search first!!!", MessageType.Warning, Page);
        //}
    }

    #region Get Criteria  
    protected string GetCriteria()
    {
        string criteria = "";
        if (ddlTeacher.SelectedValue != "")
        {
            if (criteria == "")
                criteria = ddlTeacher.SelectedItem.Value;
            Session["Teacher"] = ddlTeacher.SelectedItem.Value;
           
        }

        if (ddlYear.SelectedValue != "")
        {
            if (criteria == "")
            {
                criteria = ddlYear.SelectedItem.Text;
                Session["Year"] = ddlMonth.SelectedItem.Text;
            }
            else
            {
                criteria +=","+ ddlYear.SelectedItem.Text;
                Session["Year"] = ddlYear.SelectedItem.Text;
            }
        }

        if (ddlMonth.SelectedValue != "")
        {
            if (criteria == "")
            {
                Session["Month"] = ddlMonth.SelectedItem.Value;
                criteria = ddlMonth.SelectedItem.Value;
            }
            else
            {
                criteria += "," + ddlMonth.SelectedItem.Value;
                Session["Month"] = ddlMonth.SelectedItem.Value;
            }
        }
       
        return criteria;
    }
    #endregion
    private void GeneratePDF()
    {
        int columnsCount = loadTeacherGridView.HeaderRow.Cells.Count;
        PdfPTable pdfTable = new PdfPTable(columnsCount);
        foreach (TableCell gridViewHeaderCell in loadTeacherGridView.HeaderRow.Cells)
        {
            PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));
            pdfTable.AddCell(pdfCell);
        }
        foreach (GridViewRow gridViewRow in loadTeacherGridView.Rows)
        {
            if (gridViewRow.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell gridViewCell in gridViewRow.Cells)
                {
                    PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));
                    pdfTable.AddCell(pdfCell);
                }
            }
        }
        DateTime dateTime = DateTime.Today;
        Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);
        PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
        string reportName = "Teacher Attendenc Report";
        string teacherName = ("Teacher Name: " + ddlTeacher.SelectedItem.Text);
        string monthName = ("Report Month: " + ddlMonth.SelectedItem.Text);
        pdfDocument.Open();
        pdfDocument.Add(new Paragraph(" \n"));
        pdfDocument.Add(new Paragraph("\t Date: " + dateTime.ToString("dd/mm/yyyy")));
        pdfDocument.Add(new Paragraph("\t" + monthName));
        pdfDocument.Add(new Paragraph("\t" + teacherName));
        //pdfDocument.Add(new Paragraph(centerName));
        pdfDocument.Add(new Paragraph(" \n"));
        pdfDocument.Add(new Paragraph("\t" + reportName));
        pdfDocument.Add(new Paragraph(" \n\n"));

        pdfDocument.Add(pdfTable);
        pdfDocument.Close();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("content-disposition",
            "attachment;filename=Attendence.pdf");
        Response.Write(pdfDocument);
        Response.Flush();
        Response.End();
    }
    public int serialNo = 0;

    protected void loadTeacherGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (serialNo += 1).ToString();
            e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Date", "{0:dd/MM/yyyy}").ToString();
            e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "InTime").ToString();
            e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "OutTime").ToString();

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "";
            e.Row.Cells[2].Text = "";
            e.Row.Cells[3].Text = "";

        }
    }
    protected void searchButton_Click(object sender, EventArgs e)
    {
        clear();
        if (ddlTeacher.SelectedIndex == 0)
        {
            MessageController.Show("Please select teacher!!!", MessageType.Warning, Page);
        }
        else
        {
            int teacherID = Convert.ToInt32(ddlTeacher.SelectedItem.Value);
            var year = ddlYear.SelectedItem.Text;
            var month = ddlMonth.SelectedItem.Value;

            DataTable TeacherAddendence = obj.GetTeacherAttendenceByTeacherID(teacherID, year, month);

            //if (TeacherAddendence.Rows.Count == 0)
            //{
            //    TeacherAddendence.Rows.Add();
            //}
            if (TeacherAddendence.Rows.Count > 0)
            {
                loadTeacherGridView.DataSource = TeacherAddendence;
                loadTeacherGridView.DataBind();
            }
            else
            {
                MessageController.Show("No Data Found!!!", MessageType.Information, Page);
            }
        }

    }

    void clear()
    {
        loadTeacherGridView.DataSource = null;
        loadTeacherGridView.DataBind();
    }

}