# Projeto Avanade - Demonstração de Arquitetura Moderna

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

```yaml
# Exemplo para a API de Autenticação
environment:
  - ConnectionStrings__AuthDbConnection=Server=db-auth;Database=AvanadeAuthDb;User Id=sa;Password=${MSSQL_SA_PASSWORD};TrustServerCertificate=True
# ... (configurações similares para outros serviços)
