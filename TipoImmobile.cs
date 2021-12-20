using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmobiliWPF
{
    public class TipoImmobile
    {
        public int Id { set; get; }
        public string Descrizione { set; get; }
        public TipoImmobile(int id, string descrizione)
        {
            Id = id;
            Descrizione = descrizione;
        }
    }
}
