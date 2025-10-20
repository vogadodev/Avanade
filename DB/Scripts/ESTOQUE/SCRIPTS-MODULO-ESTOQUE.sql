-- =============================================================================
-- SCRIPT DE CRIAÇÃO DE TABELAS - AVANADE ESTOQUE
-- DIALETO: SQL Server
-- Documentação feita por Marcus Vogado do Lago
-- =============================================================================
--Execute esse comando para criarção de um novo data base, após isso abra uma nova janela de consulta no banco de dados que foi criado
--Aqui está a configuração para acessar o SQLServer Studio 
--Servidor:
--Usuario: sa
--Password:SuaSenhaF0rteAqui!
 
CREATE DATABASE  AvanadeEstoqueDb;

--Abra uma nova aba após a criação do BD e executes esses comandos: 
-- Tabela de Marcas
-- Armazena os fabricantes dos produtos.
-- =============================================================================
CREATE TABLE AVA_MARCA_MAR (
    MAR_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    MAR_NOME NVARCHAR(100) NOT NULL
);

-- Índices para a tabela de Marcas
CREATE INDEX IDX_MARCA_MAR_NOME ON AVA_MARCA_MAR (MAR_NOME);


-- =============================================================================
-- Tabela de Categorias
-- Armazena as categorias dos produtos, com suporte a hierarquia.
-- =============================================================================
CREATE TABLE AVA_CATEGORIA_CAT (
    CAT_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    CAT_NOME NVARCHAR(100) NOT NULL,
    CAT_DESCRICAO NVARCHAR(500) NULL,
    CAT_IDCATEGORIAPAI UNIQUEIDENTIFIER NULL,
    
    CONSTRAINT FK_CATEGORIA_CATEGORIAPAI FOREIGN KEY (CAT_IDCATEGORIAPAI)
        REFERENCES AVA_CATEGORIA_CAT (CAT_ID)
);

-- Índices para a tabela de Categorias
CREATE INDEX IDX_CATEGORIA_CAT_NOME ON AVA_CATEGORIA_CAT (CAT_NOME);
CREATE INDEX IDX_CATEGORIA_CAT_IDCATEGORIAPAI ON AVA_CATEGORIA_CAT (CAT_IDCATEGORIAPAI);


-- =============================================================================
-- Tabela de Fornecedores
-- Armazena os fornecedores dos produtos.
-- =============================================================================
CREATE TABLE AVA_FORNECEDOR_FOR (
    FOR_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    FOR_NOME NVARCHAR(200) NOT NULL,
    FOR_NOMECONTATO NVARCHAR(100) NULL,
    FOR_TELEFONE NVARCHAR(20) NULL,
    FOR_NOMEFANTASIA NVARCHAR(200) NULL,
    FOR_CNPJ NVARCHAR(18) NOT NULL,
	FOR_EMAIL NVARCHAR(200) NOT NULL	
);

-- Índices para a tabela de Fornecedores (ATUALIZADOS)
CREATE INDEX IDX_FORNECEDOR_FOR_NOME ON AVA_FORNECEDOR_FOR (FOR_NOME);
CREATE INDEX IDX_FORNECEDOR_FOR_NOMEFANTASIA ON AVA_FORNECEDOR_FOR (FOR_NOMEFANTASIA);
CREATE UNIQUE INDEX UIX_FORNECEDOR_FOR_CNPJ ON AVA_FORNECEDOR_FOR (FOR_CNPJ);


-- =============================================================================
-- Tabela de Armazéns
-- Armazena os locais físicos de estoque.
-- =============================================================================
CREATE TABLE AVA_ARMAZEM_ARM (
    ARM_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    ARM_NOME NVARCHAR(100) NOT NULL,
    ARM_LOCALIZACAO NVARCHAR(200) NULL
);


