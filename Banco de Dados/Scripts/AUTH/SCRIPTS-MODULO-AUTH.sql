-- =============================================================================
-- SCRIPT DE CRIAÇÃO DE TABELAS - AVANADE AUTH
-- DIALETO: SQL Server
-- Documentação feita por Marcus Vogado do Lago
-- =============================================================================
-- INFORMAÇÕES DE ACESSO AO BANCO DE DADOS DE AUTH
-- Aqui está a configuração para acessar o SQLServer Studio 
-- Nome do Servidor: localhost,1433
-- Usuario: sa
-- Password: SuaSenhaF0rteAqui!

-- Execute esse comando para criação de um novo data base
CREATE DATABASE AvanadeAuthDb;
GO

USE AvanadeAuthDb;
GO

-- =============================================================================
-- Tabela de Refresh Token
-- Armazena os refresh tokens emitidos para os usuários. Estes tokens
-- são usados para renovar tokens de acesso (JWT) expirados sem que o 
-- usuário precise fazer login novamente.
-- =============================================================================
CREATE TABLE AVANADE_USUARIOREFRESHTOKEN_URT (
    URT_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    URT_IDUSUARIO UNIQUEIDENTIFIER NOT NULL,
    URT_TOKEN NVARCHAR(500) NOT NULL,
    URT_CREATED DATETIME2 NOT NULL,
    URT_EXPIRES DATETIME2 NOT NULL,
    URT_ISUSED BIT NOT NULL,
    URT_REVOKED BIT NULL
);

-- Índices para a tabela de Refresh Token
CREATE UNIQUE INDEX IX_URT_TOKEN
ON AVANADE_USUARIOREFRESHTOKEN_URT(URT_TOKEN);

CREATE INDEX IX_URT_IDUSUARIO
ON AVANADE_USUARIOREFRESHTOKEN_URT(URT_IDUSUARIO);
GO