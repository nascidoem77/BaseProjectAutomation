using BaseProjectAutomation.Core;

namespace BaseProjectAutomation.Test
{
    class ValidaCepPage : Begin
    {
        public string LogHeader()
        {
            return Log("<b>TESTE:</b> Valida CEP<br><b>===========</b><p>");
        }
        public void PreencheCEP()
        {
            string cep = "30492040";
            EscreveTexto("//*[@id='endereco']", cep);
            ValidaStep("Preencheu CEP " + cep,
                "Erro ao preencher CEP " + cep);
        }
        public void ClicaBtnBuscar()
        {
            ClicaElemento("//*[contains(text(),'Buscar')]");
            ValidaStep("Clicou no botão Buscar",
                "Erro ao clicar no botão Buscar");
        }
        public void ValidaLogradouro()
        {
            string logradouro = "Rua Ernani Agricola";
            ValidaDados("//*[contains(text(),'Rua Ernani Agricola')]", logradouro);
            ValidaStep("Validou o logradouro " + logradouro,
                "Erro ao validar o logradouro " + logradouro);
        }
    }
}