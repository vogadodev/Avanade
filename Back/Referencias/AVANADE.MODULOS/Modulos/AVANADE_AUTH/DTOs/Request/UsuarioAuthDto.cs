namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Request
{
    public class UsuarioAuthDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
