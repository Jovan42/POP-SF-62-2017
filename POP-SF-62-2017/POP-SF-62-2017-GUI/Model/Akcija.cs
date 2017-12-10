using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

//Model.Akcija
//  - int ID
//  - DateTime Početak
//  - DateTime Kraj
//  - List<int> NamestajNaAkcijiID
//  - bool Obrisan

namespace POP_SF_62_2017.Model {
    public class Akcija : Entitet {

        #region Fields and properties
        private int id;

        public int ID {
            get { return id; }
            set { id = value; onPropertyChanged("ID"); }
        }

        private DateTime pocetak;

        public DateTime Pocetak {
            get { return pocetak; }
            set { pocetak = value; onPropertyChanged("Pocetak"); }
        }

        private DateTime kraj;
        public DateTime Kraj {
            get { return kraj; }
            set { kraj = value; onPropertyChanged("Kraj"); }
        }

        private bool obrisan;

        public bool Obrisan {
            get { return obrisan; }
            set { obrisan = value; onPropertyChanged("Obrisan"); }
        }

        private List<int> namestajNaAkcijiID;

        public List<int> NamestajNaAkcijiID {
            get { return namestajNaAkcijiID; }
            set { namestajNaAkcijiID = value; onPropertyChanged("NamestajNaAkciji"); }
        }

        private double popust;

        public double Popust {
            get { return popust; }
            set { popust = value; onPropertyChanged("Popust"); }
        }

        private List<Namestaj> namestajNaAkciji;

        [XmlIgnore]
        public List<Namestaj> NamestajNaAkciji {
            get {
                if (namestajNaAkciji == null) {
                    List<Namestaj> tmp = new List<Namestaj>();
                    foreach (int id in namestajNaAkcijiID) {
                        tmp.Add((Namestaj)NamestajDataProvider.Instance.GetByID(id));
                    }
                    return tmp;
                } else
                    return namestajNaAkciji;
            }
            set {
                namestajNaAkciji = value;
                if (namestajNaAkciji != null) {
                    List<int> tmp = new List<int>();
                    foreach (Namestaj namestaj in namestajNaAkciji) {
                        tmp.Add(namestaj.ID);
                    }
                    NamestajNaAkcijiID = tmp;
                }
                onPropertyChanged("NamestajiNaAkciji");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        //  "<Pocetak> - <Kraj> - Popust = <Popust>%
        //      <Namestaj.Naziv> ..."
        public override string ToString() {
            string tmp = "";
            foreach (int namestajNaAkciji in NamestajNaAkcijiID) {
                if (NamestajDataProvider.Instance.GetByID(namestajNaAkciji) != null)
                    tmp += $"\n\t{((Namestaj)NamestajDataProvider.Instance.GetByID(namestajNaAkciji)).Naziv}";
            }
            return $"({Pocetak.ToString("dd.MM.yyyy.")} - {Kraj.ToString("dd.MM.yyyy.")}) - Popust = {Popust}%:" + tmp;
        }

        public bool IfAkcijaByNamestajID(int id) {
            foreach (int namestajID in NamestajNaAkcijiID) {
                if (id == namestajID) return true;
            }
            return false;
        }

        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

        public object Clone() {
            return new Akcija() {
                ID = id,
                Popust = popust,
                Pocetak = pocetak,
                Kraj = kraj,
                NamestajNaAkcijiID = namestajNaAkcijiID,
                Obrisan = obrisan,
            };
        }
    }
}
