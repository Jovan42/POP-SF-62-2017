using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class TipNamestaja {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public bool Obrisan { get; set; }

        public static TipNamestaja GetById(int id) {
            foreach (TipNamestaja tipNamestaja in Projekat.Instance.TipoviNamestaja) {
                if (tipNamestaja.ID == id) {
                    return tipNamestaja;
                }
            }
            return null;
        }
    }
}
