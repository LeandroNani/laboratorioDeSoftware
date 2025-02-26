# 🎓 Sistema de Matrículas - Universidade

Este é um sistema de matrículas para uma universidade, desenvolvido em **C#**, permitindo que alunos realizem matrículas em disciplinas, professores acompanhem os alunos matriculados e a secretaria gerencie os currículos e ofertas de disciplinas.

---

## 📌 Índice

- [📖 Visão Geral](#-visão-geral)
- [🛠 Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [🏗 Arquitetura do Projeto](#-arquitetura-do-projeto)
- [📂 Estrutura de Diretórios](#-estrutura-de-diretórios)
- [🚀 Como Executar o Projeto](#-como-executar-o-projeto)
- [📊 Modelagem UML](#-modelagem-uml)
- [📜 Histórico de Versões](#-histórico-de-versões)

---

## 📖 Visão Geral

O **Sistema de Matrículas** permite a informatização do processo de matrícula na universidade, incluindo funcionalidades para:

✅ Alunos se matricularem em disciplinas obrigatórias e optativas.  
✅ Controle de disponibilidade das disciplinas (mínimo 3 alunos, máximo 60).  
✅ Professores visualizarem suas turmas.  
✅ Notificação do sistema de cobrança ao finalizar matrícula.  
✅ Controle de acesso via autenticação de usuários.  

---

## 🛠 Tecnologias Utilizadas

### **Back-end**
- ASP.NET Core 7.0+

### **Front-end**


### **Banco de Dados**

---

## 🏗 Arquitetura do Projeto

O sistema segue a **arquitetura MVC (Model-View-Controller)** e os seguintes padrões de design:

- **Repository Pattern**: Separação das regras de negócios e acesso aos dados.
- **DTO (Data Transfer Object)**: Para evitar exposição direta de entidades.
- **Dependency Injection**: Melhor gerenciamento de dependências.

### 📌 **Camadas do projeto**
- **WebApp (Apresentação)**: Interface com os usuários.
- **Application (Serviços e Regras de Negócio)**: Contém a lógica do domínio.
- **Infrastructure (Acesso a Dados e Persistência)**: Implementação do EF Core.
- **Domain (Modelagem de Entidades)**: Definição das entidades e interfaces.

---

## 📂 Estrutura de Diretórios

```plaintext
/LAB001
│── /Backend
│   │── /bin                      # Compiled binaries
│   │── /Migrations               # Database migrations
│   │   │──                       # Database 
│   │── /obj                      # Object files
│   │── /Properties               # Project properties
│   │── /src  
│   │   │── /Controllers         
│   │   │── /Data
│   │   │── /DTOs
│   │   │── /Middlewares
│   │   │   │── /Exceptions
│   │   │── /Models
│   │   │── /Services
│   │   │   │── /Helpers
│   │   │   │── /Interfaces
│   │── appsettings.Development.json # Development settings
│   │── appsettings.json          # Application settings
│   │── Backend.csproj            # Project file
│   │── Backend.sln               # Solution file
│   │── Program.cs                # Main program file
│   │── README.md                 # Backend documentation
│── /doc
│   │── README.md                 # Documentation
│── /Frontend
│   │── /sistema-de-matriculas    # Frontend source code
│   │   │── /public
│   │   │── /src
│   │   │   │── /api
│   │   │   │   │── /lib
│   │   │   │── /app
│   │   │   │   │── /(pages)
│   │   │   │── /types
│── /images
│   │── casosdeuso.png            # Use case diagram
│   │── diagrama_de_classes_v2.0.png # Class diagram v2.0
│   │── diagramadeclasses.png     # Class diagram
│── README.md                     # Project documentation
```` 
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
dotnet run --project SistemaMatriculas.Web
````