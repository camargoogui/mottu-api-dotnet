oi# 🚀 CP4 - 2TDS
## CLEAN CODE, DDD E CLEAN ARCH COM .NET - 2025

Bem-vindo ao **Checkpoint 4!**  
Neste desafio, você irá aplicar os fundamentos de **Clean Code**, **Domain-Driven Design (DDD)** e **Clean Architecture** para criar uma API escalável, legível e bem estruturada, refletindo práticas profissionais do mercado.

---

## 🎯 Objetivo

Desenvolver ou refatorar uma **WebAPI em .NET 8**, utilizando os conceitos de **DDD**, **Clean Architecture** e os princípios de **Clean Code**, com base no **projeto do Challenge da Mottu** (ou outro domínio real, bem definido).

---

## 👥 Trabalho em Grupo

- ✅ Este trabalho pode ser realizado **em grupo**, desde que os integrantes estejam oficialmente vinculados ao projeto do **Challenge (Mottu)** ou outro projeto conjunto validado.
- ❌ Projetos idênticos entre grupos diferentes, ou com participantes que não pertencem ao grupo, terão nota 0.
- ❌ Códigos que não compilam ou sem estrutura mínima não serão avaliados.

---

## 📦 Entrega

- O projeto deve ser entregue via **repositório público no GitHub**
- Somente um integrante deverá entregar
- A entrega deverá ser feita via portal do aluno, Obs.: SOMENTE O LINK DO GIT, NÃO PRECISA ENVIAR O CODIGO
- O repositório deve conter:
  - Incluir no readme os integrantes do grupo 
  - Código-fonte organizado
  - README completo
  - Swagger funcional

---

## 📐 O que será avaliado?

### 1. Arquitetura em camadas (Clean Architecture) – até **2 pontos**

Organização do projeto em camadas:

```plaintext
📦 src
 ┣ 📂 Api             -> Controllers, validações de entrada
 ┣ 📂 Application     -> Casos de uso, DTOs
 ┣ 📂 Domain          -> Entidades, Value Objects, Interfaces
 ┗ 📂 Infrastructure  -> Acesso a dados, serviços externos
```


**Critérios:**
- Baixo acoplamento entre as camadas
- Inversão de dependência aplicada corretamente
- Lógica de negócio concentrada no domínio

---

### 2. Domain-Driven Design (DDD) – até **3 pontos**

- Mínimo de **duas entidades ricas**, com comportamento (não apenas dados)
- Pelo menos **um Agregado Raiz**
- Pelo menos **um Value Object**
- Interface de repositório definida dentro do **Domínio**

**Critérios:**
- Modelagem clara e orientada a comportamento
- Separação entre Entidades e VOs
- Regras de negócio encapsuladas corretamente

---

### 3. Clean Code (Boas práticas) – até **3 pontos**

Avaliação com base nos princípios:

- **SRP - Single Responsibility Principle:** Uma responsabilidade por classe/método  
- **DRY - Don’t Repeat Yourself:** Evite duplicação de código  
- **KISS - Keep It Simple, Stupid:** Soluções simples, diretas e eficazes  
- **YAGNI - You Aren’t Gonna Need It:** Implemente apenas o necessário

**Também será avaliado:**
- Nomeação clara
- Métodos pequenos e legíveis
- Separação de responsabilidades
- Código limpo e sem trechos comentados desnecessários

---

### 4. Persistência + Migrations – até **1 ponto**

- Uso do **Entity Framework Core**
- Migrations criadas e aplicáveis
- Conexão com o banco via `appsettings` ou variáveis de ambiente
- Instruções no README para executar localmente

---

### 5. Documentação com Swagger + README – até **1 ponto**

- Swagger configurado com todos os endpoints visíveis
- README com:
  - Descrição do domínio
  - Instruções de execução
  - Exemplos de requisições (GET, POST, etc)

---

### 6. Organização do GitHub + Commits – até **1 ponto**

Será avaliada a organização do repositório:

- Uso de **commits semânticos** (conventional commits):  
  Ex: `feat: criar endpoint de cadastro`, `fix: corrigir validação`
- Histórico limpo e com mensagens claras
- Branch com nomenclatura padronizada (ex: `feature/RM12345`)
- Estrutura de pastas organizada
- Arquivos desnecessários removidos

---

## 🧠 Dicas

- Modele bem o domínio antes de iniciar a codificação
- Separe responsabilidades entre camadas de forma clara
- Use DTOs em vez de entidades diretamente nos Controllers
- Utilize `dotnet format` ou `ReSharper` para revisar seu código
- Commits devem contar **o que foi feito** e **por que foi feito**

---

## 📝 Avaliação Final

| Critério                                  | Pontos Máximos |
|------------------------------------------|----------------|
| Clean Architecture                       | até 2          |
| Domain-Driven Design (DDD)               | até 3          |
| Clean Code (SRP, DRY, KISS, YAGNI)       | até 3          |
| Persistência + Migrations                | até 1          |
| Swagger + README                         | até 1          |
| Organização do GitHub + Commits          | até 1          |
| **Total Máximo**                         | **10 pontos**  |

---

## ✍️ Autores

- [@ProfThiagoVicco](https://github.com/ProfThiagoVicco)

---

## 🌟 Propósito

> “Código limpo sempre parece que foi escrito por alguém que se importa.”  
> — **Robert C. Martin (Uncle Bob)**
