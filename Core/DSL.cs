using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace BaseProjectAutomation.Core
{
    class DSL : LogSystem
    {
        // Funções de interação ------------------------
        public void Wait(int time) => Thread.Sleep(time);

        public void WaitElement(string element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
            Wait(1000); wait.Until((d) => { return d.FindElement(By.XPath(element)); }); Wait(2000);
        }

        public void WaitElementGone(string element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
            Wait(1000); wait.Until(d => d.FindElements(By.XPath(element)).Count == 0); Wait(2000);
        }

        public void ClearData(string element) => driver.FindElement(By.XPath(element)).Clear();

        public void ClickOut() => driver.FindElement(By.XPath("//html")).Click();

        public string EndFile()
        {
            if (!testPassed) { Assert.Fail(); }
            return testOk;
        }


        // Funções de atribuição ------------------------------------
        public string CapturaDadosBy(string element, string attribute)
        {
            string value = driver.FindElement(By.XPath(element)).GetAttribute(attribute).Trim();
            return value;
        }
        public string GeraStringPorTamanho(int times, string value)
        {
            string s = string.Empty;
            for (int i = 0; i < times; i++)
                s = string.Concat(s, value);
            return s;
        }
        public string GeraStringNumericaAleatoria(int size)
        {
            var chars = "123456789";
            var rnd = new Random();
            var result = new string(
                Enumerable.Repeat(chars, size)
                .Select(s => s[rnd.Next(s.Length)])
                .ToArray()); Wait(500);
            return result;
        }
        public string GeraStringAlfanumericoAleatorio(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rnd = new Random();
            var result = new string(
                Enumerable.Repeat(chars, size)
                .Select(s => s[rnd.Next(s.Length)])
                .ToArray());
            return result;
        }
        public string CapturaUrlAtual()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var url = js.ExecuteScript("return document.URL");
            return url.ToString();
        }


        // Funções de interação --------------------------------------------------
        public string ClicaElemento(string element, string msgOk, string msgError)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                driver.FindElement(By.XPath(element)).Click();
                return msgOk + "<br>";
            }
            catch
            {
                testPassed = false;
                return "<font color = red>" + msgError + testNok;
            }
        }
        public string EscreveTexto(string element, string value, string msgOk, string msgError)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                driver.FindElement(By.XPath(element)).SendKeys(value);
                return msgOk + "<br>";
            }
            catch
            {
                testPassed = false;
                return "<font color = red>" + msgError + testNok;
            }
        }
        public string ValidaDados(string element, string value, string msgOk, string msgError)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                Assert.IsTrue(driver.FindElement(By.XPath(element)).Text.Contains(value));
                return msgOk + "<br>";
            }
            catch
            {
                testPassed = false;
                return "<font color = red>" + msgError + testNok;
            }
        }
        public string MenuDropDown(string element, string value, string msgOk, string msgError)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                driver.FindElement(By.XPath(element)).Click(); Wait(500);
                driver.FindElement(By.XPath(value)).Click(); Wait(750);
                return msgOk + "<br>";
            }
            catch
            {
                testPassed = false;
                return "<font color = red>" + msgError + testNok;
            }
        }
        public string UploadArquivo(string input, string path, string msgOk, string msgError)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                driver.FindElement(By.XPath(input)).SendKeys(path);
                return msgOk + "<br>";
            }
            catch
            {
                testPassed = false;
                return "<font color = red>" + msgError + testNok;
            }
        }
        public string DownloadArquivo(string fileName, string msgOk, string msgError)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                string downloadsPath = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads\";
                string downloadedFile = downloadsPath + fileName;
                for (int i = 0; i < 30; i++)
                {
                    if (File.Exists(downloadedFile)) { break; }
                    Wait(1000);
                }
                var length = new FileInfo(downloadedFile).Length;
                for (int i = 0; i < 60; i++)
                {
                    Wait(1000);
                    var newLength = new FileInfo(downloadedFile).Length;
                    if (newLength == length && length != 0) { break; }
                    length = newLength;
                }
                File.Delete(downloadedFile);
                return msgOk + "<br>";
            }
            catch
            {
                testPassed = false;
                return "<font color = red>" + msgError + testNok;
            }
        }
    }
}