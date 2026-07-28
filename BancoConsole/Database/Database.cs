using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class DataBase
{
    public static void SqlNonQuery(string query,Dictionary<string,object> parametros,MySqlTransaction? trans = null)
    {
        string sql = query;
        using MySqlCommand comando = new MySqlCommand(sql,DataBaseConfig.conn);

        if(trans != null)
        {
            comando.Transaction = trans;
        }

        foreach(var (chave, valor) in parametros)
        {
            comando.Parameters.AddWithValue(chave, valor);
        }
        comando.ExecuteNonQuery();
    }
    public static T SqlScalar<T>(string query,Dictionary<string,object> parametros,MySqlTransaction? trans = null)
    {
        string sql = query;
        using MySqlCommand comando = new MySqlCommand(sql,DataBaseConfig.conn);

        if(trans != null)
        {
            comando.Transaction = trans;
        }

        foreach(var (chave, valor) in parametros)
        {
            comando.Parameters.AddWithValue(chave, valor);
        }
        object resultado = comando.ExecuteScalar();
        T i = (T)Convert.ChangeType(resultado, typeof(T));
        return i;
    }
    public static void SqlReader(string query)
    {
        string sql = query;
        using MySqlCommand comando = new MySqlCommand(sql,DataBaseConfig.conn);
        using MySqlDataReader reader = comando.ExecuteReader();
        while (reader.Read())
        {
            string nome = Convert.ToString(reader["Nome"]);
            string senha = Convert.ToString(reader["Senha"]);
            decimal saldo = Convert.ToDecimal(reader["Saldo"]);
            Console.WriteLine($"Nome:{nome}     |Senha:{senha}     |Saldo:{saldo}");
        }
        reader.Close();
    }
    public static int ProcurarConta(string nome)
    {
        Dictionary<string,object> parametros = new Dictionary<string, object>();

        int i;
        parametros.Add("@nome",nome);
        while (DataBase.SqlScalar<int>("select Id from contas where Nome = @nome;",parametros) == 0)
        {
            System.Console.WriteLine("Conta nao encontrada tente Novamente:");
            System.Console.WriteLine("Digite e para sair...");
            nome = Console.ReadLine();
            Utils.QuitIf(nome);
            parametros["@nome"] = nome;
        }
        i = DataBase.SqlScalar<int>("select Id from contas where Nome = @nome;",parametros);
        return i;
    }
}