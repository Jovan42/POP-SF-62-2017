using POP_SF_62_2017.Util;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Projekat : INotifyPropertyChanged {
        public static Projekat Instance { get; } = new Projekat();

        private Entitet izabrano;

        public Entitet Izabrano {
            get { return izabrano; }
            set { izabrano = value; onPropertyChanged("Izabrano"); }
        }

        private ObservableCollection<Namestaj> namestaji;

        public ObservableCollection<Namestaj> Namestaji {
            get { return namestaji; }
            set { namestaji = value; onPropertyChanged("Namestaji"); }
        }

        private ObservableCollection<Akcija> akcije;

        public ObservableCollection<Akcija> Akcije {
            get { return akcije; }
            set { akcije = value; onPropertyChanged("Akcije"); }
        }

        private ObservableCollection<TipNamestaja> tipoviNamestaja;

        public ObservableCollection<TipNamestaja> TipoviNamestaja {
            get { return tipoviNamestaja; }
            set { tipoviNamestaja = value; onPropertyChanged("TipoviNamestaja"); }
        }

        private ObservableCollection<Korisnik> korisnici;

        public ObservableCollection<Korisnik> Korisnici {
            get { return korisnici; }
            set { korisnici = value; onPropertyChanged("Korisnici"); }
        }

        private ObservableCollection<Prodaja> prodaje;

        public ObservableCollection<Prodaja> Prodaje {
            get { return prodaje; }
            set { prodaje = value; onPropertyChanged("Prodaje"); }
        }

        private ObservableCollection<Salon> saloni;

        public ObservableCollection<Salon> Saloni {
            get { return saloni; }
            set { saloni = value; onPropertyChanged("Saloni"); }
        }

        private ObservableCollection<DodatnaUsluga> dodatneUsluge;

        public ObservableCollection<DodatnaUsluga> DodatneUsluge {
            get { return dodatneUsluge; }
            set { dodatneUsluge = value; onPropertyChanged("DodatneUsluge"); }
        }

        //public ObservableCollection<Namestaj> Namestaji { get; set; }
        //public ObservableCollection<TipNamestaja> TipoviNamestaja { get; set; }
        //public ObservableCollection<Akcija> Akcije { get; set; }
        // public ObservableCollection<Korisnik> Korisnici { get; set; }
        //public ObservableCollection<Prodaja> Prodaje { get; set; }
        //public ObservableCollection<Salon> Saloni { get; set; }
        //public ObservableCollection<DodatnaUsluga> DodatneUsluge { get; set; }

        //Deserijalizacija
        private Projekat() {
            TipoviNamestaja = TipNamestajaDataProvider.Instance.GetAll();
            Namestaji = NamestajDataProvider.Instance.GetAll();
            Akcije = AkcijaDataProvider.Instance.GetAll();
            Prodaje = ProdajaDataProvider.Instance.GetAll();
            Korisnici = KorisnikDataProvider.Instance.GetAll();
            Saloni = new ObservableCollection<Salon>();
            Saloni.Add((Salon)SalonDataProvider.Instance.GetByID(1));
            DodatneUsluge = DodatnaUslugaDataProvider.Instance.GetAll();
        }    
        
        //Serijalizacija
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
        public void SetDodatneUsluge(ObservableCollection<DodatnaUsluga> dodatneUsluge) {
            GenericSerializer.Serialize<DodatnaUsluga>("dodatne_usluge.xml", dodatneUsluge);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }
    }
}
