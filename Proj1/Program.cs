using System;
using System.Linq.Expressions;
using System.Net;
using Microsoft.VisualBasic;
class Program
{
    static bool Rodando = true;
    static string nomeConta;
    static string senha;
    static string[] nomes = new string[5];
    static int Ncontagem = 0;
    static string[] senhas = new string[5];
    static int Scontagem = 0;
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

            int resp = Convert.ToInt32(Console.ReadLine());
            if(resp == 1)
            {
            CriarConta();
            }
            else if (resp == 2)
            {
                while(LogarConta() == false)
                {
                }
                System.Console.WriteLine("Deu GREEN");
            }else if(resp == 3)
            {
                Listar();
            }
            else if (resp == 4)
            {
                Rodando = false;
            }
            else if(resp != 1 && resp != 2 && resp != 3)
            {
                System.Console.WriteLine("Resposta nao aceita, tente novamente");
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
            System.Console.WriteLine("Nomeie sua Conta");
            nomeConta = Console.ReadLine();
            nomes[Ncontagem] = nomeConta;
            Ncontagem++;

            Load();

            System.Console.WriteLine("");
            System.Console.WriteLine(nomeConta);
            System.Console.WriteLine("Qual sera sua senha?  ");
            senha = Console.ReadLine();

            senhas[Scontagem] = senha;
            Scontagem++;

            string Nsenha;

            System.Console.WriteLine("Digite novamente:  ");
            Nsenha = Console.ReadLine();
            while(senha != Nsenha)
            {
                System.Console.WriteLine("Senha incorreta,tente novamente");
                Nsenha = Console.ReadLine();
            }

            System.Console.WriteLine("Conta Criada com sucesso!");
            Thread.Sleep(2000);
        }
        static bool LogarConta()
        {
           System.Console.WriteLine("Digite seu Usuario:  ");
           nomeConta = Console.ReadLine();
           int i = 0;
           while(nomes[i] != nomeConta)
            {
                i++;
                if(i == 5)
                {
                    System.Console.WriteLine("Usuario nao Existe,tente Novamente");
                    return false;
                }
            }
            System.Console.WriteLine("Digite sua senha:");
            senha = Console.ReadLine();
            while(senhas[i] != senha)
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
            int i = 0;
            System.Console.WriteLine("id | Nome | senha");
            while (i != nomes.Length)
            {
                if(nomes[i] == null)
                {
                    System.Console.WriteLine($"{i}: Vazio");
                }else
                System.Console.WriteLine($"{i}: {nomes[i]} | {senhas[i]}");
                i++;
            }
            Console.ReadLine();
        }
    }
}