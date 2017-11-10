using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Namestaj {
        public string Naziv { get; set; }
        public int ID { get; set; }
        public double Cena { get; set; }
        public int Kolicina { get; set; }
        public int TipNamestajaID { get;  set; }
        public bool Obrisan { get; set; }

        public override string ToString() {

            return $"{Naziv}, {Cena}, {TipNamestaja.GetById(TipNamestajaID).Naziv}";
        }
        
    }
}
