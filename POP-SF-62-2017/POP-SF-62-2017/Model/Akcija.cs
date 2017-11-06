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
        public int NamestajID { get; set; }
        public bool Obrisan { get; set; }
        public List<Namestaj> NamestajNaAkciji { get; set; }
        public double Popust { get; set; }

        public Akcija GetById(int id) {
            foreach (Akcija akcija in Projekat.Instance.Akcije) {
                if (akcija.ID == id) {
                    return akcija;
                }
            }
            return null;
        }
    }
}
