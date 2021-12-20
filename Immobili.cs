using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmobiliWPF
{
    public class Immobili
    {
        public int Id { set; get; }
        public TipoImmobile Tipo_imm { set; get; }
        public string Via { set; get; }
        public string Superfice { set; get; }
        public int Vani { set; get; }
        public int Prezzo { set; get; } 
        public Proprietario Prop { set; get; }
        public bool In_vendita { set; get; }
        public Immobili(int id,TipoImmobile tipo_imm,string via,string superfice,int vani,int prezzo,Proprietario prop,bool in_vendita)
        {
            Id = id;
            Tipo_imm = tipo_imm;
            Via = via;
            Superfice = superfice;
            Vani = vani;
            Prezzo = prezzo;
            Prop = prop;
            In_vendita = in_vendita;
        }

    }
}
