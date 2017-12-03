using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Model.Prodaja
//  - int ID
//  - DateTime DatumProdaje
//  - List<int> ProdatNamestaj
//  - List<int> Kolicina
//  - string Kupac
//  - Map<string, double> DodatneUsluge //TODO 
//  - bool Obrisan

namespace POP_SF_62_2017.Model {
    public class Prodaja : Entitet {

        #region Fields and properties
        private int id;

        public int ID {
            get { return id; }
            set { id = value; onPropertyChanged("ID"); }
        }

        private DateTime datumProdaje;

        public DateTime DatumProdaje {
            get { return datumProdaje; }
            set { datumProdaje = value; onPropertyChanged("DatumProdaje"); }
        }

        private ObservableCollection<ProdatNamestaj> prodatNamestaj;

        public ObservableCollection<ProdatNamestaj> ProdatNamestaj {
            get { return prodatNamestaj; }
            set { prodatNamestaj = value; onPropertyChanged("ProdatNamestaj"); }
        }

        private string kupac;

        public string Kupac {
            get { return kupac; }
            set { kupac = value; onPropertyChanged("Kupac"); }
        }

        private List<DodatnaUsluga> dodatneUsluge;

        public List<DodatnaUsluga> DodatneUsluge {
            get { return dodatneUsluge; }
            set { dodatneUsluge = value; onPropertyChanged("DodatneUsluge"); }
        }

        private bool obrisan;

        public bool Obrisan {
            get { return obrisan; }
            set { obrisan = value; onPropertyChanged("Obrisan"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        //"<DatumProdaje>
        //  <Namestaj.Naziv>: <Kolicina>
        //..."
        public override string ToString() {
            string tmp = "";
            foreach (ProdatNamestaj prodatNamestaj in ProdatNamestaj) {
                if (NamestajDataProvider.Instance.GetByID(prodatNamestaj.NamestajID) != null &&
                    !(((Namestaj)NamestajDataProvider.Instance.GetByID(prodatNamestaj.NamestajID)).Obrisan)) {
                    tmp += $"\n\t{((Namestaj)NamestajDataProvider.Instance.GetByID(prodatNamestaj.NamestajID)).Naziv}: " +
                        $"{prodatNamestaj.Kolicina}";
                }
            }
            return $"({DatumProdaje.ToString("dd. MM. yyyy.")})" + tmp;
        }

        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }

        }

        public object Clone() {
            Prodaja prodaja = new Prodaja() {
                ID = id,
                DatumProdaje = datumProdaje,
                DodatneUsluge = dodatneUsluge,
                Kupac = kupac,
                ProdatNamestaj = prodatNamestaj,
                Obrisan = obrisan
            };
            if (DatumProdaje == null) DatumProdaje = DateTime.Now;
            return prodaja;
        }

        public int getKolicinaFromNamestajID(int id) {
            foreach (ProdatNamestaj prodatNamestaj in ProdatNamestaj) {
                if (prodatNamestaj.NamestajID == id)
                    return prodatNamestaj.Kolicina;
            }
            return 0;
        }

        public double getUkupnaCena() {
            double cena = 0;
            foreach (ProdatNamestaj prodatNamestaj in ProdatNamestaj) {
                cena += prodatNamestaj.Kolicina * ((Namestaj)NamestajDataProvider.Instance.GetByID(prodatNamestaj.NamestajID)).Cena;
            }

            foreach(DodatnaUsluga dodatnaUsluga in DodatneUsluge) {
                cena += dodatnaUsluga.Cena;
            }

            cena *= 1.2;

            return cena;
        }
    }

}
