using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.planowanie
{
    public class MeldunekView
    {
        //pola z tabeli meldunki
        public int id { get; set; }
        public int id_zlecenie { get; set; }
        public int zmiana { get; set; }
        public DateTime data_meldunku { get; set; }
        public int ilosc { get; set; }
        public int ilosc_n { get; set; }
        public int ilosc_nn { get; set; }

        public string NazwaMaszyny { get; set; }

    }

    class frmMeldunki_db
    {
        public static List<zlecenia_produkcyjne> PobierzZleceniaDlaMaszynyOdDo(int nr_maszyny, DateTime? data_zlecenia_od, DateTime? data_zlecenia_do)
        {
            using (var db = new ecoplastolEntities())
            {
                if (nr_maszyny == 0)
                {
                    var list = (from w in db.zlecenia_produkcyjne
                                where w.zlecenie_data_rozp <= data_zlecenia_do && w.zlecenie_data_zak >= data_zlecenia_od
                                orderby w.id ascending
                                select w).ToList();
                    return list;
                }
                else
                {
                    var list = (from w in db.zlecenia_produkcyjne
                                where w.zlecenie_nr_maszyny == nr_maszyny && w.zlecenie_data_rozp <= data_zlecenia_do && w.zlecenie_data_zak >= data_zlecenia_od
                                orderby w.id ascending
                                select w).ToList();
                    return list;
                }
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
    }
}
