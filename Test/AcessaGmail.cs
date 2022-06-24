using NUnit.Framework;
using BaseProjectAutomation.Core;

namespace BaseProjectAutomation.Test
{
    class AcessaGmail : Begin
    {
        [Test]
        public void AcessaGmailTest()
        {
            var page = new AcessaGmailPage();

            // Define nome para arquivo log (null = class name)
            logFileName = "Acessa G-Mail";

            // Define o cabeçalho do log
            page.LogHeader();

            // Execução dos steps
            page.PreencheEmail();
            page.ClicaBtnProxima();

            // Fim do Teste
            EndFile();
        }
    }
}