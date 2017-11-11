using POP_SF_62_2017.Util.Model;
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

        public override string ToString() {
            string tmp = "";
            foreach (int namestajNaAkciji in NamestajNaAkcijiID) {
                if (UtilNamestaj.GetById(namestajNaAkciji) != null)
                    tmp += $"\n\t{UtilNamestaj.GetById(namestajNaAkciji).Naziv}";
            }
            return $"{ID}. ({Pocetak.ToString("dd.MM.yyyy.")} - {Kraj.ToString("dd.MM.yyyy.")}) - Popust = {Popust}%:" + tmp;
        }

    }
}
