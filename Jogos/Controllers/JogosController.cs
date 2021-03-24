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
        [HttpGet]
        public IActionResult Get(string nome)
        {
            JogosModel jogosMod = new JogosModel();
            DBconnection dBconnection = new DBconnection();
            jogosMod = dBconnection.JogosDBReceive(nome);
            return Ok(jogosMod);
        }

        [HttpPost]
        public IActionResult Post(JogosModel jogosModel)
        {
            return Ok("Funcionou");
        }
    }
}
