using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        private List<int> dodatneUslugeID;

        public List<int> DodatneUslugeID {
            get { return dodatneUslugeID; }
            set { dodatneUslugeID = value; onPropertyChanged("DodatneUsluge"); }
        }

        private bool obrisan;

        public bool Obrisan {
            get { return obrisan; }
            set { obrisan = value; onPropertyChanged("Obrisan"); }
        }

        private List<DodatnaUsluga> dodatnaUsluga;

        [XmlIgnore]
        public List<DodatnaUsluga> DodatnaUsluga {
            get {
                if (dodatnaUsluga == null) {
                    List<DodatnaUsluga> tmp = new List<DodatnaUsluga>();
                    foreach (int id in dodatneUslugeID) {
                        tmp.Add((DodatnaUsluga)DodatnaUslugaDataProvider.Instance.GetByID(id));
                    }
                    return tmp;
                } else
                    return dodatnaUsluga;
            }
            set {
                dodatnaUsluga = value;
                if (dodatnaUsluga != null) {
                    List<int> tmp = new List<int>();
                    foreach (DodatnaUsluga usluga in dodatnaUsluga) {
                        tmp.Add(usluga.ID);
                    }
                    DodatneUslugeID = tmp;
                }
                onPropertyChanged("NamestajiNaAkciji");
            }
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
                DodatneUslugeID = dodatneUslugeID,
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
            //TODO dodaj dodatne usluge
            double cena = 0, popust = 1;
            foreach (int id in DodatneUslugeID) {
                cena += ((DodatnaUsluga)DodatnaUslugaDataProvider.Instance.GetByID(id)).Cena;
            }
            foreach (ProdatNamestaj namestaj in prodatNamestaj) {
                popust = 1;
                foreach (Akcija akcija in AkcijaDataProvider.Instance.GetActiveAkcije()) {
                    if (akcija.NamestajNaAkcijiID.Contains(namestaj.NamestajID) && akcija.Popust / 100 + 1 > popust) {
                        popust = 1 - akcija.Popust / 100;
                    }
                }
                cena += namestaj.Kolicina * ((Namestaj)(NamestajDataProvider.Instance.GetByID(namestaj.NamestajID))).Cena * popust;
            }
            return cena;
        }
    }

}
