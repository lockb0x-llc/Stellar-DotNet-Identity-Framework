#Stellar DotNet Identity Framework
## Stellar Account Integration for Microsoft Identity Platform

## Overview

This project integrates Stellar account operations and management into the Microsoft Identity Platform Framework. It refactors .NET MVC classes, Razor pages, and database schemas to incorporate Stellar blockchain operations seamlessly within the framework.

The Microsoft Identity Platform, a widely-used authentication system, serves millions daily across services like Windows, Xbox, Office, Outlook, LinkedIn, and thousands of enterprise applications using Microsoft Entra (formerly Active Directory). It implements OAuth 2.0 and OpenID 1.0 protocols, providing comprehensive features for registration, sign-in, password management, multi-factor authentication, and more.

![image](https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework/assets/8976999/5e05ca83-3453-4b32-a7e6-4f28257df452)

## Features

- **Account Abstraction**: Enhances username/password or password-less authentication with Stellar transaction signing and account recovery mechanisms.
- **QR-Code Authorization and Signing Flow**: Introduces a QR code-based multi-factor authentication system that integrates with Stellar for secure transactions.
- **Role and Group Management**: Adds capabilities to create and manage roles and groups within Microsoft Entra using Stellar Muxed accounts.

## Compatibility and Deployment

### Database Compatibility

The integration is designed to work with various databases through the Entity Framework, ensuring developers can plug the Stellar operations into their existing or new database schema with minimal configuration.

### Deployment Environments

The framework is designed to be compatible with multiple deployment environments, including:
- **Windows**: Fully supported on all recent versions of Windows, making it ideal for desktop and server applications.
- **Linux**: Compatible with major Linux distributions, ensuring that applications can be hosted on a cost-effective and scalable platform.
- **Azure**: Ready for deployment on Azure, supporting both Windows and Linux environments, and leveraging Azure's cloud capabilities for enhanced performance and reliability.

## Usage

- Clone or fork this repo and open it in Visual Studio.
- Rename the solution and project to your preferred application name.
- Configure the database connection.
- Apply the database schema migration script.
- Use the framework as with the original Microsoft Identity Framework.
- Customize User Registration and related Identity Management Razor Views.
- Build your Stellar Blockchain DotNet Razor App.
- Consider utilizing the <a href="https://www.pakanacomponents.com" target="_blank">Pakana Stellar Components</a> to quickly get started with transaction signing, payment processing, and more.

This approach saves weeks of development time, reduces costs, and mitigates risks while enabling robust account security features integrated with Stellar Account Management Features.

## Blockchain Transparency

For end-users, blockchain operations and signing are designed to be transparent. In scenarios requiring direct user interaction, such as transaction approvals or enhanced security measures, users will engage through intuitive interfaces like QR code scans or Freighter signatures.

---

This integration not only extends the capabilities of the Microsoft Identity Platform with cutting-edge blockchain technology but also maintains the platform's ease of use and robust security features, making it an essential upgrade for any developer working within the Microsoft ecosystem.

![image](https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework/assets/8976999/a1324197-a474-482f-8f3d-8b5474c52028)

