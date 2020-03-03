using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdZmiany_db
    {
        public static List<zmiany> PobierzZmiany()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from wyr in db.zmiany
                            orderby wyr.id ascending
                            select wyr).ToList();
                return list;
            }
        }

        public static string PobierzNazweZmiany(int nrZmiany)
        {
            using (var db = new ecoplastolEntities())
            {
                var nazwa = (from w in db.zmiany
                             where w.id == nrZmiany
                             select w.nazwa).FirstOrDefault().ToString();
                return nazwa;
            }
        }
    }
}
