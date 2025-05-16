# MottuApi (.NET) — Mapeamento Inteligente de Motos no Pátio

API RESTful desenvolvida em ASP.NET Core para cadastro, consulta e gerenciamento de motos e filiais, integrando-se ao banco Oracle via Entity Framework Core.

## Funcionalidades

- CRUD completo de **Filial**
- CRUD completo de **Moto**
- Relacionamento Moto ↔ Filial
- Documentação automática via Swagger/OpenAPI

## Rotas Principais

### Filial

- `GET /api/filial` — Lista todas as filiais
- `GET /api/filial/{id}` — Busca filial por ID
- `POST /api/filial` — Cria uma nova filial
- `PUT /api/filial/{id}` — Atualiza uma filial existente
- `DELETE /api/filial/{id}` — Remove uma filial

### Moto

- `GET /api/moto` — Lista todas as motos
- `GET /api/moto/{id}` — Busca moto por ID
- `GET /api/moto/por-filial/{filialId}` — Lista motos de uma filial
- `GET /api/moto/por-placa?placa=XXX1234` — Busca moto por placa
- `POST /api/moto` — Cria uma nova moto
- `PUT /api/moto/{id}` — Atualiza uma moto existente
- `DELETE /api/moto/{id}` — Remove uma moto

## Instalação e Execução

1. **Pré-requisitos:**

   - .NET 7 ou superior
   - Banco Oracle (ou aguarde para rodar o banco depois)

2. **Clone o repositório:**

   ```sh
   git clone https://github.com/seu-usuario/mottu-api-dotnet.git
   cd mottu-api-dotnet/MottuApi
   ```

3. **Restaure os pacotes:**

   ```sh
   dotnet restore
   ```

4. **Configure a connection string do Oracle** em `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "OracleConnection": "User Id=usuario;Password=senha;Data Source=localhost:1521/XE;"
   }
   ```

5. **Crie as tabelas (quando o Oracle estiver disponível):**

   ```sh
   dotnet ef database update
   ```

6. **Execute a API:**

   ```sh
   dotnet run
   ```

7. **Acesse o Swagger:**
   ```
   http://localhost:5231/swagger
   ```

## Observações

- Enquanto o Oracle não estiver disponível, os endpoints retornarão erro 500 ao tentar acessar o banco.
- O Swagger estará disponível para consulta e documentação da API.
- O projeto segue o padrão de camadas: Controllers, Services, Repositories, DTOs, Models.

---

**Organização e apresentação do código são avaliadas!**  
Qualquer dúvida, consulte o código ou abra uma issue no repositório.
