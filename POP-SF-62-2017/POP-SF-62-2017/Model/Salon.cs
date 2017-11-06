using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Salon {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Mail { get; set; }
        public string Sajt { get; set; }
        public string Telefon { get; set; }
        public int PIB { get; set; }
        public int MatBr { get; set; }
        public int ZiroRacun { get; set; }
        public bool Obrisan { get; set; }

        public Salon GetById(int id) {
            foreach (Salon salon in Projekat.Instance.Saloni) {
                if (salon.ID == id) {
                    return salon;
                }
            }
            return null;
        }
    }
}
