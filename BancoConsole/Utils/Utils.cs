using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class Utils
{
    public static void Load()
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
    public static bool QuitIf(string variavel)
    {   
        if(variavel == "e")
        {
            Environment.Exit(0);
        }
        return false;
    }
    public static bool Verificar(string resp, out decimal valor)
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
}
