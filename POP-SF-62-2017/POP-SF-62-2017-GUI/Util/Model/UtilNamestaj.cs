using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util.Model {
    public class UtilNamestaj {

        public static Namestaj GetById(int id) {
            foreach (Namestaj namestaj in Projekat.Instance.Namestaji) {
                if (namestaj.ID == id && namestaj.Obrisan == false) {
                    return namestaj;
                }
            }
            return null;
        }

        public static bool DeleteById(int id) {
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();
            namestaji = Projekat.Instance.Namestaji;
            foreach (Namestaj namestaj in namestaji) {
                if (namestaj.ID == id) {
                    namestaj.Obrisan = true;
                    Projekat.Instance.SetNamestaj(namestaji);
                    return true;
                }
            }
            return false;
        }

        public static void Add(Namestaj a) {
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();
            namestaji = Projekat.Instance.Namestaji;
            a.ID = namestaji.Count();
            namestaji.Add(a);
            Projekat.Instance.SetNamestaj(namestaji);
        }

        public static bool ChangeById(Namestaj n, int id) {
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();
            namestaji = Projekat.Instance.Namestaji;
            foreach (Namestaj namestaj in namestaji) {
                if (namestaj.ID == id) {
                    if (namestaj.Equals(n)) return true;

                    namestaj.Cena = n.Cena;
                    namestaj.Kolicina = n.Kolicina;
                    namestaj.Naziv = n.Naziv;
                    namestaj.Obrisan = n.Obrisan;
                    namestaj.TipNamestajaID = n.TipNamestajaID;
                    Projekat.Instance.SetNamestaj(namestaji);
                    return true;

                }
            }
            return false;
        }
        public static ObservableCollection<Namestaj> getAll() {
            ObservableCollection<Namestaj>namestaji = new ObservableCollection<Namestaj>();
            foreach (Namestaj namesetaj in Projekat.Instance.Namestaji) {
                if (!namesetaj.Obrisan)
                    namestaji.Add(namesetaj);
            }
            return namestaji;
        }

        public static void Initialize() {
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();
            namestaji.Add(new Namestaj {
                ID = 0,
                Cena = 0,
                Kolicina = 0,
                Naziv = "",
                Obrisan = true,
                TipNamestajaID = 0
            });
            Projekat.Instance.SetNamestaj(namestaji);
        }
    }
}
