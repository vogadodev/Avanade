using AVANADE.AUTH.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class RefreshTokenService
{
    private readonly RefreshTokenRepository<AuthDbContext> _refreshTokenRepository;
    private readonly TokenService _tokenService;

    public RefreshTokenService(
        RefreshTokenRepository<AuthDbContext> refreshTokenRepository,
        TokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
    }

    public async Task<RefreshTokenResultDto> RefreshSessionAsync(string expiredAccessToken, string receivedRefreshToken)
    {
        // 1. Lê as informações do usuário do token de acesso EXPIRADO.
        var principal = _tokenService.GetPrincipalFromExpiredToken(expiredAccessToken);
        var usuarioIdStr = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!Guid.TryParse(usuarioIdStr, out Guid usuarioId))
        {
            return new RefreshTokenResultDto(false, ErrorMessage: "Access Token inválido.");
        }

        // 2. Busca o refresh token no banco de dados local.
        var refreshTokenInDb = await _refreshTokenRepository.SelecionarObjetoAsync(rt => rt.Token == receivedRefreshToken);

        // 3. Valida o refresh token usando a propriedade computada IsActive.
        if (refreshTokenInDb == null || !refreshTokenInDb.IsActive || refreshTokenInDb.IDUsuario != usuarioId)
        {
            return new RefreshTokenResultDto(false, ErrorMessage: "Refresh Token inválido, expirado ou já utilizado.");
        }

        // 4. Rotação de Token: Invalida o token antigo para prevenir reuso.
        refreshTokenInDb.IsUsed = true;
        _refreshTokenRepository.DbSet.Update(refreshTokenInDb);

        // 5. Gera um NOVO Access Token, preservando TODAS as claims do usuário.
        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);

        // 6. Gera um NOVO Refresh Token.
        var newRefreshToken = _tokenService.GenerateRefreshToken(usuarioId);
        await _refreshTokenRepository.DbSet.AddAsync(newRefreshToken);

        await _refreshTokenRepository.SaveChangesAsync();

        return new RefreshTokenResultDto(true, newAccessToken, newRefreshToken.Token);
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        var refreshTokenInDb = await _refreshTokenRepository.SelecionarObjetoAsync(rt => rt.Token == token);

        if (refreshTokenInDb == null || !refreshTokenInDb.IsActive)
        {
            return false; // Token não existe ou já está inativo.
        }

        refreshTokenInDb.IsRevoked = true;
        _refreshTokenRepository.DbSet.Update(refreshTokenInDb);
        await _refreshTokenRepository.SaveChangesAsync();

        return true;
    }
}