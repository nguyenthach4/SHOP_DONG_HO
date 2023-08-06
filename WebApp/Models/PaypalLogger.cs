using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PaypalLogger
    {
        public static string LogDirectoryPath = Environment.CurrentDirectory;

        public static void Log(string message)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(LogDirectoryPath + "\\PaypalError.log", true);
                streamWriter.WriteLine("{0}--->{1}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}