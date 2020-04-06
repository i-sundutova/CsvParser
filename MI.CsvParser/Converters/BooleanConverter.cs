using System;
using System.Reflection;

namespace MI.CsvParser.Converters
{
    /// <summary>
    /// Converts string to boolean.
    /// </summary>
    public sealed class BooleanConverter : ICsvConverter
    {
        private readonly string _trueStr;
        private readonly string _falseStr;
        public static BooleanConverter Default { get; } = new BooleanConverter("true", "false");

        public BooleanConverter(string trueStr, string falseStr)
        {
            _trueStr = trueStr;
            _falseStr = falseStr;
        }

        public object Convert(string value, PropertyInfo propertyInfo)
        {
            if (_trueStr.Equals(value, StringComparison.OrdinalIgnoreCase)) return true;
            if (_falseStr.Equals(value, StringComparison.OrdinalIgnoreCase)) return false;

            throw new FormatException($"Invalid boolean value: {value}");
        }
    }
}