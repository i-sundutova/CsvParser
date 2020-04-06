using System;
using System.Reflection;

namespace MI.CsvParser.Converters
{
    public interface ICsvConverter
    {
        /// <summary>
        /// Converts string to another type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="TargetException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="FieldAccessException"></exception>
        object Convert(string value, PropertyInfo propertyInfo);
    }
}
