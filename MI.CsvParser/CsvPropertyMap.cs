using System.Reflection;
using MI.CsvParser.Converters;

namespace MI.CsvParser
{
    /// <summary>
    /// Represents class property map specifications.
    /// </summary>
    public class CsvPropertyMap
    {
        public string Name { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public ICsvConverter Converter { get; set; }

        public bool IsNullable { get; set; }
    }
}