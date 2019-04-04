using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja.traceability
{
    class PanelTrace_db
    {
        /// --------------------------------------
        /// LITERY
        /// --------------------------------------
        /// 
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

        public static void PoprawTraceLitery(trace_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceLitery(trace_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_litery.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceLitery()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_litery.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceLitery(trace_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_litery.Attach(poz);
                db.trace_litery.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// KATEGORIE
        /// --------------------------------------
        /// 
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

        public static void PoprawTraceKategorie(trace_kategoria poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceKategorie(trace_kategoria poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_kategoria.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceKategorie()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_kategoria.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceKategorie(trace_kategoria poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_kategoria.Attach(poz);
                db.trace_kategoria.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// ŚREDNICE
        /// --------------------------------------
        /// 
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

        public static void PoprawTraceSr(trace_sr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceSr(trace_sr poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_sr.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceSr()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_sr.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceSr(trace_sr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_sr.Attach(poz);
                db.trace_sr.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// PRODUCENCI
        /// --------------------------------------
        /// 
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

        public static void PoprawTraceProducent(trace_producent poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceProducent(trace_producent poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_producent.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceProducent()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_producent.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceProducent(trace_producent poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_producent.Attach(poz);
                db.trace_producent.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// SDR
        /// --------------------------------------
        /// 
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

        public static void PoprawTraceSDR(trace_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceSDR(trace_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_sdr.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceSDR()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_sdr.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceSDR(trace_sdr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_sdr.Attach(poz);
                db.trace_sdr.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// PEm
        /// --------------------------------------
        /// 
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

        public static void PoprawTracePEm(trace_pe_m poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTracePEm(trace_pe_m poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_pe_m.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTracePEm()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_pe_m.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTracePEm(trace_pe_m poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_pe_m.Attach(poz);
                db.trace_pe_m.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// MATERIAL
        /// --------------------------------------
        /// 
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

        public static void PoprawTraceMaterial(trace_material poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceMaterial(trace_material poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_material.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceMaterial()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_material.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceMaterial(trace_material poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_material.Attach(poz);
                db.trace_material.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// PEo
        /// --------------------------------------
        /// 
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

        public static void PoprawTracePEo(trace_pe_o poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTracePEo(trace_pe_o poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_pe_o.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTracePEo()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_pe_o.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTracePEo(trace_pe_o poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_pe_o.Attach(poz);
                db.trace_pe_o.Remove(poz);
                db.SaveChanges();
            }
        }

        /// --------------------------------------
        /// MFR
        /// --------------------------------------
        /// 
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

        public static void PoprawTraceMFR(trace_mfr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajTraceMFR(trace_mfr poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.trace_mfr.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdTraceMFR()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.trace_mfr.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunTraceMFR(trace_mfr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.trace_mfr.Attach(poz);
                db.trace_mfr.Remove(poz);
                db.SaveChanges();
            }
        }
    }
}