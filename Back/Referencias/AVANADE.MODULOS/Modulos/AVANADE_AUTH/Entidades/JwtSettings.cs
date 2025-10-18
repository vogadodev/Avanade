﻿namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.Entidades
{
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty!;
        public string Issuer { get; set; } = string.Empty!;
        public string Audience { get; set; } = string.Empty!;
        public string AccessTokenCookieName { get; set; } = string.Empty!;
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }
}
