# BancoConsole

BancoConsole is a console-based banking application built with C# and MySQL,
The main goal of this project was to practice backend development fundamentals, database interaction, code organization, and object-oriented programming.

## Features

- Create bank acounts
- User authentication with password login
- Password hashing using SHA-256
- Check account balance
- Deposit funds
- Withdraw funds
- Transfer money between accounts
- List registered users
- SQL transactions to ensure data consistency during transfers

## Database

Table used by the application:

```sql
CREATE TABLE Contas
(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(64) NOT NULL,
    Senha VARCHAR(64) NOT NULL,
    Saldo DECIMAL(10,2) NOT NULL DEFAULT 0
);
```
## Purpose

The purpose of this project was to consolidate the fundamentals of backend development with C#, including database operations, authentication, transactions, and project organization, before moving on to ASP.NET Core projects.



Developed by Yan Stella as a personal learning project.<3

