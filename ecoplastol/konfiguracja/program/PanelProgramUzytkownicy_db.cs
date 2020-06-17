using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.program
{
    class UzytkownicyView : uzytkownicy
    {
        public string opis_profilu { get; set; }
    }
    class PanelProgramUzytkownicy_db
    {
        public static List<UzytkownicyView> PobierzUzytkownikowView()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from u in db.uzytkownicy
                            from p in db.uzytkownicy_profile
                            where u.profil == p.profil && u.id > 0
                            orderby u.id ascending
                            select new UzytkownicyView
                            {
                                // pola z tabeli meldunki
                                id = u.id,
                                imie = u.imie,
                                nazwisko = u.nazwisko,
                                login = u.login,
                                haslo = u.haslo,
                                aktywny = u.aktywny,
                                profil = u.profil,
                                opw = u.opw,
                                czasw = u.czasw,
                                opm = u.opm,
                                czasm = u.czasm,

                                //profile
                                opis_profilu = p.opis
                            }).ToList();
                return list;
            }
        }
        public static int IdUzytkownika()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.uzytkownicy.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void DodajUzytkownika(uzytkownicy poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.uzytkownicy.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawUzytkownika(uzytkownicy poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UsunUzytkownika(UzytkownicyView poz)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.uzytkownicy
                            where w.id == poz.id
                            select w).ToList();
                foreach (var detail in list)
                {
                    db.uzytkownicy.Remove(detail);
                }
                db.SaveChanges();

            }
        }
    }
}
