# Teste Técnico – Desenvolvedor Backend
**Empresa: Inside Sistemas – Toledo/PR**

## 🧭 Objetivo

Desenvolver uma WebAPI RESTful em ASP.NET que permita gerenciar os pedidos de uma loja com base em regras de negócio específicas e utilizando boas práticas de projeto e arquitetura DDD (Domain-Driven Design).

---

## ✅ Funcionalidades Implementadas

- Iniciar um novo pedido
- Adicionar produtos ao pedido
- Remover produtos do pedido
- Fechar o pedido
- Listar pedidos com paginação e filtro por status (aberto/fechado)
- Obter um pedido específico com seus produtos

---

## 🧠 Regras de Negócio Aplicadas

- Produtos **não podem ser adicionados ou removidos** de pedidos **fechados**
- Um pedido **só pode ser fechado** se contiver **ao menos um produto**

---

## 🔧 Tecnologias e Arquitetura

- ASP.NET Core 8
- Entity Framework Core com banco **InMemory** (ideal para testes)
- Swagger (Swashbuckle)
- Arquitetura **DDD**:
  - Domain (Entidades, Interfaces, Requests, Responses)
  - Application (Serviços e interfaces de caso de uso)
  - Infrastructure (Repositórios e contexto de dados)
  - WebAPI (Controllers)

---

## 📂 Estrutura do Projeto

InsideTest/
├── InsideTest.Domain  
│   ├── Entities/  
│   ├── Interfaces/  
│   ├── Requests/  
│   └── Responses/  
├── InsideTest.Application  
│   ├── Interfaces/  
│   └── Services/  
├── InsideTest.Infrastructure  
│   ├── Data/  
│   └── Repositories/  
├── InsideTest (WebAPI)  
│   ├── Controllers/  
│   └── Program.cs  

---

## 🚀 Como Executar o Projeto

1. Clonar o repositório:

   git clone https://github.com/usuario/inside-pedidos-api.git

2. Acessar a pasta do projeto:

   cd inside-pedidos-api

3. Restaurar os pacotes:

   dotnet restore

4. Rodar o projeto:

   dotnet run --project InsideTest

5. Acessar a documentação da API via Swagger:

   https://localhost:5001/swagger

---

## 📄 Endpoints da API

| Método | Rota                          | Descrição                               |
|--------|-------------------------------|------------------------------------------|
| POST   | /api/pedidos                  | Inicia um novo pedido                    |
| POST   | /api/pedidos/{id}/produtos    | Adiciona um produto ao pedido            |
| DELETE | /api/pedidos/{id}/produtos    | Remove um produto do pedido              |
| POST   | /api/pedidos/{id}/fechar      | Fecha o pedido                           |
| GET    | /api/pedidos                  | Lista pedidos com paginação e filtro     |
| GET    | /api/pedidos/{id}             | Retorna um pedido específico             |

---

## 🧪 Testes Automatizados

A estrutura do projeto já está preparada para testes unitários utilizando **xUnit**, seguindo os princípios de isolamento de lógica e uso de injeção de dependência.  
Os testes incluirão cenários como:

- Início de pedido
- Adição e remoção de produtos
- Regras de fechamento de pedidos
- Paginação e filtragem de listagens

Os testes ficarão organizados na pasta:

InsideTest.Tests/

---

## 📌 Observações Finais

- Toda a API está documentada com comentários XML e exibida no Swagger.
- O projeto utiliza banco de dados InMemory apenas para fins de simplicidade e validação funcional.
- A arquitetura segue fielmente os princípios do DDD, separando responsabilidades em camadas específicas.