-- =============================================================================
-- Tabela de Produtos
-- Tabela central que armazena as informações principais dos produtos.
-- =============================================================================
CREATE TABLE AVA_PRODUTO_PRO (
    PRO_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    PRO_NOME NVARCHAR(200) NOT NULL,
    PRO_DESCRICAO NVARCHAR(MAX) NULL,
    PRO_SKU NVARCHAR(50) NOT NULL,
    PRO_PRECO DECIMAL(18, 2) NOT NULL,
    PRO_PRECOPROMOCIONAL DECIMAL(18, 2) NULL,
    PRO_ESTAEMPROMOCAO BIT NOT NULL DEFAULT 0,
    PRO_TEMFRETEGRATIS BIT NOT NULL DEFAULT 0,
    PRO_QUANTIDADEESTOQUE INT NOT NULL DEFAULT 0,
    PRO_ESTAATIVO BIT NOT NULL DEFAULT 1,
    PRO_IDMARCA UNIQUEIDENTIFIER NOT NULL,
    PRO_IDCATEGORIA UNIQUEIDENTIFIER NOT NULL,
    PRO_IDFORNECEDOR UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_PRODUTO_MARCA FOREIGN KEY (PRO_IDMARCA)
        REFERENCES AVA_MARCA_MAR (MAR_ID),
    CONSTRAINT FK_PRODUTO_CATEGORIA FOREIGN KEY (PRO_IDCATEGORIA)
        REFERENCES AVA_CATEGORIA_CAT (CAT_ID),
    CONSTRAINT FK_PRODUTO_FORNECEDOR FOREIGN KEY (PRO_IDFORNECEDOR)
        REFERENCES AVA_FORNECEDOR_FOR (FOR_ID)
);

-- Índices para a tabela de Produtos para otimizar buscas
CREATE UNIQUE INDEX UIX_PRODUTO_PRO_SKU ON AVA_PRODUTO_PRO (PRO_SKU);
CREATE INDEX IDX_PRODUTO_PRO_NOME ON AVA_PRODUTO_PRO (PRO_NOME);
CREATE INDEX IDX_PRODUTO_PRO_IDMARCA ON AVA_PRODUTO_PRO (PRO_IDMARCA);
CREATE INDEX IDX_PRODUTO_PRO_IDCATEGORIA ON AVA_PRODUTO_PRO (PRO_IDCATEGORIA);


-- =============================================================================
-- Tabela de Avaliações
-- Armazena as avaliações dos clientes para cada produto.
-- =============================================================================
CREATE TABLE AVA_AVALIACAO_AVA (
    AVA_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    AVA_IDPRODUTO UNIQUEIDENTIFIER NOT NULL,
    AVA_NOMEAUTOR NVARCHAR(100) NOT NULL,
    AVA_NOTA INT NOT NULL,
    AVA_COMENTARIO NVARCHAR(MAX) NULL,
    AVA_DATAENVIO DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    CONSTRAINT FK_AVALIACAO_PRODUTO FOREIGN KEY (AVA_IDPRODUTO)
        REFERENCES AVA_PRODUTO_PRO (PRO_ID)
        ON DELETE CASCADE
);

-- Índices para a tabela de Avaliações
CREATE INDEX IDX_AVALIACAO_AVA_IDPRODUTO ON AVA_AVALIACAO_AVA (AVA_IDPRODUTO);


-- =============================================================================
-- Tabela de Imagens do Produto
-- Armazena múltiplas imagens por produto.
-- =============================================================================
CREATE TABLE AVA_PRODUTOIMAGEM_PIM (
    PIM_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    PIM_IDPRODUTO UNIQUEIDENTIFIER NOT NULL,
    PIM_URLIMAGEM NVARCHAR(1024) NOT NULL,
    PIM_TEXTOALTERNATIVO NVARCHAR(200) NULL,
    PIM_ORDEM INT NOT NULL DEFAULT 0,

    CONSTRAINT FK_PRODUTOIMAGEM_PRODUTO FOREIGN KEY (PIM_IDPRODUTO)
        REFERENCES AVA_PRODUTO_PRO (PRO_ID)
        ON DELETE CASCADE
);

