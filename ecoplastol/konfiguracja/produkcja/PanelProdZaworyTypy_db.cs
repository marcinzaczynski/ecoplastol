using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdZaworyTypy_db
    {
        public static List<wyroby_zast_zaworu> PobierzWyrobyZaworyTypy()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zast_zaworu
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }
        public static List<wyroby_zast_zaworu> PobierzZastZaworu()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zast_zaworu
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }
        public static void UsunWyrobZaworTyp(wyroby_zast_zaworu poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zast_zaworu.Attach(poz);
                db.wyroby_zast_zaworu.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyZaworTyp()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_zast_zaworu.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobZaworTyp(wyroby_zast_zaworu poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zast_zaworu.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobZaworTyp(wyroby_zast_zaworu poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
