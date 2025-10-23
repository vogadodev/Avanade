using AVANADE.AUTH.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.EnumService;
using AVANADE.INFRASTRUCTURE.ServicesComum.IntegracaoApiService;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Enums;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Resourcers;
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
                return new LoginResultDto(false,null,null,null, ComumResource.UsuarioSenhaInvalido);
            }

            //Desserializa os dados do usuário que a API de Usuário retornou
            var jsonElement = (JsonElement)retornoApi.Data;
            var usuario = jsonElement.Deserialize<UsuarioAuthDto>(_jsonOptions);

            if (usuario == null)
            {
                return new LoginResultDto(false, ErrorMessage: "Formato de dados do usuário inválido.");
            }
                      
            var claims = CriarClaims(usuario);
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken(usuario.IdUsuario);
            
            await _refreshTokenRepository.DbSet.AddAsync(refreshToken);
            await _refreshTokenRepository.DbContext.SaveChangesAsync();
            return new LoginResultDto(true, usuario.IdUsuario, usuario.NomeUsuario, accessToken, refreshToken.Token);
        }
              
        public UsuarioAuthDto? ObterIdUsuarioLogado(ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var userNome = user.FindFirstValue(ClaimTypes.Name) ?? user.FindFirstValue(JwtRegisteredClaimNames.Name);
            if (Guid.TryParse(userIdValue, out Guid userId))
            {
                return new UsuarioAuthDto() { IdUsuario = Guid.Parse(userIdValue), NomeUsuario = userNome! };
            }
            return null;
        }

        private static IEnumerable<Claim> CriarClaims(UsuarioAuthDto usuario)
        {
            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Name, usuario.NomeUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserType", usuario.Tipo.GetDescription())
            };
           
            return claims;
        }
    }
}