using System.Data.SQLite;
using cadastro.models;
namespace cadastro.database
{
    public class DbAction
    {
        public SQLiteConnection conectar()
        {
            string sql = @" CREATE TABLE IF NOT EXISTS cliente(
                            id INTEGER PRIMARY KEY  AUTOINCREMENT,
                            nome TEXT NOT NULL,
                            telefone TEXT NOT NULL,
                            cep TEXT NOT NULL,
                            email TEXT NOT NULL
                            );
                            CREATE TABLE IF NOT EXISTS contato(
                            id INTEGER PRIMARY KEY  AUTOINCREMENT,
                            nome TEXT NOT NULL,
                            telefone TEXT NOT NULL UNIQUE,
                            grau_parentesco TEXT NOT NULL UNIQUE,
                            idCliente INTEGER NOT NULL,
                            FOREIGN KEY(idCliente) REFERENCES cliente(id)
                            ); ";

            var cnx = new SQLiteConnection("Data Source=cadastro.db");
            cnx.Open();
            var cmd = new SQLiteCommand(sql, cnx);
            cmd.ExecuteNonQuery();
            return cnx;
        }
        public bool salvarCliente(Cliente cl)
        {
            string sql = "INSERT INTO cliente(nome,telefone,email,cep) VALUES (@nome,@telefone,@email,@cep)";
            using (var db = this.conectar())
            {
                try
                {
                    var cmd = new SQLiteCommand(sql, db);
                    cmd.Parameters.AddWithValue("@nome", cl.nome);
                    cmd.Parameters.AddWithValue("@telefone", cl.telefone);
                    cmd.Parameters.AddWithValue("@email", cl.email);
                    cmd.Parameters.AddWithValue("@cep", cl.cep);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Console.WriteLine("Cliente cadastrado com sucesso :-)");
                        return true;

                    };
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine($"Erro: {0}", ex.Message);

                }
                Console.WriteLine("Cliente cadastrado com sucesso :-(");
                return false;

            }
        }
        public bool salvarContato(Contato ct)
        {
            bool status = false;
            string sql = "INSERT INTO contato(nome,telefone,grau_parentesco,idCliente) VALUES (@nome,@telefone,@grau_parentesco,@idCliente)";
            using (var db = this.conectar())
            {
                try
                {
                    var cmd = new SQLiteCommand(sql, db);
                    cmd.Parameters.AddWithValue("@nome", ct.nome);
                    cmd.Parameters.AddWithValue("@telefone", ct.telefone);
                    cmd.Parameters.AddWithValue("@grau_parentesco", ct.grau_parentesco);
                    cmd.Parameters.AddWithValue("@idCliente", ct.idCliente);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Console.WriteLine("Contato cadastrado com sucesso :-)");
                        status = true;

                    }
                    else
                    {

                        Console.WriteLine("Contato  não cadastrado :-(");
                    }

                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return status;
        }
        public bool validacao(Contato ct)
        {

            string sql = "SELECT * FROM contato WHERE telefone=@telefone AND grau_parentesco=@grau_parentesco AND idCliente=@idCliente";
            List<Contato> listContato = new List<Contato>();
            Contato ctread;

            using (var db = this.conectar())
            {
                var cmd = new SQLiteCommand(sql, db);
                cmd.Parameters.AddWithValue("@telefone", ct.telefone);
                cmd.Parameters.AddWithValue("@grau_parentesco", ct.grau_parentesco);
                cmd.Parameters.AddWithValue("@idCliente", ct.idCliente);
                var dados = cmd.ExecuteReader();
                while (dados.Read())
                {
                    ctread = new Contato();
                    ctread.id = Convert.ToInt32(dados["id"]);
                    ctread.nome = dados["nome"].ToString();
                    ctread.telefone = dados["telefone"].ToString();
                    ctread.idCliente = Convert.ToInt32(dados["idCliente"]);
                    listContato.Add(ctread);

                }
            }
            if (listContato.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }


    }
}