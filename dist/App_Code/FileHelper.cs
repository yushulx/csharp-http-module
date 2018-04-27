using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for FileHelper
/// </summary>
public class FileHelper
{
    private string _strFilePath = "";
    private bool enableLog = true;

    public FileHelper(string strPath)
    {
        _strFilePath = strPath;
    }

    public void Write(string strMessage)
    {
        FileStream fw = null;
        StreamWriter sw = null;

        try
        {
            fw = new FileStream(this._strFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            sw = new StreamWriter(fw);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.Write(strMessage + "\r\n");
            sw.Flush();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }

            if (fw != null)
            {
                fw.Close();
            }
        }
    }

    public void WriteLog(string strLog)
    {
        if (!enableLog)
        {
            return;
        }

        try
        {
            string strLogs = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]  " + strLog + "\r\n";
            Write(strLogs);
        }
        catch (Exception)
        { }
    }
}