﻿using AVANADE.ESTOQUE.API.Services.FronecedorServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AVANADE.ESTOQUE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly GravarFornecedorService _gravarFornecedorService;
        private readonly ObterFornecedorService _obterFornecedorService;
        private readonly ExcluirFornecedorService _excluirFornecedorService;

        public FornecedorController(
              GravarFornecedorService gravarFornecedorService
            , ObterFornecedorService obterFornecedorService
            , ExcluirFornecedorService excluirFornecedorService)
        {
            _gravarFornecedorService = gravarFornecedorService;
            _obterFornecedorService = obterFornecedorService;
            _excluirFornecedorService = excluirFornecedorService;
        }
        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
        [HttpPost]
        public async Task<IActionResult> GravarFornecedor(FornecedorRequestDto dto)
        {
            await _gravarFornecedorService.GravarFornecedor(dto);
            return _gravarFornecedorService.ResponderRequest(this);
        }

        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
        [HttpGet("{nomeFantasia}")]
        public async Task<IActionResult> ObterPorNomeFantasia(string nomeFantasia)
        {
            await _obterFornecedorService.ObterFornecedorPorNomeFantasia(nomeFantasia);
            return _obterFornecedorService.ResponderRequest(this);
        }

        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
        [HttpGet("obterTodosPaginado")]
        public async Task<IActionResult> ObterTodosPaginados([FromQuery]int pagina, [FromQuery] int qtdItemPagina)
        {
            await _obterFornecedorService.ObterTodosFornecedorPaginado(pagina, qtdItemPagina);
            return _obterFornecedorService.ResponderRequest(this);
        }
        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
        [HttpDelete("{idFornecedor}")]
        public async Task<IActionResult> ExcluirFornecedor(Guid idFornecedor)
        {
            await _excluirFornecedorService.ExcluirFornecedor(idFornecedor);
            return _excluirFornecedorService.ResponderRequest(this);
        }
    }
}
