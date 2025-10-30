using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.Response
{
    public record VendaResponseDto(
      Guid Id,
      Guid? ClienteId,
      decimal ValorTotal,
      string StatusVenda,
      string StatusPagamento,
      DateTime DataCriacao,
      DateTime? DataAtualizacao,
      bool EstaAtivo,
      ICollection<ItemVenda> ItensVenda
    );
}
