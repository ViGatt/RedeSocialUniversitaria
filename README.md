# 📚 Rede Social Universitária - Projeto Acadêmico

## 🚀 Visão Geral
Este projeto é uma implementação de uma rede social voltada para o ambiente universitário, desenvolvida como parte de estudos em arquitetura de software e boas práticas de desenvolvimento.

## 🔧 Tecnologias Utilizadas
- **Backend**: .NET 6
- **ORM**: Entity Framework Core
- **Banco de Dados**: SQL Server
- **Padrões Arquiteturais**:
  - Domain-Driven Design (DDD)
  - Repository Pattern
  - Clean Architecture

## 📌 Funcionalidades Principais
- 👥 Cadastro e gerenciamento de usuários
- 📝 Criação e interação com postagens
- ❤️ Sistema de curtidas
- 💬 Comentários em postagens
- 🗓️ Criação e inscrição em eventos
- 🤝 Sistema de seguidores/seguindo

## 🏗️ Estrutura do Projeto
```
RedeSocialUniversitaria/
├── Core/                     (Camada de Domínio)
│   ├── Entities/             (Entidades de negócio)
│   ├── Interfaces/           (Contratos de repositórios)
│   └── Specifications/       (Especificações de consulta)
│
├── Infrastructure/           (Camada de Infraestrutura)
│   ├── Data/                 (Configuração do EF Core)
│   ├── Repositories/         (Implementações concretas)
│   └── Services/             (Serviços complementares)
│
└── API/                      (Camada de Apresentação)
    ├── Controllers/          (Endpoints da API)
    ├── DTOs/                 (Objetos de transferência)
    └── Configurations/       (Configurações da aplicação)
```

## 📚 Aprendizados
- Implementação de DDD na prática
- Uso do Repository Pattern com EF Core
- Configuração de relacionamentos complexos
- Migrações e gerenciamento de banco de dados
- Tratamento de ciclos de cascata em SQL Server
- Documentação de API com Swagger

## ⚙️ Configuração do Ambiente
1. Instale o .NET 6 SDK
2. Configure a string de conexão em `API/appsettings.json`
3. Execute as migrações:
```bash
dotnet ef migrations add InitialCreate -p ../Infrastructure/ -s . -o Data/Migrations
dotnet ef database update
```
