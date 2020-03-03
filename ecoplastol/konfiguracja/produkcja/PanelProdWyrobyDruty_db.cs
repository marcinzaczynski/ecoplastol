using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdWyrobyDruty_db
    {
       

        public static List<wyroby_druty> PobierzWyrobyDruty()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_druty
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunWyrobDrut(wyroby_druty poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_druty.Attach(poz);
                db.wyroby_druty.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyDruty()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_druty.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobDrut(wyroby_druty poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_druty.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobDrut(wyroby_druty poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
