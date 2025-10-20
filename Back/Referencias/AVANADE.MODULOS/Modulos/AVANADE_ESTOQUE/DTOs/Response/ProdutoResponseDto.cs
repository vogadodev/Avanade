namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Response
{
    public record ProdutoResponseDto(
        Guid Id,
        string Nome,
        string Descricao,
        long CodigoUnico,
        decimal Preco,
        decimal? PrecoPromocional,
        bool EstaEmPromocao,
        bool TemFreteGratis,
        int QuantidadeEstoque,
        bool EstaAtivo,
        Guid MarcaId,
        string NomeMarca,
        Guid CategoriaId,
        string NomeCategoria,
        List<AvaliacaoResponseDto> Avaliacoes,
        List<ProdutoImagemDto> Imagens,
        List<ProdutoEspecificacaoDto> Especificacoes
    );
}
