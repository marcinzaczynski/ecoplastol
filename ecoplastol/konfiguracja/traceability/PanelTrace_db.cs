using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.traceability
{
    class PanelTrace_db
    {
        /// --------------------------------------
        /// LITERY
        /// --------------------------------------
        public static void PoprawTraceLitery(trace_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceLitery(trace_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_litery.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceLitery()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_litery.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceLitery(trace_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_litery.Attach(poz);
                db.trace_litery.Remove(poz);
                db.SaveChanges();
            }
        }
    }
}