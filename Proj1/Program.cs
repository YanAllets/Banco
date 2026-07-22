using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MySqlConnector;

class Program
{
    
    static bool Rodando = true;
    static List<Conta> contas = new List<Conta>();
    static int IdAtualConta;
    static string conexao = "Server=localhost;Database=banco;User ID=root;Password=Hypnotize-Overrule-Luckiness7;";
    static MySqlConnection conn = new MySqlConnection(conexao);
    

    static void Main()
    
    {
        conn.Open();
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
                if (LogarConta() == true)
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
        static void CriarConta()
        {
            Dictionary<string,object> parametros = new Dictionary<string, object>();
            Console.Clear();
            System.Console.WriteLine("Nomeie sua Conta");
            System.Console.WriteLine("e - Para Sair");
            string nomeConta = Console.ReadLine();
            if (QuitIf(nomeConta))
            {
                return;
            }
            Conta conta = new Conta();

            Load();
            System.Console.WriteLine(nomeConta);
            System.Console.WriteLine("Qual sera sua senha?  ");
            System.Console.WriteLine("e - Para Sair");
            string senha = Console.ReadLine();
            if (QuitIf(senha))
            {
                return;
            }

            string Nsenha;

            System.Console.WriteLine("Digite novamente:  ");
            Nsenha = Console.ReadLine();
            while(senha != Nsenha)
            {
                System.Console.WriteLine("Senha incorreta,tente novamente");
                Nsenha = Console.ReadLine();
            }

            System.Console.WriteLine("Conta Criada com sucesso!");
            parametros.Add("@nome",nomeConta);
            parametros.Add("@senha",Nsenha);
            SqlNonQuery("INSERT INTO Contas (Nome,Senha,Saldo) VALUES (@nome,@senha,0)",parametros);
            Thread.Sleep(2000);
        }
        static bool LogarConta()
        {
            Dictionary<string,object> parametros = new Dictionary<string, object>();
            Thread.Sleep(200);
            Console.Clear();
            
            if(SqlScalarInt("Select Count(*) from contas",parametros) == 0)
            {
                System.Console.WriteLine("Nao existem contas disponiveis - Enter para Continuar"); 
                Console.ReadLine();
                return false;
            }


           System.Console.WriteLine("Digite seu Usuario:  ");
           System.Console.WriteLine("e - Para Sair");
           string nomeConta = Console.ReadLine();
           QuitIf(nomeConta);
           int i = ProcurarConta(nomeConta);

            IdAtualConta = i;
            parametros.Add("@Id",IdAtualConta);

            System.Console.WriteLine("Digite sua senha:");
            System.Console.WriteLine("e - Para Sair");
            string senha = Console.ReadLine();

            if (QuitIf(senha))
            {
                return false;
            }
            while(SqlScalarString("select senha from contas where id = @Id;",parametros) != senha)
            {
                System.Console.WriteLine("Senha incorreta,tente novamente");
                senha = Console.ReadLine();
            }
            System.Console.WriteLine("Usuario Logado com sucesso!");
            Thread.Sleep(2000);
            Console.Clear();
            return true;      
        }
        static void Listar()
        {
        
            SqlReader("select * from contas;");
            System.Console.WriteLine("Enter - Para continuar");
            Console.ReadLine();
        }
        static bool QuitIf(string variavel)
        {   
            if(variavel == "e")
            {
                Environment.Exit(0);
            }
            return false;
        }
        static int ProcurarConta(string nome)
        {
            Dictionary<string,object> parametros = new Dictionary<string, object>();

            int i;
            parametros.Add("@nome",nome);
            while (SqlScalarInt("select Id from contas where Nome = @nome;",parametros) == 0)
            {
                System.Console.WriteLine("Conta nao encontrada tente Novamente:");
                System.Console.WriteLine("Digite e para sair...");
                nome = Console.ReadLine();
                QuitIf(nome);
                parametros["@nome"] = nome;
            }
            i = SqlScalarInt("select Id from contas where Nome = @nome;",parametros);
            return i;
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
                Saldo();
                return true;
            }
            else if(resp == "2")
            {
                Deposito();
                return true;
            }else if(resp == "3")
            {
                Sacar();
                return true;
            }else if(resp == "4")
            {
                Transferir();
                return true;
            }else if(resp == "5")
            {
                return false;
            }
            return true;
        }
        static void Saldo()
        {
            Dictionary<string,object> parametros = new Dictionary<string, object>();
            parametros.Add("@Id",IdAtualConta);
            decimal Saldo = SqlScalarDecimal("select Saldo From contas where id = @Id;",parametros);
            System.Console.WriteLine($"Saldo:{Saldo}");
            System.Console.WriteLine("Enter Para Continuar");
            Console.ReadLine();
        }
        static void Deposito()
        {
        Dictionary<string,object> parametros = new Dictionary<string, object>();      
        System.Console.WriteLine("Digite o valor a Depositar: ");
        string resp = Console.ReadLine();
        decimal valor;
        while(Verificar(resp, out valor) == false)
            {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
            }
        parametros.Add("@valor",valor);
        parametros.Add("@Id",IdAtualConta);
        SqlNonQuery("update contas set saldo = saldo + @valor where id = @Id;",parametros);
        System.Console.WriteLine($"Foram Depositados R${valor} -  Enter para continuar");
        Console.ReadLine();
        }
        static void Sacar()
        {
            Dictionary<string,object> parametros = new Dictionary<string, object>();
            parametros.Add("@Id",IdAtualConta);

            System.Console.WriteLine("Digite o valor a sacar: ");
            string resp = Console.ReadLine();
            decimal saldo = SqlScalarDecimal($"select saldo from contas where id = @Id;",parametros);
            decimal valor;

            while(Verificar(resp, out valor) == false)
            {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
            }

            while(valor > saldo)
            {
                System.Console.WriteLine($"Valor maior que o saldo da conta ({saldo}) Tente novamente");
                resp = Console.ReadLine();

                while(Verificar(resp, out valor) == false)
                {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
                }
            }
            parametros.Add("@valor",valor);
            SqlNonQuery("update contas set saldo = saldo - @valor where id = @Id;",parametros);
            System.Console.WriteLine($"R${valor} Sacado com sucesso, saldo atual:R${saldo}");
            System.Console.WriteLine("Enter para Continuar");
            Console.ReadLine();
        }
        static void Transferir()
        {
            Dictionary<string,object> parametros = new Dictionary<string, object>();

            System.Console.WriteLine("Digite o nome do usuario de DESTINO:");
            string ContaDestino = Console.ReadLine();
            int i = ProcurarConta(ContaDestino);
            parametros.Add("@Id",IdAtualConta);
            decimal saldo = SqlScalarDecimal($"select saldo from contas where id = @Id;",parametros);

            Console.WriteLine("Digite o valor a trasnferir: ");
            string resp = Console.ReadLine();
            decimal valor;
            while(Verificar(resp, out valor) == false)
            {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
            }

            while(valor > saldo)
            {
                System.Console.WriteLine($"Valor maior que o saldo da conta (Saldo:{saldo}) Tente novamente");
                resp = Console.ReadLine();

                while(Verificar(resp, out valor) == false)
                {
                System.Console.WriteLine("tente novamente:");
                resp = Console.ReadLine();
                }
            }
            parametros.Add("@valor",valor);
            parametros.Add("@Destino",i);
            SqlNonQuery("update contas set saldo = saldo - @valor where id = @Id;",parametros);
            SqlNonQuery($"update contas set saldo = saldo + @valor where id = @Destino;",parametros);

            System.Console.WriteLine($"Foram Transferidos R${valor} Para {resp} -  Enter para continuar");
            Console.ReadLine();
        }
        static bool Verificar(string resp, out decimal valor)
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
        //Funcoes antigas que salvavam tudo em JSON
        /*static void SalvarContas()
        {
            string texto;
            texto = JsonSerializer.Serialize(contas);
            File.WriteAllText("contas.json",texto);
        }
        static void AbrirContas()
        {
            bool existe = File.Exists("contas.json");
            string texto;
            if (existe == true)
            {
                texto = File.ReadAllText("contas.json");
                contas = JsonSerializer.Deserialize<List<Conta>>(texto);
            }
        }*/
        static void SqlNonQuery(string query,Dictionary<string,object> parametros)
        {
            string sql = query;
            MySqlCommand comando = new MySqlCommand(sql,conn);
            foreach(var (chave, valor) in parametros)
            {
                comando.Parameters.AddWithValue(chave, valor);
            }
            comando.ExecuteNonQuery();

        }
        static int SqlScalarInt(string query,Dictionary<string,object> parametros)
        {
            string sql = query;
            MySqlCommand comando = new MySqlCommand(sql,conn);
            foreach(var (chave, valor) in parametros)
            {
                comando.Parameters.AddWithValue(chave, valor);
            }
            object resultado = comando.ExecuteScalar();
            int i = Convert.ToInt32(resultado);
            return i;
        }
        static decimal SqlScalarDecimal(string query,Dictionary<string,object> parametros)
        {
            string sql = query;
            MySqlCommand comando = new MySqlCommand(sql,conn);
            foreach(var (chave, valor) in parametros)
            {
                comando.Parameters.AddWithValue(chave, valor);
            }
            object resultado = comando.ExecuteScalar();
            decimal i = Convert.ToDecimal(resultado);
            return i;
        }
        static void SqlReader(string query)
        {
            string sql = query;
            MySqlCommand comando = new MySqlCommand(sql,conn);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                string nome = Convert.ToString(reader["Nome"]);
                string senha = Convert.ToString(reader["Senha"]);
                decimal saldo = Convert.ToDecimal(reader["Saldo"]);
                Console.WriteLine($"Nome:{nome}     |Senha:{senha}     |Saldo:{saldo}");
            }
            reader.Close();
        }
        static string SqlScalarString(string query,Dictionary<string,object> parametros)
        {
            string sql = query;
            MySqlCommand comando = new MySqlCommand(sql,conn);
            foreach(var (chave, valor) in parametros)
            {
                comando.Parameters.AddWithValue(chave, valor);
            }
            object resultado = comando.ExecuteScalar();
            string i = Convert.ToString(resultado);
            return i;
        }
    }
}