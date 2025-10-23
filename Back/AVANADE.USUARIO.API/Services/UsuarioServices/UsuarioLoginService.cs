using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Resourcers;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Repositories;
using AVANADE.USUARIO.API.Data;

namespace AVANADE.USUARIO.API.Services.UsuarioServices
{
    public class UsuarioLoginService : RetornoPadraoService, IServicoComBuscaPadrao
    {
        private readonly UsuarioRepository<UsuarioDbContext> _usuarioRepository;

        public UsuarioLoginService(UsuarioRepository<UsuarioDbContext> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool Encontrado { get; set; }

        public async Task RealizarLoginUsuario(UsuarioLoginRequestDto dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailComSenhaAsync(dto.email);
            if (usuario == null || !HasherPasswordServices.CheckPassword(dto.password, usuario.UsuarioPassword.Hash, usuario.UsuarioPassword.Salt))
            {
                Mensagens.Add(new Mensagem(ComumResource.UsuarioSenhaInvalido, EnumTipoMensagem.Erro));
                return;
            }
            Encontrado = true;
            Data = new LoginResponseDto(usuario.Id, usuario.NomeCompleto, usuario.Email, usuario.Tipo);
        }
    }
}
