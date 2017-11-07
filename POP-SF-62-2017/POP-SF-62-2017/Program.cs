using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017 {
    class Program {
        static void Main(string[] args) {

            /*List<Akcija> akcije = new List<Akcija>();
            akcije.Add(new Akcija {
                ID = 1,
                Popust = 20,
                Pocetak = new DateTime(2017, 10, 10),
                Kraj = new DateTime(2020, 10, 10),
                NamestajNaAkcijiID = null,
                Obrisan = false
             });
             Projekat.Instance.Akcije = akcije;*/
            /*
           (new Akcija {
               ID = 2,
               Popust = 20,
               Pocetak = new DateTime(2017, 10, 10),
               Kraj = new DateTime(2020, 10, 10),
               NamestajNaAkcijiID = null,
               Obrisan = false
           }).Add();*/

            Akcija a = new Akcija() {
                Kraj = new DateTime(2500, 5, 5),
                Pocetak = new DateTime(2100, 5, 5),
                NamestajNaAkcijiID = null,
                Popust = 20,
            };
            //Util.UtilAkcija.ChangeByID(a, 3);


            Namestaj namestaj = new Namestaj();
            namestaj = namestaj.GetById(5);
            if(namestaj != null)
                Console.WriteLine(namestaj.Naziv);

            
            /*var akcija = new List<Akcija>();
            Projekat.Instance.Akcije = akcija;
            foreach (var popust in akcija) {
                Console.WriteLine($"{popust.NamestajNaAkciji}");
            }*/
            Console.ReadLine();
        }
    }
}
