-- =============================================================================
-- SCRIPT DE CRIAÇÃO DE TABELAS - AVANADE USUARIO
-- DIALETO: SQL Server
-- Documentação feita por Marcus Vogado do Lago
-- =============================================================================
-- INFORMAÇÕES DE ACESSO AO BANCO DE DADOS DE USUARIO
-- Aqui está a configuração para acessar o SQLServer Studio 
-- Nome do Servidor: localhost,1434
-- Usuario: sa
-- Password: SuaSenhaF0rteAqui!

-- Execute esse comando para criação de um novo data base
CREATE DATABASE AvanadeUsuarioDb;
GO

USE AvanadeUsuarioDb;
GO

-- =============================================================================
-- Tabela de Usuário
-- Armazena as informações cadastrais principais dos usuários do sistema.
-- =============================================================================
CREATE TABLE AVA_USUARIO_USU (
    USU_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    USU_NOME NVARCHAR(255) NOT NULL,
    CLI_EMAIL NVARCHAR(255) NOT NULL,
    CLI_TIPO INT NOT NULL,
    CLI_DATACADASTRO DATETIME2 NOT NULL,
    CLI_DATAATUALIZACAO DATETIME2 NULL
);

-- Índices para a tabela de Usuário
CREATE UNIQUE INDEX IX_AVA_USUARIO_USU_EMAIL
ON AVA_USUARIO_USU(CLI_EMAIL);
GO

-- =============================================================================
-- Tabela de Senha do Usuário
-- Armazena o hash e o salt da senha, em uma relação 1-para-1 com o usuário.
-- =============================================================================
CREATE TABLE AVA_USUARIOPASSWORD_UPW (
    UPW_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    UPW_IDUSUARIO UNIQUEIDENTIFIER NOT NULL,
    UPW_HASHPASSWORD VARBINARY(MAX) NOT NULL,
    UPW_SALTPASSWORD VARBINARY(MAX) NOT NULL,
   
    CONSTRAINT FK_UPW_USUARIO FOREIGN KEY (UPW_IDUSUARIO)
        REFERENCES AVA_USUARIO_USU(USU_ID)
        ON DELETE CASCADE 
);

-- Índices para a tabela de Senha do Usuário
CREATE UNIQUE INDEX IX_AVA_USUARIOPASSWORD_UPW_IDUSUARIO
ON AVA_USUARIOPASSWORD_UPW(UPW_IDUSUARIO);
GO


