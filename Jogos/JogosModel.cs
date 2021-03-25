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
        public DateTime DatadeLancamento { get; set; }
        public string Genero { get; set; }
        public string Plataforma { get; set; }
        public string Desenvoveldora { get; set; }
        public int NumerodeJogadores { get; set; }
    }
}
