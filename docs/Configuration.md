## Configuration

Before running the application, you'll need to configure various settings.

### App Settings

Open the `appsettings.json` file and configure the following sections:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your database connection string here"
  },
  "Stellar": {
    "Network": "TestNet", // or "PublicNet"
    "HorizonUrl": "https://horizon-testnet.stellar.org" // or the public network URL
  },
  "Logging": {
    // Logging configuration
  },
  "AllowedHosts": "*"
}
```

### Database Configuration

The project uses Entity Framework Core with a SQL Server database by default. You can change the database provider or connection string as needed.

**Update the Connection String:**

- Replace `"Your database connection string here"` with your actual database connection string.

**Apply Migrations:**

```bash
dotnet ef database update
```

---

