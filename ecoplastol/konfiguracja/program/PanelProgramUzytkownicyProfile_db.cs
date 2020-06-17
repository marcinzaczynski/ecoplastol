using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.program
{
    class PanelProgramUzytkownicyProfile_db
    {
        public static List<uzytkownicy_profile> PobierzProfile()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.uzytkownicy_profile
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static int IdProfilu()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.uzytkownicy_profile.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void UsunProfil(uzytkownicy_profile poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.uzytkownicy_profile.Attach(poz);
                db.uzytkownicy_profile.Remove(poz);
                db.SaveChanges();
            }
        }

        public static void DodajProfil(uzytkownicy_profile poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.uzytkownicy_profile.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawProfil(uzytkownicy_profile poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
