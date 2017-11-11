using POP_SF_62_2017.Util.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Prodaja {
        public int ID { get; set; }
        public DateTime DatumProdaje { get; set; }
        public List<int> ProdatNamestaj { get; set; } 
        public List<int> Kolicina { get; set; }
        public string Kupac { get; set; }
        public List<string> DodatneUsluge { get; set; }
        public bool Obrisan { get; set; }

        public override string ToString() {
            string tmp = "";
            foreach (int prodatNamestaj in ProdatNamestaj) {
                if (UtilNamestaj.GetById(prodatNamestaj) != null && !(UtilNamestaj.GetById(prodatNamestaj).Obrisan)) {
                    int index = ProdatNamestaj.IndexOf(prodatNamestaj);
                    tmp += $"\n\t{UtilNamestaj.GetById(prodatNamestaj).Naziv}: {Kolicina[index]}";
                }
            }
            return $"{ID}. ({DatumProdaje.ToString("dd. MM. yyyy.")})" + tmp;
        }

        private static int GetProdatNamestaj(int prodatNamestaj) {
            return prodatNamestaj;
        }
    }
}
