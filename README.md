# Stellar DotNet Identity Framework

## Stellar Account Integration for Microsoft Identity Platform

![Stellar DotNet Identity Framework](assets/5e05ca83-3453-4b32-a7e6-4f28257df452.png)

---

## Overview

This project integrates Stellar account operations and management into the Microsoft Identity Platform Framework. It refactors .NET MVC classes, Razor pages, and database schemas to incorporate Stellar blockchain operations seamlessly within the framework.

The Microsoft Identity Platform, a widely used authentication system, serves millions daily across services like Windows, Xbox, Office, Outlook, LinkedIn, and thousands of enterprise applications using Microsoft Entra (formerly Active Directory). It implements OAuth 2.0 and OpenID Connect protocols, providing comprehensive features for registration, sign-in, password management, multi-factor authentication, and more.

---

## Features

- **Account Abstraction**: Enhances username/password or passwordless authentication with Stellar transaction signing and account recovery mechanisms.
- **QR-Code Authorization and Signing Flow**: Introduces a QR code-based multi-factor authentication system that integrates with Stellar for secure transactions.
- **Role and Group Management**: Adds capabilities to create and manage roles and groups within Microsoft Entra using Stellar Muxed accounts.

---

## Documentation

Comprehensive documentation is available to help you get started with the Stellar .NET Identity Platform Template:

1. [Introduction](docs/Introduction.md)
2. [Prerequisites](docs/Prerequisites.md)
3. [Getting Started](docs/GettingStarted.md)
4. [Configuration](docs/Configuration.md)
5. [Authentication Methods](docs/AuthenticationMethods.md)
    - [Passwordless Authentication with Freighter](docs/AuthenticationMethods.md#passwordless-authentication-with-freighter)
    - [Multi-Factor Authentication (MFA)](docs/AuthenticationMethods.md#multi-factor-authentication-mfa)
6. [Keypair Management](docs/KeypairManagement.md)
    - [Generating Keypairs](docs/KeypairManagement.md#generating-keypairs)
    - [Managing Keypairs](docs/KeypairManagement.md#managing-keypairs)
7. [Running the Application](docs/RunningTheApplication.md)
8. [Deployment](docs/Deployment.md)
9. [Troubleshooting](docs/Troubleshooting.md)
10. [Contributing](docs/Contributing.md)
11. [License](docs/License.md)
12. [Contact Information](docs/ContactInformation.md)

---

## Usage

- **Clone or Fork the Repository**: Clone this repo and open it in Visual Studio.

  ```bash
  git clone https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework.git
  ```

- **Rename the Solution and Project**: Rename the solution and project to your preferred application name.
- **Configure the Database Connection**: Update the database connection string in `appsettings.json` or user secrets.
- **Update JWT Settings**: Configure JWT settings with your preferred Key, Issuer, and Audience values.
- **Select Your Database Provider**: If not using PostgreSQL, update `Program.cs` to use your preferred database provider or directory service (e.g., MS Entra).
- **Apply Database Migrations**: Generate and apply the database schema migration script using the standard Microsoft Entity Framework tooling.

  ```bash
  dotnet ef database update
  ```

- **Customize Identity Management Views**: Customize user registration and related Identity Management Razor views in `/Areas/Identity/Pages`.
- **Build Your Stellar Blockchain App**: Utilize the framework to build your Stellar blockchain .NET Razor app.
- **Use Pakana Stellar Components**: Consider utilizing the [Pakana Stellar Components](https://www.pakanacomponents.com) to quickly get started with transaction signing, payment processing, and more.

This approach saves weeks of development time, reduces costs, and mitigates risks while enabling robust account security features integrated with Stellar account management features.

---

## Compatibility and Deployment

### Database Compatibility

The integration is designed to work with various databases through the Entity Framework, ensuring developers can plug the Stellar operations into their existing or new database schema with minimal configuration.

### Deployment Environments

The framework is designed to be compatible with multiple deployment environments, including:

- **Windows**: Fully supported on all recent versions of Windows, making it ideal for desktop and server applications.
- **Linux**: Compatible with major Linux distributions, ensuring that applications can be hosted on a cost-effective and scalable platform.
- **Azure**: Ready for deployment on Azure, supporting both Windows and Linux environments, and leveraging Azure's cloud capabilities for enhanced performance and reliability.

---

## Blockchain Transparency

For end-users, blockchain operations and signing are designed to be transparent. In scenarios requiring direct user interaction, such as transaction approvals or enhanced security measures, users will engage through intuitive interfaces like QR code scans or Freighter signatures.

This integration not only extends the capabilities of the Microsoft Identity Platform with cutting-edge blockchain technology but also maintains the platform's ease of use and robust security features, making it an essential upgrade for any developer working within the Microsoft ecosystem.

![Stellar Integration](assets/a1324197-a474-482f-8f3d-8b5474c52028.png)

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Contributing

Contributions are welcome! Please follow these guidelines:

- **Fork the Repository**: Create your own fork to work on.
- **Create a Branch**: Use a descriptive branch name (e.g., `feature/add-freighter-support`).
- **Submit a Pull Request**: Describe your changes and link any relevant issues.

---

## Contact Information

For questions, issues, or support, please open an issue on the [GitHub repository](https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework/issues).

---

## Additional Resources

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

## Need Help?

If you have any questions or need assistance, feel free to contact us:

- **Email**: [support@lockb0x-llc.com](mailto:support@lockb0x-llc.com)
- **GitHub Issues**: [Create an Issue](https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework/issues/new)

