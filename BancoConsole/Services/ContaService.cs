using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class ContaService
{
    public static void CriarConta()
    {
        Dictionary<string,object> parametros = new Dictionary<string, object>();
        Console.Clear();
        System.Console.WriteLine("Nomeie sua Conta");
        System.Console.WriteLine("e - Para Sair");
        string nomeConta = Console.ReadLine();
        if (Utils.QuitIf(nomeConta))
        {
            return;
        }

        Utils.Load();
        System.Console.WriteLine(nomeConta);
        System.Console.WriteLine("Qual sera sua senha?  ");
        System.Console.WriteLine("e - Para Sair");
        string senha = Console.ReadLine();
        if (Utils.QuitIf(senha))
        {
            return;
        }

        string Nsenha;

        System.Console.WriteLine("Digite novamente:  ");
        Nsenha = Console.ReadLine();
        while(senha != Nsenha)
        {
            System.Console.WriteLine("Senha incorreta,tente novamente");
            Nsenha = Console.ReadLine();
        }

        System.Console.WriteLine("Conta Criada com sucesso!");
        string senhaHash = HashService.GerarHash(Nsenha);
        parametros.Add("@nome",nomeConta);
        parametros.Add("@senha",senhaHash);
        DataBase.SqlNonQuery("INSERT INTO Contas (Nome,Senha,Saldo) VALUES (@nome,@senha,0)",parametros);
        Thread.Sleep(2000);
    }
    public static bool LogarConta()
    {
        Dictionary<string,object> parametros = new Dictionary<string, object>();
        Thread.Sleep(200);
        Console.Clear();
        
        if(DataBase.SqlScalar<int>("Select Count(*) from contas",parametros) == 0)
        {
            System.Console.WriteLine("Nao existem contas disponiveis - Enter para Continuar"); 
            Console.ReadLine();
            return false;
        }

        System.Console.WriteLine("Digite seu Usuario:  ");
        System.Console.WriteLine("e - Para Sair");
        string nomeConta = Console.ReadLine();
        Utils.QuitIf(nomeConta);
        int i = DataBase.ProcurarConta(nomeConta);

        AppState.IdAtualConta = i;
        parametros.Add("@Id",AppState.IdAtualConta);

        System.Console.WriteLine("Digite sua senha:");
        System.Console.WriteLine("e - Para Sair");
        string senha = Console.ReadLine();
        string senhaHash = HashService.GerarHash(senha);

        if (Utils.QuitIf(senha))
        {
            return false;
        }
        while(DataBase.SqlScalar<string>("select senha from contas where id = @Id;",parametros) != senhaHash)
        {
            System.Console.WriteLine("Senha incorreta,tente novamente");
            senha = Console.ReadLine();
            senhaHash = HashService.GerarHash(senha);
        }
        System.Console.WriteLine("Usuario Logado com sucesso!");
        Thread.Sleep(2000);
        Console.Clear();
        return true;      
    }
    public static void Saldo()
    {
        Dictionary<string,object> parametros = new Dictionary<string, object>();
        parametros.Add("@Id",AppState.IdAtualConta);
        decimal Saldo = DataBase.SqlScalar<Decimal>("select Saldo From contas where id = @Id;",parametros);
        System.Console.WriteLine($"Saldo:{Saldo}");
        System.Console.WriteLine("Enter Para Continuar");
        Console.ReadLine();
    }
    public static void Deposito()
    {
    Dictionary<string,object> parametros = new Dictionary<string, object>();      
    System.Console.WriteLine("Digite o valor a Depositar: ");
    string resp = Console.ReadLine();
    decimal valor;
    while(Utils.Verificar(resp, out valor) == false)
        {
            System.Console.WriteLine("tente novamente:");
            resp = Console.ReadLine();
        }
    parametros.Add("@valor",valor);
    parametros.Add("@Id",AppState.IdAtualConta);
    DataBase.SqlNonQuery("update contas set saldo = saldo + @valor where id = @Id;",parametros);
    System.Console.WriteLine($"Foram Depositados R${valor} -  Enter para continuar");
    Console.ReadLine();
    }
    public static void Sacar()
    {
        Dictionary<string,object> parametros = new Dictionary<string, object>();
        parametros.Add("@Id",AppState.IdAtualConta);

        System.Console.WriteLine("Digite o valor a sacar: ");
        string resp = Console.ReadLine();
        decimal saldo = DataBase.SqlScalar<decimal>("select saldo from contas where id = @Id;",parametros);
        decimal valor;

        while(Utils.Verificar(resp, out valor) == false)
        {
            System.Console.WriteLine("tente novamente:");
            resp = Console.ReadLine();
        }

        while(valor > saldo)
        {
            System.Console.WriteLine($"Valor maior que o saldo da conta ({saldo}) Tente novamente");
            resp = Console.ReadLine();

            while(Utils.Verificar(resp, out valor) == false)
            {
            System.Console.WriteLine("tente novamente:");
            resp = Console.ReadLine();
            }
        }
        parametros.Add("@valor",valor);
        DataBase.SqlNonQuery("update contas set saldo = saldo - @valor where id = @Id;",parametros);
        saldo = DataBase.SqlScalar<Decimal>("select Saldo from contas where id = @Id",parametros);
        System.Console.WriteLine($"R${valor} Sacado com sucesso, saldo atual:R${saldo}");
        System.Console.WriteLine("Enter para Continuar");
        Console.ReadLine();
    }
    public static void Transferir()
    {
        Dictionary<string,object> parametros = new Dictionary<string, object>();

        System.Console.WriteLine("Digite o nome do usuario de DESTINO:");
        string ContaDestino = Console.ReadLine();
        int i = DataBase.ProcurarConta(ContaDestino);
        parametros.Add("@Id",AppState.IdAtualConta);

        Console.WriteLine("Digite o valor a trasnferir: ");
        string resp = Console.ReadLine();
        decimal valor;
        while(Utils.Verificar(resp, out valor) == false)
        {
            System.Console.WriteLine("tente novamente:");
            resp = Console.ReadLine();
        }
        using MySqlTransaction transacao = DataBaseConfig.conn.BeginTransaction();
        decimal saldo = DataBase.SqlScalar<decimal>("select saldo from contas where id = @Id;",parametros,transacao);

        while(valor > saldo)
        {
            System.Console.WriteLine($"Valor maior que o saldo da conta (Saldo:{saldo}) Tente novamente");
            resp = Console.ReadLine();

            while(Utils.Verificar(resp, out valor) == false)
            {
            System.Console.WriteLine("tente novamente:");
            resp = Console.ReadLine();
            }
        }

        parametros.Add("@valor",valor);
        parametros.Add("@Destino",i);
        try
        {
            DataBase.SqlNonQuery("update contas set saldo = saldo - @valor where id = @Id;",parametros,transacao);
            DataBase.SqlNonQuery($"update contas set saldo = saldo + @valor where id = @Destino;",parametros,transacao);
            transacao.Commit();

        }
        catch(Exception ex)
        {
            Console.Clear();
            transacao.Rollback();
            System.Console.WriteLine(ex.Message);
            return;
        }
        System.Console.WriteLine($"Foram Transferidos R${valor} Para {ContaDestino} -  Enter para continuar");
        Console.ReadLine();
    }
}