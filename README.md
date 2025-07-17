
# 🛍️ ShopAPP – Sistema de Gestão de Loja Online

**ShopAPP** é uma aplicação Full Stack desenvolvida como projeto de teste, demonstrando habilidades com **ASP.NET Core 9**, **Angular 18**, **Entity Framework Core**, e **Clean Architecture**. O sistema permite o gerenciamento completo de uma loja virtual, com recursos de autenticação, CRUDs, upload de imagem e visualização gráfica de dados.

---

## 🔥 Funcionalidades

- 📦 CRUD completo de **Produtos**, com upload de imagens  
- 👥 Gerenciamento de **Clientes** com imagem de perfil  
- 🛍️ Registro e acompanhamento de **Pedidos**  
- 🔐 Autenticação e Autorização com **ASP.NET Identity** e **JWT**  
- 🧑‍💼 Cadastro e atualização de **Usuários** com imagem de perfil  
- 📊 Dashboard com gráficos utilizando **Highcharts**  
- 🌐 Frontend moderno e responsivo com **Angular 18 + TailwindCSS + AdminLTE**

---

## 🧰 Tecnologias Utilizadas

### Backend

- ASP.NET Core 9
- Entity Framework Core
- ASP.NET Identity
- JWT Authentication
- AutoMapper
- FluentValidation
- Clean Architecture (DDD + CQRS)
- Swagger / Scalar UI

### Frontend

- Angular 18
- Tailwind CSS
- AdminLTE 3
- Highcharts / angular-highcharts
- Angular Interceptors, Guards e Services

### Outros

- Docker + Docker Compose (opcional)
- SQL Server (Code First)
- Upload de arquivos com IFormFile
- Deploy-ready com separação por camadas

---

## 📁 Estrutura do Projeto

```
ShopAPP/
├── ShopAPP.API/                → API ASP.NET Core (Controllers, Auth, Program.cs)
├── ShopAPP.Application/       → DTOs, Interfaces, Services, Mappings
├── ShopAPP.Domain/            → Entidades e Interfaces de domínio
├── ShopAPP.Infrastructure/    → Repositórios, Identity, DataContext
├── ShopAPP.Web/               → Frontend Angular 18
└── wwwroot/uploads/           → Armazenamento de imagens (produtos, usuários, etc.)
```

---

## ▶️ Como executar o projeto

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js e Angular CLI](https://angular.io/cli)
- [SQL Server LocalDB](https://learn.microsoft.com/pt-br/sql/database-engine/configure-windows/sql-server-express-localdb)

### Backend (API)

```bash
cd ShopAPP.API
dotnet ef database update
dotnet run
```

### Frontend (Angular)

```bash
cd ShopAPP.Web
npm install
ng serve
```

---

## 📸 Prints (Sugestão)

> (Adicione aqui imagens ou um link para vídeo demonstrativo do sistema)

---

## 👤 Autor

**Wellington Americano de Oliveira**  
📧 [americanosdigital@gmail.com](mailto:americanosdigital@gmail.com)  
🔗 [LinkedIn](https://www.linkedin.com/in/wellington-de-oliveira-8100b139)

---

## 📝 Licença

Este projeto é apenas para fins educacionais e de demonstração de portfólio.
