using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace CompareVoucherStatusApp
{
    public class DataTableToDataListHelper : IDataTableToDataListHelper
    {
        public DataTableToDataListHelper()
        {

        }

        public IEnumerable<T> ConvertDataTableToList<T>(DataTable table) where T : new()
        {
            foreach (DataRow row in table.Rows)
            {
                T t = new T();
                Type tType = t.GetType();

                foreach (var property in tType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (table.Columns.Contains(property.Name) && !row.IsNull(property.Name))
                    {
                        property.SetValue(t, row[property.Name]);
                    }
                }

                yield return t;
            }
        }

        public DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
