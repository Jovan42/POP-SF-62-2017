using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Akcija {
        public int ID { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }
        public bool Obrisan { get; set; }
        public List<int> NamestajNaAkcijiID { get; set; }
        public double Popust { get; set; } 


    }
}
