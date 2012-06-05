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
                                                                                       .Count()) != 0);
                /*tabels with attributes*/
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
                        propertyInfo.SetValue(this, new SelectList(coll, "Key", "Value", coll.First().Key), null);
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
        private Dictionary<int, string> interActionDB(Type tableType, string tableColumnName)
        {

            Dictionary<int, string> collectionVK = new Dictionary<int, string>();


            PropertyInfo primaryKeyProp = tableType.GetProperties().First(pr =>
                                                                          ((EdmScalarPropertyAttribute)
                                                                           (pr.GetCustomAttributes(
                                                                               typeof (EdmScalarPropertyAttribute),
                                                                               false)
                                                                               .First())).EntityKeyProperty == true);
                /*Seek for primary key property*/

            if (tableType != null)
            {
                var query =
                    ((System.Collections.IEnumerable) db.GetType().GetProperty(tableType.Name).GetValue(db, null));


                foreach (var row in query)
                {
                    collectionVK.Add((int) row.GetType().GetProperty(primaryKeyProp.Name).GetValue(row, null),
                                     (string) row.GetType().GetProperty(tableColumnName).GetValue(row, null));
                }
            }
            return collectionVK;
        }

        public void createBinding<EFTableType>(EFTableType modelTable)
        {

            IEnumerable<PropertyInfo> localProps =
                this.GetType().GetProperties().Where(pr =>
                    pr.GetCustomAttributes(typeof(BindingPropertyAttribute), false).FirstOrDefault() != null);

            BindingPropertyAttribute localAttr = null;
            foreach (PropertyInfo localProp in localProps)
            {
                localAttr =
                    (BindingPropertyAttribute)
                    localProp.GetCustomAttributes(typeof(BindingPropertyAttribute), false).First();
                var currentEFProperty = modelTable.GetType().GetProperty(localAttr.ModelPropertyName);

                var currObj = this.GetType().GetProperties()
                                .FirstOrDefault(lo => ((BindingPropertyAttribute)lo
                                .GetCustomAttributes(typeof(BindingPropertyAttribute), false)
                                .FirstOrDefault()).ModelPropertyName == localAttr.ModelPropertyName);

                if (currentEFProperty != null)
                {
                    currObj.SetValue(this, currentEFProperty.GetValue(modelTable, null), null);
                }
                else
                {
                    IEnumerable<PropertyInfo> relationTableProperties =
                        modelTable.GetType().GetProperties().Where(pr =>
                        pr.GetCustomAttributes(typeof(EdmRelationshipNavigationPropertyAttribute), false).FirstOrDefault() != null);
                    foreach (PropertyInfo property in relationTableProperties)
                    {
                        var relationalProperty = property.PropertyType.GetProperty(localAttr.ModelPropertyName);
                        if (relationalProperty != null)
                        {

                            var relpObj = modelTable.GetType().GetProperty(property.Name).GetValue(modelTable, null);
                            var newData = relpObj.GetType().GetProperty(localAttr.ModelPropertyName).GetValue(relpObj, null);

                            currObj.SetValue(this, newData, null);
                            break;
                        }
                    }
                }
            }
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

        public FillListAttribute(Type typeType, string colName)
        {
            TableType = typeType;
            ColumnName = colName;
        }
    }
}
