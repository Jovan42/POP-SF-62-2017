using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util.Model {
    class UtilProdaja {
        public static Prodaja GetById(int id) {
            foreach (Prodaja prodaja in Projekat.Instance.Prodaje) {
                if (prodaja.ID == id) {
                    return prodaja;
                }
            }
            return null;
        }

        public static bool DeleteById(int id) {
            List<Prodaja> prodaje = new List<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            foreach (Prodaja prodaja in prodaje) {
                if (prodaja.ID == id) {
                    prodaja.Obrisan = true;
                    Projekat.Instance.Prodaje = prodaje;
                    return true;

                }
            }
            return false;
        }

        public static void Add(Prodaja a) {
            List<Prodaja> prodaje = new List<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            a.ID = prodaje.Count();
            prodaje.Add(a);
            Projekat.Instance.Prodaje = prodaje;
        }

        public static bool ChangeById(Prodaja p, int id) {
            List<Prodaja> prodaje = new List<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            foreach (Prodaja prodaja in prodaje) {
                if (prodaja.ID == id) {
                    if (prodaja.Equals(p)) return true;
                    prodaja.DatumProdaje = p.DatumProdaje;
                    prodaja.DodatneUsluge = p.DodatneUsluge;
                    prodaja.Kupac = p.Kupac;
                    prodaja.ProdatNamestaj = p.ProdatNamestaj;
                    prodaja.Obrisan = p.Obrisan;
                    prodaja.Kolicina = p.Kolicina;
                    Projekat.Instance.Prodaje = prodaje;
                    return true;

                }
            }
            return false;
        }

        public static List<Prodaja> getAll() {
            List<Prodaja> prodaje = new List<Prodaja>();
            foreach (Prodaja prodaja in Projekat.Instance.Prodaje) {
                if (!prodaja.Obrisan)
                    prodaje.Add(prodaja);
            }
            return prodaje;
        }

        public static void Initialize() {
            List<Prodaja> prodaje = new List<Prodaja>();
            List<int> i = new List<int>();
            
            i.Add(0);
            prodaje.Add(new Prodaja {
                ID = 0,
                DatumProdaje = DateTime.MinValue,
                DodatneUsluge = null,
                Kupac = "",
                ProdatNamestaj = i,
                Kolicina = i,
                Obrisan = true
        });
            Projekat.Instance.Prodaje = prodaje;
        }
    }
}
