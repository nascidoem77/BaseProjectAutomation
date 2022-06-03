using OpenQA.Selenium;

namespace ProjectBase.Core
{
    public class GlobalVariables
    {

        // Define 'driver' como trigger para os WebElements
        public static IWebDriver driver;

        // Define 'true' como padrão para o fluxo dos testes
        public static bool testPassed = true;

        // Define 'Fechar navegador ao final do teste' como padrão
        public bool driverQuit = true;

        // Gera log em formato HTML
        public bool saveLog = true;

        // Determina OK ~ NOK ao final do teste
        public string testOk = "<p><b>=============<br>FIM DO TESTE – OK!";
        public string testNok = "<p><font color = black><b>==============<br>FIM DO TESTE – <font color = red>NOK!<p align=center>";
    }
}