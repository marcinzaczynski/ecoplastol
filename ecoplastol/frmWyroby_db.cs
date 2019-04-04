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
