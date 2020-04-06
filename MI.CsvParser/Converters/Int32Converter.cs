using System.Reflection;

namespace MI.CsvParser.Converters
{
    ///  <summary>
    ///  Converts string to int32. <inheritdoc cref="NumberTypeConverter"/>
    ///  </summary>
    public sealed class Int32Converter : NumberTypeConverter, ICsvConverter
    {
        public static Int32Converter Default { get; } = new Int32Converter();

        public override object Convert(string value, PropertyInfo propertyInfo)
        {
            return int.Parse(value, _numberStyles, _formatProvider);
        }
    }
}
