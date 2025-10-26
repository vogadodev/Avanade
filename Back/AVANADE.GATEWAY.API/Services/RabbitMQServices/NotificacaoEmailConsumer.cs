using AVANADE.INFRASTRUCTURE.RabbitMQServices.Interfaces;
using AVANADE.INFRASTRUCTURE.RabbitMQServices.Queues;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem;
using System.Text;

namespace AVANADE.GATEWAY.API.Services.RabbitMQServices
{
    //Aqui é o serviço que consome as mensagens de pedidos concluídos ou não concluídos, por exemplo, para enviar e-mails de confirmação ou falha.
    public class NotificacaoEmailConsumer : BackgroundService
    {
        private readonly ILogger<NotificacaoEmailConsumer> _logger;
        private readonly IMessageBusService _messageBus;
        private readonly IServiceScopeFactory _scopeFactory;

        public NotificacaoEmailConsumer(
            ILogger<NotificacaoEmailConsumer> logger,
            IMessageBusService messageBus,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _messageBus = messageBus;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)        {
            
            //Escuta a fila de concluído com sucesso
            await _messageBus.ConsumirMensagensAsync<PedidoDto>(
                RabbitMqQueues.PedidosConcluidos,
                async (pedido) => await EnviarEmailConfirmacaoAsync(pedido)
            );

            // Escuta a fila de falha
            await _messageBus.ConsumirMensagensAsync<PedidoNaoConcluidoDto>(
                RabbitMqQueues.PedidosNaoConcluidos,
                async (falha) => await EnviarEmailFalhaAsync(falha)
            );
        }

        private async Task EnviarEmailConfirmacaoAsync(PedidoDto pedido)
        {
            var logMessage = new StringBuilder();
            logMessage.AppendLine("==============================Dados-Email-Sucesso=====================");
            logMessage.AppendLine($"PedidoID = {pedido.IdPedido}");
            logMessage.AppendLine($"ClienteID = {pedido.IdCliente}");
            logMessage.AppendLine("Itens[");

            if (pedido.Itens != null && pedido.Itens.Any())
            {
                foreach (var item in pedido.Itens)
                {
                    logMessage.AppendLine("-------------------");
                    logMessage.AppendLine($"  Nome: {item.Nome}");
                    logMessage.AppendLine($"  ProdutoID: {item.IdProduto}");
                    logMessage.AppendLine($"  Quantidade: {item.Quantidade}");
                    logMessage.AppendLine("------------------");
                }
            }
            else
            {
                logMessage.AppendLine("  (Nenhum item no pedido)");
            }

            logMessage.AppendLine("]");
            logMessage.AppendLine("=================================================================");

            _logger.LogInformation(logMessage.ToString());         
            await Task.CompletedTask; 
        }
                
        public record ItemInvalidoDto(Guid IdProduto, string Motivo); 

        private async Task EnviarEmailFalhaAsync(PedidoNaoConcluidoDto falha)
        {
            var logMessage = new StringBuilder();
            logMessage.AppendLine("==============================Dados-Email-Falha======================");
            logMessage.AppendLine("--- Pedido Original ---");
            logMessage.AppendLine($"PedidoID = {falha.PedidoOriginal.IdPedido}");
            logMessage.AppendLine($"ClienteID = {falha.PedidoOriginal.IdCliente}");
            logMessage.AppendLine("Itens Solicitados[");

            if (falha.PedidoOriginal.Itens != null && falha.PedidoOriginal.Itens.Any())
            {
                foreach (var item in falha.PedidoOriginal.Itens)
                {
                    logMessage.AppendLine("-------------------");
                    logMessage.AppendLine($"  Nome: {item.Nome}");
                    logMessage.AppendLine($"  ProdutoID: {item.IdProduto}");
                    logMessage.AppendLine($"  Quantidade: {item.Quantidade}");
                    logMessage.AppendLine("------------------");
                }
            }
            else
            {
                logMessage.AppendLine("  (Nenhum item solicitado)");
            }
            logMessage.AppendLine("]");

            logMessage.AppendLine("--- Itens Indisponíveis ---");
            logMessage.AppendLine("Itens[");
            if (falha.ItensIndisponiveis != null && falha.ItensIndisponiveis.Any())
            {
                foreach (var itemInvalido in falha.ItensIndisponiveis)
                {
                    logMessage.AppendLine("-------------------");
                    logMessage.AppendLine($"  ProdutoID: {itemInvalido.IdProduto}");
                    logMessage.AppendLine($"  Motivo: {itemInvalido.Motivo}");
                    logMessage.AppendLine("------------------");
                }
            }
            else
            {
                logMessage.AppendLine("  (Nenhum item indisponível reportado)");
            }
            logMessage.AppendLine("]");
            logMessage.AppendLine("================================================================");

            _logger.LogWarning(logMessage.ToString());            
            await Task.CompletedTask;
        }
    }
}
