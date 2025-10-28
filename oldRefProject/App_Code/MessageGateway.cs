using System;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

public class MessageGateway
{
    public string SendSms(string mobile, string txt)
    {
        string endpoint = "";
        string userName = "";
        string password = "";
        

        DataTable dt = new Common().GetAll("bs_SMSGetway");
        if (dt.Rows.Count > 0)
        {
            endpoint = dt.Rows[0]["URL"].ToString();
            userName = dt.Rows[0]["UserName"].ToString();
            password = EncryptionDecryption.EncryptDecryptString(dt.Rows[0]["Password"].ToString());
        }
        try
        {
            if (mobile.Substring(0, 1) != "+")
            {
                switch (mobile.Length)
                {
                    case 11:
                        mobile = "88" + mobile;
                        break;
                    case 10:
                        mobile = "880" + mobile;
                        break;
                }
            }
            else
            {
                mobile = mobile.Substring(1, 13);
            }

            string requestUrl = GetPlainTextFromHtml(endpoint.Replace("@ms", mobile)).Replace("@txt", HttpUtility.UrlEncode(txt));

            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent: Other");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            if (mobile.Length == 13)
            {
                return webClient.DownloadString(requestUrl);
            }
            else
            {
                return "Not A valid Number";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private string GetPlainTextFromHtml(string htmlString)
    {
        string htmlTagPattern = "<.*?>";
        var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        htmlString = regexCss.Replace(htmlString, string.Empty);
        htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
        htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
        htmlString = htmlString.Replace("&nbsp;", string.Empty);

        return htmlString;
    }

    #region Nested type: ConnectedHandler

    private delegate void ConnectedHandler(bool connected);

    #endregion
}