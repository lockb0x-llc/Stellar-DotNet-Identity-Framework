## Authentication Methods

### Passwordless Authentication with Freighter

This template supports passwordless authentication using the **Freighter** wallet extension. Users can register and log in using their Stellar accounts managed by Freighter.

#### Setting Up Freighter Authentication

1. **Install Freighter Wallet Extension**

   - [Download Freighter](https://www.freighter.app/)

2. **Configure Freighter in the Application**

   - No additional server-side configuration is needed.
   - Ensure the client-side scripts include calls to the Freighter API.

#### Registration Flow

- Users can register by connecting their Freighter wallet.
- The application stores the user's Stellar public key for authentication.

#### Login Flow

- Users authenticate by signing a challenge message using Freighter.
- The server verifies the signature to authenticate the user.

### Multi-Factor Authentication (MFA)

Enhance security by enabling MFA using authenticator apps.

#### Enabling MFA

1. **Configure Identity Options**

   In `Startup.cs` or `Program.cs`:

   ```csharp
   services.AddIdentity<ApplicationUser, IdentityRole>(options =>
   {
       options.SignIn.RequireConfirmedAccount = true;
       options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
   })
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();
   ```

2. **Add MFA Views and Logic**

   - **Enable MFA Page**: Guides users to set up MFA.
   - **Login with MFA**: Prompts users for the verification code during login.

3. **Update User Interface**

   - Include options for users to manage their MFA settings.

---

