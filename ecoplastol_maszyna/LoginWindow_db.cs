using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol_maszyna
{

    class LoginWindow_db
    {
        public static List<operatorzy_maszyn> PobierzOperatorow()
        {
            using (var db = new ecoplastolEntities())
            {
                var list = (from o in db.operatorzy_maszyn
                            where o.id > 0
                            orderby o.id ascending
                            select o).ToList();
                return list;
            }
        }
    }
}
