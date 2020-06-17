using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmUzytkownicy_db
    {
        public static uzytkownicy PobierzUzytkownika(string _user)
        {
            using (var db = new ecoplastolEntities())
            {

                var dbUser = db.uzytkownicy.Single(u => u.login == _user);

                return dbUser;
            }
        }

        public static List<uzytkownicy> PobierzUzytkownikow()
        {
            using (var db = new ecoplastolEntities())
            {
                var listUzytkownicy = (from us in db.uzytkownicy
                                       where us.aktywny == true
                                       orderby us.id ascending
                                       select us).ToList();
                return listUzytkownicy;
            }
        }
    }
}
