using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Jogos
{
    public class DBconnection
    {
        private string ConnectionString
        {
            get { return @"Server=tcp:jogoslibrary.database.windows.net,1433;Initial Catalog=JogosLibrary;Persist Security Info=False;User ID=Minawa;Password=Drash72/;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"; }
        }
        SqlConnection connection;
        SqlCommand SCmd;
        SqlDataReader dataReader;
        string sql;

        private void ConnectionOpen()
        {
            connection = new SqlConnection(ConnectionString);

            connection.Open();
        }

        private void ConnectionClose()
        {
            SCmd.Dispose();
            connection.Close();
        }

        public string sqlCreate(string nomeJogo, string datatype)
        {
            if (datatype == "nome")
            {
                return "SELECT * FROM Jogos WHERE Nome LIKE '%" + nomeJogo + "%'";
            }
            else if (datatype == "id")
            {
                return "SELECT * FROM Jogos WHERE ID = " + nomeJogo;
            }
            return "";
        }

        public List<JogosModel> JogosDBReceive(string nomeJogo, string datatype)
        {
            ConnectionOpen();

            List<JogosModel> listJogMod = new List<JogosModel>();

            sql = sqlCreate(nomeJogo, datatype);

            SCmd = new SqlCommand(sql, connection);

            dataReader = SCmd.ExecuteReader();

            while(dataReader.Read())
            {
                JogosModel jogosModel = new JogosModel();
                jogosModel.ID = Convert.ToInt32(dataReader.GetValue(0));
                jogosModel.Nome = dataReader.GetValue(1).ToString();
                jogosModel.DatadeLancamento = Convert.ToDateTime(dataReader.GetValue(2));
                jogosModel.Genero = dataReader.GetValue(3).ToString();
                jogosModel.Plataforma = dataReader.GetValue(4).ToString();
                jogosModel.Desenvoveldora = dataReader.GetValue(5).ToString();
                jogosModel.NumerodeJogadores = Convert.ToInt32(dataReader.GetValue(6));
                listJogMod.Add(jogosModel);
            }
            ConnectionClose();
            dataReader.Close();
            return listJogMod;
        }

        public bool JogosDBSend(JogosModel jogosModel)
        {
            ConnectionOpen();

            sql = "INSERT INTO Jogos (Nome, DataLan, Genero, Plataforma, Dev, NumJog)" +
                "VALUES (@Nome, @DataLan, @Genero, @Plataforma, @Dev, @NumJog)";

            SCmd = new SqlCommand(sql, connection);

            SCmd.Parameters.AddWithValue("@Nome", jogosModel.Nome);
            SCmd.Parameters.AddWithValue("@DataLan", jogosModel.DatadeLancamento);
            SCmd.Parameters.AddWithValue("@Genero", jogosModel.Genero);
            SCmd.Parameters.AddWithValue("@Plataforma", jogosModel.Plataforma);
            SCmd.Parameters.AddWithValue("@Dev", jogosModel.Desenvoveldora);
            SCmd.Parameters.AddWithValue("@NumJog", jogosModel.NumerodeJogadores);

            try
            {
                SCmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception erro)
            {
                throw new Exception("Ocorreu um erro durante a adição de novos dados" + erro.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
    }
}
