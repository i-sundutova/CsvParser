using System;
using System.Globalization;
using System.Reflection;

namespace MI.CsvParser.Converters
{
    /// <summary>
    /// Converts string to DateTime. String format default value is 'yyyy-MM-dd HH:mm'. Format provider default value is <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public sealed class DateTimeConverter : ICsvConverter
    {
        private readonly string _format;
        private readonly IFormatProvider _formatProvider;

        public static DateTimeConverter Default { get; } = new DateTimeConverter();

        /// <summary>
        /// Initializes a new instance of the class. String format default value is 'yyyy-MM-dd HH:mm'. Format provider default value is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        public DateTimeConverter() : this("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) {}

        /// <summary>
        /// Initializes a new instance of the class. Format provider default value is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="format"></param>
        public DateTimeConverter(string format) : this(format, CultureInfo.InvariantCulture) { }

        /// <summary>
        /// Initializes a new instance of the class. String format default value is 'yyyy-MM-dd HH:mm'.
        /// </summary>
        /// <param name="formatProvider"></param>
        public DateTimeConverter(IFormatProvider formatProvider) : this("yyyy-MM-dd HH:mm", formatProvider) { }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        public DateTimeConverter(string format, IFormatProvider formatProvider)
        {
            _format = !string.IsNullOrEmpty(format) ? format : throw new ArgumentException(nameof(format));
            _formatProvider = formatProvider ?? throw new ArgumentNullException(nameof(formatProvider));
        }

        public object Convert(string value, PropertyInfo propertyInfo)
        {
            return DateTime.ParseExact(value, _format, _formatProvider);
        }
    }
}
