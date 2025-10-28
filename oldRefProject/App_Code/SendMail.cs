using System;
using System.Data;
using System.Net;
using System.Net.Mail;
public class SendEmail
{
    public SendEmail()
    {
    }
    public static bool SendSimpleMail(string mailTo, string emailSubject, string emailBody)
    {
        try
        {
            if (mailTo.Contains(";"))
            {
                string[] tos = mailTo.Split(';');
                if (tos.Length > 0)
                {
                    for (int i = 0; i < tos.Length; i++)
                    {
                        Send(tos[i].ToString(), emailSubject, emailBody);
                    }
                }
            }
            else
            {
                Send(mailTo, emailSubject, emailBody);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool SendSimpleMail(string EmailFrom, string EmailTo, string emailSubject, string emailBody)
    {
        return SendSimpleMail(EmailTo, emailSubject, emailBody);
    }
    private static void Send(string mailTo, string emailSubject, string emailBody)
    {
        SmtpClient objClient;
        MailMessage objMessage;
        MailAddress From;
        DataTable dt = new Common().GetAll("EmailConfig");
        if (dt.Rows.Count == 0) return;
        From = new MailAddress(dt.Rows[0]["DisplayEmail"].ToString(), dt.Rows[0]["DisplayName"].ToString());
        if (mailTo == "")
            mailTo = dt.Rows[0]["ReplyToEmail"].ToString();
        MailAddress To = new MailAddress(mailTo);
        objMessage = new MailMessage(From, To);
        objMessage.ReplyToList.Add(dt.Rows[0]["ReplyToEmail"].ToString()); //
        objMessage.IsBodyHtml = true;
        objMessage.Subject = emailSubject;
        objMessage.Body = emailBody;
        objMessage.Priority = MailPriority.Normal;
        objClient = new SmtpClient();
        objClient.Port = Convert.ToInt32(dt.Rows[0]["Port"].ToString());
        objClient.EnableSsl = Convert.ToBoolean(dt.Rows[0]["SSL"].ToString()); // SSL should be false
        objClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        objClient.UseDefaultCredentials = Convert.ToBoolean(dt.Rows[0]["Authentication"].ToString());
        objClient.Host = dt.Rows[0]["SMTPServer"].ToString(); // mail host or server 
        if (Convert.ToBoolean(dt.Rows[0]["Authentication"].ToString()))
            objClient.Credentials = new NetworkCredential(dt.Rows[0]["UserName"].ToString(),
                dt.Rows[0]["Password"].ToString());
        objClient.Send(objMessage);
    }
}
