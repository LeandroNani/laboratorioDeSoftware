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
/SistemaMatriculas
│── /SistemaMatriculas.Web          # Camada de apresentação (MVC)
│── /SistemaMatriculas.Application  # Serviços e Regras de Negócio
│── /SistemaMatriculas.Domain       # Modelagem do Domínio
│── /SistemaMatriculas.Infrastructure # Banco de dados e Repositórios
│── /SistemaMatriculas.Tests        # Testes Unitários
│── SistemaMatriculas.sln           # Solução do projeto
│── README.md                       # Documentação do projeto
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

## 📜 Histórico de Versões
### 📅 Sprint 1
Criado modelo de caso de uso e histórias de usuário
Implementação inicial da API REST.

---
# Histórias de Usuário

| Número | Ator       | Descrição                                                                                   |
|--------|------------|---------------------------------------------------------------------------------------------|
| 01     | Admin      | O admin deve gerenciar cada curso (Nome, Número de Créditos, Disciplinas).                  |
| 02     | Usuário    | O usuário deve logar no sistema como admin, aluno ou professor.                             |
| 03     | Aluno      | O aluno deve poder se matricular em uma disciplina, seguindo as restrições definidas.       |
| 04     | Aluno      | O aluno deve poder cancelar as matrículas feitas no período certo.                          |
| 05     | Aluno      | O aluno só terá sua disciplina confirmada se houver ao menos 3 alunos matriculados.         |
| 06     | Aluno      | O aluno deve ser notificado e cobrado pelas disciplinas daquele semestre.                   |
| 07     | Professor  | O professor deve poder ver quais são os alunos matriculados em cada disciplina.             |

# Casos De uso 
[Casos de Uso no Figma](https://www.figma.com/board/wgrxUGQZNYCmVDGKUjQr75/DIAGRAMA-DE-CASO-DE-USO-(Community)?node-id=0-1&t=yvtsfLDaWPLSANSA-1)
![alt text](/LAB001/images/casosdeuso.png)

# Diagrama de Classes
[Diagrama de classes no Figma](https://www.figma.com/board/nv4dDPlsAJE6QxKSL0CkC2/Class-Diagram-Template-(Community)?node-id=0-1&t=e14welY9WEIl3acg-1)
![alt text](/LAB001/images/diagramadeclasses.png)