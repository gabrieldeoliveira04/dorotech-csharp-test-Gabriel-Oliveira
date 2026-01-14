DoroTech BookStore API

API REST desenvolvida em ASP.NET Core para gerenciamento de livros, seguindo princípios de Clean Architecture, DDD e boas práticas de mercado.

Projeto criado como parte de um desafio técnico, com foco em organização de código, separação de responsabilidades, validações de domínio e experiência do avaliador.

Tecnologias Utilizadas

.NET 8

ASP.NET Core Web API

Entity Framework Core

PostgreSQL

Swagger / OpenAPI

JWT Authentication (com controle de roles)

ILogger

Docker-ready (compatível com deploy em cloud)

 Arquitetura

O projeto está organizado em camadas bem definidas:

ProjectLibrary/
│
├── DoroTech.BookStore.API          → Controllers e configuração da API
├── DoroTech.BookStore.Application  → Services, DTOs e regras de aplicação
├── DoroTech.BookStore.Domain       → Entidades e contratos (interfaces)
└── DoroTech.BookStore.Infrastructure → EF Core, DbContext e Repositórios

Princípios aplicados

Separação de responsabilidades

Domínio rico (validações dentro da entidade)

Repository Pattern

DTOs para comunicação externa

Services para regras de negócio

Infra desacoplada do domínio

Funcionalidades
Livros

Listar livros com paginação

Filtrar livros por título (case-insensitive)

Buscar livro por ID

Criar livro (somente Admin)

Atualizar livro (somente Admin)

Excluir livro (somente Admin)

 Autenticação e Autorização

Endpoints de escrita protegidos por JWT

Role necessária: Admin

Endpoints de leitura são públicos

Validações

Validações de entrada via DataAnnotations

Regras de negócio no Domínio

Garantia de unicidade: Título + Autor

Preço sempre maior que zero

Estoque não pode ser negativo

 Banco de Dados

PostgreSQL

Tipos corretos mapeados:

uuid → Guid

numeric → decimal

Índices:

Chave primária em Id

Índice único para (Title, Author)

Swagger / OpenAPI

O Swagger está habilitado em todos os ambientes para facilitar a avaliação técnica.

Acesso

Local:

http://localhost:PORT


Produção:

https://dorotech-csharp-test-gabriel-oliveira.onrender.com/swagger/index.html


Ao acessar a raiz (/), o usuário é redirecionado automaticamente para o Swagger.

▶️ Como rodar o projeto localmente

Passos
cd ProjectLibrary

cd DoroTech.BookStore.API

export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=bookstore;Username=postgres;Password=postgres"

dotnet restore

dotnet build

dotnet run

Login admin: admin
senha admin: 123456


A API estará disponível e abrirá diretamente no Swagger.

 Testes Manuais

Todos os endpoints podem ser testados diretamente via Swagger, incluindo:

Paginação

Filtros

Autenticação JWT

Controle de acesso por role

 Observações para o avaliador

Swagger habilitado em produção intencionalmente para facilitar testes

Código prioriza clareza, legibilidade e boas práticas

Projeto preparado para evolução (testes automatizados, cache, CQRS, etc.)

 Autor

Gabriel de Oliveira Ataides Gomes
Desenvolvedor .NET
