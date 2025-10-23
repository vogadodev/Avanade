using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.GeradorDeIDsService;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Repositories;
using AVANADE.USUARIO.API.Data;

namespace AVANADE.USUARIO.API.Services.UsuarioServices
{
    //Cadastro apenas de usuarios do tipo Adm, feito assim para facilitar os testes.
    public class GravarUsuarioAdmService : RetornoPadraoService
    {
        private readonly ValidarUsuarioService _validarUsuarioService;
        private readonly UsuarioRepository<UsuarioDbContext> _usuarioRepository;
        public GravarUsuarioAdmService(ValidarUsuarioService validarUsuarioService,
                                    UsuarioRepository<UsuarioDbContext> usuarioRepository)
        {
            _validarUsuarioService = validarUsuarioService;
            _usuarioRepository = usuarioRepository;
        }
        public async Task GravarUsuarioAsync(UsuarioRequestDto dto)
        {
            var dtoTemErro = await _validarUsuarioService.ValidarUsuario(dto);
            if (dtoTemErro)
            {
                Mensagens.AddRange(_validarUsuarioService.Mensagens);
                return;
            }
            var usuario = CriarUsuario(dto);

            await _usuarioRepository.DbSet.AddAsync(usuario);
            await _usuarioRepository.DbContext.SaveChangesAsync();
        }

        private Usuario CriarUsuario(UsuarioRequestDto dto)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                NomeCompleto = dto.NomeCompleto,
                Email = dto.Email,
                Tipo = TipoUsuarioEnum.AdminMaster
            };
            usuario.UsuarioPassword = CriarSenhaCriptografada(dto);
            return usuario;
        }
        private UsuarioPassword CriarSenhaCriptografada(UsuarioRequestDto dto)
        {
            HasherPasswordServices.CreatedHashPassword(dto.Senha, out byte[] hash, out byte[] salt);

            return new UsuarioPassword()
            {
                Id = CriarIDService.CriarNovoID(),
                Hash = hash,
                Salt = salt
            };
        }
    }
}
