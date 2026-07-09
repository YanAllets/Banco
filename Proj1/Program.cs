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
                while(LogarConta() == false)
                {
                }

                System.Console.WriteLine("==== MENU CONTA ====");
                System.Console.WriteLine("1 - Saldo");
                resp = Console.ReadLine();

                if(resp == "1")
                {
                    System.Console.WriteLine($"Saldo:{contas[IdAtualConta].Saldo}");
                    System.Console.WriteLine($"Conta Atual:{IdAtualConta}");
                    Console.ReadLine();
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
                return false;
            }


           System.Console.WriteLine("Digite seu Usuario:  ");
           System.Console.WriteLine("e - Para Sair");
           nomeConta = Console.ReadLine();
           if (Quit(nomeConta))
            {
                return false;
            }
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
                System.Console.WriteLine($"Nome:{conta.Nome}   |Senha: {conta.Senha}");
            }
            System.Console.WriteLine("Enter - Para Sair");
            Console.ReadLine();
        }
        static bool Quit(string variavel)
        {   
            if(variavel == "e")
            {
                Rodando = false;
                return true;
            }
            return false;
        }
    }
}
class Conta
{
    public string Nome;
    public string Senha;
    public float Saldo;
}