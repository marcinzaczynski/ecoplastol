using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class produkcja_db
    {
        /// <summary>
        /// MASZYNY
        /// </summary>
        
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
                             select w.numer).FirstOrDefault().ToString();
                return nazwa;
            }
        }

        ///
        /// ZMIANY
        ///

        public static List<zmiany> PobierzZmiany()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from wyr in db.zmiany
                            orderby wyr.id ascending
                            select wyr).ToList();
                return list;
            }
        }

        /// <summary>
        /// WYROBY
        /// </summary>

        public static List<wyroby> PobierzWyroby()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from wyr in db.wyroby
                            orderby wyr.wyrob_kod ascending
                            select wyr).ToList();
                return list;
            }
        }

        public static void DodajWyrob(wyroby poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.wyroby.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrob(wyroby poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UsunWyrob(wyroby poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby.Attach(poz);
                db.wyroby.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyroby()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby.Max(p => p.id) + 1;
                return newId;
            }
        }

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

        public static List<wyroby_zakres_sdr> PobierzZakresSDR()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zakres_sdr
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<wyroby_zast_zaworu> PobierzZastZaworu()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zast_zaworu
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        /// <summary>
        /// DRUTY
        /// </summary>
        public static List<wyroby_druty> PobierzDruty()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_druty
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        /// <summary>
        /// WYRÓB - ZAKRES SDR
        /// </summary>
        public static string PobierzWyrobZakresSdrWartosc(int id)
        {
            using (var db = new ecoplastolEntities())
            {
                var wartosc = (from w in db.wyroby_zakres_sdr
                               where w.id == id
                               select w.wartosc).FirstOrDefault().ToString();
                return wartosc;
            }
        }

        /// <summary>
        /// OPERATORZY
        /// </summary>

        public static List<operatorzy_maszyn> PobierzOperatorow()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.operatorzy_maszyn
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunOperatora(operatorzy_maszyn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.operatorzy_maszyn.Attach(poz);
                db.operatorzy_maszyn.Remove(poz);
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

    }
}
