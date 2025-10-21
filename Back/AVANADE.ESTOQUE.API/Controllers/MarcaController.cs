using AVANADE.ESTOQUE.API.Services.MarcaServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace AVANADE.ESTOQUE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly ObterMarcaService _obterMarcaService;
        public MarcaController(ObterMarcaService obterMarcaService)
        {
            _obterMarcaService = obterMarcaService;           
        }

        [HttpPost]
        public async Task<IActionResult> GravarMarca(MarcaRequestDto dto)
        {
            //TODO-IMPLEMENTAR METODO DE GRAVAÇÃO

                return Ok();
        }
        [HttpGet("{nome}")]
        public async Task<IActionResult>ObterMarcaPorNome(string nome)
        {
            await _obterMarcaService.ObterPorNome(nome);
            return _obterMarcaService.ResponderRequest(this);
        }

        [HttpGet("{pagina}/{qtdItensPagina}")]
        public async Task<IActionResult> ObterTodasMarcasPaginado(int pagina, int qtdItensPagina)
        {
            await _obterMarcaService.ObterTodas(pagina, qtdItensPagina);
            return _obterMarcaService.ResponderRequest(this);
        }

    }
}
