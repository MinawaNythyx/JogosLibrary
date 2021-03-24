using System;
using System.Collections.Generic;
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
            dataReader.Close();
            SCmd.Dispose();
            connection.Close();
        }

        public JogosModel JogosDBReceive(string nomeJogo)
        {
            ConnectionOpen();

            JogosModel jogosModel = new JogosModel();

            sql = "SELECT * FROM Jogos WHERE Nome = '" + nomeJogo + "'";

            SCmd = new SqlCommand(sql, connection);

            dataReader = SCmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (nomeJogo == dataReader.GetValue(1).ToString())
                {
                    jogosModel.ID = Convert.ToInt32(dataReader.GetValue(0));
                    jogosModel.Nome = dataReader.GetValue(1).ToString();
                    jogosModel.DataLan = dataReader.GetValue(2).ToString();
                    jogosModel.Genero = dataReader.GetValue(3).ToString();
                    jogosModel.Plataforma = dataReader.GetValue(4).ToString();
                    jogosModel.Dev = dataReader.GetValue(5).ToString();
                    jogosModel.NumJog = Convert.ToInt32(dataReader.GetValue(6));
                }
            }
            ConnectionClose();

            return jogosModel;
        }

        
    }
}
