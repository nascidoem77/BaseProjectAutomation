using NUnit.Framework;

namespace BaseProjectAutomation.Test
{
    class ValidaCepTest : ValidaCepPage
    {
        [Test]
        public void ValidaCep()
        {
            // Define nome para arquivo log (null = class name)
            logFileName = "Valida CEP";

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