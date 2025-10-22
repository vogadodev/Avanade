﻿using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.GeradorDeIDsService;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.CategoriaServices
{
    public class GravarCategoriaService : RetornoPadraoService
    {
        private readonly CategoriaRepository<EstoqueDbContext> _categoriaRepository;
        private readonly ValidarCategoriaService _validarCategoriaService;
        public GravarCategoriaService(CategoriaRepository<EstoqueDbContext> categoriaRepository, ValidarCategoriaService validarCategoriaService)
        {
            _categoriaRepository = categoriaRepository;
            _validarCategoriaService = validarCategoriaService;
        }

        public async Task GravarCategoria(CategoriaRequestDto dto)
        {
            var categoriaExistente = await _categoriaRepository.SelecionarObjetoAsync(c => c.Id == dto.id);
            var ehAtualizacao = categoriaExistente != null;

            var dtoTemErro = await _validarCategoriaService.Validar(dto, ehAtualizacao);
            if (dtoTemErro)
            {
                Mensagens.AddRange(_validarCategoriaService.Mensagens);
                return;
            }


            if (ehAtualizacao)
            {
                PreencherCategoria(categoriaExistente!, dto);
                _categoriaRepository.DbSet.Update(categoriaExistente!);
            }
            else
            {
                var novaCategoria = new Categoria() { Id = CriarIDService.CriarNovoID() };
                PreencherCategoria(novaCategoria, dto);
                await _categoriaRepository.DbSet.AddAsync(novaCategoria);
            }

           await _categoriaRepository.DbContext.SaveChangesAsync();
        }

        private void PreencherCategoria(Categoria categoria, CategoriaRequestDto dto)
        {
            categoria.Nome = dto.Nome;
            categoria.Descricao = dto.Descricao;
            categoria.CategoriaPaiId = dto.paiId ?? null;   
        }
    }
}
