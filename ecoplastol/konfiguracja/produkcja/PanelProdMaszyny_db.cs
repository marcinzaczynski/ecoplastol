using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdMaszyny_db
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

        public static void UsunMaszyne(maszyny poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.maszyny.Attach(poz);
                db.maszyny.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdMaszyny()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.maszyny.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajMaszyne(maszyny poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.maszyny.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawMaszyne(maszyny poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static string PobierzNazweMaszyny(int nrMaszyny)
        {
            using (var db = new ecoplastolEntities())
            {
                var nazwa = (from w in db.maszyny
                             where w.id == nrMaszyny
                             select w.nazwa).FirstOrDefault().ToString();
                return nazwa;
            }
        }
    }
}
