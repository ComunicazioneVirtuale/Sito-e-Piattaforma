using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SitoWeb.Utility
{
    public static class Estenzioni
    {
        //divide una lista in n sottoliste di dimensioni specificate
        public static IEnumerable<List<T>> SplitList<T>(this List<T> list, int nSize = 30)
        {
            for (int i = 0; i < list.Count; i += nSize)
            {
                yield return list.GetRange(i, Math.Min(nSize, list.Count - i));
            }
        }

        public static string SerializeToXml<T>(this T entity)
        {
            if (entity == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, entity);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public static T DeserializeFromXML<T>(this string xml)
        {
            if (String.IsNullOrEmpty(xml)) throw new NotSupportedException("ERROR: input string cannot be null.");
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringReader = new StringReader(xml);
                using (var reader = XmlReader.Create(stringReader))
                {
                    return (T)xmlserializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public static bool isNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool isNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        public static T ConvertDataRowToEntity<T>(this DataRow row)
        {
            try
            {
                Type objType = typeof(T);
                T obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo property =
                        objType.GetProperty(column.ColumnName,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    //setto la proprietà
                    if (property != null && property.CanWrite)
                    {
                        object value = row[column.ColumnName];
                        object castValue = new object();
                        ///default è se stesso
                        castValue = value;

                        if (value == DBNull.Value)
                        {
                            castValue = null;
                        }
                        else
                        {
                            if (property.PropertyType.FullName.Contains("DateTime"))
                            {
                                castValue = Convert.ToDateTime(value);
                            }

                            if (property.PropertyType.FullName.Contains("Decimal"))
                            {
                                castValue = Convert.ToDecimal(value);
                            }

                            if (property.PropertyType.FullName.Contains("float"))
                            {
                                castValue = Convert.ToDouble(value);
                            }

                            if (property.PropertyType.FullName.Contains("Int"))
                            {
                                castValue = Convert.ToInt32(value);
                            }

                            if (property.PropertyType.FullName.Contains("long"))
                            {
                                castValue = Convert.ToInt64(value);
                            }
                        }
                        property.SetValue(obj, castValue, null);
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<T> ToListEntity<T>(this DataTable dt) //where T : class, new()
        {
            if (dt == null || dt.Rows.Count == 0) return null;
            IList<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = ConvertDataRowToEntity<T>(row);
                list.Add(obj);
            }
            return list;
        }
    }
}
