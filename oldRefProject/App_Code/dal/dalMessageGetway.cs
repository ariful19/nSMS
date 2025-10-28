using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalMessageGetway
/// </summary>
public class dalMessageGetway
{
    public void SendMessage(string number, string messageBody)
    {
        string successMessage = string.Empty;
        try
        {
            successMessage = new MessageGateway().SendSms(number, messageBody);

            //set message log data for success or failed.
            MessageLog messageLog = new MessageLog();
            messageLog.MobileNumber = number;
            messageLog.MessageBody = messageBody;

            if (successMessage.Contains("Not A valid Number"))
            {
                messageLog.Status = false ;
            }
            else
            {
                messageLog.Status = true ;
            }
        }
        catch (Exception)
        {

        }
    }
    public void SendMessage(List<string> lstMobileNumber, string messageBody)
    {
        try
        {
            foreach (string number in lstMobileNumber)
            {
                new MessageGateway().SendSms(number, messageBody);
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}