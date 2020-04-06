using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using MI.CsvParser.Converters;

namespace MI.CsvParser
{
    /// <summary>
    /// Class mapper. Represents mapping of class properties and their specifications.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public class CsvClassMap<TClass>
    {
        internal IReadOnlyList<CsvPropertyMap> Properties => _properties;

        private readonly List<CsvPropertyMap> _properties = new List<CsvPropertyMap>();

        /// <summary>
        /// Class mapping.
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        protected void Map<TMember>(Expression<Func<TClass, TMember>> expression, ICsvConverter converter = null)
        {
            var member = (MemberExpression)expression.Body;
            var propInfo = (PropertyInfo)member.Member;

            _properties.Add(new CsvPropertyMap
            {
                Name = propInfo.Name,
                PropertyInfo = propInfo,
                Converter = converter,
                IsNullable = propInfo.PropertyType.IsGenericType && propInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
            });
        }
    }
}
