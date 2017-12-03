using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

//Model.Namestaj
//  - int ID
//  - string Naziv
//  - double Cena
//  - int Kolicina
//  - int TipNamestajaID
//  - bool Obrisan
//  - TipNamestaja TipNamestaja [XmlIgnore]

namespace POP_SF_62_2017.Model  {
    public class Namestaj : Entitet {

        #region Fields and properties

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
                if (tipNamestaja == null) {
                    TipNamestaja tmp = (TipNamestaja)TipNamestajaDataProvider.Instance.GetByID(tipNamestajaID);
                    return tmp;
                }               
                else
                    return tipNamestaja;
            }
            set {
                tipNamestaja = value;
                if (tipNamestaja != null)
                TipNamestajaID = tipNamestaja.ID;
                OnPropertyChanged("TipNamestaja");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

#endregion

        //"<Naziv>, <Cena>, <TipNamestaja.Naziv>"
        public override string ToString() {

            return $"{Naziv}, {Cena}, {TipNamestaja.GetById(TipNamestajaID).Naziv}";
        }
        
        protected void OnPropertyChanged(string properyName) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

        public object Clone() {
            return new Namestaj() {
                ID = id,
                Cena = cena,
                Kolicina = kolicina,
                Naziv = naziv,
                Obrisan = obrisan,
                TipNamestajaID = tipNamestajaID,
                TipNamestaja = tipNamestaja
            };
        }
    }
}
