using ProjectBase.Core;

namespace ProjectBase.Test
{
    class AcessaGmailPage : Begin
    {
        public string LogHeader()
        {
            return "<b>TESTE:</b> Acessa G-Mail<br><b>==============</b><p>";
        }
        public string PreencheEmail()
        {
            string email = "ricardo.bornin77@gmail.com";
            return EscreveTexto(
                    "//*[@id='identifierId']",
                    email,
                    "Preencheu o e-mail " + email,
                    "Erro ao preencher o e-mail " + email
                    );
        }
        public string ClicaBtnProxima()
        {
            return ClicaElemento(
                "//*[contains(text(),'Próxima')]",
                "Clicou no botão Próxima",
                "Erro ao clicar no botão Próxima"
                );
        }
    }
}