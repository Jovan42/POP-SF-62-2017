using POP_SF_62_2017.Util.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
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

        private List<int> prodatNamestaj;

        public List<int> ProdatNamestaj {
            get { return prodatNamestaj; }
            set { prodatNamestaj = value; onPropertyChanged("ProdatNamestaj"); }
        }

        private List<int> kolicina;

        public List<int> Kolicina {
            get { return kolicina; }
            set { kolicina = value; onPropertyChanged("Kolicina"); }
        }

        private string kupac;

        public string Kupac {
            get { return kupac; }
            set { kupac = value; onPropertyChanged("Kupac"); }
        }

        private List<string> dodatneUsluge;

        public List<string> DodatneUsluge {
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
            foreach (int prodatNamestaj in ProdatNamestaj) {
                if (UtilNamestaj.GetById(prodatNamestaj) != null && !(UtilNamestaj.GetById(prodatNamestaj).Obrisan)) {
                    int index = ProdatNamestaj.IndexOf(prodatNamestaj);
                    tmp += $"\n\t{UtilNamestaj.GetById(prodatNamestaj).Naziv}: {Kolicina[index]}";
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
            return new Prodaja() {
                ID = id,
                DatumProdaje = datumProdaje,
                DodatneUsluge = dodatneUsluge,
                Kupac = kupac,
                ProdatNamestaj = prodatNamestaj,
                Kolicina = kolicina,
                Obrisan = obrisan
            };
        }
    }
}
