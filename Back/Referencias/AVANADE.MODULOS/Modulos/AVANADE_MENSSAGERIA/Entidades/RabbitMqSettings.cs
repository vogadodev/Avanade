﻿namespace AVANADE.MODULOS.Modulos.AVANADE_MENSSAGERIA.Entidades
{
    public class RabbitMqSettings
    {
        public const string SectionName = "RabbitMq";
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 5672;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
