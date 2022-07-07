using NUnit.Framework;

namespace BaseProjectAutomation.Test
{
    class ValidaCepTest : ValidaCepPage
    {
        [Test]
        public void AcessaGmailTest()
        {
            // Define nome para arquivo log (null = class name)
            logFileName = "Valice CEP";

            // Define o cabeçalho do log
            LogHeader();

            // Execução dos steps
            PreencheCEP();
            ClicaBtnBuscar();
            ValidaLogradouro();

            // Fim do Teste
            EndFile();
        }
    }
}