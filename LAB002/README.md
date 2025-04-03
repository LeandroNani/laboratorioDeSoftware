# 🚗 Sistema de Aluguel de Carros

Este é um sistema de aluguel de carros, desenvolvido em **C#**, permitindo que clientes realizem pedidos de aluguel, agentes aprovem ou neguem seus pedidos com base nos rendimentos do cliente e admins gerenciem os automóveis da locadora, bem como cadastrem novos agentes. Caso a solicitação de aluguel do cliente seja aprovado, o sistema gera um contrato entre o Cliente e o Agente.

---

## 📌 Índice

- [📖 Visão Geral](#-visão-geral)
- [🛠 Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [🚀 Como Executar o Projeto](#-como-executar-o-projeto)
- [📊 Diagrama de Classe](#-modelagem-uml)

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

## Diagrama de Classe
![Diagrama de Componentes](https://raw.githubusercontent.com/imcathalat/projeto-de-software/main/sgo/artefatos/DiagramaDeComponentes.jpg)
