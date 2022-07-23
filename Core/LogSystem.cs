using System;
using System.IO;
using System.Text;
using OpenQA.Selenium;

namespace BaseProjectAutomation.Core
{
    class LogSystem : GlobalVariables
    {
        public string logFileName; string fileName;
        public static StringBuilder log = new StringBuilder();
        public string Log(string text)
        {
            return log.Append(text).ToString(); ;
        }
        public void SaveLog()
        {
            string htmlStart = "<html><body><p><font face = Verdana size = 2>";
            string htmlEnd = "</p></font></body></html>";
            string header = "<b>ACESSO AO SISTEMA<br>==============</b><br>" +
                "Abriu o navegador e acessou o <b>NOME DO SISETMA</b><p>";

            if (logFileName != null) { fileName = logFileName; }
            else { fileName = GetType().Name; }

            string folderDate = DateTime.Now.ToString("yyyy-MM-dd");
            string filePath = @"C:\Projetos\BaseProjectAutomation\_Logs\" + folderDate + "\\";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            Screenshot imgCaptured = ((ITakesScreenshot)driver).GetScreenshot();
            string image = "<img src='data:/image/png;base64, " + imgCaptured + "'width='972' height='435'/>";
            string file = filePath + fileName + ".html";
            StreamWriter sw = new StreamWriter(file);
            if (!testPassed)
            {
                string logResult = htmlStart + header + log + image + htmlEnd.ToString();
                sw.Write(logResult); sw.Close(); testPassed = true;
            }
            else
            {
                string logResult = htmlStart + header + log + htmlEnd.ToString();
                sw.Write(logResult); sw.Close();
            }
        }
    }
}