using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdWyrobyZakresSDR_db
    {
        public static List<wyroby_zakres_sdr> PobierzWyrobyZakresySDR()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zakres_sdr
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }
        public static string PobierzWyrobZakresSdrWartosc(int id)
        {
            using (var db = new ecoplastolEntities())
            {
                var wartosc = (from w in db.wyroby_zakres_sdr
                               where w.id == id
                               select w.wartosc).FirstOrDefault().ToString();
                return wartosc;
            }
        }
        public static void UsunWyrobZakresSDR(wyroby_zakres_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zakres_sdr.Attach(poz);
                db.wyroby_zakres_sdr.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyZakresSDR()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_zakres_sdr.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobZakresSDR(wyroby_zakres_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zakres_sdr.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobZakresSDR(wyroby_zakres_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
