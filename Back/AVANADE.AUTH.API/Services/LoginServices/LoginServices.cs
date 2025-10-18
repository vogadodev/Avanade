using AVANADE.AUTH.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.EnumService;
using AVANADE.INFRASTRUCTURE.ServicesComum.IntegracaoApiService;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text.Json;

namespace AVANADE.AUTH.API.Services.LoginServices
{
    public class LoginServices: MensagemService
    {
        private readonly ConsumirApiExternaService _apiUsuarioService;
        private readonly TokenService _tokenService;
        private readonly RefreshTokenRepository<AuthDbContext> _refreshTokenRepository;
        private readonly JsonSerializerOptions _jsonOptions;
       

        public LoginServices(
            ConsumirApiExternaService apiUsuarioService,
            TokenService tokenService,
            RefreshTokenRepository<AuthDbContext> refreshTokenRepository)
        {
            _apiUsuarioService = apiUsuarioService;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<LoginResultDto> LoginAsync(string email, string password)
        {
            //Chama a API de Usuário para validar as credenciais
            var loginRequestData = new { Email = email, Password = password };
            var retornoApi = await _apiUsuarioService.Post(EnumEndpointsExternos.LoginEndpoint.GetDescription(), loginRequestData);

            //Verifica se a chamada falhou ou se a API de Usuário retornou erro
            if (retornoApi == null || retornoApi.Data == null)
            {               
                return new LoginResultDto(false, ErrorMessage: "Credenciais inválidas ou falha de comunicação.");
            }

            //Desserializa os dados do usuário que a API de Usuário retornou
            var jsonElement = (JsonElement)retornoApi.Data;
            var usuario = jsonElement.Deserialize<UsuarioAuthDto>(_jsonOptions);

            if (usuario == null)
            {
                return new LoginResultDto(false, ErrorMessage: "Formato de dados do usuário inválido.");
            }

            //Gera os tokens com base nos dados recebidos
            var claims = CriarClaims(usuario);
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken(usuario.Id);

            //Persiste o refresh token no banco de dados da AUTH.API
            await _refreshTokenRepository.DbSet.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();
            return new LoginResultDto(true, usuario.Id.ToString(), accessToken, refreshToken.Token);
        }

        // Este método agora apenas extrai informações do token, sem acessar o banco.
        public Guid? ObterIdUsuarioLogado(ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (Guid.TryParse(userIdValue, out Guid userId))
            {
                return userId;
            }
            return null;
        }

        private static IEnumerable<Claim> CriarClaims(UsuarioAuthDto usuario)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, usuario.Email),
                new(JwtRegisteredClaimNames.Name, usuario.Nome),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(usuario.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }
    }
}