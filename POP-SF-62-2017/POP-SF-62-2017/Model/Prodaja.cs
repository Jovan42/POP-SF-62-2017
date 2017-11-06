using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Prodaja {
        public int ID { get; set; }
        public List<Namestaj> ListaNamestaja { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string Kupac { get; set; }
        public List<string> DodatneUsluge { get; set; }
        public bool Obrisan { get; set; }

        public Prodaja GetById(int id) {
            foreach (Prodaja prodaja in Projekat.Instance.Prodaje) {
                if (prodaja.ID == id) {
                    return prodaja;
                }
            }
            return null;
        }
    }
}
