using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    class Akcija {
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }
        public int NamestajID { get; set; }
        public bool Obrisan { get; set; }
    }
}
