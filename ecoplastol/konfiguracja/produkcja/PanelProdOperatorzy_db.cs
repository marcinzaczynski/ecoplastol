using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class OperatorzyView
    {
        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
        public bool aktywny { get; set; }
        public int brygada  { get; set; }
        public string opw { get; set; }
        public DateTime czasw { get; set; }
        public string opm { get; set; }
        public DateTime czasm { get; set; }

        //brygadziści
        public string nazwa_brygadzisty { get; set; }
    }

    class PanelProdOperatorzy_db
    {
        public static List<operatorzy_maszyn> PobierzOperatorow(int odId)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.operatorzy_maszyn
                            where w.id >= odId
                            orderby w.nazwisko ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<OperatorzyView> PobierzOperatorowView()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from o in db.operatorzy_maszyn
                            from b in db.brygadzisci
                            where o.brygada == b.id && o.id > 0
                            orderby o.id ascending
                            select new OperatorzyView
                            {
                                // pola z tabeli meldunki
                                id = o.id,
                                imie = o.imie,
                                nazwisko = o.nazwisko,
                                login = o.login,
                                haslo = o.haslo,
                                aktywny = o.aktywny,
                                brygada = o.brygada,
                                opw = o.opw,
                                czasw = o.czasw,
                                opm = o.opm,
                                czasm = o.czasm,

                                //brygadziści
                                nazwa_brygadzisty = b.imie + " " + b.nazwisko
                            }).ToList() ;
                return list;
            }
        }

        public static void UsunOperatora(OperatorzyView poz)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.operatorzy_maszyn
                            where w.id == poz.id
                            select w).ToList();
                foreach (var detail in list)
                {
                    db.operatorzy_maszyn.Remove(detail);
                }
                db.SaveChanges();
          
            }
        }

        public static int IdOperatora()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.operatorzy_maszyn.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajOperatora(operatorzy_maszyn poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.operatorzy_maszyn.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawOperatora(operatorzy_maszyn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static string PobierzImieNazwiskoOperatora(int nrOperatora)
        {
            using (var db = new ecoplastolEntities())
            {
                var nazwa = (from w in db.operatorzy_maszyn
                             where w.id == nrOperatora
                             select w.imie + " " + w.nazwisko).FirstOrDefault().ToString();
                return nazwa;
            }
        }
    }
}
