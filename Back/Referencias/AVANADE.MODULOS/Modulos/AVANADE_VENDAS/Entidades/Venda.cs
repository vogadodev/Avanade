using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Enums;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades
{
    public class Venda
    {        
        public Guid Id { get; set; }
        public Guid? ClienteId { get; set; } 
        public decimal ValorTotal { get; set; }        
        public StatusVendaEnum StatusVenda { get; set; }  
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool EstaAtivo { get; set; }

        // Relacionamento
        public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();
    }
}
