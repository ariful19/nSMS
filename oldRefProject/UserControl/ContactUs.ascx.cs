using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ContactUs : System.Web.UI.UserControl
{
    dalContactInfo obj = new dalContactInfo();

    protected static int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void btn_OnClick(object sender, EventArgs e)
    {
        if (tbxName.Text != "" && tbxEmail.Text != "" && tbxMobile.Text !="" && tbxSubject.Text != "" && tbxMessage.Text != "")
        {
            ID = obj.InsertContactInfo(tbxName.Text, tbxEmail.Text, tbxMobile.Text, tbxSubject.Text, tbxMessage.Text);
        
        if (ID != -1)
        {
            ClearAll();
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        else
            MessageController.Show("This Name, Email or Mobile already exists. Please try anothor.", MessageType.Error, Page);
        ClearAll();
        }
        else
        {
            MessageController.Show("Please fill the required field!!!", MessageType.Error, Page);
        }
    }
    protected void ClearAll()
    {
        tbxName.Text = string.Empty;
        tbxEmail.Text = string.Empty;
        tbxMobile.Text = string.Empty;
        tbxSubject.Text = string.Empty;
        tbxMessage.Text = string.Empty;
    }
}