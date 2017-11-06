using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017 {
    class Program {
        static void Main(string[] args) {

            /* List<Namestaj> namestaji = new List<Namestaj>();
             namestaji.Add(new Namestaj {
                 ID = 1,
                 Naziv = "Naziv",
                 Cena = 100,
                 Kolicina = 10,
                 TipNamestajaID = 1
             });
             Projekat.Instance.Namestaj = namestaji;*/

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
