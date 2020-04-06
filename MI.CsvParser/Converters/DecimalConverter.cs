using System.Reflection;

namespace MI.CsvParser.Converters
{
    /// <summary>
    /// Converts string to decimal. <inheritdoc cref="NumberTypeConverter"/>.
    /// </summary>
    public sealed class DecimalConverter : NumberTypeConverter, ICsvConverter
    {
        public static DecimalConverter Default { get; } = new DecimalConverter();

        public override object Convert(string value, PropertyInfo propertyInfo)
        {
            return decimal.Parse(value, _numberStyles, _formatProvider);
        }
    }
}
