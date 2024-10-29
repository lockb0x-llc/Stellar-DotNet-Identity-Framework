# Stellar DotNet Identity Framework

## Stellar Account Integration for Microsoft Identity Platform

![Stellar DotNet Identity Framework](https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework/assets/8976999/5e05ca83-3453-4b32-a7e6-4f28257df452)

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

1. [Introduction](./docs/Introduction.md)
2. [Prerequisites](./docs/Prerequisites.md)
3. [Getting Started](./docs/GettingStarted.md)
4. [Configuration](./docs/Configuration.md)
5. [Authentication Methods](./docs/AuthenticationMethods.md)
    - [Passwordless Authentication with Freighter](./docs/AuthenticationMethods.md#passwordless-authentication-with-freighter)
    - [Multi-Factor Authentication (MFA)](./docs/AuthenticationMethods.md#multi-factor-authentication-mfa)
6. [Keypair Management](./docs/KeypairManagement.md)
    - [Generating Keypairs](./docs/KeypairManagement.md#generating-keypairs)
    - [Managing Keypairs](./docs/KeypairManagement.md#managing-keypairs)
7. [Running the Application](./docs/RunningTheApplication.md)
8. [Deployment](./docs/Deployment.md)
9. [Troubleshooting](./docs/Troubleshooting.md)
10. [Contributing](./docs/Contributing.md)
11. [License](./docs/License.md)
12. [Contact Information](./docs/ContactInformation.md)

---

## Usage

- **Clone or Fork the Repository**: Clone this repo and open it in Visual Studio.

  ```bash
  git clone https://github.com/lockb0x-llc/Stellar-DotNet-Identity-Framework.git
