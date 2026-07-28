using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    
    {
        DataBaseConfig.conn.Open();
        while(AppState.Rodando == true)
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
            ContaService.CriarConta();
            }
            else if (resp == "2")
            {
                if (ContaService.LogarConta() == true)
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
                AppState.Rodando = false;
            }
            else if(resp != "1" && resp != "2" && resp != "3" )
            {
                System.Console.WriteLine("Resposta nao aceita, tente novamente - Enter para continuar...");
                Console.ReadLine();
            }
        }
        static void Listar()
        {
            DataBase.SqlReader("select * from contas;");
            System.Console.WriteLine("Enter - Para continuar");
            Console.ReadLine();
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
                ContaService.Saldo();
                return true;
            }
            else if(resp == "2")
            {
                ContaService.Deposito();
                return true;
            }else if(resp == "3")
            {
                ContaService.Sacar();
                return true;
            }else if(resp == "4")
            {
                ContaService.Transferir();
                return true;
            }else if(resp == "5")
            {
                return false;
            }
            return true;
        }

    }
}