# 🚀 Mottu API - Sistema de Gerenciamento de Motos e Filiais

## 📋 Sobre o Projeto

A **Mottu API** é uma aplicação desenvolvida em .NET 8 que implementa os princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e **Clean Code** para gerenciar motos e filiais da empresa Mottu.

## 👥 Integrantes

- Guilherme Camargo - RM555166 - 2TDSPM
- Icaro Albuquerque - RM555161 - 2TDSPM

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
│   │   └── MotoController.cs       # 9 endpoints para motos
│   ├── Program.cs                  # Configuração da aplicação
│   ├── appsettings.json           # Configurações
│   └── Properties/
├── MottuApi.Application/           # Camada de Aplicação
│   ├── DTOs/
│   │   ├── FilialDTO.cs           # DTOs para filiais
│   │   └── MotoDTO.cs             # DTOs para motos
│   ├── Interfaces/
│   │   ├── IFilialService.cs      # Interface do serviço de filiais
│   │   └── IMotoService.cs        # Interface do serviço de motos
│   ├── Services/
│   │   ├── FilialService.cs       # Lógica de aplicação para filiais
│   │   └── MotoService.cs         # Lógica de aplicação para motos
│   └── Mappings/
│       └── MappingProfile.cs      # Configuração do AutoMapper
├── MottuApi.Domain/               # Camada de Domínio
│   ├── Entities/
│   │   ├── Filial.cs              # Entidade rica (Agregado Raiz)
│   │   └── Moto.cs                # Entidade rica
│   ├── ValueObjects/
│   │   └── Endereco.cs            # Value Object imutável
│   ├── Exceptions/
│   │   └── DomainException.cs     # Exceções de domínio
│   └── Interfaces/
│       ├── IFilialRepository.cs   # Interface do repositório de filiais
│       └── IMotoRepository.cs     # Interface do repositório de motos
└── MottuApi.Infrastructure/       # Camada de Infraestrutura
    ├── Data/
    │   └── ApplicationDbContext.cs # Contexto do EF Core
    ├── Repositories/
    │   ├── FilialRepository.cs    # Implementação do repositório de filiais
    │   └── MotoRepository.cs      # Implementação do repositório de motos
    └── Migrations/                # Migrations do banco de dados
```

### 🎯 Domain-Driven Design (DDD)

- **Entidades Ricas**: `Moto` e `Filial` com comportamento encapsulado
- **Agregado Raiz**: `Filial` como agregado raiz que gerencia suas motos
- **Value Object**: `Endereco` como value object imutável
- **Interfaces no Domínio**: `IFilialRepository` e `IMotoRepository`

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
git clone https://github.com/seu-usuario/mottu-api.git
cd mottu-api
```

### 2. Configure a string de conexão

Edite o arquivo `MottuApi/MottuApi.Presentation/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XE;"
  }
}
```

### 3. Execute as migrations

```bash
cd MottuApi/MottuApi.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../MottuApi.Presentation
dotnet ef database update --startup-project ../MottuApi.Presentation
```

### 4. Execute a aplicação

```bash
cd MottuApi/MottuApi.Presentation
dotnet run
```

A API estará disponível em:
- **HTTP**: `http://localhost:5000`
- **Swagger**: `http://localhost:5000` (raiz)

## 📚 Documentação da API

### Swagger

Acesse a documentação interativa do Swagger em:
- **Desenvolvimento**: `http://localhost:5000`
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

## 📝 Exemplos de Uso

### Criar uma Filial

```bash
curl -X POST "http://localhost:5000/api/filial" \
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
curl -X POST "http://localhost:5000/api/moto" \
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
curl -X GET "http://localhost:5000/api/moto/por-placa?placa=ABC1234"
```

### Marcar Moto como Indisponível

```bash
curl -X PATCH "http://localhost:5000/api/moto/1/indisponivel"
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

## 🧪 Testes

### Executar a aplicação

```bash
cd MottuApi/MottuApi.Presentation
dotnet run
```

### Verificar se está funcionando

```bash
curl -X GET "http://localhost:5000/api/filial"
```

### Executar testes automatizados

```bash
./teste_endpoints.sh
```

## 📄 Arquivos de Suporte

### Scripts e Documentação

- **`script_oracle.sql`** - Script SQL para criação manual das tabelas no Oracle
- **`INSTRUCOES_BANCO.md`** - Instruções detalhadas para configurar o banco Oracle
- **`teste_endpoints.sh`** - Script automatizado para testar todos os 16 endpoints
- **`PROVA.md`** - Especificações originais do projeto

### Configuração do Banco

Se preferir configurar o banco manualmente:

1. **Execute o script SQL**:
   ```bash
   sqlplus usuario/senha@localhost:1521/XE @script_oracle.sql
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
- [x] 16 endpoints implementados
- [x] Filiais: 7 endpoints (CRUD + ativar/desativar)
- [x] Motos: 9 endpoints (CRUD + disponibilidade + busca por placa/filial)
- [x] Tratamento de exceções
- [x] Validações de domínio

### ✅ Documentação e Testes
- [x] Swagger configurado e acessível
- [x] README completo e atualizado
- [x] Exemplos de uso com cURL
- [x] Script de testes automatizados
- [x] Instruções de configuração do banco

## 🎯 Status do Projeto

### ✅ **PROJETO 100% FUNCIONAL**

| Critério | Status | Detalhes |
|----------|--------|----------|
| **Clean Architecture** | ✅ | 4 camadas bem separadas |
| **Domain-Driven Design** | ✅ | Entidades ricas + Value Objects |
| **Clean Code** | ✅ | SRP, DRY, KISS, YAGNI aplicados |
| **API RESTful** | ✅ | 16 endpoints funcionando |
| **Persistência** | ✅ | EF Core + Oracle + Migrations |
| **Documentação** | ✅ | Swagger + README + Exemplos |
| **Testes** | ✅ | Script automatizado + Validações |
| **Estrutura** | ✅ | Limpa e organizada |

### 🚀 **Como Executar Rapidamente**

```bash
# 1. Clone e navegue
git clone <repo>
cd mottu-api

# 2. Execute a aplicação
cd MottuApi/MottuApi.Presentation
dotnet run

# 3. Teste a API
curl http://localhost:5000/api/filial

# 4. Acesse o Swagger
open http://localhost:5000
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

---

> "Código limpo sempre parece que foi escrito por alguém que se importa."  

> — **Robert C. Martin (Uncle Bob)**
