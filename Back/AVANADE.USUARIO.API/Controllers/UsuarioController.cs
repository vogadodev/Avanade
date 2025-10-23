using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.DTOs.Request;
using AVANADE.USUARIO.API.Services.UsuarioServices;
using Microsoft.AspNetCore.Mvc;

namespace AVANADE.USUARIO.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly GravarUsuarioClienteService _gravarUsuarioService;
        private readonly UsuarioLoginService _usuarioLoginService;

        public UsuarioController(
              GravarUsuarioClienteService gravarUsuarioService
            , UsuarioLoginService usuarioLoginService)
        {
            _gravarUsuarioService = gravarUsuarioService;
            _usuarioLoginService = usuarioLoginService;
        }

        [HttpPost("cliente")]
        public async Task<IActionResult>GravarUsuarioCliente(UsuarioRequestDto dto)
        {
            await _gravarUsuarioService.GravarUsuarioAsync(dto);
            return _gravarUsuarioService.ResponderRequest(this);
        }
        [HttpPost("adm")]
        public async Task<IActionResult> GravarUsuarioAdm(UsuarioRequestDto dto)
        {
            await _gravarUsuarioService.GravarUsuarioAsync(dto);
            return _gravarUsuarioService.ResponderRequest(this);
        }
        [HttpPost("Login")]
        public async Task<IActionResult>UsuarioLogin(UsuarioLoginRequestDto dto)
        {
            await _usuarioLoginService.RealizarLoginUsuario(dto);
            return _usuarioLoginService.ResponderRequest(this);
        }
    }
}
