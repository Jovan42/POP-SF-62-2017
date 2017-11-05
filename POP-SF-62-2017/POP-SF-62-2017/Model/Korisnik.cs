using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    class Korisnik {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorIme { get; set; }
        public string Lozinka { get; set; }
        public bool Admin { get; set; }
        public bool Obrisan { get; set; }
    }
}
