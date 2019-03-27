using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja
{
    class PanelITF_db
    {
        public static void PoprawITFKategoria(itf_kategorie poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFKategoria(itf_kategorie poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_kategorie.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFKategoria()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_kategorie.Max(p => p.id) + 1;
                return newId;
            }
        }
    }
}
