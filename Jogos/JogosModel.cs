using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jogos
{
    public class JogosModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string DataLan { get; set; }
        public string Genero { get; set; }
        public string Plataforma { get; set; }
        public string Dev { get; set; }
        public int NumJog { get; set; }
    }
}
