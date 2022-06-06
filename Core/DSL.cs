using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        public string GeraNomeAleatorio()
        {
            Random rnd = new Random();
            string[] nome = File.ReadAllLines(@"C:\Projetos\BaseProjectAutomation\_TextFilesDataBase\nomes.txt");
            string[] sobrenome = File.ReadAllLines(@"C:\Projetos\BaseProjectAutomation\_TextFilesDataBase\sobrenomes.txt");
            string nomeCompleto = nome[rnd.Next(nome.Length)] + " " + sobrenome[rnd.Next(sobrenome.Length)];
            return nomeCompleto;
        }
        public string GeraEmailAleatorio()
        {
            Random rnd = new Random();
            string[] nome = File.ReadAllLines(@"C:\Projetos\BaseProjectAutomation\_TextFilesDataBase\nomes.txt");
            string[] sobrenome = File.ReadAllLines(@"C:\Projetos\BaseProjectAutomation\_TextFilesDataBase\sobrenomes.txt");
            int num = rnd.Next(1, 9999);
            string[] dominio = File.ReadAllLines(@"C:\Projetos\BaseProjectAutomation\_TextFilesDataBase\dominios.txt");
            string email = nome[rnd.Next(nome.Length)]
                + "." + sobrenome[rnd.Next(sobrenome.Length)]
                + num + dominio[rnd.Next(dominio.Length)];
            return email.ToLower();
        }
        public string GeraDataNascimento()
        {
            Random rnd = new Random();
            int dia = rnd.Next(1, 28);
            int mes = rnd.Next(1, 12);
            int ano = rnd.Next(1950, 2000);
            string data = Convert.ToString(dia).PadLeft(2, '0')
                + Convert.ToString(mes).PadLeft(2, '0') + ano;
            return data;
        }
        public string GeraCPF()
        {
            int soma = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            Random rnd = new Random();
            string cpf = rnd.Next(100000000, 999999999).ToString();
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador1[i];
            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            cpf += resto;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            cpf += resto;
            return cpf;
        }
        public string GeraCNPJ()
        {
            Random rnd = new Random();
            int n1 = rnd.Next(0, 10);
            int n2 = rnd.Next(0, 10);
            int n3 = rnd.Next(0, 10);
            int n4 = rnd.Next(0, 10);
            int n5 = rnd.Next(0, 10);
            int n6 = rnd.Next(0, 10);
            int n7 = rnd.Next(0, 10);
            int n8 = rnd.Next(0, 10);
            int n9 = 0;
            int n10 = 0;
            int n11 = 0;
            int n12 = 1;
            int Soma1 = n1 * 5 + n2 * 4 + n3 * 3 + n4 * 2 + n5 * 9 + n6 * 8 + n7 * 7 + n8 * 6 + n9 * 5 + n10 * 4 + n11 * 3 + n12 * 2;
            int DV1 = Soma1 % 11;
            if (DV1 < 2)
                DV1 = 0;
            else
                DV1 = 11 - DV1;
            int Soma2 = n1 * 6 + n2 * 5 + n3 * 4 + n4 * 3 + n5 * 2 + n6 * 9 + n7 * 8 + n8 * 7 + n9 * 6 + n10 * 5 + n11 * 4 + n12 * 3 + DV1 * 2;
            int DV2 = Soma2 % 11;
            if (DV2 < 2)
                DV2 = 0;
            else
                DV2 = 11 - DV2;
            return n1.ToString() + n2 + n3 + n4 + n5 + n6 + n7 + n8 + n9 + n10 + n11 + n12 + DV1 + DV2;
        }
        public string GeraCEP()
        {
            Random rnd = new Random();
            string[] cep = File.ReadAllLines(@"C:\Projetos\BaseProjectAutomation\_TextFilesDataBase\ceps.txt");
            string cepRnd = cep[rnd.Next(cep.Length)];
            return cepRnd;
        }


        // Funções de interação --------------------------------------------------
        public string ClicaElemento(string element, string msgOk, string msgError, int wait = 1000, string loading = null)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                driver.FindElement(By.XPath(element)).Click(); Wait(wait);
                if (loading != null) WaitElementGone("value");
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
        public string ValidaDados(string element, string value, [Optional] string msgOk, [Optional] string msgError)
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
        public string MenuDropDown(string element, string value, [Optional] string msgOk, [Optional] string msgError)
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