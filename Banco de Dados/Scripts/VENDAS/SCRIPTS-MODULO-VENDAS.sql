-- =============================================================================
-- SCRIPT DE CRIAÇÃO DE TABELAS - AVANADE VENDAS
-- DIALETO: SQL Server
-- Documentação feita por Marcus Vogado do Lago
-- =============================================================================
--INFORMAÇÕES DE ACESSO AO BANCO DE DADOS DE VENDAS
--Aqui está a configuração para acessar o SQLServer Studio 
--Nome do Servidor: localhost,1435
--Usuario: sa
--Password: SuaSenhaF0rteAqui!

--Execute esse comando para criação de um novo data base
CREATE DATABASE AvanadeVendasDb;
GO

--Abra uma nova aba após a criação do BD e executes esses comandos: 
USE AvanadeVendasDb;
GO

-- =============================================================================
-- Tabela de Vendas
-- Armazena o cabeçalho das vendas realizadas, status do pagamento e cliente.
-- =============================================================================
CREATE TABLE AVA_VENDA_VEN (
    VEN_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    VEN_IDCLIENTE UNIQUEIDENTIFIER NULL,
    VEN_DATAVENDA DATETIME2(7) NOT NULL,
    VEN_VALORTOTAL DECIMAL(18, 2) NOT NULL,
    VEN_STATUSPAGAMENTO INT NOT NULL,
    VEN_ESTAATIVO BIT NOT NULL DEFAULT 1
);

-- Índices para a tabela de Vendas
CREATE INDEX IDX_VENDA_VEN_IDCLIENTE ON AVA_VENDA_VEN (VEN_IDCLIENTE) WHERE VEN_IDCLIENTE IS NOT NULL;
CREATE INDEX IDX_VENDA_VEN_DATAVENDA ON AVA_VENDA_VEN (VEN_DATAVENDA);


-- =============================================================================
-- Tabela de Itens da Venda
-- Armazena os produtos associados a uma venda específica.
-- =============================================================================
CREATE TABLE AVA_ITEMVENDA_ITV (
    ITV_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    ITV_IDVENDA UNIQUEIDENTIFIER NOT NULL,
    ITV_IDPRODUTO UNIQUEIDENTIFIER NOT NULL,
    ITV_QUANTIDADE INT NOT NULL,
    ITV_PRECOUNITARIO DECIMAL(18, 2) NOT NULL,
    ITV_ESTAATIVO BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_ITEMVENDA_VENDA FOREIGN KEY (ITV_IDVENDA)
        REFERENCES AVA_VENDA_VEN (VEN_ID)
        ON DELETE CASCADE
);

-- Índices para a tabela de Itens da Venda
CREATE INDEX IDX_ITEMVENDA_ITV_IDVENDA ON AVA_ITEMVENDA_ITV (ITV_IDVENDA);
CREATE INDEX IDX_ITEMVENDA_ITV_IDPRODUTO ON AVA_ITEMVENDA_ITV (ITV_IDPRODUTO);
GO