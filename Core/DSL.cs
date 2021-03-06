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
        #region Funções de manipulação
        public void Wait(int time) => Thread.Sleep(time);
        public void ClearData(string element) => driver.FindElement(By.XPath(element)).Clear();
        public void ClickOut() => driver.FindElement(By.XPath("//html")).Click();
        public void WaitElement(string element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
            wait.Until((d) => { return d.FindElement(By.XPath(element)); });
        }
        public void WaitElementGone(string element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
            wait.Until(d => d.FindElements(By.XPath(element)).Count == 0);
        }
        public string ValidaStep(string msgOk, string msgError)
        {
            if (testPassed)
            { return Log(msgOk + "<br>"); }
            else { return Log("<font color = red>" + msgError + testNok); }
        }
        public bool ValidaElementoExistente(string xPath)
        {
            try
            { driver.FindElement(By.XPath(xPath)); return true; }
            catch (NoSuchElementException) { return false; }
        }
        public string EndFile() { if (!testPassed) { Assert.Fail(); } return Log(testOk); }
        #endregion

        #region Funções de interação
        public void ClicaElemento(string element)
        {
            if (!testPassed) Assert.Fail();
            try { driver.FindElement(By.XPath(element)).Click(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); testPassed = false; }
        }
        public void EscreveTexto(string element, string value)
        {
            if (!testPassed) Assert.Fail();
            try { driver.FindElement(By.XPath(element)).SendKeys(value); }
            catch (Exception ex) { Console.WriteLine(ex.Message); testPassed = false; }
        }
        public void ValidaDados(string xPath, string value)
        {
            if (!testPassed) Assert.Fail();
            try { Assert.IsTrue(driver.FindElement(By.XPath(xPath)).Text.Contains(value)); }
            catch (Exception ex) { Console.WriteLine(ex.Message); testPassed = false; }
        }
        public void MenuDropDown(string xPath1, string xPath2)
        {
            if (!testPassed) Assert.Fail();
            try { driver.FindElement(By.XPath(xPath1)).Click(); driver.FindElement(By.XPath(xPath2)).Click(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); testPassed = false; }
        }
        public void UploadArquivo(string input, string path)
        {
            if (!testPassed) Assert.Fail();
            try { driver.FindElement(By.XPath(input)).SendKeys(path); }
            catch (Exception ex) { Console.WriteLine(ex.Message); testPassed = false; }
        }
        public void ValidaDownload(string partialFileName)
        {
            if (!testPassed) Assert.Fail();
            try
            {
                DirectoryInfo downloadsPath = new DirectoryInfo(Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads\");
                FileInfo[] filesInFolder = downloadsPath.GetFiles(partialFileName + "*.*");

                foreach (FileInfo foundFile in filesInFolder)
                {
                    string fullFileName = foundFile.FullName;

                    for (int i = 0; i < 30; i++)
                    {
                        if (File.Exists(fullFileName)) { break; }
                        Wait(1000);
                    }
                    var length = new FileInfo(fullFileName).Length;
                    for (int i = 0; i < 60; i++)
                    {
                        Wait(1000);
                        var newLength = new FileInfo(fullFileName).Length;
                        if (newLength == length && length != 0) { break; }
                        length = newLength;
                    }
                    File.Delete(fullFileName);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); testPassed = false; }
        }
        #endregion

        #region Funções de atribuição
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
        #endregion
    }
}