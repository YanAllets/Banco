using System;
using System.Linq.Expressions;
using System.Net;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Net.Mail;
class Program
{
    static bool Rodando = true;
    static string nomeConta;
    static string senha;
    static List<Conta> contas = new List<Conta>();
    static int IdAtualConta;
    static void Main()
    {
        while(Rodando == true)
        {
            Console.Clear();
            Console.WriteLine("===BEM VINDO===");
            System.Console.WriteLine("1 - Criar Conta");
            System.Console.WriteLine("2 - Logar na Conta");
            System.Console.WriteLine("3 - Listar Usuarios");
            System.Console.WriteLine("4 - Sair");

            string resp = Console.ReadLine();
            if(resp == "1")
            {
            CriarConta();
            }
            else if (resp == "2")
            {
                while(Rodando && LogarConta() == false)
                {
                }

                MenuConta();
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

            while(contador < 2)
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

        }
        static void CriarConta()
        {
            Console.Clear();
            System.Console.WriteLine("Nomeie sua Conta");
            System.Console.WriteLine("e - Para Sair");
            nomeConta = Console.ReadLine();
            if (Quit(nomeConta))
            {
                return;
            }
            Conta conta = new Conta();

            conta.Nome = nomeConta;

            Load();

            System.Console.WriteLine("");
            System.Console.WriteLine(nomeConta);
            System.Console.WriteLine("Qual sera sua senha?  ");
            System.Console.WriteLine("e - Para Sair");
            senha = Console.ReadLine();
            if (Quit(senha))
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
            Thread.Sleep(2000);
        }
        static bool LogarConta()
        {
            if(contas.Count == 0)
            {
                System.Console.WriteLine("Nao existem contas disponiveis,Enter - para continuar");
                Rodando = false; 
                return true;
            }


           System.Console.WriteLine("Digite seu Usuario:  ");
           System.Console.WriteLine("e - Para Sair");
           nomeConta = Console.ReadLine();
           Quit(nomeConta);
           int i = 0;
           while(contas[i].Nome != nomeConta)
            {
                i++;
                if(i == contas.Count)
                {
                    System.Console.WriteLine("Usuario nao Existe,tente Novamente");
                    return false;
                }
            }
            IdAtualConta = i;
            System.Console.WriteLine("Digite sua senha:");
            System.Console.WriteLine("e - Para Sair");
            senha = Console.ReadLine();

            if (Quit(senha))
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
        static bool Quit(string variavel)
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
                    System.Console.WriteLine("Usuario nao Existe,tente Novamente");
                    nome = Console.ReadLine();
                }
            }
            return i;
        }
        static void MenuConta()
        {
            System.Console.WriteLine("==== MENU CONTA ====");
            System.Console.WriteLine("1 - Saldo");
            System.Console.WriteLine("2 - Depositar");
            System.Console.WriteLine("3 - Sacar");
            System.Console.WriteLine("4 - Transferir");
            string resp = Console.ReadLine();

            if(resp == "1")
            {
                Saldo();
            }
            else if(resp == "2")
            {
                Deposito();
            }else if(resp == "3")
            {
                Sacar();
            }else if(resp == "4")
            {
                Transferir();
            }
        }
        static void Saldo()
        {
            System.Console.WriteLine($"Saldo:{contas[IdAtualConta].Saldo}");
            Console.ReadLine();
        }
        static void Deposito()
        {      
        System.Console.WriteLine("Digite o valor a Depositar: ");
        float valor = Convert.ToInt32(Console.ReadLine());
        contas[IdAtualConta].Saldo = contas[IdAtualConta].Saldo + valor;
        System.Console.WriteLine($"Foram Depositados R${valor} -  Enter para continuar");
        Console.ReadLine();
        }
        static void Sacar()
        {
            System.Console.WriteLine("Digite o valor a sacar: ");
            float valor = Convert.ToInt32(Console.ReadLine());
            while(valor > contas[IdAtualConta].Saldo)
            {
                System.Console.WriteLine($"Valor maior que o saldo da conta ({contas[IdAtualConta].Saldo}) Tente novamente");
            }
            contas[IdAtualConta].Saldo = contas[IdAtualConta].Saldo - valor;
            System.Console.WriteLine($"R${valor} Sacado com sucesso, saldo atual:R${contas[IdAtualConta].Saldo}");
        }
        static void Transferir()
        {
            System.Console.WriteLine("Digite o nome do usuario de DESTINO:");
            string ContaDestino = Console.ReadLine();
            int i = 0;
            while(contas[i].Nome != ContaDestino)
            {
                i++;
                if(i == contas.Count)
                {
                    System.Console.WriteLine("Usuario nao Existe,tente Novamente");
                }
            }

            Console.WriteLine("Digite o valor a trasnferir: ");
            float valor = Convert.ToInt32(Console.ReadLine());
            while(valor > contas[IdAtualConta].Saldo)
            {
                System.Console.WriteLine($"Valor maior que o saldo da conta (Saldo:{contas[IdAtualConta].Saldo}) Tente novamente");
                valor = Convert.ToInt32(Console.ReadLine());
            }

            contas[IdAtualConta].Saldo = contas[IdAtualConta].Saldo - valor;

            contas[i].Saldo = contas[i].Saldo + valor;

            System.Console.WriteLine($"Foram Transferidos R${valor} -  Enter para continuar");
            Console.ReadLine();
        }
    }
}