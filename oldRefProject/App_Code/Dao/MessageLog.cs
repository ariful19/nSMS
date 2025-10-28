using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MessageLog
/// </summary>
public class MessageLog
{
    public int MessageLogID { get; set; }
    public DateTime SendDate { get; set; }
    public string MobileNumber { get; set; }
    public string MessageBody { get; set; }
    public bool Status { get; set; }
    public string Sender { get; set; }

    public string MessageType { get; set; }
}