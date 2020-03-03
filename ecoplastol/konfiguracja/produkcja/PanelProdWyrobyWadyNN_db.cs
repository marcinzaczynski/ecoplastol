using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdWyrobyWadyNN_db
    {

        public static List<wady_nn> PobierzWyrobyWadyNN()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wady_nn
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunWyrobWadaNN(wady_nn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wady_nn.Attach(poz);
                db.wady_nn.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyWadyNN()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wady_nn.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobWadaNN(wady_nn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wady_nn.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobWadaNN(wady_nn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
