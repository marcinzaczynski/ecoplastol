using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdWyrobyTypy_db
    {
        public static List<wyroby_typ> PobierzTypy()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_typ
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<wyroby_typ> PobierzWyrobyTypy()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_typ
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunWyrobTyp(wyroby_typ poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_typ.Attach(poz);
                db.wyroby_typ.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyTyp()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_typ.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobTyp(wyroby_typ poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.wyroby_typ.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobTyp(wyroby_typ poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }


}
