using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.Request
{
    public record PedidoRequestDto(Guid IdPedido, List<ItemPedidoDto> listaDeProdutos);
}
