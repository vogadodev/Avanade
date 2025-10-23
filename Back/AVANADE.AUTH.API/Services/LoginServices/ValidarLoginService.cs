using AVANADE.INFRASTRUCTURE.ServicesComum.MenssagemService;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Resourcers;

namespace AVANADE.AUTH.API.Services.LoginServices
{
    public class ValidarLoginService : MensagemService
    {
        public ValidarLoginService()
        {

        }

        public bool ValidarLogin(LoginRequestDto loginRequest)
        {
            ValidarCampoEmail(loginRequest);
            ValidarCampoSenha(loginRequest);
            return Mensagens.TemErros();
        }

        private void ValidarCampoSenha(LoginRequestDto loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Password))
                Mensagens.AdicionarErro(ComumResource.SenhaObrigatoria);
        }
        private void ValidarCampoEmail(LoginRequestDto loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Email))
                Mensagens.AdicionarErro(ComumResource.EmailObrigatorio);
        }
    }
}
