using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmParametry_db
    {
        public static int Id()
        {
            using (var db = new ecoplastolEntities())
            {
                int newId = db.parametry.Max(p => p.id) + 1;
                return newId;
            }
        }

        public static List<parametry> GetParameters(string _kategoria)
        {
            using (var db = new ecoplastolEntities())
            {
                var listP = (from par in db.parametry
                             where par.kategoria == _kategoria
                             orderby par.grupa, par.indeks ascending
                             select par).ToList();
                return listP;
            }
        }

        public static void AddParameter(parametry _newParameter)
        {
            using (var db = new ecoplastolEntities())
            {

                db.parametry.Add(_newParameter);
                db.SaveChanges();
            }
        }

        public static void UpdParameter(parametry _newParameter)
        {
            using (var db = new ecoplastolEntities())
            {
                var entity = db.parametry.Find(_newParameter.id);
                if (entity != null)
                {
                    entity.grupa = _newParameter.grupa;
                    entity.parametr = _newParameter.parametr;
                    entity.wartosc = _newParameter.wartosc;
                    entity.opis = _newParameter.opis;
                    entity.opm = _newParameter.opm;
                    entity.czasm = _newParameter.czasm;
                    db.SaveChanges();
                }
            }
        }

        public static void UpdParameter2(parametry _newParameter)
        {
            using (var db = new ecoplastolEntities())
            {
                db.Entry(_newParameter).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeletedParameter(int _ParametrId)
        {
            using (var db = new ecoplastolEntities())
            {
                var row = new parametry() { id = _ParametrId };
                db.parametry.Attach(row);
                db.parametry.Remove(row);
                db.SaveChanges();
            }
        }
    }
}
