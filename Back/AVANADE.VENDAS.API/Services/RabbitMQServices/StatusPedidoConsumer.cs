using AVANADE.INFRASTRUCTURE.RabbitMQServices.Interfaces;
using AVANADE.INFRASTRUCTURE.RabbitMQServices.Queues;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Repositories;
using AVANADE.VENDAS.API.Data;

namespace AVANADE.VENDAS.API.Services.RabbitMQServices
{
    public class StatusPedidoConsumer : BackgroundService
    {
        private readonly ILogger<StatusPedidoConsumer> _logger;
        private readonly IMessageBusService _messageBus;
        private readonly VendaRepository<VendaDbContext> _vendaRepository;

        public StatusPedidoConsumer(
              ILogger<StatusPedidoConsumer> logger
            , IMessageBusService messageBus
            , VendaRepository<VendaDbContext> vendaRepository)
        {
            _logger = logger;
            _messageBus = messageBus;
            _vendaRepository = vendaRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Escuta a fila de sucesso
            await _messageBus.ConsumirMensagensAsync<PedidoDto>(
                RabbitMqQueues.PedidosConcluidos,
                async (pedido) => await AtualizarStatusAsync(pedido.IdPedido, StatusVendaEnum.Processando)
            );

            // Escuta a fila de falha
            await _messageBus.ConsumirMensagensAsync<PedidoNaoConcluidoDto>(
                RabbitMqQueues.PedidosNaoConcluidos,
                async (falha) => await AtualizarStatusAsync(falha.PedidoOriginal.IdPedido, StatusVendaEnum.Cancelado)
            );
        }

        private async Task AtualizarStatusAsync(Guid pedidoId, StatusVendaEnum status)
        {
            _logger.LogInformation("Atualizando status do Pedido {PedidoId} para {Status}", pedidoId, status);

            var pedido = await _vendaRepository.SelecionarObjetoAsync(p => p.Id == pedidoId);
            if(pedido == null)
            {
                _logger.LogWarning("Pedido {PedidoId} não encontrado para atualização de status.", pedidoId);
                return;
            }
            pedido.StatusVenda = status;
            _vendaRepository.DbSet.Update(pedido);
            await _vendaRepository.DbContext.SaveChangesAsync();            
        }
    }
}
