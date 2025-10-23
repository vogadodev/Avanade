using AVANADE.ESTOQUE.API.Services.CategoriaServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AVANADE.ESTOQUE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly GravarCategoriaService _gravarCategoriaService;
        private readonly ObterCategoriaService _obterCategoriaService;
        private readonly ExcluirCategoriaService _excluirCategoriaService;

        public CategoriaController(
              GravarCategoriaService gravarCategoriaService
            , ObterCategoriaService obterCategoriaService
            , ExcluirCategoriaService excluirCategoriaService)
        {
            _gravarCategoriaService = gravarCategoriaService;
            _obterCategoriaService = obterCategoriaService;
            _excluirCategoriaService = excluirCategoriaService;
        }

        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
        [HttpPost]
        public async Task<IActionResult> GravarCategoria(CategoriaRequestDto dto)
        {
            await _gravarCategoriaService.GravarCategoria(dto);
            return _gravarCategoriaService.ResponderRequest(this);
        }

        [Authorize(Policy = PoliciesTipoUsuario.Todos)]
        [HttpGet("{nome}")]
        public async Task<IActionResult>ObterCategoriaPorNome(string nome)
        {
            await _obterCategoriaService.ObterCategoriaPorNome(nome);
            return _obterCategoriaService.ResponderRequest(this);
        }

        [Authorize(Policy = PoliciesTipoUsuario.Todos)]
        [HttpGet("obterTodasComSubCategorias")]
        public async Task<IActionResult> ObterTodasComSubCategorias()
        {
            await _obterCategoriaService.ObterCategoriasSubCategirias();
            return _obterCategoriaService.ResponderRequest(this);
        }

        [Authorize(Policy = PoliciesTipoUsuario.Todos)]
        [HttpGet("obterTodasSemSubCategorias")]
        public async Task<IActionResult> ObterTodasSemSubCategorias()
        {
            await _obterCategoriaService.ObterTodasCategorias();
            return _obterCategoriaService.ResponderRequest(this);
        }

        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
        [HttpDelete("{categoriaId}")]
        public async Task<IActionResult> ExcluirCategoria(Guid categoriaId)
        {
            await _excluirCategoriaService.ExcluirCategoria(categoriaId);
            return _excluirCategoriaService.ResponderRequest(this);
        }
    }
}
