namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Response
{
    public record CategoriaResponseDto(Guid Id, string Nome, string Descricao, List<CategoriaResponseDto>? listaSubCategoria = null);
}
