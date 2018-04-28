using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for HttpModuleManager
/// </summary>
public class HttpModuleManager : IHttpModule
{
    private FileHelper mFileHelper;

    public void Dispose()
    {
        
    }

    public void Init(HttpApplication context)
    {
        string dir = Path.Combine(context.Context.Server.MapPath("."), "data");
        string logFile = null;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Write record to text file.
        //logFile = Path.Combine(dir, "log.txt");
        //mFileHelper = new FileHelper(logFile, false);

        // Write record to db file.
        logFile = Path.Combine(dir, "record.db");
        if (mFileHelper == null)
            mFileHelper = new FileHelper(logFile, true);

        context.LogRequest += new EventHandler(OnLogRequest);
    }

    public void OnLogRequest(Object source, EventArgs e)
    {
        HttpApplication application = (HttpApplication)source;
        HttpContext context = application.Context;
        string url = context.Request.Url.AbsoluteUri.ToLower();

        if (url.Contains("dynamsoft.webtwain.min.js"))
        {
            // Write record to text file.
            //mFileHelper.WriteLog(context.Request.UserHostAddress);

            // Write record to db file.
            mFileHelper.WriteDB(context.Request.UserHostAddress);
        }
    }
}