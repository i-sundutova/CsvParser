using System.Reflection;

namespace MI.CsvParser.Converters
{
    /// <summary>
    /// Converts string to long. <inheritdoc cref="NumberTypeConverter"/>
    /// </summary>
    public sealed class LongConverter : NumberTypeConverter, ICsvConverter
    {
        public static LongConverter Default { get; } = new LongConverter();

        public override object Convert(string value, PropertyInfo propertyInfo)
        {
            return long.Parse(value, _numberStyles, _formatProvider);
        }
    }
}
