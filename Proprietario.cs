using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmobiliWPF
{
    public class Proprietario
    {
        public int Id { set; get; }
        public string Cf { set; get; }
        public string Nome { set; get; }
        public string Cognome { set; get; }
        public string Telefono { set; get; }
        public Proprietario(int id, string cf, string nome, string cognome, string telefono)
        {
            Id = id;
            Cf = cf;
            Nome = nome;
            Cognome = cognome;
            Telefono = telefono;
        }
    }
}