-- Índices para a tabela de Imagens do Produto
CREATE INDEX IDX_PRODUTOIMAGEM_PIM_IDPRODUTO ON AVA_PRODUTOIMAGEM_PIM (PIM_IDPRODUTO);


-- =============================================================================
-- Tabela de Especificações do Produto
-- Armazena dados técnicos em formato chave-valor.
-- =============================================================================
CREATE TABLE AVA_PRODUTOESPECIFICACAO_PES (
    PES_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    PES_IDPRODUTO UNIQUEIDENTIFIER NOT NULL,
    PES_CHAVE NVARCHAR(100) NOT NULL,
    PES_VALOR NVARCHAR(255) NOT NULL,

    CONSTRAINT FK_PRODUTOESPECIFICACAO_PRODUTO FOREIGN KEY (PES_IDPRODUTO)
        REFERENCES AVA_PRODUTO_PRO (PRO_ID)
        ON DELETE CASCADE
);

-- Índices para a tabela de Especificações do Produto
CREATE INDEX IDX_PRODUTOESPECIFICACAO_PES_IDPRODUTO ON AVA_PRODUTOESPECIFICACAO_PES (PES_IDPRODUTO);
CREATE INDEX IDX_PRODUTOESPECIFICACAO_PES_CHAVE_VALOR ON AVA_PRODUTOESPECIFICACAO_PES (PES_CHAVE, PES_VALOR);


