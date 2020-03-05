using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.produkcja
{
    class WyrobyView : wyroby
    {
        public string RodzajDrutu { get; set; }
        public string CzasChlodzenia1 { get; set; }
        public string CzasChlodzenia2 { get; set; }
    }

    class PanelProdWyroby_db
    {
        public static List<WyrobyView> PobierzWyrobyView(bool aktywne, int typWyrobu)
        {
            List<WyrobyView> ListaWyrobyView = new List<WyrobyView>();
            using (var db = new ecoplastolEntities())
            {
                // wyszukuję wszystkie żeby potem odfiltrować typ_wyrobu i aktywne
                var list = (from w in db.wyroby
                            from d in db.wyroby_druty
                            from cc1 in db.itf_cc
                            from cc2 in db.itf_cc
                            where w.wyrob_rodzaj_drutu == d.id &&
                                  w.itf_cc1 == cc1.id &&
                                  w.itf_cc2 == cc2.id
                            orderby w.id
                            select new WyrobyView
                            {
                                id = w.id,
                                wyrob_kod = w.wyrob_kod,
                                wyrob_kod_indeks = w.wyrob_kod_indeks,
                                wyrob_kod_opis = w.wyrob_kod_opis,
                                wyrob_typ = w.wyrob_typ,
                                wyrob_il_w_op_zb = w.wyrob_il_w_op_zb,
                                wyrob_waga_op = w.wyrob_waga_op,
                                wyrob_waga_1szt = w.wyrob_waga_1szt,
                                wyrob_zakres_sdr = w.wyrob_zakres_sdr,
                                wyrob_zast = w.wyrob_zast,
                                wyrob_rodzaj_drutu = w.wyrob_rodzaj_drutu,
                                wyrob_norma = w.wyrob_norma,
                                wyrob_il_w_partii = w.wyrob_il_w_partii,
                                itf_kategoria = w.itf_kategoria,
                                itf_znak1 = w.itf_znak1,
                                itf_znak2 = w.itf_znak2 ,
                                itf_icc = w.itf_icc,
                                itf_cc1 = w.itf_cc1,
                                itf_cc2 = w.itf_cc2,
                                itf_smin = w.itf_smin,
                                itf_smax = w.itf_smax,
                                itf_trn = w.itf_trn,
                                itf_prn = w.itf_prn,
                                itf_rez = w.itf_rez,
                                itf_odch = w.itf_odch,
                                itf_cz1 = w.itf_cz1,
                                itf_cz2 = w.itf_cz2,
                                itf_ke = w.itf_ke,
                                trace_znak1 = w.trace_znak1, 
                                trace_znak2 = w.trace_znak2,
                                trace_kategoria = w.trace_kategoria,
                                trace_smin = w.trace_smin,
                                trace_smax = w.trace_smax,
                                trace_partia = w.trace_partia, 
                                trace_producent = w.trace_producent,
                                trace_sdr = w.trace_sdr,
                                trace_pe_m = w.trace_pe_m, 
                                trace_material = w.trace_material,
                                trace_pe_o = w.trace_pe_o,
                                trace_mfr = w.trace_mfr,
                                aktywny = w.aktywny,
                                opw = w.opw,
                                czasw = w.czasw, 
                                opm = w.opm,
                                czasm = w.czasm,
                                RodzajDrutu = d.wartosc,
                                CzasChlodzenia1 = cc1.wartosc,
                                CzasChlodzenia2 = cc2.wartosc
                            }
                            );
                switch (typWyrobu)
                {
                    case (-1): // wszystkie
                        switch (aktywne)
                        {
                            // wyświetl aktywne
                            case (true):
                                ListaWyrobyView = list.Where(l => l.aktywny == true).OrderBy(l => l.wyrob_kod).ThenBy(l => l.wyrob_kod_indeks).ToList();
                                break;
                            // wyświetl wszystkie
                            case (false):
                                ListaWyrobyView = list.OrderBy(l => l.wyrob_kod).ThenBy(l => l.wyrob_kod_indeks).ToList();
                                break;
                            // defaultowo wyświetl aktywne
                            default:
                                ListaWyrobyView = list.Where(l => l.aktywny == true).OrderBy(l => l.wyrob_kod).ThenBy(l => l.wyrob_kod_indeks).ToList();
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
                                ListaWyrobyView = list.Where(l => l.aktywny == true && l.wyrob_typ == typWyrobu).OrderBy(l => l.wyrob_kod).ThenBy(l => l.wyrob_kod_indeks).ToList();
                                break;
                            // wyświetl wszystkie
                            case (false):
                                ListaWyrobyView = list.Where(l => l.wyrob_typ == typWyrobu).OrderBy(l => l.wyrob_kod).ThenBy(l => l.wyrob_kod_indeks).ToList();
                                break;
                            // defaultowo wyświetl aktywne
                            default:
                                ListaWyrobyView = list.Where(l => l.aktywny == true && l.wyrob_typ == typWyrobu).OrderBy(l => l.wyrob_kod).ThenBy(l => l.wyrob_kod_indeks).ToList();
                                break;
                        }
                        break;
                }
            };
            
            return ListaWyrobyView;
        }

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
