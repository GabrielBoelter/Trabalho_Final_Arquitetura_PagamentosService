# 💰 Academia - Microsserviço: PagamentosService

Este repositório faz parte de um sistema de academia desenvolvido com **C# (.NET 8)** e arquitetura de **microsserviços**, utilizando **SQLite** como persistência local e comunicação via **HTTP POST** entre os serviços.

## 🎯 Propósito do Microsserviço

Gerenciar e registrar os pagamentos mensais dos alunos da academia. Além disso, este serviço é responsável por identificar inadimplência e notificar o microsserviço de alunos, alterando o status do aluno para "inadimplente".

## 👥 Usuários do Sistema

- **Funcionários da recepção**: realizam os registros e consultas de pagamentos.
- **Administradores da academia**: verificam status financeiros e relatórios de adimplência.

---

## ✅ Requisitos Funcionais Atendidos

- RF03: Registrar pagamentos mensais para um aluno.
- RF05: Detectar inadimplência e atualizar status do aluno via POST.
- RF08: Consultar todos os pagamentos de um aluno.
- RF09: Verificar se um aluno está com pagamentos em dia.

---

## 🔁 Integrações Entre Microsserviços

| Tipo de Integração | Serviço Origem     | Serviço Destino     | Ação Realizada                                                                  |
|--------------------|--------------------|----------------------|----------------------------------------------------------------------------------|
| POST (alteração)   | PagamentosService  | AlunosService        | Atualiza o status do aluno para "inadimplente" se não houver pagamento registrado |

---

## 📦 Estrutura do Projeto

O projeto segue o padrão em camadas, com pastas separadas por responsabilidade:

```
PagamentosService/
├── Controllers/               # Exposição de endpoints REST
│   └── PagamentosController.cs
├── DTOs/                      # Objetos de transferência de dados
│   ├── PagamentoCreateDTO.cs
│   ├── PagamentoRequestDTO.cs
│   ├── PagamentoResponseDTO.cs
│   └── PagamentoUpdateDTO.cs
├── Models/                    # Entidades de domínio
│   ├── Pagamento.cs
│   └── AcessoTreino.cs
├── Repositories/              # Interfaces e implementações de acesso a dados
│   ├── IPagamentoRepository.cs
│   ├── PagamentoRepository.cs
│   ├── IAcessoTreinoRepository.cs
│   └── AcessoTreinoRepository.cs
├── Services/                  # Regras de negócio
│   ├── PagamentoService.cs
│   └── AcessoTreinoService.cs
├── Data/                      # Contexto e configuração do banco de dados
│   ├── AppDbContext.cs
│   └── AppDbContextFactory.cs
├── Migrations/                # Histórico de migrações do banco SQLite
│   ├── 20250619205220_InitialCreate.cs
│   └── AppDbContextModelSnapshot.cs
├── External/                  # Comunicação com TreinosService (e futuramente AlunosService)
│   ├── TreinosServiceClient.cs
│   └── TreinoExternalDTO.cs
├── appsettings.json           # Configuração da conexão com o banco
├── pagamentos.db              # Arquivo do banco SQLite gerado
├── Program.cs                 # Configuração da aplicação e injeção de dependências
└── PagamentosService.http     # Arquivo de teste de requisições via HTTP
```

---

## 🔗 Endpoints Disponíveis

| Verbo | Rota                                     | Ação                                               |
|-------|------------------------------------------|----------------------------------------------------|
| POST  | `/api/pagamentos`                        | Registra um novo pagamento para um aluno           |
| GET   | `/api/pagamentos/aluno/{alunoId}`        | Retorna todos os pagamentos de um aluno            |
| GET   | `/api/pagamentos/status/{alunoId}`       | Verifica se o aluno está em dia com os pagamentos  |
| PUT   | `/api/pagamentos/{id}`                   | Atualiza um pagamento existente (ex: corrigir data)|
| POST  | `/api/pagamentos/sinalizar-inadimplente` | Envia POST ao AlunosService para marcar inadimplência |

> ⚠️ Endpoints de integração utilizam `HttpClient` e JSON para se comunicar com outros microsserviços.

---

## 💾 Banco de Dados

- Utiliza **SQLite** como armazenamento local leve e simples.
- Configurado via `appsettings.json` com o caminho para `pagamentos.db`.
- Gerenciado com **Entity Framework Core**, utilizando `DbContext`, `DbSet<>` e migrações.

---

## 🛠️ Tecnologias e Ferramentas

- **.NET 8 Web API**
- **C#**
- **Entity Framework Core**
- **SQLite**
- **Swagger** (documentação automática de API)
- **Postman / .http file** (para testes)
- **Arquitetura em camadas** (Controller, Service, Repository, DTOs, Models)
- **Injeção de Dependência** nativa do ASP.NET Core

---

## 🚀 Como Executar Localmente

1. Clone o repositório:
   ```bash
   git clone https://github.com/GabrielBoelter/Trabalho_Final_Arquitetura_PagamentosService
   ```

2. Abra a solução no **Visual Studio 2022**.

3. Execute as migrações (se necessário):
   ```bash
   dotnet ef database update
   ```

4. Rode o projeto (`F5`) e acesse o Swagger:
   ```
   https://localhost:xxxx/swagger
   ```

5. Teste os endpoints diretamente via Swagger ou Postman.

---

## 📂 Repositórios Relacionados

- [`academia-alunos-service`](https://github.com/GabrielBoelter/Trabalho_Final_Arquitetura_AlunosService)
- [`academia-treinos-service`](https://github.com/GabrielBoelter/Trabalho_Final_Arquitetura_TreinosService)

---

## 👨‍🏫 Desenvolvido para a disciplina de Arquitetura de Software  
**Centro Universitário SATC** – Prof. Eduardo Cizeski Meneghel  
