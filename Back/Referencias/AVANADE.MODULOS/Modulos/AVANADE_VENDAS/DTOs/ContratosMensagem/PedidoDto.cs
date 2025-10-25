namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem
{
    public record PedidoDto(
    Guid IdPedido,
    string IdCliente,
    List<ItemPedidoDto> Itens
);
}
