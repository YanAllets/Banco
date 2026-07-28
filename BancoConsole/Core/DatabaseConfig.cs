using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class DataBaseConfig
{
    public static MySqlConnection conn = new MySqlConnection(AppState.conexao);
}