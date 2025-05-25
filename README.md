# MottuApi (.NET) — Mapeamento Inteligente de Motos no Pátio

API RESTful desenvolvida em ASP.NET Core para cadastro, consulta e gerenciamento de motos e filiais, integrando-se ao banco Oracle via Entity Framework Core.

## 🎯 Sobre o Projeto

Este projeto faz parte do CP2 da disciplina Advanced Business Development with .NET e tem como objetivo criar uma API RESTful, usando .NET 8 e banco Oracle, aplicando Clean Architecture e Domain-Driven Design (DDD), com foco em solucionar um desafio real da empresa Mottu.

## 🚀 Funcionalidades

- CRUD completo de **Filial**
  - Cadastro com endereço completo
  - Validações de negócio
  - Ativação/Desativação de filial
- CRUD completo de **Moto**
  - Cadastro com informações detalhadas
  - Validações de negócio
  - Disponibilidade da moto
  - Relacionamento com filial
- Documentação automática via Swagger/OpenAPI

## 🛠️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Oracle Database
- AutoMapper
- Swagger/OpenAPI
- Clean Architecture
- Domain-Driven Design (DDD)

## 📋 Pré-requisitos

- .NET 8 SDK
- Oracle Database
- Visual Studio 2022 ou VS Code

## 🔧 Instalação e Execução

1. **Clone o repositório:**

   ```sh
   git clone https://github.com/camargoogui/DOTNET---Challenge.git
   cd DOTNET---Challenge
   ```

2. **Configure a string de conexão:**

   - Abra o arquivo `MottuApi.Presentation/appsettings.json`
   - Atualize a string de conexão do Oracle com suas credenciais

3. **Restaure os pacotes:**

   ```sh
   cd MottuApi
   dotnet restore
   ```

4. **Execute as migrações:** (dentro da MottuApi)

   ```sh
   dotnet ef database update
   ```

5. **Execute a aplicação:** (dentro da MottuApi)

   ```sh
   dotnet run
   ```

6. **Acesse o Swagger:**
   ```
   http://localhost:5231/swagger
   ```

## 📚 Rotas da API

### Filial

- `GET /api/filial` — Lista todas as filiais
- `GET /api/filial/{id}` — Busca filial por ID
- `POST /api/filial` — Cria uma nova filial
- `PUT /api/filial/{id}` — Atualiza uma filial existente
- `DELETE /api/filial/{id}` — Remove uma filial

### Moto

- `GET /api/moto` — Lista todas as motos
- `GET /api/moto/{id}` — Busca moto por ID
- `GET /api/moto/por-placa?placa=XXX1234` — Busca moto por placa
- `GET /api/moto/por-filial/{filialId}` — Lista motos de uma filial
- `POST /api/moto` — Cria uma nova moto
- `PUT /api/moto/{id}` — Atualiza uma moto existente
- `DELETE /api/moto/{id}` — Remove uma moto

## 🏗️ Arquitetura

O projeto segue os princípios da Clean Architecture e DDD, com as seguintes camadas:

- **Domain**: Entidades, Value Objects e Interfaces
- **Application**: DTOs, Interfaces de Serviço e Mapeamentos
- **Infrastructure**: Implementações dos Repositórios e Contexto do Banco
- **Presentation**: Controllers e Configurações da API

## 👥 Integrantes

- RM556270 - Bianca Vitoria - 2TDSPZ
- RM555166 - Guilherme Camargo - 2TDSPM
- RM555131 - Icaro Americo - 2TDSPM
