using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class frmParametry_db
    {
        public static List<parameters> GetParameters(string _kategoria)
        {
            using (var db = new wannaEntities())
            {
                var listP = (from par in db.parameters
                             where par.kategoria == _kategoria
                             orderby par.grupa, par.indeks ascending
                             select par).ToList();
                return listP;
            }
        }
        public static void AddParameter(parameters _newParameter)
        {
            using (var db = new wannaEntities())
            {

                db.parameters.Add(_newParameter);
                db.SaveChanges();
            }
        }

        public static void UpdParameter(parameters _newParameter)
        {
            using (var db = new wannaEntities())
            {
                var entity = db.parameters.Find(_newParameter.id);
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

        public static void UpdParameter2(parameters _newParameter)
        {
            using (var db = new wannaEntities())
            {
                db.Entry(_newParameter).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeletedParameter(int _ParametrId)
        {
            using (var db = new wannaEntities())
            {
                var row = new parameters() { id = _ParametrId };
                db.parameters.Attach(row);
                db.parameters.Remove(row);
                db.SaveChanges();
            }
        }
    }
}
