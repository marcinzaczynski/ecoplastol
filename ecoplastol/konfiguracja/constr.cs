using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja
{
    class constr
    {
        public string adres { get; set; }
        public string db { get; set; }
        public string user { get; set; }
        public string pass { get; set; }

        public constr()
        {
            adres = "localhost";
            db = "ecoplastol";
            user = "postgres";
            pass = "postgres";
        }
    }
}
