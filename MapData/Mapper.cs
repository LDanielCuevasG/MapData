using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapData
{
    public class Mapper
    {

        public static T MapObject<T>(DbDataReader reader)
        {
            T data = Activator.CreateInstance<T>();
            Type dataType = data.GetType();

            if (reader.HasRows)
            {
                int columns = reader.FieldCount;
                while (reader.Read())
                {
                    for (int i = 0; i < columns; ++i)
                    {
                        string columnName = reader.GetName(i);
                        object columnValue = reader.GetValue(i);
                        if (columnValue != DBNull.Value)
                        {
                            PropertyInfo dataProperty = dataType.GetProperty(columnName);
                            if (dataProperty != null)
                            {
                                Type dataPropertyType = dataProperty.PropertyType;
                                dataProperty.SetValue(data, Convert.ChangeType(columnValue, dataPropertyType));
                            }
                        }

                    }
                }
            }
            else  {
                return default(T);
            }

            return data;
        }

        public static List<T> MapList<T>(DbDataReader reader)
        {
            List<T> list = Activator.CreateInstance<List<T>>();
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
                        if (columnValue != DBNull.Value)
                        {
                            PropertyInfo singleProperty = singleType.GetProperty(columnName);
                            if (singleProperty != null)
                            {
                                Type singlePropertyType = singleProperty.PropertyType;
                                singleProperty.SetValue(single, Convert.ChangeType(columnValue, singlePropertyType));
                            }
                        }

                    }

                    list.Add(single);
                }
            }
            else {
                return default(List<T>);
            }

            return list;
        }

    }
}
