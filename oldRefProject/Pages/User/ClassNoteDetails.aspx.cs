using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class Pages_User_ClassNoteDetails : BasePage
{
    dalNotes obj = new dalNotes();
    public static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            logoDiv.Visible = false;
            if (Request.QueryString["ID"] != null)
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]);
                //LoadNews();
                LoadNotes();
            }
        }
    }

    private void LoadNotes()
    {
        DataTable dt = new DataTable();
        string criteria = "bs_Notes.Id=" + ID;
        
            if (Request.Cookies["CurrentLanguage"].Value == "bn-BD")
            {
                dt = obj.GetBanglaNotes(criteria);

            }
            else
            {
                dt = obj.GetByCriteria(criteria);

            }

        if (dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["Title"].ToString();
            litDetails.Text = dt.Rows[0]["Details"].ToString();
        }
        else
        {
            lblTitle.Text ="";
            litDetails.Text = "";
        }
    }

    protected void LoadNewsBangla()
    {
        DataTable dt = obj.GetByIdBangla(ID);
        if (dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["Title"].ToString();
            litDetails.Text = dt.Rows[0]["Details"].ToString();
        }
    }
    protected void LoadNewsEnglaish()
    {
        DataTable dt = obj.GetByIdEnglish(ID);
        if (dt.Rows.Count > 0)
        {
            lblTitle.Text = dt.Rows[0]["Title"].ToString();
            litDetails.Text = dt.Rows[0]["Details"].ToString();
        }
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        logoDiv.Visible = true;

        StringWriter sw = new StringWriter();
        HtmlTextWriter w = new HtmlTextWriter(sw);
        HtmlForm f = new HtmlForm();
        //pdf.Controls.Remove(CalendarExtender1);
        //pdf.Controls.Remove(CalendarExtender2);
        //pdf.Controls.Remove(CalendarExtender3);
        this.Controls.Add(f);
        f.Controls.Add(pdf);
        f.RenderControl(w);


        var sbhtml = sw.GetStringBuilder();

        sw.Close();
        w.Close();
        sbhtml.Insert(0, @"<!DOCTYPE html><html><head><title>HTML5</title>    <meta charset='utf-8'/><script src='jquery-3.1.0.js'></script>    <link href='Styles/bootstrap.min.css' rel='stylesheet' /><link href='Styles/ApplicantsView.css' rel='stylesheet' /> <link href='Styles/Custom.css' rel='stylesheet' />  <script src='Scripts/jquery-2.1.3.min.js'></script>   <script src='Scripts/bootstrap.min.js'></script>
<script>
window.onload = function () {
    var tbls = document.getElementsByTagName('table');
    for (var i = 0; i < tbls.length; i++) {
        var tbl = tbls.item(i);
        var rows = tbl.querySelectorAll('tr');
        var lastRow = rows.item(rows.length - 1);
        lastRow.remove();
        var rows = tbl.querySelectorAll('tr');
        for (var j = 0; j < rows.length; j++) {
            var r = rows.item(j);
            var lastColumn = r.children.item(r.children.length - 1);
            lastColumn.remove();
        }
    }
};
</script>
</head><body>");

        sbhtml.Append("</body></html>");
        sbhtml.Replace("../../", "");
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\cpaa.html";

        var str = Regex.Replace(sbhtml.ToString(), "<button.*</button>", "");
        str = Regex.Replace(str, "<input.*type=\"submit\".*>", "");
        str = Regex.Replace(str, "<a.*</a>", "");
        System.IO.File.WriteAllText(path, str, System.Text.Encoding.UTF8);
        var uri = new System.Uri(path);
        var converted = uri.AbsoluteUri;

        SelectPdf.HtmlToPdf cnv = new SelectPdf.HtmlToPdf();
        cnv.Options.AutoFitHeight = SelectPdf.HtmlToPdfPageFitMode.AutoFit;
        cnv.Options.MarginTop = 30;
        cnv.Options.MarginLeft = 50;
        cnv.Options.MarginRight = 50;
        var doc = cnv.ConvertUrl(converted);
        doc.Save(Response, false, "sth.pdf");
        doc.Close();
    }
  
}