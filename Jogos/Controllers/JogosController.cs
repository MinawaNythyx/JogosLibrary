using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jogos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        List<JogosModel> listJogMod = new List<JogosModel>();
        //JogosModel jogosMod = new JogosModel();
        DBconnection dBconnection = new DBconnection();

        [HttpGet("GetNome/{nome}")]
        public IActionResult GetNome(string nome)
        {
            try
            {
                listJogMod = dBconnection.JogosDBReceive(nome, "nome");
                return Ok(listJogMod);
            }
            catch(Exception erro)
            {
                throw new Exception("Ocorreu um erro ao buscar os dados" + erro.Message);
            }
        }

        [HttpGet("GetID/{id}")]
        public IActionResult GetID(string id)
        {
            try
            {
                listJogMod = dBconnection.JogosDBReceive(id, "id");
                return Ok(listJogMod);
            }
            catch(Exception erro)
            {
                throw new Exception("Ocorreu um erro ao buscar os dados" + erro.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(JogosModel jogosModel)
        {
            return Ok(dBconnection.JogosDBSend(jogosModel));
        }
    }
}
