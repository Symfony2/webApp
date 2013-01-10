using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Web;

namespace webf.SvcDependencies.db
{
    public class DataBaseManeger<TDBmodel> : IDbServices<TDBmodel>
    {
        protected TDBmodel db = Activator.CreateInstance<TDBmodel>();
        
        public TDBmodel DataBase
        {
            get { return db; }
        }

        public bool saveModeltoDB<Tmodel>(Type tableType, Tmodel modelTableObj)
        {

            PropertyInfo keyProp = tableType.GetProperties().First(pr =>
                ((EdmScalarPropertyAttribute)(pr.GetCustomAttributes(typeof (EdmScalarPropertyAttribute), false)
                     .First())).EntityKeyProperty == true);   /*Seek for primary key property*/


            MethodInfo addMethod = db.GetType().GetMethod("AddTo" + modelTableObj.GetType().Name);
            addMethod.Invoke(db, new object[] { modelTableObj });
            String str = modelTableObj.GetType().Name;
            try
            {
                MethodInfo methodSave = db.GetType().GetMethod("SaveChanges", new Type[0]);
                methodSave.Invoke(db, null);
                return true;
            }
            catch
            {
                return false;
            }
        }        
    }
}