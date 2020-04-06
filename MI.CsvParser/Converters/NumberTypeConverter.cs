using System;
using System.Globalization;
using System.Reflection;

namespace MI.CsvParser.Converters
{
    /// <summary>
    /// Number styles default value is <see cref="NumberStyles.Any"/>. Format provider default value is <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public abstract class NumberTypeConverter
    {
        protected readonly NumberStyles _numberStyles;
        protected readonly IFormatProvider _formatProvider;

        /// <summary>
        /// Initializes a new instance of the class. Number styles default value is <see cref="NumberStyles.Any"/>. Format provider default value is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        protected NumberTypeConverter() : this(NumberStyles.Any, CultureInfo.InvariantCulture) { }

        /// <summary>
        /// Initializes a new instance of the class. Format provider default value is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="numberStyles"></param>
        protected NumberTypeConverter(NumberStyles numberStyles) : this(numberStyles, CultureInfo.InvariantCulture) { }

        /// <summary>
        /// Initializes a new instance of the class. Number styles default value is <see cref="NumberStyles.Any"/>.
        /// </summary>
        /// <param name="formatProvider"></param>
        protected NumberTypeConverter(IFormatProvider formatProvider) : this(NumberStyles.Any, formatProvider) { }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="numberStyles"></param>
        /// <param name="formatProvider"></param>
        protected NumberTypeConverter(NumberStyles numberStyles, IFormatProvider formatProvider)
        {
            _numberStyles = numberStyles;
            _formatProvider = formatProvider ?? throw new ArgumentNullException(nameof(formatProvider));
        }

        public abstract object Convert(string value, PropertyInfo propertyInfo);
    }
}
