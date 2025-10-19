# 🚀 Mottu API - Sistema de Gerenciamento de Motos e Filiais

## 📋 Sobre o Projeto

A **Mottu API** é uma aplicação desenvolvida em .NET 8 que implementa os princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e **Clean Code** para gerenciar motos e filiais da empresa Mottu.

## 👥 Integrantes

- **RM556270** - Bianca Vitoria - 2TDSPZ
- **RM555166** - Guilherme Camargo - 2TDSPM  
- **RM555131** - Icaro Americo - 2TDSPM

## 🔗 Repositório GitHub

**Link do Projeto**: [https://github.com/camargoogui/mottu-api-dotnet.git](https://github.com/camargoogui/mottu-api-dotnet.git)

## 🏗️ Arquitetura

O projeto segue a **Clean Architecture** com separação clara de responsabilidades em 4 camadas:

```
📦 MottuApi
 ┣ 📂 MottuApi.Presentation    -> Controllers, Program.cs, appsettings
 ┣ 📂 MottuApi.Application     -> Services, DTOs, Interfaces, Mappings
 ┣ 📂 MottuApi.Domain          -> Entities, ValueObjects, Exceptions, Interfaces
 ┗ 📂 MottuApi.Infrastructure  -> Data, Repositories, HealthChecks
```

### 📁 Estrutura Detalhada

```
MottuApi/
├── MottuApi.sln                    # Arquivo de solução
├── MottuApi.Presentation/          # Camada de Apresentação
│   ├── Controllers/
│   │   ├── FilialController.cs     # 7 endpoints para filiais
│   │   ├── MotoController.cs       # 9 endpoints para motos
│   │   └── LocacaoController.cs    # 15 endpoints para locações
│   ├── Program.cs                  # Configuração da aplicação
│   ├── appsettings.json           # Configurações
│   └── Properties/
├── MottuApi.Application/           # Camada de Aplicação
│   ├── DTOs/
│   │   ├── FilialDTO.cs           # DTOs para filiais
│   │   ├── MotoDTO.cs             # DTOs para motos
│   │   └── LocacaoDTO.cs          # DTOs para locações
│   ├── Interfaces/
│   │   ├── IFilialService.cs      # Interface do serviço de filiais
│   │   ├── IMotoService.cs        # Interface do serviço de motos
│   │   └── ILocacaoService.cs     # Interface do serviço de locações
│   ├── Services/
│   │   ├── FilialService.cs       # Lógica de aplicação para filiais
│   │   ├── MotoService.cs         # Lógica de aplicação para motos
│   │   └── LocacaoService.cs      # Lógica de aplicação para locações
│   └── Mappings/
│       └── MappingProfile.cs      # Configuração do AutoMapper
├── MottuApi.Domain/               # Camada de Domínio
│   ├── Entities/
│   │   ├── Filial.cs              # Entidade rica (Agregado Raiz)
│   │   ├── Moto.cs                # Entidade rica
│   │   └── Locacao.cs             # Entidade rica (Core Business)
│   ├── ValueObjects/
│   │   └── Endereco.cs            # Value Object imutável
│   ├── Exceptions/
│   │   └── DomainException.cs     # Exceções de domínio
│   └── Interfaces/
│       ├── IFilialRepository.cs   # Interface do repositório de filiais
│       ├── IMotoRepository.cs     # Interface do repositório de motos
│       └── ILocacaoRepository.cs  # Interface do repositório de locações
└── MottuApi.Infrastructure/       # Camada de Infraestrutura
    ├── Data/
    │   └── MongoDbContext.cs      # Contexto do MongoDB
    ├── Repositories/
    │   ├── FilialMongoRepository.cs    # Implementação MongoDB do repositório de filiais
    │   ├── MotoMongoRepository.cs      # Implementação MongoDB do repositório de motos
    │   └── LocacaoMongoRepository.cs   # Implementação MongoDB do repositório de locações
    └── HealthChecks/
        └── MongoHealthCheck.cs     # Health Check para MongoDB
```

### 🎯 Domain-Driven Design (DDD)

- **Entidades Ricas**: `Moto`, `Filial` e `Locacao` com comportamento encapsulado
- **Agregado Raiz**: `Filial` como agregado raiz que gerencia suas motos
- **Core Business**: `Locacao` como entidade central do negócio
- **Value Object**: `Endereco` como value object imutável
- **Interfaces no Domínio**: `IFilialRepository`, `IMotoRepository` e `ILocacaoRepository`

### 🧹 Clean Code

- **SRP**: Uma responsabilidade por classe
- **DRY**: Eliminação de duplicação de código
- **KISS**: Soluções simples e diretas
- **YAGNI**: Implementação apenas do necessário

## 🛠️ Tecnologias Utilizadas

- **.NET 8**
- **MongoDB Driver 2.28.0**
- **MongoDB** (banco de dados NoSQL)
- **AutoMapper 12.0.1**
- **Swagger/OpenAPI 6.5.0**
- **Health Checks**
- **ASP.NET Core Web API**

## 🚀 Como Executar

### Pré-requisitos

- .NET 8 SDK
- MongoDB (local ou Atlas)
- Visual Studio 2022 ou VS Code

### 1. Clone o repositório

```bash
git clone https://github.com/camargoogui/mottu-api-dotnet.git
cd mottu-api-dotnet
```

### 2. Configure o MongoDB

**Opção A - MongoDB Local (Recomendado):**

Se você tem MongoDB instalado localmente (via Homebrew no Mac):

```bash
# Verificar se o MongoDB está rodando
brew services list | grep mongodb

# Se não estiver rodando, iniciar:
brew services start mongodb-community@7.0
```

**Opção B - MongoDB Atlas (Cloud):**

Edite o arquivo `MottuApi/MottuApi.Presentation/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MongoConnection": "mongodb+srv://usuario:senha@cluster.mongodb.net/"
  },
  "MongoDatabaseName": "mottu_db"
}
```

**Configuração padrão (MongoDB local):**
- **Host**: `localhost`
- **Porta**: `27017`
- **Database**: `mottu_db`

### 3. Execute a aplicação

```bash
cd MottuApi/MottuApi.Presentation
dotnet run --urls "http://localhost:5001"
```

A API estará disponível em:
- **HTTP**: `http://localhost:5001`
- **Swagger**: `http://localhost:5001` (raiz)
- **Health Check**: `http://localhost:5001/health`

## 📚 Documentação da API

### Swagger

Acesse a documentação interativa do Swagger em:
- **Desenvolvimento**: `http://localhost:5001`
- **Produção**: `https://sua-api.com`

### Endpoints Principais

#### 🏢 Filiais

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/v1/filial` | Listar todas as filiais |
| GET | `/api/v1/filial/{id}` | Buscar filial por ID |
| POST | `/api/v1/filial` | Criar nova filial |
| PUT | `/api/v1/filial/{id}` | Atualizar filial |
| DELETE | `/api/v1/filial/{id}` | Excluir filial |
| PATCH | `/api/v1/filial/{id}/ativar` | Ativar filial |
| PATCH | `/api/v1/filial/{id}/desativar` | Desativar filial |

#### 🏍️ Motos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/v1/moto` | Listar todas as motos |
| GET | `/api/v1/moto/{id}` | Buscar moto por ID |
| GET | `/api/v1/moto/por-placa?placa=ABC1234` | Buscar moto por placa |
| GET | `/api/v1/moto/por-filial/{filialId}` | Listar motos de uma filial |
| POST | `/api/v1/moto` | Criar nova moto |
| PUT | `/api/v1/moto/{id}` | Atualizar moto |
| DELETE | `/api/v1/moto/{id}` | Excluir moto |
| PATCH | `/api/v1/moto/{id}/disponivel` | Marcar moto como disponível |
| PATCH | `/api/v1/moto/{id}/indisponivel` | Marcar moto como indisponível |

#### 🚗 Locações

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/v1/locacao` | Listar todas as locações (com paginação) |
| GET | `/api/v1/locacao/{id}` | Buscar locação por ID |
| GET | `/api/v1/locacao/por-moto/{motoId}` | Listar locações de uma moto |
| GET | `/api/v1/locacao/por-filial/{filialId}` | Listar locações de uma filial |
| GET | `/api/v1/locacao/por-cliente?cpf=12345678901` | Buscar locações por CPF do cliente |
| GET | `/api/v1/locacao/por-periodo?inicio=2024-01-01&fim=2024-12-31` | Buscar locações por período |
| GET | `/api/v1/locacao/ativas` | Listar locações ativas |
| GET | `/api/v1/locacao/finalizadas` | Listar locações finalizadas |
| POST | `/api/v1/locacao` | Criar nova locação |
| PUT | `/api/v1/locacao/{id}` | Atualizar locação |
| DELETE | `/api/v1/locacao/{id}` | Excluir locação |
| PATCH | `/api/v1/locacao/{id}/iniciar` | Iniciar locação |
| PATCH | `/api/v1/locacao/{id}/finalizar` | Finalizar locação |
| PATCH | `/api/v1/locacao/{id}/cancelar` | Cancelar locação |
| GET | `/api/v1/locacao/{id}/calcular-valor` | Calcular valor total da locação |

#### 🏥 Health Check

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/health` | Health check geral (aplicação + MongoDB) |
| GET | `/health/ready` | Health check do banco de dados |
| GET | `/health/live` | Health check da aplicação |

## 📝 Exemplos de Uso

### Criar uma Filial

```bash
curl -X POST "http://localhost:5001/api/v1/filial" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Filial São Paulo",
    "logradouro": "Rua das Flores",
    "numero": "123",
    "complemento": "Sala 1",
    "bairro": "Centro",
    "cidade": "São Paulo",
    "estado": "SP",
    "cep": "01234567",
    "telefone": "(11) 99999-9999"
  }'
```

### Criar uma Moto

```bash
curl -X POST "http://localhost:5001/api/v1/moto" \
  -H "Content-Type: application/json" \
  -d '{
    "placa": "ABC1234",
    "modelo": "Honda CG 160",
    "ano": 2023,
    "cor": "Vermelha",
    "filialId": 1
  }'
```

### Buscar Moto por Placa

```bash
curl -X GET "http://localhost:5001/api/v1/moto/por-placa?placa=ABC1234"
```

### Marcar Moto como Indisponível

```bash
curl -X PATCH "http://localhost:5001/api/v1/moto/1/indisponivel"
```

### Criar uma Locação

```bash
curl -X POST "http://localhost:5001/api/v1/locacao" \
  -H "Content-Type: application/json" \
  -d '{
    "motoId": 1,
    "filialId": 1,
    "clienteNome": "João Silva",
    "clienteCpf": "12345678901",
    "clienteTelefone": "(11) 99999-9999",
    "dataInicio": "2024-01-15T10:00:00Z",
    "dataFim": "2024-01-15T18:00:00Z",
    "valorHora": 15.50
  }'
```

### Buscar Locações Ativas

```bash
curl -X GET "http://localhost:5001/api/v1/locacao/ativas"
```

### Finalizar Locação

```bash
curl -X PATCH "http://localhost:5001/api/v1/locacao/1/finalizar"
```

### Testar Health Check

```bash
# Health check geral
curl -X GET "http://localhost:5001/health"

# Health check do banco
curl -X GET "http://localhost:5001/health/ready"

# Health check da aplicação
curl -X GET "http://localhost:5001/health/live"
```

## 🗄️ Estrutura do Banco de Dados (MongoDB)

### Collection: filiais

```json
{
  "_id": 0,
  "Nome": "Filial São Paulo",
  "Endereco": {
    "Logradouro": "Rua das Flores",
    "Numero": "123",
    "Complemento": "Sala 1",
    "Bairro": "Centro",
    "Cidade": "São Paulo",
    "Estado": "SP",
    "CEP": "01234567"
  },
  "Telefone": "(11) 99999-9999",
  "Ativo": true,
  "DataCriacao": "2024-01-15T10:00:00Z",
  "DataAtualizacao": null,
  "Motos": []
}
```

### Collection: motos

```json
{
  "_id": 1,
  "Placa": "ABC1234",
  "Modelo": "Honda CG 160",
  "Ano": 2023,
  "Cor": "Vermelha",
  "Disponivel": true,
  "FilialId": 1,
  "DataCriacao": "2024-01-15T10:00:00Z",
  "DataAtualizacao": null
}
```

### Collection: locacoes

```json
{
  "_id": 1,
  "MotoId": 1,
  "FilialId": 1,
  "ClienteNome": "João Silva",
  "ClienteCpf": "12345678901",
  "ClienteTelefone": "(11) 99999-9999",
  "DataInicio": "2024-01-15T10:00:00Z",
  "DataFim": "2024-01-15T18:00:00Z",
  "ValorHora": 15.50,
  "ValorTotal": 124.00,
  "Status": 2,
  "DataCriacao": "2024-01-15T10:00:00Z",
  "DataAtualizacao": null
}
```

## 🧪 Testes

### Executar a aplicação

```bash
cd MottuApi/MottuApi.Presentation
dotnet run
```

### Verificar se está funcionando

```bash
curl -X GET "http://localhost:5001/api/v1/filial"
```

### Executar testes unitários

```bash
cd MottuApi.Tests
dotnet test
```

## 📄 Arquivos de Suporte

### Documentação

- **`README.md`** - Documentação completa do projeto

### Configuração do MongoDB

O projeto está configurado para usar MongoDB local ou Atlas:

1. **MongoDB Local** (padrão):
   - Host: `localhost:27017`
   - Database: `mottu_db`

2. **MongoDB Atlas** (cloud):
   - Configure a connection string no `appsettings.json`

## 📊 Funcionalidades Implementadas

### ✅ Clean Architecture
- [x] Separação clara de camadas (Presentation, Application, Domain, Infrastructure)
- [x] Inversão de dependência com interfaces
- [x] Lógica de negócio no domínio
- [x] Estrutura limpa sem duplicações

### ✅ Domain-Driven Design
- [x] Entidades ricas com comportamento encapsulado
- [x] Agregado raiz (Filial) que gerencia suas motos
- [x] Value Object (Endereco) imutável
- [x] Interfaces no domínio (IFilialRepository, IMotoRepository)
- [x] Exceções de domínio (DomainException)

### ✅ Clean Code
- [x] Princípio da Responsabilidade Única (SRP)
- [x] Don't Repeat Yourself (DRY)
- [x] Keep It Simple, Stupid (KISS)
- [x] You Aren't Gonna Need It (YAGNI)
- [x] Código limpo e bem organizado

### ✅ Persistência
- [x] MongoDB Driver 2.28.0
- [x] Repositórios MongoDB funcionais
- [x] Configuração via appsettings
- [x] Conexão local e Atlas suportada
- [x] Health Check para MongoDB

### ✅ API RESTful
- [x] 31 endpoints implementados
- [x] Filiais: 7 endpoints (CRUD + ativar/desativar)
- [x] Motos: 9 endpoints (CRUD + disponibilidade + busca por placa/filial)
- [x] Locações: 15 endpoints (CRUD + operações específicas + relatórios)
- [x] Tratamento de exceções
- [x] Validações de domínio
- [x] **Paginação** implementada em todos os endpoints de listagem
- [x] **HATEOAS** com links de navegação
- [x] **Status codes** apropriados (200, 201, 400, 404, 500)

### ✅ Swagger/OpenAPI
- [x] Documentação completa da API
- [x] Descrições detalhadas de endpoints e parâmetros
- [x] Exemplos de payloads para todos os DTOs
- [x] Modelos de dados descritos
- [x] Interface acessível em http://localhost:5001
- [x] **Versionamento** da API (v1, v2)

### ✅ Health Check
- [x] Endpoint `/health` configurado
- [x] Verificação da conectividade com MongoDB
- [x] Health checks específicos (`/health/ready`, `/health/live`)
- [x] Monitoramento da aplicação e banco de dados

### ✅ Testes
- [x] Projeto de testes xUnit
- [x] Testes de integração para todos os endpoints
- [x] Cobertura de cenários de sucesso e erro
- [x] Validação de status codes e respostas

### ✅ Documentação
- [x] README completo e atualizado
- [x] Exemplos de uso com cURL
- [x] Testes unitários com xUnit
- [x] Instruções de configuração do MongoDB

### ✅ Melhorias Implementadas
- [x] **Paginação**: Implementada em todos os endpoints de listagem com metadados
- [x] **HATEOAS**: Links de navegação para melhor descoberta da API
- [x] **Validações**: Data Annotations para validação de modelos
- [x] **Swagger Avançado**: Exemplos de payloads e documentação detalhada
- [x] **Testes xUnit**: Cobertura completa de todos os endpoints
- [x] **Estrutura Limpa**: Remoção de pastas duplicadas e organização otimizada

## 🎯 Status do Projeto

### ✅ **PROJETO 100% FUNCIONAL**

| Critério | Status | Detalhes |
|----------|--------|----------|
| **Clean Architecture** | ✅ | 4 camadas bem separadas |
| **Domain-Driven Design** | ✅ | Entidades ricas + Value Objects |
| **Clean Code** | ✅ | SRP, DRY, KISS, YAGNI aplicados |
| **API RESTful** | ✅ | 31 endpoints funcionando |
| **Paginação** | ✅ | Implementada em todos os listagens |
| **HATEOAS** | ✅ | Links de navegação implementados |
| **Swagger/OpenAPI** | ✅ | Documentação completa + Versionamento |
| **Health Check** | ✅ | Monitoramento MongoDB + Aplicação |
| **Persistência** | ✅ | MongoDB + Repositórios funcionais |
| **Testes** | ✅ | xUnit + Testes de integração |
| **Documentação** | ✅ | README + Swagger + Exemplos |
| **Estrutura** | ✅ | Limpa e organizada |

### 🚀 **Como Executar Rapidamente**

```bash
# 1. Clone e navegue
git clone https://github.com/camargoogui/mottu-api-dotnet.git
cd mottu-api-dotnet

# 2. Execute a aplicação
cd MottuApi/MottuApi.Presentation
dotnet run --urls "http://localhost:5001"

# 3. Teste a API
curl http://localhost:5001/api/v1/filial

# 4. Acesse o Swagger
open http://localhost:5001
```

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'feat: add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📞 Contato

- **Email**: contato@mottu.com.br
- **LinkedIn**: [Mottu](https://linkedin.com/company/mottu)
- **Website**: [www.mottu.com.br](https://www.mottu.com.br)
- **GitHub**: [https://github.com/camargoogui/mottu-api-dotnet.git](https://github.com/camargoogui/mottu-api-dotnet.git)

---

> "Código limpo sempre parece que foi escrito por alguém que se importa."  

> — **Robert C. Martin (Uncle Bob)**

