﻿using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.MenssagemService;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Resourcers;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AVANADE.ESTOQUE.API.Services.FronecedorServices
{
    public class ValidarFornecedorService : MensagemService
    {
        private readonly FornecedorRepository<EstoqueDbContext> _fornecedorRepository;

        public ValidarFornecedorService(FornecedorRepository<EstoqueDbContext> fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<bool> Validar(FornecedorRequestDto dto, bool ehAtualizacao)
        {
            ValidarCampoNomeObrigatorio(dto);
            ValidarCampoCNPJObrigatorio(dto);
            ValidarCampoEmailObrigatorio(dto);
            ValidarCampoNomeContatoObrigatorio(dto);
            ValidarCampoTelefoneObrigatorio(dto);
            ValidarCampoNomeFantasiaObrigatorio(dto);

            if (!Mensagens.TemErros() && !ehAtualizacao)
            {
                await ValidarSeCnpjJaExiste(dto);
            }

            return Mensagens.TemErros();
        }

        private void ValidarCampoNomeObrigatorio(FornecedorRequestDto dto)
        {
            Mensagens.AdicionarErroSe(string.IsNullOrWhiteSpace(dto.RazaoSocial), FornecedorResourcer.RazaoSocialObrigatorio);
        }

        private void ValidarCampoNomeFantasiaObrigatorio(FornecedorRequestDto dto)
        {
            Mensagens.AdicionarErroSe(string.IsNullOrWhiteSpace(dto.NomeFantasia), FornecedorResourcer.NomeFantasiaObrigatorio);
        }

        private void ValidarCampoTelefoneObrigatorio(FornecedorRequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.Telefone))
            {
                Mensagens.AdicionarErro(FornecedorResourcer.TelefoneObrigatorio);
                return;
            }

            var regex = new Regex(@"^\(\d{2}\)\s\d\s\d{4}-\d{4}$");

            if (!regex.IsMatch(dto.Telefone))
            {
                Mensagens.AdicionarErro(FornecedorResourcer.TelefoneInvalido);
            }
        }

        private void ValidarCampoCNPJObrigatorio(FornecedorRequestDto dto)
        {
            if (!string.IsNullOrEmpty(dto.CNPJ))
            {
                var regexCnpj = new Regex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$");
                if (!regexCnpj.IsMatch(dto.CNPJ))
                {
                    Mensagens.AdicionarErro(FornecedorResourcer.CNPJInvalido);
                }
            }
            else
            {
                Mensagens.AdicionarErro(FornecedorResourcer.CNPJObrigatorio);
            }
        }

        private void ValidarCampoNomeContatoObrigatorio(FornecedorRequestDto dto)
        {
            Mensagens.AdicionarErroSe(string.IsNullOrWhiteSpace(dto.NomeContato), FornecedorResourcer.NomeContatoObrigatorio);
        }

        private void ValidarCampoEmailObrigatorio(FornecedorRequestDto dto)
        {
            if (!MailAddress.TryCreate(dto.Email, out _))
            {
                Mensagens.AdicionarErro(FornecedorResourcer.EmailInvalido);
            }
        }
        private async Task ValidarSeCnpjJaExiste(FornecedorRequestDto dto)
        {   
            if (await _fornecedorRepository.ValidarExistenciaAsync(f => f.CNPJ == dto.CNPJ))
            {
                Mensagens.AdicionarErro(string.Format(FornecedorResourcer.CnpjJaCadastrado, dto.CNPJ));
            }
        }
    }
}
