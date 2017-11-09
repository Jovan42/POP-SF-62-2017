using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model.Interface {
    interface IntefaceModel {
        Object getById();
        bool DeleteById(int id);
        void Add(Object o);
        bool ChangeById(Object o, int id);
        void Initialize();
    }
}
