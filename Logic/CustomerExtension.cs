using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CustomerExtension : IFormatProvider, ICustomFormatter
    {
        private readonly IFormatProvider parentProvider;

        public CustomerExtension() : this(CultureInfo.CurrentCulture) { }
        public CustomerExtension(IFormatProvider parent)
        {
            parentProvider = parent;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return parentProvider.GetFormat(formatType);
        }

        /// <summary>
        /// Return string representation of Customer in special format and culture
        /// </summary>
        /// <param name="format">string format</param>
        /// <param name="arg">customer to be formatted</param>
        /// <param name="formatProvider">format provider</param>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!(arg is Customer)) throw new ArgumentException($"{nameof(arg)} isn't a customer");
            if (ReferenceEquals(formatProvider, null))
                formatProvider = parentProvider;

            Customer customer = (Customer)arg;

            switch (format.ToUpperInvariant())
            {
                case "NP": return $"Customer record: {customer.Name}, {customer.ContactPhone}";
                case "NR": return $"Customer record: {customer.Name},{customer.Revenue.ToString("N1", formatProvider)}";
                case "PR": return $"Customer record: {customer.ContactPhone}, {customer.Revenue.ToString("N1", formatProvider)}";
                default: throw new FormatException($"The {format} format string is not supported.");
            }
        }
    }
}
