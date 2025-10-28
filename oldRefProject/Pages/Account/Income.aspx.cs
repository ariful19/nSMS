using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_Income : BasePage
{
    dalAccount objAccount = new dalAccount();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {          
            LoadIncomeCategory();
            LoadIncome();
            tbxDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        } 
    }
    protected void LoadIncomeCategory()
    {
        DataTable dt = new Common().GetAll("acc_IncomeCategory");
        ddlIncome.DataSource = dt;
        ddlIncome.DataBind();
    }

    private void ResetAllControls()
    {
        tbxAmount.Text = string.Empty;
        tbxDate.Text = string.Empty;
        pdfHolder.Visible = false;
    }
    protected void LoadIncome()
    {
        DataTable dt = objAccount.GetIncome();
        rpt.DataSource = dt;
        rpt.DataBind();
    }
    protected bool ValidImage(FileUpload file, out string fileExtension)
    {
        fileExtension = string.Empty;
        bool flag = false;
        string extension = Path.GetExtension(file.FileName).ToLower();
        fileExtension = extension;
        if (extension == ".jpg" || extension == ".pdf" || extension == ".png") ////extension == "jpeg" ||  extension == ".png" ||
        {
            if (file.PostedFile.ContentLength < 6218595)
            {
                flag = true;
            }
        }
        return flag;
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        btnSave.Visible = false;
        btnEdit.Visible = true;
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = objAccount.GetIncomeById(ID);
        if (dt.Rows.Count > 0)
        {

            string fileName = dt.Rows[0]["Attachment"].ToString();
            Session["UpdatedFileName"] = fileName;



            string extension = Path.GetExtension(dt.Rows[0]["Attachment"].ToString()).ToLower();
            string location = Server.MapPath("~/Images/Attachment/" + dt.Rows[0]["Attachment"].ToString());
            if (extension != ".pdf")
            {
                tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToString("dd/MM/yyyy");
                tbxAmount.Text = dt.Rows[0]["Amount"].ToString();
                tbxNote.Text = dt.Rows[0]["Details"].ToString();
                ddlIncome.SelectedValue = dt.Rows[0]["IncomeCategoryId"].ToString();
                imgAttachment.ImageUrl = "~/Images/Attachment/" + dt.Rows[0]["Attachment"].ToString();
                imgAttachment.Visible = true;
            }
            else
            {

                tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToString("dd/MM/yyyy");
                tbxAmount.Text = dt.Rows[0]["Amount"].ToString();
                tbxNote.Text = dt.Rows[0]["Details"].ToString();
                ddlIncome.SelectedValue = dt.Rows[0]["IncomeCategoryId"].ToString();
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"600px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}&download=1\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                pdfContent.Text = string.Format(embed, ResolveUrl("~/pdfHandler.ashx?FileName=" + fileName + " &Location=" + location));
                pdfHolder.Visible = true;
                imgAttachment.Visible = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string attachment = "";

        if (attachmentUpload.HasFile)
        {
            if (Session["UpdatedFileName"] != null)
            {
                string updatedFileName = (string)Session["UpdatedFileName"];
                System.IO.File.Delete(Server.MapPath("~/Images/Attachment/" + updatedFileName));
            }

            string extension = string.Empty;
            if (ValidImage(attachmentUpload, out extension))
            {


                attachment = Guid.NewGuid() + extension.ToString();
                if (extension == ".pdf")
                {
                    attachmentUpload.SaveAs(Server.MapPath("~/Images/Attachment/" + attachment));
                }
                else
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(attachmentUpload.FileContent);
                    System.Drawing.Image image2 = Controller.resizeImage(image, new Size(700, 750));
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);
                    string MediumImagePath = Server.MapPath("~/Images/Attachment/" + attachment);
                    image2.Save(string.Concat(MediumImagePath), ImageCodecInfo.GetImageEncoders()[1], encoderParameters);
                }
            }

            else
            {
                MessageController.Show("Select.jpg or.pdf file less than 6 mb", MessageType.Error, Page);
                return;
            }
        }

        DateTime incomeDate = DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null);
        if (ID > 0)
        {
            if (!attachmentUpload.HasFile && Session["UpdatedFileName"] != null)
            {
                attachment = (string)Session["UpdatedFileName"];
            }
            objAccount.IncomeUpdate(ID, Convert.ToInt32(ddlIncome.SelectedValue), incomeDate, Convert.ToDouble(tbxAmount.Text), attachment, tbxNote.Text, Page.User.Identity.Name, DateTime.Now);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        else
        {
            objAccount.IncomeInsert(Convert.ToInt32(ddlIncome.SelectedValue), incomeDate, Convert.ToDouble(tbxAmount.Text), attachment, tbxNote.Text, Page.User.Identity.Name, DateTime.Now);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        ResetAllControls();
        LoadIncome();
        tbxDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
    }
    protected void btnIncomeCategory_Click(object sender, EventArgs e)
    {
        ID = objAccount.InsertIncomeCategory(tbxName.Text);
        if (ID != -1)
        {
            tbxName.Text = "";
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This category already exists. Please try anothor.", MessageType.Error, Page);
        LoadIncomeCategory();
    }


    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("acc_Income", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        LoadIncome();
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

    //protected void tbxDate_TextChanged(object sender, EventArgs e)
    //{
    //    if (DateTime.ParseExact(tbxDate.Text, "dd/MM/yyyy", null) > DateTime.Now)
    //    {
    //        MessageController.Show("Selected date must be smaller then current date.", MessageType.Error, Page);
    //        return;
    //    }
    //    else
    //    {
    //        //DataTable dt = obj.GetByDate(Convert.ToDateTime(tbxDate.Text));
    //        //rptTeacher.DataSource = dt;
    //        //rptTeacher.DataBind();
    //    }
    //}

}