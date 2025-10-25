namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem
{
    public record ItemInvalidoDto(
    Guid IdProduto,
    int QuantidadeSolicitada,
    int QuantidadeEmEstoque,
    string Motivo = "Estoque Insuficiente"
);
}
