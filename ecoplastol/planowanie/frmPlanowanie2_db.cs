using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmPlanowanie2_db
    {
        public static List<maszyny> PobierzMaszyny()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.maszyny
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<zlecenia_produkcyjne> PobierzZleceniaDlaMaszyny(int nr_maszyny, DateTime? data_zlecenia)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.zlecenia_produkcyjne
                            where w.zlecenie_nr_maszyny == nr_maszyny && w.zlecenie_data_rozp <= data_zlecenia && w.zlecenie_data_zak >= data_zlecenia
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }


    }
}
