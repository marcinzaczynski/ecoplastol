using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class konf_produkcja_db
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
                             select w.nazwa).FirstOrDefault().ToString();
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

        public static string PobierzNazweZmiany(int nrZmiany)
        {
            using (var db = new ecoplastolEntities())
            {
                var nazwa = (from w in db.zmiany
                             where w.id == nrZmiany
                             select w.nazwa).FirstOrDefault().ToString();
                return nazwa;
            }
        }

        /// <summary>
        /// WYROBY
        /// </summary>

        public static List<wyroby> PobierzWyroby(bool aktywne)
        {
            using (var db = new ecoplastolEntities())
            {
                List<wyroby> list = new List<wyroby>();
                switch (aktywne)
                {
                    case (true):
                        list = (from wyr in db.wyroby
                                    where wyr.aktywny == aktywne
                                    orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                    select wyr).ToList();
                        break;
                    case (false):
                        list = (from wyr in db.wyroby
                                orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                select wyr).ToList();
                        break;
                    default:
                        list = (from wyr in db.wyroby
                                    where wyr.aktywny == aktywne
                                    orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                    select wyr).ToList();
                        break;

                }


                
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
                            orderby w.nazwisko ascending
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

        /// <summary>
        /// WADY NN
        /// </summary>

        public static List<wady_nn> PobierzWadyNN()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wady_nn
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        /// <summary>
        /// WYROBY - TYPY
        /// </summary>
        
        public static List<wyroby_typ> PobierzWyrobyTypy()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_typ
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunWyrobTyp(wyroby_typ poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_typ.Attach(poz);
                db.wyroby_typ.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyTyp()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_typ.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobTyp(wyroby_typ poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.wyroby_typ.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobTyp(wyroby_typ poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// WYROBY - ZAWORY - TYPY
        /// </summary>

        public static List<wyroby_zast_zaworu> PobierzWyrobyZaworyTypy()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zast_zaworu
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunWyrobZaworTyp(wyroby_zast_zaworu poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zast_zaworu.Attach(poz);
                db.wyroby_zast_zaworu.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyZaworTyp()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_zast_zaworu.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobZaworTyp(wyroby_zast_zaworu poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zast_zaworu.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobZaworTyp(wyroby_zast_zaworu poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// WYROBY - ZAKRESY SDR
        /// </summary>

        public static List<wyroby_zakres_sdr> PobierzWyrobyZakresySDR()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zakres_sdr
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunWyrobZakresSDR(wyroby_zakres_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zakres_sdr.Attach(poz);
                db.wyroby_zakres_sdr.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyZakresSDR()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_zakres_sdr.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobZakresSDR(wyroby_zakres_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_zakres_sdr.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobZakresSDR(wyroby_zakres_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// WYROBY - DRUTY
        /// </summary>

        public static List<wyroby_druty> PobierzWyrobyDruty()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_druty
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void UsunWyrobDrut(wyroby_druty poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_druty.Attach(poz);
                db.wyroby_druty.Remove(poz);
                db.SaveChanges();
            }
        }

        public static int IdWyrobyDruty()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.wyroby_druty.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajWyrobDrut(wyroby_druty poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.wyroby_druty.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawWyrobDrut(wyroby_druty poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// WYROBY - WADY NN
        /// </summary>

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
