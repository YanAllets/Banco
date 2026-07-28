using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class AppState
{
    public static bool Rodando = true;
    public static int IdAtualConta;
    public static string conexao = "Server=localhost;Database=banco;User ID=root;Password=Hypnotize-Overrule-Luckiness7;";
}