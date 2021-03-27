using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jogos.Controllers
{
    //Rota utilizada para ativar a API
    [Route("[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        //Lista modelo que sera utilizada para retornar os itens em JSON
        List<JogosModel> listJogMod = new List<JogosModel>();

        //Instancia da classe que comunica com o banco de dados
        DBconnection dBconnection = new DBconnection();

        //Metodo Get para buscar por nome, podendo retornar muitos itens
        [HttpGet("GetNome/{nome}")]
        public IActionResult GetNome(string nome)
        {
            //Lista recebida atraves do metodo
            return Ok(dBconnection.JogosDBReceive(nome, "nome"));
        }

        //Metodo Get para buscar por ID retornando um unico item
        [HttpGet("GetID/{id}")]
        public IActionResult GetID(string id)
        {
            int valorlim = dBconnection.DataMax();
            //Lista recebida atraves do metodo
            try
            {
                listJogMod = dBconnection.JogosDBReceive(id, "id");
            }
            catch
            {
                return Ok("Essa recebe apenas numeros");
            }
            if(Convert.ToInt32(id) >= 1 && Convert.ToInt32(id) <= valorlim)
            {
                return Ok(listJogMod);
            }
            else
            {
                return Ok($"Essa funcionalidade so retorna com IDs de 1 até {valorlim}");
            }
        }

        //Metodo Post para inserir dados no banco de dados
        [HttpPost]
        public IActionResult Post(JogosModel jogosModel)
        {
            //Retornando um bool para saber se foi corretamente inserido
            return Ok(dBconnection.JogosDBSend(jogosModel));
        }
    }
}