-- =============================================================================
-- Tabela de Estoque por Armazém (Tabela de Junção)
-- Controla a quantidade de cada produto em cada armazém.
-- =============================================================================
CREATE TABLE AVA_ESTOQUEARMAZEM_EAR (
    EAR_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    EAR_IDPRODUTO UNIQUEIDENTIFIER NOT NULL,
    EAR_IDARMAZEM UNIQUEIDENTIFIER NOT NULL,
    EAR_QUANTIDADE INT NOT NULL,
    EAR_ULTIMAATUALIZACAO DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    CONSTRAINT FK_ESTOQUEARMAZEM_PRODUTO FOREIGN KEY (EAR_IDPRODUTO)
        REFERENCES AVA_PRODUTO_PRO (PRO_ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ESTOQUEARMAZEM_ARMAZEM FOREIGN KEY (EAR_IDARMAZEM)
        REFERENCES AVA_ARMAZEM_ARM (ARM_ID)
        ON DELETE CASCADE
);

CREATE UNIQUE INDEX UIX_ESTOQUEARMAZEM_EAR_PRODUTO_ARMAZEM ON AVA_ESTOQUEARMAZEM_EAR (EAR_IDPRODUTO, EAR_IDARMAZEM);
CREATE INDEX IDX_ESTOQUEARMAZEM_EAR_IDARMAZEM ON AVA_ESTOQUEARMAZEM_EAR (EAR_IDARMAZEM);

-- =============================================================================
-- INSERÇÃO DE DADOS PARA FACILITAR OS TESTES (SEED)
-- Execute esta seção após criar as tabelas para ter dados de exemplo.
-- =============================================================================

-- Declaração de variáveis para os IDs
DECLARE @marca_nvidia_id UNIQUEIDENTIFIER = NEWID();
DECLARE @marca_amd_id UNIQUEIDENTIFIER = NEWID();
DECLARE @marca_intel_id UNIQUEIDENTIFIER = NEWID();

DECLARE @cat_hardware_id UNIQUEIDENTIFIER = NEWID();
DECLARE @cat_placa_video_id UNIQUEIDENTIFIER = NEWID();
DECLARE @cat_processador_id UNIQUEIDENTIFIER = NEWID();

DECLARE @fornecedor_id UNIQUEIDENTIFIER = NEWID();

-- Inserir Marcas
INSERT INTO AVA_MARCA_MAR (MAR_ID, MAR_NOME) VALUES
(@marca_nvidia_id, 'Nvidia'),
(@marca_amd_id, 'AMD'),
(@marca_intel_id, 'Intel');

-- Inserir Categoria Pai (Hardware)
INSERT INTO AVA_CATEGORIA_CAT (CAT_ID, CAT_NOME, CAT_DESCRICAO, CAT_IDCATEGORIAPAI) VALUES
(@cat_hardware_id, 'Hardware', 'Componentes de computador em geral', NULL);

-- Inserir Categorias Filhas
INSERT INTO AVA_CATEGORIA_CAT (CAT_ID, CAT_NOME, CAT_DESCRICAO, CAT_IDCATEGORIAPAI) VALUES
(@cat_placa_video_id, 'Placas de Vídeo', 'Responsáveis pelo processamento gráfico.', @cat_hardware_id),
(@cat_processador_id, 'Processadores', 'Unidade Central de Processamento (CPU).', @cat_hardware_id);

-- Inserir Fornecedor
INSERT INTO AVA_FORNECEDOR_FOR (FOR_ID, FOR_NOME, FOR_NOMEFANTASIA, FOR_CNPJ, FOR_EMAIL, FOR_NOMECONTATO, FOR_TELEFONE) VALUES
(@fornecedor_id, 'Distribuidora de Componentes LTDA', 'Mega Peças PC', '12.345.678/0001-99', 'contato@megapecas.com', 'Carlos Silva', '(11) 98765-4321');

-- Exibir os IDs gerados para usar no JSON do Swagger
PRINT '--- IDs GERADOS PARA USAR NO CADASTRO DE PRODUTO ---';
SELECT @marca_nvidia_id AS 'ID_MARCA_NVIDIA';
SELECT @marca_amd_id AS 'ID_MARCA_AMD';
SELECT @marca_intel_id AS 'ID_MARCA_INTEL';
SELECT @cat_placa_video_id AS 'ID_CATEGORIA_PLACA_DE_VIDEO';
SELECT @cat_processador_id AS 'ID_CATEGORIA_PROCESSADOR';
SELECT @fornecedor_id AS 'ID_FORNECEDOR_PADRAO';
PRINT '----------------------------------------------------';
GO


-- =============================================================================
-- EXEMPLO DE JSON PARA CADASTRO DE PRODUTO VIA SWAGGER
-- =============================================================================
/*
  Para cadastrar um produto via Swagger na sua API de Estoque, você usará o
  endpoint POST /api/v1/Produto.

  No campo 'produtoJson', cole o JSON abaixo, substituindo os valores de
  "marcaId" e "categoriaId" pelos IDs que foram exibidos no resultado do
  script de inserção acima.

  {
    "nome": "Placa de Vídeo RTX 5090",
    "descricao": "A mais nova placa de vídeo da Nvidia, para jogos em 8K.",
    "codigoUnico": "NV-RTX5090-24G",
    "preco": 12500.00,
    "precoPromocional": 11999.90,
    "estaEmPromocao": true,
    "temFreteGratis": false,
    "quantidadeEstoque": 50,
    "estaAtivo": true,
    "marcaId": "240B03B7-F518-4958-A602-E3B43E056D4E",
    "categoriaId": "6004ACB1-D771-4AA3-BFA8-270E8A203718",
    "fornecedorId": "EF0F35BA-0D9F-4A4C-B5B4-D9CBAE67688E",
    "especificacoes": [
      {
        "chave": "Memória",
        "valor": "24GB GDDR7"
      },
      {
        "chave": "Interface",
        "valor": "PCI Express 5.0"
      }
    ],
    "imagens": []
  }

  No campo 'imagens' do formulário no Swagger, você pode fazer o upload
  dos arquivos de imagem do produto.
*/
