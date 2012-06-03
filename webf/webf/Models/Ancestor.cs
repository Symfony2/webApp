using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using webf.Models.EntityModel;

namespace webf.Models
{
    public abstract class Ancestor
    {
        protected FlexDBEntities db = new FlexDBEntities();

        protected void fillCollections()
        {
            IEnumerable<PropertyInfo> props = this.GetType().GetProperties().Where(pr =>
                                                                                   (pr.GetCustomAttributes(
                                                                                       typeof (FillListAttribute), false)
                                                                                       .Count()) != 0); /*tabels with attributes*/
            if (props != null)
            {

                FillListAttribute attr = null;
                Type tableType = null;
                string columnName = string.Empty;
                /*webf.Models.EntityModel*/
                foreach (var propertyInfo in props)
                {
                    attr = ((FillListAttribute)
                            propertyInfo.GetCustomAttributes(typeof (FillListAttribute), false).First());
                    tableType = attr.TableType;
                    columnName = attr.ColumnName;
                    if (propertyInfo.PropertyType.Name.Equals("SelectList"))
                    {
                        Dictionary<int, string> coll = interActionDB(tableType, columnName);
                        propertyInfo.SetValue(this,new SelectList(coll,"Key","Value",coll.First().Key),null);
                    }

                }
            }
        }
        
        /// <summary>
        /// Метод возвращяет коллекцию ключ-значение. Ограничение ключ должен иметь тип (integer) 
        /// </summary>
        /// <param name="tableType">Тип табличной модели из концептуального слоя</param>
        /// <param name="tableColumnName">Имя свойства из этой таблицы</param>
        /// <returns></returns>
        private Dictionary<int,string> interActionDB(Type tableType,string tableColumnName)
        {
            
            Dictionary<int,string> collectionVK = new Dictionary<int, string>();


            PropertyInfo primaryKeyProp = tableType.GetProperties().First(pr =>
                    ((EdmScalarPropertyAttribute)(pr.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false)
                         .First())).EntityKeyProperty == true);   /*Seek for primary key property*/

            if (tableType != null)
            {
                var query = ((System.Collections.IEnumerable)db.GetType().GetProperty(tableType.Name).GetValue(db, null));

                
                foreach (var row in query)
                {
                    collectionVK.Add((int)row.GetType().GetProperty(primaryKeyProp.Name).GetValue(row, null),
                                 (string)row.GetType().GetProperty(tableColumnName).GetValue(row, null));
                }
            }
            return collectionVK;
        }

        protected void modelBinding<TableType>(TableType _tableEF)
        {
            
        }
    }

    public class BindingPropertyAttribute : Attribute
    {
        public string ModelPropertyName { get; set; }
        public BindingPropertyAttribute(string propertyNameEF)
        {
            ModelPropertyName = propertyNameEF;
        }
    }
    
    public class FillListAttribute : Attribute
    {
        public Type TableType { get; set; }
        public string ColumnName { get; set; }

        public FillListAttribute(Type typeType,string colName)
        {
            TableType = typeType;
            ColumnName = colName;
        }
    }
}