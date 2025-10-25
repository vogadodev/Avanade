using AVANADE.INFRASTRUCTURE.ServicesComum.EnumService;
using AVANADE.INFRASTRUCTURE.ServicesComum.IntegracaoApiService;
using AVANADE.INFRASTRUCTURE.ServicesComum.ServicoComMensagemService;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Resourcers;
using System.Text.Json;

namespace AVANADE.VENDAS.API.Services.VendaServices
{
    public class ValidarPedidoService : MensagemService
    {
        private readonly ConsumirApiExternaService _consumirApiService;
        private readonly JsonSerializerOptions _jsonOptions;
        public ValidarPedidoService(ConsumirApiExternaService apiEstoqueService)
        {
            _consumirApiService = apiEstoqueService;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<bool> ValidarPedido(PedidoRequestDto dto)
        {

            ValidarItensPedido(dto);

            if (!Mensagens.TemErros())
            {
                await ValidarProdutoEstoque(dto);
            }

            return Mensagens.TemErros();
        }

        private void ValidarItensPedido(PedidoRequestDto dto)
        {
            foreach (var item in dto.listaDeProdutos)
            {
                Mensagens.AdicionarErroSe(item.Quantidade <= 0, string.Format(VendaResource.ProdutoSemEstoque, item.Nome));
            }
        }       

        private async Task ValidarProdutoEstoque(PedidoRequestDto dto)
        {
            var retornoApi = await _consumirApiService.Post(EndpointsVendasExternosEnum.ValidarEstoqueEndPoint.GetDescription(), dto);
            if (retornoApi is not null && retornoApi.Data is not null)
            {
                var dataResponse = (JsonElement)retornoApi.Data;
                var listaDeProdutosSemEstoque = dataResponse.Deserialize<List<ItemPedidoDto>>(_jsonOptions);
                foreach (var produtoSemEstoque in listaDeProdutosSemEstoque!)
                {
                    Mensagens.AdicionarErro(string.Format(VendaResource.ProdutoSemEstoque, produtoSemEstoque.Nome));
                }
            }
        }
    }
}
