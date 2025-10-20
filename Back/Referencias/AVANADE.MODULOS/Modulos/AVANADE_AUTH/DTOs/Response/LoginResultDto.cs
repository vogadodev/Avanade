namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response
{
    public record LoginResultDto(
     bool Succeeded,
     string? IDUsuario = null,
     string? NomeUsuario = null,
     string? AccessToken = null,
     string? RefreshToken = null,
     string ErrorMessage = ""
 );
}
