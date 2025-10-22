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
        private readonly GravarMarcaService _gravarMarcaService;
        private readonly ObterMarcaService _obterMarcaService;
        private readonly ExcluirMarcaService _excluirMarcaService;

        public MarcaController(
              GravarMarcaService gravarMarcaService
            , ObterMarcaService obterMarcaService
            , ExcluirMarcaService excluirMarcaService)
        {
            _gravarMarcaService = gravarMarcaService;
            _obterMarcaService = obterMarcaService;
            _excluirMarcaService = excluirMarcaService;
        }

        [HttpPost]
        public async Task<IActionResult> GravarMarca(MarcaRequestDto dto)
        {
            await _gravarMarcaService.GravarMarca(dto);
            return _gravarMarcaService.ResponderRequest(this);

        }

        [HttpGet("{nome}")]
        public async Task<IActionResult> ObterMarcaPorNome(string nome)
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

        [HttpDelete("{idMarca}")]
        public async Task<IActionResult> ExcluirMarca(Guid idMarca)
        {
            await _excluirMarcaService.ExcluirMarca(idMarca);
            return _excluirMarcaService.ResponderRequest(this);
        }

    }
}
