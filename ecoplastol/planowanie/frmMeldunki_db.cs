using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.planowanie
{
    public class MeldunekView : meldunki
    {
        // pola z tabeli meldunki
        //public int id { get; set; }
        //public int id_zlecenie { get; set; }
        //public int id_operator { get; set; }
        //public int id_brygadzista { get; set; }
        //public int zmiana { get; set; }
        //public DateTime data_meldunku { get; set; }
        //public int ilosc { get; set; }
        //public int ilosc_techn { get; set; }
        //public TimeSpan godz_spr_wtr { get; set; }
        //public int wynik_spr_wtr { get; set; }
        //public int wyglad_zew { get; set; }
        //public int wyglad_grzejnika { get; set; }
        //public string opw { get; set; }
        //public DateTime czasw { get; set; }
        //public string opm { get; set; }
        //public DateTime czasm { get; set; }
        //public int przeglad_codz_masz { get; set; }
        //public string uwagi { get; set; }
        
        // ilość wad nn
        public int ilosc_wad_nn { get; set; }
        
        //dodatkowe pola na potrzeby wyświetlania listy - opisy wartości int z tabeli meldunki
        public string nazwa_operatora { get; set; }
        public string opis_wynik_spr_wtr { get; set; }
        public string opis_wyglad_zew { get; set; }
        public string opis_wyglad_grzejnika { get; set; }

        // kilka dodatkowych żeby fajnie było widać filtrowanie za pomocą górnych comboboxów
        public string nazwa_maszyny { get; set; }
        public int id_maszyny { get; set; }
        public string kod_zlecenia { get; set; }
    }

    public class PrzyczynyBrakowView
    {
        // pola z tabeli meldunki_wady_nn
        public int id { get; set; }
        public int id_meldunek { get; set; }
        public int id_zlecenie { get; set; }
        public int id_wada_nn { get; set; }
        public int ilosc { get; set; }
        public string opw { get; set; }
        public DateTime czasw { get; set; }
        public string opm { get; set; }
        public DateTime czasm { get; set; }

        //dodatkowe pola na potrzeby wyświetlania listy - opisy wartości int z tabeli meldunki
        public string nazwa_wady_nn { get; set; }
    }

    public class ZlecenieView
    {
        // pola z tabeli zlecenia_produkcyjne, potrzebne do wyświetlenia listy zleceń w comboboxie na górze
        public int? id { get; set; }
        public string wyrob_kod { get; set; }
        public string wyrob_kod_indeks { get; set; }
        public string wyrob_kod_opis { get; set; }
        public DateTime? zlecenie_data_rozp { get; set; }
        public DateTime? zlecenie_data_zak { get; set; }

        // pola z tabeli maszyny, potrzebne do wyświetlenia listy zleceń w comboboxie na górze
        public string nazwa_maszyny { get; set; }
    }

    class frmMeldunki_db
    {
        public static List<ZlecenieView> PobierzZleceniaDlaMaszynyOdDo(int id_maszyny, DateTime? data_zlecenia_od, DateTime? data_zlecenia_do)
        {
            using (var db = new ecoplastolEntities())
            {
                if (id_maszyny == 0)
                {
                    var list = (from zp in db.zlecenia_produkcyjne
                                from m in db.maszyny
                                where zp.zlecenie_data_rozp <= data_zlecenia_do && zp.zlecenie_data_zak >= data_zlecenia_od && zp.zlecenie_nr_maszyny == m.id
                                orderby zp.id ascending
                                select new ZlecenieView
                                {
                                    id = zp.id, 
                                    wyrob_kod = zp.wyrob_kod,
                                    wyrob_kod_indeks = zp.wyrob_kod_indeks,
                                    wyrob_kod_opis = zp.wyrob_kod_opis,
                                    zlecenie_data_rozp = zp.zlecenie_data_rozp,
                                    zlecenie_data_zak = zp.zlecenie_data_zak,

                                    // pola z tabeli maszyny, potrzebne do wyświetlenia listy zleceń w comboboxie na górze
                                    nazwa_maszyny =  m.nazwa
                                }
                                ).ToList();
                    return list;
                }
                else
                {
                    var list = (from zp in db.zlecenia_produkcyjne
                                from m in db.maszyny
                                where zp.zlecenie_nr_maszyny == id_maszyny && zp.zlecenie_data_rozp <= data_zlecenia_do && zp.zlecenie_data_zak >= data_zlecenia_od && zp.zlecenie_nr_maszyny == m.id
                                orderby zp.id ascending
                                select new ZlecenieView
                                {
                                    id = zp.id,
                                    wyrob_kod = zp.wyrob_kod,
                                    wyrob_kod_indeks = zp.wyrob_kod_indeks,
                                    wyrob_kod_opis = zp.wyrob_kod_opis,
                                    zlecenie_data_rozp = zp.zlecenie_data_rozp,
                                    zlecenie_data_zak = zp.zlecenie_data_zak,

                                    // pola z tabeli maszyny, potrzebne do wyświetlenia listy zleceń w comboboxie na górze
                                    nazwa_maszyny = m.nazwa
                                }
                                ).ToList();
                    return list;
                }
            }
        }

        public static List<meldunki> PobierzMeldunki()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki
                                //where
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<meldunki_wynik> PobierzWynikiDlaMeldunki()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki_wynik
                                //where
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<meldunki_wynik_prz_maszyny> PobierzWynikiPrzMasz()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki_wynik_prz_maszyny
                                //where
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<MeldunekView> PobierzMeldunki2(DateTime dataOd, DateTime dataDo, int idMaszyny, int idZlecenia, int idZmiany, int idOperatora)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from m in db.meldunki
                            from mw1 in db.meldunki_wynik
                            from mw2 in db.meldunki_wynik
                            from mw3 in db.meldunki_wynik
                            from o in db.operatorzy_maszyn
                            from maszyny in db.maszyny
                            from zlecenia in db.zlecenia_produkcyjne
                            where
                                    //m.id_zlecenie == nrZlecenia &&
                                    //(nrZlecenia > 0) ? m.id_zlecenie == nrZlecenia : m.id_zlecenie.ToString().Contains("*") &&
                                    m.data_meldunku >= dataOd &&
                                    m.data_meldunku <= dataDo &&
                                    mw1.id == m.wynik_spr_wtr &&
                                    mw2.id == m.wyglad_zew &&
                                    mw3.id == m.wyglad_grzejnika &&
                                    o.id == m.id_operator &&
                                    m.id_zlecenie == zlecenia.id &&
                                    zlecenia.zlecenie_nr_maszyny == maszyny.id
                            orderby m.data_meldunku descending, m.id_zmiana ascending
                            select new MeldunekView
                            {
                                // pola z tabeli meldunki
                                id = m.id,
                                id_zlecenie = m.id_zlecenie,
                                id_operator = m.id_operator,
                                id_brygadzista = m.id_brygadzista,
                                id_zmiana = m.id_zmiana,
                                data_meldunku = m.data_meldunku,
                                ilosc = m.ilosc,
                                ilosc_techn = m.ilosc_techn,
                                //id_wady_nn = from s in (from wnn in db.meldunki_wady_nn where wnn.id_meldunek == m.id select wnn.ilosc).ToList()
                                //             where s.
                                //               ?
                                //               ((from wnn in db.meldunki_wady_nn
                                //                 where (wnn.id_meldunek == m.id)
                                //                 select wnn.ilosc).ToList()).Sum()
                                //               :
                                //               0,
                                godz_spr_wtr = m.godz_spr_wtr,
                                wynik_spr_wtr = m.wynik_spr_wtr,
                                wyglad_zew = m.wyglad_zew,
                                wyglad_grzejnika = m.wyglad_grzejnika,
                                zatwierdzony = m.zatwierdzony,
                                opw = m.opw,
                                czasw = m.czasw,
                                opm = m.opm,
                                czasm = m.czasm,
                                przeglad_codz_masz = m.przeglad_codz_masz,
                                uwagi = m.uwagi,
                                // ilosc wad nn
                                ilosc_wad_nn = (
                                             ((from wnn in db.meldunki_wady_nn
                                               where (wnn.id_meldunek == m.id)
                                               select wnn.ilosc).ToList()).Count > 0)
                                               ?
                                               ((from wnn in db.meldunki_wady_nn
                                                 where (wnn.id_meldunek == m.id)
                                                 select wnn.ilosc).ToList()).Sum()
                                               :
                                               0,
                                //dodatkowe pola na potrzeby wyświetlania listy - opisy wartości int z tabeli meldunki
                                nazwa_operatora = o.nazwisko + " " + o.imie,
                                opis_wynik_spr_wtr = mw1.wynik,
                                opis_wyglad_zew = mw2.wynik,
                                opis_wyglad_grzejnika = mw3.wynik,

                                // kilka dodatkowych żeby fajnie było widać filtrowanie za pomocą górnych comboboxów
                                nazwa_maszyny = maszyny.nazwa,
                                 id_maszyny = maszyny.id,
                                kod_zlecenia = zlecenia.wyrob_kod
                            });

                if (idMaszyny > 0)
                {
                    list = list.Where(r => r.id_maszyny == idMaszyny);
                }

                if (idZlecenia > 0)
                {
                    list = list.Where(r => r.id_zlecenie == idZlecenia);
                }

                if (idZmiany > 0)
                {
                    list = list.Where(r => r.id_zmiana == idZmiany);
                }

                if (idOperatora > 0)
                {
                    list = list.Where(r => r.id_operator == idOperatora);
                }
                return list.ToList();
            }
        }

        private static int SumaWad(int id)
        {
            //List<int> intList = new List<int>();

            //var suma = intList.Sum();
            //return suma;

            return id;
        }

        public static int IdMeldunki()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.meldunki.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static void DodajMeldunek(meldunki poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.meldunki.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawMeldunek(meldunki poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UsunMeldunek(MeldunekView poz)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki
                            where w.id == poz.id
                            select w).ToList();
                foreach (var detail in list)
                {
                    db.meldunki.Remove(detail);
                }
                db.SaveChanges();
            }
        }

        public static List<PrzyczynyBrakowView> PobierzPrzyczynyBrakow(int nrMeldunku)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from mw in db.meldunki_wady_nn
                            from wnn in db.wady_nn
                            where
                                (mw.id_meldunek == nrMeldunku || mw.id_meldunek == 0) &&
                                mw.id_wada_nn == wnn.id
                            orderby mw.id ascending
                            select new PrzyczynyBrakowView
                            {
                                // pola z tabeli meldunki_wady_nn
                                id = mw.id,
                                id_meldunek = mw.id_meldunek,
                                id_zlecenie = mw.id_zlecenie,
                                id_wada_nn = mw.id_wada_nn,
                                ilosc = mw.ilosc,
                                opw = mw.opw,
                                czasw = mw.czasw,
                                opm = mw.opm,
                                czasm = mw.czasm,
                                //dodatkowe pola na potrzeby wyświetlania listy - opisy wartości int z tabeli meldunki
                                nazwa_wady_nn = wnn.wartosc
                            }).ToList();
                return list;
            }
        }

        public static List<wady_nn> PobierzWadyNN()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wady_nn
                            where w.parametr > 0
                            orderby w.parametr ascending
                            select w).ToList();
                return list;
            }
        }

        public static void DodajPrzyczyneBraku(meldunki_wady_nn poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.meldunki_wady_nn.Add(poz);
                db.SaveChanges();
            }
        }

        public static void PoprawPrzyczyneBraku(meldunki_wady_nn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static int IdPrzyczynyBraku()
        {
            using (var db = new ecoplastolEntities())
            {
                int? newId = db.meldunki_wady_nn.Max(p => (int?)p.id);
                if (newId.HasValue)
                    return newId.Value + 1;
                else
                    return 1;
            }
        }

        public static void UsunPrzyczynyBrakow()
        {
            // przy dodawaniu przyczyn braków, zawsze jako id_meldunku podaje 0
            // przy zatwierdzaniu poprwiam na prawidłowy numer meldunku a przy anulowaniu usuwam te z 0
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki_wady_nn
                            where w.id_meldunek == 0
                            select w).ToList();
                foreach (var detail in list)
                {
                    db.meldunki_wady_nn.Remove(detail);
                }
                db.SaveChanges();
            }
        }

        public static void UsunPrzyczynyBrakow(int idMeldunku)
        {
            // przy dodawaniu przyczyn braków, zawdze jako id_meldunku podaje 0
            // przy zatwierdzaniu poprwiam na prawidłowy numer meldunku a przy anulowaniu usuwam te z 0
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki_wady_nn
                            where w.id_meldunek == idMeldunku
                            select w).ToList();
                foreach (var detail in list)
                {
                    db.meldunki_wady_nn.Remove(detail);
                }
                db.SaveChanges();
            }
        }

        public static void UsunPrzyczyneBrakow(PrzyczynyBrakowView poz)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.meldunki_wady_nn
                            where w.id == poz.id
                            select w).ToList();
                foreach (var detail in list)
                {
                    db.meldunki_wady_nn.Remove(detail);
                }
                db.SaveChanges();
            }
        }

        public static void PoprawIDPrzyczynyBrakow(int idMeldunku, int idZlecenia)
        {
            // przy dodawaniu przyczyn braków, zawdze jako id_meldunku podaje 0
            // przy zatwierdzaniu poprwiam na prawidłowy numer meldunku a przy anulowaniu usuwam te z 0
            using (var db = new ecoplastolEntities())
            {
                (from w in db.meldunki_wady_nn
                 where w.id_meldunek == 0
                 select w).ToList().ForEach(x => x.id_meldunek = idMeldunku);

                db.SaveChanges();

                (from w in db.meldunki_wady_nn
                 where w.id_zlecenie == 0
                 select w).ToList().ForEach(x => x.id_zlecenie = idZlecenia);

                db.SaveChanges();
            }
        }
    }
}
