using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmTools_db
    {
        public static List<uzytkownicy> PobierzUzytkownikow()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.uzytkownicy
                                //where
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }


        public static List<meldunki> PobierzMeldunki()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki
                                //where
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<meldunki_wady_nn> PobierzWadyNN()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki_wady_nn
                                //where
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static int PobierzIdZlecenia(int id_meld)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from m in db.meldunki
                            where
                                m.id == id_meld
                            select m.id_zlecenie).FirstOrDefault();

                return Convert.ToInt32(list);
            }
        }


    }
}
