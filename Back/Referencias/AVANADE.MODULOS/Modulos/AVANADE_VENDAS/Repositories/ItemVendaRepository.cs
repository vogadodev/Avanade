using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Repositories
{
    public class ItemVendaRepository<T> : BaseRepository<ItemVenda> where T : AvanadeDbContextComum<T>
    {
        public ItemVendaRepository(T context) : base(context) { }
    }
}
