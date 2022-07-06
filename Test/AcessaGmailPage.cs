using BaseProjectAutomation.Core;

namespace BaseProjectAutomation.Test
{
    class AcessaGmailPage : Begin
    {
        public string LogHeader()
        {
            return Log("<b>TESTE:</b> Acessa G-Mail<br><b>=============</b><p>");
        }
        public void PreencheEmail()
        {
            string email = "ricardo.bornin77@gmail.com";
            EscreveTexto("//*[@id='identifierId']", email);
            ValidaStep("Preencheu e-mail " + email,
                "Erro ao preencher e-mail " + email);
        }
        public void ClicaBtnProxima()
        {
            ClicaElemento("//*[contains(text(),'Próxima')]");
            ValidaStep("Clicou no botão Próxima",
                "Erro ao clicar no botão Próxima");
        }
    }
}