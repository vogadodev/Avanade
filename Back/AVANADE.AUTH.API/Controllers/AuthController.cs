using AVANADE.AUTH.API.Services.LoginServices;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AVANADE.AUTH.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginServices _loginService;
        private readonly ValidarLoginService _validarLoginService;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly JwtSettings _jwtSettings;
        public AuthController(LoginServices loginService
            , ValidarLoginService validarLoginService
            , IOptions<JwtSettings> jwtSettings
            , RefreshTokenService refreshTokenService)
        {
            _loginService = loginService;
            _validarLoginService = validarLoginService;
            _jwtSettings = jwtSettings.Value;   
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            if (_validarLoginService.ValidarLogin(dto))
            {
                return BadRequest(_validarLoginService.Mensagens);
            }

            var result = await _loginService.LoginAsync(dto.Email, dto.Password);

            if (!result.Succeeded)
            {
                return Unauthorized(new { message = result.ErrorMessage });
            }

            SetTokenCookies(result.AccessToken!, result.RefreshToken!);
            return Ok(new LoginResponseDto(result.IDUsuario!, result.NomeUsuario!));
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {            
            string? expiredAccessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string? refreshToken = Request.Cookies[_jwtSettings.AccessTokenCookieName];

            if (string.IsNullOrEmpty(expiredAccessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "Tokens necessários não fornecidos." });
            }

            var result = await _refreshTokenService.RefreshSessionAsync(expiredAccessToken, refreshToken);

            if (result.Succeeded)
            {
                SetTokenCookies(result.NewAccessToken!, result.NewRefreshToken!);
                return Ok();
            }

            return Unauthorized(new { message = result.ErrorMessage });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {       
            var userId = _loginService.ObterIdUsuarioLogado(User);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userId == null)
            {               
                return Unauthorized();
            }           
            return Ok(new { id = userId, email = userEmail });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies[_jwtSettings.AccessTokenCookieName];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _refreshTokenService.RevokeTokenAsync(refreshToken);
            }

            DeleteTokenCookies();
            return Ok(new { message = "Logout bem-sucedido" });
        }

        private void SetTokenCookies(string accessToken, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = true,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append(_jwtSettings.AccessTokenCookieName, refreshToken, cookieOptions);           
            cookieOptions.Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            Response.Cookies.Append(_jwtSettings.AccessTokenCookieName, accessToken, cookieOptions);
        }

        private void DeleteTokenCookies()
        {
            Response.Cookies.Delete(_jwtSettings.AccessTokenCookieName);            
        }
    }
}
