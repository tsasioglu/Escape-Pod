using System.Globalization;
using EscapePod.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EscapePod.Tests
{
    [TestClass]
    public class LongDateToShortDateConverterTests
    {
        private LongDateToShortDateConverter _converter;

        [TestInitialize]
        public void SetUp()
        {
            _converter = new LongDateToShortDateConverter();
        }

        [TestMethod]
        public void Convert_ReturnsInput_WhenInputNull()
        {
            const string longDate = null;

            var shortDate = _converter.Convert(longDate, typeof (string), null, CultureInfo.CurrentCulture);

            Assert.Equals(longDate, shortDate);
        }

        [TestMethod]
        public void Convert_ReturnsInput_WhenInputEmpty()
        {
            const string longDate = null;

            var shortDate = _converter.Convert(longDate, typeof (string), null, CultureInfo.CurrentCulture);

            Assert.Equals(longDate, shortDate);
        }

        [TestMethod]
        public void Convert_ReturnsInput_WhenInputOfWrongType()
        {
            const string longDate = "Thrusday the 18th";

            var shortDate = _converter.Convert(longDate, typeof (string), null, CultureInfo.CurrentCulture);

            Assert.Equals(longDate, shortDate);
        }

        [TestMethod]
        public void Convert_ReturnsShortDate()
        {
            const string longDate = "Fri, 08 Aug 2014 08:10:00 +0000";

            var shortDate = _converter.Convert(longDate, typeof (string), null, CultureInfo.CurrentCulture);

            Assert.Equals("Fri, 08 Aug 2014", shortDate);
        }

        
    }
}
