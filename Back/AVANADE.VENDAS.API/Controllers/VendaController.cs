using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.Request;
using AVANADE.VENDAS.API.Services.VendaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AVANADE.VENDAS.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VendaController : ControllerBase
    {       
        private readonly GravarPedidoService _gravarPedidoService;

        public VendaController(GravarPedidoService gravarPedidoService)
        {
            _gravarPedidoService = gravarPedidoService;
        }

        [Authorize(Policy = PoliciesTipoUsuario.Todos)]
        [HttpPost]
        public async Task<IActionResult> GravarVenda(PedidoRequestDto dto)
        {
            await _gravarPedidoService.GravarPedido(dto, User);
            return _gravarPedidoService.ResponderRequest(this);
        }
    }
}
