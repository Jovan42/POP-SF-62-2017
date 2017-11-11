using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util.Model {
    public static class UtilAkcija {
        public static Akcija GetById(int id) {
            foreach (Akcija akcija in Projekat.Instance.Akcije) {
                if (akcija.ID == id) {
                    return akcija;
                }
            }
            return null;
        }

        public static bool DeleteById(int id) {
            List<Akcija> akcije = new List<Akcija>();
            akcije = Projekat.Instance.Akcije;
            foreach (Akcija akcija in akcije) {
                if (akcija.ID == id) {
                    akcija.Obrisan = true;
                    Projekat.Instance.Akcije = akcije;
                    return true;

                }
            }
            return false;
        }

        public static void Add(Akcija a) {
            List<Akcija> akcije = new List<Akcija>();
            akcije = Projekat.Instance.Akcije;
            a.ID = akcije.Count();
            akcije.Add(a);
            Projekat.Instance.Akcije = akcije;
        }

        public static bool ChangeById(Akcija a, int id) {
            List<Akcija> akcije = new List<Akcija>();
            akcije = Projekat.Instance.Akcije;
            foreach (Akcija akcija in akcije) {
                if (akcija.ID == id) {
                    if (akcija.Equals(a)) return true;

                    akcija.Kraj = a.Kraj;
                    akcija.NamestajNaAkcijiID = a.NamestajNaAkcijiID;
                    akcija.Obrisan = a.Obrisan;
                    akcija.Pocetak = a.Pocetak;
                    akcija.Popust = a.Popust;
                    Projekat.Instance.Akcije = akcije;
                    return true;

                }
            }
            return false;
        }

        public static List<Akcija> getAll() {
            List<Akcija> akcije = new List<Akcija>();
            foreach (Akcija akcija in Projekat.Instance.Akcije) {
                if (!akcija.Obrisan)
                    akcije.Add(akcija);
            }
            return akcije;
        }

        public static void Initialize() {
            List<Akcija> akcije = new List<Akcija>();
            List<int> i = new List<int>();
            i.Add(0);
            akcije.Add(new Akcija {
                ID = 0,
                Popust = 0,
                Pocetak = DateTime.MinValue,
                Kraj = DateTime.MinValue,
                NamestajNaAkcijiID = i,
                Obrisan = true,
            });
            Projekat.Instance.Akcije = akcije;
        }
    }
}

