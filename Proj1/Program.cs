using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;

class Program
{
    
    static bool Rodando = true;
    static List<Conta> contas = new List<Conta>();
    static int IdAtualConta;
    static string conexao = "Server=localhost;Database=banco;User ID=root;Password=Hypnotize-Overrule-Luckiness7;";

    static MySqlConnection conn = new MySqlConnection(conexao);

    static void Main()
    
    {
        AbrirContas();
        while(Rodando == true)
        {
            Console.Clear();
            Console.WriteLine("===BEM VINDO===");
            System.Console.WriteLine("1 - Criar Conta");
            System.Console.WriteLine("2 - Logar na Conta");
            System.Console.WriteLine("3 - Listar Usuarios");
            System.Console.WriteLine("4 - Sair");
            InserirContaTeste();

            string resp = Console.ReadLine();
            if(resp == "1")
            {
            CriarConta();
            }
            else if (resp == "2")
            {
                if (LogarConta() == true)
                {
                    while(MenuConta() == true)
                    {   
                    }
                }

            }else if(resp == "3")
            {
                Listar();
            }
            else if (resp == "4")
            {
                Rodando = false;
            }
            else if(resp != "1" && resp != "2" && resp != "3" )
            {
                System.Console.WriteLine("Resposta nao aceita, tente novamente - Enter para continuar...");
                Console.ReadLine();
            }
        }
        static void Load()
        {
            string ponto = ". ";

            int i = 0;
            int contador = 0;

            while(contador < 1)
            {
                contador++;
                i = 0;
                Console.Clear();
                while(i < 3)
                {
                    System.Console.Write(ponto);
                    i++;
                    Thread.Sleep(250);
                }
            }
            Thread.Sleep(200);
            Console.Clear();

        }
        static void CriarConta()
        {
            Console.Clear();
            System.Console.WriteLine("Nomeie sua Conta");
            System.Console.WriteLine("e - Para Sair");
            string nomeConta = Console.ReadLine();
            if (QuitIf(nomeConta))
            {
                return;
            }
            Conta conta = new Conta();

            conta.Nome = nomeConta;

            Load();

            System.Console.WriteLine(nomeConta);
            System.Console.WriteLine("Qual sera sua senha?  ");
            System.Console.WriteLine("e - Para Sair");
            string senha = Console.ReadLine();
            if (QuitIf(senha))
            {
                return;
            }
            conta.Senha = senha;

            string Nsenha;

            System.Console.WriteLine("Digite novamente:  ");
            Nsenha = Console.ReadLine();
            while(senha != Nsenha)
            {
                System.Console.WriteLine("Senha incorreta,tente novamente");
                Nsenha = Console.ReadLine();
            }

            contas.Add(conta);
            System.Console.WriteLine("Conta Criada com sucesso!");
            SalvarContas();
            Thread.Sleep(2000);
        }
        static bool LogarConta()
        {
            Thread.Sleep(200);
            Console.Clear();
            
            if(contas.Count == 0)
            {
                System.Console.WriteLine("Nao existem contas disponiveis - Enter para Continuar"); 
                Console.ReadLine();
                return false;
            }


           System.Console.WriteLine("Digite seu Usuario:  ");
           System.Console.WriteLine("e - Para Sair");
           string nomeConta = Console.ReadLine();
           QuitIf(nomeConta);
           int i = ProcurarConta(nomeConta);

            IdAtualConta = i;
            System.Console.WriteLine("Digite sua senha:");
            System.Console.WriteLine("e - Para Sair");
            string senha = Console.ReadLine();

            if (QuitIf(senha))
            {
                return false;
            }
            while(contas[i].Senha != senha)
            {
                System.Console.WriteLine("senha incorreta,tente Novamente");
                senha = Console.ReadLine();
            }
            System.Console.WriteLine("Usuario Logado com sucesso!");
            Thread.Sleep(2000);
            Console.Clear();
            return true;      
        }
        static void Listar()
        {
            foreach(Conta conta in contas)
            {
                System.Console.WriteLine($"Nome:{conta.Nome}   |  Senha: {conta.Senha}  | Saldo: {conta.Saldo}");
            }
            System.Console.WriteLine("Enter - Para Sair");
            Console.ReadLine();
        }
        static bool QuitIf(string variavel)
        {   
            if(variavel == "e")
            {
                Environment.Exit(0);
            }
            return false;
        }
        static int ProcurarConta(string nome)
        {
            int i = 0;
            while(contas[i].Nome != nome)
            {
                i++;
                if(i == contas.Count)
                {
                    System.Console.WriteLine("Conta nao encontrada tente Novamente:");
                    nome = Console.ReadLine();
                    QuitIf(nome);
                    i = 0;
                }
            }
            return i;
        }
        static bool MenuConta()
        {
            Thread.Sleep(200);
            Console.Clear();
            System.Console.WriteLine("==== MENU CONTA ====");
            System.Console.WriteLine("1 - Saldo");
            System.Console.WriteLine("2 - Depositar");
            System.Console.WriteLine("3 - Sacar");
            System.Console.WriteLine("4 - Transferir");
            System.Console.WriteLine("5 - Sair");
            string resp = Console.ReadLine();

            if(resp == "1")
            {
                Saldo();
                return true;
            }
            else if(resp == "2")
            {
                Deposito();
                return true;
            }else if(resp == "3")
            {
                Sacar();
                return true;
            }else if(resp == "4")
            {
                Transferir();
                return true;
            }else if(resp == "5")
            {
                return false;
            }
            return true;
        }
        static void Saldo()
        {
            Console.Clear();
            System.Console.WriteLine($"Saldo:{contas[IdAtualConta].Saldo}");
            System.Console.WriteLine("Enter Para Continuar");
            Console.ReadLine();
        }
        static void Deposito()
        {      
        System.Console.WriteLine("Digite o valor a Depositar: ");
        string resp = Console.ReadLine();
        decimal valor;
        while(Verificar(resp, out valor) == false)
            {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
            }
        contas[IdAtualConta].Saldo = contas[IdAtualConta].Saldo + valor;
        System.Console.WriteLine($"Foram Depositados R${valor} -  Enter para continuar");
        Console.ReadLine();
        }
        static void Sacar()
        {
            System.Console.WriteLine("Digite o valor a sacar: ");
            string resp = Console.ReadLine();
            decimal valor;

            while(Verificar(resp, out valor) == false)
            {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
            }

            while(valor > contas[IdAtualConta].Saldo)
            {
                System.Console.WriteLine($"Valor maior que o saldo da conta ({contas[IdAtualConta].Saldo}) Tente novamente");
                resp = Console.ReadLine();

                while(Verificar(resp, out valor) == false)
                {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
                }
            }
            contas[IdAtualConta].Saldo = contas[IdAtualConta].Saldo - valor;
            System.Console.WriteLine($"R${valor} Sacado com sucesso, saldo atual:R${contas[IdAtualConta].Saldo}");
            System.Console.WriteLine("Enter para Continuar");
            Console.ReadLine();
        }
        static void Transferir()
        {
            System.Console.WriteLine("Digite o nome do usuario de DESTINO:");
            System.Console.WriteLine("Enter ");
            string ContaDestino = Console.ReadLine();
            int i = ProcurarConta(ContaDestino);

            Console.WriteLine("Digite o valor a trasnferir: ");
            string resp = Console.ReadLine();
            decimal valor;
            while(Verificar(resp, out valor) == false)
            {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
            }

            while(valor > contas[IdAtualConta].Saldo)
            {
                System.Console.WriteLine($"Valor maior que o saldo da conta (Saldo:{contas[IdAtualConta].Saldo}) Tente novamente");
                resp = Console.ReadLine();

                while(Verificar(resp, out valor) == false)
                {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
                }
            }

            contas[IdAtualConta].Saldo = contas[IdAtualConta].Saldo - valor;

            contas[i].Saldo = contas[i].Saldo + valor;

            System.Console.WriteLine($"Foram Transferidos R${valor} -  Enter para continuar");
            Console.ReadLine();
        }
        static bool Verificar(string resp, out decimal valor)
        {
                if(decimal.TryParse(resp,out valor))
            {
                return true;
            }
            else
            {
                System.Console.WriteLine("Valor Invalido");
                return false;
            }
        }
        static void SalvarContas()
        {
            string texto;
            texto = JsonSerializer.Serialize(contas);
            File.WriteAllText("contas.json",texto);
        }
        static void AbrirContas()
        {
            bool existe = File.Exists("contas.json");
            string texto;
            if (existe == true)
            {
                texto = File.ReadAllText("contas.json");
                contas = JsonSerializer.Deserialize<List<Conta>>(texto);
            }
        }
        
        static void InserirContaTeste()
        {
            conn.Open();
            string sql = "INSERT INTO Contas (Nome,Senha,Saldo) VALUES ('Yan','123',0)";
            MySqlCommand comando = new MySqlCommand(sql, conn);
            comando.ExecuteNonQuery();
        }
    }
}