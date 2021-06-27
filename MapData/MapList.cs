using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapData
{
    public class MapList
    {
        public static List<T> Map<T>(DbDataReader reader)
        {
            List<T> returnedList = Activator.CreateInstance<List<T>>();
            T objectTarget = Activator.CreateInstance<T>();
            Type dataType = objectTarget.GetType();

            if (reader.HasRows)
            {
                int columns = reader.FieldCount;
                while (reader.Read())
                {
                    T single = Activator.CreateInstance<T>();
                    Type singleType = objectTarget.GetType();

                    for (int i = 0; i < columns; ++i)
                    {
                        string columnName = reader.GetName(i);
                        object columnValue = reader.GetValue(i);
                        PropertyInfo singleProperty = singleType.GetProperty(columnName);
                        if (singleProperty != null)
                        {
                            Type singlePropertyType = singleProperty.PropertyType;
                            singleProperty.SetValue(single, Convert.ChangeType(columnValue, singlePropertyType));
                        }

                    }

                    returnedList.Add(single);
                }
            }

            return returnedList;
        }
    }
}
