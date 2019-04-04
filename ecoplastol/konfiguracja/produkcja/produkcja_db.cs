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
        /// WYROBY
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



    }
}
