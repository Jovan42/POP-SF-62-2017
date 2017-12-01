using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class NamestajDataProvider : DataAccess{
        public static NamestajDataProvider Instance { get; } = new NamestajDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Namestaj namestaj = (Namestaj)e;
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();
            namestaji = Projekat.Instance.Namestaji;
            namestaj.ID = namestaji.Count();
            namestaji.Add(namestaj);
            Projekat.Instance.SetNamestaj(namestaji);
        }

        public bool DeleteByID(int id) {
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

        public bool EditByID(Entitet e, int id) {
            Namestaj n = (Namestaj)e;
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

        public ObservableCollection<Entitet> GetAll() {
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();
            foreach (Namestaj korisnk in Projekat.Instance.Namestaji) {
                if (!korisnk.Obrisan)
                    namestaji.Add(korisnk);
            }
            return new ObservableCollection<Entitet>(namestaji);
        }

        public Entitet GetByID(int id) {
            foreach (Namestaj korisnik in Projekat.Instance.Namestaji) {
                if (korisnik.ID == id) {
                    return korisnik;
                }
            }
            return null;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }
#endregion
    }
}
