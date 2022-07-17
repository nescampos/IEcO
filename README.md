# IEcO
![cover](https://user-images.githubusercontent.com/7274106/179380246-00d89efc-48f0-4212-a5e5-d5c6cac38c01.png)


IEcO is the Initial Coin Offering for ReFi webapp, built with C# for Hedera Hashgraph Network.

You can try the demo: https://iecoapp.azurewebsites.net/ 

You can create sustainable projects, filling in specific information and collect HBAR, in exchange for tokens given to the contributors of your project (the tokens are created at the time of creating the project).
You can also create a test Hedera account to use for your app.

This project use [Hashgraph](https://bugbytesinc.github.io/Hashgraph/), a .NET library to access the Hedera network.

You can view how IEcO works in this [Youtube video](https://youtu.be/SOFH_4grcOg).
![image](https://user-images.githubusercontent.com/7274106/179380270-e6e17882-b89b-484e-9017-8b9eb35a49b0.png)


## License
----
Apache-2.0 License
 
## Contributors
----

- [Néstor Nicolás Campos Rojas](https://www.linkedin.com/in/nescampos/)


## Available options

What you can currently do in the web app is:
- Create Hedera test accounts to test the purchase and sending of tokens.
- Create projects (which store their information in the database) and create the token in the Hedera network, associated to the Hedera account that governs your application specified in the configuration.
- See available projects, buy tokens and see the latest transactions.
- Being able to send the HBARs obtained to the account associated with the project (this URL does not appear directly in the graphical interface, you must access the "Admin" module).



## Use this project
**The internal name for this project is EmiCert**

First, you need to update the database applying the available migration in the project:
```sh
    dotnet ef database update
```

To consume this project, just run with Visual Studio or DotNet CLI in your project.

```sh
    dotnet run
```

*This project is built with .NET 6.0*

## Configuration

You need to edit the **appsettings.json** file for adding:
- DefaultConnection: A SQL Server connection (use for saving info about the projects).
- HederaGatewayUrl: The URL for Hedera gateway (testing or production).
- HederaNodeNumber: The node number for Hedera gateway (0,1,2,3,4,5, etc.).
- Environment: Specify if you are in test environment or not ('test' or any other value). The 'test' value, you can enable the action for creating Hedera test account.
- HederaAccountId: The Hedera Account Id for govern this app (send transactions, create tokens, create Hedera test accounts, etc.).
- HederaPublicKey: The Hedera Public Key for govern this app (send transactions, create tokens, create Hedera test accounts, etc.).
- HederaPrivateKey: The Hedera Private Key for govern this app (send transactions, create tokens, create Hedera test accounts, etc.).

## Contributions

If you want to colaborate, just fork this repository and build new things. Thanks!!
