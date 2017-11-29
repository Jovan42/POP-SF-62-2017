using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF_62_2017.Model  {
    public class Namestaj : INotifyPropertyChanged {
        private string naziv;

        public string Naziv {
            get { return naziv; }
            set { naziv = value; OnPropertyChanged("Naziv"); }
        }

        private int id;

        public int ID {
            get { return id; }
            set {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        private double cena;

        public double Cena {
            get { return cena; }
            set { cena = value; OnPropertyChanged("Cena"); }
        }

        private int kolicina;

        public int Kolicina {
            get { return kolicina; }
            set { kolicina = value; OnPropertyChanged("Kolicina"); }
        }

        private int tipNamestajaID;

        public int TipNamestajaID {
            get { return tipNamestajaID; }
            set { tipNamestajaID = value; OnPropertyChanged("TipNamestajaID"); }
        }

        private bool obrisan;

        public bool Obrisan {
            get { return obrisan; }
            set { obrisan = value; OnPropertyChanged("Obrisan"); }
        }

        private TipNamestaja tipNamestaja;

        [XmlIgnore]
        public TipNamestaja TipNamestaja {
            get {
                if (tipNamestaja == null)
                    return Util.Model.UtilTipNamestaja.GetById(tipNamestajaID);
                else
                    return tipNamestaja;
            }
            set {
                tipNamestaja = value;
                TipNamestajaID = tipNamestaja.ID;
                OnPropertyChanged("TipNamestaja");
            }
        }


        public override string ToString() {

            return $"{Naziv}, {Cena}, {TipNamestaja.GetById(TipNamestajaID).Naziv}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        
        protected void OnPropertyChanged(string properyName) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

    }
}
