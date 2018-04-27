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

        logFile = Path.Combine(dir, "log.txt");
        mFileHelper = new FileHelper(logFile);

        context.LogRequest += new EventHandler(OnLogRequest);
    }

    public void OnLogRequest(Object source, EventArgs e)
    {
        HttpApplication application = (HttpApplication)source;
        HttpContext context = application.Context;
        string url = context.Request.Url.AbsoluteUri.ToLower();

        if (url.Contains("dynamsoft.webtwain.min.js"))
        {
            mFileHelper.WriteLog(context.Request.UserHostAddress);
        }
    }
}