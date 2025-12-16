# Small Backend

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Core](https://img.shields.io/badge/.NET-10.0-purple)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120)](https://learn.microsoft.com/en-us/dotnet/csharp/)

**Small Backend** is a high-performance RESTful API built with **ASP.NET Core**, serving as the core engine for the Small blogging platform. It handles secure user authentication, article management, and data persistence using modern .NET technologies.

## 🚀 Live Demo & Documentation

The API is deployed and live. You can access the Base URL and explore the endpoints via Swagger UI:

* **Base URL:** [https://small.onurkarabeyoglu.online](https://small.onurkarabeyoglu.online)
* **Swagger / OpenAPI:** [https://small.onurkarabeyoglu.online/swagger](https://small.onurkarabeyoglu.online/swagger)

## 🔗 Related Project

This backend API is designed to power the **Small Frontend** application. You can view the source code or visit the live application below:

* **Live Frontend Application:** [https://sm.mmdjamali.ir](https://sm.mmdjamali.ir)
* **Frontend Repository:** [https://github.com/mmdjamali/small-frontend](https://github.com/mmdjamali/small-frontend)

## 🛠 Technologies & Frameworks

* **Framework:** ASP.NET Core Web API (.NET 10.0)
* **Language:** C# 12
* **Database:** PostgreSQL / MSSQL (via Entity Framework Core)
* **Authentication:** JWT (JSON Web Tokens)
* **Documentation:** Swagger / OpenAPI
* **Validation:** FluentValidation

## ⚙️ Installation & Setup

### Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
* PostgreSQL or SQL Server instance

### 1. Clone the Repository
```bash
git clone [https://github.com/karabeyogluonur/small-backend.git](https://github.com/karabeyogluonur/small-backend.git)
cd small-backend
```

### 2. Configuration
Update `appsettings.json` with your database connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SmallDb;User Id=postgres;Password=yourpassword;"
}
```

### 3. Run the Application
Restore packages and start the server:
```bash
dotnet restore
dotnet run
```
The API will run on `https://localhost:5001` (or `http://localhost:5000`) locally.

## 📡 Key API Endpoints

**Auth**
* `POST /api/auth/login` - User login
* `POST /api/auth/register` - User registration

**Articles**
* `GET /api/articles` - Get all articles
* `GET /api/articles/{slug}` - Get article by slug
* `POST /api/articles` - Create article (Authorized)

## 🤝 Contributing

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/NewFeature`)
3.  Commit your Changes (`git commit -m 'Add NewFeature'`)
4.  Push to the Branch (`git push origin feature/NewFeature`)
5.  Open a Pull Request

## 👥 Contributors

* **Onur Karabeyoğlu** - [GitHub Profile](https://github.com/karabeyogluonur)
* **Mohammad Jamali** - [GitHub Profile](https://github.com/mmdjamali)

## 📄 License

Distributed under the MIT License.
