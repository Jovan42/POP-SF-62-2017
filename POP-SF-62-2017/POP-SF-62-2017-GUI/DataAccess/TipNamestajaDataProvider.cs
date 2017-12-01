using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class TipNamestajaDataProvider : DataAccess {
        public static KorisnikDataProvider Instance { get; } = new KorisnikDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            TipNamestaja tipNamestaja = (TipNamestaja)e;
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
            tipNamestaja.ID = tipoviNamestaja.Count();
            tipoviNamestaja.Add(tipNamestaja);
            Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
        }

        public bool DeleteByID(int id) {
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
            foreach (TipNamestaja akcija in tipoviNamestaja) {
                if (akcija.ID == id) {
                    akcija.Obrisan = true;
                    Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
                    return true;

                }
            }
            return false;
        }

        public bool EditByID(Entitet e, int id) {
            TipNamestaja t = (TipNamestaja)e;
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

        public ObservableCollection<Entitet> GetAll() {
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            foreach (TipNamestaja korisnk in Projekat.Instance.TipoviNamestaja) {
                if (!korisnk.Obrisan)
                    tipoviNamestaja.Add(korisnk);
            }
            return new ObservableCollection<Entitet>(tipoviNamestaja);
        }

        public Entitet GetByID(int id) {
            foreach (TipNamestaja tipNamestaja in Projekat.Instance.TipoviNamestaja) {
                if (tipNamestaja.ID == id) {
                    return tipNamestaja;
                }
            }
            return null;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }
        #endregion

    }
}
