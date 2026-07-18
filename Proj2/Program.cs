using System;
using System.Net;
using Microsoft.VisualBasic;
class Program
{
    static void Main()
    {
       if (int.TryParse(Console.ReadLine(), out int numero))
    {
    Console.WriteLine("Número válido!");
    }
else
    {
    Console.WriteLine("Você não digitou um número.");
    }
    }
    
}