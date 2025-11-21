# Minha API .NET 8 

Este projeto é uma API desenvolvida em **ASP.NET Core 8** e persistência usando **Entity Framework Core**.

## ✅ Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)
- [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet)

## 🚀 Como rodar o projeto

1. Clone o repositório:
```bash
git clone [(https://github.com/lilioliveira1801/PSI14930_14931_C072073.git)]


2. Configure a connection string no arquivo `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\MSSQLLocalDB;Database=MinhaApiDb;Trusted_Connection=True;"
}
```

3. Aplique as migrations para criar o banco:
```bash
dotnet ef database update
```

4. Execute a API:
```bash
dotnet run
```

A API estará disponível em `(https://localhost:7228/swagger/index.html)`.


## 🛠 Tecnologias usadas
- ASP.NET Core 8
- Entity Framework Core


## 📄 Como contribuir
1. Faça um fork do projeto
2. Crie uma branch: `git checkout -b minha-feature`
3. Commit suas alterações: `git commit -m 'Minha feature'`
4. Push: `git push origin minha-feature`
5. Abra um Pull Request

