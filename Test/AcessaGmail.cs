using NUnit.Framework;

namespace BaseProjectAutomation.Test
{
    class AcessaGmail : AcessaGmailPage
    {
        [Test]
        public void AcessaGmailTest()
        {
            // Define nome para arquivo log (null = class name)
            logFileName = "Acessa G-Mail";

            // Define o cabeçalho do log
            LogHeader();

            // Execução dos steps
            PreencheEmail();
            ClicaBtnProxima();

            // Fim do Teste
            EndFile();
        }
    }
}