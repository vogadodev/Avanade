namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem
{
    public record PedidoNaoConcluidoDto(
    PedidoDto PedidoOriginal,
    List<ItemInvalidoDto> ItensIndisponiveis
);
}
