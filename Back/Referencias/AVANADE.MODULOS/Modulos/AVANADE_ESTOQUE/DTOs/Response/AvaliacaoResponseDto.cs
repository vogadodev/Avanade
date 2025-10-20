namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Response
{
    public record AvaliacaoResponseDto(
        Guid Id,
        Guid ProdutoId,
        string Autor,
        string Titulo,
        string Texto,
        int Nota,
        DateTime DataCriacao
    );
}
