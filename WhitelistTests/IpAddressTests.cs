using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whitelist;

namespace WhitelistTests
{
    [TestClass]
    public class IpAddressTests
    {
        [TestMethod]
        public void TryParse_Null()
        {
            // Setup
            string input = null;

            // Action
            IpAddress result;
            var success = IpAddress.TryParse(input, out result);

            // Verify
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TryParse_IncorrectFormat()
        {
            // Setup
            string input = "1231";

            // Action
            IpAddress result;
            var success = IpAddress.TryParse(input, out result);

            // Verify
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TryParse_MaxValue()
        {
            // Setup
            var expected = new IpAddress(uint.MaxValue);
            string input = "255.255.255.255";

            // Action
            IpAddress result;
            var success = IpAddress.TryParse(input, out result);

            // Verify
            Assert.IsTrue(success);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryParse_MinValue()
        {
            // Setup
            var expected = new IpAddress(0);
            string input = "0.0.0.0";

            // Action
            IpAddress result;
            var success = IpAddress.TryParse(input, out result);

            // Verify
            Assert.IsTrue(success);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryParse_NegativeValues()
        {
            // Setup
            string input = "-10.-10.-10.-10";

            // Action
            IpAddress result;
            var success = IpAddress.TryParse(input, out result);

            // Verify
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TryParse_ValidValue()
        {
            // Setup
            var expected = new IpAddress(3232238081);
            string input = "192.168.10.1";

            // Action
            IpAddress result;
            var success = IpAddress.TryParse(input, out result);

            // Verify
            Assert.IsTrue(success);
            Assert.AreEqual(expected, result);
        }
    }
}
