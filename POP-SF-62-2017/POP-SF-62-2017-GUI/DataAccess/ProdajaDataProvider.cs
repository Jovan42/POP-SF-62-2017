using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class ProdajaDataProvider {
        public static ProdajaDataProvider Instance { get; } = new ProdajaDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Prodaja prodaja = (Prodaja)e;
            ObservableCollection<Prodaja> prodaje = new ObservableCollection<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            prodaja.ID = prodaje.Count();
            prodaje.Add(prodaja);
            Projekat.Instance.SetProdaje(prodaje);
        }

        public bool DeleteByID(int id) {
            ObservableCollection<Prodaja> prodaje = new ObservableCollection<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            foreach (Prodaja prodaja in prodaje) {
                if (prodaja.ID == id) {
                    prodaja.Obrisan = true;
                    Projekat.Instance.SetProdaje(prodaje);
                    return true;

                }
            }
            return false;
        }

        public bool EditByID(Entitet e, int id) {
            Prodaja p = (Prodaja)e;
            ObservableCollection<Prodaja> prodaje = new ObservableCollection<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            foreach (Prodaja prodaja in prodaje) {
                if (prodaja.ID == id) {
                    prodaja.DatumProdaje = p.DatumProdaje;
                    prodaja.DodatneUslugeID = p.DodatneUslugeID;
                    prodaja.Kupac = p.Kupac;
                    prodaja.ProdatNamestaj = p.ProdatNamestaj;
                    prodaja.Obrisan = p.Obrisan;
                    Projekat.Instance.Prodaje = prodaje;
                    return true;
                }
            }
            return false;
        }

        public ObservableCollection<Prodaja> GetAll() {
            ObservableCollection<Prodaja> prodaja = new ObservableCollection<Prodaja>();
            foreach (Prodaja korisnk in Projekat.Instance.Prodaje) {
                if (!korisnk.Obrisan)
                    prodaja.Add(korisnk);
            }
            return prodaja;
        }

        public Entitet GetByID(int id) {
            foreach (Prodaja korisnik in Projekat.Instance.Prodaje) {
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
