using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Jogos
{
    //Classe para comunicar com o banco de dados
    public class DBconnection
    {
        //String de conexão com o banco de dados na nuvem
        private string ConnectionString
        {
            get { return @"Server=tcp:jogoslibrary.database.windows.net,1433;Initial Catalog=JogosLibrary;Persist Security Info=False;User ID=Minawa;Password=Drash72/;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"; }
        }
        //Comandos para conexão instanciandos
        SqlConnection connection;
        SqlCommand SCmd;
        SqlDataReader dataReader;
        string sql;
        
        //Metodo para abrir a conexão
        private void ConnectionOpen()
        {
            connection = new SqlConnection(ConnectionString);

            connection.Open();
        }

        //Metodo para o fechamento da conexão e descarte de comando previamente alocados
        private void ConnectionClose()
        {
            SCmd.Dispose();
            connection.Close();
        }

        //Metodo para definir o meio de busca, por ID ou por Nome
        public string sqlCreate(string nomeJogo, string datatype)
        {
            if (datatype == "nome")
            {
                //Caso o metodo seja por nome, pesquisa usando "LIKE" para todo objeto que contenha o nome desejado
                return "SELECT * FROM Jogos WHERE Nome LIKE '%" + nomeJogo + "%'";
            }
            else if (datatype == "id")
            {
                //Caso o metodo seja por ID, pesquisa direto pelo ID desejado, é esperado nesse caso que o ID sera usado para buscar a informação precisamente
                return "SELECT * FROM Jogos WHERE ID = " + nomeJogo;
            }
            else
            {
                //O metodo exige um else
                return "";
            }
        }

        public List<JogosModel> JogosDBReceive(string nomeJogo, string datatype)
        {
            ConnectionOpen();

            //Lista de objetos para o retorno
            List<JogosModel> listJogMod = new List<JogosModel>();

            //Comando que foi instanciado anteriormente, definindo o tipo de busca usada
            sql = sqlCreate(nomeJogo, datatype);

            //Ativando o comando sql
            SCmd = new SqlCommand(sql, connection);

            //Leitor utilizando para receber as informações do banco de dados
            dataReader = SCmd.ExecuteReader();

            //Comando enviados para tentar comunicar com o banco de dados
            try
            {
                //Um loop para receber todos os parametros buscados
                while (dataReader.Read())
                {
                    //Recebendo e montando o modelo para ser colocado na lista que sera retornada
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
            }
            catch (Exception erro)
            {
                //Possivel erro que ocorrer tras uma Exception
                throw new Exception("Ocorreu um erro ao buscar os dados" + erro.Message);
            }
            finally
            {
                //Finally para fechar as conexões
                ConnectionClose();
                //O fechamento do DataReader não foi colocado junto ao metodo de fechar conexão pois apenas esse metodo o utiliza
                dataReader.Close();
            }
            //O retorno da lista
            return listJogMod;
        }

        public bool JogosDBSend(JogosModel jogosModel)
        {
            ConnectionOpen();

            //Criando a string de inserção no banco de dados
            sql = "INSERT INTO Jogos (Nome, DataLan, Genero, Plataforma, Dev, NumJog)" +
                "VALUES (@Nome, @DataLan, @Genero, @Plataforma, @Dev, @NumJog)";

            SCmd = new SqlCommand(sql, connection);

            //Acionando o comando e passando os valores separadamente para maior segurança
            SCmd.Parameters.AddWithValue("@Nome", jogosModel.Nome);
            SCmd.Parameters.AddWithValue("@DataLan", jogosModel.DatadeLancamento);
            SCmd.Parameters.AddWithValue("@Genero", jogosModel.Genero);
            SCmd.Parameters.AddWithValue("@Plataforma", jogosModel.Plataforma);
            SCmd.Parameters.AddWithValue("@Dev", jogosModel.Desenvoveldora);
            SCmd.Parameters.AddWithValue("@NumJog", jogosModel.NumerodeJogadores);

            try
            {
                //Tentativa de execução do comando
                SCmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception erro)
            {
                //Caso tenha algum erro, a mensagem sera retornada
                throw new Exception("Ocorreu um erro durante a adição de novos dados" + erro.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
    }
}
