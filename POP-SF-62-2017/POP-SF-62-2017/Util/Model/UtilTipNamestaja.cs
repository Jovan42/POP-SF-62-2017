using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
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

    public static bool DeleteById(int id) {
        List<TipNamestaja> tipoviNamestaja = new List<TipNamestaja>();
        tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
        foreach (TipNamestaja tipNamestaja in tipoviNamestaja) {
            if (tipNamestaja.ID == id) {
                tipNamestaja.Obrisan = true;
                Projekat.Instance.TipoviNamestaja = tipoviNamestaja;
                return true;

            }
        }
        return false;
    }

    public static void Add(TipNamestaja a) {
        List<TipNamestaja> tipoviNamestaja = new List<TipNamestaja>();
        tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
        a.ID = tipoviNamestaja.Count() + 1;
        tipoviNamestaja.Add(a);
        Projekat.Instance.TipoviNamestaja = tipoviNamestaja;
    }

    public static bool ChangeById(TipNamestaja t, int id) {
        List<TipNamestaja> tipoviNamestaja = new List<TipNamestaja>();
        tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
        foreach (TipNamestaja tipNamestaja in tipoviNamestaja) {
            if (tipNamestaja.ID == id) {
                if (tipNamestaja.Equals(t)) return true;
                tipNamestaja.Naziv = t.Naziv;
                tipNamestaja.Obrisan = t.Obrisan;
                Projekat.Instance.TipoviNamestaja = tipoviNamestaja;
                return true;

            }
        }
        return false;
    }
}
}
