using AVANADE.INFRASTRUCTURE.ServicesComum.ServicoComMensagemService;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Resourcers;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Repositories;
using AVANADE.USUARIO.API.Data;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AVANADE.USUARIO.API.Services.UsuarioServices
{
    public class ValidarUsuarioService : MensagemService
    {
        private readonly UsuarioRepository<UsuarioDbContext> _usuarioRepository;

        public ValidarUsuarioService(UsuarioRepository<UsuarioDbContext> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<bool> ValidarUsuario(UsuarioRequestDto dto)
        {
            ValidarCampoNome(dto);
            ValidarCampoEmail(dto);           
            ValidarCampoSenha(dto);
            ValidarCampoConfirmacaoSenha(dto);
            ValidarIgualdadeDeSenha(dto);

            await ValidarSeUsuarioJaExiste(dto);

            return Mensagens.TemErros();
        }

        private void ValidarCampoNome(UsuarioRequestDto dto)
        {            
                Mensagens.AdicionarErroSe(string.IsNullOrEmpty(dto.NomeCompleto), ComumResource.NomeObrigatorio);
        }

        private void ValidarCampoEmail(UsuarioRequestDto dto)
        {
            if (!MailAddress.TryCreate(dto.Email, out _))
            {
                Mensagens.AdicionarErro(ComumResource.EmailInvalido);
            }
        }

        private void ValidarCampoSenha(UsuarioRequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.Senha))
            {
                Mensagens.AdicionarErro(ComumResource.SenhaObrigatoria);
            }
            else
            {
                var caracteresEspeciais = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$";
                bool senhaEhValida = Regex.IsMatch(dto.Senha, caracteresEspeciais);
                if (!senhaEhValida)
                {
                    Mensagens.AdicionarErro(ComumResource.SenhaInvalida);
                }
            }
        }

        private void ValidarCampoConfirmacaoSenha(UsuarioRequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.ConfirmacaoSenha))
            {
                Mensagens.AdicionarErro(ComumResource.ConfirmacaoSenha);
            }
        }

        private void ValidarIgualdadeDeSenha(UsuarioRequestDto dto)
        {
            if (!dto.Senha.Equals(dto.ConfirmacaoSenha))
            {
                Mensagens.AdicionarErro(ComumResource.ConfirmacaoSenhaDiferentes);
            }
        }

        private async Task ValidarSeUsuarioJaExiste(UsuarioRequestDto dto)
        {
            var existeUsuariorio = await _usuarioRepository.ValidarExistenciaAsync(u => u.Email == dto.Email );
            if (existeUsuariorio)
            {
                Mensagens.AdicionarErro(ComumResource.ExisteUsuario);
            }
        }
    }
}
