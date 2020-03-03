using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdBrygadzisci_db
    {

        public static List<brygadzisci> PobierzBrygadzistow(int odId)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.brygadzisci
                            where w.id >= odId
                            orderby w.nazwisko ascending
                            select w).ToList();
                return list;
            }
        }
        public static void UsunBrygadziste(brygadzisci poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.brygadzisci.Attach(poz);
                db.brygadzisci.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdBrygadzisty()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.brygadzisci.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajBrygadziste(brygadzisci poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.brygadzisci.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawBrygadziste(brygadzisci poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
