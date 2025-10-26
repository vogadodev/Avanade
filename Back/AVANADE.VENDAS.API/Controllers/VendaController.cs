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
        private readonly ObterVendaService _obterVendaService;

        public VendaController(GravarPedidoService gravarPedidoService, ObterVendaService obterVendaService)
        {
            _gravarPedidoService = gravarPedidoService;
            _obterVendaService = obterVendaService;
        }

        [Authorize(Policy = PoliciesTipoUsuario.Todos)]
        [HttpPost]
        public async Task<IActionResult> GravarVenda(PedidoRequestDto dto)
        {
            await _gravarPedidoService.GravarPedido(dto, User);
            return _gravarPedidoService.ResponderRequest(this);
        }
        [Authorize(Policy = PoliciesTipoUsuario.Todos)]
        [HttpGet("vendas")]
        public async Task<IActionResult> ObterVendaPorId()
        {
            await _obterVendaService.ObterVendasPorCliente(User);
            return _obterVendaService.ResponderRequest(this);
        }
    }
}
