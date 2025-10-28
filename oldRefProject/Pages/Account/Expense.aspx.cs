using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_Expense : BasePage
{
    dalAccount objAccount = new dalAccount();
    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadExpenseCategory();
            LoadExpense();
            tbxDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }
    }
    protected void LoadExpenseCategory()
    {
        DataTable dt = new Common().GetAll("acc_ExpenseCategory");
        ddlExpense.DataSource = dt;
        ddlExpense.DataBind();
    }
    protected void LoadExpense()
    {
        DataTable dt = objAccount.GetExpense();
        rpt.DataSource = dt;
        rpt.DataBind();
    }
    protected bool ValidImage(FileUpload file, out string fileExtension)
    {
        fileExtension = string.Empty;
        bool flag = false;
        string extension = Path.GetExtension(file.FileName).ToLower();
        fileExtension = extension;
        if (extension == ".pdf" || extension == ".png"|| extension == ".jpg")  ////extension == "jpeg" ||extension == ".png" ||
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
        DataTable dt = objAccount.GetExpenseById(ID);
        if (dt.Rows.Count > 0)
        {
           
            tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["ExpenseDate"]).ToString("dd/MM/yyyy");
            tbxAmount.Text = dt.Rows[0]["Amount"].ToString();
            tbxNote.Text = dt.Rows[0]["Purpose"].ToString();
            ddlExpense.SelectedValue = dt.Rows[0]["ExpenseCategoryId"].ToString();
            string fileName = dt.Rows[0]["Attachment"].ToString();
            string extension = Path.GetExtension(dt.Rows[0]["Attachment"].ToString()).ToLower();
            string location = Server.MapPath("~/Images/Attachment/" + dt.Rows[0]["Attachment"].ToString());
            if (extension != ".pdf")
            {
                

                imgAttachment.ImageUrl = "~/Images/Attachment/" + dt.Rows[0]["Attachment"].ToString();
                imgAttachment.Visible = true;
                pdfHolder.Visible = false;
            }
            else
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"600px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}&download=1\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                pdfContent.Text = string.Format(embed, ResolveUrl("~/pdfHandler.ashx?FileName=" + fileName + " &Loacaton=" + location));
                pdfHolder.Visible = true;
                imgAttachment.Visible = false;
            }
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("acc_Expense", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        LoadExpense();
    } 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string attachment = "";
        if (attachmentUpload.HasFile)
        {
            string extension = string.Empty;
            if (ValidImage(attachmentUpload, out extension) == true)
            {
                attachment = Guid.NewGuid() + "-" + attachmentUpload.FileName;
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
                MessageController.Show("Select.jpg /.pdf or .png file less than 6 mb", MessageType.Error, Page);
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
            objAccount.ExpenseUpdate(ID, Convert.ToInt32(ddlExpense.SelectedValue), incomeDate, Convert.ToDouble(tbxAmount.Text), attachment, tbxNote.Text, Page.User.Identity.Name, DateTime.Now);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        }
        else
        {
            objAccount.ExpenseInsert(Convert.ToInt32(ddlExpense.SelectedValue), incomeDate, Convert.ToDouble(tbxAmount.Text), attachment, tbxNote.Text, Page.User.Identity.Name, DateTime.Now);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        ResetAllControls();
        LoadExpense();
        tbxDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
    }
    protected void btnIncomeCategory_Click(object sender, EventArgs e)
    {
        ID = objAccount.InsertExpenseCategory(tbxName.Text);
        if (ID != -1)
        {
            tbxName.Text = "";
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This category already exists. Please try anothor.", MessageType.Error, Page);
        LoadExpenseCategory();
    }

    private void ResetAllControls()
    {
        tbxAmount.Text = string.Empty;
        tbxDate.Text = string.Empty;
        pdfHolder.Visible = false;
    }

    Regex regex = new Regex(@"[1-9]");  //@"[1-9]"
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

    //private System.Drawing.Image GetUploadedImage()
    //{
    //    string myPath;
    //    Guid myGuid;
    //    FrameDimension myDimension;
    //    ArrayList myImages = new ArrayList();
    //    int myPageCount;
    //    Bitmap myBMP;

    //    MemoryStream ms;
    //    System.Drawing.Image myImage;
    //    myGuid = Guid.NewGuid();
    //    // myPath = attachmentUpload.PostedFile.FileName;

    //    string directoryPath = Server.MapPath("TempImg");

    //    if (!System.IO.Directory.Exists(directoryPath))
    //    {
    //        System.IO.Directory.CreateDirectory(directoryPath);
    //    }
    //    string savedFileName = Server.MapPath("TempImg//" + attachmentUpload.FileName);
    //    attachmentUpload.SaveAs(savedFileName);


    //    FileStream fs = new FileStream(savedFileName, FileMode.Open);
    //    myImage = System.Drawing.Image.FromStream(fs);
    //    // myGuid = myGuid + "-" + myImage.FrameDimensionsList[0].ToString();
    //    myDimension = new FrameDimension(myGuid);
    //    myPageCount = myImage.GetFrameCount(myDimension);
    //    for (int i = 0; i < myPageCount; i++)
    //    {
    //        ms = new MemoryStream();
    //        myImage.SelectActiveFrame(myDimension, i);
    //        myImage.Save(ms, ImageFormat.Bmp);
    //        myBMP = new Bitmap(ms);
    //        myImages.Add(myBMP);
    //        ms.Close();
    //    }
    //    fs.Close();



    //    System.Drawing.Image returnImage = null;

    //    if (ValidImage(attachmentUpload))
    //    {
    //        //string directoryPath = Server.MapPath("TempImg");

    //        //if (!System.IO.Directory.Exists(directoryPath))
    //        //{
    //        //    System.IO.Directory.CreateDirectory(directoryPath);
    //        //}
    //        //string savedFileName = Server.MapPath("TempImg//" + attachmentUpload.FileName);
    //        //attachmentUpload.SaveAs(savedFileName);


    //        //  System.Drawing.Image image = System.Drawing.Image.FromStream(attachmentUpload.PostedFile.InputStream);

    //        //using (MemoryStream ms = new MemoryStream(attachmentUpload.FileBytes))
    //        //{
    //        //    returnImage = System.Drawing.Image.FromStream(ms);
    //        //}
    //        return returnImage;
    //    }

    //    return returnImage;
    //}

}