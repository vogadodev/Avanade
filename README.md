# Projeto Avanade - Demonstração de Arquitetura Moderna

## LINK PARA VÍDEO ONDE EXPLICO UM POUCO DO PROJETO E CONFIGURAÇÕES INICIAIS

[![Assista ao vídeo de apresentação do projeto](https://img.youtube.com/vi/_k4GsLopNuU/hqdefault.jpg)](https://youtu.be/_k4GsLopNuU)

*(Clique na imagem acima para assistir ao vídeo)*

## Sobre Mim

Olá! Pessoal da Avanade! 

Sou **Marcus Vogado do Lago**, Programador Full Stack, e este repositório é uma demonstração das minhas habilidades na construção de aplicações web modernas e escaláveis.

Minha trajetória profissional reflete uma transição focada e um forte desejo de aplicar conhecimentos técnicos em desafios complexos. Após um período significativo atuando com atendimento e suporte na CAESB, direcionei minha carreira para o desenvolvimento de software. 
Sou graduado em Análise e Desenvolvimento de Sistemas pela UNIP (concluído em Dez/2023) e estou aprofundando meus conhecimentos com uma Pós-Graduação em Arquitetura de Software, Ciência de Dados e Cybersecurity na PUCPR (previsão de término em Jun/2026).

Atualmente, atuo como Desenvolvedor Full Stack na MXM Sistemas, onde trabalho na modernização de sistemas ERP legados. Utilizo .NET 8, C#, ASP.NET Core Web API e Angular para construir soluções baseadas em Microsserviços e Micro Frontends. Nesse alabilidade e performance dasmbiente, aplico diariamente conceitos de Clean Architecture, Domain-Driven Design (DDD), SOLID e Clean Code para garantir a qualidade, esca entregas.
Também tenho a oportunidade de oferecer suporte técnico e mentoria à equipe. Minha experiência anterior na TecnoGas e Gaseng envolveu o desenvolvimento .NET e mobile com Xamarin.Forms e MySQL.

Possuo um conjunto sólido de competências técnicas, incluindo C#, .NET (Core e 8), ASP.NET Core, ORMs (Entity Framework, Dapper), Angular, bancos de dados relacionais (SQL Server, Oracle, MySQL) e NoSQL (MongoDB), além de experiência com Git, RabbitMQ e metodologias ágeis.

Este projeto "Avanade" é um reflexo prático dessas tecnologias e conceitos.

* **LinkedIn:** [https://www.linkedin.com/in/marcus-vogado/](https://www.linkedin.com/in/marcus-vogado/)
* **GitHub:** [https://github.com/vogadodev](https://github.com/vogadodev)

## Sobre o Projeto "Avanade"
Primeiros passos ao realizar o clone do projeto:

**git clone https://github.com/vogadodev/Avanade.git**

Como estamos trabalhando com submodules do git para os projetos frontends precisamos executar o seguinte comando dentro da pasta onde o projeto foi clonado: 

**git submodule update --init --recursive**

**Para Desenvolvimento e Testes sem ser pelo docker-compose faça as seguites configurações

Altere o arquivo AmbienteURlPadraoService.cs
<img width="1881" height="558" alt="image" src="https://github.com/user-attachments/assets/daa13caa-ba20-4e75-a770-861401a2068b" />

**Altere o arquivo appsettigns.Development.json.
<img width="1536" height="525" alt="image" src="https://github.com/user-attachments/assets/f9b74e49-fa7a-4493-9e96-52e9b41ad086" />


Este repositório demonstra a implementação de um sistema (ex: E-commerce/ERP) utilizando uma arquitetura distribuída e moderna. O objetivo é apresentar a aplicação prática de:

* **Microsserviços no Backend:** Utilizando .NET 8 e ASP.NET Core Web API, cada serviço focado em um domínio específico (Autenticação, Usuários, Estoque, Vendas).
* **Micro Frontends no Frontend:** Usando Angular e Single-SPA para permitir o desenvolvimento e deploy independentes das interfaces de usuário.
* **API Gateway:** Ocelot atuando como ponto único de entrada para as APIs, simplificando o consumo pelo frontend e adicionando uma camada de segurança.
* **Mensageria Assíncrona:** RabbitMQ para comunicação desacoplada entre os microsserviços (ex: Vendas notificando Estoque).
* **Proxy Reverso:** Nginx configurado para rotear o tráfego para os diferentes frontends e para o API Gateway, além de gerenciar certificados SSL (HTTPS).
* **Conteinerização:** Docker e Docker Compose para facilitar a configuração do ambiente de desenvolvimento e garantir a portabilidade.
* **Boas Práticas:** Aplicação de Clean Architecture, DDD, SOLID e Clean Code em todo o desenvolvimento.

## Tecnologias Utilizadas

**Backend:**

* **Linguagem:** C#
* **Framework:** .NET 8, ASP.NET Core Web API
* **Arquitetura:** Microsserviços
* **API Gateway:** Ocelot
* **Mensageria:** RabbitMQ
* **ORM:** Entity Framework Core
* **Banco de Dados:** SQL Server
* **Princípios:** Clean Architecture, DDD, SOLID, Clean Code

**Frontend:**

* **Framework:** Angular (v12+) 
* **Linguagem:** TypeScript 
* **Arquitetura:** Micro Frontends (Single-SPA)
* **Estilização:** Bootstrap, CSS/SCSS

**Infraestrutura e DevOps:**

* **Conteinerização:** Docker / Docker Compose
* **Proxy Reverso:** Nginx
* **Controle de Versão:** Git / GitHub 

## Configuração do Banco de Dados

O sistema utiliza bancos de dados separados para diferentes contextos, conforme a arquitetura de microsserviços. As configurações de conexão estão nas variáveis de ambiente dentro do `docker-compose.yml` e são lidas pelos arquivos `appsettings.json` das APIs.

**Exemplos de Bancos (SQL Server):**

* `AvanadeAuthDb`: Para autenticação e tokens.
* `AvanadeUsuarioDb`: Para dados de usuários.
* `AvanadeEstoqueDb`: Para produtos e estoque.
* `AvanadeVendasDb`: Para pedidos e vendas.

**Configuração de Conexão (no `docker-compose.yml`):**

## Exemplo para a API de Autenticação
**environment:
  - ConnectionStrings__AuthDbConnection=Server=db-auth;Database=AvanadeAuthDb;User Id=sa;Password=${MSSQL_SA_PASSWORD};TrustServerCertificate=True
... (configurações similares para outros serviços)**

# Tutorial da Ferramenta "Apoio ao Desenvolvedor Avanade"
<img width="794" height="677" alt="image" src="https://github.com/user-attachments/assets/8e4fcf4a-5939-4618-a395-4d8b27954e61" />

Esta ferramenta foi criada para simplificar e automatizar as tarefas comuns de configuração e execução do ambiente de desenvolvimento do projeto Avanade. Ela ajuda a gerenciar o Nginx, instalar dependências, criar novos projetos (frontend e backend) e rodar os serviços necessários.

**IMPORTANTE:** A ferramenta precisa ser executada **como Administrador** para realizar operações como instalação de software, modificação do arquivo `hosts` e gerenciamento de serviços (Nginx, Docker).

### 1. Gerenciar NGINX

Esta aba centraliza as operações relacionadas ao Nginx, que atua como proxy reverso para os frontends e o API Gateway.

* **Instalar NGINX + mkcert:** Instala o Nginx (se ainda não estiver instalado) e a ferramenta `mkcert` para gerar certificados SSL/TLS locais confiáveis (permitindo HTTPS em `localhost` ou domínios locais).
* **Iniciar NGINX:** Inicia o serviço do Nginx.
* **Parar NGINX:** Para o serviço do Nginx.
* **Aplicar Certificados e Hosts:** Executa o `mkcert` para gerar/instalar os certificados para os domínios definidos e atualiza o arquivo `hosts` do Windows para mapear esses domínios para `127.0.0.1`.
* **Status:** A área à direita indica se o Nginx está rodando (`NGINX ATIVO`) ou parado (`NGINX PARADO`).

### 2. Instalar Dependências

Esta aba facilita a instalação das ferramentas e SDKs necessários para o desenvolvimento.

* **Pré-requisito:** Antes de instalar o Node.js, é recomendado instalar o **NVM (Node Version Manager)** para gerenciar múltiplas versões do Node. O link para download do NVM para Windows está disponível na tela.
* **1. Instalar Node.js 16 (Requer NVM):** Instala a versão 16 do Node.js usando o NVM.
* **2. Instalar .NET 8 SDK:** Instala o SDK do .NET 8.
* **3. Instalar Angular CLI 17.3.12:** Instala a versão específica do Angular CLI necessária para os projetos frontend.
* **4. Instalar Prettier e Configurar VSCode:** Instala o Prettier globalmente e configura as definições recomendadas no VS Code para formatação de código.

### 3. Criar Frontend

Esta aba automatiza a criação de novos projetos de Micro Frontend Angular, seguindo a estrutura do Single-SPA.

* **1. Caminho da Pasta Pai:** Selecione a pasta onde o novo projeto frontend será criado (ex: `C:\Avanade\Front`).
* **2. Nome da Aplicação:** Digite o nome do microfrontend (ex: `estoque`, `vendas`, `usuario`).
* **3. Nome do Workspace:** Define o nome da pasta do projeto. O padrão é `avanade-<nome-da-app>-front`.
* **4. Porta:** Define a porta em que o servidor de desenvolvimento (`ng serve`) deste microfrontend rodará (ex: `4203`, `4204`).
* **Criar Projeto Frontend:** Executa os comandos do Angular CLI e do Single-SPA para gerar a estrutura do projeto com as configurações base.

### 4. Criar Backend

Esta aba automatiza a criação de novos projetos de Microsserviço .NET.

* **1. Caminho da Pasta dos Backends:** Selecione a pasta onde o novo projeto de microsserviço será criado (ex: `C:\Avanade\App\Back`).
* **2. Nome do Microsserviço:** Digite o nome do microsserviço (ex: `Estoque`, `Vendas`, `Notificacao`).
* **Criar Microsserviço:** Executa os comandos `dotnet new` (provavelmente usando um template customizado) para gerar a estrutura base do microsserviço, já configurado com a arquitetura do projeto (ex: Clean Architecture).

### 5. Rodar Backend

Esta aba gerencia a execução dos serviços backend via Docker Compose.

* **1. Selecione o arquivo docker-compose.yml:** Use o botão "Procurar..." para localizar o arquivo `docker-compose.yml` principal do projeto.
* **(Implícito/Faltante na Imagem)** **Seleção de Perfis/Bancos:** Geralmente, aqui haveria checkboxes ou uma lista para selecionar quais `profiles` (ex: `auth`, `estoque`, `infra`, `gateway`) e quais bancos de dados (`db-auth`, `db-estoque`) você deseja iniciar. A ferramenta lê os perfis disponíveis no arquivo compose.
* **Rodar Serviços (docker compose up):** Executa o comando `docker compose up -d --build` com os perfis e serviços selecionados, iniciando os contêineres em segundo plano.
* **Parar Serviços (docker compose down):** Executa o comando `docker compose down`, parando e removendo os contêineres definidos nos perfis.

### 6. Rodar Frontend

Esta aba permite iniciar os servidores de desenvolvimento (`ng serve`) para os Micro Frontends selecionados.

* **1. Selecione a Pasta dos Frontends:** Use o botão "Procurar..." para indicar a pasta principal que contém todos os projetos frontend (ex: `C:\Avanade\App\Front`).
* **Selecione os microfrontends (Remotes) adicionais para rodar:** Marque as caixas correspondentes aos microfrontends que você deseja iniciar. O projeto 'single-root' (Host) e 'navprincipal' (Menu) geralmente são necessários. A lista é populada com os projetos encontrados na pasta selecionada.
* **Rodar Projetos Selecionados:** Executa o comando `ng serve` (ou script NPM customizado) para cada microfrontend selecionado, cada um em sua respectiva porta configurada no `angular.json`.

### Log de Saída

Em todas as abas, a área "Log de Saída" na parte inferior exibe o output dos comandos executados (Docker, Nginx, Angular CLI, dotnet, etc.), mostrando o progresso, mensagens de sucesso ou erros que possam ocorrer. É fundamental acompanhar este log para diagnosticar problemas.





