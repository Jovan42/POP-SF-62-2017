using POP_SF_62_2017.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Projekat {
        public static Projekat Instance { get; } = new Projekat();

        private List<Namestaj> namestaji = new List<Namestaj>();
        private List<TipNamestaja> tipoviNamestaja = new List<TipNamestaja>();
        private List<Akcija> akcije = new List<Akcija>();
        private List<Korisnik> korisnici = new List<Korisnik>();
        private List<Prodaja> prodaje = new List<Prodaja>();
        private List<Salon> saloni = new List<Salon>();

        public List<Namestaj> Namestaji {
            get {
                this.namestaji = GenericSerializer.Deserialize<Namestaj>("namestaji.xml");
                return this.namestaji;
            }

            set {
                this.namestaji = value;
                GenericSerializer.Serialize<Namestaj>("namestaji.xml", this.namestaji);
            }
        }

        public List<TipNamestaja> TipoviNamestaja {
            get {
                this.tipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
                return this.tipoviNamestaja;
            }

            set {
                this.tipoviNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tipovi_namestaja.xml", this.tipoviNamestaja);
            }
        }

        public List<Akcija> Akcije {
            get {
                this.akcije = GenericSerializer.Deserialize<Akcija>("akcije.xml");
                return this.akcije;
            }

            set {
                this.akcije = value;
                GenericSerializer.Serialize<Akcija>("akcije.xml", this.akcije);
            }
        }

        public List<Prodaja> Prodaje {
            get {
                this.prodaje = GenericSerializer.Deserialize<Prodaja>("prodaje.xml");
                return this.prodaje;
            }

            set {
                this.prodaje = value;
                GenericSerializer.Serialize<Prodaja>("prodaje.xml", this.prodaje);
            }
        }

        public List<Salon> Saloni {
            get {
                this.saloni = GenericSerializer.Deserialize<Salon>("saloni.xml");
                return this.saloni;
            }

            set {
                this.saloni = value;
                GenericSerializer.Serialize<Salon>("saloni.xml", this.saloni);
            }
        }

        public List<Korisnik> Korisnici {
            get {
                this.korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
                return this.korisnici;
            }

            set {
                this.korisnici = value;
                GenericSerializer.Serialize<Korisnik>("korisnici.xml", this.korisnici);
            }
        }

        
    }
}
