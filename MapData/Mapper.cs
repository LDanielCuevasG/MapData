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

            try
            {
                if (reader.HasRows == true)
                {
                    int numColumns = reader.FieldCount;
                    if (reader.Read() == true)
                    {
                        for (int i = 0; i < numColumns; ++i)
                        {
                            string columnName = reader.GetName(i);
                            object columnValue = reader.GetValue(i);
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
            catch (Exception e) {
                throw;
            }

            return data;
        }

        public static List<T> MapList<T>(DbDataReader reader)
        {
            List<T> list = Activator.CreateInstance<List<T>>();
            T data = Activator.CreateInstance<T>();
            Type dataType = data.GetType();

            try
            {
                if (reader.HasRows == true)
                {
                    int numColumns = reader.FieldCount;
                    while (reader.Read() == true)
                    {
                        T entry = Activator.CreateInstance<T>();
                        Type entryType = entry.GetType();

                        for (int i = 0; i < numColumns; ++i)
                        {
                            string columnName = reader.GetName(i);
                            object columnValue = reader.GetValue(i);
                            PropertyInfo entryProperty = entryType.GetProperty(columnName);
                            if (entryProperty != null)
                            {
                                Type entryPropertyType = entryProperty.PropertyType;
                                entryProperty.SetValue(entry, Convert.ChangeType(columnValue, entryPropertyType));
                            }

                        }

                        list.Add(entry);
                    }
                }
            }
            catch (Exception e) {
                throw;
            }

            return list;
        }

    }
}
