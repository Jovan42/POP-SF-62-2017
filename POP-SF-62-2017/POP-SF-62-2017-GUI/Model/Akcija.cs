﻿using POP_SF_62_2017.Util.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Model.Akcija
//  - int ID
//  - DateTime Početak
//  - DateTime Kraj
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

        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        //  "<Pocetak> - <Kraj> - Popust = <Popust>%
        //      <Namestaj.Naziv> ..."
        public override string ToString() {
            string tmp = "";
            foreach (int namestajNaAkciji in NamestajNaAkcijiID) {
                if (UtilNamestaj.GetById(namestajNaAkciji) != null)
                    tmp += $"\n\t{UtilNamestaj.GetById(namestajNaAkciji).Naziv}";
            }
            return $"({Pocetak.ToString("dd.MM.yyyy.")} - {Kraj.ToString("dd.MM.yyyy.")}) - Popust = {Popust}%:" + tmp;
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
