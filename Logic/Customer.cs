using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Class for working with Customer information
    /// </summary>
    public class Customer : IFormattable
    {
        private string name;
        private string contactPhone;
        private decimal revenue;

        public string Name
        {
            get { return name; }
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(Name));
                if (value == string.Empty) throw new ArgumentException(nameof(Name));
                name = value;
            }
        }
        public string ContactPhone
        {
            get { return contactPhone; }
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(ContactPhone));
                contactPhone = value;
            }
        }
        public decimal Revenue
        {
            get { return revenue; }
            private set
            {
                if (value < 0) throw new ArgumentException(nameof(Revenue));
                revenue = value;
            }
        }


        public Customer(string name, string phone, decimal revenue)
        {
            Name = name;
            ContactPhone = phone;
            Revenue = revenue;
        }

        /// <summary>
        /// Return string representation of Customer in special format and culture
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "G";
            if (ReferenceEquals(formatProvider, null)) formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G": return $"Customer record: {Name}, {ContactPhone}, {Revenue.ToString("N1", formatProvider)}";
                case "N": return $"Customer record: {Name}";
                case "P": return $"Customer record: {ContactPhone}";
                case "R": return $"Customer record: {Revenue.ToString("N1", formatProvider)}";
                default: throw new FormatException($"The {format} format string is not supported.");
            }
        }

        /// <summary>
        /// Return string representation of Customer
        /// </summary>
        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);
        /// <summary>
        /// Return string representation of Customer in special format
        /// </summary>
        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);
    }
}
