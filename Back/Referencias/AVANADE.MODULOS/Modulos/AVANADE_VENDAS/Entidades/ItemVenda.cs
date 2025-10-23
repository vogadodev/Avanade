namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades
{
    public class ItemVenda
    {       
        public Guid Id { get; set; }
        public Guid VendaId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool EstaAtivo { get; set; }

        // Relacionamento
        public Venda? Venda { get; set; }
    }
}
