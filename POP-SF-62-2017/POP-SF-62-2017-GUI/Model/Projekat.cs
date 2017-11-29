using POP_SF_62_2017.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Projekat {
        public static Projekat Instance { get; } = new Projekat();

        public ObservableCollection<Namestaj> Namestaji { get; set; }
        public ObservableCollection<TipNamestaja> TipoviNamestaja { get; set; }
        public ObservableCollection<Akcija> Akcije { get; set; }
        public ObservableCollection<Korisnik> Korisnici { get; set; }
        public ObservableCollection<Prodaja> Prodaje { get; set; }
        public ObservableCollection<Salon> Saloni { get; set; }

       private Projekat() {
            TipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
            Namestaji = GenericSerializer.Deserialize<Namestaj>("namestaji.xml");
            Akcije = GenericSerializer.Deserialize<Akcija>("akcije.xml");
            Prodaje = GenericSerializer.Deserialize<Prodaja>("prodaje.xml");
            Korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
            Saloni = GenericSerializer.Deserialize<Salon>("saloni.xml");
        }    
        
        public void SetNamestaj(ObservableCollection<Namestaj> namestaj) {
            GenericSerializer.Serialize<Namestaj>("namestaji.xml", namestaj);
        }
        public void SetTipoveNamestaja(ObservableCollection<TipNamestaja> tipNamestaj) {
            GenericSerializer.Serialize<TipNamestaja>("tipovi_namestaja.xml", tipNamestaj);
        }
        public void SetAkcije(ObservableCollection<Akcija> akcija) {
            GenericSerializer.Serialize<Akcija>("akcije.xml", akcija);
        }
        public void SetProdaje(ObservableCollection<Prodaja> prodaja) {
            GenericSerializer.Serialize<Prodaja>("prodaje.xml", prodaja);
        }
        public void SetKorisnici(ObservableCollection<Korisnik> korisnik) {
            GenericSerializer.Serialize<Korisnik>("korisnici.xml", korisnik);
        }
        public void SetSaloni(ObservableCollection<Salon> saloni) {
            GenericSerializer.Serialize<Salon>("saloni.xml", saloni);
        }
    }
}
