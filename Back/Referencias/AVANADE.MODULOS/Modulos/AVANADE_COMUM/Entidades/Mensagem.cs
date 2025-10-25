﻿using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Enums;

namespace AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades
{
    public class Mensagem
    {
        public string Texto { get; set; } = string.Empty!;
        public EnumTipoMensagem Tipo { get; set; }
        public string? Detalhe { get; set; }

        public Mensagem() { }
        public Mensagem(string mensagem, EnumTipoMensagem enumTipoMensagem , string? detalhe = null)
        {
            Texto = mensagem;
            Tipo = enumTipoMensagem;
            Detalhe = detalhe;
        }
        public Mensagem(Exception exception)
        {
            Texto = exception.Message;
            Tipo = EnumTipoMensagem.Erro;
        }

        
    }
}
