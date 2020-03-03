using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class PanelProdWyroby_db
    {
        public static List<wyroby> PobierzWyroby(bool aktywne, int typWyrobu)
        {
            using (var db = new ecoplastolEntities())
            {
                List<wyroby> list = new List<wyroby>();

                switch (typWyrobu)
                {
                    // wszystkie
                    case (-1):
                        switch (aktywne)
                        {
                            // wyświetl aktywne
                            case (true):
                                list = (from wyr in db.wyroby
                                        where wyr.aktywny == aktywne
                                        orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                        select wyr).ToList();
                                break;

                            // wyświetl wszystkie
                            case (false):
                                list = (from wyr in db.wyroby
                                        orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                        select wyr).ToList();
                                break;

                            // defaultowo wyświetl aktywne
                            default:
                                list = (from wyr in db.wyroby
                                        where wyr.aktywny == aktywne
                                        orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                        select wyr).ToList();
                                break;
                        }

                        break;


                    case (0): // elektrooporowe
                    case (1): // doczołowe
                    case (2): // zawory
                    case (3): // adaptery
                        switch (aktywne)
                        {
                            // wyświetl aktywne
                            case (true):
                                list = (from wyr in db.wyroby
                                        where wyr.aktywny == aktywne && wyr.wyrob_typ == typWyrobu
                                        orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                        select wyr).ToList();
                                break;

                            // wyświetl wszystkie
                            case (false):
                                list = (from wyr in db.wyroby
                                        where wyr.wyrob_typ == typWyrobu
                                        orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                        select wyr).ToList();
                                break;

                            // defaultowo wyświetl aktywne
                            default:
                                list = (from wyr in db.wyroby
                                        where wyr.aktywny == aktywne
                                        orderby wyr.wyrob_kod ascending, wyr.wyrob_kod_indeks ascending
                                        select wyr).ToList();
                                break;
                        }
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
    }
}
