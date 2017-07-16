using System;
using NUnit.Framework;

namespace Logic.Tests
{
    public class CustomerTest
    {
        [TestCase("", null, ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100, 1 000 000,0")]
        [TestCase("G", null, ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100, 1 000 000,0")]
        [TestCase("P", null, ExpectedResult = "Customer record: +1 (425) 555-0100")]
        [TestCase("N", null, ExpectedResult = "Customer record: Jeffrey Richter")]
        [TestCase("R", null, ExpectedResult = "Customer record: 1 000 000,0")]
        public string ToString_PositiveTest(string format, IFormatProvider formatProvider)
        {
            Customer c = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            return c.ToString(format, formatProvider);
        }

        [TestCase()]
        public void ToString_InvalidFormat_FormatException()
        {
            Assert.Throws<FormatException>(
                () => new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000).ToString("I need your exception"));
        }
    }

    public class CustomerExtensionTest
    {
        [TestCase("NP", null, ExpectedResult ="Customer record: Jeffrey Richter, +1 (425) 555-0100")]
        [TestCase("NR", null, ExpectedResult = "Customer record: Jeffrey Richter,1 000 000,0")]
        [TestCase("PR", null, ExpectedResult = "Customer record: +1 (425) 555-0100, 1 000 000,0")]
        public string ToString_PositiveTest(string format, IFormatProvider formatProvider)
        {
            CustomerExtension ce = new CustomerExtension();
            Customer c = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            return ce.Format(format, c, formatProvider);
        }

        [TestCase()]
        public void ToString_InvalidFormat_ThrowsFormatException()
        {
            Customer c = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            Assert.Throws<FormatException>(() => c.ToString("1++", null));
        }

    }
}
