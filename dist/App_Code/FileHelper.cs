using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SQLite;

/// <summary>
/// Summary description for FileHelper
/// </summary>
public class FileHelper
{
    private string _strFilePath = "";
    private bool enableLog = true;
    private SQLiteConnection m_dbConnection;

    public FileHelper(string strPath, bool isDB)
    {
        _strFilePath = strPath;

        if (!isDB)
            return;

        if (!File.Exists(strPath))
        {
            SQLiteConnection.CreateFile(strPath);
            string connectionString = "Data Source=" + strPath + ";Version=3;";
            m_dbConnection = new SQLiteConnection(connectionString);
            try
            {
                m_dbConnection.Open();
                string sql = "create table record (content text)";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                int number = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            string connectionString = "Data Source=" + strPath + ";Version=3;";
            m_dbConnection = new SQLiteConnection(connectionString);
            try
            {
                m_dbConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public void Dispose()
    {
        if (m_dbConnection != null)
        {
            m_dbConnection.Close();
        }
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

    public void WriteDB(string content)
    {
        if (m_dbConnection != null)
        {
            string sql = "insert into record (content) values ('" + content + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            int number = command.ExecuteNonQuery();
        }
    }
}