using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmUzytkownicy_db
    {
        public static users PobierzUzytkownika(string _user)
        {
            using (var db = new wannaEntities())
            {

                var dbUser = db.users.Single(users => users.login == _user);


                return dbUser;
            }
        }

        public static List<users> PobierzUzytkownikow()
        {
            using (var db = new wannaEntities())
            {
                var listUzytkownicy = (from us in db.users
                                       orderby us.id ascending
                                       select us).ToList();
                return listUzytkownicy;
            }
        }
    }
}
