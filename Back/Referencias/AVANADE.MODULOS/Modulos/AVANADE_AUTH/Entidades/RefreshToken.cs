namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.Entidades
{
    public class RefreshToken
    {
        public Guid ID { get; set; }
        public string Token { get; set; } = string.Empty!;
        public Guid IDUsuario { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? IsRevoked { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => IsRevoked == null && !IsExpired && !IsUsed;
    }
}