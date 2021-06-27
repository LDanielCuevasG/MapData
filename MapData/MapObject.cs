using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapData
{
    public class MapObject
    {
        public static T Map<T>(DbDataReader reader)
        {
            T returnedObject = Activator.CreateInstance<T>();
            Type dataType = returnedObject.GetType();

            if (reader.HasRows)
            {
                int columns = reader.FieldCount;
                while (reader.Read())
                {
                    for (int i = 0; i < columns; ++i)
                    {
                        string columnName = reader.GetName(i);
                        object columnValue = reader.GetValue(i);
                        PropertyInfo dataProperty = dataType.GetProperty(columnName);
                        if (dataProperty != null)
                        {
                            Type dataPropertyType = dataProperty.PropertyType;
                            dataProperty.SetValue(returnedObject, Convert.ChangeType(columnValue, dataPropertyType));
                        }

                    }
                }
            }

            return returnedObject;
        }
    }


}
