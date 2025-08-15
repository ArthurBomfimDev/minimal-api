# Minimal API com Arquitetura Limpa e DDD

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-12-green?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019%2B-red?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Status](https://img.shields.io/badge/status-Pronto-brightgreen?style=for-the-badge)

---

## 🚀 Sobre o Projeto

Este é um **Projeto de Estudos** originalmente proposto no **Bootcamp GFT Start #7 .NET** da [Digital Innovation One (DIO)](https://www.dio.me/). Partindo da base inicial, propus-me a evoluir e refatorar o projeto para implementar uma arquitetura robusta, aplicando de fato os princípios do **Domain-Driven Design (DDD)** e da **Clean Architecture**.

Meu foco principal não foi em adicionar novas entidades, mas sim em aprimorar a fundação do software. Isso incluiu a implementação da **Camada de Serviço (Service Layer)** para orquestrar a lógica de negócio, os padrões **Repository** e **Unit of Work**, a criação de **classes base** para reutilização de código, o desenvolvimento de um **sistema robusto de validação e notificação**, a implementação de um **sistema seguro de criptografia de senhas (hashing)** e a **remodelagem das classes** para uma melhor separação de responsabilidades. O resultado é um projeto modular que permite adicionar novas funcionalidades de forma fácil e consistente, seguindo os padrões estabelecidos.

<p align="center">
  <a href="https://www.dio.me/" target="_blank">
    <img src="https://hermes.digitalinnovation.one/assets/diome/logo-full.svg" alt="Logotipo da DIO" width="250">
  </a>
</p>

---

## 🏛️ Arquitetura e Conceitos Aplicados

A base deste projeto é a **Clean Architecture**, que garante um baixo acoplamento e uma alta coesão entre as camadas, tornando o sistema testável, manutenível e independente de frameworks ou bancos de dados.

-   **Domain-Driven Design (DDD):** O coração da aplicação está na camada de Domínio, que contém as entidades e a lógica de negócio pura, sem dependências externas.
-   **Minimal APIs (.NET 8):** Utilização da abordagem moderna e de alta performance para a criação de endpoints HTTP, com código limpo e enxuto.
-   **Camada de Serviço (Service Layer):** Utilização de uma camada de serviço para orquestrar os casos de uso da aplicação, atuando como um intermediário entre a camada de apresentação e a lógica de domínio.
-   **Repository & Unit of Work Patterns:** Abstração da camada de acesso a dados, centralizando a lógica de persistência e garantindo a atomicidade das transações.
-   **Sistema de Validação e Notificação:** A lógica de validação é encapsulada dentro das entidades de domínio, utilizando um "Result Pattern" para retornar sucesso ou uma lista de notificações de erro, evitando o uso de exceções para controle de fluxo.
-   **Entity Framework Core & SQL Server:** Utilização do EF Core como ORM para mapeamento objeto-relacional e persistência dos dados em um banco SQL Server.
-   **Injeção de Dependência:** Configuração centralizada e organizada utilizando métodos de extensão, mantendo o arquivo de inicialização da API limpo e legível.
-   **Autenticação JWT:** Implementação de segurança nos endpoints utilizando JSON Web Tokens.
-   **Segurança de Senhas (Hashing):** As senhas dos utilizadores nunca são armazenadas em texto plano. É utilizado o algoritmo BCrypt para gerar um *hash* seguro de cada senha, garantindo que mesmo em caso de acesso à base de dados, as senhas permaneçam protegidas.

---

### 📂 Estrutura dos Projetos

A solução é dividida em projetos que representam as camadas da Clean Architecture:

-   **`Core/Minimal.Domain`**: Camada mais interna. Contém as entidades, enums e a lógica de negócio pura. Não depende de nenhum outro projeto.
-   **`Core/Minimal.Application`**: Camada de aplicação. Contém as interfaces (contratos) dos serviços e repositórios, DTOs, Input Models e a lógica de orquestração dos casos de uso.
-   **`Infrastructure/Minimal.Infrastructure`**: Camada de infraestrutura. Contém as implementações concretas dos repositórios, o `DbContext` do Entity Framework e outras dependências externas.
-   **`Presentation/Minimal.Api`**: Camada de apresentação. É o ponto de entrada da aplicação, onde os endpoints da Minimal API são definidos.
-   **`Test/Minimal.Test`**: Projeto de testes. Contém os testes unitários e de integração para garantir a qualidade do código.

---

## 🏁 Como Executar o Projeto

Siga os passos abaixo para configurar e executar a aplicação localmente.

### Pré-requisitos

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (Express ou Developer Edition)
-   Uma IDE como [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/) ou [VS Code](https://code.visualstudio.com/).

### Passos

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/ArthurBomfimDev/minimal-api.git](https://github.com/ArthurBomfimDev/minimal-api.git)
    cd minimal-api
    ```

2.  **Configure a Connection String:**
    -   Abra o arquivo `src/Presentation/Minimal.Api/appsettings.json`.
    -   Altere a `DefaultConnection` para apontar para a sua instância do SQL Server.

3.  **Aplique as Migrations:**
    -   Abra um terminal na pasta `src/Infrastructure/Minimal.Infrastructure`.
    -   Execute o comando abaixo para criar o banco de dados e as tabelas:
    ```bash
    dotnet ef database update --startup-project ../../Presentation/Minimal.Api
    ```

4.  **Execute a API:**
    -   Você pode executar o projeto `Minimal.Api` diretamente pelo Visual Studio (pressionando F5) ou pelo terminal, a partir da pasta raiz:
    ```bash
    dotnet run --project src/Presentation/Minimal.Api
    ```

5.  **Acesse o Swagger:**
    -   Abra o seu navegador e acesse a URL indicada no terminal (geralmente `https://localhost:PORTA/swagger`) para ver a documentação da API e testar os endpoints.

---

## 👨‍💻 Autor

**Arthur Bomfim**

-   GitHub: [@ArthurBomfimDev](https://github.com/ArthurBomfimDev)
