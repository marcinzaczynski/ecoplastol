using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmWyroby_db
    {
        public static List<wyroby> GetProducts()
        {
            using (var db = new ecoplastolEntities())
            {
                var listWyroby = (from wyr in db.wyroby
                                  orderby wyr.wyrob_kod ascending
                                  select wyr).ToList();
                return listWyroby;
            }
        }

        public static List<parametry> GetParams(string _kategoria, string _grupa)
        {
            using (var db = new ecoplastolEntities())
            {
                var listP = (from par in db.parametry
                             where par.kategoria == _kategoria && par.grupa == _grupa
                             orderby par.indeks ascending
                             select par).ToList();
                return listP;
            }
        }

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

        public static List<wyroby_zast_zaworu> PobierzZastZaworu()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zast_zaworu
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<wyroby_zakres_sdr> PobierzZakresSDR()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_zakres_sdr
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<wyroby_typ> PobierzTypy()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby_typ
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_kategorie> PobierzITFKategorie()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_kategorie
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_litery> PobierzITFZnaki()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_litery
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_icc> PobierzITFicc()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_icc
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_cc> PobierzITFcc()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_cc
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_sr> PobierzITFsr()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_sr
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_trn> PobierzITFtrn()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_trn
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_odch> PobierzITFodch()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_odch
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_litery> PobierzTraceZnak()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_litery
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_kategoria> PobierzTraceKategorie()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_kategoria
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_sr> PobierzTraceSr()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_sr
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_producent> PobierzTraceProducent()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_producent
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_sdr> PobierzTraceSdr()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_sdr
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_pe_m> PobierzTracePem()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_pe_m
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_material> PobierzTraceMaterial()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_material
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_pe_o> PobierzTracePeo()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_pe_o
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<trace_mfr> PobierzTraceMfr()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.trace_mfr
                            orderby w.id ascending
                            select w).ToList();
                return list;
            }
        }

        public static void AddProduct(wyroby _nowyWyrob)
        {
            using (var db = new ecoplastolEntities())
            {

                db.wyroby.Add(_nowyWyrob);
                db.SaveChanges();
            }
        }

        public static void UpdProduct(wyroby _nowyWyrob)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(_nowyWyrob).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
