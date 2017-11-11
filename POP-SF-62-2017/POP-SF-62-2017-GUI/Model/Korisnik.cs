using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Korisnik {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorIme { get; set; }
        public string Lozinka { get; set; }
        public bool Admin { get; set; }
        public bool Obrisan { get; set; }

        public override string ToString() {
            string tip = Admin ? "Admin" : "Korisnik";
            return $"{tip} - {KorIme}: {Ime} {Prezime}";
        }
    }
}
