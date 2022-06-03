using NUnit.Framework;
using ProjectBase.Core;

namespace ProjectBase.Test
{
    class AcessaGmail : Begin
    {
        [Test]
        public void AcessaGmailTest()
        {
            var page = new AcessaGmailPage();

            // Define o cabeçalho do log
            Log(page.LogHeader());

            // Execução dos steps
            Log(page.PreencheEmail());
            Log(page.ClicaBtnProxima());

            // Fim do Teste
            Log(EndFile());
        }
    }
}