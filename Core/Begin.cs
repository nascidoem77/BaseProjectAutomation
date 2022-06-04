using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace BaseProjectAutomation.Core
{
    class Begin : DSL
    {
        // Executar os testes em modo "headless"
        public bool headlessTest = false;

        // Abre navegador com argumentos pré-definidos
        public void AbreNavegador()
        {
            ChromeOptions headlessMode = new ChromeOptions();
            headlessMode.AddArgument("--window-size=1366x768");
            headlessMode.AddArgument("--disk-cache-size=0");
            headlessMode.AddArgument("--headless");

            ChromeOptions devMode = new ChromeOptions();
            devMode.AddArgument("--disk-cache-size=0");
            devMode.AddArgument("--start-maximized");

            if (headlessTest) { driver = new ChromeDriver(headlessMode); }
            else { driver = new ChromeDriver(devMode); driverQuit = false; }
        }

        [SetUp]
        public void StartTest()
        {
            AbreNavegador();
            driver.Navigate().GoToUrl("http://www.gmail.com.br");
        }

        [TearDown]
        public void EndTest()
        {
            // Gera log em formato HTML
            if (saveLog) { SaveLog(); }

            // Fecha navegador após teste
            if (driverQuit) { driver.Quit(); }
        }
    }
}