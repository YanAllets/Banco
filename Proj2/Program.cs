using System;
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
            Console.WriteLine("===BEM VINDO===");
            System.Console.WriteLine("1 - Criar Conta");
            System.Console.WriteLine("2 - Logar na Conta");
            System.Console.WriteLine("3 - Sair");

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
            }else if (resp == 3)
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
            System.Console.WriteLine("nomeie sua Conta");
            nomeConta = Console.ReadLine();
            nomes[Ncontagem] = nomeConta;
            Ncontagem++;

            Load();

            System.Console.WriteLine("");
            System.Console.WriteLine(nomeConta);
            System.Console.WriteLine("Qual sera sua senha?:  ");
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
        }
        static bool LogarConta()
        {
            System.Console.WriteLine("Digite o nome da Sua Conta:");
            string typedNomeConta = Console.ReadLine();
            if(nomeConta != typedNomeConta)
            {
                System.Console.WriteLine("Essa conta nao existe,tente Novamente");
                return false;
            }else
            {
                System.Console.WriteLine("Digite a senha");
                string typedSenha = Console.ReadLine();
                if(senha != typedSenha)
                {
                    System.Console.WriteLine("senha incorreta,tente novamente.");
                    return false;
                }
                System.Console.WriteLine("Conta logada com sucesso!");
                return true;
            }
        }
    }
    
}