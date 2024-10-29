## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework/blob/main/LICENSE) file for details.

---


# Detailed Sections

Below are detailed explanations and instructions for each section of the documentation.

---

## 1. Introduction

The **Stellar .NET Identity Platform Template** is designed to help developers quickly set up an ASP.NET Core application with integrated Stellar account management and advanced authentication features.

Key features include:

- **Passwordless Authentication**: Users can register and log in using their Stellar accounts via the Freighter wallet extension.
- **Multi-Factor Authentication**: Enhance security with MFA using authenticator apps.
- **Secure Keypair Management**: Generate, encrypt, and manage Stellar keypairs within the application.

---

## 2. Prerequisites

Ensure that your development environment meets the necessary requirements.

### Software Requirements

- **.NET SDK**

  - Download from [Microsoft .NET Downloads](https://dotnet.microsoft.com/download).

- **Visual Studio 2022**

  - Download from [Visual Studio Downloads](https://visualstudio.microsoft.com/downloads/).

  - Alternatively, use **Visual Studio Code** with the C# extension.

- **Node.js**

  - Download from [Node.js Downloads](https://nodejs.org/en/download/).

- **Git**

  - Download from [Git Downloads](https://git-scm.com/downloads).

### Tools and Extensions

- **Freighter Wallet Extension**

  - Install from [Freighter Website](https://www.freighter.app/).

- **Authenticator App**

  - Install **Microsoft Authenticator** or **Google Authenticator** on your mobile device.

---

## 3. Getting Started

### Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework.git
```

### Setting Up the Project

1. **Navigate to the Project Directory**

   ```bash
   cd Stellar-DotNet-Identity-Framework
   ```

2. **Restore NuGet Packages**

   ```bash
   dotnet restore
   ```

3. **Install Front-End Dependencies**

   If applicable:

   ```bash
   npm install
   ```

4. **Build the Project**

   ```bash
   dotnet build
   ```

5. **Apply Database Migrations**

   ```bash
   dotnet ef database update
   ```

---

## 4. Configuration

### App Settings

Edit `appsettings.json` and configure the necessary settings.

#### Connection Strings

Provide your database connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

#### Stellar Network Configuration

Set the Stellar network:

```json
"Stellar": {
  "Network": "TestNet",
  "HorizonUrl": "https://horizon-testnet.stellar.org"
}
```

- Use `"PublicNet"` and `https://horizon.stellar.org` for the production network.

### Database Configuration

If you prefer a different database provider (e.g., PostgreSQL, MySQL), update the `ApplicationDbContext` configuration in `Startup.cs` or `Program.cs`.

---

## 5. Authentication Methods

### Passwordless Authentication with Freighter

#### Client-Side Integration

Include the Freighter API in your HTML pages:

```html
<script src="https://www.freighter.app/freighter-api.js"></script>
```

#### Registration and Login

- **Registration**: Users connect their Freighter wallet to register.
- **Login**: Users authenticate by signing a challenge message.

**Note:** Ensure your client-side scripts handle Freighter API interactions and error handling.

### Multi-Factor Authentication (MFA)

#### Enabling MFA

Configure Identity options to enable MFA:

```csharp
options.SignIn.RequireConfirmedAccount = true;
options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
```

#### User Interface

- **Enable MFA Page**: Users can enable MFA in their account settings.
- **Verification During Sensitive Actions**: Require MFA verification when revealing secret keys or changing account settings.

---

## 6. Keypair Management

### Generating Keypairs

Users can generate new Stellar keypairs within the application.

- **Encryption**: Private keys are encrypted using a passphrase provided by the user.
- **Storage**: Encrypted private keys are stored securely in the database.

### Managing Keypairs

- **Viewing Keypairs**: Users can see a list of their keypairs with labels and public keys.
- **Revealing Secret Keys**: MFA verification is required to reveal the secret key.
- **Adding Keypairs**: Users can add additional keypairs as needed.

---

## 7. Running the Application

To run the application locally:

1. **Start the Application**

   ```bash
   dotnet run
   ```

2. **Access the Application**

   - Open your browser and navigate to `https://localhost:5001`.

**Note:** If you encounter issues with HTTPS, ensure that your development certificates are set up correctly.

---

## 8. Deployment

### Preparing for Deployment

- **Update Configuration**

  - Set the `ASPNETCORE_ENVIRONMENT` variable to `Production`.
  - Update `appsettings.Production.json` with production settings.

- **Database Migration**

  - Apply migrations to the production database.

### Deployment Options

- **Azure App Service**
- **AWS Elastic Beanstalk**
- **Docker Containers**

Refer to the respective cloud provider's documentation for detailed deployment instructions.

---

## 9. Troubleshooting

### Freighter Not Detected

- **Ensure Freighter is Installed**

  - Verify that the Freighter extension is installed and enabled.

- **HTTPS Requirement**

  - Freighter requires the site to be served over HTTPS.

### Authentication Issues

- **Invalid Signatures**

  - Check that the correct network (TestNet or PublicNet) is configured.
  - Ensure the challenge messages are consistent between client and server.

### Database Errors

- **Connection Issues**

  - Confirm that the database server is accessible.
  - Verify the connection string is correct.

---

## 10. Contributing

We welcome contributions from the community!

- **Reporting Issues**

  - Use the GitHub Issues tab to report bugs or request features.

- **Submitting Pull Requests**

  - Fork the repository.
  - Create a new branch for your feature or fix.
  - Submit a pull request with a detailed description.

---

## 11. License

This project is licensed under the MIT License.

**License Summary:**

- **Commercial Use**: Permitted
- **Modification**: Permitted
- **Distribution**: Permitted
- **Private Use**: Permitted
- **Liability**: The software is provided "as is", without warranty of any kind.

---

## 12. Contact Information

For assistance, please contact:

- **Email**: [support@lockb0x-llc.com](mailto:support@lockb0x-llc.com)
- **GitHub Issues**: [Create an Issue](https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework/issues/new)

---

# Appendix

## A. Frequently Asked Questions (FAQ)

### Q1: Can I use this template with other authentication providers?

Yes, you can extend the template to include other authentication providers like Google, Facebook, or Microsoft by adding the necessary packages and configuration.

### Q2: How are private keys stored securely?

Private keys are encrypted using a passphrase provided by the user and stored in the database. They can only be decrypted by providing the correct passphrase.

### Q3: Is it safe to use this template in production?

While the template includes security features, you should perform a thorough security review and make necessary adjustments before deploying to production.

---

## B. Resources

- **Stellar Development Foundation**

  - [Stellar Developers](https://developers.stellar.org/)
  - [Stellar Documentation](https://developers.stellar.org/docs/)

- **Freighter Wallet**

  - [Freighter Website](https://www.freighter.app/)
  - [Freighter API Documentation](https://github.com/stellar/freighter-api)

- **Microsoft Identity Platform**

  - [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
  - [Secure Passwordless Authentication](https://docs.microsoft.com/en-us/azure/active-directory/authentication/concept-authentication-passwordless)

---

This documentation should provide developers with the information they need to get started with your Stellar .NET Identity Platform template. Feel free to adjust and expand upon this template to suit your project's specific features and requirements.

If you have any further questions or need additional assistance with any section, please let me know!