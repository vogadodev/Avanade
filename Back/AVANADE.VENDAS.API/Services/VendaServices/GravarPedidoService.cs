using AVANADE.INFRASTRUCTURE.RabbitMQServices.Interfaces;
using AVANADE.INFRASTRUCTURE.RabbitMQServices.Queues;
using AVANADE.INFRASTRUCTURE.ServicesComum.GeradorDeIDsService;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Repositories;
using AVANADE.VENDAS.API.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AVANADE.VENDAS.API.Services.VendaServices
{
    //Após gravação na base, enviar mensagem para o serviço de estoque para dar baixa nos produtos vendidos
    //Aqui adicionaria também a lógica de pagamento, integração com gateway de pagamento redirecionamento para pagamento api de pagamento, etc.
    //Mas para manter o foco e simplificar o exemplo, eu não construi essa parte.
    //Por isso o valor total dos produtos não foram calculados, pois seriam processados na etapa de pagamento via api.
    public class GravarPedidoService : RetornoPadraoService
    {
        private readonly ValidarPedidoService _validarPedidoService;
        private readonly VendaRepository<VendaDbContext> _vendaRepository;
        private readonly IMessageBusService _messageBus;
        public GravarPedidoService(
              ValidarPedidoService validarPedidoService
            , VendaRepository<VendaDbContext> vendaRepository
            , IMessageBusService messageBus)
        {
            _validarPedidoService = validarPedidoService;
            _vendaRepository = vendaRepository;
            _messageBus = messageBus;
        }
        public async Task GravarPedido(PedidoRequestDto dto, ClaimsPrincipal user)
        {
            var dtoTemErro = await _validarPedidoService.ValidarPedido(dto);
            if (dtoTemErro)
            {
                Mensagens.AddRange(_validarPedidoService.Mensagens);
                return;
            }
            var pedido = CriarNovaVenda(dto, user);
            await _vendaRepository.DbSet.AddAsync(pedido);
            await _vendaRepository.DbContext.SaveChangesAsync();

            var pedidoMensagem = CriaPedidoFilaRabbitMQ(pedido);
            await _messageBus.PublicarMensagemAsync(pedidoMensagem, RabbitMqQueues.PedidosNovos);
        }
        private Venda CriarNovaVenda(PedidoRequestDto dto, ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Guid.TryParse(userIdValue, out Guid userId);

            return new Venda
            {
                Id = CriarIDService.CriarNovoID(),
                ClienteId = userId,
                StatusVenda = StatusVendaEnum.Novo,
                StatusPagamento = StatusPagamentoEnum.Pendente,
                ItensVenda = dto.listaDeProdutos.Select(item => new ItemVenda
                {
                    Id = CriarIDService.CriarNovoID(),
                    ProdutoId = item.IdProduto,
                    Quantidade = item.Quantidade, 
                    NomeProduto= item.Nome,
                    EstaAtivo = true,
                }).ToList(),
                DataCriacao = DateTime.Now
            };
        }

        private PedidoDto CriaPedidoFilaRabbitMQ(Venda novaVenda)
        {
          return  new PedidoDto(
        novaVenda.Id,
        novaVenda.ClienteId.ToString(),
        novaVenda.ItensVenda.Select(item =>
            new ItemPedidoDto(item.Id , item.NomeProduto, item.Quantidade)
        ).ToList()
    );
        }
    }
}
