using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    interface DataAccess {
        // Vraća sve neobrisane entitete iz XML fajla
        //ObservableCollection<Entitet> GetAll();

        // Vraća entitet iz XML fajla sa odgovarajućim ID-jem
        Entitet GetByID(int id);

        // Dodaje entitet u XML fajl
        void Add(Entitet e);

        // Menja postojeći entitet iz XML fajla
        bool EditByID(Entitet e, int id);

        // Briše (logički) entitet, sa odgovarajućim ID-jem iz XML fajla
        bool DeleteByID(int id);

        // Inicijalizacija XML fajla
        void Initialize();
    }
}
