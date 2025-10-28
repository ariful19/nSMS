<%@ WebHandler Language="C#" Class="pdfHandler" %>

 
using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public class pdfHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string fileName = context.Request.QueryString["FileName"];
        string location = context.Request.QueryString["Location"];

        context.Response.Buffer = true;
        context.Response.Charset = "";
        if (context.Request.QueryString["download"] == "1")
        {
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        }

        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "application/pdf";
        if (System.IO.File.Exists(location))
        {
            byte[] fileByte = System.IO.File.ReadAllBytes(location);
            context.Response.BinaryWrite(fileByte);
        }
        context.Response.Flush();
        context.Response.End();

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}