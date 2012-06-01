using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Web;
using webf.Models.EntityModel;

namespace webf.SvcDependencies
{
    public class DataBaseManeger<Tentities> : IDbServices
    {
        protected Tentities db;
   
        public DataBaseManeger()
        {
            db = Activator.CreateInstance<Tentities>(); // Main model DataBase
        }
        
        /// <summary>
        /// Записывает данные в базу и сохраняет ее
        /// </summary>
        /// <typeparam name="Tmodel"></typeparam>
        /// <returns></returns>
        public bool saveModeltoDB<TableType>(TableType tableObj)
        {
            PropertyInfo[] slaveProperty = tableObj.GetType().GetProperties();
            
            TableType obj = Activator.CreateInstance<TableType>();
            PropertyInfo[] objProps = obj.GetType().GetProperties();

            PropertyInfo objProp = objProps.First(pr => ((EdmScalarPropertyAttribute)pr
                .GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false).First())
                .EntityKeyProperty == true); // Seek for primary key property in collection
            
            if (objProp != null && objProp.PropertyType.Name.Equals("Guid"))
                objProp.SetValue(obj, Guid.NewGuid(), null);

            foreach (PropertyInfo prop in slaveProperty)
            {
                typeof(TableType).GetProperty(prop.Name).
                    SetValue(obj, prop.GetValue(this, null), null);
            }
            //db.AddToOffice
            MethodInfo addMethod = db.GetType().GetMethod("AddTo" + obj.GetType().Name);
            addMethod.Invoke(db, new object[] { obj });
            String str = obj.GetType().Name;
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