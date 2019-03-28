using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol.konfiguracja
{
    class PanelITF_db
    {
        /// --------------------------------------
        /// KATEGORIE
        /// --------------------------------------
        public static void PoprawITFkategoria(itf_kategorie poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFkategoria(itf_kategorie poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_kategorie.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFkategoria()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_kategorie.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunITFkategoria(itf_kategorie poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.itf_kategorie.Attach(poz);
                db.itf_kategorie.Remove(poz);
                db.SaveChanges();
            }
        }
        /// --------------------------------------
        /// LITERY
        /// --------------------------------------
        public static void PoprawITFlitery(itf_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFlitery(itf_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_litery.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFlitery()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_litery.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunITFlitery(itf_litery poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.itf_litery.Attach(poz);
                db.itf_litery.Remove(poz);
                db.SaveChanges();
            }
        }
        /// --------------------------------------
        /// ICC
        /// --------------------------------------
        public static void PoprawITFicc(itf_icc poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFicc(itf_icc poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_icc.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFicc()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_icc.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunITFicc(itf_icc poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.itf_icc.Attach(poz);
                db.itf_icc.Remove(poz);
                db.SaveChanges();
            }
        }
        /// --------------------------------------
        /// CC
        /// --------------------------------------
        public static void PoprawITFcc(itf_cc poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFcc(itf_cc poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_cc.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFcc()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_cc.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunITFcc(itf_cc poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.itf_cc.Attach(poz);
                db.itf_cc.Remove(poz);
                db.SaveChanges();
            }
        }
        /// --------------------------------------
        /// SR
        /// --------------------------------------
        public static void PoprawITFsr(itf_sr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFsr(itf_sr poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_sr.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFsr()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_sr.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunITFsr(itf_sr poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.itf_sr.Attach(poz);
                db.itf_sr.Remove(poz);
                db.SaveChanges();
            }
        }
        /// --------------------------------------
        /// TRN
        /// --------------------------------------
        public static void PoprawITFtrn(itf_trn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFtrn(itf_trn poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_trn.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFtrn()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_trn.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunITFtrn(itf_trn poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.itf_trn.Attach(poz);
                db.itf_trn.Remove(poz);
                db.SaveChanges();
            }
        }
        /// --------------------------------------
        /// ODCH
        /// --------------------------------------
        public static void PoprawITFodch(itf_odch poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(poz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DodajITFodch(itf_odch poz)
        {
            using (var db = new ecoplastolEntities())
            {

                db.itf_odch.Add(poz);
                db.SaveChanges();
            }
        }
        public static int IdITFodch()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.itf_odch.Max(p => p.id) + 1;
                return newId;
            }
        }
        public static void UsunITFodch(itf_odch poz)
        {
            using (var db = new ecoplastolEntities())
            {
                db.itf_odch.Attach(poz);
                db.itf_odch.Remove(poz);
                db.SaveChanges();
            }
        }
    }
}
