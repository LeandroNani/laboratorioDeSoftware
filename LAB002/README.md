# 🚗 Sistema de Aluguel de Carros

Este é um sistema de aluguel de carros, desenvolvido em **C#**, permitindo que clientes realizem pedidos de aluguel, agentes aprovem ou neguem seus pedidos com base nos rendimentos do cliente e admins gerenciem os automóveis da locadora, bem como cadastrem novos agentes. Caso a solicitação de aluguel do cliente seja aprovado, o sistema gera um contrato entre o Cliente e o Agente.

---

## 📌 Índice

- [📖 Visão Geral](#-visão-geral)
- [🛠 Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [🏗 Arquitetura do Projeto](#-arquitetura-do-projeto)
- [📁 Estrutura do Projeto](#-estrutura-do-projeto)
- [🚀 Como Executar o Projeto](#-como-executar-o-projeto)
- [📊 Diagrama de Classe](#-diagrama-de-classe)


---

## 📖 Visão Geral

O **Sistema de Aluguel de Carros** permite a informatização do processo de alugar um carro, incluindo funcionalidades para:

✅ Clientes se registrarem na plataforma.  
✅ Clientes introduzam, modificam, consultam e cancelam pedidos de aluguel de automóveis.  
✅ Administradores cadastrem automóveis na locadora.
✅ Administradores cadastrem novos agentes atuantes na locadora.
✅ Agentes aprovam ou negam os pedidos de aluguel emitidos pelos cliente.
✅ Agentes visualizam todos os pedidos de aluguel vinculados a eles.
✅ Contratos são gerados com base na aprovação do pedido de aluguel.

---

## 🛠 Tecnologias Utilizadas

### **Back-end**
- *ASP.NET Core 7.0+*

### **Front-end**

- *Next.js (v15.2.4)*  
  Framework React com renderização híbrida, roteamento automático e otimizações.

- *React (v19.0.0)*  
  Biblioteca para construção de interfaces reativas e modulares.

- *TypeScript (v5.x)*  
  Superset do JavaScript com tipagem estática e maior segurança.

---
## 🎨 Estilização

- *Tailwind CSS (v4.x)*  
  Estilização utilitária com classes direto no JSX.

- *PostCSS + LightningCSS*  
  Processamento e otimização de CSS.


### **Banco de Dados**
- PostgreSQL

## 🏗 Arquitetura do Projeto

O projeto está dividido em duas camadas principais:

### 🔹 frontend/
Aplicação web desenvolvida com *Next.js, **React* e *Tailwind CSS*, estruturada por responsabilidades. Cada tipo de usuário (Cliente, Agente e Administrador) possui telas dedicadas e isoladas, com comunicação via requisições HTTP para a API. O código é modular e organizado por componentes reutilizáveis, facilitando a manutenção e escalabilidade.

### 🔸 backend/
API desenvolvida em *.NET*, seguindo boas práticas de separação de responsabilidades:

- *Controllers*: Ponto de entrada das requisições HTTP. Responsáveis por receber e repassar os dados corretamente às camadas de negócio.
- *DTOs (Data Transfer Objects)*: Classes auxiliares para transferência de dados entre a API e o cliente, garantindo segurança e clareza.
- *Models*: Representações das entidades do domínio (ex: Cliente, Pedido, Automovel, etc).
- *Services*: Contêm a lógica de negócio e são responsáveis pelo processamento dos dados.
- *Data*: Responsável pela configuração do banco de dados, contexto (DbContext) e acesso aos dados.
- *Migrations*: Controle e versionamento da estrutura do banco de dados utilizando Entity Framework.

Além disso, segue a **arquitetura MVC (Model-View-Controller)** e os seguintes padrões de design:

- **Repository Pattern**: Separação das regras de negócios e acesso aos dados.
- **DTO (Data Transfer Object)**: Para evitar exposição direta de entidades.
- **Dependency Injection**: Melhor gerenciamento de dependências.

## 📁 Estrutura do Projeto
```plaintex
├── /Backend.API                         # Projeto back-end em .NET
│   ├── appsettings.json                # Configurações da aplicação
│   ├── appsettings.Development.json   # Configurações para ambiente de desenvolvimento
│   ├── Backend.API.csproj              # Arquivo de definição do projeto .NET
│   ├── Program.cs                      # Ponto de entrada da aplicação
│   ├── Backend.API.http                # Arquivo de testes de requisições HTTP
│
│   ├── /Migrations                     # Histórico e versionamento do banco de dados (EF Core)
│   │   └── *.cs                        # Arquivos de migração
│
│   ├── /Properties
│   │   └── launchSettings.json         # Configurações de inicialização do projeto
│
│   └── /src
│       ├── /Controllers                # Controllers com endpoints da API
│       ├── /Data                       # Contexto do banco de dados (AppDbContext)
│       ├── /DTOs                       # Data Transfer Objects usados na comunicação entre camadas
│       ├── /Model                      # Entidades que representam o domínio da aplicação
│       └── /Services                   # Lógica de negócio e interfaces de serviços

├── /frontend                            # Projeto front-end com Next.js + Tailwind
│   └── /app
│       ├── favicon.ico                 # Ícone da aba do navegador
│       ├── globals.css                 # Estilos globais da aplicação
│       ├── layout.tsx                  # Layout base da aplicação
│       ├── page.tsx                    # Página inicial
│
│       ├── /admin                      # Interface da área administrativa
│       ├── /agente                     # Interface do agente
│       ├── /cliente                    # Interface do cliente
│       ├── /login                      # Página de login
│       └── /register                   # Página de registro

```

### 📌 **Camadas do projeto**
- **WebApp (Apresentação)**: Interface com os usuários.
- **Application (Serviços e Regras de Negócio)**: Contém a lógica do domínio.
- **Infrastructure (Acesso a Dados e Persistência)**: Implementação do EF Core.
- **Domain (Modelagem de Entidades)**: Definição das entidades e interfaces.

---

## 🚀 Como Executar o Projeto
### 🔧 **Pré-requisitos**
- .NET 7.0+ SDK
- SQL Server ou SQLite
### **🏃‍♂️ Passos para rodar a aplicação**
Clone o repositório:
````bash
git clone https://github.com/seu-usuario/sistema-matriculas.git
cd sistema-matriculas
````
Instale as dependências:

```` bash
dotnet restore
````

### **Configure o banco de dados (ver seção abaixo).**

Inicie o projeto:

````bash
dotnet run
````

## Diagrama de Classe
![Diagrama de Componentes](https://github.com/LeandroNani/laboratorioDeSoftware/blob/2f715b7518ca4da62f18a379043fed5f9faeb64a/LAB002/Artefatos/dc_aluguel_carros_v2.png)

## Endpoints

