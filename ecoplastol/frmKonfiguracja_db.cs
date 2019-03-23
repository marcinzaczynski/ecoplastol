using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmKonfiguracja_db
    {
        public static List<itf_kategorie> PobierzITFKategorie()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_kategorie
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_litery> PobierzITFLitery()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_litery
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_icc> PobierzITFicc()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_icc
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_cc> PobierzITFcc()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_cc
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_sr> PobierzITFsr()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_sr
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_trn> PobierzITFtrn()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_trn
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }

        public static List<itf_odch> PobierzITFodch()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.itf_odch
                            orderby w.indeks ascending
                            select w).ToList();
                return list;
            }
        }
    }
}
