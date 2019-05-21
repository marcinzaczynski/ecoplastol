using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmZlecenieProdukcji_db
    {
        public static int IdZlecenieProdukcji()
        {
            using (var db = new ecoplastolEntities())
            {
                int? newId = db.zlecenia_produkcyjne.Max(p => (int?)p.id);
                if (newId.HasValue)
                    return newId.Value + 1;
                else
                    return 1;
            }
        }

        public static void DodajZlecenie(zlecenia_produkcyjne poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.zlecenia_produkcyjne.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawZlecenie(zlecenia_produkcyjne poz)
        {
            using (var db = new ecoplastolEntities())
            { 
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UsunZlecenie(zlecenia_produkcyjne poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.zlecenia_produkcyjne.Attach(poz);
                db.zlecenia_produkcyjne.Remove(poz);
                db.SaveChanges();
            }
        }

        public static string PobierzKodZlecenia(int nrZlecenia)
        {
            using (var db = new ecoplastolEntities())
            {
                var nazwa = (from w in db.zlecenia_produkcyjne
                             where w.id == nrZlecenia
                             select w.wyrob_kod).FirstOrDefault().ToString();
                return nazwa;
            }
        }
    }
}
