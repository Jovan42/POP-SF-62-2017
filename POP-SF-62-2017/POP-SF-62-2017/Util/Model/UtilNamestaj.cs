using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util {
    public class UtilNamestaj {
        public static Namestaj GetById(int id) {
            foreach (Namestaj namestaj in Projekat.Instance.Namestaji) {
                if (namestaj.ID == id) {
                    return namestaj;
                }
            }
            return null;
        }

        public static bool DeleteById(int id) {
            List<Namestaj> namestaji = new List<Namestaj>();
            namestaji = Projekat.Instance.Namestaji;
            foreach (Namestaj namestaj in namestaji) {
                if (namestaj.ID == id) {
                    namestaj.Obrisan = true;
                    Projekat.Instance.Namestaji = namestaji;
                    return true;

                }
            }
            return false;
        }

        public static void Add(Namestaj a) {
            List<Namestaj> namestaji = new List<Namestaj>();
            namestaji = Projekat.Instance.Namestaji;
            a.ID = namestaji.Count() + 1;
            namestaji.Add(a);
            Projekat.Instance.Namestaji = namestaji;
        }

        public static bool ChangeById(Namestaj n, int id) {
            List<Namestaj> namestaji = new List<Namestaj>();
            namestaji = Projekat.Instance.Namestaji;
            foreach (Namestaj namestaj in namestaji) {
                if (namestaj.ID == id) {
                    if (namestaj.Equals(n)) return true;

                    namestaj.Cena = n.Cena;
                    namestaj.Kolicina = n.Kolicina;
                    namestaj.Naziv = n.Naziv;
                    namestaj.Obrisan = n.Obrisan;
                    namestaj.TipNamestajaID = n.TipNamestajaID;
                    Projekat.Instance.Namestaji = namestaji;
                    return true;

                }
            }
            return false;
        }
    }
}
