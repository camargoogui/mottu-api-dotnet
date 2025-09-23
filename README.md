# 🚀 Mottu API - Sistema de Gerenciamento de Motos e Filiais

## 📋 Sobre o Projeto

A **Mottu API** é uma aplicação desenvolvida em .NET 8 que implementa os princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e **Clean Code** para gerenciar motos e filiais da empresa Mottu.

## 👥 Integrantes

- **Guilherme Camargo** - RM555166 - 2TDSPM
- **Icaro Albuquerque** - RM555131 - 2TDSPM

## 🔗 Repositório GitHub

**Link do Projeto**: [https://github.com/camargoogui/DOTNET---Challenge.git](https://github.com/camargoogui/DOTNET---Challenge.git)

## 🏗️ Arquitetura

O projeto segue a **Clean Architecture** com separação clara de responsabilidades em 4 camadas:

```
📦 MottuApi
 ┣ 📂 MottuApi.Presentation    -> Controllers, Program.cs, appsettings
 ┣ 📂 MottuApi.Application     -> Services, DTOs, Interfaces, Mappings
 ┣ 📂 MottuApi.Domain          -> Entities, ValueObjects, Exceptions, Interfaces
 ┗ 📂 MottuApi.Infrastructure  -> DbContext, Repositories, Migrations
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
    │   └── ApplicationDbContext.cs # Contexto do EF Core
    ├── Repositories/
    │   ├── FilialRepository.cs    # Implementação do repositório de filiais
    │   ├── MotoRepository.cs      # Implementação do repositório de motos
    │   └── LocacaoRepository.cs   # Implementação do repositório de locações
    └── Migrations/                # Migrations do banco de dados
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
- **Entity Framework Core 8.0.4**
- **Oracle Database** (Oracle.EntityFrameworkCore 8.21.121)
- **AutoMapper 12.0.1**
- **Swagger/OpenAPI 6.5.0**
- **ASP.NET Core Web API**

## 🚀 Como Executar

### Pré-requisitos

- .NET 8 SDK
- Oracle Database (ou Docker com Oracle XE)
- Visual Studio 2022 ou VS Code

### 1. Clone o repositório

```bash
git clone https://github.com/camargoogui/DOTNET---Challenge.git
cd DOTNET---Challenge
```

### 2. Configure a string de conexão

**⚠️ IMPORTANTE**: O arquivo `appsettings.json` está incluído no repositório com credenciais genéricas.

**Edite o arquivo** `MottuApi/MottuApi.Presentation/appsettings.json` substituindo as credenciais:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL;"
  }
}
```

**Substitua:**
- `SEU_USUARIO` → Seu usuário do Oracle
- `SUA_SENHA` → Sua senha do Oracle

**Configuração recomendada para Oracle da FIAP:**
- **Host**: `oracle.fiap.com.br`
- **Porta**: `1521`
- **SID**: `ORCL`
- **Usuário**: Seu RM da FIAP
- **Senha**: Sua senha da FIAP

### 3. Execute as migrations

```bash
cd MottuApi/MottuApi.Presentation
dotnet ef database update
```

### 4. Execute a aplicação

```bash
cd MottuApi/MottuApi.Presentation
dotnet run --urls "http://0.0.0.0:5001"
```

A API estará disponível em:
- **HTTP**: `http://localhost:5001`
- **Swagger**: `http://localhost:5001` (raiz)

## 📚 Documentação da API

### Swagger

Acesse a documentação interativa do Swagger em:
- **Desenvolvimento**: `http://localhost:5001`
- **Produção**: `https://sua-api.com`

### Endpoints Principais

#### 🏢 Filiais

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/filial` | Listar todas as filiais |
| GET | `/api/filial/{id}` | Buscar filial por ID |
| POST | `/api/filial` | Criar nova filial |
| PUT | `/api/filial/{id}` | Atualizar filial |
| DELETE | `/api/filial/{id}` | Excluir filial |
| PATCH | `/api/filial/{id}/ativar` | Ativar filial |
| PATCH | `/api/filial/{id}/desativar` | Desativar filial |

#### 🏍️ Motos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/moto` | Listar todas as motos |
| GET | `/api/moto/{id}` | Buscar moto por ID |
| GET | `/api/moto/por-placa?placa=ABC1234` | Buscar moto por placa |
| GET | `/api/moto/por-filial/{filialId}` | Listar motos de uma filial |
| POST | `/api/moto` | Criar nova moto |
| PUT | `/api/moto/{id}` | Atualizar moto |
| DELETE | `/api/moto/{id}` | Excluir moto |
| PATCH | `/api/moto/{id}/disponivel` | Marcar moto como disponível |
| PATCH | `/api/moto/{id}/indisponivel` | Marcar moto como indisponível |

#### 🚗 Locações

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/locacao` | Listar todas as locações (com paginação) |
| GET | `/api/locacao/{id}` | Buscar locação por ID |
| GET | `/api/locacao/por-moto/{motoId}` | Listar locações de uma moto |
| GET | `/api/locacao/por-filial/{filialId}` | Listar locações de uma filial |
| GET | `/api/locacao/por-cliente?cpf=12345678901` | Buscar locações por CPF do cliente |
| GET | `/api/locacao/por-periodo?inicio=2024-01-01&fim=2024-12-31` | Buscar locações por período |
| GET | `/api/locacao/ativas` | Listar locações ativas |
| GET | `/api/locacao/finalizadas` | Listar locações finalizadas |
| POST | `/api/locacao` | Criar nova locação |
| PUT | `/api/locacao/{id}` | Atualizar locação |
| DELETE | `/api/locacao/{id}` | Excluir locação |
| PATCH | `/api/locacao/{id}/iniciar` | Iniciar locação |
| PATCH | `/api/locacao/{id}/finalizar` | Finalizar locação |
| PATCH | `/api/locacao/{id}/cancelar` | Cancelar locação |
| GET | `/api/locacao/{id}/calcular-valor` | Calcular valor total da locação |

## 📝 Exemplos de Uso

### Criar uma Filial

```bash
curl -X POST "http://localhost:5001/api/filial" \
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
curl -X POST "http://localhost:5001/api/moto" \
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
curl -X GET "http://localhost:5001/api/moto/por-placa?placa=ABC1234"
```

### Marcar Moto como Indisponível

```bash
curl -X PATCH "http://localhost:5001/api/moto/1/indisponivel"
```

### Criar uma Locação

```bash
curl -X POST "http://localhost:5001/api/locacao" \
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
curl -X GET "http://localhost:5001/api/locacao/ativas"
```

### Finalizar Locação

```bash
curl -X PATCH "http://localhost:5001/api/locacao/1/finalizar"
```

## 🗄️ Estrutura do Banco de Dados

### Tabela: Filiais

| Campo | Tipo | Descrição |
|-------|------|-----------|
| Id | INT | Chave primária |
| Nome | VARCHAR(100) | Nome da filial |
| Logradouro | VARCHAR(100) | Logradouro do endereço |
| Numero | VARCHAR(10) | Número do endereço |
| Complemento | VARCHAR(50) | Complemento do endereço |
| Bairro | VARCHAR(50) | Bairro |
| Cidade | VARCHAR(50) | Cidade |
| Estado | VARCHAR(2) | Estado (UF) |
| CEP | VARCHAR(8) | CEP |
| Telefone | VARCHAR(15) | Telefone |
| Ativo | BOOLEAN | Status da filial |
| DataCriacao | DATETIME | Data de criação |
| DataAtualizacao | DATETIME | Data de atualização |

### Tabela: Motos

| Campo | Tipo | Descrição |
|-------|------|-----------|
| Id | INT | Chave primária |
| Placa | VARCHAR(7) | Placa da moto |
| Modelo | VARCHAR(50) | Modelo da moto |
| Ano | INT | Ano da moto |
| Cor | VARCHAR(30) | Cor da moto |
| Disponivel | BOOLEAN | Status de disponibilidade |
| FilialId | INT | FK para Filial |
| DataCriacao | DATETIME | Data de criação |
| DataAtualizacao | DATETIME | Data de atualização |

### Tabela: Locações

| Campo | Tipo | Descrição |
|-------|------|-----------|
| Id | INT | Chave primária |
| MotoId | INT | FK para Moto |
| FilialId | INT | FK para Filial |
| ClienteNome | VARCHAR(100) | Nome do cliente |
| ClienteCpf | VARCHAR(11) | CPF do cliente |
| ClienteTelefone | VARCHAR(15) | Telefone do cliente |
| DataInicio | DATETIME | Data de início da locação |
| DataFim | DATETIME | Data de fim da locação |
| ValorHora | DECIMAL(10,2) | Valor por hora |
| ValorTotal | DECIMAL(10,2) | Valor total calculado |
| Status | INT | Status da locação (1=Solicitada, 2=Iniciada, 3=Finalizada, 4=Cancelada) |
| DataCriacao | DATETIME | Data de criação |
| DataAtualizacao | DATETIME | Data de atualização |

## 🧪 Testes

### Executar a aplicação

```bash
cd MottuApi/MottuApi.Presentation
dotnet run
```

### Verificar se está funcionando

```bash
curl -X GET "http://localhost:5001/api/filial"
```

### Executar testes unitários

```bash
cd MottuApi.Tests
dotnet test
```

## 📄 Arquivos de Suporte

### Scripts e Documentação

- **`script_oracle.sql`** - Script SQL para criação manual das tabelas no Oracle
- **`INSTRUCOES_BANCO.md`** - Instruções detalhadas para configurar o banco Oracle
- **`PROVA.md`** - Especificações originais do projeto

### Configuração do Banco

Se preferir configurar o banco manualmente:

1. **Execute o script SQL**:
   ```bash
   sqlplus SEU_USUARIO/SUA_SENHA@oracle.fiap.com.br:1521/ORCL @script_oracle.sql
   ```

2. **Ou siga as instruções** em `INSTRUCOES_BANCO.md`

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
- [x] Entity Framework Core 8.0.4
- [x] Migrations funcionais para Oracle
- [x] Configuração via appsettings
- [x] Script SQL para criação manual

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

### ✅ Testes
- [x] Projeto de testes xUnit
- [x] Testes de integração para todos os endpoints
- [x] Cobertura de cenários de sucesso e erro
- [x] Validação de status codes e respostas

### ✅ Documentação
- [x] README completo e atualizado
- [x] Exemplos de uso com cURL
- [x] Testes unitários com xUnit
- [x] Instruções de configuração do banco

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
| **Swagger/OpenAPI** | ✅ | Documentação completa |
| **Persistência** | ✅ | EF Core + Oracle + Migrations |
| **Testes** | ✅ | xUnit + Testes de integração |
| **Documentação** | ✅ | README + Swagger + Exemplos |
| **Estrutura** | ✅ | Limpa e organizada |

### 🚀 **Como Executar Rapidamente**

```bash
# 1. Clone e navegue
git clone https://github.com/camargoogui/DOTNET---Challenge.git
cd DOTNET---Challenge

# 2. Execute a aplicação
cd MottuApi/MottuApi.Presentation
dotnet run --urls "http://localhost:5001"

# 3. Teste a API
curl http://localhost:5001/api/filial

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
- **GitHub**: [https://github.com/camargoogui/DOTNET---Challenge.git](https://github.com/camargoogui/DOTNET---Challenge.git)

---

> "Código limpo sempre parece que foi escrito por alguém que se importa."  

> — **Robert C. Martin (Uncle Bob)**

