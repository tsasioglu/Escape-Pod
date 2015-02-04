using EscapePod.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EscapePod.Tests
{
    [TestClass]
    public class CrashReporterTests
    {
        private CrashReporter crashReporter;

        [TestInitialize]
        public void SetUp()
        {
            crashReporter = new CrashReporter();
        }

        [TestMethod]
        public void TestMethod1()
        {
            crashReporter.Report("test error");
        }
    }
}
