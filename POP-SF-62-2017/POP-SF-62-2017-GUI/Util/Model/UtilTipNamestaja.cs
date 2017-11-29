using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util.Model {
    public class UtilTipNamestaja { 
        public static TipNamestaja GetById(int id) {
        foreach (TipNamestaja tipNamestaja in Projekat.Instance.TipoviNamestaja) {
            if (tipNamestaja.ID == id) {
                return tipNamestaja;
            }
        }
        return null;
        }

        public static TipNamestaja GetByName(string name) {
            foreach (TipNamestaja tipNamestaja in Projekat.Instance.TipoviNamestaja) {
                if (tipNamestaja.Naziv == name) {
                    return tipNamestaja;
                }
            }
            return null;
        }

        public static ObservableCollection<TipNamestaja> getAll() {
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            foreach (TipNamestaja tipNamestaja in Projekat.Instance.TipoviNamestaja) {
                if (!tipNamestaja.Obrisan)
                    tipoviNamestaja.Add(tipNamestaja);
            }
            return tipoviNamestaja;
        }

        public static bool DeleteById(int id) {
        ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
        tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
        foreach (TipNamestaja tipNamestaja in tipoviNamestaja) {
            if (tipNamestaja.ID == id) {
                tipNamestaja.Obrisan = true;
                    Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
                    return true;

            }
        }
        return false;
    }

    public static void Add(TipNamestaja a) {
        ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
        tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
        a.ID = tipoviNamestaja.Count();
        tipoviNamestaja.Add(a);
            Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
        }

    public static bool ChangeById(TipNamestaja t, int id) {
        ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
        tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
        foreach (TipNamestaja tipNamestaja in tipoviNamestaja) {
            if (tipNamestaja.ID == id) {
                if (tipNamestaja.Equals(t)) return true;
                tipNamestaja.Naziv = t.Naziv;
                tipNamestaja.Obrisan = t.Obrisan;
                Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
               return true;

            }
        }
        return false;
    }

        public static void Initialize() {
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            tipoviNamestaja.Add(new TipNamestaja {
                Naziv = "",
                Obrisan = true,
                ID = 0
        });
            Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
        }
    }
}
