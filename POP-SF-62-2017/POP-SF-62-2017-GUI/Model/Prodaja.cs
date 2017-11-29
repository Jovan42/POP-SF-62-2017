using POP_SF_62_2017.Util.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Prodaja : INotifyPropertyChanged {

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

        private static int GetProdatNamestaj(int prodatNamestaj) {
            return prodatNamestaj;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }

        }
    }
}
