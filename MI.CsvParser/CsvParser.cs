using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MI.CsvParser.Converters;
using MI.CsvParser.Exceptions;

namespace MI.CsvParser
{
    public static class CsvParser<T, TMap> where T : new() where TMap : CsvClassMap<T>, new()
    {
        private static readonly TMap _map = new TMap();

        private static readonly ConcurrentDictionary<Type, ICsvConverter> _converters = new ConcurrentDictionary<Type, ICsvConverter>
        {
            [typeof(int)] = Int32Converter.Default,
            [typeof(long)] = LongConverter.Default,
            [typeof(decimal)] = DecimalConverter.Default,
            [typeof(DateTime)] = DateTimeConverter.Default,
            [typeof(Enum)] = EnumConverter.Default,
            [typeof(Boolean)] = BooleanConverter.Default
        };


        /// <summary>
        /// Parses csv file (UTF8 encoded) by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimiter"></param>
        /// <returns>Lists of objects.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="System.Reflection.TargetException"></exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="MethodAccessException"></exception>
        /// <exception cref="InvalidFileException"></exception>
        /// <exception cref="ConvertationException"></exception>
        public static Task<List<T>> ParseAsync(string filePath, string delimiter = ";")
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentException(nameof(filePath));
            if (string.IsNullOrEmpty(delimiter)) throw new ArgumentException(nameof(delimiter));

            return ParseAsync(filePath, Encoding.UTF8, delimiter);
        }

        /// <summary>
        /// Parses csv file by file path according to specified encoding.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <param name="delimiter"></param>
        /// <returns>Lists of objects.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="System.Reflection.TargetException"></exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="MethodAccessException"></exception>
        /// <exception cref="InvalidFileException"></exception>
        /// <exception cref="ConvertationException"></exception>
        public static async Task<List<T>> ParseAsync(string filePath, Encoding encoding, string delimiter = ";")
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentException(nameof(filePath));
            if (string.IsNullOrEmpty(delimiter)) throw new ArgumentException(nameof(delimiter));

            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream, encoding))
            {
                return await ParseAsync(reader, delimiter).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Parses csv file.
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="delimiter"></param>
        /// <returns>Lists of objects.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="System.Reflection.TargetException"></exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="MethodAccessException"></exception>
        /// <exception cref="InvalidFileException"></exception>
        /// <exception cref="ConvertationException"></exception>
        public static async Task<List<T>> ParseAsync(StreamReader streamReader, string delimiter = ";")
        {
            if (streamReader == null) throw new ArgumentNullException(nameof(streamReader));
            if (string.IsNullOrEmpty(delimiter)) throw new ArgumentException(nameof(delimiter));

            var result = new List<T>();
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync().ConfigureAwait(false);
                var values = line.Split(new[] { delimiter }, StringSplitOptions.None);

                if (_map.Properties.Count > values.Length) throw new InvalidFileException("Invalid file.");

                var newObject = new T();

                for (var i = 0; i < _map.Properties.Count; i++)
                {
                    var propertyMap = _map.Properties[i];

                    if (propertyMap.Converter != null) propertyMap.PropertyInfo.SetValue(newObject, propertyMap.Converter.Convert(values[i], propertyMap.PropertyInfo));
                    else
                    {
                        if (propertyMap.PropertyInfo.PropertyType == typeof(string)) propertyMap.PropertyInfo.SetValue(newObject, values[i]);
                        else
                        {
                            var propertyType = propertyMap.PropertyInfo.PropertyType;

                            if (propertyMap.IsNullable)
                            {
                                if (string.IsNullOrEmpty(values[i])) continue;
                                propertyType = Nullable.GetUnderlyingType(propertyType);
                            }

                            if (!_converters.TryGetValue(propertyType, out var converter))
                            {
                                if (!_converters.TryGetValue(propertyType.BaseType, out converter)) throw new ConvertationException($"Unable to find converter for type '{propertyType.FullName}'");
                            }

                            propertyMap.PropertyInfo.SetValue(newObject, converter.Convert(values[i], propertyMap.PropertyInfo));
                        }
                    }
                }

                result.Add(newObject);
            }

            return result;
        }
    }
}
