using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MI.CsvParser.Converters
{
    /// <summary>
    /// Converts string to Enum.
    /// </summary>
    public sealed class EnumConverter : ICsvConverter
    {
        public static EnumConverter Default { get; } = new EnumConverter();

        public object Convert(string value, PropertyInfo propertyInfo)
        {
            object enumValue = null;

            foreach (var field in propertyInfo.PropertyType.GetFields().Where(f => f.FieldType == propertyInfo.PropertyType))
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == value) enumValue = field.GetValue(null);
                }
                else
                {
                    if (field.Name == value) enumValue = field.GetValue(null);
                }
            }

            return enumValue;
        }
    }
}
