using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.produkcja
{
    public class ZleceniaView
    {
        // pola z tabeli meldunki 
        public int id { get; set; }
        public int wyrob_kod_id { get; set; }
        public string wyrob_kod { get; set; }
        public string wyrob_kod_indeks { get; set; }
        public string wyrob_kod_opis { get; set; }
        public int wyrob_typ { get; set; }
        public int wyrob_il_w_op_zb { get; set; }
        public int wyrob_waga_op { get; set; }
        public int wyrob_waga_1szt { get; set; }
        public int wyrob_zakres_sdr { get; set; }
        public int? wyrob_zast_zaworu { get; set; }
        public int? wyrob_rodzaj_drutu { get; set; }
        public int wyrob_norma { get; set; }
        public int wyrob_il_w_partii { get; set; }
        public int zlecenie_nr_maszyny { get; set; }
        public int zlecenie_ilosc { get; set; }
        public DateTime zlecenie_data_rozp { get; set; }
        public DateTime zlecenie_data_zak { get; set; }
        public string zlecenie_nr_partii_surowca { get; set; }
        public string zlecenie_nr_partii_drutu { get; set; }
        public int itf_kategoria { get; set; }
        public int itf_znak1 { get; set; }
        public int itf_znak2 { get; set; }
        public int itf_icc { get; set; }
        public int itf_cc1 { get; set; }
        public int itf_cc2 { get; set; }
        public int itf_smin { get; set; }
        public int itf_smax { get; set; }
        public int itf_trn { get; set; }
        public string itf_prn { get; set; }
        public string itf_rez { get; set; }
        public int itf_odch { get; set; }
        public string itf_cz1 { get; set; }
        public string itf_cz2 { get; set; }
        public string itf_ke { get; set; }
        public int trace_znak1 { get; set; }
        public int trace_znak2 { get; set; }
        public int trace_kategoria { get; set; }
        public int trace_smin { get; set; }
        public int trace_smax { get; set; }
        public string trace_partia { get; set; }
        public int trace_producent { get; set; }
        public int trace_sdr { get; set; }
        public int trace_pe_m { get; set; }
        public int trace_material { get; set; }
        public int trace_pe_o { get; set; }
        public int trace_mfr { get; set; }
        public string opw { get; set; }
        public DateTime czasw { get; set; }
        public string opm { get; set; }
        public DateTime czasm { get; set; }

    }


    class produkcja_db
    {
        public static List<ZleceniaView> PobierzZlecenia(DateTime dataOd, DateTime dataDo, int idMaszyny)
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from z in db.zlecenia_produkcyjne
                            where
                                    //m.id_zlecenie == nrZlecenia &&
                                    //(nrZlecenia > 0) ? m.id_zlecenie == nrZlecenia : m.id_zlecenie.ToString().Contains("*") &&
                                    z.zlecenie_data_rozp >= dataOd &&
                                    z.zlecenie_data_rozp <= dataDo
                            orderby z.zlecenie_data_rozp descending
                            select new ZleceniaView
                            {
                                // pola z tabeli zlecenia produkcyjne
                                id = z.id,
                                wyrob_kod_id = z.wyrob_kod_id,
                                wyrob_kod = z.wyrob_kod,
                                wyrob_kod_indeks = z.wyrob_kod_indeks,
                                wyrob_kod_opis = z.wyrob_kod_opis,
                                wyrob_typ = z.wyrob_typ,
                                wyrob_il_w_op_zb = z.wyrob_il_w_op_zb,
                                wyrob_waga_op = z.wyrob_waga_op,
                                wyrob_waga_1szt = z.wyrob_waga_1szt,
                                wyrob_zakres_sdr = z.wyrob_zakres_sdr,
                                wyrob_zast_zaworu = z.wyrob_zast_zaworu,
                                wyrob_rodzaj_drutu = z.wyrob_rodzaj_drutu,
                                wyrob_norma = z.wyrob_norma,
                                wyrob_il_w_partii = z.wyrob_il_w_partii,
                                zlecenie_nr_maszyny = z.zlecenie_nr_maszyny,
                                zlecenie_ilosc = z.zlecenie_ilosc,
                                zlecenie_data_rozp = z.zlecenie_data_rozp,
                                zlecenie_data_zak = z.zlecenie_data_zak,
                                zlecenie_nr_partii_surowca = z.zlecenie_nr_partii_surowca,
                                zlecenie_nr_partii_drutu = z.zlecenie_nr_partii_drutu,
                                itf_kategoria = z.itf_kategoria,
                                itf_znak1 = z.itf_znak1,
                                itf_znak2 = z.itf_znak2,
                                itf_icc = z.itf_icc,
                                itf_cc1 = z.itf_cc1,
                                itf_cc2 = z.itf_cc2,
                                itf_smin = z.itf_smin,
                                itf_smax = z.itf_smax,
                                itf_trn = z.itf_trn,
                                itf_prn = z.itf_prn,
                                itf_rez = z.itf_rez,
                                itf_odch = z.itf_odch,
                                itf_cz1 = z.itf_cz1,
                                itf_cz2 = z.itf_cz2,
                                itf_ke = z.itf_ke,
                                trace_znak1 = z.trace_znak1,
                                trace_znak2 = z.trace_znak2,
                                trace_kategoria = z.trace_kategoria,
                                trace_smin = z.trace_smin,
                                trace_smax = z.trace_smax,
                                trace_partia = z.trace_partia,
                                trace_producent = z.trace_producent,
                                trace_sdr = z.trace_sdr,
                                trace_pe_m = z.trace_pe_m,
                                trace_material = z.trace_material,
                                trace_pe_o = z.trace_pe_o,
                                trace_mfr = z.trace_mfr,
                                opw = z.opw,
                                czasw = z.czasw,
                                opm = z.opm,
                                czasm = z.czasm 
                            });

                if (idMaszyny > 0)
                {
                    list = list.Where(r => r.zlecenie_nr_maszyny == idMaszyny);
                }

                return list.ToList();
            }
        }
    }
}
