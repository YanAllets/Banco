using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class HashService
{
    public static string GerarHash(string senha)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(senha);
        SHA256 sha = SHA256.Create();
        byte [] hash = sha.ComputeHash(bytes);
        string HashString = Convert.ToHexString(hash);
        return HashString;
    }
}