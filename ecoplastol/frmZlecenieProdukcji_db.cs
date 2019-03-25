using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmZlecenieProdukcji_db
    {
        public static List<wyroby> PobierzWyroby()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from w in db.wyroby
                            orderby w.wyrob_kod ascending
                            select w).ToList();
                return list;
            }
        }
    }
}
