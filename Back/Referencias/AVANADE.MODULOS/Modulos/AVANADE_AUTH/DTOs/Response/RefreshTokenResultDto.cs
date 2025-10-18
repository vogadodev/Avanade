namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response
{
    public record RefreshTokenResultDto(
    bool Succeeded,
    string? NewAccessToken = null,
    string? NewRefreshToken = null,
    string ErrorMessage = ""
);
}
